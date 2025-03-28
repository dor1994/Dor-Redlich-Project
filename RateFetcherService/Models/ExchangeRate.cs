namespace RateFetcherService.Models
{
    public class ExchangeRate
    {
        public string PairName { get; set; }
        public decimal Rate { get; set; }
        public DateTime LastUpdateTime { get; set; }

        // Validation method to ensure data integrity
        public bool IsValid() =>
            !string.IsNullOrEmpty(PairName) &&
            Rate > 0 &&
            LastUpdateTime != default;
    }
}
