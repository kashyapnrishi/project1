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
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;

            MiniBrowser1.Navigate(new Uri("http://www.facebook.com", UriKind.Absolute));
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


        }
    }
}