using RateFetcherService.Models;

namespace RatePrinter.Services.Interfaces
{
    public interface IExchangeRateService
    {
        Task<IEnumerable<ExchangeRate>> GetAllExchangeRatesAsync();
        Task<ExchangeRate> GetExchangeRateAsync(string currencyPair);
    }
}
