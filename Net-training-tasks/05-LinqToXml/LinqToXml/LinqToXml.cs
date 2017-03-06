using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;

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
            rootElement.Element("TaxRate").Remove();
            

            var categoryElementA = from dataTag in rootElement.Elements()
                                   where dataTag.Element("Category").Value == "A"
                                   select dataTag;
            
            var categoryElementB = from dataTag in rootElement.Elements()
                                   where dataTag.Element("Category").Value == "B"
                                   select dataTag;

            rootElement.ReplaceAll(new XElement("Group", new XAttribute("ID", "A")),
                new XElement("Group", new XAttribute("ID", "B")));


            return xDocument.ToString();
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
            var purchases = from purchace in xmlDocument.Elements()
                            where purchace.Element("Address").Attribute("Type").Value == "Shipping"
                            && purchace.Element("Address").Element("State").Value == "NY"
                            select purchace.Attribute("PurchaseOrderNumber").Value;
            return xmlDocument.ToString();
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
                    new XElement("Country", fields[9]))
                ));
            return xmlCustomers.ToString();
        }

        /// <summary>
        /// Gets recursive concatenation of elements
        /// </summary>
        /// <param name="xmlRepresentation">Xml representation of document with Sentence, Word and Punctuation elements. (refer to ConcatenationStringSource.xml in Resources)</param>
        /// <returns>Concatenation of all this element values.</returns>
        public static string GetConcatenationString(string xmlRepresentation)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Replaces all "customer" elements with "contact" elements with the same childs
        /// </summary>
        /// <param name="xmlRepresentation">Xml representation with customers (refer to ReplaceCustomersWithContactsSource.xml in Resources)</param>
        /// <returns>Xml representation with contacts (refer to ReplaceCustomersWithContactsResult.xml in Resources)</returns>
        public static string ReplaceAllCustomersWithContacts(string xmlRepresentation)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Finds all ids for channels with 2 or more subscribers and mark the "DELETE" comment
        /// </summary>
        /// <param name="xmlRepresentation">Xml representation with channels (refer to FindAllChannelsIdsSource.xml in Resources)</param>
        /// <returns>Sequence of channels ids</returns>
        public static IEnumerable<int> FindChannelsIds(string xmlRepresentation)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sort customers in docement by Country and City
        /// </summary>
        /// <param name="xmlRepresentation">Customers xml representation (refer to GeneralCustomersSourceFile.xml in Resources)</param>
        /// <returns>Sorted customers representation (refer to GeneralCustomersResultFile.xml in Resources)</returns>
        public static string SortCustomers(string xmlRepresentation)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets total value of orders by calculating products value
        /// </summary>
        /// <param name="xmlRepresentation">Orders and products xml representation (refer to GeneralOrdersFileSource.xml in Resources)</param>
        /// <returns>Total purchase value</returns>
        public static int GetOrdersValue(string xmlRepresentation)
        {
            throw new NotImplementedException();
        }
    }
}
