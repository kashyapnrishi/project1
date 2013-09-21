using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Input;
namespace PanoramaApp1
{
    public partial class settings : PhoneApplicationPage
    {
        public settings()
        {
            InitializeComponent();
             setupMenuItems();
        }



        public void setupMenuItems(bool calledFromWithin = false)
        {
            ViewModels.TheModel TM = ViewModels.TheModel.GetModel();
            TM.selectedCount = 0;
            for (int i = 0, j = 0; i < TM.linkCount; i++)
            {
                int selectedMyIndex = j;
                int myIndex = i;

                var menuItem = new ListBoxItem();
                var sp = new StackPanel();
                sp.Orientation = System.Windows.Controls.Orientation.Horizontal;
                var cb = new CheckBox();
                cb.Unchecked += new RoutedEventHandler((object o, RoutedEventArgs a) =>
                {
                    TM.selectedCount--;
                    TM.linksSelected[myIndex] = false;
                });

                cb.Checked += new RoutedEventHandler((object o, RoutedEventArgs a) =>
                {
                    if (TM.selectedCount >= 3)
                    {
                        ((CheckBox)o).IsChecked = false;
                    }
                    else
                    {
                        TM.linksSelected[myIndex] = true;
                    }
                    TM.selectedCount++;
                });

                sp.Children.Add(cb);
                var menuText = new TextBlock();
                menuText.Text = TM.linkNames[i];
                menuText.FontSize = 40;
                sp.Children.Add(menuText);
                menuItem.Content = sp; // 
                if (TM.linksSelected[i])
                {
                    cb.IsChecked = true;
                    menuText.Tap += new EventHandler<System.Windows.Input.GestureEventArgs>((object sender, GestureEventArgs args) =>
                    {
                        MainPanorama.DefaultItem = MainPanorama.Items[selectedMyIndex + 1];
                    });
                    j++;
                }

                MainMenuList.Children.Add(menuItem);
            }


            //if (!calledFromWithin)
            //{
            //    Button updateButton = new Button();
            //    updateButton.Content = "Click";
            //    updateButton.Click += new RoutedEventHandler((object sender, RoutedEventArgs e) =>
            //    {
            //        try
            //        {
            //            foreach (var pi in pis)
            //            {
            //                MainPanorama.Items.Remove(pi);
            //            }
            //            TM.defaultPanoramaItem = 0;
            //        }
            //        catch (Exception) { }
            //        //setupBrowsers();
            //        //NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
            //    });
            //    MainMenuList.Children.Add(updateButton);
            //}
        }

        private void ApplicationBarIcon_Done(object sender, EventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}