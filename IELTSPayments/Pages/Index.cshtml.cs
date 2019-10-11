using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IELTSPayments.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;

namespace IELTSPayments.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IELTSPayments.Data.ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public IndexModel(IELTSPayments.Data.ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public string UserDetails { get; set; }
        public string UserGreeting { get; set; }
        public string SystemVersion { get; set; }
        public string SystemDatabase { get; set; }
        public string Browser { get; set; }

        public List<SelectListItem> ReportTypes;

        public async Task OnGetAsync()
        {
            ReportTypes = new List<SelectListItem>();
            ReportTypes.Add(new SelectListItem() { Text = "IELTS Only", Value = "a" });
            ReportTypes.Add(new SelectListItem() { Text = "Pre-Test", Value = "b" });
            ReportTypes.Add(new SelectListItem() { Text = "Post-Test", Value = "c" });

            ViewData["ReportTypeID"] = new SelectList(ReportTypes, "Value", "Text");

            UserDetails = await Identity.GetFullName(User.Identity.Name.Split('\\').Last(), _context, _configuration);
            UserGreeting = Identity.GetGreeting();
            SystemVersion = _configuration["Version"];
            SystemDatabase = SystemInfo.GetDatabase(_configuration);
            Browser = Request.Headers["User-Agent"];
        }
    }
}
