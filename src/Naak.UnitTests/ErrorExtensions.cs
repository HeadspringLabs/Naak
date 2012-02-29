using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using Naak.HtmlRules;

namespace Naak.UnitTests
{
    public static class ErrorExtensions
    {
        public static bool ContainsError(this IEnumerable<ValidationError> errors, string  message)
        {
			return errors.Any(record => record.Message == message);
        }

        public static ValidationError[] ValidateHtml(this IHtmlRule rule, string bodyHtml)
        {
            var document = GetDocument(bodyHtml);

            return rule.ValidateHtml(document).ToArray();
        }

        public static ValidationError[] ValidateHtml(this IHtmlRule rule, StringBuilder bodyHtml)
        {
            return rule.ValidateHtml(bodyHtml.ToString());
        }

        private static HtmlDocument GetDocument(string body)
        {
            var builder = new StringBuilder();
            builder.AppendLine("<!DOCTYPE html PUBLIC \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">");
            builder.AppendLine("<html xmlns=\"http://www.w3.org/1999/xhtml\">");
            builder.AppendLine("  <head>");
            builder.AppendLine("    <title>Application</title>");
            builder.AppendLine("  </head>");
            builder.AppendLine("  <body>");
            builder.AppendLine("    <div id=\"container\">");
            builder.AppendFormat("      {0}", body);
            builder.AppendLine();
            builder.AppendLine("    </div>");
            builder.AppendLine("  </body>");
            builder.AppendLine("</html>");
            var html = builder.ToString();

            var document = new HtmlDocument();
            document.LoadHtml(html);
            return document;
        }
    }
}