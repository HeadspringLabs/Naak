using System.Collections.Generic;
using HtmlAgilityPack;

namespace Naak.HtmlRules.Default
{
    [HtmlRule]
	public class AtLeastOneH1 : IHtmlRule
	{
        public IEnumerable<ValidationError> ValidateHtml(HtmlDocument document)
		{
			var records = new List<ValidationError>();

			var nodes = document.GetNodes("//h1");

			if (nodes.Count == 0)
			{
				records.Add(new ValidationError("There must be at least one H1 tag on the page"));
			}

			return records;
		}
	}
}