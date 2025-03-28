namespace GrpcShareService.Interfaces
{
    public interface IExchangeRateRepository
    {
        Task SaveExchangeRatesAsync(IEnumerable<ExchangeRate> rates);
        Task<IEnumerable<ExchangeRate>> GetAllExchangeRatesAsync();
        Task<ExchangeRate> GetExchangeRateAsync(string currencyPair);
    }
}
