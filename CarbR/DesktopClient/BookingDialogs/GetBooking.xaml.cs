using ServiceStack.ServiceClient.Web;
using System;
using System.Collections.Generic;
using System.Configuration;
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
using Web.Requests;

namespace DesktopClient.BookingDialogs
{
    /// <summary>
    /// Interaction logic for GetBooking.xaml
    /// </summary>
    public partial class GetBooking : Window
    {
        public GetBooking()
        {
            InitializeComponent();

            FillComboBox();
        }

        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            if (cbxID.SelectedIndex == -1)
            {
                GetBooking dialog = new GetBooking();

                dialog.ShowDialog();
                if (dialog.DialogResult.HasValue && dialog.DialogResult.Value)
                    MessageBox.Show("Please select ID!");
            }
            else
            {
                JsonServiceClient client = new JsonServiceClient(ConfigurationManager.AppSettings["Client"]);
                var bookings = client.Get(new BookingRequest
                {
                    Id = (int)cbxID.SelectedItem
                });
                foreach (var item in bookings.Result)
                {
                    txtTime.Text = item.StartDate.ToString();
                    txtName.Text = item.User.FullName;
                    txtNoOfPeople.Text = item.Table.NoOfSeats.ToString();
                    txtCafe.Text = item.Cafe.Name;
                }
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void FillComboBox()
        {
            JsonServiceClient client = new JsonServiceClient(ConfigurationManager.AppSettings["Client"]);
            var bookings = client.Get(new GetAllCafeBookingsRequest
            {
                CafeId = 4
            });
            foreach (var item in bookings.Result)
            {
                cbxID.Items.Add(item.Id);
            }
        }

        private void cbxID_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            JsonServiceClient client = new JsonServiceClient(ConfigurationManager.AppSettings["Client"]);
            var bookings = client.Get(new BookingRequest
            {
                Id = (int)cbxID.SelectedItem
            });
            foreach (var item in bookings.Result)
            {
                txtTime.Text = item.StartDate.ToString();
                txtName.Text = item.User.FullName;
                txtNoOfPeople.Text = item.Table.NoOfSeats.ToString();
                txtCafe.Text = item.Cafe.Name;
            }
        }
    }
}
