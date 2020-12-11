using CsvHelper;
using CsvHelper.TypeConversion;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.SqlClient;
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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Quiz3FinalFlights
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Flight> flightList = new List<Flight>();
        private Flight currFlight;
        Database db;

        public MainWindow()
        {
            InitializeComponent();
            try
            {
                db = new Database();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("FATAL: failed to connect database: " + ex.Message, "Error Information");
            }
            loadDataFromDatabase();
            lstView.ItemsSource = flightList;
            ((INotifyCollectionChanged)lstView.Items).CollectionChanged += listView_CollectionChanged;
        }

        private void listView_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            tbStatus.Text = $"Total Flights: {flightList.Count}";
        }

        private void loadDataFromDatabase()
        {
            try
            {
                flightList = db.GetAllFlights();
                foreach (Flight f in flightList)
                {
                    Console.WriteLine(f.ToString());
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
            if (lstView.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please choose at least one item from list to export", "Information");
                return;
            }
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "CSV files (*.csv)|*.csv";
            saveFileDialog.Title = "Export to csv";
            try
            {
                if (saveFileDialog.ShowDialog() == true)
                {
                    using (var writer = new StreamWriter(saveFileDialog.FileName))
                    using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                    {
                        var options = new TypeConverterOptions { Formats = new[] { "yyyy-MM-dd" } };
                        csv.Configuration.TypeConverterOptionsCache.AddOptions<DateTime>(options);
                        csv.WriteRecords(lstView.SelectedItems);
                    }
                }
            }
            catch (Exception ex) when (ex is IOException || ex is DataInvalidException)
            {
                MessageBox.Show("Error saving data into csv file: " + ex.Message, "Error Information");
            }
        }

        private void CommandBinding_AddNewFlight(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                AddEditFlightDialog addEditDialog = new AddEditFlightDialog(null);
                addEditDialog.Owner = this;

                addEditDialog.AddNewFlightCallback += (t) => { currFlight = t; };
                bool? result = addEditDialog.ShowDialog();

                if (result == true)
                {
                    int newId = db.AddFlight(currFlight);
                    currFlight.Id = newId;
                    flightList.Add(currFlight);
                    lstView.Items.Refresh();
                }
            }
            catch (DataInvalidException ex)
            {
                MessageBox.Show(ex.Message, "Error Information");
            }
        }

        private void miDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lstView.SelectedItems.Count != 1)
            {
                MessageBox.Show("Please choose at least one item to delete", "Information");
                return;
            }
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure to delete?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
            try
            {
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    Flight flight = (Flight)lstView.SelectedItem;
                    db.DeleteFlight(flight.Id);
                    flightList.Remove(flight);
                    lstView.Items.Refresh();
                }
            }
            catch (DataInvalidException ex)
            {
                MessageBox.Show(ex.Message, "Error Information");
            }
        }

        private void Update_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lstView.SelectedItems.Count != 1)
            {
                MessageBox.Show("Please choose one item to update", "Information");
                return;
            }

            try
            {
                int index = lstView.SelectedIndex;
                AddEditFlightDialog addEditDialog = new AddEditFlightDialog((Flight)lstView.SelectedItem);
                addEditDialog.Owner = this;

                addEditDialog.AddNewFlightCallback += (c) => { currFlight = c; };
                bool? result = addEditDialog.ShowDialog();  // this line must be stay after the assignment, otherwise value is not assigned

                if (result == true)
                {
                    db.UpdateFlight(currFlight);
                    flightList.ElementAt(index).Id = currFlight.Id;
                    flightList.ElementAt(index).OnDay = currFlight.OnDay;
                    flightList.ElementAt(index).FromCode = currFlight.FromCode;
                    flightList.ElementAt(index).ToCode = currFlight.ToCode;
                    flightList.ElementAt(index).Type = currFlight.Type;
                    flightList.ElementAt(index).Passengers = currFlight.Passengers;
                    lstView.Items.Refresh();
                }
            }
            catch (DataInvalidException ex)
            {
                MessageBox.Show(ex.Message, "Error Information");
            }
        }
    }
}
