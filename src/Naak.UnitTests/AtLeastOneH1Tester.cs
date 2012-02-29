using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Naak.HtmlRules.Default;

namespace Naak.UnitTests
{
	[TestFixture]
	public class AtLeastOneH1Tester
	{
		[Test]
		public void Correctly_identifies_missing_h1_tag()
		{
			const string bodyHtml = "<span>Invalid Body</span>";

			var errors = new AtLeastOneH1().ValidateHtml(bodyHtml);

			Assert.That(errors.Length, Is.EqualTo(1));
			Assert.That(errors.ContainsError("There must be at least one H1 tag on the page"));
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