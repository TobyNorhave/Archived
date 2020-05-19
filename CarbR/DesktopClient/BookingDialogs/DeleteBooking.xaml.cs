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
    /// Interaction logic for DeleteBooking.xaml
    /// </summary>
    public partial class DeleteBooking : Window
    {
        public DeleteBooking()
        {
            InitializeComponent();

            FillComboBox();
        }

        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            if (cbxID.SelectedIndex == -1)
            {
                DeleteBooking dialog = new DeleteBooking();

                dialog.ShowDialog();
                if (dialog.DialogResult.HasValue && dialog.DialogResult.Value)
                    MessageBox.Show("Please select Id!");
            }
            else
            {
                JsonServiceClient client = new JsonServiceClient(ConfigurationManager.AppSettings["Client"]);
                client.Delete(new DeleteBookingRequest
                {
                    Id = (int)cbxID.SelectedItem
                });

                DialogResult = true;
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
            foreach (var booking in bookings.Result)
            {
                cbxID.Items.Add(booking.Id);
            }
        }
    }
}
