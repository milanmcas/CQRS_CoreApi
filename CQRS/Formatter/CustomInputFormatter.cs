using Microsoft.AspNetCore.Mvc.Formatters;
using System.Text;

namespace CQRS.Formatter
{
    public class CustomInputFormatter : TextInputFormatter
    {
        public override Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context, Encoding encoding)
        {
            throw new NotImplementedException();
        }
    }
}
