using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Windows_programmering_RGB.Models
{
    public class ViewModels : INotifyPropertyChanged
    {
        //private string _newColor;
        private ColorModel _code;

        public ViewModels()
        {
            _code = new ColorModel { RNumber = 0, GNumber = 0, BNumber = 0 };
            _code.PropertyChanged += (sender, args) => OnPropertyChanged();
        }

        public ColorModel Code
        {
            get { return _code; }
            set
            {
                _code = value;
                OnPropertyChanged();
            }
        }

        //public string NewColor
        //{
        //    get { return _newColor; }
        //    set
        //    {
        //        _newColor = value;
        //        OnPropertyChanged("NewColor");
        //    }
        //}

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
