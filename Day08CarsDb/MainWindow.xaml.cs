using CsvHelper;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Day08CarsDb
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Car> carList = new ObservableCollection<Car>();
        private Car currentCar;
        Database db;

        public MainWindow()
        {
            InitializeComponent();
            db = new Database();
            loadDataFromDatabase();
            lstViewCar.ItemsSource = carList;
        }

        private void loadDataFromDatabase()
        {
            try
            { 
                carList = db.GetAllCars();
                foreach (Car c in carList)
                {
                    Console.WriteLine(c.ToString());
                }
            }
            catch (DataInvalidException ex)
            {
                MessageBox.Show("Error loading data from database: " + ex.Message, "Error Information");
            }
        }

        private void CommandBinding_Exit(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }

        private void CommandBinding_ExportToCsv(object sender, ExecutedRoutedEventArgs e)
        {
            if (lstViewCar.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please choose at least one item from list to export", "Information");
                return;
            }
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "CSV files (*.csv)|*.csv";   // use | to seperate filter type, example: "CSV files (*.csv)|*.csv|Text File (*.txt)|*.txt|All files (*.*)|*.*"
            saveFileDialog.Title = "Export to csv";
            try
            {
                if (saveFileDialog.ShowDialog() == true)
                {
                    using (var writer = new StreamWriter(saveFileDialog.FileName))
                    using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                    {
                        //csv.Configuration.Delimiter = ";";
                        csv.WriteRecords(lstViewCar.SelectedItems);
                    }
                }
            }
            catch (Exception ex) when (ex is IOException || ex is DataInvalidException)
            {
                MessageBox.Show("Error saving data into csv file: " + ex.Message, "Error Information");
            }
        }

        private void CommandBinding_AddNewCar(object sender, ExecutedRoutedEventArgs e)
        {

            try
            {
                AddEditDialog addEditDialog = new AddEditDialog(null);
                addEditDialog.Owner = this;

                addEditDialog.AddNewCarCallback += (c) => { currentCar = c; };
                bool? result = addEditDialog.ShowDialog();  // this line must be stay after the assignment, otherwise value is not assigned

                if (result == true)
                {
                    int newId = db.AddCar(currentCar);
                    currentCar.Id = newId;
                    carList.Add(currentCar);
                }
            }
            catch (DataInvalidException ex)
            {
                MessageBox.Show(ex.Message, "Error Information");
            }
            lstViewCar.SelectedIndex = lstViewCar.Items.Count - 1;//for status bar selection change
        }

        private void miUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (lstViewCar.SelectedItems.Count != 1)
            {
                MessageBox.Show("Please choose one item to update", "Information");
                return;
            }

            try
            {
                int index = lstViewCar.SelectedIndex;
                AddEditDialog addEditDialog = new AddEditDialog((Car)lstViewCar.SelectedItem);
                addEditDialog.Owner = this;

                addEditDialog.AddNewCarCallback += (c) => { currentCar = c; };
                bool? result = addEditDialog.ShowDialog();  // this line must be stay after the assignment, otherwise value is not assigned

                if (result == true)
                {
                    db.UpdateCar(currentCar);
                    carList.ElementAt(index).Id = currentCar.Id;
                    carList.ElementAt(index).MakeModel = currentCar.MakeModel;
                    carList.ElementAt(index).EngineSize = currentCar.EngineSize;
                    carList.ElementAt(index).FuelType = currentCar.FuelType;
                }
            }
            catch (DataInvalidException ex)
            {
                MessageBox.Show(ex.Message, "Error Information");
            }
        }

        private void miDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lstViewCar.SelectedItems.Count != 1)
            {
                MessageBox.Show("Please choose at least one item to delete", "Information");
                return;
            }
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure to delete?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
            try
            {
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    Car c = (Car)lstViewCar.SelectedItem;
                    db.DeleteCar(c.Id);
                    carList.Remove(c); 
                }
            }
            catch (DataInvalidException ex)
            {
                MessageBox.Show(ex.Message, "Error Information");
            }
        }

        private void StatusBar(object sender, RoutedEventArgs e)
        {
            lblStatus.Text = string.Format("You have {0} car(s) currently", carList.Count);
        }
    }
}
