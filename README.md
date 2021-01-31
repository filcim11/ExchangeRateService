# ExchangeRateService
Exchange rate service is a small REST API service written in C# and ASP.NET in .NET 5. This service provides minimum, maximum and average exchange currency rate for given time period, base currency and target currency.

**Prerequisites**: [The .NET Core SDK](https://www.microsoft.com/net/download)

## Testing it out

1. Clone this repository
2. Build the solution using Visual Studio, VS Code, or on the command line `dotnet build`(run this command on `..{path}\Crayon_ExchangeRateService)`
3. Run the project. The API will start up on https://localhost:44347, or https://localhost:5001 with `dotnet run` or `dotnet watch run`
4. You can use HTTP client like [Postman](https://www.getpostman.com/), or test it on browser by calling  `https://localhost:5001/api/exchangeRates?dates=2018-02-01,2018-02-15,2018-03-01&baseCurr=SEK&targetCurr=NOK`

## Final words
Hope this will work out! :)
