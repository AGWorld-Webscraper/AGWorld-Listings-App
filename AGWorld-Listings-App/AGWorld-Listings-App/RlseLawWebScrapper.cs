using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGWorld_Listings_App
{
    using HtmlAgilityPack;
    internal class RlseLawWebScrapper
    {

        public static List<Listing_Info> Scrape()
        {

            // Create a list to store return listings
            List<Listing_Info> listings = new List<Listing_Info>();

            // Create a list to store property data
            List<PropertyData> properties = new List<PropertyData>();

            // URL of the website to scrape
            string url = "https://rlselaw.com/property-listing/tennessee-property-listings/";

            // Load HTML from the website
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);

            // XPath to locate the table rows containing property data
            string xpath = "//*/table/tbody/tr";


            // Select all rows from the table
            HtmlNodeCollection rows = doc.DocumentNode.SelectNodes(xpath);

            // Check if rows are found
            if (rows != null)
            {
                

                // Iterate through each row
                foreach (HtmlNode row in rows)
                {
                    // Select individual cells from the row
                    HtmlNodeCollection cells = row.SelectNodes("td");

                    // Extract data from each cell
                    string saleDate = cells[0].InnerText.Trim();
                    string fileNumber = cells[1].InnerText.Trim();
                    string propertyAddress = cells[2].InnerText.Trim();
                    string city = cells[3].InnerText.Trim();
                    string zip = cells[4].InnerText.Trim();
                    string county = cells[5].InnerText.Trim();
                    string bid = cells[6].InnerText.Trim();

                    // Create PropertyData object and add it to the list
                    properties.Add(new PropertyData
                    {
                        SaleDate = saleDate,
                        FileNumber = fileNumber,
                        PropertyAddress = propertyAddress,
                        City = city,
                        Zip = zip,
                        County = county,
                        Bid = bid
                    });
                }
                foreach( PropertyData property in properties)
                {
                    if(property.isValid())  listings.Add(property.toListingInfo());
                }
            }
            return listings;
        }

        static string GenerateHtmlTable(List<PropertyData> properties)
        {
            // Start building the HTML table
            string html = "<table border='1'>";

            // Add table header
            html += "<tr>";
            html += "<th>Sale Date</th>";
            html += "<th>File Number</th>";
            html += "<th>Property Address</th>";
            html += "<th>City</th>";
            html += "<th>Zip</th>";
            html += "<th>County</th>";
            html += "<th>Bid</th>";
            html += "</tr>";

            // Add table rows
            foreach (var property in properties)
            {
                html += "<tr>";
                html += $"<td>{property.SaleDate}</td>";
                html += $"<td>{property.FileNumber}</td>";
                html += $"<td>{property.PropertyAddress}</td>";
                html += $"<td>{property.City}</td>";
                html += $"<td>{property.Zip}</td>";
                html += $"<td>{property.County}</td>";
                html += $"<td>{property.Bid}</td>";
                html += "</tr>";
            }

            // Close the HTML table
            html += "</table>";

            return html;
        }
    }

    class PropertyData
    {
        static readonly String[] validZipCodes =
        {
                        "37921",
            "37912",
            "37849",
            "37918",
            "37917",
            "37902",
            "37916",
            "37915",
            "37919",
            "37920",
            "37853",
            "37701",
            "37804",
            "37865",
            "37914",
            "37777",
            "37803",
            "37886",
            "37862",
            "37863",
            "37876",
            "37738",
            "37821",
            "37725",
            "37871",
            "37764",
            "37760",
            "37877",
            "37890",
            "37813",
            "37860",
            "37814",
            "37924",
            "37779",
            "37721",
            "37938",
            "37806",
            "37830",
            "37934",
            "37932",
            "37923",
            "37931",
            "37772",
            "37922",
            "37909"
        };
        public string SaleDate { get; set; }
        public string FileNumber { get; set; }
        public string PropertyAddress { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public string County { get; set; }
        public string Bid { get; set; }

        private Firm RlseLaw = new Firm("Rubin Lublin, LLC (RL)", "(770)-246-3300", "(407)-508-9401", " deeds@rlselaw.com");

        public string FullAddress()
        {
            return PropertyAddress + ", " + City + ", TN " + Zip;
        }

        public bool isValid()
        {
            foreach (String zip in validZipCodes)
            {
                if(zip.Equals(Zip)) return true;
            }
            return false;
        }

        public Listing_Info toListingInfo()
        {
            return new Listing_Info(
                FullAddress(),
                RlseLaw,
                "https://rlselaw.com/property-listing/tennessee-property-listings/",
                DateTime.Today,
                DateTime.Parse(SaleDate.Substring(0,10))
                );
        }
    }
}
