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

namespace Day07Sandwich
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

        private void ButtonMakeSandwich_Click(object sender, RoutedEventArgs e)
        {
            string str1 = string.Empty;
            string str2 = string.Empty;
            string str3 = string.Empty;

            CustomDialog SandwichDialog = new CustomDialog();
            SandwichDialog.Owner = this;

            SandwichDialog.AssignResult += (s1, s2, s3) => { str1 = s1; str2 = s2; str3 = s3; };  // lambda expreasion to assign the value get from the dialog
            bool? result = SandwichDialog.ShowDialog();

            if (result == true)
            {
                lblBread.Content += str1;
                lblVeggie.Content += str2;
                lblMeet.Content += str3;
            }
        }

        //Another way to implement instead of the lambda expression
        //SandwichDialog.AssignResult += CustDlg_AssignResult;   //in this way to assign value, instead of check if return result from dialog is true
        public void CustDlg_AssignResult(string str1, string str2, string str3)
        {
            lblBread.Content += str1;
            lblVeggie.Content += str2;
            lblMeet.Content += str3;
        }
    }
}
