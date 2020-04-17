using IELTSPayments.Data;
using IELTSPayments.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IELTSPayments.Shared
{
    public class Identity
    {
        public static StaffMember StaffMember { get; set; }

        public static string GetUserName(ClaimsPrincipal user, ApplicationDbContext _context)
        {
            var userName = user.Identity.Name.ToString();

            //In case cannot obtain current user then set to this default user as created by field is required
            userName = "UNKNOWN";

            return userName;
        }
        //public static StaffMember StaffMember { get; set; }
        public static async Task<string> GetFullName(string username, ApplicationDbContext _context, IConfiguration _configuration)
        {
            StaffMember = (await _context.StaffMember
                .FromSqlInterpolated($"EXEC SPR_IEL_GetStaffMember @UserName={username}")
                .ToListAsync())
                .FirstOrDefault();

            if (StaffMember != null)
            {
                return StaffMember.StaffDetails;
            }
            else
            {
                return username;
            }
        }


        public static string GetGreeting()
        {
            string greeting = "";
            int currentHour = DateTime.Now.Hour;

            if (currentHour < 12)
            {
                greeting = "Good Morning";
            }
            else if (currentHour < 17)
            {
                greeting = "Good Afternoon";
            }
            else
            {
                greeting = "Good Evening";
            }

            return greeting;
        }
    }
}
