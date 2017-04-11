using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace BestTickets.Models
{
    public class TicketChecker
    {
        public static IEnumerable<Ticket> FindTickets(RouteViewModel route)
        {
            string raspRwUrl = String.Format($"http://rasp.rw.by/ru/route/?from={route.DeparturePlace}&to={route.ArrivalPlace}&date={route.Date}");
            var raspRwContent = Parser.ParseSiteAsString(raspRwUrl);
            var raspRwTickets = RaspRwSearch(raspRwContent);


            string ticketsbusUrl = "http://ticketbus.by/";
            var ticketsbusContent = Parser.ParseSiteAsString(ticketsbusUrl);
            var ticketsbusSessionId = ticketsbusContent.Skip(ticketsbusContent.IndexOf("var url")).Skip(11).TakeWhile(x => x != '"');
            ticketsbusSessionId = string.Join("", ticketsbusSessionId);
            var cityRequestUrl = ticketsbusUrl + ticketsbusSessionId + "&prog=getcity" + " HTTP/1.1";
            var ticketsRequestUrl = string.Concat(ticketsbusUrl, ticketsbusSessionId) + "&prog=marshrut1&host=1&station_id=100001&station_id1=500000&date=12.04.2017 HTTP/1.1";
            var city = TicketbyPostRequest(cityRequestUrl);
            var tiks = TicketbyPostRequest(ticketsRequestUrl);

            return raspRwTickets;

        }

     
        

        public static string TicketbyPostRequest(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            if (request != null)
            {
                request.Method = "POST";
                //request.ContentType = "application/x-www-form-urlencoded";
                request.Referer = "http://ticketbus.by/";
                using (var response = request.GetResponse().GetResponseStream()){
                    
                    using (System.IO.StreamReader stream = new System.IO.StreamReader(response))
                    {
                        var siteContent = stream.ReadToEnd();
                        return siteContent;
                    }
                }
            }
            return null;
        }

        private static IEnumerable<Ticket> TicketbySearch(string siteContent)
        {
            var htmlDocument = Parser.LoadHtmlRoot(siteContent);

            var cityInfo = Parser.GetElementById(htmlDocument, "getcity").ChildNodes.Where(x => x.Name == "option");
            var cityCode = from city in cityInfo
                           select new Tuple<string, string>(
                               city.InnerText,
                               city.Attributes["value"].Value
                            );
            var cityTest = Parser.GetElementById(htmlDocument, "getcity");
            //Extract data 
            var schedule = Parser.GetElementById(htmlDocument, "schedule-races");
            var ticketsInfoNodes = Parser.GetElementByClass(schedule, "odd").Concat(Parser.GetElementByClass(schedule, "even"));
            var tickets = from ticket in ticketsInfoNodes
                          select new Ticket()
                          {
                              VehicleName = Parser.GetElementValueByClass(ticket, "typ"),
                              VehicleType = Parser.GetElementValueByClass(ticket, ""),
                              Route = Parser.GetElementValueByClass(ticket, "marshrut"),
                              DepartureTime = Parser.GetElementValueByClass(ticket, "time"),
                              ArrivalTime = Parser.GetElementValueByClass(ticket, "time"),
                              VehiclePlace = new List<Tuple<string, string, string>>() {
                                                new Tuple<string, string, string>(
                                                   Parser.GetElementValueByClass(ticket, ""),
                                                   Parser.GetElementValueByClass(ticket, "bus-info"),
                                                   Parser.GetElementValueByClass(ticket, "price")
                                               )}
                          };

            return tickets;
        }

        private static IEnumerable<Ticket> RaspRwSearch(string siteContent)
        {
            var htmlDocument = Parser.LoadHtmlRoot(siteContent);
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

      
    }
}