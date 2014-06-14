using System;
using System.IO;
using System.Net;
using System.Xml;
using Bahtiar.Model;

namespace Bahtiar.ViewModel
{
    public class BahtiarViewModel : EntityBase
    {

        private const string Uri = "http://bahtiyar.savgroup.ru/engine/modules/catalog/soft/data.php?api_info=take_subsections_by_ids&ids=all";
        public CategoryGroup Categories { get; private set; }
        private Category _currentCategory;

        public Category CurrentCategory
        {
            get { return _currentCategory; }
            set
            {
                _currentCategory = value;
                OnPropertyChanged();
            }
        }

        public BahtiarViewModel()
        {
            Categories = new CategoryGroup();
        }

        public void LoadData()
        {
            var data = GetData(Uri);
            if (string.IsNullOrEmpty(data))
                return;
            var xml = XmlReader.Create(new StringReader(data));
            var doc = new XmlDocument();
            doc.Load(xml);
            var nodes = doc.SelectNodes("subsections/category");
            if (nodes == null)
                return;
            foreach (XmlNode node in nodes)
            {
                Categories.Add(new Category
                {
                    Id = int.Parse(node.SelectSingleNode("id").InnerText),
                    Name = node.SelectSingleNode("name").InnerText
                });
            }

        }

        private string GetData(string uri)
        {
            string res;
            using (var wc = new WebClient())
            {
                try
                {
                    res = wc.DownloadString(uri).Replace("&lt;", "<").Replace("&gt;", ">").Replace("&quot;", "\"");
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
