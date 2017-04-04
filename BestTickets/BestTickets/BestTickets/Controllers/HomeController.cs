using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BestTickets.Models;
using System.Net;
using System.Text;
using System.Xml.Linq;

namespace BestTickets.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ShowTickets()
        {
            return View();
        }

        private List<Vehicle> FindTickets(RouteViewModel route)
        {
            
            string site1 = "http://ticketbus.by/";
            string site2 = String.Format($"http://rasp.rw.by/ru/route/?from={route.DeparturePlace}&to={route.ArrivalPlace}&date={route.Date}");
            var site2Content = new WebClient().DownloadData(site2);
            var site2ContentStr = Encoding.Default.GetString(site2Content);
        }

        //private List<Vehicle> FirstSiteSearch()
        //{

        //}

        //private List<Vehicle> SecondSiteSearch(string siteContent) {
        //    XDocument siteXml = XDocument.Parse(siteContent);
        //    var pageRoot = siteXml.Root;
        //    var que = from x in pageRoot.Element("body").Elements()
        //              where x.FirstAttribute.Value == "g - wrapper"
        //              from y in x.Elements()
        //              where y.FirstAttribute.Value == "g-wrapper_inner"
        //              from z in y.Elements()
        //              where z.FirstAttribute.Value == "g-main"
        //              select z.Elements();
        //}
    }
}