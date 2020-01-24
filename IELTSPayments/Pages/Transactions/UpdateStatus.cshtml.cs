using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IELTSPayments.Data;
using IELTSPayments.Models;
using IELTSPayments.Shared;
using Microsoft.Extensions.Configuration;

namespace IELTSPayments.Pages.Transactions
{
    public class UpdateStatusModel : PageModel
    {
        private readonly IELTSPayments.Data.ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public UpdateStatusModel(IELTSPayments.Data.ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public string UserDetails { get; set; }
        public string UserGreeting { get; set; }
        public string SystemVersion { get; set; }
        public string SystemDatabase { get; set; }
        public string Browser { get; set; }

        [BindProperty]
        public IELTSTransaction IELTSTransaction { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //IELTSTransaction = await _context.IELTSTransaction.FirstOrDefaultAsync(m => m.PaymentID == id);

            IELTSTransaction = (await _context.IELTSTransaction
                .FromSqlInterpolated($"EXEC SPR_IEL_Transactions @ReportType=null, @BritishCouncilRef=null, @Email=null, @PaymentDateFrom=null, @PaymentDateTo=null, @ActionsRequired=null, @TransactionID={id}")
                .ToListAsync())
                .FirstOrDefault();

            if (IELTSTransaction == null)
            {
                return NotFound();
            }

            UserDetails = await Identity.GetFullName(User.Identity.Name.Split('\\').Last(), _context, _configuration);
            UserGreeting = Identity.GetGreeting();
            SystemVersion = _configuration["Version"];
            SystemDatabase = SystemInfo.GetDatabase(_configuration);
            Browser = Request.Headers["User-Agent"];

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            string result = "";

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(IELTSTransaction).State = EntityState.Modified;

            try
            {
                //await _context.SaveChangesAsync();
                string UserName = User.Identity.Name.Split('\\').Last();

                await _context.Database
                    .ExecuteSqlInterpolatedAsync($"EXEC SPR_IEL_UpdateStatus @TransactionID={IELTSTransaction.TransactionID}, @IsBookSent={IELTSTransaction.BookSent}, @IsDVDSent={IELTSTransaction.DVDSent}, @UserName={UserName}");

                result = "{ saved: \"Y\", error: \"\" }";
            }
            catch (DbUpdateConcurrencyException e)
            {
                //if (!IELTSTransactionExists(IELTSTransaction.PaymentID))
                //{
                //    return NotFound();
                //}
                //else
                //{
                //    throw;
                //}

                result = "{ saved: \"N\", error: " + e.Message + " }";
            }

            //return RedirectToPage("./Index");

            return Content(result);
        }

        private bool IELTSTransactionExists(int id)
        {
            return _context.IELTSTransaction.Any(e => e.PaymentID == id);
        }
    }
}
