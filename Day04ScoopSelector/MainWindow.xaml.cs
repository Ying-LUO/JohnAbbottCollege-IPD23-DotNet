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
            

        }

        private void btClear_click(object sender, RoutedEventArgs e)
        {
            selectList.Clear();
            flavourList.Clear();
            listInitial();
        }

        private void btDelete_Click(object sender, RoutedEventArgs e)
        {
            if(selectListBinding.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please choose one from Selected to delete");
            }
            else
            {
                var itemsToDelete = selectListBinding.SelectedItems;
                foreach (string s in itemsToDelete)
                {
                    selectList.Remove(s);
                    selectListBinding.ItemsSource = selectList;
                }
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
                //var itemsToAdd = flavourListBinding.SelectedItems;
                //foreach (var s in itemsToAdd)
                //{
                    //selectList.Add(s.ToString());
                    selectListBinding.Items.Add(flavourListBinding.SelectedItem);
                    //flavourList.Remove(s.ToString());
                    flavourListBinding.Items.Remove(flavourListBinding.SelectedItem.ToString());
                //}
            }
        }

        private void flavourList_Changed(object sender, SelectionChangedEventArgs e)
        {
           

        }
    }
}
