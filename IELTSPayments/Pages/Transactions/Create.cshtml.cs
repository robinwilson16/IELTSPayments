using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using IELTSPayments.Data;
using IELTSPayments.Models;
using Microsoft.AspNetCore.Authorization;

namespace IELTSPayments.Pages.Transactions
{
    [Authorize(Roles = "IELTS Payments")]
    public class CreateModel : PageModel
    {
        private readonly IELTSPayments.Data.ApplicationDbContext _context;

        public CreateModel(IELTSPayments.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public IELTSTransaction IELTSTransaction { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.IELTSTransaction.Add(IELTSTransaction);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}