using Crayon_ExchangeRateService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Crayon_ExchangeRateService.ExternalServices
{
    public interface IExchangeRateService
    {
        Task<HttpResponseMessage> ExchangeRateResponse(ExchangeRateInput rateInput);
    }
}
