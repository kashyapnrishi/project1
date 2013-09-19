using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace PanoramaApp1
{
    public partial class MainPage : PhoneApplicationPage
    {
        const int NumberOfPanoramaItems = 3;
        PanoramaItem[] pis;
        WebBrowser[] browsers;


        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;
            
            MiniBrowser1.Navigate(new Uri("http://www.facebook.com", UriKind.Absolute));
            this.pis = new PanoramaItem[3];
            this.browsers = new WebBrowser[3];
            DataTemplate dt = (DataTemplate)App.Current.Resources["SmallPanoramaItemTitle"];

            for(int i = 0; i < NumberOfPanoramaItems; i++)
            {
                this.pis[i] = new PanoramaItem();
                this.pis[i].Header = "item " + i;
                
                this.pis[i].HeaderTemplate = dt;
                var sp = new StackPanel();
                sp.Margin = new Thickness(0, -50, 0, 0);
                this.browsers[i] = new WebBrowser();
                this.browsers[i].Height = 500;
                this.browsers[i].Width = 500;                
                this.browsers[i].Source = new Uri("http://www.google.com");
                sp.Children.Add(browsers[i]);
                this.pis[i].Content = sp;
                MainPanorama.Items.Add(pis[i]);
            }
            
        }

        // Load data for the ViewModel Items
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
        }

        private void PanoramaItem_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            MiniBrowser1.Navigate(new Uri("https://m.facebook.com", UriKind.Absolute));
            
        }

        private void MiniBrowser1_Navigated(object sender, NavigationEventArgs e)
        {
            WebBrowser b = (WebBrowser)sender;
            MiniBrowser1Url.Text = b.Source.ToString();

            MainPanorama.DefaultItem = MainPanorama.Items[1];
        }
    }
}