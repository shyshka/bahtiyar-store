using System.Xml;
using Bahtiar.Helper;

namespace Bahtiar.Model
{
    public class Product : NamedItemBase
    {
        private const string XmlGoodId = "good_id";
        private const string XmlName = "name";
        private const string XmlWeight = "weight";
        private const string XmlPrice = "price";
        private const string XmlPriceOld = "price_old";
        private const string XmlGift = "gift";
        private const string XmlPeople = "people";
        private const string XmlCurrencyId = "currency_id";
        private const string XmlDescription = "descr";
        private const string XmlStatus = "status";
        private const string XmlDelivery = "delivery";
        private const string XmlVendorArticul = "vendor_articulus";
        private const string XmlCityId = "city_id";
        private const string XmlGoodUrl = "good_url";
        private const string XmlInAgreement = "in_agreement";
        private const string XmlArenda = "arenda";

        public Product(XmlNode node)
        {
            double tmpValDec;
            int tmpValInt;

            Id = int.TryParse(node.With(x => x.SelectSingleNode(XmlGoodId)).With(x => x.InnerText), out tmpValInt)
                ? tmpValInt
                : 0;
            Name = node.With(x => x.SelectSingleNode(XmlName)).With(x => x.InnerText);
            Weight = int.TryParse(node.With(x => x.SelectSingleNode(XmlWeight)).With(x => x.InnerText), out tmpValInt)
                ? tmpValInt
                : 0;
            Price = double.TryParse(node.With(x => x.SelectSingleNode(XmlPrice)).With(x => x.InnerText), out tmpValDec)
                ? tmpValDec
                : 0.0;
            PriceOld = double.TryParse(node.With(x => x.SelectSingleNode(XmlPriceOld)).With(x => x.InnerText),
                out tmpValDec)
                ? tmpValDec
                : 0.0;
            Gift = double.TryParse(node.With(x => x.SelectSingleNode(XmlGift)).With(x => x.InnerText), out tmpValDec)
                ? tmpValDec
                : 0.0;
            People = int.TryParse(node.With(x => x.SelectSingleNode(XmlPeople)).With(x => x.InnerText), out tmpValInt)
                ? tmpValInt
                : 0;
            CurrencyId = int.TryParse(node.With(x => x.SelectSingleNode(XmlCurrencyId)).With(x => x.InnerText),
                out tmpValInt)
                ? tmpValInt
                : 0;
            Description = node.With(x => x.SelectSingleNode(XmlDescription)).With(x => x.InnerText);
            Status = node.With(x => x.SelectSingleNode(XmlStatus)).With(x => x.InnerText);
            Delivery = double.TryParse(node.With(x => x.SelectSingleNode(XmlDelivery)).With(x => x.InnerText),
                out tmpValDec)
                ? tmpValDec
                : 0.0;
            VendorArticul = node.With(x => x.SelectSingleNode(XmlVendorArticul)).With(x => x.InnerText);
            CityId = int.TryParse(node.With(x => x.SelectSingleNode(XmlCityId)).With(x => x.InnerText), out tmpValInt)
                ? tmpValInt
                : 0;
            GoodUlr = node.With(x => x.SelectSingleNode(XmlGoodUrl)).With(x => x.InnerText);
            InAgrrement = int.TryParse(node.With(x => x.SelectSingleNode(XmlInAgreement)).With(x => x.InnerText),
                out tmpValInt)
                ? tmpValInt
                : 0;
            Arenda = int.TryParse(node.With(x => x.SelectSingleNode(XmlArenda)).With(x => x.InnerText), out tmpValInt)
                ? tmpValInt
                : 0;
        }

        private int _weight;
        public int Weight
        {
            get { return _weight; }
            set
            {
                if (_weight == value)
                    return;
                _weight = value;
                OnPropertyChanged();
            }
        }

        private double _price;
        public double Price
        {
            get { return _price; }
            set
            {
                if (_price == value)
                    return;
                _price = value;
                OnPropertyChanged();
            }
        }

        private double _priceOld;
        public double PriceOld
        {
            get { return _priceOld; }
            set
            {
                if (_priceOld == value)
                    return;
                _priceOld = value;
                OnPropertyChanged();
            }
        }

        private double _gift;
        public double Gift
        {
            get { return _gift; }
            set
            {
                if (_gift == value)
                    return;
                _gift = value;
                OnPropertyChanged();
            }
        }

        private int _people;
        public int People
        {
            get { return _people; }
            set
            {
                if (_people == value)
                    return;
                _people = value;
                OnPropertyChanged();
            }
        }

        private int _currencyId;
        public int CurrencyId
        {
            get { return _currencyId; }
            set
            {
                if (_currencyId == value)
                    return;
                _currencyId = value;
                OnPropertyChanged();
            }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                if (_description == value)
                    return;
                _description = value;
                OnPropertyChanged();
            }
        }

        private string _status;
        public string Status
        {
            get { return _status; }
            set
            {
                if (_status == value)
                    return;
                _status = value;
                OnPropertyChanged();
            }
        }

        private double _delivery;
        public double Delivery
        {
            get { return _delivery; }
            set
            {
                if (_delivery == value)
                    return;
                _delivery = value;
                OnPropertyChanged();
            }
        }

        private string _vendorArticul;
        public string VendorArticul
        {
            get { return _vendorArticul; }
            set
            {
                if (_vendorArticul == value)
                    return;
                _vendorArticul = value;
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

        private string _goodUlr;
        public string GoodUlr
        {
            get { return _goodUlr; }
            set
            {
                if (_goodUlr == value)
                    return;
                _goodUlr = value;
                OnPropertyChanged();
            }
        }

        private int _inAgrrement;
        public int InAgrrement
        {
            get { return _inAgrrement; }
            set
            {
                if (_inAgrrement == value)
                    return;
                _inAgrrement = value;
                OnPropertyChanged();
            }
        }

        private int _arenda;
        public int Arenda
        {
            get { return _arenda; }
            set
            {
                if (_arenda == value)
                    return;
                _arenda = value;
                OnPropertyChanged();
            }
        }
    }
}
