namespace AGWorld_Listings_App
{
    partial class Expanded_Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtAddress = new TextBox();
            txtName = new TextBox();
            txtContact = new TextBox();
            boxPriority = new ListBox();
            richNotes = new RichTextBox();
            txtAuction = new TextBox();
            txtListing = new TextBox();
            lblAuction = new Label();
            lblListing = new Label();
            txtPhone = new TextBox();
            txtFax = new TextBox();
            lblPhone = new Label();
            lblFax = new Label();
            listingLinkBox = new RichTextBox();
            boxAuthor = new ListBox();
            btnNote = new Button();
            label1 = new Label();
            txtFirmName = new TextBox();
            label2 = new Label();
            txtFirmAddress = new RichTextBox();
            button1 = new Button();
            SuspendLayout();
            // 
            // txtAddress
            // 
            txtAddress.Location = new Point(4, 11);
            txtAddress.Name = "txtAddress";
            txtAddress.ReadOnly = true;
            txtAddress.Size = new Size(200, 23);
            txtAddress.TabIndex = 0;
            txtAddress.Text = "Address";
            // 
            // txtName
            // 
            txtName.Location = new Point(209, 11);
            txtName.Name = "txtName";
            txtName.Size = new Size(191, 23);
            txtName.TabIndex = 1;
            txtName.Text = "Author Name";
            txtName.TextChanged += txtName_TextChanged;
            // 
            // txtContact
            // 
            txtContact.Location = new Point(405, 11);
            txtContact.Name = "txtContact";
            txtContact.Size = new Size(179, 23);
            txtContact.TabIndex = 2;
            txtContact.Text = "Author Contact";
            txtContact.TextChanged += txtContact_TextChanged;
            // 
            // boxPriority
            // 
            boxPriority.FormattingEnabled = true;
            boxPriority.ItemHeight = 15;
            boxPriority.Items.AddRange(new object[] { "Disregarded", "Low", "High" });
            boxPriority.Location = new Point(4, 51);
            boxPriority.Name = "boxPriority";
            boxPriority.Size = new Size(201, 19);
            boxPriority.TabIndex = 3;
            // 
            // richNotes
            // 
            richNotes.Location = new Point(209, 38);
            richNotes.Name = "richNotes";
            richNotes.Size = new Size(375, 428);
            richNotes.TabIndex = 4;
            richNotes.Text = "";
            richNotes.TextChanged += richNotes_TextChanged;
            // 
            // txtAuction
            // 
            txtAuction.Location = new Point(51, 89);
            txtAuction.Name = "txtAuction";
            txtAuction.ReadOnly = true;
            txtAuction.Size = new Size(153, 23);
            txtAuction.TabIndex = 5;
            txtAuction.Text = "mm/dd/yy";
            // 
            // txtListing
            // 
            txtListing.Location = new Point(51, 116);
            txtListing.Name = "txtListing";
            txtListing.ReadOnly = true;
            txtListing.Size = new Size(153, 23);
            txtListing.TabIndex = 6;
            txtListing.Text = "mm/dd/yy";
            // 
            // lblAuction
            // 
            lblAuction.AutoSize = true;
            lblAuction.Location = new Point(1, 89);
            lblAuction.Name = "lblAuction";
            lblAuction.Size = new Size(49, 15);
            lblAuction.TabIndex = 7;
            lblAuction.Text = "Auction";
            // 
            // lblListing
            // 
            lblListing.AutoSize = true;
            lblListing.Location = new Point(1, 116);
            lblListing.Name = "lblListing";
            lblListing.Size = new Size(42, 15);
            lblListing.TabIndex = 8;
            lblListing.Text = "Listing";
            // 
            // txtPhone
            // 
            txtPhone.Location = new Point(49, 168);
            txtPhone.Name = "txtPhone";
            txtPhone.Size = new Size(154, 23);
            txtPhone.TabIndex = 9;
            // 
            // txtFax
            // 
            txtFax.Location = new Point(50, 194);
            txtFax.Name = "txtFax";
            txtFax.Size = new Size(154, 23);
            txtFax.TabIndex = 10;
            // 
            // lblPhone
            // 
            lblPhone.AutoSize = true;
            lblPhone.Location = new Point(24, 170);
            lblPhone.Name = "lblPhone";
            lblPhone.Size = new Size(21, 15);
            lblPhone.TabIndex = 11;
            lblPhone.Text = "Ph";
            // 
            // lblFax
            // 
            lblFax.AutoSize = true;
            lblFax.Location = new Point(27, 196);
            lblFax.Name = "lblFax";
            lblFax.Size = new Size(19, 15);
            lblFax.TabIndex = 12;
            lblFax.Text = "Fx";
            // 
            // listingLinkBox
            // 
            listingLinkBox.Location = new Point(4, 277);
            listingLinkBox.Name = "listingLinkBox";
            listingLinkBox.ReadOnly = true;
            listingLinkBox.Size = new Size(200, 33);
            listingLinkBox.TabIndex = 13;
            listingLinkBox.Text = "";
            // 
            // boxAuthor
            // 
            boxAuthor.FormattingEnabled = true;
            boxAuthor.ItemHeight = 15;
            boxAuthor.Items.AddRange(new object[] { "Note Author 1", "Note Author 2" });
            boxAuthor.Location = new Point(4, 315);
            boxAuthor.Name = "boxAuthor";
            boxAuthor.Size = new Size(200, 94);
            boxAuthor.TabIndex = 15;
            boxAuthor.DoubleClick += boxAuthor_SelectedIndexChanged;
            // 
            // btnNote
            // 
            btnNote.Location = new Point(4, 424);
            btnNote.Name = "btnNote";
            btnNote.Size = new Size(150, 41);
            btnNote.TabIndex = 16;
            btnNote.Text = "Add Note";
            btnNote.UseVisualStyleBackColor = true;
            btnNote.Click += btnNote_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(11, 142);
            label1.Name = "label1";
            label1.Size = new Size(31, 15);
            label1.TabIndex = 18;
            label1.Text = "Firm";
            // 
            // txtFirmName
            // 
            txtFirmName.Location = new Point(50, 142);
            txtFirmName.Name = "txtFirmName";
            txtFirmName.Size = new Size(153, 23);
            txtFirmName.TabIndex = 17;
            txtFirmName.Text = "Firm Name";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(1, 218);
            label2.Name = "label2";
            label2.Size = new Size(76, 15);
            label2.TabIndex = 20;
            label2.Text = "Firm Address";
            // 
            // txtFirmAddress
            // 
            txtFirmAddress.Location = new Point(4, 236);
            txtFirmAddress.Name = "txtFirmAddress";
            txtFirmAddress.Size = new Size(199, 36);
            txtFirmAddress.TabIndex = 21;
            txtFirmAddress.Text = "";
            // 
            // button1
            // 
            button1.Location = new Point(158, 424);
            button1.Name = "button1";
            button1.Size = new Size(44, 40);
            button1.TabIndex = 22;
            button1.Text = "Del\r\n";
            button1.UseVisualStyleBackColor = true;
            button1.Click += delBtn_Click;
            // 
            // Expanded_Form
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(587, 469);
            Controls.Add(button1);
            Controls.Add(txtFirmAddress);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtFirmName);
            Controls.Add(btnNote);
            Controls.Add(boxAuthor);
            Controls.Add(listingLinkBox);
            Controls.Add(lblFax);
            Controls.Add(lblPhone);
            Controls.Add(txtFax);
            Controls.Add(txtPhone);
            Controls.Add(lblListing);
            Controls.Add(lblAuction);
            Controls.Add(txtListing);
            Controls.Add(txtAuction);
            Controls.Add(richNotes);
            Controls.Add(boxPriority);
            Controls.Add(txtContact);
            Controls.Add(txtName);
            Controls.Add(txtAddress);
            Name = "Expanded_Form";
            Text = "Expanded View";
            FormClosing += Expanded_Form_FormClosing;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtContact;
        private System.Windows.Forms.ListBox boxPriority;
        private System.Windows.Forms.RichTextBox richNotes;
        private System.Windows.Forms.TextBox txtAuction;
        private System.Windows.Forms.TextBox txtListing;
        private System.Windows.Forms.Label lblAuction;
        private System.Windows.Forms.Label lblListing;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.TextBox txtFax;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.Label lblFax;
        private System.Windows.Forms.RichTextBox listingLinkBox;
        private System.Windows.Forms.ListBox boxAuthor;
        private System.Windows.Forms.Button btnNote;
        private Label label1;
        private TextBox txtFirmName;
        private Label label2;
        private RichTextBox txtFirmAddress;
        private Button button1;
    }
}