using Crayon_ExchangeRateService.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Crayon_ExchangeRateService.ExternalServices
{
    public class ExchangeRateService : IExchangeRateService
    {
        private readonly IConfiguration _configuration;

        public ExchangeRateService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public async Task<HttpResponseMessage> ExchangeRateResponse(ExchangeRateInput rateInput)
        {
            using (var client = new HttpClient())
            {
                //Passing service base url  
                var baseUri = _configuration.GetSection("BaseUri").Get<string>();
                client.BaseAddress = new Uri(baseUri);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetDepartments using HttpClient
                return await client
                        .GetAsync(@$"history?start_at={rateInput.MinDate}&end_at={rateInput.MaxDate}&base={rateInput.BaseCurr}&symbols={rateInput.TargetCurr}");
            }
        }
    }
}
