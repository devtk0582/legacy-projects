namespace AtoTechScan
{
    partial class popupManualEntry
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(popupManualEntry));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.Panel2 = new System.Windows.Forms.Panel();
            this.pbClose = new System.Windows.Forms.PictureBox();
            this.lblCaption = new System.Windows.Forms.Label();
            this.txtMEItemNum = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.txtMEBatchNum = new System.Windows.Forms.TextBox();
            this.lbItemNum = new System.Windows.Forms.Label();
            this.lbBatchNum = new System.Windows.Forms.Label();
            this.Panel1.SuspendLayout();
            this.Panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel1
            // 
            this.Panel1.BackColor = System.Drawing.Color.LightCyan;
            this.Panel1.Controls.Add(this.lbBatchNum);
            this.Panel1.Controls.Add(this.lbItemNum);
            this.Panel1.Controls.Add(this.txtMEBatchNum);
            this.Panel1.Controls.Add(this.Panel2);
            this.Panel1.Controls.Add(this.txtMEItemNum);
            this.Panel1.Controls.Add(this.btnClose);
            this.Panel1.Controls.Add(this.btnSubmit);
            this.Panel1.Location = new System.Drawing.Point(62, 130);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(372, 230);
            // 
            // Panel2
            // 
            this.Panel2.BackColor = System.Drawing.Color.DarkSlateGray;
            this.Panel2.Controls.Add(this.pbClose);
            this.Panel2.Controls.Add(this.lblCaption);
            this.Panel2.Location = new System.Drawing.Point(0, 2);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(372, 33);
            // 
            // pbClose
            // 
            this.pbClose.Image = ((System.Drawing.Image)(resources.GetObject("pbClose.Image")));
            this.pbClose.Location = new System.Drawing.Point(339, 0);
            this.pbClose.Name = "pbClose";
            this.pbClose.Size = new System.Drawing.Size(33, 33);
            this.pbClose.Click += new System.EventHandler(this.pbClose_Click);
            // 
            // lblCaption
            // 
            this.lblCaption.BackColor = System.Drawing.Color.DarkSlateGray;
            this.lblCaption.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblCaption.ForeColor = System.Drawing.Color.White;
            this.lblCaption.Location = new System.Drawing.Point(0, 0);
            this.lblCaption.Name = "lblCaption";
            this.lblCaption.Size = new System.Drawing.Size(236, 33);
            this.lblCaption.Text = "Manual Entry";
            // 
            // txtMEItemNum
            // 
            this.txtMEItemNum.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.txtMEItemNum.Location = new System.Drawing.Point(174, 51);
            this.txtMEItemNum.Name = "txtMEItemNum";
            this.txtMEItemNum.Size = new System.Drawing.Size(185, 41);
            this.txtMEItemNum.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.DarkSlateGray;
            this.btnClose.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(238, 172);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(99, 29);
            this.btnClose.TabIndex = 103;
            this.btnClose.Text = "Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSubmit
            // 
            this.btnSubmit.BackColor = System.Drawing.Color.DarkSlateGray;
            this.btnSubmit.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.btnSubmit.ForeColor = System.Drawing.Color.White;
            this.btnSubmit.Location = new System.Drawing.Point(56, 172);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(99, 29);
            this.btnSubmit.TabIndex = 102;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // txtMEBatchNum
            // 
            this.txtMEBatchNum.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.txtMEBatchNum.Location = new System.Drawing.Point(174, 98);
            this.txtMEBatchNum.Name = "txtMEBatchNum";
            this.txtMEBatchNum.Size = new System.Drawing.Size(185, 41);
            this.txtMEBatchNum.TabIndex = 104;
            // 
            // lbItemNum
            // 
            this.lbItemNum.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lbItemNum.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lbItemNum.Location = new System.Drawing.Point(3, 51);
            this.lbItemNum.Name = "lbItemNum";
            this.lbItemNum.Size = new System.Drawing.Size(152, 30);
            this.lbItemNum.Text = "Product Code: ";
            // 
            // lbBatchNum
            // 
            this.lbBatchNum.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lbBatchNum.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lbBatchNum.Location = new System.Drawing.Point(3, 99);
            this.lbBatchNum.Name = "lbBatchNum";
            this.lbBatchNum.Size = new System.Drawing.Size(165, 40);
            this.lbBatchNum.Text = "Batch Number: ";
            // 
            // popupManualEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(480, 536);
            this.Controls.Add(this.Panel1);
            this.Location = new System.Drawing.Point(0, 52);
            this.Menu = this.mainMenu1;
            this.Name = "popupManualEntry";
            this.Text = "Manual Entry";
            this.Panel1.ResumeLayout(false);
            this.Panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Panel Panel1;
        internal System.Windows.Forms.Panel Panel2;
        internal System.Windows.Forms.Label lblCaption;
        internal System.Windows.Forms.TextBox txtMEItemNum;
        internal System.Windows.Forms.Button btnClose;
        internal System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.PictureBox pbClose;
        private System.Windows.Forms.Label lbItemNum;
        private System.Windows.Forms.TextBox txtMEBatchNum;
        private System.Windows.Forms.Label lbBatchNum;
    }
}