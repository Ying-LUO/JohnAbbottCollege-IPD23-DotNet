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

namespace Day08CarsDb
{
    /// <summary>
    /// Interaction logic for AddEditDialog.xaml
    /// </summary>
    public partial class AddEditDialog : Window
    {
        public event Action<Car> AddNewCarCallback;
        private Car.FuelTypeEnum fuelType;

        public AddEditDialog(Car car)
        {
            InitializeComponent();
            cmbFuelType.ItemsSource = Enum.GetValues(typeof(Car.FuelTypeEnum));
            if (car != null)
            {
                tbId.Text = car.Id+string.Empty;
                tbModel.Text = car.MakeModel;
                sldEngineSize.Value = car.EngineSize;
                cmbFuelType.SelectedItem = car.FuelType;
                btSave.Content = "Update";
            }
            else
            {
                btSave.Content = "Create";
            }
        }

        private void btSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cmbFuelType.SelectedItem == null)
                {
                    MessageBox.Show("Please choose one Fuel Type", "Information");
                    return;
                }
                int id;
                int.TryParse(tbId.Text, out id);
                string modelStr = tbModel.Text.ToString();
                double engineSize = sldEngineSize.Value;
                Car.FuelTypeEnum.TryParse(cmbFuelType.Text, out fuelType);

                AddNewCarCallback?.Invoke(new Car(id, modelStr, engineSize, fuelType));
                DialogResult = true;
            }
            catch (DataInvalidException ex)
            {
                MessageBox.Show(ex.Message, "Error Information");
            }
            //
        }
    }
}
