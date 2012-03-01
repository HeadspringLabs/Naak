using System.Text;
using Naak.HtmlRules.Default;
using NUnit.Framework;

namespace Naak.UnitTests
{
	[TestFixture]
	public class LabelsRelateToFormElementsTester
	{
		[Test]
		public void Correctly_identifies_labels_that_do_not_relate_to_form_elements()
		{
			var html = new StringBuilder();

			html.Append(@"<form>");
			html.Append(@"  <label for=""txtFirstName"">First Name</label>");
			html.Append(@"  <input type=""text"" id=""txtFirstName"" />");

			html.Append(@"  <label for=""txtMiddle"">First Name</label>");
			html.Append(@"  <input type=""text"" id=""txtMiddleName"" />");

			html.Append(@"  <label>Last Name</label>");
			html.Append(@"  <input type=""text"" id=""txtLastName"" />");

			html.Append(@"  <label for=""txtPassword"">Password</label>");
			html.Append(@"  <input type=""text"" id=""txtPassword"" />");

			html.Append(@"  <label for=""txtRetype"">Retype Password</label>");
			html.Append(@"  <input type=""password"" id=""txtRetypePassword"" />");

			html.Append(@"  <label for=""chkOption1"">Option 1</label>");
			html.Append(@"  <input type=""checkbox"" id=""chkOption1"" />");

			html.Append(@"  <label for=""chkOption2"">Option 2</label>");

			html.Append(@"  <label for=""ddlStatus1"">Status 1</label>");
			html.Append(@"  <select id=""ddlStatus1"" />");

			html.Append(@"  <label for=""ddlStatus2"">Status 2</label>");

			html.Append(@"  <label for=""txtArea1"">Area 1</label>");
			html.Append(@"  <textarea id=""txtArea1"" />");

			html.Append(@"  <label for=""txtArea2"">Area 2</label>");
			html.Append(@"</form>");

			var errors = new LabelsRelateToFormElements().ValidateHtml(html);

			Assert.That(errors.Length, Is.EqualTo(6));

            Assert.That(errors.ContainsError(@"Label does not relate to a form control: <label for=""txtMiddle"">First Name</label>"));
            Assert.That(errors.ContainsError(@"Label does not relate to a form control: <label>Last Name</label>"));
            Assert.That(errors.ContainsError(@"Label does not relate to a form control: <label for=""txtRetype"">Retype Password</label>"));
            Assert.That(errors.ContainsError(@"Label does not relate to a form control: <label for=""chkOption2"">Option 2</label>"));
            Assert.That(errors.ContainsError(@"Label does not relate to a form control: <label for=""ddlStatus2"">Status 2</label>"));
            Assert.That(errors.ContainsError(@"Label does not relate to a form control: <label for=""txtArea2"">Area 2</label>"));
		}
	}
}