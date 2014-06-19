using System;
using System.ComponentModel;
using Bahtiar.Annotations;

namespace Bahtiar.Model
{
    [Serializable]
    public abstract class EntityBase : INotifyPropertyChanged
    {
        // конструктор для сериализации
        protected EntityBase()
        {
        }

        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
