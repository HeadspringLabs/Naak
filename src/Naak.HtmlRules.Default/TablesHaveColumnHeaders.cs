using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;

namespace Naak.HtmlRules.Default
{
    [HtmlRule]
	public class TablesHaveColumnHeaders : IHtmlRule
	{
		public IEnumerable<ValidationError> ValidateHtml(HtmlDocument document)
		{
			var records = new List<ValidationError>();

			var nodes = document.GetNodes("//table");

			if (nodes != null)
				foreach (var node in nodes)
				{
                    var tableHeaders = node.GetNodes("tr/th");
                    var tableHeadersWithThead = node.GetNodes("thead/tr/th");
                    if (!tableHeaders.Any() && !tableHeadersWithThead.Any())
					{
						string message =
							string.Format(
								"Layout table detected - if the table is a data table, use TH for the column or row headers. Otherwise, use CSS for layout: {0}",
								node.OuterHtml);
						records.Add(new ValidationError(message));
					}
				}
			return records;
		}
	}
}