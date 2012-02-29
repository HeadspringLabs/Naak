using System.Collections.Generic;
using HtmlAgilityPack;

namespace Naak.HtmlRules.Default
{
    [HtmlRule]
	public class ImageInputsHaveAltText : IHtmlRule
	{
		public IEnumerable<ValidationError> ValidateHtml(HtmlDocument document)
		{
			var records = new List<ValidationError>();

			const string formElementXPath = "//input[@type='image'][not(@alt) or @alt='']";

			var imageButtonsWithoutAlt = document.GetNodes(formElementXPath);

			foreach (var imageButton in imageButtonsWithoutAlt)
			{
				string message = string.Format("Image input missing alt text: {0}", imageButton.OuterHtml);
                records.Add(new ValidationError(message, imageButton.Line, imageButton.LinePosition));
			}

			return records;
		}
	}
}