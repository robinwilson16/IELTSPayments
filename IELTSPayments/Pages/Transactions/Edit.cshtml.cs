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
using Microsoft.AspNetCore.Authorization;

namespace IELTSPayments.Pages.Transactions
{
    [Authorize(Roles = "IELTS Payments")]
    public class EditModel : PageModel
    {
        private readonly IELTSPayments.Data.ApplicationDbContext _context;

        public EditModel(IELTSPayments.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public IELTSTransaction IELTSTransaction { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            IELTSTransaction = await _context.IELTSTransaction.FirstOrDefaultAsync(m => m.PaymentID == id);

            if (IELTSTransaction == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(IELTSTransaction).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IELTSTransactionExists(IELTSTransaction.PaymentID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool IELTSTransactionExists(int id)
        {
            return _context.IELTSTransaction.Any(e => e.PaymentID == id);
        }
    }
}
