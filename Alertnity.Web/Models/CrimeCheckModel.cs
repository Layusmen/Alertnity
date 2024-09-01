using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Alertnity.Web.Models
{
    public class CrimeCheckModel
    {
        [Required(ErrorMessage = "Postcode is required.")]
        public string Postcode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Date is required.")]
        public DateTime? Date { get; set; }

        public string GetDateString()
        {
            return Date?.ToString("yyyy-MM") ?? string.Empty;
        }

    }
}
