using System;
using System.IO;
using System.Net;
using System.Windows;
using System.Xml;
using Bahtiar.Helper;
using Bahtiar.Model;

namespace Bahtiar.ViewModel
{
    public class BahtiarViewModel : EntityBase
    {
        public CategoryGroup Categories { get; private set; }
        private Category _currentCategory;
        public Category CurrentCategory
        {
            get { return _currentCategory; }
            set
            {
                _currentCategory = value;

                if (_currentCategory != null && !_currentCategory.IsBrandsLoaded)
                    _currentCategory.LoadBrands();
                OnPropertyChanged();
            }
        }

        private RelayCommand _refreshCommand;
        public RelayCommand RefreshCommmand
        {
            get { return _refreshCommand ?? (_refreshCommand = new RelayCommand(o => LoadCategories())); }
        }

        public bool IsNetworkEnabled
        {
            get 
            { 
                return System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
            }
        }

        public BahtiarViewModel()
        {
            Categories = new CategoryGroup();
        }

        public void LoadCategories()
        {
            XmlNodeList nodes = null;
            using (var worker = new Worker(
                (sender, args) =>
                {
                    var data = GetData(string.Format(Constants.UriGetCategories, "all"));
                    if (string.IsNullOrEmpty(data))
                        return;
                    var xml = XmlReader.Create(new StringReader(data));
                    var doc = new XmlDocument();
                    doc.Load(xml);
                    nodes = doc.SelectNodes("subsections/category");
                },
                (sender, args) =>
                {
                    if (nodes == null)
                        return;
                    Categories.Clear();
                    foreach (XmlNode node in nodes)
                        Categories.Add(new Category(node));
                }))
            {
                worker.RunWorkerAsync();
            }
        }

        public static string GetData(string uri)
        {
            string res;
            using (var wc = new WebClient())
            {
                try
                {
                    res = wc.DownloadString(uri);
                    if (res.Equals("0"))
                        return null;
                }
                catch (Exception)
                {
                    return null;
                }
            }
            return res;
        }
    }
}
