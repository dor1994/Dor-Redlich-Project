using System.ComponentModel.DataAnnotations;

namespace GrpcShareService.Models
{
    public class ExchangeRate
    {
        [Required]
        public string PairName { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Rate { get; set; }

        public DateTime LastUpdateTime { get; set; }

        // Validation method
        public bool Validate()
        {
            return !string.IsNullOrWhiteSpace(PairName)
                   && Rate > 0
                   && LastUpdateTime != default;
        }
    }
}
