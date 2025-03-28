using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using RateFetcherService.Repository.Interfaces;
using RateFetcherService.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;

namespace RateFetcherService.Services
{
    public class ExchangeRateFetcherService : BackgroundService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IExchangeRateRepository _repository;
        private readonly ILogger<ExchangeRateFetcherService> _logger;
        private readonly IConfiguration _configuration;
        private readonly string _apiUrl;
        private static System.Timers.Timer _refreshTimer;
        private static readonly string[] CurrencyPairs =
        {
            "USD/ILS", "EUR/ILS", "GBP/ILS",
            "EUR/USD", "EUR/GBP"
        };

        public ExchangeRateFetcherService(
            IHttpClientFactory clientFactory,
            IExchangeRateRepository repository,
            ILogger<ExchangeRateFetcherService> logger,
            IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            _repository = repository;
            _logger = logger;
            _configuration = configuration;
            _apiUrl = _configuration["exchangeApi"];

            _refreshTimer = new System.Timers.Timer(1000); // 10 seconds
            _refreshTimer.Elapsed += async (sender, e) => await FetchExchangeRates();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _refreshTimer.Start();
            await Task.Delay(Timeout.Infinite, stoppingToken);
        }

        private async Task FetchExchangeRates()
        {
            var client = _clientFactory.CreateClient();
            var rates = new List<ExchangeRate>();

            foreach (var pair in CurrencyPairs)
            {
                try
                {
                    // Example with FreeForexAPI (replace with actual API call)
                    var response = await client.GetAsync($"{_apiUrl}{pair}");
                    response.EnsureSuccessStatusCode();

                    var readData = await response.Content.ReadFromJsonAsync<ExchangeRateFromApi>();
                    if (readData != null)
                    {
                        var rateData = new ExchangeRate
                        {
                            PairName = pair,
                            LastUpdateTime =  DateTime.Parse(readData.time_last_update_utc),
                            Rate = readData.conversion_rate,

                        };

                        rates.Add(rateData);

                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error fetching rate for {pair}: {ex.Message}");
                }
            }

            await _repository.SaveExchangeRatesAsync(rates);
        }
    }
}
