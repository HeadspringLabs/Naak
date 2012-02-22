using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;

namespace Naak.HtmlRules
{
	public class AccessibilityRenderer
	{
	    public object Render(string html, IEnumerable<IHtmlRule> rules)
		{
            var accessibilityErrors = GetAccessibilityErrors(html, rules);

            return RenderHtml(accessibilityErrors);
		}

        public ValidationError[] GetAccessibilityErrors(string html, IEnumerable<IHtmlRule> rules)
        {
            var records = new List<ValidationError>();

            try
            {
                var document = BuildDocument(html);

                records.AddRange(rules.SelectMany(htmlRule => htmlRule.ValidateHtml(document)));
            }
            catch (Exception exc)
            {
                records.Add(new ValidationError(exc.Message));
            }

            return records.ToArray();
        }

        private static HtmlDocument BuildDocument(string html)
        {
            var document = new HtmlDocument();
            document.LoadHtml(html);
            return document;
        }
        
        private static object RenderHtml(IEnumerable<ValidationError> records)
		{
            if (records != null && records.Any())
            {
                var data = new List<object[]> { new object[] { "Message", "LineNumber", "Position" } };
                data.AddRange(records.Select(record => new object[] { record.Message, record.LineNumber, record.LinePosition, "error" }));
                return data;
            }
            return "All accessibility rules passed.";
		}
	}
}