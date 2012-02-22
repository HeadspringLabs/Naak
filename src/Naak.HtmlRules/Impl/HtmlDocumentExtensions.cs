using HtmlAgilityPack;

namespace Naak.HtmlRules.Impl
{
    public static class HtmlDocumentExtensions
    {
        public static HtmlNodeCollection GetNodes(this HtmlDocument document, string xpath)
        {
            return document.DocumentNode.GetNodes(xpath);
        }

        public static HtmlNodeCollection GetNodes(this HtmlNode node, string xpath)
        {
            return node.SelectNodes(xpath) ?? new HtmlNodeCollection(node);
        }

        public static HtmlNode GetNode(this HtmlDocument document, string xpath)
        {
            return document.DocumentNode.SelectSingleNode(xpath) ;
        }
    }
}