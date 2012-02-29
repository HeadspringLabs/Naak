using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using Glimpse.Core.Extensibility;

namespace Naak.HtmlRules
{
    [GlimpsePlugin]
    public class Plugin : IGlimpsePlugin
    {

        public object GetData(HttpContextBase context)
        {
            var html = NaakModule.CurrentContentStream.ReadToEnd();
            var renderer = new AccessibilityRenderer();

            var results = renderer.GetAccessibilityErrors(html, Rules);
            
            return GetGlimpseResults(results);
        }

        public void SetupInit()
        {
        }

        [ImportMany]
        public IEnumerable<IHtmlRule> Rules { get; set; }

        public string Name
        {
            get { return "Naak"; }
        }

        private static object GetGlimpseResults(IEnumerable<ValidationError> records)
        {
            if (records.Any())
            {
                var data = new List<object[]> { new object[] { "Message", "LineNumber", "Position" } };
                data.AddRange(records.Select(record => new object[] { record.Message, record.LineNumber, record.LinePosition, "error" }));
                return data;
            }
            return "All accessibility rules passed.";
        }

    }
}