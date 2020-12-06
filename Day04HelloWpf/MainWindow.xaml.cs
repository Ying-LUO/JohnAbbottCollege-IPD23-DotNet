using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
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

namespace Day04WpfAllInputs
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ComboBoxViewModel();
        }

        public class ComboBoxViewModel
        {
            public List<string> Continents { get; set; }

            public ComboBoxViewModel()
            {
                Continents = new List<string>()
                {
                    "Asia",
                    "Africa",
                    "North America",
                    "South America",
                    "Antarctica"
                };
            }
        }

        private void btRegister_Click(object sender, RoutedEventArgs e)
        {
            List<RadioButton> radioButtons = Grid.Children.OfType<RadioButton>().ToList();
            RadioButton rbTarget = radioButtons.Where(r => r.IsChecked == true).Single();

            List<CheckBox> checkBoxes = Grid.Children.OfType<CheckBox>().Where(c => c.IsChecked == true).ToList();
            string checkedStr = "";
            foreach (CheckBox c in checkBoxes)
            {
                checkedStr += c.Content + ", ";
            }

            lblResult.Content += string.Format("{0}; {1}; {2}; {3}; {4}", tbName.Text, rbTarget.Content, checkedStr.Substring(0, checkedStr.Length-2), cmbContinent.SelectedItem, sldTemp.Value);
        }
    }
}
