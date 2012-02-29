using System.Collections.Generic;
using HtmlAgilityPack;

namespace Naak.HtmlRules
{
    public interface IHtmlRule
	{
		IEnumerable<ValidationError> ValidateHtml(HtmlDocument document);
	}
}