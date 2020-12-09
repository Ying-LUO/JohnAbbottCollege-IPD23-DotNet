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

namespace Day07HelloTwoWindows
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

        private void ButtonSayHello_Click(object sender, RoutedEventArgs e)
        {
            int ageVal = 0;
            double randVal = 0;

            string name = tbName.Text;

            HelloDialog helloDialog = new HelloDialog(name);
            helloDialog.Owner = this;   // use with the property setting in dialog: WindowStartupLocation="CenterOwner", to set who is the Owner of the dialog

            helloDialog.AssignResult += (a, r) => { ageVal = a; randVal = r; };  // lambda expression, this is not a assignment yet, this is a setup call-back

            bool? result = helloDialog.ShowDialog();  //nullable, in xaml set property isDefault to OK button, isCancel to cancel button

            if (result == true)  // ok button is clicked
            {
                lblResult.Content = string.Format("Age: {0}, Rand: {1}", ageVal, randVal);
            }

            //Console.WriteLine("result is: "+result);
            // result return false, if you choose cancel in dialog;
            // return true after set DialogResult = true under ok button in dialog code behind.
        }
    }
}
