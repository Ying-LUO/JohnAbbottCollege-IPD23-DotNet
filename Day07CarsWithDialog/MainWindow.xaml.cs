using CsvHelper;
using CsvHelper.Configuration;
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

namespace Day07CarsWithDialog
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Car> carList = new ObservableCollection<Car>();
        private string model = string.Empty;
        private double engine = 0;
        private Car.FuelTypeEnum fuel = Car.FuelTypeEnum.Other;
        private string path = @"..\..\cars.csv";
        private string text = string.Empty;

        public MainWindow()
        {
            InitializeComponent();
            loadDataFromFile();
            lstViewCar.ItemsSource = carList;
        }

        private void loadDataFromFile()
        {
            try
            {
                using (var reader = new StreamReader(path))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<Car>();
                    foreach (Car c in records)
                    {
                        carList.Add(c);
                    }
                }
            }
            catch (Exception ex) when (ex is IOException || ex is DataInvalidException || ex is XamlParseException || ex is ReaderException || ex is ArgumentException)
            {
                MessageBox.Show("Error loading data from csv: " + ex.Message, "Error Information");
            }
        }

        private void saveDataToFile()
        {
            try
            {
                using (var writer = new StreamWriter(path))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    //csv.Configuration.Delimiter = ";";      // when write records with ;, the whole line is combined as one string, instead of three seperated string delimited by , //check thru EXCEL is much clearer
                    csv.WriteHeader<Car>();
                    csv.NextRecord();
                    csv.WriteRecords(carList);
                }
            }
            catch (Exception ex) when (ex is IOException || ex is DataInvalidException)
            {
                MessageBox.Show("Error saving data into file: " + ex.Message, "Error Information");
            }
        }

        private void CommandBinding_Exit(object sender, ExecutedRoutedEventArgs e)
        {
            saveDataToFile();
            Close();
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            saveDataToFile();
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
            try
            {
                if (saveFileDialog.ShowDialog() == true)
                {
                    using (var writer = new StreamWriter(saveFileDialog.FileName))
                    using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                    {
                        csv.Configuration.Delimiter = ";";
                        csv.WriteRecords(lstViewCar.SelectedItems);
                    }
                }
            }
            catch (Exception ex) when (ex is IOException || ex is DataInvalidException)
            {
                MessageBox.Show("Error saving data into file: " + ex.Message, "Error Information");
            }
        }

        private void CommandBinding_AddNewCar(object sender, ExecutedRoutedEventArgs e)
        {
            
            try
            {
                AddEditDialog addEditDialog = new AddEditDialog(null);
                addEditDialog.Owner = this;
                
                addEditDialog.AssignResult += (m, en, f) => { model = m; engine = en; fuel = f; };
                bool? result = addEditDialog.ShowDialog();  // this line must be stay after the assignment, otherwise value is not assigned

                if (result == true)
                {
                    carList.Add(new Car(model, engine, fuel));
                }
            }
            catch (DataInvalidException ex)
            {
                MessageBox.Show(ex.Message, "Error Information");
            }
            lstViewCar.SelectedIndex = lstViewCar.Items.Count-1;//for status bar selection change
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

                addEditDialog.AssignResult += (m, en, f) => { model = m; engine = en; fuel = f; };
                bool? result = addEditDialog.ShowDialog();  // this line must be stay after the assignment, otherwise value is not assigned

                if (result == true)
                {
                    carList.ElementAt(index).MakeModel = model;
                    carList.ElementAt(index).EngineSize = engine;
                    carList.ElementAt(index).FuelType = fuel;
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
                    int index = lstViewCar.SelectedIndex;
                    carList.RemoveAt(index);
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
