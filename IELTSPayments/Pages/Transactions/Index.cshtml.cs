using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using IELTSPayments.Data;
using IELTSPayments.Models;
using IELTSPayments.Shared;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace IELTSPayments.Pages.Transactions
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

        public IList<IELTSTransaction> IELTSTransaction { get;set; }

        public async Task OnGetAsync(
            string britishCouncilRef,
            string reportType,
            string email,
            DateTime? paymentDateFrom,
            DateTime? paymentDateTo
            )
        {
            //IELTSTransaction = await _context.IELTSTransaction.ToListAsync();

            IELTSTransaction = await _context.IELTSTransaction
                .FromSqlInterpolated($"EXEC SPR_IEL_Transactions @ReportType={reportType}, @BritishCouncilRef={britishCouncilRef}, @Email={email}, @PaymentDateFrom={paymentDateFrom}, @PaymentDateTo={paymentDateTo}")
                .ToListAsync();

            UserDetails = await Identity.GetFullName(User.Identity.Name.Split('\\').Last(), _context, _configuration);
            UserGreeting = Identity.GetGreeting();
            SystemVersion = _configuration["Version"];
            SystemDatabase = SystemInfo.GetDatabase(_configuration);
            Browser = Request.Headers["User-Agent"];
        }

        public async Task<JsonResult> OnGetJsonAsync(
            string britishCouncilRef, 
            string reportType, 
            string email,
            DateTime? paymentDateFrom,
            DateTime? paymentDateTo
            )
        {

            IELTSTransaction = await _context.IELTSTransaction
                .FromSqlInterpolated( $"EXEC SPR_IEL_Transactions @ReportType={reportType}, @BritishCouncilRef={britishCouncilRef}, @Email={email}, @PaymentDateFrom={paymentDateFrom}, @PaymentDateTo={paymentDateTo}")
                .ToListAsync();

            var collectionWrapper = new
            {
                transactions = IELTSTransaction
            };

            return new JsonResult(IELTSTransaction);
        }
    }
}
