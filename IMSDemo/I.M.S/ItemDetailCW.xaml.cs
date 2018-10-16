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
using System.ServiceModel.DomainServices.Client;
using System.Windows.Media.Imaging;

namespace I.M.S
{
    public partial class ItemDetailCW : ChildWindow
    {
        private int? id;

        public int? ItemID
        {
            get { return id; }
            set { id = value; }
        }
        private IMSDomainContext context;
        private LoadOperation<ItemDetail> loadOp;
        private LoadOperation<Location> loadOpLoc;
        private LoadOperation<Employee> loadOpEmp;
        public ItemDetailCW()
        {
            InitializeComponent();
            context = new IMSDomainContext();
            loadOpLoc = context.Load(context.GetLocationsQuery());
            loadOpEmp = context.Load(context.GetEmployeesQuery());
        }


        public void LoadData(int? id)
        {
            this.ItemID = id;
            loadOp = context.Load(context.GetItemDetailsQuery());
            loadOp.Completed += new EventHandler(loadOp_Completed);
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void loadOp_Completed(object sender, EventArgs e)
        {
            var items = (from o in loadOp.Entities
                         where o.ItemID == ItemID
                         select o).ToList();
            var defaultItem = items[0];
            List<clsItemDetail> itemList = new List<clsItemDetail>();
            clsItemDetail item;
            int[] sizeCount = new int[7] { 0, 0, 0, 0, 0, 0,0 };
            double[] ratio = new double[6]{1,3.0/4, 2.0/3,1.0/2,1.0/3,1.0/4};
            bool flag = false;
            foreach (ItemDetail id in items)
            {
                flag = false;
                item = new clsItemDetail();
                item.ItemName = id.ItemName;
                item.ItemCode = id.ItemBarcode;
                item.Qty = id.Qty;
                item.ScanDate = id.ScanDate;
                item.Location = (from o in loadOpLoc.Entities
                                 where o.LocationID == id.LocationID
                                 select o).First().LocationName;
                item.Employee = (from o in loadOpEmp.Entities
                                 where o.EmployeeID == id.EmployeeID
                                 select o).First().EmployeeName;
                item.FullSize = id.FullSize;

                for (int i = 0; i < 6; i++)
                {
                    if ((int)Math.Round(ratio[i] * (int)id.FullSize) == (int)id.Qty)
                    {
                        sizeCount[i]++;
                        flag = true;
                        break;
                    }   
                }
                if (flag == false)
                {
                    sizeCount[6]++;
                }
                itemList.Add(item);
            }
            dgItemDetails.ItemsSource = itemList;
            lblItemName.Content = defaultItem.ItemName;
            lblFullSize.Content = defaultItem.FullSize;
            lblItemCode.Content = defaultItem.ItemBarcode;
            lblSize1.Content = sizeCount[0];
            lblSize2.Content = sizeCount[1];
            lblSize3.Content = sizeCount[2];
            lblSize4.Content = sizeCount[3];
            lblSize5.Content = sizeCount[4];
            lblSize6.Content = sizeCount[5];
            lblSize7.Content = sizeCount[6];
            imgItem.ImageSource = new BitmapImage(new Uri("Images/" + defaultItem.ItemImage.ToString() + ".jpg", UriKind.RelativeOrAbsolute));
        }
    }
}

