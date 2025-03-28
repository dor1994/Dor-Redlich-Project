using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using RateFetcherService.Models;
using RateFetcherService.Repository.Interfaces;
using RatePrinter.Services.Interfaces;

namespace RatePrinter.Controllers
{
    [ApiController]
    [Route("api/rates")]
    public class RatesController : ControllerBase
    {
        private readonly IExchangeRateService _exchangeRateService;

        public RatesController(IExchangeRateService exchangeRateService)
        {
            _exchangeRateService = exchangeRateService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExchangeRate>>> GetAllRates()
        {
            var rates = await _exchangeRateService.GetAllExchangeRatesAsync();
            return Ok(rates);
        }

        [HttpGet("{currencyPair}")]
        public async Task<ActionResult<ExchangeRate>> GetRateForPair(string currencyPair)
        {
            var rate = await _exchangeRateService.GetExchangeRateAsync(currencyPair);

            return rate != null
                ? Ok(rate)
                : NotFound($"No rate found for {currencyPair}");
        }
    }
}
