using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
        List<Owner> ownerList = new List<Owner>();
        public MainWindow()
        {
            try
            {
                InitializeComponent();

                using (var ctx = new CarsOwnerDbContext())
                {
                    //ownerList = (from o in ctx.Owners select o).ToList<Owner>();
                    ownerList = ctx.Owners.ToList<Owner>();
                }
                lstViewOwner.ItemsSource = ownerList;
            }
            catch (SystemException ex)
            {
                MessageBox.Show("Fatal error connecting to database:\n" + ex.Message, "Error Information");
                Environment.Exit(1);
            }   
        }

        private void btAdd_Click(object sender, RoutedEventArgs e)
        {

            using (var ctx = new CarsOwnerDbContext())
            {
                string name = tbName.Text;
                byte[] image = image.
            }
        }


        private void btUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (lstViewOwner.SelectedItems.Count != 1)
            {
                MessageBox.Show("Please choose at least one item to delete", "Information");
                return;
            }
            try
            {
                Owner curentOwner = (Owner)lstViewOwner.SelectedItem;
                using (var ctx = new CarsOwnerDbContext())
                {
                    Owner toUpdate = (from o in ctx.Owners where o.OwnerId == curentOwner.OwnerId select o).FirstOrDefault<Owner>();
                    toUpdate.Name = tbName.Text;
                    if (image.Source.Equals(null))
                    {
                        MessageBox.Show("Please select one image");
                        return;
                    }
                    
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(" ERROR Manage cars: " + ex.Message, "Error Information");
            }
        }

        private void btDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lstViewOwner.SelectedItems.Count != 1)
            {
                MessageBox.Show("Please choose at least one item to delete", "Information");
                return;
            }
            try
            {
                Owner curentOwner = (Owner)lstViewOwner.SelectedItem;
                using (var ctx = new CarsOwnerDbContext())
                {
                    Owner toDelete = (from o in ctx.Owners where o.OwnerId == curentOwner.OwnerId select o).FirstOrDefault<Owner>();
                    if (toDelete != null)
                    {
                        ctx.Owners.Remove(toDelete);
                        ctx.SaveChanges();
                    }
                    else
                    {
                        MessageBox.Show("Record to delete not found");
                    }
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(" ERROR Manage cars: " + ex.Message, "Error Information");
            }
        }

        private void btManage_Click(object sender, RoutedEventArgs e)
        {
            if (lstViewOwner.SelectedItems.Count != 1)
            {
                MessageBox.Show("Please choose at least one Owner to manage car", "Information");
                return;
            }
            try
            {
                    CarsDialog manageCarsDialog = new CarsDialog((Owner)lstViewOwner.SelectedItem);
                    manageCarsDialog.Owner = this;
            }
            catch (Exception ex)
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
                        image.Source = new BitmapImage(new Uri(openFileDialog.FileName));
                        tbImage.Text = "";
                    }
                }
            }catch (IOException ex)
            {
                MessageBox.Show("ERROR Select image: " + ex.Message, "Error Information");
            }
        }
    }
}
