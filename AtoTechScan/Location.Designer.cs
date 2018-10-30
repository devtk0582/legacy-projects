namespace AtoTechScan
{
    partial class Location
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Location));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnMain = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.txtLocationName = new System.Windows.Forms.TextBox();
            this.lbCustName = new System.Windows.Forms.Label();
            this.lbLocationName = new System.Windows.Forms.Label();
            this.lbActivity = new System.Windows.Forms.Label();
            this.lbUser = new System.Windows.Forms.Label();
            this.llLogout = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(63, 290);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(356, 40);
            this.label1.Text = "Please Scan or Enter Location";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(142, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(200, 100);
            // 
            // btnMain
            // 
            this.btnMain.Location = new System.Drawing.Point(52, 449);
            this.btnMain.Name = "btnMain";
            this.btnMain.Size = new System.Drawing.Size(163, 40);
            this.btnMain.TabIndex = 3;
            this.btnMain.Text = "MAIN MENU";
            this.btnMain.Click += new System.EventHandler(this.btnMain_Click);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(288, 449);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(144, 40);
            this.btnNext.TabIndex = 4;
            this.btnNext.Text = "Next";
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // txtLocationName
            // 
            this.txtLocationName.Location = new System.Drawing.Point(94, 342);
            this.txtLocationName.Name = "txtLocationName";
            this.txtLocationName.Size = new System.Drawing.Size(301, 41);
            this.txtLocationName.TabIndex = 7;
            // 
            // lbCustName
            // 
            this.lbCustName.Location = new System.Drawing.Point(17, 146);
            this.lbCustName.Name = "lbCustName";
            this.lbCustName.Size = new System.Drawing.Size(423, 41);
            this.lbCustName.Text = "Customer: ";
            // 
            // lbLocationName
            // 
            this.lbLocationName.Location = new System.Drawing.Point(17, 186);
            this.lbLocationName.Name = "lbLocationName";
            this.lbLocationName.Size = new System.Drawing.Size(423, 40);
            this.lbLocationName.Text = "Location: ";
            // 
            // lbActivity
            // 
            this.lbActivity.Location = new System.Drawing.Point(17, 226);
            this.lbActivity.Name = "lbActivity";
            this.lbActivity.Size = new System.Drawing.Size(423, 40);
            this.lbActivity.Text = "Activity: ";
            // 
            // lbUser
            // 
            this.lbUser.Location = new System.Drawing.Point(189, 106);
            this.lbUser.Name = "lbUser";
            this.lbUser.Size = new System.Drawing.Size(241, 40);
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
            // Location
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
            this.Controls.Add(this.txtLocationName);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnMain);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.Location = new System.Drawing.Point(0, 52);
            this.Menu = this.mainMenu1;
            this.MinimizeBox = false;
            this.Name = "Location";
            this.Text = "Location";
            this.Load += new System.EventHandler(this.Location_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnMain;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.TextBox txtLocationName;
        private System.Windows.Forms.Label lbCustName;
        private System.Windows.Forms.Label lbLocationName;
        private System.Windows.Forms.Label lbActivity;
        private System.Windows.Forms.Label lbUser;
        private System.Windows.Forms.LinkLabel llLogout;
    }
}