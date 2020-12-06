using System;
using System.Collections.Generic;
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

namespace Day04WpfTempConv
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void TempConvert()
        {
            double tempInput = 0;

            if (double.TryParse(tbInput.Text, out tempInput))
            {
                if (btInputKelvin.IsChecked == true && btOutputCelsius.IsChecked == true)
                {
                    tbOutput.Text = string.Format("{0:#.##} °C", (tempInput - 273.15));
                }
                else if (btInputKelvin.IsChecked == true && btOutputFah.IsChecked == true)
                {
                    tbOutput.Text = string.Format("{0:#.##} °F", (tempInput - 273.15) * 9 / 5 + 32);
                }
                else if (btInputFah.IsChecked == true && btOutputCelsius.IsChecked == true)
                {
                    tbOutput.Text = string.Format("{0:#.##} °C", (tempInput - 32) * 5 / 9);
                }
                else if (btInputFah.IsChecked == true && btOutputKelvin.IsChecked == true)
                {
                    tbOutput.Text = string.Format("{0:#.##} °K", (tempInput - 32) * 5 / 9 + 273.15);
                }
                else if (btInputCelsius.IsChecked == true && btOutputKelvin.IsChecked == true)
                {
                    tbOutput.Text = string.Format("{0:#.##} °K", tempInput + 273.15);
                }
                else if (btInputCelsius.IsChecked == true && btOutputFah.IsChecked == true)
                {
                    tbOutput.Text = string.Format("{0:#.##} °F", tempInput * 9 / 5 + 32);
                }
                else
                {
                    tbOutput.Text = tempInput.ToString();
                }
            }
        }

        private void rdbt_Checked(object sender, RoutedEventArgs e)
        {
            TempConvert();
        }

        private void tbInput_Changed(object sender, TextChangedEventArgs e)
        {
            TempConvert();
        }
    }
}
