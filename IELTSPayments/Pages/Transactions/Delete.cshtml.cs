using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using IELTSPayments.Data;
using IELTSPayments.Models;

namespace IELTSPayments.Pages.Transactions
{
    public class DeleteModel : PageModel
    {
        private readonly IELTSPayments.Data.ApplicationDbContext _context;

        public DeleteModel(IELTSPayments.Data.ApplicationDbContext context)
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            IELTSTransaction = await _context.IELTSTransaction.FindAsync(id);

            if (IELTSTransaction != null)
            {
                _context.IELTSTransaction.Remove(IELTSTransaction);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
