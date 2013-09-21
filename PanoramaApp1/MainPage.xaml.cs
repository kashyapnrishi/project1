﻿using System;
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

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            ViewModels.TheModel TM = ViewModels.TheModel.GetModel();

            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;
            
            
            this.pis = new PanoramaItem[3];
            this.browsers = new WebBrowser[3];
            DataTemplate dt = (DataTemplate)App.Current.Resources["SmallPanoramaItemTitle"];
            
            
            for(int i = 0; i < TM.linkCount; i++)
            {
                this.pis[i] = new PanoramaItem();
                this.pis[i].Header = TM.linkNames[i];
                
                this.pis[i].HeaderTemplate = dt;
                this.browsers[i] = new WebBrowser();
                this.browsers[i].Source = new Uri(TM.links[i]);
                int myIndex = i;
                browsers[i].IsScriptEnabled = true;                
                this.browsers[i].Navigated += new EventHandler<NavigationEventArgs>((object sender, NavigationEventArgs e) => {
                    WebBrowser b = (WebBrowser)sender;                    
                    TM.links[myIndex] = b.Source.ToString();
                });
                this.pis[i].Content = browsers[i];
                MainPanorama.Items.Add(pis[i]);

                var menuItem = new TextBlock();
                menuItem.FontSize = 40;
                menuItem.Margin = new Thickness(0, 0, 0, 20);
                menuItem.Text = TM.linkNames[i];
                menuItem.Tap+=new EventHandler<System.Windows.Input.GestureEventArgs>((object sender, GestureEventArgs args) => {
                    MainPanorama.DefaultItem = MainPanorama.Items[myIndex +1];
                });
                MainMenuStackPanel.Children.Add(menuItem);


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

        //private void PanoramaItem_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        //{
        //    MiniBrowser1.Navigate(new Uri("https://m.facebook.com", UriKind.Absolute));            
        //}

        //private void MiniBrowser1_Navigated(object sender, NavigationEventArgs e)
        //{
        //    WebBrowser b = (WebBrowser)sender;
        //    MiniBrowser1Url.Text = b.Source.ToString();

        //    MainPanorama.DefaultItem = MainPanorama.Items[1];
        //}
    }
}