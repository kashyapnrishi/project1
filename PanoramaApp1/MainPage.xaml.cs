using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows.Input;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.IO.IsolatedStorage;

namespace PanoramaApp1
{
    public partial class MainPage : PhoneApplicationPage
    {

        PanoramaItem[] pis;
        WebBrowser[] browsers;

    
        public void setupBrowsers()
        {
            ViewModels.TheModel TM = ViewModels.TheModel.GetModel();
            pis = new PanoramaItem[TM.selectedCount];
            browsers = new WebBrowser[TM.selectedCount];
            DataTemplate dt = (DataTemplate)App.Current.Resources["SmallPanoramaItemTitle"];

            for (int i = 0, j = 0; i < TM.linkCount; i++)
            {
                if (!TM.linksSelected[i])
                    continue;
                int mySelectedIndex = j;
                int myIndex = i;
                j++;

                pis[mySelectedIndex] = new PanoramaItem();
                pis[mySelectedIndex].Header = "";
                pis[mySelectedIndex].HeaderTemplate = dt;
                browsers[mySelectedIndex] = new WebBrowser();
                browsers[mySelectedIndex].IsScriptEnabled = true;
                browsers[mySelectedIndex].Source = new Uri(TM.links[i], UriKind.Absolute);
                browsers[mySelectedIndex].IsEnabled = true;
                

                browsers[mySelectedIndex].Margin = new Thickness(0, -25, 0, 0);

                browsers[mySelectedIndex].Navigated += new EventHandler<NavigationEventArgs>((object sender, NavigationEventArgs e) =>
                {
                    WebBrowser b = (WebBrowser)sender;
                    
                   // TM.links[myIndex] = b.Source.ToString();
                });

                pis[mySelectedIndex].Content = browsers[mySelectedIndex];
                MainPanorama.Items.Add(pis[mySelectedIndex]);

                pis[mySelectedIndex].GotFocus += new RoutedEventHandler((object o, RoutedEventArgs a) =>
                {

                    TM.defaultPanoramaItem = mySelectedIndex;
                });
            }
        }
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            ViewModels.TheModel TM = ViewModels.TheModel.GetModel();

            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;

            
           // setupMenuItems();
            setupBrowsers();
            MainPanorama.DefaultItem = MainPanorama.Items[TM.defaultPanoramaItem + 1]; ;
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            if (browsers[MainPanorama.SelectedIndex].CanGoBack)
            {
                e.Cancel = true;
                browsers[MainPanorama.SelectedIndex].GoBack();
            }
            base.OnBackKeyPress(e);
        }

        // Load data for the ViewModel Items
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
            else
            {
                ViewModels.TheModel TM = ViewModels.TheModel.GetModel();
                foreach (var pi in pis)
                {
                    MainPanorama.Items.Remove(pi);
                }
                TM.defaultPanoramaItem = 0;
                setupBrowsers();
            }


        }

        private void ApplicationBarIcon_ResetClick(object sender, EventArgs e)
        {
       //     MainPanorama.DefaultItem = MainPanorama.Items[0];
            this.NavigationService.Navigate(new Uri("/Settings.xaml", UriKind.RelativeOrAbsolute));
        }

        private void adPubCenter_ErrorOccurred_1(object sender, Microsoft.Advertising.AdErrorEventArgs e)
        {

        }

        private void adPubCenter_AdRefreshed_1(object sender, EventArgs e)
        {

        }
    }
}