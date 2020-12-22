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

namespace Quiz4PersonEF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            try
            {
                InitializeComponent();
                RefreshList();
            }
            catch (SystemException ex)
            {
                MessageBox.Show("Fatal error connecting to database:\n" + ex.Message, "Error Information");
                Environment.Exit(1);
            }
        }

        private void RefreshList()
        {
            using (var ctx = new PersonDbContext())
            {
                lstViewPerson.ItemsSource = ctx.People.Include("Passport").ToList<Person>();
            }
            lstViewPerson.Items.Refresh();
        }

        private void NewPerson(object sender, ExecutedRoutedEventArgs e)
        {
            AddEditPersonDialog personDialog = new AddEditPersonDialog();
            personDialog.Owner = this;
            personDialog.ShowDialog();
            RefreshList();
        }

        private void lstViewPerson_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Person currentPerson = (Person)lstViewPerson.SelectedItem;
            AddEditPassportDialog passportDialog = new AddEditPassportDialog(currentPerson);
            passportDialog.Owner = this;
            passportDialog.ShowDialog();
            RefreshList();
        }
    }
}
