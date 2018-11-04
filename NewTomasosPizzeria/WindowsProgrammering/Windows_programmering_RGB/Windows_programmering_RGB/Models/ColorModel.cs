using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
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
                    Colors = new SolidColorBrush(Color.FromArgb(255, Convert.ToByte(_rNumber), Convert.ToByte(_gNumber), Convert.ToByte(_bNumber)));
                }
            }
        }

        public int GNumber
        {
            get { return _gNumber; }
            set
            {
                if (_gNumber != value)
                {
                    _gNumber = value;
                    OnPropertyChanged();
                    Colors = new SolidColorBrush(Color.FromArgb(255, Convert.ToByte(_rNumber), Convert.ToByte(_gNumber), Convert.ToByte(_bNumber)));
                }
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
                    Colors = new SolidColorBrush(Color.FromArgb(255, Convert.ToByte(_rNumber), Convert.ToByte(_gNumber), Convert.ToByte(_bNumber)));
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


        //private bool _counting;
        //public bool Counting
        //{
        //    get { return _counting; }
        //    set
        //    {
        //        if (_counting != value)
        //        {
        //            _counting = DisableIfEmptyList();
        //            OnPropertyChanged();
        //        }

        //    }
        //}

        //private int _selectedIndex;
        //public int SelectedIndex
        //{
        //    get { return _selectedIndex; }
        //    set
        //    {
        //        if (_selectedIndex != value)
        //        {
        //            _selectedIndex = value;
        //            OnPropertyChanged();

        //            var myStr = ListBox.Items[_selectedIndex].ToString();
        //            int[] newValue = new int[3];
        //            string[] rgbValues = myStr.Split(',');

        //            int i = 0;
        //            foreach (var values in rgbValues)
        //            {
        //                var onlyNumbers = Regex.Replace(values, "[^0-9.+-]", "").Replace(".", "");

        //                newValue[i] = Convert.ToInt32(onlyNumbers);
        //                i++;
        //            }

        //            Colors = new SolidColorBrush(Color.FromArgb(255, Convert.ToByte(newValue[0]), Convert.ToByte(newValue[1]), Convert.ToByte(newValue[2])));
        //            RNumber = newValue[0];
        //            GNumber = newValue[1];
        //            BNumber = newValue[2];
        //        }
        //    }
        //}

    }
}
