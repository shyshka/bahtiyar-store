using System;
using System.IO;
using System.Xml;
using Bahtiar.ViewModel;

namespace Bahtiar.Model
{
    public class Brand : EntityBase
    {
        private const string XmlId = "id";
        private const string XmlName = "name";

        private readonly Category _category;

        private int _id;
        public int Id
        {
            get { return _id; }
            set
            {
                if (_id == value)
                    return;
                _id = value;
                OnPropertyChanged();
            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name == value)
                    return;
                _name = value;
                OnPropertyChanged();
            }
        }

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
            var data = BahtiarViewModel.GetData(string.Format(Constants.UriGetProducts, _category.Id, Id, 1, 0, 1000));
            if (string.IsNullOrEmpty(data)) 
                return;
            var xml = XmlReader.Create(new StringReader(data));
            var doc = new XmlDocument();
            try
            {
                doc.Load(xml);
                var nodes = doc.SelectNodes("goods_vendors/product");
                if (nodes == null) return;
                foreach (XmlNode node in nodes)
                    Products.Add(new Product(node));
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.Message);
            }
            
        }
    }
}
