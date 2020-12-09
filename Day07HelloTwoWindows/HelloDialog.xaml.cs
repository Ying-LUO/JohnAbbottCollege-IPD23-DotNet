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

namespace Day07HelloTwoWindows
{
    /// <summary>
    /// Interaction logic for HelloDialog.xaml
    /// </summary>
    public partial class HelloDialog : Window
    {
        // values returned to whoever instantiated this window
        public event Action<int, double> AssignResult;  // a method to take integer as parameter, similar to delegate
        // could pass multiple parameters


        // values from constructor passed to this window
        //string name;   // only if needed to save information in dialog window otherwise use set parameter way to pass data from main window

        public HelloDialog(string name)
        {
            InitializeComponent();
            lblMessage.Content = string.Format("Hello {0}, nice to meet you", name);
        }

        private void btOK_Click(object sender, RoutedEventArgs e)
        {
            int age;
            int.TryParse(tbAge.Text, out age);  //FIXME or TODO for exception handling
            double rand = new Random().NextDouble();
            AssignResult?.Invoke(age, rand);  // execute call-back, if resultAge is not null, then call it and set it to age
            DialogResult = true;  // without setting this, press ok button has no reaction, after setting this, ok button will also close the dialog, just like the cancel button, but the result return bool true
        }
    }
}
