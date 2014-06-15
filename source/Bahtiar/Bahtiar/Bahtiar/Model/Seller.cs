using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bahtiar.Model
{
    public class Seller:EntityBase
    {

        public CategoryGroup CategoriesConnected { get; private set; }

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

        public int pricesCnt
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
