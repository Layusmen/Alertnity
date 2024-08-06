using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Alertnity.Web.Models
{
    public class CrimeCheckModel
    {
        [Required]
        public string Postcode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Date is required.")]
        [RegularExpression(@"\d{4}-\d{2}", ErrorMessage = "Date must be in the format YYYY-MM.")]
        public string DateString { get; set; } = string.Empty;

        [Required]
        public DateTime Date
        {
            get
            {
                return DateTime.ParseExact(DateString, "yyyy-MM", CultureInfo.InvariantCulture);
            }
        }
    }
}
