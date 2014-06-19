using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using System.Xml;
using System.Xml.Serialization;
using Bahtiar.Helper;
using Bahtiar.Model;

namespace Bahtiar.ViewModel
{
    public class BahtiarViewModel : EntityBase
    {
        private DirectoryInfo _dataDirectory;
        private string _tempName = "data.temp";
        private Seller _seller;

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
        public RelayCommand RefreshCommand
        {
            get
            {
                return _refreshCommand ?? (_refreshCommand = new RelayCommand(o =>
                {
                    LoadCategories();
                    LoadSeller();
                }));
            }
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
            _dataDirectory = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data"));
            if(!_dataDirectory.Exists)
                _dataDirectory.Create();

            Categories = new CategoryGroup();
        }

        public void LoadCategories()
        {
            if (!IsNetworkEnabled)
            {
                LoadLocal();
            }


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

        public void LoadSeller()
        {
            XmlNode node = null;
            using (var worker = new Worker(
                (sender, args) =>
                {
                    var data = GetData(string.Format(Constants.UriGetSelletInfoByHash, Constants.TmpHash));
                    if (string.IsNullOrEmpty(data))
                        return;
                    var xml = XmlReader.Create(new StringReader(data));
                    var doc = new XmlDocument();
                    doc.Load(xml);
                    node = doc.SelectSingleNode("vendors");
                },
                (sender, args) =>
                {
                    if (node == null)
                        return;
                    _seller = new Seller(node);
                    _seller.LoadConnectedCategories();

                    _seller.UnLockCategoryById(5);

                    string s =
                        string.Format("Seller:\nId={0}\nPrices={1}\nBalance={2}\nCity={3}", _seller.Id, _seller.PricesCnt, _seller.Balance, _seller.CityId);
                    MessageBox.Show(s);
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

        public void SaveLocal()
        {
            var formatter = new BinaryFormatter();
            var file = new FileInfo(Path.Combine(_dataDirectory.FullName, _tempName));
            using (var fs = new FileStream(file.FullName, FileMode.Create))
            {
                formatter.Serialize(fs, Categories);
            }
            
        }

        public void LoadLocal()
        {
            var formatter = new BinaryFormatter();
            var file = new FileInfo(Path.Combine(_dataDirectory.FullName, _tempName));

            if (!file.Exists)
            {
                return;
            }


            using (var fs = new FileStream(file.FullName, FileMode.Open))
            {
                var temp = formatter.Deserialize(fs) as CategoryGroup;
                if (temp == null) return;
                foreach (var t in temp)
                {
                    Categories.Add(t);
                }
            }
        }
    }
}
