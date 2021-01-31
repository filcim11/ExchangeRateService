using Crayon_ExchangeRateService.Errors;
using Crayon_ExchangeRateService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace Crayon_ExchangeRateService.Extensions
{
    public static class ExchangeRateExtensions
    {

        public static void ValidateInputRate(this ExchangeRateInput rateInput)
        {
            // TODO - Probably the best way is to load all currencies on Startup.cs
            var currencies = new string[] { "CAD", "HKD", "USD", "PHP", "DKK", "HUF", "CZK", "GBP", "RON",
                "SEK", "IDR", "INR", "BRL", "RUB", "HRK", "JPY", "THB", "CHF", "EUR", "MYR", "BGN", "TRY",
                "CNY", "NOK", "NZD", "ZAR", "MXN", "SGD", "AUD", "ILS", "KRW", "PLN"};

            if (string.IsNullOrWhiteSpace(rateInput.Dates))
                throw new ApiException(HttpStatusCode.BadRequest, "Parameter 'dates' must not be empty");


            if (!Regex.IsMatch(rateInput.BaseCurr, @"[A-Z]{3}$") || !currencies.Contains(rateInput.BaseCurr))
                throw new ApiException(HttpStatusCode.BadRequest, $"Base currency {rateInput.BaseCurr} is not supported");

            if (!Regex.IsMatch(rateInput.TargetCurr, @"[A-Z]{3}$") || !currencies.Contains(rateInput.TargetCurr))
                throw new ApiException(HttpStatusCode.BadRequest, $"Target currency {rateInput.TargetCurr} is not supported");

            rateInput.DatesArray = rateInput.Dates.Trim().Split(',');

            if (!rateInput.DatesArray.IsDatesMatch())
                throw new ApiException(HttpStatusCode.BadRequest, "Parameter 'dates' needs to be in 'yyyy-MM-dd' format, ',' separated");
        }

        private static bool IsDatesMatch(this string[] dates)
        {
            foreach (var date in dates)
            {
                if (!Regex.IsMatch(date, @"^\d{4}\-(0[1-9]|1[012])\-(0[1-9]|[12][0-9]|3[01])$"))
                    return false;
            }
            return true;
        }

        public static void SetMinMaxDate(this ExchangeRateInput rateInput)
        {
            if (rateInput.DatesArray != null)
            {
                var datesList = new List<DateTime>();

                foreach (var date in rateInput.DatesArray)
                    datesList.Add(DateTime.Parse(date));

                if (datesList != null)
                {
                    rateInput.MinDate = datesList.Min().ToString("yyyy-MM-dd");
                    rateInput.MaxDate = datesList.Max().ToString("yyyy-MM-dd");
                }
            }
        }

        public static void FilterRateOutput(this ExchangeRateOutput rateOutput, string[] dates)
        {
            rateOutput.Rates = rateOutput.Rates.Where(x => dates.Contains(x.Key))
                    .ToDictionary(val => val.Key, val => val.Value);

            // Because it's always goding to be one value
            var rates = rateOutput.Rates.Values.Select(v => v.Values.FirstOrDefault());

            rateOutput.MinRate = rateOutput.Rates.Where(x => x.Value.ContainsValue(rates.Min()))
                .ToDictionary(k => k.Key, val => val.Value.Values.First());
            rateOutput.MaxRate = rateOutput.Rates.Where(x => x.Value.ContainsValue(rates.Max()))
                .ToDictionary(k => k.Key, val => val.Value.Values.First());
            rateOutput.AvgRate = rates.Average();
        }

        #region GetAllRatesOld
        //private static List<double> GetAllRates(this Dictionary<string, Dictionary<string, double>> rates)
        //{
        //    var ratesList = new List<double>();

        //    foreach (var rate in rates)
        //    {
        //        ratesList.AddRange(rate.Value.Values.ToList());
        //    }

        //    return ratesList;
        //}
        #endregion
    }
}
