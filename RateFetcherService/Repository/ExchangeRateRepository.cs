using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using RateFetcherService.Models;
using RateFetcherService.Repository.Interfaces;

namespace RateFetcherService.Repository
{
    public class ExchangeRateRepository : IExchangeRateRepository
    {
        private readonly string _filePath;
        private readonly object _fileLock = new object();

        public ExchangeRateRepository(string filePath)
        {
            _filePath = filePath;
            // Ensure file exists
            if (!File.Exists(_filePath))
            {
                File.WriteAllText(_filePath, "[]");
            }
        }

        public async Task SaveExchangeRatesAsync(IEnumerable<ExchangeRate> rates)
        {
            var jsonContent = JsonSerializer.Serialize(rates, new JsonSerializerOptions { WriteIndented = true });

            lock (_fileLock)
            {
                File.WriteAllText(_filePath, jsonContent);
            }
        }

        public async Task<IEnumerable<ExchangeRate>> GetAllExchangeRatesAsync()
        {
            lock (_fileLock)
            {
                var jsonContent = File.ReadAllText(_filePath);
                return JsonSerializer.Deserialize<List<ExchangeRate>>(jsonContent)
                    ?? new List<ExchangeRate>();
            }
        }

        public async Task<ExchangeRate> GetExchangeRateAsync(string currencyPair)
        {
            var rates = await GetAllExchangeRatesAsync();
            return rates.FirstOrDefault(r => r.PairName == currencyPair);
        }
    }
}
