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

namespace Day04ScoopSelector
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            listInitial();

        }

        static List<string> flavourList = new List<string>();
        static List<string> selectList = new List<string>();
        static int flavourIndex;
        static int selectIndex;

        public void listInitial()
        {
            flavourList.Add("Vanilla");
            flavourList.Add("Chocolate");
            flavourList.Add("Strawberry");
            flavourList.Add("Peach");
            flavourList.Add("Berry");
            flavourList.Add("Sweet");
            flavourList.Add("Candy");
            flavourList.Add("Tasty");
            flavourList.Add("Cotton");

            flavourListBinding.ItemsSource = flavourList;
            selectListBinding.ItemsSource = selectList;
        }

        private void btClear_click(object sender, RoutedEventArgs e)
        {
            selectList.Clear();
            flavourList.Clear();
            listInitial();
            flavourListBinding.Items.Refresh();
            selectListBinding.Items.Refresh();
        }

        private void btDelete_Click(object sender, RoutedEventArgs e)
        {
            if(selectListBinding.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please choose one from Selected to delete");
            }
            else
            {
                //single
                //selectList.RemoveAt(selectIndex);
                //multiple
                var selectedToDelete = selectListBinding.SelectedItems;
                foreach (string s in selectedToDelete)
                {
                    selectList.Remove(s);
                }
                selectListBinding.Items.Refresh();
            }
        }

        private void btAdd_Click(object sender, RoutedEventArgs e)
        {
            if (flavourListBinding.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please choose one from flavour to add");
            }
            else
            {
                //single record
                //selectList.Add(flavourList.ElementAt(flavourIndex));
                //flavourList.RemoveAt(flavourIndex);
                //multiple records
                var selectedFlavour = flavourListBinding.SelectedItems;
                foreach (string s in selectedFlavour)
                {
                    selectList.Add(s);
                    flavourList.Remove(s);
                }
                flavourListBinding.Items.Refresh();
                selectListBinding.Items.Refresh();
            }
        }

        private void flavour_Changed(object sender, SelectionChangedEventArgs e)
        {
            flavourIndex = flavourListBinding.SelectedIndex;
        }

        private void selected_Changed(object sender, SelectionChangedEventArgs e)
        {
            selectIndex = selectListBinding.SelectedIndex;
        }
    }
}
