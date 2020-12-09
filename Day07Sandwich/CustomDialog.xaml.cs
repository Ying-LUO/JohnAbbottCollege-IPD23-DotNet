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
using System.Windows.Shapes;

namespace Day07Sandwich
{
    /// <summary>
    /// Interaction logic for CustomDialog.xaml
    /// </summary>
    public partial class CustomDialog : Window
    {

        public event Action<string, string, string> AssignResult;
        string[] breadList = { "Bread 1", "Bread 2", "Bread 3" };

        public CustomDialog()
        {
            InitializeComponent();
            cmbBread.ItemsSource = breadList;
        }

        private void btSave_Click(object sender, RoutedEventArgs e)
        {
            string breadStr = cmbBread.SelectedItem.ToString();
            
            List<CheckBox> checkBoxes = WpPanelVeggie.Children.OfType<CheckBox>().Where(c => c.IsChecked == true).ToList();
            string veggieStr = string.Empty;
            foreach (CheckBox c in checkBoxes)
            {
                veggieStr += c.Content + ", ";
            }

            RadioButton rbTarget = WpPanelMeet.Children.OfType<RadioButton>().Where(r => r.IsChecked == true).First();
            string meetStr = rbTarget.Content.ToString();

            AssignResult?.Invoke(breadStr, veggieStr.Substring(0, veggieStr.Length - 2), meetStr);

            DialogResult = true;

        }
    }
}
