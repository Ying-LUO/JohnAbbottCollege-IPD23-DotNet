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

namespace Day11CarsOwnersEF
{
    /// <summary>
    /// Interaction logic for CarsDialog.xaml
    /// </summary>
    public partial class CarsDialog : Window
    {
        Owner currentOwner = null;

        public CarsDialog(Owner owner)
        {
            InitializeComponent();
            currentOwner = owner;
            try
            {
                resetAndRefresh(currentOwner);
            }
            catch (SystemException ex)
            {
                MessageBox.Show("Fatal error connecting to database:\n" + ex.Message, "Error Information");
                Environment.Exit(1);
            }

        }

        private void resetAndRefresh(Owner owner)
        {
            tbOwnerName.Text = owner.Name;
            tbModel.Text = string.Empty;
            btDelete.IsEnabled = false;
            btUpdate.IsEnabled = false;
            using (var ctx = new CarsOwnerDbContext())
            {
                lstViewCar.ItemsSource = (from c in ctx.Cars.Include("Owner") where (c.OwnerId == owner.OwnerId) select c).ToList<Car>();
            }
        }

        private void btAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(tbModel.Text))
                {
                    MessageBox.Show("Please input Model");
                    return;
                }
                Car newCar = new Car { MakeModel= tbModel.Text, OwnerId = currentOwner.OwnerId};
                using (var ctx = new CarsOwnerDbContext())
                {
                    ctx.Cars.Add(newCar);
                    currentOwner.CarsInGarage.Add(newCar);
                    ctx.SaveChanges();
                    MessageBox.Show("Car Added");
                    resetAndRefresh(currentOwner);
                    
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(" ERROR Select Car: " + ex.Message, "Error Information");
            }
        }

        private void btUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (lstViewCar.SelectedItems.Count != 1)
            {
                MessageBox.Show("Please choose just one item to update", "Information");
                return;
            }
            try
            {
                Car currentCar = (Car)lstViewCar.SelectedItem;
                using (var ctx = new CarsOwnerDbContext())
                {
                    Car toUpdate = ctx.Cars.Include("Owner").Where(c => c.CarId == currentCar.CarId).FirstOrDefault<Car>();
                    if (toUpdate != null)
                    {
                        toUpdate.MakeModel = tbModel.Text;
                        ctx.SaveChanges();
                        MessageBox.Show("Car Updated");
                        resetAndRefresh(currentOwner);
                    }
                    else
                    {
                        MessageBox.Show("Record to update not found");
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(" ERROR Update Owner: " + ex.Message, "Error Information");
            }
        }

        private void btDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lstViewCar.SelectedItems.Count != 1)
            {
                MessageBox.Show("Please choose just one item to delete", "Information");
                return;
            }
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure to delete?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
            try
            {
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    Car currentCar = (Car)lstViewCar.SelectedItem;

                    using (var ctx = new CarsOwnerDbContext())
                    {
                        Car toDelete = ctx.Cars.Include("Owner").Where( c=>c.CarId == currentCar.CarId ).FirstOrDefault<Car>();
                        if (toDelete != null)
                        {
                            ctx.Cars.Remove(toDelete);
                            ctx.SaveChanges();
                            MessageBox.Show("Car Delete");
                            resetAndRefresh(currentOwner);
                        }
                        else
                        {
                            MessageBox.Show("Record to update not found");
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(" ERROR Update Owner: " + ex.Message, "Error Information");
            }
        }

        private void lstViewCar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (lstViewCar.SelectedItems.Count != 0)
                {
                    btUpdate.IsEnabled = true;
                    btDelete.IsEnabled = true;
                    Car currentCar = (Car)lstViewCar.SelectedItem;
                    using (var ctx = new CarsOwnerDbContext())
                    {
                        Car selectedCar = (from c in ctx.Cars where c.CarId == currentCar.CarId select c).FirstOrDefault<Car>();
                        tbModel.Text = selectedCar.MakeModel;
                    }
                }
                else
                {
                    resetAndRefresh(currentOwner);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(" ERROR Select Car: " + ex.Message, "Error Information");
            }
           
        }
    }
}
