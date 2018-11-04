using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Windows_programmering_RGB
{
    public class RemoveCommand : ICommand
    {
        private ViewModels _viewModels;
        public event EventHandler CanExecuteChanged;


        public RemoveCommand(ViewModels viewModel)
        {
            _viewModels = viewModel;
            _viewModels.PropertyChanged += VmOnPropertyChanged;
        }

        private void VmOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName == "SelectedItem")
            {
                CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public bool CanExecute(object parameter)
        {
            return _viewModels.SelectedItem != null;
        }

        public void Execute(object parameter)
        {
            _viewModels.Items.Remove(_viewModels.SelectedItem);
        }


    }
}
