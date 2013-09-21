using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.IsolatedStorage;

namespace PanoramaApp1.ViewModels
{
    class TheModel
    {
        private static IsolatedStorageSettings ISS = IsolatedStorageSettings.ApplicationSettings;
        private static TheModel theModel;

        static readonly string str_linkCount = "link_count";
        static readonly string str_defaultPanoramaItem = "defaultPanoramaItem";
        static readonly string str_linkName = "link_name";
        static readonly string str_link = "link_";
        static readonly string str_linkSelected = "link_selected_";
        
        public int linkCount;
        public int defaultPanoramaItem;
        public string[] links;
        public string[] linkNames;
        public bool[] linksSelected;
        public int selectedCount;

        private TheModel() {}
        public static TheModel GetModel() {
            if (theModel == null)
            {
                theModel = new TheModel();

                if (!theModel.load())
                {
                    theModel.initializeValuesForTheFirstTime();
                    theModel.saveData();
                }
            }
            return theModel;
        }
        void initializeValuesForTheFirstTime()
        {
            linkCount = 5;
            selectedCount = 3;
            defaultPanoramaItem = 0;

            links = new string[linkCount];
            linkNames = new string[linkCount];
            linksSelected = new bool[linkCount];

            links[0] = "https://m.facebook.com";
            linkNames[0] = "facebook";
            linksSelected[0] = true;

            links[1] = "https://twitter.com/twittermobile";
            linkNames[1] = "twitter";
            linksSelected[1] = true;

            links[2] = "https://www.pinterest.com/";
            linkNames[2] = "pinterest";
            linksSelected[2] = true;

            links[3] = "http://mobile.yahoo.com/flickr/";
            linkNames[3] = "flikr";
            linksSelected[3] = false;

            links[4] = "https://www.linkedin.com";
            linkNames[4] = "linkendin";
            linksSelected[4] = false;
        }

        void removeKey(string key)
        {
            try
            {
                ISS.Remove(key);
            }
            catch (Exception)
            { }
        }

        public void saveData()
        {
            removeKey(str_linkCount);
            removeKey(str_defaultPanoramaItem);            
            try
            {
                ISS.Add(str_linkCount, linkCount);
                ISS.Add(str_defaultPanoramaItem, defaultPanoramaItem);
                for (int i = 0; i < linkCount; i++)
                {
                    string key = str_linkName + i;
                    removeKey(key);
                    ISS.Add(key, linkNames[i]);
                    key = str_link + i;
                    removeKey(key);
                    ISS.Add(key, links[i]);
                    key = str_linkSelected + i;
                    removeKey(key);
                    ISS.Add(key, linksSelected[i]);
                }
            }catch(Exception)
            {
               
            }
        }

        bool load()
        {
            if(!ISS.TryGetValue(str_linkCount, out linkCount))
            {
                return false;
            }
            ISS.TryGetValue(str_defaultPanoramaItem, out defaultPanoramaItem);
            links = new string[linkCount];            
            linkNames = new string[linkCount];
            linksSelected = new bool[linkCount];
            selectedCount = 0;
            for (int i = 0; i < linkCount; i++)
            {
                string key = str_linkName + i;
                ISS.TryGetValue(key, out linkNames[i]);
                key = str_link + i;
                ISS.TryGetValue(key, out links[i]);
                key = str_linkSelected + i;
                ISS.TryGetValue(key, out linksSelected[i]);
                if (linksSelected[i]) selectedCount++;
            }

            return true;
        }
   
    }
}
