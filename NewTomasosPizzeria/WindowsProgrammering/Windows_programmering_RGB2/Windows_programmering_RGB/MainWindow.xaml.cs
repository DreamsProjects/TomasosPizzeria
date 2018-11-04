using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Windows_programmering_RGB.DAL;
using Windows_programmering_RGB.Models;
using Windows_programmering_RGB.Properties;

namespace Windows_programmering_RGB
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ViewModels model = new ViewModels();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = model;
            model.Code.Colors = new SolidColorBrush(Color.FromRgb(0, 0, 0)); //Start värde
        }

        public int IsOver255(int value)
        {
            if (value > 255)
            {
                MessageBox.Show("Du kan inte skriva in mer än 255");
                return 255;
            }

            else
            {
                return value;
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e) //Gör till en ICommand
        {
            ListBox.Items.Add(new ListBoxItem { Content = model.Code.RNumber + "," + model.Code.GNumber + "," + model.Code.BNumber, Background = model.Code.Colors });
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (ListBox.SelectedIndex > -1)
            {
                ListBox.Items.RemoveAt(ListBox.SelectedIndex);
            }

            else MessageBox.Show("Du har inte markerat någon rad!");
        }

        private void LoadFromDatabase_Click(object sender, RoutedEventArgs e) //Läser från databasen
        {
            ListBox.Items.Clear();

            DataReader reader = new DataReader();

            var commandText = "select ColorCode from dbo.Colors";

            var dbValues = reader.GetColors(commandText, CommandType.Text, null);

            foreach (var values in dbValues.Distinct())
            {
                string[] rgbValues = values.StringCode.Split(',');
                var color = new SolidColorBrush(Color.FromRgb(Convert.ToByte(rgbValues[0]), Convert.ToByte(rgbValues[1]), Convert.ToByte(rgbValues[2])));
                ListBox.Items.Add(new ListBoxItem { Content = values.StringCode, Background = color });
            }
        }

        private void SaveToDatabase_Click(object sender, RoutedEventArgs e)//Sparar till databasen
        {
            DataAccess dataAccess = new DataAccess();

            for (int i = 0; i < ListBox.Items.Count; i++)
            {
                var getCurrentItem = ListBox.Items[i].ToString();

                string onlyNumbers = getCurrentItem.Substring(getCurrentItem.IndexOf(':') + 1);
                var removeSpace = onlyNumbers.Replace(" ", "");

                SqlParameter[] parameters = new SqlParameter[]
                {
                        new SqlParameter("@ColorCode", removeSpace)
                };

                var command = "INSERT INTO dbo.Colors(ColorCode)" +
                    "Values(@ColorCode)";

                var result = dataAccess.ExecuteNonQuery(command, CommandType.Text, parameters);
            }
        }
    }
}