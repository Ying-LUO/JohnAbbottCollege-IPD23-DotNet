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

namespace Day11CarsOwnersEF
{
    /// <summary>
    /// Interaction logic for CarsDialog.xaml
    /// </summary>
    public partial class CarsDialog : Window
    {
        List<Car> carList = new List<Car>();

        public CarsDialog(Owner owner)
        {
            InitializeComponent();
            using (var ctx = new CarsOwnerDbContext())
            {
                carList = (from c in ctx.Cars where (c.OwnerId == owner.OwnerId) select c).ToList<Car>();
            }
            lstViewCar.ItemsSource = carList;
        }

        private void btAdd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btUpdate_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btDelete_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
