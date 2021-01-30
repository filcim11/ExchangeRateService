using Crayon_ExchangeRateService.Extensions;
using Crayon_ExchangeRateService.ExternalServices;
using Crayon_ExchangeRateService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Crayon_ExchangeRateService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExchangeRatesController : ControllerBase
    {
        private readonly IExchangeRateService _exchangeRateService;

        public ExchangeRatesController(IExchangeRateService exchangeRateService)
        {
            _exchangeRateService = exchangeRateService;
        }

        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return Ok();
        }

        public async Task<IActionResult> GetExchangeRateResult([FromQuery] ExchangeRateInput rateInput)
        {
            rateInput.ValidateInputRate();
            rateInput.SetMinMaxDate();

            var response = await _exchangeRateService.ExchangeRateResponse(rateInput);

            if (response.IsSuccessStatusCode)
            {
                var objectResult = response.Content.ReadAsStringAsync().Result;
                var rateOutput = JsonConvert.DeserializeObject<ExchangeRateOutput>(objectResult);

                rateOutput.FilterRateOutput(rateInput.DatesArray);

                return Ok(new { rateOutput.MinRate, rateOutput.MaxRate, rateOutput.AvgRate });
            }

            throw new Exception("Internal Service Error");
        }
    }
}
