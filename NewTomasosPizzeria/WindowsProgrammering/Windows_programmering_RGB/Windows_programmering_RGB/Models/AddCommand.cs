using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Windows_programmering_RGB
{
    public class AddCommand : ICommand
    {
        private ViewModels viewModel;

        public AddCommand(ViewModels vm)
        {
            viewModel = vm;
            viewModel.PropertyChanged += VmOnPropertyChanged;
        }

        private void VmOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName == "NewColor")
            {
                CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public bool CanExecute(object parameter)
        {
            return !String.IsNullOrEmpty(viewModel.NewColor);
        }

        public void Execute(object parameter)
        {
            viewModel.Items.Add(viewModel.NewColor);
            viewModel.NewColor = String.Empty;
        }

        public event EventHandler CanExecuteChanged;
    }
}
