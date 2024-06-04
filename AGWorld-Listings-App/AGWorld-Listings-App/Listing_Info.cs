using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AGWorld_Listings_App
{
    internal class Listing_Info
    {
        [JsonInclude]
        private String _address;
        [JsonInclude]
        private Firm _firm;
        [JsonInclude]
        private DateTime _listingDate;
        [JsonInclude]
        private DateTime _auctionDay;
        [JsonInclude]
        private String[] Links;

        [JsonConstructor]
        public Listing_Info(string _address, Firm _firm, string[] Links, DateTime _listingDate, DateTime _auctionDay)
        {
            this._address = _address;
            this._firm = _firm;
            this._listingDate = _listingDate;
            this._auctionDay = _auctionDay;
            this.Links = Links;
        }
        public Listing_Info(string _address, Firm _firm, string _listingLink, DateTime _listingDate, DateTime _auctionDay)
        {
            this._address = _address;
            this._firm = _firm;
            this._listingDate = _listingDate;
            this._auctionDay = _auctionDay;
            Links = new String[1];
            Links[0] = _listingLink;
        }

        public Listing_Info(Listing_Info other)
        {
            _address = other._address;
            _listingDate = other._listingDate;
            _auctionDay = other._auctionDay;
            Links = new String[other.Links.Length];
            for (int i = 0; i < other.Links.Length; i++)
            {
                Links[i] = other.Links[i];
            }
            _firm = new Firm(other._firm);
        }

        public String[] toDataGridRow()
        {
            return new String[]
            {
                _address, _firm.getName(), _listingDate.ToString(), _auctionDay.ToString(), "Note Placeholder"
            };
        }

        /*
        public int numTimesListing() { return _listingDates.Length; }
        public bool isBuyable() { return _listingDates.Length == 3; }
        */
        public bool stillValid() { return _auctionDay > DateTime.Today; }
        public String getName() { return _address; }

        public String getAddress() { return _address; }
        public Firm getFirm() { return _firm; }
        public DateTime getListingDate() { return _listingDate; }
        public DateTime getAuctionDate() { return _auctionDay; }
        public String[] getLinks() { return Links; }
    }
}