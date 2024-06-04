using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AGWorld_Listings_App
{
    internal class Listings_Manager
    {
        [JsonInclude]
        private Dictionary<String, ListingEntry> _listings;

        public Listings_Manager()
        {
            _listings = new Dictionary<String, ListingEntry>();
        }

        [JsonConstructor]
        public Listings_Manager(Dictionary<String, ListingEntry> _listings)
        {
            this._listings = _listings;
        }

        //use this to get the current Listings as dictionairy
        public Dictionary<String, ListingEntry> getListings() { return _listings; }

        //USe this to generate rows :)
        //Clears and sets the grid view
        public DataGridView setDataGridView(DataGridView dv)
        {
            dv.Rows.Clear();
            foreach (ListingEntry le in _listings.Values)
            {
                DataGridViewRow dr = new DataGridViewRow();
                dr.SetValues(le);
                dv.Rows.Add(le.toDataGrideRow());
            }
            return dv;
        }

        //Use this to add the current listing or combine it into existing listing
        //returns if the entry existed yet, Compares by string Address
        private bool addListing(ListingEntry entry)
        {
            String entry_Key = entry.getAddress();
            if (_listings.Keys.Contains(entry_Key)){
                _listings[entry_Key] = _listings[entry_Key] + entry;
                return true;
            }
           _listings.Add(entry_Key, entry);
            return false;
        }
        private bool addListing(Listing_Info entry)
        {
            ListingEntry le = new ListingEntry(entry);
            return addListing(le);
        }

        //Use this to save or update a listing after it was edited
        public void updateListing(ListingEntry entry)
        {
            addListing(entry);
        }

        //Use this for loading in extra data
        private void combineListingsTool(List<ListingEntry> listings)
        {
            foreach (ListingEntry entry in listings)
            {
                addListing(entry);
            }
        }

        //Use this to set the default author contact and name (the user's name)
        public void setDefaultContactInfo(String AuthorName, String Contact)
        {
            Note.setDefaultContact(AuthorName, Contact);
        }

        //Use this to load in any file to the current List
        public bool Load(String FilePath)
        {
            if (File.Exists(FilePath))
            {
                List<ListingEntry> loaded = FileManager.load(FilePath);
                combineListingsTool(loaded);
                return true;
            }
            return false;
        }

        //Use this to save an array of listings, or to save the current dictionairy to the filepath
        public void Save(String FilePath) { Save(_listings.Values.ToList<ListingEntry>(), FilePath); }
        public void Save(List<ListingEntry> listings, String FilePath)
        {
            FileManager.save(listings, FilePath);
        }

        //Use this at start to load websites
        public void loadWebsites()
        {
            List<Listing_Info> list = TNLedgerScraper.scrape();
            foreach(Listing_Info info in list)
            {
                addListing(info);
            }
            list = RlseLawWebScrapper.Scrape();
            foreach (Listing_Info info in list)
            {
                addListing(info);
            }
        }

        //Updates the dictionairy from datagridview
        //Assumes no data but the priorty can be changed.
        //IMPORTANT : WILL NOT CHANGE ANY OTHER VALUE THAN PRIORTY
        public void updateFromDataGridView(DataGridView dv)
        {
            for (int i = 0; i < dv.Rows.Count; i++)
            {
                updateFromDataGridView(i, dv);
            }
        }
        public void updateFromDataGridView(DataGridViewCellEventArgs e, DataGridView dv)
        {
            updateFromDataGridView(e.RowIndex, dv);
        }
        public void updateFromDataGridView(int row, DataGridView dv)
        {
            String address = dv.Rows[row].Cells[1].Value.ToString();
            _listings[address].fromDataGridRow(dv.Rows[row]);
        }

        //Grabs the listing object and returns it
        public ListingEntry getListing(int rowIndex, DataGridView dv) 
        {
            return getListing(dv.Rows[rowIndex].Cells[1].Value.ToString());
        }
        public ListingEntry getListing(String address)
        {
            return _listings[address];
        }

        /// <summary>
        /// Opens up a message box to confirm before purging
        /// shows how many records to purge
        /// </summary>
        public void purgeOld()
        {
            List<ListingEntry> purgables = new List<ListingEntry>();
            foreach(ListingEntry entry in _listings.Values)
            {
                if(!entry.getListing().stillValid())
                { purgables.Add(entry); }
            }
            int numPurges = purgables.Count;
            DialogResult r = MessageBox.Show("About to purge " + numPurges + ".\n Do you still want to purge?", "Purge Passed Auctions", MessageBoxButtons.YesNo);
            if (r == DialogResult.Yes)
            {
                foreach(ListingEntry entry in purgables)
                {
                    _listings.Remove(entry.getAddress());
                }
            }
        }

    }
}
