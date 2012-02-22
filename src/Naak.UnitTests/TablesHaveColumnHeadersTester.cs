using System.Text;
using Naak.HtmlRules.Default;
using Naak.UnitTests;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Should;

namespace Naak.UnitTests
{
	[TestFixture]
	public class TablesHaveColumnHeadersTester : HtmlRuleTester
	{
		[Test]
		public void Correctly_identifies_tables_missing_horizontal_column_headers()
		{
			var bodyHtml = new StringBuilder();
			bodyHtml.Append(@"<table>");
			bodyHtml.Append(@"  <tr><th>Column 1</th><th>Column 2</th></tr>");
			bodyHtml.Append(@"  <tr><td>Value 1</td><td>Value 2</td></tr>");
			bodyHtml.Append(@"</table>");

			bodyHtml.Append(@"<table>");
			bodyHtml.Append(@"  <tr><td>Column 1</td><td>Column 2</td></tr>");
			bodyHtml.Append(@"  <tr><td>Value 1</td><td>Value 2</td></tr>");
			bodyHtml.Append(@"</table>");

			bodyHtml.Append(@"<table>");
			bodyHtml.Append(@"  <tr><td>Value 1</td><td>Value 2</td></tr>");
			bodyHtml.Append(@"</table>");

			ExecuteTest(new TablesHaveColumnHeaders(), bodyHtml.ToString());

            ErrorCount.ShouldEqual(2);
		    Errors[0].Message.ShouldEqual(
		        @"Layout table detected - if the table is a data table, use TH for the column or row headers. Otherwise, use CSS for layout: <table>  <tr><td>Column 1</td><td>Column 2</td></tr>  <tr><td>Value 1</td><td>Value 2</td></tr></table>");
            Errors[1].Message.ShouldEqual(
                @"Layout table detected - if the table is a data table, use TH for the column or row headers. Otherwise, use CSS for layout: <table>  <tr><td>Value 1</td><td>Value 2</td></tr></table>");
		}

		[Test]
		public void Correctly_identifies_tables_missing_vertical_column_headers()
		{
			var bodyHtml = new StringBuilder();
			bodyHtml.Append(@"<table>");
			bodyHtml.Append(@"  <tr><th>Row 1</th><td>Value 1</td></tr>");
			bodyHtml.Append(@"  <tr><th>Row 2</th><td>Value 2</td></tr>");
			bodyHtml.Append(@"</table>");

			bodyHtml.Append(@"<table>");
			bodyHtml.Append(@"  <tr><td>Row 1</td><td>Value 1</td></tr>");
			bodyHtml.Append(@"  <tr><td>Row 2</td><td>Value 2</td></tr>");
			bodyHtml.Append(@"</table>");

			bodyHtml.Append(@"<table>");
			bodyHtml.Append(@"  <tr><td>Value 1</td></tr>");
			bodyHtml.Append(@"  <tr><td>Value 2</td></tr>");
			bodyHtml.Append(@"</table>");

			ExecuteTest(new TablesHaveColumnHeaders(), bodyHtml.ToString());

            ErrorCount.ShouldEqual(2);
            Errors[0].Message.ShouldEqual(
                @"Layout table detected - if the table is a data table, use TH for the column or row headers. Otherwise, use CSS for layout: <table>  <tr><td>Row 1</td><td>Value 1</td></tr>  <tr><td>Row 2</td><td>Value 2</td></tr></table>");
            Errors[1].Message.ShouldEqual(
                @"Layout table detected - if the table is a data table, use TH for the column or row headers. Otherwise, use CSS for layout: <table>  <tr><td>Value 1</td></tr>  <tr><td>Value 2</td></tr></table>");
		}

        [Test]
        public void Should_allow_thead_and_tbody_as_valid_child_of_table()
        {
            var bodyHtml = new StringBuilder();
            bodyHtml.Append(@"<table>");
            bodyHtml.Append(@"  <thead>");
            bodyHtml.Append(@"      <tr><th>Row 1</th><td>Value 1</td></tr>");
            bodyHtml.Append(@"      <tr><th>Row 2</th><td>Value 2</td></tr>");
            bodyHtml.Append(@"  </thead>");
            bodyHtml.Append(@"  <tbody>");
            bodyHtml.Append(@"      <tr><td>Row 1</td><td>Value 1</td></tr>");
            bodyHtml.Append(@"      <tr><td>Row 2</td><td>Value 2</td></tr>");
            bodyHtml.Append(@"  </tbody>");
            bodyHtml.Append(@"</table>");

            ExecuteTest(new TablesHaveColumnHeaders(), bodyHtml.ToString());

            Assert.That(ErrorCount, Is.EqualTo(0));
        }
	}
}