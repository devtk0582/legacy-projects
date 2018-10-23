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
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace MyAlbum
{
    public partial class MainPage : UserControl
    {
        List<Uri> natureUri;
        List<Uri> cartoonUri;
        List<Uri> beachUri;

        List<Uri> vcartoonUri;
        List<Uri> vpetsUri;
        List<Uri> vsportsUri;

        List<Uri> vthumbcartoonUri;
        List<Uri> vthumbpetsUri;
        List<Uri> vthumbsportsUri;

        List<Uri> currentPicCategory;

        List<Uri> currentVidCategory;
        List<Uri> currentVidThumbCategory;

        int playIndex;
        string showMode;
        DispatcherTimer timer = new DispatcherTimer();

        public MainPage()
        {
            InitializeComponent();
            currentPicCategory = new List<Uri>();
            currentVidCategory = new List<Uri>();
            currentVidThumbCategory = new List<Uri>();
            natureUri = new List<Uri>();
            natureUri.Add(new Uri("Nature/Chrysanthemum.jpg", UriKind.Relative));
            natureUri.Add(new Uri("Nature/Desert.jpg", UriKind.Relative));
            natureUri.Add(new Uri("Nature/Hydrangeas.jpg", UriKind.Relative));
            natureUri.Add(new Uri("Nature/Jellyfish.jpg", UriKind.Relative));
            natureUri.Add(new Uri("Nature/Tulips.jpg", UriKind.Relative));

            cartoonUri = new List<Uri>();
            cartoonUri.Add(new Uri("Cartoon/Koala.jpg", UriKind.Relative));
            cartoonUri.Add(new Uri("Cartoon/Penguins.jpg", UriKind.Relative));

            beachUri = new List<Uri>();
            beachUri.Add(new Uri("Beach/Lighthouse.jpg", UriKind.Relative));

            vcartoonUri = new List<Uri>();
            vcartoonUri.Add(new Uri("vCartoon/01.wmv", UriKind.Relative));

            vpetsUri = new List<Uri>();
            vpetsUri.Add(new Uri("vCartoon/01.wmv", UriKind.Relative));

            vsportsUri = new List<Uri>();
            vsportsUri.Add(new Uri("vCartoon/01.wmv", UriKind.Relative));

            vthumbcartoonUri = new List<Uri>();
            vthumbcartoonUri.Add(new Uri("vCartoon/01.jpg", UriKind.Relative));

             vthumbpetsUri = new List<Uri>();
            vthumbpetsUri.Add(new Uri("vCartoon/01.jpg", UriKind.Relative));

             vthumbsportsUri = new List<Uri>();
            vthumbsportsUri.Add(new Uri("vCartoon/01.jpg", UriKind.Relative));

            showMode = "picture";
            currentPicCategory = natureUri;

            playIndex = 0;
            showPicture();
        }

        private void categories_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Grow.Stop();
            TextBlock txtRef = (TextBlock)sender;
            Grow.SetValue(Storyboard.TargetNameProperty, txtRef.Name);
            Grow.Begin();
        }

        private void categories_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Shrink.Stop();
            TextBlock txtRef = (TextBlock)sender;
            Shrink.SetValue(Storyboard.TargetNameProperty, txtRef.Name);
            Shrink.Begin();
        }

        private void picCategories_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            TextBlock catRef = (TextBlock)sender;
            switch (catRef.Name.ToUpper())
            {
                case "CATNATURE":
                    currentPicCategory = natureUri;
                    currentView.Text = "Nature";
                    break;
                case "CATBEACH":
                    currentPicCategory = beachUri;
                    currentView.Text = "Beach";
                    break;
                case "CATCARTOON":
                    currentPicCategory = cartoonUri;
                    currentView.Text = "Cartoon";
                    break;
            }

            showMode = "picture";
            cPlay.Visibility = Visibility.Visible;
            cStop.Visibility = Visibility.Collapsed;

            vidStage.Visibility = Visibility.Collapsed;
            imgStage.Visibility = Visibility.Visible;

            playIndex = 0;

            showPicture();
        }

        private void vidCategories_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            TextBlock catRef = (TextBlock)sender;
            switch (catRef.Name.ToUpper())
            {
                case "VCATSPORTS":
                    currentVidCategory = vsportsUri;
                    currentVidThumbCategory = vthumbsportsUri;
                    currentView.Text = "Sports";
                    break;
                case "VCATPETS":
                    currentVidCategory = vpetsUri;
                    currentVidThumbCategory = vthumbpetsUri;
                    currentView.Text = "Pets";
                    break;
                case "VCATCARTOON":
                    currentVidCategory = vcartoonUri;
                    currentVidThumbCategory = vthumbcartoonUri;
                    currentView.Text = "Cartoon";
                    break;
            }

            showMode = "video";
            cPlay.Visibility = Visibility.Collapsed;
            cStop.Visibility = Visibility.Visible;

            vidStage.Visibility = Visibility.Visible;
            imgStage.Visibility = Visibility.Collapsed;

            playIndex = 0;

            playVideo();
        }

        private void showPicture()
        {
            FadeThumb.Stop();
            //FadeIn.Stop();
            imgStage.SetValue(Image.SourceProperty, new BitmapImage(currentPicCategory[playIndex]));

            createThumbnails();

            FadeThumb.SetValue(Storyboard.TargetNameProperty, "thumb" + playIndex);
            FadeThumb.Begin();
            FadeIn.Begin();
        }

        private void playVideo()
        {
            FadeThumb.Stop();
            vidStage.SetValue(MediaElement.SourceProperty, currentVidCategory[playIndex]);
            vidStage.AutoPlay = true;

            createThumbnails();

            FadeThumb.SetValue(Storyboard.TargetNameProperty, "thumb" + playIndex);
            FadeThumb.Begin();
            FadeIn.Begin();
        }

        private void createThumbnails()
        {
            spPictures.Children.Clear();
            int idx = 0;

            if (showMode.ToUpper() == "PICTURE")
            {
                foreach (Uri item in currentPicCategory)
                {
                    Image thumbnails = new Image();
                    thumbnails.Margin = new Thickness(0, 0, 10, 0);
                    thumbnails.SetValue(NameProperty, "thumb" + idx);
                    thumbnails.Source = new BitmapImage(item);
                    thumbnails.Cursor = Cursors.Hand;
                    thumbnails.Opacity = 0.4;
                    spPictures.Children.Add(thumbnails);
                    thumbnails.MouseLeftButtonDown += new MouseButtonEventHandler(thumbnails_MouseLeftButtonDown);
                    thumbnails.MouseEnter += new MouseEventHandler(thumbnails_MouseEnter);
                    idx++;
                }
            }
            else
            {
                foreach (Uri item in currentVidThumbCategory)
                {
                    Image thumbnails = new Image();
                    thumbnails.Margin = new Thickness(0, 0, 10, 0);
                    thumbnails.SetValue(NameProperty, "thumb" + idx);
                    thumbnails.Source = new BitmapImage(item);
                    thumbnails.Cursor = Cursors.Hand;
                    thumbnails.Opacity = 0.4;
                    spPictures.Children.Add(thumbnails);
                    thumbnails.MouseLeftButtonDown += new MouseButtonEventHandler(thumbnails_MouseLeftButtonDown);
                    thumbnails.MouseEnter += new MouseEventHandler(thumbnails_MouseEnter);
                    idx++;
                }
            }
        }

        public void thumbnails_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            BitmapImage bmi = (BitmapImage)img.Source;
            if (showMode == "picture")
                imgStage.Source = new BitmapImage(bmi.UriSource);
            else
                vidStage.Source = new Uri(bmi.UriSource.ToString().Replace(".jpg", ".wmv"), UriKind.Relative);
            FadeIn.Begin();
        }

        public void thumbnails_MouseEnter(object sender, MouseEventArgs e)
        {
            FadeThumb.Stop();
            Image imgRef = (Image)sender;
            FadeThumb.SetValue(Storyboard.TargetNameProperty, imgRef.Name);
            FadeThumb.Begin();
        }

        private void goFullScreen_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Host.Content.IsFullScreen = !Application.Current.Host.Content.IsFullScreen;
            if (goFullScreen.Text.ToUpper() == "FULL SCREEN")
                goFullScreen.Text = "Normal Screen";
            else
                goFullScreen.Text = "Full Screen";
        }

        private void previous_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (showMode == "picture")
            {
                if (playIndex == 0)
                    playIndex = currentPicCategory.Count - 1;
                else
                    playIndex--;
                showPicture();
            }
            else
            {
                if (playIndex == 0)
                    playIndex = currentVidCategory.Count - 1;
                else
                    playIndex--;
                playVideo();
            }
                
        }

        private void next_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (showMode == "picture")
            {
                if (playIndex == currentPicCategory.Count - 1)
                    playIndex = 0;
                else
                    playIndex++;
                showPicture();
            }
            else
            {
                if (playIndex == currentVidCategory.Count - 1)
                    playIndex = 0;
                else
                    playIndex++;
                playVideo();
            }
        }

        private void playStopToggle(object sender, MouseButtonEventArgs e)
        {
            if (cPlay.Visibility == Visibility.Visible)
            {
                cPlay.Visibility = Visibility.Collapsed;
                cStop.Visibility = Visibility.Visible;

                if (showMode == "picture")
                {
                    timer.Interval = new TimeSpan(0, 0, 4);
                    timer.Start();
                    timer.Tick += new EventHandler(timer_Tick);
                }
                else
                {
                    vidStage.Play();
                }
            }
            else
            {
                cPlay.Visibility = Visibility.Visible;
                cStop.Visibility = Visibility.Collapsed;

                if (showMode == "picture")
                {
                    timer.Stop();
                }
                else
                {
                    vidStage.Pause();
                }
            }
        }

        void timer_Tick(object sender, EventArgs e)
        {
            if (playIndex == currentPicCategory.Count - 1)
                playIndex = 0;
            else
                playIndex++;
            showPicture();
        }

    }
}
