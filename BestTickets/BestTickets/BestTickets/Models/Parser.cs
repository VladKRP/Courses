using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using System.Text;
using System.Net;

namespace BestTickets.Models
{
    public class Parser
    {
        public static string ParseSiteAsString(string url)
        {
            var pageContent = new WebClient().DownloadData(url);
            return Encoding.UTF8.GetString(pageContent);
        }

        public static HtmlNode LoadHtmlRoot(string siteContent)
        {
            HtmlDocument html = new HtmlDocument();
            html.LoadHtml(siteContent);
            return html.DocumentNode;
        }

        public static HtmlNode GetElementById(HtmlNode node, string id)
        {
            return node.Descendants().Where(x => (x.Attributes["id"] != null && x.Attributes["id"].Value == id)).FirstOrDefault();
        }

        public static IEnumerable<HtmlNode> GetElementByClass(HtmlNode node, string className)
        {
            return node.Descendants().Where(x => (x.Attributes["class"] != null && x.Attributes["class"].Value == className));
        }

        public static string GetElementValueByClass(HtmlNode node, string className)
        {
            var data = node.Descendants().Where(x => (x.Attributes["class"] != null && x.Attributes["class"].Value == className))
                .Select(x => x.InnerText);
            return String.Join("", data);
        }
        
    }
}