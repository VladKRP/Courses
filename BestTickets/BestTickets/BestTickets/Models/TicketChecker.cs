using System;
using System.Collections.Generic;
using System.Linq;

namespace BestTickets.Models
{
    public class TicketChecker
    {
        public static IEnumerable<Ticket> FindTickets(RouteViewModel route)
        {
            string raspRwUrl = string.Format($"http://rasp.rw.by/ru/route/?from={route.DeparturePlace}&to={route.ArrivalPlace}&date={DateFormatChange(route.Date,"-",true)}");
            var raspRwContent = Parser.ParseSiteAsString(raspRwUrl);
            var raspRwTickets = RaspRwSearch(raspRwContent);

            string ticketBusUrl = "http://ticketbus.by/";
            var ticketBusContent = Parser.ParseSiteAsString(ticketBusUrl);
            var ticketBusPHPSessionId = ticketBusContent.Skip(ticketBusContent.IndexOf("var url")).Skip(11).TakeWhile(x => x != '"').Aggregate("", (x, y) => x += y);
            var ticketBusTickets = TicketBusSearch(TicketBusGetData(route,ticketBusUrl,ticketBusPHPSessionId));

            return raspRwTickets.Concat(ticketBusTickets);

        }

        private static string DateFormatChange(string departureDate, string separator, bool yearMonthDay = false)
        {
            var date = departureDate.Split('/', '.', '-').Select(x => x.Length == 1 ? string.Concat("0", x) : x);
            if (yearMonthDay)
                date = date.Reverse();
            return string.Join(separator, date);
        }

        private static IEnumerable<Ticket> RaspRwSearch(string siteContent)
        {
            var htmlDocument = Parser.LoadHtmlRootElement(siteContent);
            var ticketsInfoNodes = Parser.GetElementByClass(htmlDocument, "schedule_list")
                .SelectMany(x => x.ChildNodes.Where(y => y.Name == "tr"));
            var tickets = from ticket in ticketsInfoNodes
                          select new Ticket()
                          {
                              VehicleName = Parser.GetElementValueByClass(ticket, "train_id"),
                              VehicleType = Parser.GetElementValueByClass(ticket, "train_description"),
                              Route = Parser.GetElementValueByClass(ticket, "train_name -map")
                                              .Replace("&nbsp;", "").Replace("&mdash;", " - "),
                              DepartureTime = Parser.GetElementValueByClass(ticket, "train_start-time"),
                              ArrivalTime = Parser.GetElementValueByClass(ticket, "train_end-time"),
                              VehiclePlace = from place in Parser.GetElementByClass(ticket, "train_details-group")
                                             select new Tuple<string, string, string>(
                                               Parser.GetElementValueByClass(place, "train_note"),
                                               Parser.GetElementValueByClass(place, "train_place"),
                                               Parser.GetElementValueByClass(place, "denom_after")
                                            )
                          };
            return tickets;
        }

        private static string TicketBusFindCityId(string siteCities, string city)
        {
            var htmlDocument = Parser.LoadHtmlRootElement(siteCities);
            return htmlDocument.Descendants().Where(x => x.InnerText.ToLower().Contains(city.ToLower())).Select(x => x.PreviousSibling.Attributes["value"].Value).FirstOrDefault();
        }

        private static string TicketBusGetData(RouteViewModel route, string url,string phpSessionId)
        {
            var cityRequestUrl = string.Concat(url, phpSessionId);
            var ticketsRequestUrl = string.Concat(cityRequestUrl, "&prog=marshrut1&host=1");
            var siteCities = Parser.SendPostRequest("prog=getcity", cityRequestUrl, url);
            var departureCityId = TicketBusFindCityId(siteCities, route.DeparturePlace);
            var arrivalCityId = TicketBusFindCityId(siteCities, route.ArrivalPlace);
            var postData = string.Format($"station_id={arrivalCityId}&station_id1={departureCityId}&date={DateFormatChange(route.Date, ".")}");
            return Parser.SendPostRequest(postData, ticketsRequestUrl, url);
        }

        private static IEnumerable<Ticket> TicketBusSearch(string siteContent)
        {
            var htmlDocument = Parser.LoadHtmlRootElement(siteContent);
            var tickets = from ticket in Parser.GetElementByClass(htmlDocument, "odd").Concat(Parser.GetElementByClass(htmlDocument, "even"))
                             select new Ticket()
                             {
                                 VehicleName = Parser.GetElementValueByClass(ticket, ""),
                                 VehicleType = Parser.GetElementValueByClass(ticket, "typ"),
                                 Route = Parser.GetElementValueByClass(ticket, "marshrut"),
                                 DepartureTime = Parser.GetFirstElementValueByClass(ticket, "time"),
                                 ArrivalTime = Parser.GetLastElementValueByClass(ticket, "time"),
                                 VehiclePlace = new List<Tuple<string, string, string>>() {
                                                    new Tuple<string, string, string>(
                                                       Parser.GetElementValueByClass(ticket, ""),
                                                       Parser.GetElementValueByClass(ticket, "bus-info"),
                                                       Parser.GetElementValueByClass(ticket, "price")
                                                   )}
                             };
            return tickets;
        }

        //private static IEnumerable<Ticket> TicketBusSearch(string url, string siteContent, RouteViewModel route)
        //{
        //    var ticketBusPHPSessionId = siteContent.Skip(siteContent.IndexOf("var url")).Skip(11).TakeWhile(x => x != '"').Aggregate("", (x, y) => x += y);
        //    var cityRequestUrl = string.Concat(url, ticketBusPHPSessionId);
        //    var ticketsRequestUrl = string.Concat(cityRequestUrl, "&prog=marshrut1&host=1");
        //    var siteCities = Parser.SendPostRequest("prog=getcity", cityRequestUrl, url);
        //    var departureCityId = TicketBusFindCityId(siteCities, route.DeparturePlace);
        //    var arrivalCityId = TicketBusFindCityId(siteCities, route.ArrivalPlace);
        //    var postData = string.Format($"station_id={arrivalCityId}&station_id1={departureCityId}&date={DateFormatChange(route.Date, ".")}");
        //    var tickets = Parser.SendPostRequest(postData, ticketsRequestUrl, url);
        //        var trains = Parser.LoadHtmlRootElement(tickets);
        //        var allTickets = from ticket in Parser.GetElementByClass(trains, "odd").Concat(Parser.GetElementByClass(trains, "even"))
        //                         select new Ticket()
        //                      {
        //                          VehicleName = Parser.GetElementValueByClass(ticket, ""),
        //                          VehicleType = Parser.GetElementValueByClass(ticket, "typ"),
        //                          Route = Parser.GetElementValueByClass(ticket, "marshrut"),
        //                          DepartureTime = Parser.GetFirstElementValueByClass(ticket, "time"),
        //                          ArrivalTime = Parser.GetLastElementValueByClass(ticket, "time"),
        //                          VehiclePlace = new List<Tuple<string, string, string>>() {
        //                                            new Tuple<string, string, string>(
        //                                               Parser.GetElementValueByClass(ticket, ""),
        //                                               Parser.GetElementValueByClass(ticket, "bus-info"),
        //                                               Parser.GetElementValueByClass(ticket, "price")
        //                                           )}
        //                      };
        //        return allTickets;
        //}

    }
}