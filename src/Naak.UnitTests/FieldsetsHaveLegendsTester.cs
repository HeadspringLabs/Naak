using System.Text;
using NUnit.Framework;
using Naak.HtmlRules.Default;

namespace Naak.UnitTests
{
	[TestFixture]
	public class FieldsetsHaveLegendsTester
	{
		[Test]
		public void Identifies_fieldsets_missing_legends()
		{
			var html = new StringBuilder();
			html.Append(@"<form>");
			html.Append(@"  <fieldset>");
			html.Append(@"    <legend>Search Fields</legend>");
			html.Append(@"  </fieldset>");
			html.Append(@"</form>");

			html.Append(@"<form>");
			html.Append(@"  <fieldset>");
			html.Append(@"    <legend />");
			html.Append(@"  </fieldset>");
			html.Append(@"</form>");

			html.Append(@"<form>");
			html.Append(@"  <fieldset />");
			html.Append(@"</form>");

			var errors = new FieldsetsHaveLegends().ValidateHtml(html);

			Assert.That(errors.Length, Is.EqualTo(2));
		}

		[Test]
		public void Correctly_validates_document_with_h1_tag()
		{
			const string bodyHtml = "<h1>Sample Heading</h1>";

			var errors = new AtLeastOneH1().ValidateHtml(bodyHtml);
            CollectionAssert.IsEmpty(errors);
		}
	}
}