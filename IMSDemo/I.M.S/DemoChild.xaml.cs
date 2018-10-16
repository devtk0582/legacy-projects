using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using I.M.S.Web;

namespace I.M.S
{
    public partial class DemoChild : ChildWindow
    {
        private int? id;

        public int? ItemID
        {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get { return txtName.Text; }
            set { txtName.Text = value; }
        }

        public string Code
        {
            get { return txtCode.Text; }
            set { txtCode.Text = value; }
        }

        public string Qty
        {
            get { return txtQty.Text; }
            set { txtQty.Text = value; }
        }

        public string Date
        {
            get { return txtDate.Text; }
            set { txtDate.Text = value; }
        }


        private IMSDomainContext context;

        public DemoChild()
        {
            InitializeComponent();
            context = new IMSDomainContext();
            context.Load(context.GetItemDetailsQuery());
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            var item = (from o in context.ItemDetails  
                        where o.ItemID == ItemID
                        select o).First();
            item.ItemName = txtName.Text;
            item.ItemBarcode = txtCode.Text;
            item.Qty = int.Parse(txtQty.Text);
            item.ScanDate = Convert.ToDateTime(txtDate.Text);
            context.SubmitChanges();
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}

