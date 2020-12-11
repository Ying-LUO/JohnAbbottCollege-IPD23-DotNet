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

namespace Quiz3FinalFlights
{
    /// <summary>
    /// Interaction logic for AddEditFlightDialog.xaml
    /// </summary>
    public partial class AddEditFlightDialog : Window
    {
        public event Action<Flight> AddNewFlightCallback;

        public AddEditFlightDialog()
        {
            InitializeComponent();
            // DON'T UNDERSTAND WHY NOT WORK ON CURRENT DIALOG GRID LAYOUT
            cmbType.ItemsSource = Enum.GetValues(typeof(TypeEnum));
        }

        public AddEditFlightDialog(Flight flight)
        {
            InitializeComponent();
            if (flight != null)
            {
                tbId.Text = flight.Id + string.Empty;
                dpOnDay.SelectedDate = flight.OnDay;
                tbFromCode.Text = flight.FromCode;
                tbToCode.Text = flight.ToCode;
                // HANDLE THIS WAY DUE TO COMBO ITEMSOURCE DOESN'T WORK
                if (flight.Type.Equals(TypeEnum.Domestic))
                {
                    cmbType.SelectedIndex = 0;
                }
                else if(flight.Type.Equals(TypeEnum.International))
                {
                    cmbType.SelectedIndex = 1;
                }
                else
                {
                    cmbType.SelectedIndex = 2;
                }
                sldPassenger.Value = flight.Passengers;
                btSave.Content = "Update";
            }
            else
            {
                btSave.Content = "Add";
            }
        }

        private void btSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(tbFromCode.Text) || string.IsNullOrEmpty(tbToCode.Text) || dpOnDay.SelectedDate.Equals(null) || cmbType.SelectedItem == null)
                {
                    MessageBox.Show("Please input value", "Information");
                    return;
                }
                int id;
                int.TryParse(tbId.Text, out id);
                DateTime onDay = dpOnDay.SelectedDate.Value;
                string fromCodeStr = tbFromCode.Text;
                string toCodeStr = tbToCode.Text;
                TypeEnum.TryParse(cmbType.Text, out TypeEnum type);
                int passengers = (int)sldPassenger.Value;

                AddNewFlightCallback?.Invoke(new Flight(id, onDay, fromCodeStr, toCodeStr, type, passengers));

                DialogResult = true;
            }
            catch (DataInvalidException ex)
            {
                MessageBox.Show(ex.Message, "Error Information");
            }
        }

    }
}
