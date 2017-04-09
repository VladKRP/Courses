using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;

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
            var ticketsbusKey = ticketsbusContent.Skip(ticketsbusContent.IndexOf("var url")).Skip(11).TakeWhile(x => x != '"');
            //ticketsbusUrl = ticketsbusUrl + string.Join("", ticketsbusKey);
            //var ticketbyTickets = TicketsbySearch(ticketsbusContent);

            return raspRwTickets;

        }

     
        private static IEnumerable<Ticket> TicketsbySearch(string siteContent)
        {
            HtmlDocument html = new HtmlDocument();
            //Incorrect document exception
                html.Load(siteContent);
            //
            var htmlDocument = html.DocumentNode;

            var cityInfo = Parser.GetElementById(htmlDocument, "getcity").ChildNodes.Where(x => x.Name == "option");
            var cityCode = from city in cityInfo
                           select new Tuple<string, string>(
                               city.InnerText,
                               city.Attributes["value"].Value
                            );

            //Extract data 
            var schedule = Parser.GetElementById(htmlDocument, "schedule-races");
            var ticketsInfoNodes = Parser.GetElementByClass(schedule, "odd").Concat(Parser.GetElementByClass(schedule, "even"));

            return null;
        }

        private static IEnumerable<Ticket> RaspRwSearch(string siteContent)
        {
            HtmlDocument html = new HtmlDocument();
            html.LoadHtml(siteContent);
            var htmlDocument = html.DocumentNode;
            var ticketsInfoNodes = Parser.GetElementByClass(htmlDocument, "schedule_list")
                .SelectMany(x => x.ChildNodes.Where(y => y.Name == "tr"));
            var tickets = from ticket in ticketsInfoNodes
                          select new Ticket()
                          {
                              VehicleName = Parser.GetElementValueByClass(ticket, "train_id"),
                              VehicleType = Parser.GetElementValueByClass(ticket, "train_description"),
                              Route = Parser.GetElementValueByClass(ticket, "train_name -map").Replace("&nbsp;", "").Replace("&mdash;", " - "),
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