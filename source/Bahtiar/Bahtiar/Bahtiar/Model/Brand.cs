using System;
using System.IO;
using System.Xml;
using Bahtiar.Helper;
using Bahtiar.ViewModel;

namespace Bahtiar.Model
{
    public class Brand : NamedItemBase
    {
        private const string XmlId = "id";
        private const string XmlName = "name";

        private readonly Category _category;

        public ProductGroup Products { get; private set; }

        private Product _currentProduct;
        public Product CurrentProduct
        {
            get { return _currentProduct; }
            set
            {
                if (_currentProduct == value)
                    return;
                _currentProduct = value;
                OnPropertyChanged();
            }
        }

        public Brand(XmlNode node,Category category)
        {
            _category = category;
            Id = int.Parse(node.With(x=>x.SelectSingleNode(XmlId)).With(x=>x.InnerText));
            Name = node.With(x=>x.SelectSingleNode(XmlName)).With(x=>x.InnerText);
            Products = new ProductGroup();
        }

        public bool IsProductsLoaded
        {
            get { return Products.Count != 0; }
        }
        public void LoadProducts()
        {
            XmlNodeList nodes = null;
            using (var worker = new Worker(
                (sender, args) =>
                {
                    var data = BahtiarViewModel.GetData(string.Format(Constants.UriGetProducts, 
                        _category.Id, Id, 1, 0, 1000));
                    if (string.IsNullOrEmpty(data)) 
                        return;
                    var xml = XmlReader.Create(new StringReader(data));
                    var doc = new XmlDocument();
                    try
                    {
                        doc.Load(xml);
                        nodes = doc.SelectNodes("goods_vendors/product");
                    }
                    catch
                    { }
                }, (sender, args) =>
                {
                    if (nodes == null) return;

                    Products.Clear();
                    foreach (XmlNode node in nodes)
                        Products.Add(new Product(node));
                }))
            {
                worker.RunWorkerAsync();
            }
        }
    }
}
