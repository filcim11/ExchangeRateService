using System.Collections.Generic;

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
