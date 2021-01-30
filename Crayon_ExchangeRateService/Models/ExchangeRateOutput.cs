using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crayon_ExchangeRateService.Models
{
    public class ExchangeRateOutput
    {
        public Dictionary<string, Dictionary<string, double>> Rates { get; set; }
        public Dictionary<string, double> MinRate { get; set; }
        public Dictionary<string, double> MaxRate { get; set; }
        public double AvgRate { get; set; }
    }
}
