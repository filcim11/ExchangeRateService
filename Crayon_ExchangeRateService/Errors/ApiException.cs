using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Crayon_ExchangeRateService.Errors
{
    public class ApiException : Exception
    {
        public ApiException(HttpStatusCode statusCode, string error)
        {
            StatusCode = statusCode;
            Error = error;
        }

        public HttpStatusCode StatusCode { get; set; }
        public string Error { get; set; }
    }
}
