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
using System.Windows.Controls.DataVisualization.Charting;
using System.Runtime.InteropServices.Automation;
using I.M.S.SpeechService;
using System.IO;

namespace I.M.S
{
    public partial class MainPage : UserControl
    {
        //private DemoChild dcw;
        private IMSDomainContext _context = new IMSDomainContext();
        private LoadOperation<ItemDetail> loadOp;
        private ItemDetailCW idcw;

        private List<SizeFull> SizeFullList = new List<SizeFull>();
        private List<Size1of2> Size1of2List = new List<Size1of2>();
        private List<Size1of3> Size1of3List = new List<Size1of3>();
        private List<Size1of4> Size1of4List = new List<Size1of4>();
        private List<Size2of3> Size2of3List = new List<Size2of3>();
        private List<SizeOther> SizeOtherList = new List<SizeOther>();
        private List<Size3of4> Size3of4List = new List<Size3of4>();
        public MainPage()
        {
            InitializeComponent();
            rbPie.IsChecked = true;
            loadOp = this._context.Load(this._context.GetItemDetailsQuery());
            loadOp.Completed += new EventHandler(loadOp_Completed);

            //dcw = new DemoChild();
        }

        private void loadOp_Completed(object sender, EventArgs e)
        {
            
            //clsSeries.IsSelectionEnabled = true;
            BindPieChart();
            BindBarChart();
            BindDetailChart();
            idcw = new ItemDetailCW();
            idcw.Closed += new EventHandler(idcw_Closed);
            //Speak("Welcome to Inventory Management System. Please click one in the graph, you will see detail information about the bottle. Enjoy.");
            //MessageBox.Show(clsSeries.SelectedItem.ToString());
        }

        private void BindBarChart()
        {
            var itemDetails = (from id in loadOp.Entities
                               group id by new { id.ItemID, id.ItemName } into itemGroup
                               select new GraphItem() { ItemID = itemGroup.Key.ItemID, ItemName = itemGroup.Key.ItemName, Qty = itemGroup.Count() });
            //dgItems.ItemsSource = itemDetails;
            ColumnSeries clsSeries;
            foreach (GraphItem gi in itemDetails)
            {
                clsSeries = new ColumnSeries();
                clsSeries.ItemsSource = new List<GraphItem>() { gi };
                clsSeries.IndependentValuePath = "ItemName";
                clsSeries.DependentValuePath = "Qty";
                clsSeries.IsSelectionEnabled = true;
                clsSeries.Title = gi.ItemName;
                clsSeries.AnimationSequence = AnimationSequence.LastToFirst;
                clsSeries.SelectionChanged += new SelectionChangedEventHandler(clsSeries_SelectionChanged);
                chartItem2.Series.Add(clsSeries);
            }
        }

        private void BindPieChart()
        {
            var itemDetails = (from id in loadOp.Entities
                               group id by new { id.ItemID, id.ItemName } into itemGroup
                               select new GraphItem() { ItemID = itemGroup.Key.ItemID, ItemName = itemGroup.Key.ItemName, Qty = itemGroup.Count() });
            //dgItems.ItemsSource = itemDetails;
            PieSeries clsSeries = new PieSeries();
            clsSeries.ItemsSource = itemDetails;
            clsSeries.IndependentValuePath = "ItemName";
            clsSeries.DependentValuePath = "Qty";
            clsSeries.IsSelectionEnabled = true;
            
            clsSeries.AnimationSequence = AnimationSequence.LastToFirst;
            clsSeries.SelectionChanged += new SelectionChangedEventHandler(clsSeries_SelectionChanged);
            chartItem.Series.Add(clsSeries);
        }

        private void BindDetailChart()
        {
            var itemIds = (from id in loadOp.Entities
                           group id by id.ItemID into itemGroup
                           select itemGroup.Key).ToList();
            int itemCount = itemIds.Count;
            int[] sizeCount = new int[7] { 0, 0, 0, 0, 0, 0,0 };
            double[] ratio = new double[6]{1,3.0/4, 2.0/3,1.0/2,1.0/3,1.0/4};
            bool flag = false;
            for (int i = 0; i < itemCount; i++)
            {
                flag = false;
                var itemDetails = (from id in loadOp.Entities
                                   where id.ItemID == itemIds[i]
                                   select id).ToList();
                var defaultItem = itemDetails[0];
                foreach (ItemDetail item in itemDetails)
                {
                    for (int j = 0; j < 6; j++)
                    {
                        if ((int)Math.Round(ratio[j] * (int)item.FullSize) == (int)item.Qty)
                        {
                            sizeCount[j]++;
                            flag = true;
                            break;
                        }
                    }
                    if (flag == false)
                    {
                        sizeCount[6]++;
                    }
                }
                SizeFullList.Add(new SizeFull(defaultItem.ItemName, sizeCount[0]));
                Size3of4List.Add(new Size3of4(defaultItem.ItemName, sizeCount[1]));
                Size2of3List.Add(new Size2of3(defaultItem.ItemName, sizeCount[2]));
                Size1of2List.Add(new Size1of2(defaultItem.ItemName, sizeCount[3]));
                Size1of3List.Add(new Size1of3(defaultItem.ItemName, sizeCount[4]));
                Size1of4List.Add(new Size1of4(defaultItem.ItemName, sizeCount[5]));
                SizeOtherList.Add(new SizeOther(defaultItem.ItemName, sizeCount[6]));
                for (int k = 0; k < 7; k++)
                {
                    sizeCount[k] = 0;
                }
            }
            var series = chartItem3.Series[0] as StackedColumnSeries;
            series.SeriesDefinitions[0].ItemsSource = SizeFullList;
            series.SeriesDefinitions[1].ItemsSource = Size3of4List;
            series.SeriesDefinitions[2].ItemsSource = Size2of3List;
            series.SeriesDefinitions[3].ItemsSource = Size1of2List;
            series.SeriesDefinitions[4].ItemsSource = Size1of3List;
            series.SeriesDefinitions[5].ItemsSource = Size1of4List;
            series.SeriesDefinitions[6].ItemsSource = SizeOtherList;


        }

        private void clsSeries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                GraphItem item = (GraphItem)e.AddedItems[0];
                //Speak(item.ItemName);
                //MessageBox.Show(e.OriginalSource.GetType().ToString());
                idcw.LoadData(item.ItemID);
                idcw.Show();
            }
        }

        //private void Speak(string text)
        //{
        //    SpeechServiceClient client = new SpeechServiceClient("BasicHttpBinding_ISpeechService");
        //    client.SpeakCompleted += (o, ea) =>
        //        {
        //            WavMediaStreamSource audioStream = new WavMediaStreamSource(new MemoryStream(ea.Result));
        //            audioPlayer.SetSource(audioStream);
        //        };
        //    client.SpeakAsync(text);
        //}

        private void idcw_Closed(object sender, EventArgs e)
        {
            if (idcw.DialogResult == true)
            {
                if (chartItem.Visibility == System.Windows.Visibility.Visible)
                {
                    ((PieSeries)chartItem.Series[0]).SelectedItem = null;
                }
                else
                {
                    foreach (ColumnSeries cs in chartItem2.Series)
                    {
                        cs.SelectedItem = null;
                    }
                }
            }
        }

        private void rbPie_Checked(object sender, RoutedEventArgs e)
        {
            chartItem.Visibility = System.Windows.Visibility.Visible;
            chartItem2.Visibility = System.Windows.Visibility.Collapsed;
            chartItem3.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void rbBar_Checked(object sender, RoutedEventArgs e)
        {
            chartItem2.Visibility = System.Windows.Visibility.Visible;
            chartItem.Visibility = System.Windows.Visibility.Collapsed;
            chartItem3.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void rbDetail_Checked(object sender, RoutedEventArgs e)
        {
            chartItem3.Visibility = System.Windows.Visibility.Visible;
            chartItem2.Visibility = System.Windows.Visibility.Collapsed;
            chartItem.Visibility = System.Windows.Visibility.Collapsed;
        }
    }
}
