using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Windows_programmering_RGB.Models
{
    public class ColorModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public int Id { get; set; }
        private int _rNumber;
        private int _gNumber;
        private int _bNumber;
        private Brush _colors;
        private string _stringCode;

        public int RNumber
        {
            get { return _rNumber; }
            set
            {
                if (_rNumber != value)
                {
                    _rNumber = value;
                    OnPropertyChanged();
                    if (_rNumber > 255) { _rNumber = 255; MessageBox.Show("Du kan inte ha högre värde än 255"); }
                    Colors = new SolidColorBrush(Color.FromRgb(Convert.ToByte(_rNumber), Convert.ToByte(_gNumber), Convert.ToByte(_bNumber)));
                    StringCode = $"{_rNumber}, {_gNumber}, {_bNumber}";
                }
            }
        }

        public int GNumber
        {
            get { return _gNumber; }
            set
            {
                _gNumber = value;
                if (_gNumber > 255) { _gNumber = 255; MessageBox.Show("Du kan inte ha högre värde än 255"); }
                OnPropertyChanged();
                Colors = new SolidColorBrush(Color.FromRgb(Convert.ToByte(_rNumber), Convert.ToByte(_gNumber), Convert.ToByte(_bNumber)));
                StringCode = $"{_rNumber}, {_gNumber}, {_bNumber}";

            }
        }

        public int BNumber
        {
            get { return _bNumber; }
            set
            {
                if (_bNumber != value)
                {
                    _bNumber = value;
                    OnPropertyChanged();
                    if (_bNumber > 255) { _bNumber = 255; MessageBox.Show("Du kan inte ha högre värde än 255"); }
                    Colors = new SolidColorBrush(Color.FromRgb(Convert.ToByte(_rNumber), Convert.ToByte(_gNumber), Convert.ToByte(_bNumber)));
                    StringCode = $"{_rNumber}, {_gNumber}, {_bNumber}";
                }
            }
        }

        public Brush Colors
        {
            get { return _colors; }

            set
            {
                if (_colors != value)
                {
                    _colors = value;
                    OnPropertyChanged();
                }
            }
        }

        public string StringCode
        {
            get { return _stringCode; }
            set
            {
                _stringCode = value;
                OnPropertyChanged();
            }
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}