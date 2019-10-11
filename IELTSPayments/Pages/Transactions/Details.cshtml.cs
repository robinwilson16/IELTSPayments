using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using IELTSPayments.Data;
using IELTSPayments.Models;
using System.Data.SqlClient;
using IELTSPayments.Shared;
using Microsoft.Extensions.Configuration;

namespace IELTSPayments.Pages.Transactions
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public DetailsModel(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public IELTSPayment IELTSPayment { get; set; }
        public string UserDetails { get; set; }
        public string UserGreeting { get; set; }
        public string SystemVersion { get; set; }
        public string SystemDatabase { get; set; }
        public string Browser { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //IELTSTransaction = await _context.IELTSTransaction.FirstOrDefaultAsync(m => m.PaymentID == id);

            IELTSPayment = await _context.IELTSPayment
                .FromSqlInterpolated($"EXEC SPR_IEL_Payment @TransactionID={id}")
                .FirstOrDefaultAsync();

            UserDetails = await Identity.GetFullName(User.Identity.Name.Split('\\').Last(), _context, _configuration);
            UserGreeting = Identity.GetGreeting();
            SystemVersion = _configuration["Version"];
            SystemDatabase = SystemInfo.GetDatabase(_configuration);
            Browser = Request.Headers["User-Agent"];

            if (IELTSPayment == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
