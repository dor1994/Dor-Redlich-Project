using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RateFetcherService.Models;

namespace RateFetcherService.Repository.Interfaces
{
    public interface IExchangeRateRepository
    {
        Task SaveExchangeRatesAsync(IEnumerable<ExchangeRate> rates);
        Task<IEnumerable<ExchangeRate>> GetAllExchangeRatesAsync();
        Task<ExchangeRate> GetExchangeRateAsync(string currencyPair);
    }
}
