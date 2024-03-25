using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGWorld_Listings_App
{
    internal class Listing_Info
    {
        private String _address;
        private Firm _firm;
        private String[] _listingLinks;
        private DateTime[] _listingDates;
        private String[] _rawListings;
        private DateTime AuctionDay;

        public Listing_Info(string address, Firm firm, string[] listingLinks, DateTime[] listingDates, string[] rawListings, DateTime auctionDay)
        {
            _address = address;
            _firm = firm;
            _listingLinks = listingLinks;
            _listingDates = listingDates;
            _rawListings = rawListings;
            AuctionDay = auctionDay;
        }

        public int numTimesListing() { return _listingDates.Length; }
        public bool isBuyable() { return _listingDates.Length == 3; }
        public bool stillValid() { return AuctionDay.CompareTo(DateTime.Today) < 0; }
    }
}
