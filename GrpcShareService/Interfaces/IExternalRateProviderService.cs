using GrpcShareService.Models;

namespace GrpcShareService.Interfaces
{
    public interface IExternalRateProviderService
    {
        Task<IEnumerable<ExchangeRate>> FetchCurrentRatesAsync(string[] currencyPairs);
    }
}
