using System.Collections.Generic;
using HtmlAgilityPack;

namespace Naak.HtmlRules.Default
{
    [HtmlRule]
	public class LabelsRelateToFormElements : IHtmlRule
	{
		public ValidationError[] ValidateHtml(HtmlDocument document)
		{
			var records = new List<ValidationError>();

			var labels = document.GetNodes("//label");

			foreach (var label in labels)
			{
				var forAttribute = label.Attributes["for"];
				HtmlNode relatedElement = null;

				if (forAttribute != null)
				{
					string labelId = forAttribute.Value;
					string xpath = string.Format("//input[@id='{0}']", labelId);

					relatedElement = document.GetNode(xpath);

					if (relatedElement == null)
					{
						xpath = string.Format("//select[@id='{0}']", labelId);
						relatedElement = document.GetNode(xpath);
					}

					if (relatedElement == null)
					{
						xpath = string.Format("//textarea[@id='{0}']", labelId);
						relatedElement = document.GetNode(xpath);
					}
				}

				if (relatedElement == null)
				{
					string message = string.Format("Label does not relate to a form control: {0}", label.OuterHtml);
					records.Add(new ValidationError(message));
				}
			}

			return records.ToArray();
		}
	}
}