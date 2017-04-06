using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BestTickets.Models;
using System.Net;
using System.Text;
using System.Xml.Linq;
using HtmlAgilityPack;
using System.Threading.Tasks;

namespace BestTickets.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(RouteViewModel route)
        {
            return View("GetTickets",FindTickets(route).ToList());
        }

        //public PartialViewResult GetTickets(RouteViewModel route)
        //{
        //    var tickets = FindTickets(route);
        //    return PartialView();
        //}

        private IEnumerable<Ticket> FindTickets(RouteViewModel route)
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


        private string ParseSite(string url)
        {
            var pageContent = new WebClient().DownloadData(url);
            return Encoding.UTF8.GetString(pageContent);
            //var defaultEncodingPage = Encoding.Default.GetString(pageContent);
            //var charsetValue = defaultEncodingPage.Skip(defaultEncodingPage.IndexOf("charset=")).TakeWhile(x => x != '\n');
            //var pageEncoding = string.Join("", charsetValue);
            //return Encoding.GetEncoding(pageEncoding).GetString(pageContent);
        }

        private IEnumerable<Ticket> RaspRwSearch(string siteContent)
        {
            HtmlDocument html = new HtmlDocument();
            html.LoadHtml(siteContent);
            var ticketsInfoNodes = html.DocumentNode.Descendants().Where(x => (x.Attributes["class"] != null && x.Attributes["class"].Value == "schedule_list")).SelectMany(x => x.ChildNodes.Where(y => y.Name == "tr"));
            //var vehiclePlaceTypes = from ticket in ticketsInfoNodes select ticket.Descendants().Where(x => (x.Attributes["class"] != null && x.Attributes["class"].Value == "train_details-group"));
            //var vehiclePlace = from place in vehiclePlaceTypes
            //                   select new Tuple<string, int, int>(
            //                       GetElementValue(place, "train_note"),
            //                       GetElementValue(place, "train_place"),
            //                       GetElementValue(place, "train_price")
            //                    );
            var tickets = from ticket in ticketsInfoNodes
                          select new Ticket() {
                              VehicleName = GetElementValue(ticket, "train_id"),               
                              VehicleType = GetElementValue(ticket, "train_description"),
                              Route = GetElementValue(ticket, "train_name -map"),
                              DepartureTime = GetElementValue(ticket, "train_start-time"),
                              ArrivalTime = GetElementValue(ticket, "train_end-time")            
                          };
            return tickets;
        }

        private string GetElementValue(HtmlNode node, string className)
        {
            var data = node.Descendants().Where(x => (x.Attributes["class"] != null && x.Attributes["class"].Value == className)).Select(x => x.InnerText);
            return String.Join("", data);
        }


    }
}