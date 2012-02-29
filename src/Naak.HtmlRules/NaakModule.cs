using System;
using System.Web;

namespace Naak.HtmlRules
{
    public class NaakModule : IHttpModule 
    {
        public static void PreApplicationStart()
        {
            Microsoft.Web.Infrastructure.DynamicModuleHelper.DynamicModuleUtility.RegisterModule(typeof(NaakModule));
        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += AddContentStream;
        }

        public void Dispose()
        {
        }

        private static void AddContentStream(object sender, EventArgs e)
        {
            var contentStream = new CaptureStream(HttpContext.Current.Response.Filter);
            HttpContext.Current.Response.Filter = contentStream;
            CurrentContentStream = contentStream;
        }

        internal static CaptureStream CurrentContentStream
        {
            get
            {
                return (CaptureStream)HttpContext.Current.Items["naak_response"];
            }

            private set
            {
                HttpContext.Current.Items["naak_response"] = value;
            }
        }
    }
}