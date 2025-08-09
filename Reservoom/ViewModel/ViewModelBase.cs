using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservoom.ViewModel
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        //This declares an event that notifies the UI when a property value changes. 
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            //Only call Invoke if PropertyChanged is not null.
            //PropertyName tells the UI which property has changed.
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public virtual void Dispose()
        {

        }

    }
}