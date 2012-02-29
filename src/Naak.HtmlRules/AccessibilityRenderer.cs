using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;

namespace Naak.HtmlRules
{
	public class AccessibilityRenderer
	{
        public IEnumerable<ValidationError> GetAccessibilityErrors(string html, IEnumerable<IHtmlRule> rules)
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

            return records;
        }

        private static HtmlDocument BuildDocument(string html)
        {
            var document = new HtmlDocument();
            document.LoadHtml(html);
            return document;
        }
	}
}