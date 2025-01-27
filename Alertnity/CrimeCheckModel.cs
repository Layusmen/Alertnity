using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Alertnity
{
    public class CrimeCheckModel
    {
        [Required(ErrorMessage = "Postcode is required.")]
        [RegularExpression(@"^[A-Z]{2}\d{1,2}[A-Z]?\d[A-Z]{2}$", ErrorMessage = "Invalid Postcode format. Please use the format PO148B.")]
        public string Postcode { get; set; }

        [Required(ErrorMessage = "Date is required.")]
        public IReadOnlyList<DateTime?> DateRange { get; set; } = new List<DateTime?>();
        public string GetStartDateString()
        {
            return DateRange.Count > 0 && DateRange[0].HasValue ? DateRange[0]?.ToString("yyyy-MM") : string.Empty;
        }
        public string GetEndDateString()
        {
            return DateRange.Count > 1 && DateRange[1].HasValue ? DateRange[1]?.ToString("yyyy-MM") : string.Empty;
        }


    }
}