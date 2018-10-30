namespace AtoTechScan
{
    partial class MainMenu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainMenu));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.btnScan = new System.Windows.Forms.Button();
            this.btnChgCust = new System.Windows.Forms.Button();
            this.btnChgLocation = new System.Windows.Forms.Button();
            this.btnChgActivity = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lbCustName = new System.Windows.Forms.Label();
            this.lbLocationName = new System.Windows.Forms.Label();
            this.lbActivity = new System.Windows.Forms.Label();
            this.lbUser = new System.Windows.Forms.Label();
            this.llLogout = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // btnScan
            // 
            this.btnScan.Location = new System.Drawing.Point(132, 275);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(232, 40);
            this.btnScan.TabIndex = 0;
            this.btnScan.Text = "Start Scan";
            this.btnScan.Click += new System.EventHandler(this.btnScan_Click);
            // 
            // btnChgCust
            // 
            this.btnChgCust.Location = new System.Drawing.Point(132, 340);
            this.btnChgCust.Name = "btnChgCust";
            this.btnChgCust.Size = new System.Drawing.Size(232, 40);
            this.btnChgCust.TabIndex = 1;
            this.btnChgCust.Text = "Change Customer";
            this.btnChgCust.Click += new System.EventHandler(this.btnChgCust_Click);
            // 
            // btnChgLocation
            // 
            this.btnChgLocation.Location = new System.Drawing.Point(132, 407);
            this.btnChgLocation.Name = "btnChgLocation";
            this.btnChgLocation.Size = new System.Drawing.Size(232, 40);
            this.btnChgLocation.TabIndex = 2;
            this.btnChgLocation.Text = "Change Location";
            this.btnChgLocation.Click += new System.EventHandler(this.btnChgLocation_Click);
            // 
            // btnChgActivity
            // 
            this.btnChgActivity.Location = new System.Drawing.Point(132, 470);
            this.btnChgActivity.Name = "btnChgActivity";
            this.btnChgActivity.Size = new System.Drawing.Size(232, 40);
            this.btnChgActivity.TabIndex = 3;
            this.btnChgActivity.Text = "Change Activity";
            this.btnChgActivity.Click += new System.EventHandler(this.btnChgActivity_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(142, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(200, 100);
            // 
            // lbCustName
            // 
            this.lbCustName.Location = new System.Drawing.Point(22, 144);
            this.lbCustName.Name = "lbCustName";
            this.lbCustName.Size = new System.Drawing.Size(421, 40);
            this.lbCustName.Text = "Customer: ";
            // 
            // lbLocationName
            // 
            this.lbLocationName.Location = new System.Drawing.Point(22, 192);
            this.lbLocationName.Name = "lbLocationName";
            this.lbLocationName.Size = new System.Drawing.Size(421, 40);
            this.lbLocationName.Text = "Location: ";
            // 
            // lbActivity
            // 
            this.lbActivity.Location = new System.Drawing.Point(22, 232);
            this.lbActivity.Name = "lbActivity";
            this.lbActivity.Size = new System.Drawing.Size(421, 40);
            this.lbActivity.Text = "Activity: ";
            // 
            // lbUser
            // 
            this.lbUser.Location = new System.Drawing.Point(181, 106);
            this.lbUser.Name = "lbUser";
            this.lbUser.Size = new System.Drawing.Size(286, 38);
            this.lbUser.Text = "Welcome, ";
            // 
            // llLogout
            // 
            this.llLogout.ForeColor = System.Drawing.Color.MediumBlue;
            this.llLogout.Location = new System.Drawing.Point(370, 15);
            this.llLogout.Name = "llLogout";
            this.llLogout.Size = new System.Drawing.Size(90, 40);
            this.llLogout.TabIndex = 44;
            this.llLogout.Text = "Logout";
            this.llLogout.Click += new System.EventHandler(this.llLogout_Click);
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(480, 536);
            this.Controls.Add(this.llLogout);
            this.Controls.Add(this.lbUser);
            this.Controls.Add(this.lbActivity);
            this.Controls.Add(this.lbLocationName);
            this.Controls.Add(this.lbCustName);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnChgActivity);
            this.Controls.Add(this.btnChgLocation);
            this.Controls.Add(this.btnChgCust);
            this.Controls.Add(this.btnScan);
            this.Location = new System.Drawing.Point(0, 52);
            this.Menu = this.mainMenu1;
            this.MinimizeBox = false;
            this.Name = "MainMenu";
            this.Text = "Main Menu";
            this.Load += new System.EventHandler(this.MainMenu_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnScan;
        private System.Windows.Forms.Button btnChgCust;
        private System.Windows.Forms.Button btnChgLocation;
        private System.Windows.Forms.Button btnChgActivity;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lbCustName;
        private System.Windows.Forms.Label lbLocationName;
        private System.Windows.Forms.Label lbActivity;
        private System.Windows.Forms.Label lbUser;
        private System.Windows.Forms.LinkLabel llLogout;
    }
}