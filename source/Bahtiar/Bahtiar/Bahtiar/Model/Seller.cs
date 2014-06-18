using System.IO;
using System.Xml;
using Bahtiar.Helper;
using Bahtiar.ViewModel;

namespace Bahtiar.Model
{
    public class Seller:EntityBase
    {
        private const string XmlId = "id";
        private const string XmlBalance = "balance";
        private const string XmlPricesCnt = "prices_count";
        private const string XmlCityId = "v_city_id";

        public CategoryGroup CategoriesConnected { get; private set; }

        public Seller(XmlNode node)
        {
            int tmpValInt;
            double tmpValDec;

            Id = int.TryParse(node.With(x => x.SelectSingleNode(XmlId)).With(x => x.InnerText), out tmpValInt)
                ? tmpValInt
                : 0;
            Balance = double.TryParse(node.With(x => x.SelectSingleNode(XmlBalance)).With(x => x.InnerText),
                out tmpValDec)
                ? tmpValDec
                : 0;
            PricesCnt = int.TryParse(node.With(x => x.SelectSingleNode(XmlPricesCnt)).With(x => x.InnerText), out tmpValInt)
                ? tmpValInt
                : 0;
            CityId = int.TryParse(node.With(x => x.SelectSingleNode(XmlCityId)).With(x => x.InnerText), out tmpValInt)
                ? tmpValInt
                : 0;

            CategoriesConnected = new CategoryGroup();
        }

        //Загрузка подключенных категорий
        public void LoadConnectedCategories()
        {
            XmlNodeList nodes = null;
            using (var worker = new Worker(
                (sender, args) =>
                {
                    var data = BahtiarViewModel.GetData(string.Format(Constants.UriGetConnectedCategories, Id));
                    if (string.IsNullOrEmpty(data))
                        return;
                    var xml = XmlReader.Create(new StringReader(data));
                    var doc = new XmlDocument();
                    try
                    {
                        doc.Load(xml);
                        nodes = doc.SelectNodes("vendors/vendors_subsections");
                    }
                    catch
                    {
                    }
                }, (sender, args) =>
                {
                    if (nodes == null) return;
                    CategoriesConnected.Clear();
                    foreach (XmlNode node in nodes)
                        CategoriesConnected.Add(new Category(int.Parse(node.InnerText)));
                }))
            {
                worker.RunWorkerAsync();
            }

        }

        //отключение категории по ИД
        public void LockCategoryById(int id)
        {
            BahtiarViewModel.GetData(string.Format(Constants.UriLockCategories, Id, id));
        }

        //подключение категории по ИД
        public void UnLockCategoryById(int id)
        {
            BahtiarViewModel.GetData(string.Format(Constants.UriUnlockCategories, Id, id));
        }

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

        private double _balance;

        public double Balance
        {
            get { return _balance; }
            set
            {
                if (_balance == value)
                    return;
                _balance = value;
                OnPropertyChanged();
            }
        }

        private int _pricesCnt;

        public int PricesCnt
        {
            get { return _pricesCnt; }
            set
            {
                if (_pricesCnt == value)
                    return;
                _pricesCnt = value;
                OnPropertyChanged();
            }
        }

        private int _cityId;

        public int CityId
        {
            get { return _cityId; }
            set
            {
                if (_cityId == value)
                    return;
                _cityId = value;
                OnPropertyChanged();
            }
        }
    }
}
