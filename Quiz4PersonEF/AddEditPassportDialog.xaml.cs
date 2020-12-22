using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Quiz4PersonEF
{
    /// <summary>
    /// Interaction logic for AddEditPassport.xaml
    /// </summary>
    public partial class AddEditPassportDialog : Window
    {
        string imageLocation = string.Empty;
        Person currentPerson;
        static string PassportPattern = @"^[A-Z]{2}[0-9]{6}$";

        public AddEditPassportDialog(Person person)
        {
            try
            {
                InitializeComponent();
                currentPerson = person;
                tblName.Text = person.Name;
                loadPassport(person);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error Loading Passport from database:\n" + ex.Message, "Error Information");
            }

        }

        private void loadPassport(Person person)
        {
            using (var ctx = new PersonDbContext())
            {
                Passport currentPassport = ctx.Passports.Include("Person").Where(pa => pa.PassportId == currentPerson.PersonId).FirstOrDefault<Passport>();

                if (currentPassport != null)
                {
                    btSave.Content = "Update";
                    tbPassportNo.Text = currentPassport.Number;
                    tbImage.Text = string.Empty;
                    image.Source = (ImageSource)((new ImageSourceConverter()).ConvertFrom(currentPassport.Photo));
                }
                else
                {
                    resetAndRefresh(person);
                }
            }
        }

        private void resetAndRefresh(Person person)
        {
            btSave.Content = "Add";
            tbPassportNo.Text = string.Empty;
            imageLocation = string.Empty;
            image.Source = null;
            tbImage.Text = "Click to select image";
        }

        private void btSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(tbPassportNo.Text) || string.IsNullOrEmpty(imageLocation))
                {
                    MessageBox.Show("Please input Number and choose image");
                    return;
                }
                Match m = Regex.Match(tbPassportNo.Text, PassportPattern);
                if (!m.Success)
                {
                    MessageBox.Show("PassportNo must be 2 uppercase characters with 6 numbers");
                    return;
                }
                byte[] image = File.ReadAllBytes(imageLocation);

                Passport newPassport = new Passport { PassportId = currentPerson.PersonId, Number = tbPassportNo.Text, Photo = image };

                using (var ctx = new PersonDbContext())
                {
                    Passport toUpdate = ctx.Passports.Include("Person").Where(pa => pa.PassportId == currentPerson.PersonId).FirstOrDefault<Passport>();

                    if (toUpdate != null)
                    {
                        toUpdate.Number = tbPassportNo.Text;
                        if (!string.IsNullOrEmpty(imageLocation))
                        {
                            toUpdate.Photo = File.ReadAllBytes(imageLocation);
                        }
                        ctx.SaveChanges();
                        MessageBox.Show("Passport Updated");
                        resetAndRefresh(currentPerson);
                    }
                    else
                    {
                        ctx.Passports.Add(newPassport);
                        ctx.SaveChanges();
                        MessageBox.Show("Passport Added");
                        resetAndRefresh(currentPerson);
                    }
                }
            }
            catch (Exception ex) when (ex is IOException || ex is FileNotFoundException || ex is SqlException)
            {
                MessageBox.Show("Error adding Passport to database:\n" + ex.Message, "Error Information");
            }
        }

        private void btSelectImage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
                openFileDialog.Title = "Select Image for Passport";

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
    }
}
