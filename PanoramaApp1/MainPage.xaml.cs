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

        //public void setupMenuItems(bool calledFromWithin=false)
        //{
        //    ViewModels.TheModel TM = ViewModels.TheModel.GetModel();
        //    TM.selectedCount = 0;
        //    for (int i = 0, j = 0; i < TM.linkCount; i++)
        //    {
        //        int selectedMyIndex = j;
        //        int myIndex = i;

        //        var menuItem = new ListBoxItem();
        //        var sp = new StackPanel();
        //        sp.Orientation = System.Windows.Controls.Orientation.Horizontal;
        //        var cb = new CheckBox();
        //        cb.Unchecked += new RoutedEventHandler((object o, RoutedEventArgs a) =>
        //        {
        //            TM.selectedCount--;
        //            TM.linksSelected[myIndex] = false;
        //        });

        //        cb.Checked += new RoutedEventHandler((object o, RoutedEventArgs a) =>
        //        {
        //            if (TM.selectedCount >= 3)
        //            {
        //                ((CheckBox)o).IsChecked = false;
        //            }
        //            else
        //            {
        //                TM.linksSelected[myIndex] = true;
        //            }
        //            TM.selectedCount++;
        //        });

        //        sp.Children.Add(cb);
        //        var menuText = new TextBlock();
        //        menuText.Text = TM.linkNames[i];
        //        menuText.FontSize = 40;
        //        sp.Children.Add(menuText);
        //        menuItem.Content = sp; // 
        //        if (TM.linksSelected[i])
        //        {
        //            cb.IsChecked = true;
        //            menuText.Tap += new EventHandler<System.Windows.Input.GestureEventArgs>((object sender, GestureEventArgs args) =>
        //            {
        //                MainPanorama.DefaultItem = MainPanorama.Items[selectedMyIndex + 1];
        //            });
        //            j++;
        //        }

        //        MainMenuList.Children.Add(menuItem);
        //    }


        //    if (!calledFromWithin)
        //    {
        //        Button updateButton = new Button();
        //        updateButton.Content = "Click";
        //        updateButton.Click += new RoutedEventHandler((object sender, RoutedEventArgs e) =>
        //        {
        //            try
        //            {
        //                foreach (var pi in pis)
        //                {
        //                    MainPanorama.Items.Remove(pi);
        //                }
        //                TM.defaultPanoramaItem = 0;
        //            }
        //            catch (Exception) { }
        //            setupBrowsers();                    
        //            //NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        //        });
        //        MainMenuList.Children.Add(updateButton);
        //    }
        //}
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
                browsers[mySelectedIndex].Source = new Uri(TM.links[i]);
                browsers[mySelectedIndex].IsScriptEnabled = true;
                browsers[mySelectedIndex].Margin = new Thickness(0, -25, 0, 0);

                browsers[mySelectedIndex].Navigated += new EventHandler<NavigationEventArgs>((object sender, NavigationEventArgs e) =>
                {
                    WebBrowser b = (WebBrowser)sender;
                    TM.links[myIndex] = b.Source.ToString();
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

        // Load data for the ViewModel Items
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
            else
            {
                foreach (var pi in pis)
                {
                    MainPanorama.Items.Remove(pi);
                }
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