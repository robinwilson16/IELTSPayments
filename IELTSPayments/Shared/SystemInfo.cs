using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IELTSPayments.Shared
{
    public class SystemInfo
    {
        public static string GetDatabase(IConfiguration configuration)
        {
            string systemDatabase = 
                configuration.GetSection("ConnectionStrings").GetValue<string>("DefaultConnection", "Unknown");

            string[] systemDatabaseDetails = systemDatabase.Split(";");

            string databaseDetails = "";
            char conSeperator = '=';
            int conSeperatorLoc = 0;
            string server = "";
            string database = "";

            foreach (var conDetail in systemDatabaseDetails)
            {
                conSeperatorLoc = conDetail.IndexOf(conSeperator);

                if (conSeperatorLoc > 0 ) {
                    if(conDetail.Substring(0, conSeperatorLoc) == "Server") {
                        server = conDetail.Substring(conSeperatorLoc + 1, conDetail.Length - conSeperatorLoc - 1);
                    }
                    else if (conDetail.Substring(0, conSeperatorLoc) == "Database")
                    {
                        database = conDetail.Substring(conSeperatorLoc + 1, conDetail.Length - conSeperatorLoc - 1);
                    }
                }
            }

            if(server.Length > 0 && database.Length > 0)
            {
                databaseDetails = server + "." + database;
            }
            else
            {
                databaseDetails = "Invalid Database!";
            }

            return databaseDetails;
        }
    }
}
