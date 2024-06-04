using System.Reflection;

namespace AGWorld_Listings_App
{

    internal partial class Expanded_Form : Form
    {
        Note selectedNote;
        ListingEntry entry;
        ListingEntry originalEntry;
        Form1 parent;

        public Expanded_Form(ListingEntry listing, Form1 parent)
        {
            InitializeComponent();
            txtAddress.Text = listing.getAddress();
            boxPriority.SelectedIndex = listing.getPriority();
            txtAuction.Text = listing.getListing().getAuctionDate().ToString();
            txtListing.Text = listing.getListing().getListingDate().ToString();
            txtPhone.Text = listing.getListing().getFirm()._phoneNumber;
            txtFax.Text = listing.getListing().getFirm()._faxNumber;
            txtFirmName.Text = listing.getListing().getFirm()._name;
            txtFirmAddress.Text = listing.getListing().getFirm()._address;
            boxAuthor.Items.Clear();
            entry = listing;
            originalEntry = new ListingEntry(listing);
            foreach (Note n in listing.getNotes())
            {
                boxAuthor.Items.Add(n._author);
            }
            selectedNote = listing.getNotes()[0];

            txtName.Text = selectedNote.getAuthor();
            txtContact.Text = selectedNote.getContact();
            richNotes.Text = selectedNote.getContent();

            foreach (String url in listing.getListing().getLinks())
            {
                listingLinkBox.Text += url + "\n";
            }

            this.parent = parent;
        }

        public void boxAuthor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (boxAuthor.SelectedIndex != -1)
            {

                selectedNote._author = txtName.Text;
                selectedNote._contact = txtContact.Text;
                selectedNote._content = richNotes.Text;
                String name = (boxAuthor.Items[boxAuthor.SelectedIndex].ToString());
                selectedNote = entry.getNoteByAuthor(name);
                txtName.Text = selectedNote._author;
                txtContact.Text = selectedNote._contact;
                richNotes.Text = selectedNote._content;
            }
        }

        public void btnNote_Click(object sender, EventArgs e)
        {
            Note temp = (new Note("empty note"));
            entry.getNotes().Add(temp);
            boxAuthor.Items.Add(temp._author);
        }

        public void richNotes_TextChanged(object sender, EventArgs e)
        {
            selectedNote.setContent(richNotes.Text);
        }

        private void Expanded_Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult r = MessageBox.Show("Save changes to current listing:" + entry.getAddress() + "?", "Save changes?", MessageBoxButtons.YesNo);
            if (r == DialogResult.Yes)
            {
                String Address = txtAddress.Text;
                int Priority = boxPriority.SelectedIndex;
                DateTime Auction = DateTime.Parse(txtAuction.Text);
                DateTime Listing = DateTime.Parse(txtListing.Text);
                String Phone = txtPhone.Text;
                String Fax = txtFax.Text;
                String FirmName = txtFirmName.Text;
                String FirmAddress = txtFirmAddress.Text;
                Listing_Info info = new Listing_Info(
                    Address,
                    new Firm(FirmName, Phone, Fax, FirmAddress),
                    "",
                    Listing,
                    Auction
                    );
                entry.setAll(info, Priority, entry.getNotes());
            } else
            {
                entry.setAll(originalEntry.getListing(), originalEntry.getPriority(), originalEntry.getNotes());
            }
            
            parent.reloadCurrentList();
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            String AuthorListName = selectedNote.getAuthor();
            int indexOfAuthor = boxAuthor.Items.IndexOf(AuthorListName);
            boxAuthor.Items[indexOfAuthor] = txtName.Text;
            selectedNote._author = txtName.Text;
        }

        private void delBtn_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("Delete Note:" + selectedNote._author + "?", "Delete", MessageBoxButtons.YesNo);
            if (r == DialogResult.Yes)
            {
                entry.delNote(selectedNote);
                boxAuthor.Items.Clear();
                foreach (Note n in entry.getNotes())
                {
                    boxAuthor.Items.Add(n._author);
                }
            }
        }

        private void txtContact_TextChanged(object sender, EventArgs e)
        {
            selectedNote._contact = txtContact.Text;
        }
    }
}
