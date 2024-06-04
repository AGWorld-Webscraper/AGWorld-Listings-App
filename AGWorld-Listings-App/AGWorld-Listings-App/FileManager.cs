using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AGWorld_Listings_App
{
    internal class FileManager
    {
        List<ListingEntry> listings;
        
        public static void save(List<ListingEntry> listings, String path)
        {
            //List<Listing_Info> listings = TNLedgerScraper.scrape();
            String jsonString = JsonSerializer.Serialize(listings, new JsonSerializerOptions { WriteIndented = true });
            StreamWriter writer = new StreamWriter(path);
            writer.WriteLine(jsonString);
            writer.Close();
        }

        public static List<ListingEntry> load(String path)
        {
            String JsonString = File.ReadAllText(path);

            List<ListingEntry> list = JsonSerializer.Deserialize<List<ListingEntry>>(JsonString) ?? new List<ListingEntry>();
            return list;
        }
    }
}
