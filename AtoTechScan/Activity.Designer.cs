namespace AtoTechScan
{
    partial class Activity
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Activity));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.cbActivity = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pbLogo = new System.Windows.Forms.PictureBox();
            this.btnNext = new System.Windows.Forms.Button();
            this.lbCust = new System.Windows.Forms.Label();
            this.lbLocation = new System.Windows.Forms.Label();
            this.lbActivity = new System.Windows.Forms.Label();
            this.lbUser = new System.Windows.Forms.Label();
            this.llLogout = new System.Windows.Forms.LinkLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cbActivity
            // 
            this.cbActivity.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.cbActivity.Location = new System.Drawing.Point(110, 313);
            this.cbActivity.Name = "cbActivity";
            this.cbActivity.Size = new System.Drawing.Size(254, 41);
            this.cbActivity.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Verdana", 11F, System.Drawing.FontStyle.Regular);
            this.label1.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label1.Location = new System.Drawing.Point(68, 251);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(379, 49);
            this.label1.Text = "Please choose Activity";
            // 
            // pbLogo
            // 
            this.pbLogo.Image = ((System.Drawing.Image)(resources.GetObject("pbLogo.Image")));
            this.pbLogo.Location = new System.Drawing.Point(140, 3);
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.Size = new System.Drawing.Size(200, 100);
            // 
            // btnNext
            // 
            this.btnNext.BackColor = System.Drawing.Color.DarkSlateGray;
            this.btnNext.ForeColor = System.Drawing.Color.White;
            this.btnNext.Location = new System.Drawing.Point(282, 449);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(165, 52);
            this.btnNext.TabIndex = 3;
            this.btnNext.Text = "Next";
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
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
            this.lbLocation.Location = new System.Drawing.Point(169, 180);
            this.lbLocation.Name = "lbLocation";
            this.lbLocation.Size = new System.Drawing.Size(298, 31);
            // 
            // lbActivity
            // 
            this.lbActivity.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lbActivity.Location = new System.Drawing.Point(169, 211);
            this.lbActivity.Name = "lbActivity";
            this.lbActivity.Size = new System.Drawing.Size(298, 31);
            // 
            // lbUser
            // 
            this.lbUser.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lbUser.Location = new System.Drawing.Point(189, 106);
            this.lbUser.Name = "lbUser";
            this.lbUser.Size = new System.Drawing.Size(285, 40);
            this.lbUser.Text = "Welcome, ";
            // 
            // llLogout
            // 
            this.llLogout.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.llLogout.Location = new System.Drawing.Point(370, 15);
            this.llLogout.Name = "llLogout";
            this.llLogout.Size = new System.Drawing.Size(90, 40);
            this.llLogout.TabIndex = 43;
            this.llLogout.Text = "Logout";
            this.llLogout.Click += new System.EventHandler(this.llLogout_Click);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label2.Location = new System.Drawing.Point(0, 146);
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
            this.label4.Location = new System.Drawing.Point(0, 214);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(160, 31);
            this.label4.Text = "Activity: ";
            // 
            // Activity
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.LightCyan;
            this.ClientSize = new System.Drawing.Size(480, 536);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.llLogout);
            this.Controls.Add(this.lbUser);
            this.Controls.Add(this.lbActivity);
            this.Controls.Add(this.lbLocation);
            this.Controls.Add(this.lbCust);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.pbLogo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbActivity);
            this.Location = new System.Drawing.Point(0, 52);
            this.Menu = this.mainMenu1;
            this.MinimizeBox = false;
            this.Name = "Activity";
            this.Text = "Activity";
            this.Load += new System.EventHandler(this.Activity_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbActivity;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pbLogo;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Label lbCust;
        private System.Windows.Forms.Label lbLocation;
        private System.Windows.Forms.Label lbActivity;
        private System.Windows.Forms.Label lbUser;
        private System.Windows.Forms.LinkLabel llLogout;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}