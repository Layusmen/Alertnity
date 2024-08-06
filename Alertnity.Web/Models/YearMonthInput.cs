using Microsoft.AspNetCore.Components.Forms;
using Blazorise;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Globalization;

namespace Alertnity.Web.Models
{
    public class YearMonthInput : InputBase<string>
    {

            protected override void BuildRenderTree(RenderTreeBuilder builder)
            {
                builder.OpenElement(0, "input");
                builder.AddAttribute(1, "type", "text");
                builder.AddAttribute(2,
     "class", "form-control");
                builder.AddAttribute(3,
     "value", BindConverter.FormatValue(CurrentValueAsString));
                builder.AddAttribute(4, "onchange", EventCallback.Factory.CreateBinder<string>(this, __value => CurrentValueAsString
     = __value, CurrentValueAsString));
                builder.CloseElement();
            }



        protected override string FormatValueAsString(string value)
        {
            return value;
        }

        protected override bool TryParseValueFromString(string? value, out string result, out string? validationErrorMessage)
        {
            // Implement your validation logic here
            if (string.IsNullOrEmpty(value))
            {
                result = null;
                validationErrorMessage = "Year and month are required.";
                return false;
            }

            // Check if the input is in YYYY-MM format
            if (!DateTime.TryParseExact(value, "yyyy-MM", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDate))
            {
                result = null;
                validationErrorMessage = "Invalid year and month format. Use YYYY-MM.";
                return false;
            }

            result = value; // Assuming you want to store the value as a string
            validationErrorMessage = null;
            return true;
        }

    }
}