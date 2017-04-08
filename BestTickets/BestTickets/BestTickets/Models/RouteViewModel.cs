using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Fizzler.Systems.HtmlAgilityPack;
using System.Web;

namespace BestTickets.Models
{
    public class RouteViewModel
    {
        public string DeparturePlace { get; set; }
        public string ArrivalPlace { get; set; }
        public string Date { get; set; }

        public static IEnumerable<Ticket> FindTickets(RouteViewModel route)
        {
            string raspRwUrl = String.Format($"http://rasp.rw.by/ru/route/?from={route.DeparturePlace}&to={route.ArrivalPlace}&date={route.Date}");
            var raspRwContent = ParseSite(raspRwUrl);
            var raspRwTickets = RaspRwSearch(raspRwContent);


            //string ticketsbusUrl = "http://ticketbus.by/";
            //var ticketsbusContent = ParseSite(ticketsbusUrl);
            //var ticketsbusKey = ticketsbusContent.Skip(ticketsbusContent.IndexOf("var url")).Skip(11).TakeWhile(x => x != '"');
            //ticketsbusUrl = ticketsbusUrl + string.Join("",ticketsbusKey);

            return raspRwTickets;

        }

        private static string ParseSite(string url)
        {
            var pageContent = new WebClient().DownloadData(url);
            return Encoding.UTF8.GetString(pageContent);
        }

        private static IEnumerable<Ticket> RaspRwSearch(string siteContent)
        {
            HtmlDocument html = new HtmlDocument();
            html.LoadHtml(siteContent);
            var htmlDocument = html.DocumentNode;
            var ticketsInfoNodes = GetElementByClass(htmlDocument, "schedule_list")
                .SelectMany(x => x.ChildNodes.Where(y => y.Name == "tr"));
            var tickets = from ticket in ticketsInfoNodes
                          select new Ticket()
                          {
                              VehicleName = GetElementValueByClass(ticket, "train_id"),
                              VehicleType = GetElementValueByClass(ticket, "train_description"),
                              Route = string.Join("&nbsp",GetElementValueByClass(ticket, "train_name -map")),
                              DepartureTime = GetElementValueByClass(ticket, "train_start-time"),
                              ArrivalTime = GetElementValueByClass(ticket, "train_end-time"),
                              VehiclePlace = from place in GetElementByClass(ticket, "train_details-group")
                                             select new Tuple<string, string, string>(
                                               GetElementValueByClass(place, "train_note"),
                                               GetElementValueByClass(place, "train_place"),
                                               GetElementValueByClass(place, "denom_after")
                                            )
                         };
            return tickets;
        }

        private static string GetElementValueByClass(HtmlNode node, string className)
        {
            var data = node.Descendants().Where(x => (x.Attributes["class"] != null && x.Attributes["class"].Value == className))
                .Select(x => x.InnerText);
            return String.Join("", data);
        }

        private static IEnumerable<HtmlNode> GetElementByClass(HtmlNode node, string className)
        {
            return node.Descendants().Where(x => (x.Attributes["class"] != null && x.Attributes["class"].Value == className));
        }
    }
}