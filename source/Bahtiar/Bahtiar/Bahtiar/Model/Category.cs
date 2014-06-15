using System.IO;
using System.Xml;
using Bahtiar.Helper;
using Bahtiar.ViewModel;

namespace Bahtiar.Model
{
    public class Category : NamedItemBase
    {
        private const string XmlId = "id";
        private const string XmlName = "name";

        public BrandGroup Brands { get; private set; }

        private Brand _currentBrand;
        public Brand CurrentBrand
        {
            get { return _currentBrand; }
            set
            {
                if (_currentBrand == value)
                    return;
                _currentBrand = value;
                if (!_currentBrand.IsProductsLoaded)
                    _currentBrand.LoadProducts();
                OnPropertyChanged();
            }
        }

        public Category(XmlNode node)
        {
            Id = int.Parse(node.With(x => x.SelectSingleNode(XmlId)).With(x => x.InnerText));
            Name = node.With(x => x.SelectSingleNode(XmlName)).With(x => x.InnerText);
            Brands = new BrandGroup();
        }
        public bool IsBrandsLoaded
        {
            get { return Brands.Count != 0; }
        }

        public void LoadBrands()
        {
            XmlNodeList nodes = null;
            var worker = new Worker(
                (sender, args) =>
                {
                    var data = BahtiarViewModel.GetData(string.Format(Constants.UriGetBrands, Id));
                    if (string.IsNullOrEmpty(data))
                        return;
                    var xml = XmlReader.Create(new StringReader(data));
                    var doc = new XmlDocument();
                    doc.Load(xml);
                    nodes = doc.SelectNodes("brands/brand");
                }, (sender, args) =>
                {
                    if (nodes == null) return;
                    Brands.Clear();
                    foreach (XmlNode node in nodes)
                        Brands.Add(new Brand(node, this));
                });
            worker.RunWorkerAsync();

            //var data = BahtiarViewModel.GetData(string.Format(Constants.UriGetBrands, Id));
            //if (string.IsNullOrEmpty(data))
            //    return;
            //var xml = XmlReader.Create(new StringReader(data));
            //var doc = new XmlDocument();
            //doc.Load(xml);
            //var nodes = doc.SelectNodes("brands/brand");
            //if (nodes == null) return;
            //foreach (XmlNode node in nodes)
            //    Brands.Add(new Brand(node, this));
        }
    }
}
