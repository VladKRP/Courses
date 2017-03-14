using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;
using System.Text.RegularExpressions;

namespace LinqToXml
{
    public static class LinqToXml
    {
        /// <summary>
        /// Creates hierarchical data grouped by category
        /// </summary>
        /// <param name="xmlRepresentation">Xml representation (refer to CreateHierarchySourceFile.xml in Resources)</param>
        /// <returns>Xml representation (refer to CreateHierarchyResultFile.xml in Resources)</returns>
        public static string CreateHierarchy(string xmlRepresentation)
        {
            XDocument xDocument = XDocument.Parse(xmlRepresentation);
            XElement rootElement = xDocument.Element("Root");
            List<XElement> groups = new List<XElement>();
            rootElement.Element("TaxRate").Remove();
            var categories = from dataTag in rootElement.Elements()
                             select dataTag.Element("Category").Value;
            categories = categories.Distinct().OrderBy(x => x);
            foreach (var category in categories)
                groups.Add(generateGroup(rootElement, category));
            rootElement.ReplaceAll(groups);
            return xDocument.ToString();
        }

        private static  XElement generateGroup(XElement root, string groupId){
          XElement group = (
              new XElement("Group",
                  new XAttribute("ID", groupId),
                  from dataTag in root.Elements()
                  where dataTag.Element("Category").Value == groupId
                  select
                  new XElement("Data",
                      new XElement("Quantity", dataTag.Element("Quantity").Value),
                      new XElement("Price", dataTag.Element("Price").Value)
                      )
                  )
              );
              return group;
        }

        /// <summary>
        /// Get list of orders numbers (where shipping state is NY) from xml representation
        /// </summary>
        /// <param name="xmlRepresentation">Orders xml representation (refer to PurchaseOrdersSourceFile.xml in Resources)</param>
        /// <returns>Concatenated orders numbers</returns>
        /// <example>
        /// 99301,99189,99110
        /// </example>
        public static string GetPurchaseOrders(string xmlRepresentation)
        {
            XDocument xmlDocument = XDocument.Parse(xmlRepresentation);
            XElement rootElement = xmlDocument.Root;
            var orderNumbersOfNYShipping = from purchace in rootElement.Elements()
                                           let shippingAddress = purchace.Elements().First()
                                           where shippingAddress.Elements().Skip(3).FirstOrDefault().Value == "NY"
                                           select purchace.FirstAttribute.Value;
            return String.Join(",", orderNumbersOfNYShipping);
        }

        /// <summary>
        /// Reads csv representation and creates appropriate xml representation
        /// </summary>
        /// <param name="customers">Csv customers representation (refer to XmlFromCsvSourceFile.csv in Resources)</param>
        /// <returns>Xml customers representation (refer to XmlFromCsvResultFile.xml in Resources)</returns>
        public static string ReadCustomersFromCsv(string customers)
        {
            string[] csvCustomers = customers.Split('\n');
            XElement xmlCustomers = new XElement("Root",
                from customer in csvCustomers
                let fields = customer.Split(',')
                select new XElement("Customer",
                new XAttribute("CustomerID", fields[0]),
                new XElement("CompanyName", fields[1]),
                new XElement("ContactName", fields[2]),
                new XElement("ContactTitle", fields[3]),
                new XElement("Phone", fields[4]),
                new XElement("FullAddress",
                        new XElement("Address", fields[5]),
                        new XElement("City", fields[6]),
                        new XElement("Region", fields[7]),
                        new XElement("PostalCode", fields[8]),
                        new XElement("Country", fields[9])
                        )
               )
            );
            return xmlCustomers.ToString();
        }

        /// <summary>
        /// Gets recursive concatenation of elements
        /// </summary>
        /// <param name="xmlRepresentation">Xml representation of document with Sentence, Word and Punctuation elements. (refer to ConcatenationStringSource.xml in Resources)</param>
        /// <returns>Concatenation of all this element values.</returns>
        public static string GetConcatenationString(string xmlRepresentation)
        {
            XDocument xDocument = XDocument.Parse(xmlRepresentation);
            XElement rootElement = xDocument.Root;
            
           var textFromXmlRepresentation = String.Concat(from sentence in rootElement.Elements()
                            let words = sentence.Elements()
                            from word in words
                            select word.Value);
            return textFromXmlRepresentation;
        }



        /// <summary>
        /// Replaces all "customer" elements with "contact" elements with the same childs
        /// </summary>
        /// <param name="xmlRepresentation">Xml representation with customers (refer to ReplaceCustomersWithContactsSource.xml in Resources)</param>
        /// <returns>Xml representation with contacts (refer to ReplaceCustomersWithContactsResult.xml in Resources)</returns>
        public static string ReplaceAllCustomersWithContacts(string xmlRepresentation)
        {
            XDocument xDocument = XDocument.Parse(xmlRepresentation);
            XElement rootElement = xDocument.Root;
            var contacts = from customer in rootElement.Elements()
                            select
                            new XElement("contact",
                                new XElement("name", customer.Element("name").Value),
                                new XElement("lastname", customer.Element("lastname").Value)
                            );

            rootElement.ReplaceAll(contacts);
            return rootElement.ToString();
        }

        /// <summary>
        /// Finds all ids for channels with 2 or more subscribers and mark the "DELETE" comment
        /// </summary>
        /// <param name="xmlRepresentation">Xml representation with channels (refer to FindAllChannelsIdsSource.xml in Resources)</param>
        /// <returns>Sequence of channels ids</returns>
        public static IEnumerable<int> FindChannelsIds(string xmlRepresentation)
        {
            XDocument xDocument = XDocument.Parse(xmlRepresentation);
            XElement rootElement = xDocument.Root;

            var channels = from channel in rootElement.Elements()
                           where channel.Elements().Count() >= 2 &&
                           channel.DescendantNodes().OfType<XComment>().Count() > 0
                           select channel.Attribute("id").Value;
            var channelsInInteger = channels.Select(x => int.Parse(x));
            return channelsInInteger;
        }

        /// <summary>
        /// Sort customers in document by Country and City
        /// </summary>
        /// <param name="xmlRepresentation">Customers xml representation (refer to GeneralCustomersSourceFile.xml in Resources)</param>
        /// <returns>Sorted customers representation (refer to GeneralCustomersResultFile.xml in Resources)</returns>
        public static string SortCustomers(string xmlRepresentation)
        {
            XDocument xDocument = XDocument.Parse(xmlRepresentation);
            XElement rootElement = xDocument.Root;

            var customers = from customer in rootElement.Elements()
                            let customerAddress = customer.Element("FullAddress")
                            orderby customerAddress.Element("City").Value
                            orderby customerAddress.Element("Country").Value
                            select customer;
            rootElement.ReplaceAll(customers);
            return rootElement.ToString();              
        }

        /// <summary>
        /// Gets XElement flatten string representation to save memory
        /// </summary>
        /// <param name="xmlRepresentation">XElement object</param>
        /// <returns>Flatten string representation</returns>
        /// <example>
        ///     <root><element>something</element></root>
        /// </example>
        public static string GetFlattenString(XElement xmlRepresentation)
        {
            return xmlRepresentation.ToString();
        }

        /// <summary>
        /// Gets total value of orders by calculating products value
        /// </summary>
        /// <param name="xmlRepresentation">Orders and products xml representation (refer to GeneralOrdersFileSource.xml in Resources)</param>
        /// <returns>Total purchase value</returns>
        public static int GetOrdersValue(string xmlRepresentation)
        {
            XDocument xDocument = XDocument.Parse(xmlRepresentation);
            XElement rootElement = xDocument.Root;
            int totalPurchaseValue = 0;
            var ordersOfProduct = from order in rootElement.Element("Orders").Elements()
                                  select order.Element("product").Value;
            var products = rootElement.Element("products").Elements()
                           .Select(x => new { id = x.Attribute("Id").Value, value = x.Attribute("Value").Value });
            foreach(var order in ordersOfProduct)
            {
                totalPurchaseValue += int.Parse(products.FirstOrDefault(x => x.id == order).value);
            }
            return totalPurchaseValue;
        }
    }
}
