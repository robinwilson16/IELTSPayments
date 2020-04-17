using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IELTSPayments.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace IELTSPayments.Pages
{
    public class AccessDeniedModel : PageModel
    {
        private readonly IELTSPayments.Data.ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public AccessDeniedModel(IELTSPayments.Data.ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public string UserDetails { get; set; }
        public string SystemDatabase { get; set; }

        public async Task OnGetAsync()
        {
            UserDetails = await Identity.GetFullName(User.Identity.Name.Split('\\').Last(), _context, _configuration);
            SystemDatabase = SystemInfo.GetDatabase(_configuration);
        }
    }
}