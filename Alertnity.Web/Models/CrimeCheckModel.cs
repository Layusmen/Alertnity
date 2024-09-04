﻿using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Alertnity.Web.Models
{
    public class CrimeCheckModel
    {
        [Required(ErrorMessage = "Postcode is required.")]
        [RegularExpression(@"^[A-Z]{2}\d{1,2}[A-Z]?\d[A-Z]{2}$", ErrorMessage = "Invalid Postcode format. Please use the format PO148B.")]
        public string Postcode { get; set; }

        [Required(ErrorMessage = "Date is required.")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date format. Please use YYYY-MM.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM}")]
        public DateTime? Date { get; set; }

        public string GetDateString()
        {
            return Date?.ToString("yyyy-MM") ?? string.Empty;
        }

    }
}
