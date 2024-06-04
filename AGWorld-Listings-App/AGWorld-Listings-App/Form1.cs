namespace AGWorld_Listings_App
{
    public partial class Form1 : Form
    {
    //GLOBAL VARIABLES -------------------------------------------------------------------------------------------------
        String defaultFileName()
        {
            return Path.GetFullPath(defaultFileLocation);
        }
        String defaultFileLocation = "../../default.json";
        Listings_Manager manager;
        const String superDefaultFileLocation = "../../default.json";
        const String defaultName = "AG World Webscraper File: ";

        //array containing the apropriate colors for the rows
        //order is two entries for each importance, plus an outdated color at the end
        //example RowColors[0] = High Light Color & RowColors[1] = High Dark Color
        Color[] RowColors =
        {
            Color.LightYellow,
            Color.Yellow,
            Color.LightGray,
            Color.Gray,
            Color.DarkGray,
            Color.DarkGray,
            Color.DimGray
        };

    //CONSTRUCTORS -----------------------------------------------------------------------------------------------------
        public Form1()
        {
            InitializeComponent();
            Text = defaultName;
            setDataGridView();
            manager = new Listings_Manager();
            doLoadWebsitesBox();
            updateDataGridView();

        }

        public Form1(String filePath)
        {
            //loading basics
            InitializeComponent();

            //loading the datagridview table
            setDataGridView();

            //sets default File Location for new subwindow
            defaultFileLocation = filePath;

            manager = new Listings_Manager();

            manager.Load(filePath);
            updateDataGridView();
        }

    //EVENTS -----------------------------------------------------------------------------------------------------------

        //On Combo Box Changed, hide all but chosen priority tags
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells[0].Value.ToString() == comboBox1.Text)
                {
                    dataGridView1.Rows[i].Visible = true;
                }
                else if (comboBox1.Text == "Filter Priority")
                {
                    dataGridView1.Rows[i].Visible = true;
                }
                else
                {
                    dataGridView1.Rows[i].Visible = false;
                }
            }
        }

        //on Cell Double Clicked, open expanded view
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
                {
                    if (e.RowIndex >= 0)
                    {
                        ListingEntry listingToPass = manager.getListing(
                            dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString()
                            );
                        Expanded_Form details = new Expanded_Form(listingToPass, this);
                        details.Show();
                    }
                }

        //On Column Header clicked, recolor table
        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                sortDataGridViewByPriority();
            }
            colorDataGridView();
        }

        //Update Data Grid View on Cell Change
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            manager.updateFromDataGridView(e, dataGridView1);
            updateDataGridView();
        }

        //On Form Close, Prompt user with save changes message box
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult r = MessageBox.Show("Save current data before closing to file:\n" + defaultFileName(), "You have not saved current Data.", MessageBoxButtons.YesNoCancel);
            if (r == DialogResult.Yes)
            {
                manager.Save(defaultFileLocation);
            }
            if (r == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
        }

        //On Form Load, set data grid table to dataGridView1 & Set default Filter text
        private void Form1_Load(object sender, EventArgs e)
        {
            setFormName();
            manager.Load(defaultFileLocation);
            updateDataGridView();
            comboBox1.SelectedItem = "Filter Priority";
        }

        //Import data from existing table into current table
        private void importToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            List<String> invalidFiles = new List<string>();
            OpenFileDialog opd = new OpenFileDialog();
            opd.Multiselect = true;

            DialogResult result = opd.ShowDialog();
            if (result == DialogResult.OK)
            {
                String[] FileNames = opd.FileNames;
                foreach (string f in FileNames)
                {
                    manager.Load(f);

                }
                updateDataGridView();
            }
        }

        //Open function to open listings from existing JSON
        private void openToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            List<String> invalidFiles = new List<string>();
            OpenFileDialog opd = new OpenFileDialog();
            opd.Multiselect = false;

            DialogResult result = opd.ShowDialog();
            if (result == DialogResult.OK)
            {
                Form1 otherForm = new Form1(opd.FileName);
                otherForm.Show();
            }
        }

        //Reload listings from website
        private void reloadSitesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            manager.loadWebsites();
            updateDataGridView();
        }

        //Save all entries into a JSON
        private void saveToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            SaveFileDialog opd = new SaveFileDialog();
            opd.Filter = "JSON files (*.json)|*.json|Folders|*.folder";

            DialogResult result = opd.ShowDialog();
            if (result == DialogResult.OK)
            {
                //file
                if (opd.FileName.Contains(".json"))
                {
                    manager.Save(opd.FileName);
                }
                else//folder
                {
                    manager.Save(opd.FileName + "/Listings.json");
                }
            }
        }

        //Hide entries that do not contain search conditions
        private void searchBox_TextChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Visible = false;
                foreach (DataGridViewCell cell in row.Cells)
                {

                    if (cell.Value.ToString().ToLower().Contains(searchBox.Text.ToLower()))
                    {
                        row.Visible = true;
                    }
                }
            }
            colorDataGridView();
        }

        //Save ONLY selected entries into a JSON
        private void shareToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please Select 1 or More Entry.", "Entry Not Found");
            }
            else
            {
                List<ListingEntry> saveList = new List<ListingEntry>();
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    saveList.Add(manager.getListing(row.Cells[1].Value.ToString()));
                }

                SaveFileDialog opd = new SaveFileDialog();
                opd.Filter = "JSON files (*.json)|*.json|Folders|*.folder";

                DialogResult result = opd.ShowDialog();
                if (result == DialogResult.OK)
                {
                    //file
                    if (opd.FileName.Contains(".json"))
                    {
                        manager.Save(saveList, opd.FileName);
                    }
                    else//folder
                    {
                        manager.Save(saveList, opd.FileName + "/Listings.json");
                    }
                }
            }
        }
       
        
        //Remove entries listed as Past Auction Date
        private void purgePastDueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            manager.purgeOld();
            updateDataGridView();
        }

     //OTHER FUNCTIONS -------------------------------------------------------------------------------------------------

        //Function to call to color the rows currently displayed
        //Alternates color
        //Not to be called directly, called in updateDataGridView
        private void colorDataGridView()
        {
            Boolean isDark = false;
            foreach (DataGridViewRow r in dataGridView1.Rows)
            {
                if (r.Visible)
                {
                    r.DefaultCellStyle.BackColor = getColorForRow(r, isDark);
                    isDark = !isDark;
                }
            }
        }

        //Ask user if they want to generate new data from websites
        private void doLoadWebsitesBox()
        {
            DialogResult r = MessageBox.Show("Load new data?", "Fresh Open", MessageBoxButtons.YesNo);
            if (r == DialogResult.Yes)
            {
                manager.loadWebsites();
            }
        }

        //Uses the current row priority to select the appropriate row color
        private Color getColorForRow(DataGridViewRow row, bool isDark)
        {
            if (DateTime.Parse(row.Cells[4].Value.ToString()) < DateTime.Today)
            {
                return RowColors[RowColors.Length - 1];
            }
            Color returnColor = Color.White;

            switch (row.Cells[0].Value.ToString())
            {
                case "High":
                    returnColor = RowColors[0 + (isDark ? 1 : 0)];
                    break;
                case "Low":
                    returnColor = RowColors[2 + (isDark ? 1 : 0)];
                    break;
                case "Disregarded":
                    returnColor = RowColors[4 + (isDark ? 1 : 0)];
                    break;
            }

            return returnColor;
        }

        //used by expanded form to force update the list
        public void reloadCurrentList()
        {
            dataGridView1 = manager.setDataGridView(dataGridView1);
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Visible = false;
                foreach (DataGridViewCell cell in row.Cells)
                {

                    if (cell.Value.ToString().ToLower().Contains(searchBox.Text.ToLower()))
                    {
                        row.Visible = true;
                    }
                }
            }
            colorDataGridView();
        }

        //Creates and formats the Data Grid Table
        private void setDataGridView()
        {
            //Table Headers
            DataGridViewComboBoxColumn dgvCmb = new DataGridViewComboBoxColumn();
            dgvCmb.HeaderText = "Priority";
            dgvCmb.Items.Add("High");
            dgvCmb.Items.Add("Low");
            dgvCmb.Items.Add("Disregarded");
            dgvCmb.Items.Add("PastDate");
            dgvCmb.Name = "priority";
            dataGridView1.Columns.Add(dgvCmb);

            //Table Columns
            String[] row = new String[] { "Address", "Firm", "FirstListing", "AuctionDate", "Notes" };
            foreach (String s in row)
            {
                dataGridView1.Columns.Add(s, s);
            }

            //Table Colors
            dataGridView1.RowsDefaultCellStyle.BackColor = Color.LightGray;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.Gray;
        }

        //Automatically sorts the data grid view based on priority
        //Not to be called directly, called in updateDataGridView
        private void sortDataGridViewByPriority()
        {
            dataGridView1.Sort(Comparer<DataGridViewRow>.Create((x, y) =>
            {
                ListingEntry.Priority priorityX = (ListingEntry.Priority)Enum.Parse(typeof(ListingEntry.Priority), x.Cells["priority"].Value.ToString());
                ListingEntry.Priority priorityY = (ListingEntry.Priority)Enum.Parse(typeof(ListingEntry.Priority), y.Cells["priority"].Value.ToString());
                ListingEntry.Priority[] priorityOrder = { ListingEntry.Priority.High, ListingEntry.Priority.Low, ListingEntry.Priority.Disregarded, ListingEntry.Priority.PastDate };
                return Array.IndexOf(priorityOrder, priorityX).CompareTo(Array.IndexOf(priorityOrder, priorityY));
            }));
        }

        //Used to reload manager into datagridview and call colors
        //Shorthand to call all necessary datagridview update functions
        private void updateDataGridView()
        {
            dataGridView1 = manager.setDataGridView(dataGridView1);
            sortDataGridViewByPriority();
            colorDataGridView();
        }

        //sets this form's name based on current file being viewed
        private void setFormName()
        {
            String title = defaultName + (defaultFileLocation.Equals(superDefaultFileLocation)? "Main File":defaultFileLocation);
            Text = title;
        }
        
    }
}
