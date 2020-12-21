using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
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


namespace Day11CarsOwnersEF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string imageLocation = string.Empty;
        public MainWindow()
        {
            try
            {
                InitializeComponent();
                resetAndRefresh();
            }
            catch (SystemException ex)
            {
                MessageBox.Show("Fatal error connecting to database:\n" + ex.Message, "Error Information");
                Environment.Exit(1);
            }
        }

        private void btAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(tbName.Text) || string.IsNullOrEmpty(imageLocation))
                {
                    MessageBox.Show("Please input Name and choose image");
                    return;
                }
                byte[] image = File.ReadAllBytes(imageLocation);
                Owner newOwner = new Owner { Name = tbName.Text, Photo = image };
                using (var ctx = new CarsOwnerDbContext())
                {
                    ctx.Owners.Add(newOwner);
                    ctx.SaveChanges();
                    MessageBox.Show("Owner Added");
                    resetAndRefresh();
                }
            }
            catch (Exception ex) when (ex is IOException || ex is FileNotFoundException || ex is SqlException)
            {
                MessageBox.Show("Error adding Owner to database:\n" + ex.Message, "Error Information");
            }
            
        }

        private void resetAndRefresh()
        {
            tbName.Text = string.Empty;
            imageLocation = string.Empty;
            image.Source = null;
            tbImage.Text = "Click to select image";
            btUpdate.IsEnabled = false;
            btDelete.IsEnabled = false;
            btManage.IsEnabled = false;
            using (var ctx = new CarsOwnerDbContext())
            {
                lstViewOwner.ItemsSource = ctx.Owners.Include("CarsInGarage").ToList<Owner>();
            }
            lstViewOwner.Items.Refresh();

        }

        private void btUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (lstViewOwner.SelectedItems.Count != 1)
            {
                MessageBox.Show("Please choose just one item to update", "Information");
                return;
            }
            try
            {
                Owner curentOwner = (Owner)lstViewOwner.SelectedItem;
                using (var ctx = new CarsOwnerDbContext())
                {
                    Owner toUpdate = (from o in ctx.Owners where o.OwnerId == curentOwner.OwnerId select o).FirstOrDefault<Owner>();
                    if (toUpdate != null)
                    {
                        toUpdate.Name = tbName.Text;
                        if (!string.IsNullOrEmpty(imageLocation))
                        {
                            toUpdate.Photo = File.ReadAllBytes(imageLocation);
                        }
                        ctx.SaveChanges();
                        MessageBox.Show("Owner Updated");
                        resetAndRefresh();
                    }
                    else
                    {
                        MessageBox.Show("Record to update not found");
                    }
                }
            }
            catch (Exception ex) when (ex is IOException || ex is FileNotFoundException || ex is SqlException)
            {
                MessageBox.Show(" ERROR Update Owner: " + ex.Message, "Error Information");
            }
        }

        private void btDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lstViewOwner.SelectedItems.Count != 1)
            {
                MessageBox.Show("Please choose just one item to delete", "Information");
                return;
            }
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure to delete?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
            try
            {
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    Owner curentOwner = (Owner)lstViewOwner.SelectedItem;
                    using (var ctx = new CarsOwnerDbContext())
                    {
                        Owner toDelete = (from o in ctx.Owners where o.OwnerId == curentOwner.OwnerId select o).FirstOrDefault<Owner>();
                        if (toDelete != null)
                        {
                            ctx.Owners.Remove(toDelete);
                            ctx.SaveChanges();
                            MessageBox.Show("Owner Deleted");
                            resetAndRefresh();
                        }
                        else
                        {
                            MessageBox.Show("Record to delete not found");
                        }
                    }
                }
            }
            catch (SqlException ex) 
            {
                MessageBox.Show(" ERROR Delete Owner: " + ex.Message, "Error Information");
            }
        }

        private void btManage_Click(object sender, RoutedEventArgs e)
        {
            if (lstViewOwner.SelectedItems.Count != 1)
            {
                MessageBox.Show("Please choose just one Owner to manage car", "Information");
                return;
            }
            try
            {
                Owner curentOwner = (Owner)lstViewOwner.SelectedItem;
                CarsDialog manageCarsDialog = new CarsDialog(curentOwner);
                manageCarsDialog.Owner = this;
                manageCarsDialog.ShowDialog();
                resetAndRefresh();
            }
            catch (Exception ex) when (ex is ApplicationException || ex is SqlException)
            {
                MessageBox.Show(" ERROR Manage cars: " + ex.Message, "Error Information");
            }
        }

        private void btSelectImage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
                openFileDialog.Title = "Select Image for Owner";

                if (openFileDialog.ShowDialog() == true)
                {
                    string fileName = openFileDialog.FileName;
                    if (!string.IsNullOrEmpty(fileName) && File.Exists(fileName))
                    {
                        image.Source = new BitmapImage(new Uri(fileName));
                        tbImage.Text = string.Empty;
                        imageLocation = fileName;
                    }
                }
            }
            catch (Exception ex) when (ex is IOException || ex is FileNotFoundException)
            {
                MessageBox.Show("ERROR Select image: " + ex.Message, "Error Information");
            }
        }

        private void lstViewOwner_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (lstViewOwner.SelectedItems.Count != 0)
                {
                    btUpdate.IsEnabled = true;
                    btDelete.IsEnabled = true;
                    btManage.IsEnabled = true;
                    Owner curentOwner = (Owner)lstViewOwner.SelectedItem;
                    using (var ctx = new CarsOwnerDbContext())
                    {
                        Owner selectedOwner = (from o in ctx.Owners where o.OwnerId == curentOwner.OwnerId select o).FirstOrDefault<Owner>();
                        tbName.Text = selectedOwner.Name;
                        image.Source = (ImageSource)((new ImageSourceConverter()).ConvertFrom(selectedOwner.Photo));  //WPF ImageSourceConverter class, not ImageConverter which is for WinForms
                        tbImage.Text = string.Empty;
                    }
                }
                else
                {
                    resetAndRefresh();
                }
            }
            catch (SqlException ex) 
            {
                MessageBox.Show(" ERROR Select Owner: " + ex.Message, "Error Information");
            }
        }

        
    }
}
