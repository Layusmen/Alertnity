using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Alertnity.Web.Models
{
    public class CrimeCheckModel
    {
        [Required]
        [StringLength(10, ErrorMessage = "Postcode too long (must not be more than 16 character).")]
        public string Postcode { get; set; }

        [Required(ErrorMessage = "The date field is required.")]
        [RegularExpression(@"^\d{4}-(0[1-9]|1[0-2])$", ErrorMessage = "Invalid date format. Use YYYY-MM.")]
        public string DateString { get; set; }

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
