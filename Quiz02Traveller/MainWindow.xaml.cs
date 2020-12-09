using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Quiz02Traveller
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private ObservableCollection<Trip> tripList = new ObservableCollection<Trip>();
        private string path = @"..\..\trips.txt";
        private string text = string.Empty;

        public MainWindow()
        {
            InitializeComponent();
            readFromFile();
            lstViewTrip.ItemsSource = tripList;
        }

        private void readFromFile()
        {
            try
            {
                string[] stringList = File.ReadAllLines(path);
                foreach (string dataStr in stringList)
                {
                    tripList.Add(new Trip(dataStr));
                }
            }
            catch (Exception ex) when (ex is IOException || ex is DataInvalidException)
            {
                MessageBox.Show("Error reading data from file: " + ex.Message);
            }
        }

        private void btExport_Click(object sender, RoutedEventArgs e)
        {
            if (lstViewTrip.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please choose at least one item from list to save");
                return;
            }
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text File (*.txt)|*.txt";
            try
            {
                if (saveFileDialog.ShowDialog() == true)
                {
                    foreach (Trip trip in lstViewTrip.SelectedItems)
                    {
                        text += trip.ToString();
                    }
                    File.WriteAllText(saveFileDialog.FileName, text);
                }
            }
            catch (Exception ex) when (ex is IOException || ex is DataInvalidException)
            {
                MessageBox.Show("Error saving data into file: " + ex.Message);
            }
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                foreach (Trip trip in tripList)
                {
                    text += trip.ToString();
                }
                File.WriteAllText(path, text);
            }
            catch (Exception ex) when (ex is IOException || ex is DataInvalidException)
            {
                MessageBox.Show("Error saving data into file: " + ex.Message);
            }
        }

        private void btAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbDestination.Text) || string.IsNullOrEmpty(tbName.Text)
                || string.IsNullOrEmpty(tbPassportNo.Text) || dpDepDate.SelectedDate.Equals(null) || dpRetDate.SelectedDate.Equals(null))
            {
                MessageBox.Show("Please input value");
                return;
            }
            try
            {
                tripList.Add(new Trip(tbDestination.Text, tbName.Text, tbPassportNo.Text, dpDepDate.SelectedDate.Value, dpRetDate.SelectedDate.Value));
                clearInput();
            }
            catch (DataInvalidException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void clearInput()
        {
            tbDestination.Text = string.Empty;
            tbName.Text = string.Empty;
            tbPassportNo.Text = string.Empty;
            dpDepDate.SelectedDate = null;
            dpRetDate.SelectedDate = null;
        }

        private void btUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (lstViewTrip.SelectedItems.Count != 1)
            {
                MessageBox.Show("Please choose one item to update");
                return;
            }

            try
            {
                int index = lstViewTrip.SelectedIndex;

                tripList.ElementAt(index).Destination = tbDestination.Text;
                tripList.ElementAt(index).Name = tbName.Text;
                tripList.ElementAt(index).PassportNo = tbPassportNo.Text;
                tripList.ElementAt(index).DepartureDate = dpDepDate.SelectedDate.Value;
                tripList.ElementAt(index).ReturnDate = dpRetDate.SelectedDate.Value;

                clearInput();
            }
            catch (DataInvalidException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lstViewTrip.SelectedItems.Count != 1)
            {
                MessageBox.Show("Please choose at least one item to delete");
                return;
            }
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure to delete?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
            try
            {
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    int index = lstViewTrip.SelectedIndex;
                    tripList.RemoveAt(index);
                    clearInput();
                }
            }
            catch (DataInvalidException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void listView_Click(object sender, SelectionChangedEventArgs e)
        {
            if (lstViewTrip.SelectedItems.Count!=0)
            {
                //int index = lstViewTrip.SelectedIndex;

                int index = tripList.IndexOf((Trip)lstViewTrip.SelectedItems[0]);
                tbDestination.Text = tripList.ElementAt(index).Destination;
                tbName.Text = tripList.ElementAt(index).Name;
                tbPassportNo.Text = tripList.ElementAt(index).PassportNo;
                dpDepDate.SelectedDate = tripList.ElementAt(index).DepartureDate;
                dpRetDate.SelectedDate = tripList.ElementAt(index).ReturnDate;
            }
        }
    }
}
