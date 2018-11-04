using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Windows_programmering_RGB.Models;

namespace Windows_programmering_RGB
{
    public class ViewModels : INotifyPropertyChanged
    {
        private string _newColor;
        private string _selectedItem;
        public ObservableCollection<string> Items { get; set; }
        public ICommand Adding { get; }
        public ICommand Removes { get; }
        private ColorModel _code;

        public ViewModels()
        {
            Items = new ObservableCollection<string>();
            Adding = new AddCommand(this);
            Removes = new RemoveCommand(this);

            _code = new ColorModel { RNumber = 0, GNumber = 0, BNumber = 0 };
            _code.PropertyChanged += (sender, args) => OnPropertyChanged();
        }

        public string SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged();
            }
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


        public string NewColor
        {
            get { return _newColor; }
            set
            {
                _newColor = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
