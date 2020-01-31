using IELTSPayments.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IELTSPayments.Models
{
    public class SystemSettings
    {
        public SystemSettings()
        {
            Greeting = Identity.GetGreeting();
        }

        public string Greeting { get; set; }

        public string UserName { get; set; }

        [Display(Name = "System Version Number")]
        public string Version { get; set; }
        public int MaxRecords { get; set; }
    }
}
