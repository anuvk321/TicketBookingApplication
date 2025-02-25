using System;
using System.ComponentModel;

namespace BookingSampleApplication
{
    public class ViewModelBase:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        //protected void RaisePropertyChanged(string prop)
        //{
        //    if (PropertyChanged != null)
        //    {
        //        PropertyChanged(this, new PropertyChangedEventArgs(prop));
        //    }
        //}
        //public event PropertyChangedEventHandler PropertyChanged;
        //private void RaisePropertyChanged(String info)
        //{
        //    if (PropertyChanged != null)
        //        PropertyChanged(this, new PropertyChangedEventArgs(info));
        //}
    }
}
