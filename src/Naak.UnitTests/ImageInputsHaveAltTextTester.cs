using System.Text;
using Naak.HtmlRules.Default;
using NUnit.Framework;

namespace Naak.UnitTests
{
	[TestFixture]
	public class ImageInputsHaveAltTextTester
	{
		[Test]
		public void Correctly_identifies_image_inputs_missing_alt_tag()
		{
			var bodyHtml = new StringBuilder();
			bodyHtml.Append(@"<form>");
			bodyHtml.Append(@"  <input type=""image"" id=""imgFirst"" alt=""""/>");
			bodyHtml.Append(@"  <input type=""image"" id=""imgSecond""/>");
			bodyHtml.Append(@"  <input type=""image"" id=""imgSecond"" alt=""Image Description""/>");
			bodyHtml.Append(@"</form>");

			var errors = new ImageInputsHaveAltText().ValidateHtml(bodyHtml);

			Assert.That(errors.Length, Is.EqualTo(2));
			Assert.That(errors.ContainsError(@"Image input missing alt text: <input type=""image"" id=""imgFirst"" alt="""">"));
			Assert.That(errors.ContainsError(@"Image input missing alt text: <input type=""image"" id=""imgSecond"">"));
		}
	}
}