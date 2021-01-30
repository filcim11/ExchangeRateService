using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Crayon_ExchangeRateService.Models
{
    public class ExchangeRateInput
    {
        public string Dates { get; set; }
        public string BaseCurr { get; set; } = "SEK";
        public string TargetCurr { get; set; } = "NOK";
        public string MinDate { get; set; }
        public string MaxDate { get; set; }

        public string[] DatesArray { get; set; }
    }


}
