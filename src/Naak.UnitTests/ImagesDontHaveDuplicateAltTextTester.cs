using System.Text;
using NUnit.Framework;
using Naak.HtmlRules.Default;

namespace Naak.UnitTests
{
	[TestFixture]
	public class ImagesDontHaveDuplicateAltTextTester
	{
		[Test]
		public void Identifies_images_tag_wtih_duplicate_alt_text()
		{
			var bodyHtml = new StringBuilder();
			bodyHtml.Append(@"<img id=""i1"" alt=""Unique Test"" />");
			bodyHtml.Append(@"<P/>");
			bodyHtml.Append(@"<img  alt=""Description"" id=""i2"" />");
			bodyHtml.Append(@"<img id=""i3""  alt=""Description""/>");
			bodyHtml.Append(@"<img id=""i4""  alt=""Description""/>");

			var errors = new ImagesDontHaveDuplicateAltText().ValidateHtml(bodyHtml);

			Assert.That(errors.Length, Is.EqualTo(2));
            Assert.That(errors.ContainsError(@"Image has duplicate alt text: <img id=""i3"" alt=""Description"">"));
            Assert.That(errors.ContainsError(@"Image has duplicate alt text: <img id=""i4"" alt=""Description"">"));
		}


	}
}