using System;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;

namespace AGWorld_Listings_App
{
    internal class TNLedgerScraper
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

        public static List<Listing_Info> scrape()
        {
            //listings
            List<Listing_Info> returnList = new List<Listing_Info>();

            // URL of the webpage to scrape
            string url = "https://www.tnledger.com/Knoxville/Notices.aspx?noticesDate=" + getLastFridayForUrl();

            // Load the HTML content from the webpage
            HtmlWeb web = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument doc = web.Load(url);

            List<string> URLS = new List<string>();

            // XPath expressions to select the elements containing addresses, firms, and dates
            string tableXPath = "//*[@id=\"ctl00_ContentPane_ForeclosureGridView\"]";
            string tableEntryXPath = "./tr";

            HtmlNode TableOfInfo = doc.DocumentNode.SelectSingleNode(tableXPath);
            HtmlNodeCollection ListingEntriesHtml = TableOfInfo.SelectNodes(tableEntryXPath);
            ListingEntriesHtml.RemoveAt(0);
            foreach (HtmlNode node in ListingEntriesHtml)
            {
                String openText = node.ChildNodes[1].InnerHtml;
                String[] urlComponents = openText.Split("'");
                String date = urlComponents[3].Replace("%sf", "");
                String fullUrl = "https://www.tnledger.com/Knoxville/Search/Details/ViewNotice.aspx?id=" + urlComponents[1] + "&date=" + date;
                URLS.Add(fullUrl);
            }

            foreach (String _url in URLS)
            {
                HtmlAgilityPack.HtmlDocument innerPage = web.Load(_url);
                Firm firm = getFirm(innerPage, innerPage.GetElementbyId("lbl5").InnerHtml);
                Listing_Info listing = new Listing_Info(
                    innerPage.GetElementbyId("lbl2").InnerHtml + " " + innerPage.GetElementbyId("lbl3").InnerHtml,
                    firm,
                    _url,
                    DateTime.Parse(innerPage.GetElementbyId("lbl9").InnerHtml),
                    DateTime.Parse(innerPage.GetElementbyId("lbl8").InnerHtml)
                    );
                if (Array.IndexOf(validZipCodes, listing.getName().Substring(listing.getName().Length - 5)) != -1) returnList.Add(listing);
            }

            return returnList;
        }

        private static Firm getFirm(HtmlAgilityPack.HtmlDocument doc, String attorneyName)
        {
            String regexPhoneNumberFilter = @"[pPoO][^>]*?(\W[0-9]{3}\W+[0-9]{3}\W+[0-9]{4})|(\W[0-9]{3}\W+[0-9]{3}\W+[0-9]{4})[^<>]*?[pPoO]|(?<![fF][^>]*?)(\W[0-9]{3}\W+[0-9]{3}\W+[0-9]{4})(?![fF][^>]*?)";
            Regex PhoneNumberRegEx = new Regex(regexPhoneNumberFilter);
            String PhoneNumber = (
                (
                (PhoneNumberRegEx.Match(doc.Text).Groups[1].ToString()) +
                (PhoneNumberRegEx.Match(doc.Text).Groups[2].ToString()) +
                (PhoneNumberRegEx.Match(doc.Text).Groups[3].ToString())
                ).Trim());

            String regexFaxNumberFilter = @"[fF][axX][^>]*?(\W[0-9]{3}\W+[0-9]{3}\W+[0-9]{4})|(\W[0-9]{3}\W+[0-9]{3}\W+[0-9]{4})[^<>]*?[fF][axX]";
            Regex FaxNumberRegEx = new Regex(regexFaxNumberFilter);
            String FaxNumber = (
                (
                FaxNumberRegEx.Match(doc.Text).Groups[1].ToString() +
                FaxNumberRegEx.Match(doc.Text).Groups[2].ToString())
                .Trim());
            String regexAddressFilter = @"<p>([0-9]+ [\w\s,]+)(?:</p><p>)*([\w\s,]*)</p>";
            Regex FirmAddressRegEx = new Regex(regexAddressFilter);
            String FirmAddress = (
                FirmAddressRegEx.Match(doc.Text).Groups[1].ToString() + " " +
                FirmAddressRegEx.Match(doc.Text).Groups[2].ToString()
                ).Trim();


            return new Firm(attorneyName, PhoneNumber, FaxNumber, FirmAddress);
        }

        private static String getLastFridayForUrl()
        {
            DateTime Friday = DateTime.Now;
            int deltaDays = 0;
            switch (Friday.DayOfWeek)
            {
                case DayOfWeek.Saturday: deltaDays = -1; break;
                case DayOfWeek.Friday:deltaDays = 0; break;
                default: deltaDays = -1 * ((int)Friday.DayOfWeek) - 2; break;
            }
            Friday = Friday.AddDays(deltaDays);
            return Friday.Month + "/" + Friday.Day + "/" + Friday.Year;
        }
    }
}
