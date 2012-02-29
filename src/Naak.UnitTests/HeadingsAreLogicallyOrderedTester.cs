using System.Text;
using NUnit.Framework;
using Naak.HtmlRules.Default;
using Should;

namespace Naak.UnitTests
{
	[TestFixture]
	public class HeadingsAreLogicallyOrderedTester
	{
		[Test]
		public void Identifies_missing_h1_tag()
		{
			var errors = new HeadingsAreLogicallyOrdered().ValidateHtml(@"<div><h2>Second-Level Heading</h2></div>");
			errors.Length.ShouldEqual(1);
		    errors[0].Message.ShouldEqual(
		        @"Illogical heading order: Expected to find <h1> but found <h2>Second-Level Heading</h2> instead");
		}

		[Test]
		public void Identifies_missing_h2_tag()
		{
			var html = new StringBuilder();
			html.Append("<h1 />");
			html.Append("<div><h3 /></div>");

			var errors = new HeadingsAreLogicallyOrdered().ValidateHtml(html);

            errors.Length.ShouldEqual(1);
            errors[0].Message.ShouldEqual(
                @"Illogical heading order: Expected to find <h2> but found <h3 /> instead");
		}

		[Test]
		public void Identifies_missing_header_in_complex_document()
		{
			var html = new StringBuilder();
			html.Append("<h1 />");
			html.Append("<div>");
			html.Append("  <h2 />");
			html.Append("  <div>");
			html.Append("    <h3 />");
			html.Append("  </div>");
			html.Append("  <h2 />");
			html.Append("  <div />");
			html.Append("  <div>");
			html.Append("    <div>");
			html.Append("      <h4 />");
			html.Append("    </div>");
			html.Append("    <h2 />");
			html.Append("    <div>");
			html.Append("      <h3 />");
			html.Append("    </div>");
			html.Append("  </div>");
			html.Append("</div>");

			var errors = new HeadingsAreLogicallyOrdered().ValidateHtml(html);

            errors.Length.ShouldEqual(1);
            errors[0].Message.ShouldEqual(
                @"Illogical heading order: Expected to find <h3> but found <h4 /> instead");
		}
	}
}