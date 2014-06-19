using System;
using System.IO;
using System.Xml;
using Bahtiar.Helper;
using Bahtiar.ViewModel;

namespace Bahtiar.Model
{
    [Serializable]
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

        // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        // то есть ты предлагаешь, создавать отдельный поток для загрузки каждой из групп? 
        // там же был вроде АПИ для загрузки нескольких категорий?
        // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        // використовується у разі завантаження підключених категорій
        public Category(int id)
        {
            XmlNode node = null;
            using (var worker = new Worker(
                (sender, args) =>
                {
                    var data = BahtiarViewModel.GetData(string.Format(Constants.UriGetCategories, id));
                    if (string.IsNullOrEmpty(data))
                        return;
                    var xml = XmlReader.Create(new StringReader(data));
                    var doc = new XmlDocument();
                    doc.Load(xml);
                    node = doc.SelectSingleNode("subsections/category");
                },
                (sender, args) =>
                {
                    if (node == null)
                        return;
                    Id = int.Parse(node.With(x => x.SelectSingleNode(XmlId)).With(x => x.InnerText));
                    Name = node.With(x => x.SelectSingleNode(XmlName)).With(x => x.InnerText);
                    Brands = new BrandGroup();
                }))
            {
                worker.RunWorkerAsync();
            }
        }

        // конструктор для сериализации
        protected Category()
        {
            Brands = new BrandGroup();
        }

        public bool IsBrandsLoaded
        {
            get { return Brands.Count != 0; }
        }

        public void LoadBrands()
        {
            XmlNodeList nodes = null;
            using (var worker = new Worker(
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
                }))
            {
                worker.RunWorkerAsync();
            }
        }
    }
}
