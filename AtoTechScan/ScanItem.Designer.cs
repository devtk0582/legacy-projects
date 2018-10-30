namespace AtoTechScan
{
    partial class ScanItem
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScanItem));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.pbLogo = new System.Windows.Forms.PictureBox();
            this.lbCust = new System.Windows.Forms.Label();
            this.lbLocation = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.lbQty = new System.Windows.Forms.Label();
            this.lbActivity = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lbLastScan = new System.Windows.Forms.Label();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.llLogout = new System.Windows.Forms.LinkLabel();
            this.lbQtyNum = new System.Windows.Forms.Label();
            this.lbScanItem = new System.Windows.Forms.Label();
            this.btnBack = new System.Windows.Forms.Button();
            this.lbUser = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // pbLogo
            // 
            this.pbLogo.Image = ((System.Drawing.Image)(resources.GetObject("pbLogo.Image")));
            this.pbLogo.Location = new System.Drawing.Point(140, 3);
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.Size = new System.Drawing.Size(200, 100);
            // 
            // lbCust
            // 
            this.lbCust.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lbCust.Location = new System.Drawing.Point(169, 146);
            this.lbCust.Name = "lbCust";
            this.lbCust.Size = new System.Drawing.Size(298, 34);
            // 
            // lbLocation
            // 
            this.lbLocation.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lbLocation.Location = new System.Drawing.Point(169, 183);
            this.lbLocation.Name = "lbLocation";
            this.lbLocation.Size = new System.Drawing.Size(298, 31);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Verdana", 11F, System.Drawing.FontStyle.Regular);
            this.label1.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label1.Location = new System.Drawing.Point(100, 251);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(275, 37);
            this.label1.Text = "Please Scan Item";
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.DarkSlateGray;
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(218, 409);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(107, 32);
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lbQty
            // 
            this.lbQty.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lbQty.Location = new System.Drawing.Point(218, 354);
            this.lbQty.Name = "lbQty";
            this.lbQty.Size = new System.Drawing.Size(122, 40);
            this.lbQty.Text = "Quantity: ";
            // 
            // lbActivity
            // 
            this.lbActivity.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lbActivity.Location = new System.Drawing.Point(169, 211);
            this.lbActivity.Name = "lbActivity";
            this.lbActivity.Size = new System.Drawing.Size(298, 31);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.DarkSlateGray;
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(356, 409);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(104, 32);
            this.btnCancel.TabIndex = 25;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lbLastScan
            // 
            this.lbLastScan.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lbLastScan.Location = new System.Drawing.Point(20, 288);
            this.lbLastScan.Name = "lbLastScan";
            this.lbLastScan.Size = new System.Drawing.Size(225, 35);
            this.lbLastScan.Text = "Last Scanned Item:  ";
            // 
            // txtQty
            // 
            this.txtQty.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.txtQty.Location = new System.Drawing.Point(346, 354);
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(111, 41);
            this.txtQty.TabIndex = 34;
            // 
            // llLogout
            // 
            this.llLogout.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.llLogout.Location = new System.Drawing.Point(370, 15);
            this.llLogout.Name = "llLogout";
            this.llLogout.Size = new System.Drawing.Size(90, 40);
            this.llLogout.TabIndex = 42;
            this.llLogout.Text = "Logout";
            this.llLogout.Click += new System.EventHandler(this.llLogout_Click);
            // 
            // lbQtyNum
            // 
            this.lbQtyNum.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lbQtyNum.Location = new System.Drawing.Point(20, 354);
            this.lbQtyNum.Name = "lbQtyNum";
            this.lbQtyNum.Size = new System.Drawing.Size(155, 31);
            // 
            // lbScanItem
            // 
            this.lbScanItem.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lbScanItem.Location = new System.Drawing.Point(20, 323);
            this.lbScanItem.Name = "lbScanItem";
            this.lbScanItem.Size = new System.Drawing.Size(440, 31);
            this.lbScanItem.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.Color.DarkSlateGray;
            this.btnBack.ForeColor = System.Drawing.Color.White;
            this.btnBack.Location = new System.Drawing.Point(282, 464);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(165, 52);
            this.btnBack.TabIndex = 50;
            this.btnBack.Text = "Home";
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // lbUser
            // 
            this.lbUser.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lbUser.Location = new System.Drawing.Point(192, 106);
            this.lbUser.Name = "lbUser";
            this.lbUser.Size = new System.Drawing.Size(285, 40);
            this.lbUser.Text = "Welcome, ";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label2.Location = new System.Drawing.Point(3, 146);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(160, 34);
            this.label2.Text = "Customer: ";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label3.Location = new System.Drawing.Point(3, 180);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(160, 34);
            this.label3.Text = "Location: ";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label4.Location = new System.Drawing.Point(3, 211);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(160, 31);
            this.label4.Text = "Activity: ";
            // 
            // ScanItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.LightCyan;
            this.ClientSize = new System.Drawing.Size(480, 536);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbUser);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.lbScanItem);
            this.Controls.Add(this.lbQtyNum);
            this.Controls.Add(this.llLogout);
            this.Controls.Add(this.txtQty);
            this.Controls.Add(this.lbLastScan);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lbActivity);
            this.Controls.Add(this.lbQty);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbLocation);
            this.Controls.Add(this.lbCust);
            this.Controls.Add(this.pbLogo);
            this.KeyPreview = true;
            this.Location = new System.Drawing.Point(0, 52);
            this.Menu = this.mainMenu1;
            this.MinimizeBox = false;
            this.Name = "ScanItem";
            this.Text = "ScanItem";
            this.Load += new System.EventHandler(this.ScanItem_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ScanItem_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbLogo;
        private System.Windows.Forms.Label lbCust;
        private System.Windows.Forms.Label lbLocation;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lbQty;
        private System.Windows.Forms.Label lbActivity;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lbLastScan;
        private System.Windows.Forms.TextBox txtQty;
        private System.Windows.Forms.LinkLabel llLogout;
        private System.Windows.Forms.Label lbQtyNum;
        private System.Windows.Forms.Label lbScanItem;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Label lbUser;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}