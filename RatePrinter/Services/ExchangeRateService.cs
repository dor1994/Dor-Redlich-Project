using RateFetcherService.Models;
using RateFetcherService.Repository.Interfaces;
using RatePrinter.Services.Interfaces;

namespace RatePrinter.Services
{
    public class ExchangeRateService : IExchangeRateService
    {
        private readonly IExchangeRateRepository _repository;

        public ExchangeRateService(IExchangeRateRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<ExchangeRate>> GetAllExchangeRatesAsync()
        {
            return await _repository.GetAllExchangeRatesAsync();
        }

        public async Task<ExchangeRate> GetExchangeRateAsync(string currencyPair)
        {
            return await _repository.GetExchangeRateAsync(currencyPair);
        }
    }
}
