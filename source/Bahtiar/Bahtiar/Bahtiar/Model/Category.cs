using System;
using System.ComponentModel;
using System.Windows;
using System.Xml;
using Bahtiar.Annotations;

namespace Bahtiar.Model
{
    public class Category : EntityBase
    {
        private int _id;
        public int Id
        {
            get { return _id; }
            set
            {
                if (_id != value)
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
                if (_name != value)
                    return;
                _name = value;
                OnPropertyChanged();
            }
        }
    }
}
