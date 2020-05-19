using ServiceStack.ServiceClient.Web;
using System;
using System.Configuration;
using System.Windows;
using Web.Requests;

namespace DesktopClient.BookingDialogs
{
    /// <summary>
    /// Interaction logic for CreateBooking.xaml
    /// </summary>
    public partial class CreateBooking : Window
    {
        public CreateBooking()
        {
            InitializeComponent();

            FillComboBoxes();
        }

        private void BtnClickAccept_Click(object sender, RoutedEventArgs e)
        {

            if (cbxNoOfPeople.SelectedIndex == -1)
            {
                CreateBooking dialog = new CreateBooking();

                dialog.ShowDialog();
                if (dialog.DialogResult.HasValue && dialog.DialogResult.Value)
                    MessageBox.Show("Please select Time and NoOfSeats!");
            }
            else
            {
                JsonServiceClient client = new JsonServiceClient(ConfigurationManager.AppSettings["Client"]);
                client.Post(new BookingRequest
                {
                    StartDate = DateTime.Now,
                    UserId = 2,
                    NoOfPeople = (int)cbxNoOfPeople.SelectedItem,
                    CafeId = 1
                });

                DialogResult = true;
            }
        }

        private void BtnClickCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void FillComboBoxes()
        {
            var item = DateTime.Today.AddHours(10); // 10:00:00
            while (item <= DateTime.Today.AddHours(22)) // 14:00:00
            {
                cbxHours.Items.Add(item.TimeOfDay.ToString(@"hh\:mm"));
                item = item.AddMinutes(60);
            }

            cbxNoOfPeople.Items.Add(2);
            cbxNoOfPeople.Items.Add(4);
            cbxNoOfPeople.Items.Add(6);
            cbxNoOfPeople.Items.Add(8);
        }
    }
}
