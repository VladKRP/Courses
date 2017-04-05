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
            route.ArrivalPlace = "Брест";
            route.DeparturePlace = "Минск";
            route.Date = DateTime.Now;
            var tickets = FindTickets(route);
            return View();
        }

        public PartialViewResult GetTickets(RouteViewModel route)
        {
            //var tickets = FindTickets(route);
            return PartialView();
        }

        private IEnumerable<Ticket> FindTickets(RouteViewModel route)
        {
            string raspRwUrl = String.Format($"http://rasp.rw.by/ru/route/?from={route.DeparturePlace}&to={route.ArrivalPlace}&date={route.Date}");
            var raspRwContent = ParseSite(raspRwUrl);

            string ticketsbusUrl = "http://ticketbus.by/";
            var ticketsbusContent = ParseSite(ticketsbusUrl);
            var specialUrl = ticketsbusContent.Skip(ticketsbusContent.IndexOf("var url")).Skip(11).TakeWhile(x => x != '"');

            
            //var trainName = raspRwContent.IndexOf("train_name");
            var siteCont = SecondSiteSearch(raspRwUrl);
            //var site2Cont = SecondSiteSearch(raspRwContent);
            return null;
            
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

        private IEnumerable<Ticket> TicketSearch(string siteContent)
        {
            return null;
        }
    }
}