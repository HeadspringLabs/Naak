using System.Text;
using Naak.HtmlRules.Default;
using NUnit.Framework;

namespace Naak.UnitTests
{
	[TestFixture]
	public class ImagesHaveAltTextTester
	{
		[Test]
		public void Identifies_images_missing_alt_text()
		{
			var bodyHtml = new StringBuilder();
			bodyHtml.Append(@"<img alt=""""/>");
			bodyHtml.Append(@"<img/>");
			bodyHtml.Append(@"<img alt=""Description""/>");

			var errors = new ImagesHaveAltText().ValidateHtml(bodyHtml);

            Assert.That(errors.Length, Is.EqualTo(2));
            Assert.That(errors.ContainsError(@"Image missing alt text: <img alt="""">"));
            Assert.That(errors.ContainsError(@"Image missing alt text: <img/>"));
		}
	}
}