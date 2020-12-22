using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace Quiz4PersonEF
{
    /// <summary>
    /// Interaction logic for AddEditPerson.xaml
    /// </summary>
    public partial class AddEditPersonDialog : Window
    {
        public AddEditPersonDialog()
        {
            InitializeComponent();
        }

        private void btAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(tbName.Text) || string.IsNullOrEmpty(tbAge.Text))
                {
                    MessageBox.Show("Please input Name and Age");
                    return;
                }
                int age = 0;
                int.TryParse(tbAge.Text, out age);
                if (age<0 || age>150)
                {
                    MessageBox.Show("Age must be 0-150");
                    return;
                }
                Person newPerson = new Person { Name = tbName.Text, Age = age };
                using (var ctx = new PersonDbContext())
                {
                    ctx.People.Add(newPerson);
                    ctx.SaveChanges();
                    MessageBox.Show("Person Added");
                    ResetInput();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error adding Person to database:\n" + ex.Message, "Error Information");
            }
        }

        private void ResetInput()
        {
            tbName.Text = string.Empty;
            tbAge.Text = string.Empty;
        }
    }
}
