
using DesktopClient.BookingDialogs;
using DesktopClient.CafeDialogs;
using DesktopClient.TableDialogs;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Web.Requests;

namespace DesktopClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            FillCafeInfo();
            FillUserInfo();

            FillTablesListViews();
            FillBookingsListViews();
            FillCafeListViews();
        }

        private void btnCreateBooking_Click(object sender, RoutedEventArgs e)
        {
            CreateBooking dialog = new CreateBooking();

            dialog.ShowDialog();
            if (dialog.DialogResult.HasValue && dialog.DialogResult.Value)
                MessageBox.Show("Booking has been created!");

            FillBookingsListViews();
        }

        private void btnDeleteBooking_Click(object sender, RoutedEventArgs e)
        {
            DeleteBooking dialog = new DeleteBooking();

            dialog.ShowDialog();
            if (dialog.DialogResult.HasValue && dialog.DialogResult.Value)
                MessageBox.Show("Booking has been deleted!");

            FillBookingsListViews();
        }

        private void btnGetBooking_Click(object sender, RoutedEventArgs e)
        {
            GetBooking dialog = new GetBooking();

            dialog.ShowDialog();

            FillBookingsListViews();
        }

        private void btnCreateTable_Click(object sender, RoutedEventArgs e)
        {
            CreateTable dialog = new CreateTable();

            dialog.ShowDialog();
            if (dialog.DialogResult.HasValue && dialog.DialogResult.Value)
                MessageBox.Show("Table has been created!");

            FillTablesListViews();
        }

        private void btnDeleteTable_Click(object sender, RoutedEventArgs e)
        {
            DeleteTable dialog = new DeleteTable();

            dialog.ShowDialog();
            if (dialog.DialogResult.HasValue && dialog.DialogResult.Value)
                MessageBox.Show("Table has been deleted!");

            FillTablesListViews();
        }

        private void btnGetTable_Click(object sender, RoutedEventArgs e)
        {
            GetTable dialog = new GetTable();

            dialog.ShowDialog();
        }

        private void btnUpdateTable_Click(object sender, RoutedEventArgs e)
        {
            UpdateTable dialog = new UpdateTable();

            dialog.ShowDialog();
            if (dialog.DialogResult.HasValue && dialog.DialogResult.Value)
                MessageBox.Show("Table has been updated!");

            FillTablesListViews();
        }

        private void btnCreateCafe_Click(object sender, RoutedEventArgs e)
        {
            CreateCafe dialog = new CreateCafe();

            dialog.ShowDialog();
            if (dialog.DialogResult.HasValue && dialog.DialogResult.Value)
                MessageBox.Show("Cafe has been created!");

            FillCafeListViews();
        }

        private void btnUpdateCafe_Click(object sender, RoutedEventArgs e)
        {
            UpdateCafe dialog = new UpdateCafe();

            dialog.ShowDialog();
            if (dialog.DialogResult.HasValue && dialog.DialogResult.Value)
                MessageBox.Show("Cafe has been updated!");

            FillCafeListViews();
        }

        private void FillCafeInfo()
        {
            JsonServiceClient client = new JsonServiceClient(ConfigurationManager.AppSettings["Client"]);
            var cafes = client.Get(new CafeRequest
            {
                Id = 4
            });
            foreach (var item in cafes.Result)
            {
                txtCafeName.Text = $"{item.Name}";
                txtCafePhoneNo.Text = $"{item.PhoneNo}";
                txtZipCode.Text = $"{item.Zip}";
                txtOpenTime.Text = $"{item.OpenTime}";
                txtCloseTime.Text = $"{item.CloseTime}";
                txtAddress.Text = $"{item.Address}";
                txtType.Text = $"{item.Type}";
                txtDescription.Text = $"{item.Description}";
            }
        }

        private void FillUserInfo()
        {
            JsonServiceClient client = new JsonServiceClient(ConfigurationManager.AppSettings["Client"]);
            var cafes = client.Get(new UserRequest
            {
                Id = 1
            });
            foreach (var item in cafes.Result)
            {
                txtUserName.Text = $"{item.FullName}";
                txtUserPhoneNo.Text = $"{item.PhoneNo}";
                txtUserEmail.Text = $"{item.Email}";

                txtUpdateUserName.Text = $"{item.FullName}";
                txtUpdateUserPhoneNo.Text = $"{item.PhoneNo}";
                txtUpdateEmail.Text = $"{item.Email}";
            }
        }

        private void btnUpdateUser_Click(object sender, RoutedEventArgs e)
        {
            if (txtUpdateUserName.Text.Length == 0 || txtUpdateUserPhoneNo.Text.Length == 0 || txtUpdateEmail.Text.Length == 0)
            {
                MainWindow dialog = new MainWindow();

                dialog.ShowDialog();
                if (dialog.DialogResult.HasValue && dialog.DialogResult.Value)
                    MessageBox.Show("Please fill out the remaining fields!");
            }
            else
            {
                JsonServiceClient client = new JsonServiceClient(ConfigurationManager.AppSettings["Client"]);
                client.Put(new UserRequest
                {
                    Id = 3,
                    FullName = txtUpdateUserName.Text,
                    PhoneNo = txtUpdateUserPhoneNo.Text,
                    UserName = "Moha2903",
                    Password = "123",
                    Email = txtUpdateEmail.Text,
                });
            }
        }

        private void FillTablesListViews()
        {
            lvTables.Items.Refresh();
            List<object> cafeTablesList = new List<object>(); 

            JsonServiceClient client = new JsonServiceClient(ConfigurationManager.AppSettings["Client"]);
            var cafeTables = client.Get(new GetAllTablesInCafeRequest
            {
                CafeId = 4
            });
            foreach (var item in cafeTables.Result)
            {
                cafeTablesList.Add(item);
            }

            lvTables.ItemsSource = cafeTablesList;
        }

        private void FillBookingsListViews()
        {
            lvBookings.Items.Refresh();
            List<object> bookingsList = new List<object>();

            JsonServiceClient client = new JsonServiceClient(ConfigurationManager.AppSettings["Client"]);
            var bookings = client.Get(new GetAllCafeBookingsRequest
            {
                CafeId = 4
            });
            foreach (var item in bookings.Result)
            {
                bookingsList.Add(item);
            }

            lvBookings.ItemsSource = bookingsList;
        }

        private void FillCafeListViews()
        {
            lvCafes.Items.Refresh();
            List<object> cafesList = new List<object>();

            JsonServiceClient client = new JsonServiceClient(ConfigurationManager.AppSettings["Client"]);
            var cafes = client.Get(new GetCafesByUserId
            {
                UserId = 1
            });
            foreach (var item in cafes.Result)
            {
                cafesList.Add(item);
            }

            lvBookings.ItemsSource = cafesList;
        }
    }
}
