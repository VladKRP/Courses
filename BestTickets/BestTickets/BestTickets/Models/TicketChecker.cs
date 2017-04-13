using System;
using System.Collections.Generic;
using System.Linq;

namespace BestTickets.Models
{
    public class TicketChecker
    {
        public static IEnumerable<Ticket> FindTickets(RouteViewModel route)
        {
            string raspRwUrl = string.Format($"http://rasp.rw.by/ru/route/?from={route.DeparturePlace}&to={route.ArrivalPlace}&date={RaspRwDateFormatChange(route.Date)}");
            var raspRwContent = Parser.ParseSiteAsString(raspRwUrl);
            var raspRwTickets = RaspRwSearch(raspRwContent);

            string ticketBusUrl = "http://ticketbus.by/";
            var ticketBusContent = Parser.ParseSiteAsString(ticketBusUrl);
            var ticketBusTickets = TicketBusSearch(ticketBusUrl, ticketBusContent, route);


            return raspRwTickets.Concat(ticketBusTickets);

        }

        private static string RaspRwDateFormatChange(string routeDate)
        {
            var date = routeDate.Split('.', '-', '/');
            return string.Join("-", date.Reverse());
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

        private static string TicketBusFindCityId(string allCity, string city)
        {
            var htmlDocument = Parser.LoadHtmlRootElement(allCity);
            return htmlDocument.Descendants().Where(x => x.InnerText.ToLower().Contains(city.ToLower())).Select(x => x.PreviousSibling.Attributes["value"].Value).FirstOrDefault();
        }

        private static string TicketBusDateFormatChange(string routeDate)
        {
            var date = routeDate.Split('/', '.', '-').Select(x => x.Length == 1 ? string.Concat("0", x) : x);
            return string.Join(".", date);
        }

        private static IEnumerable<Ticket> TicketBusSearch(string url, string siteContent, RouteViewModel route)
        {
            var ticketsbusSessionId = siteContent.Skip(siteContent.IndexOf("var url")).Skip(11).TakeWhile(x => x != '"').Aggregate("", (x, y) => x += y);
            var cityRequestUrl = string.Concat(url,ticketsbusSessionId);
            var ticketsRequestUrl = string.Concat(cityRequestUrl, "&prog=marshrut1&host=1");
            var city = Parser.SendPostRequest("prog=getcity", cityRequestUrl, url);
            var departureCity = TicketBusFindCityId(city, route.DeparturePlace);
            var arrivalCity = TicketBusFindCityId(city, route.ArrivalPlace);
            var date = TicketBusDateFormatChange(route.Date);
            var postData = string.Format($"station_id={arrivalCity}&station_id1={departureCity}&date={date}");
            var tickets = Parser.SendPostRequest(postData, ticketsRequestUrl, url);
                var trains = Parser.LoadHtmlRootElement(tickets);
                var allTickets = from ticket in Parser.GetElementByClass(trains, "odd").Concat(Parser.GetElementByClass(trains, "even"))
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
                return allTickets;
        }

    }
}