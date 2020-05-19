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

namespace DesktopClient.CafeDialogs
{
    /// <summary>
    /// Interaction logic for CreateCafe.xaml
    /// </summary>
    public partial class CreateCafe : Window
    {
        public CreateCafe()
        {
            InitializeComponent();

            FillComboBoxes();
        }

        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            if (txtName.Text.Length == 0 || txtPhoneNo.Text.Length == 0 || cbxZipCode.SelectedIndex == -1 ||
                txtAddress.Text.Length == 0 || txtDescription.Text.Length == 0 || cbxType.SelectedIndex == -1 || cbxOpenTime.SelectedIndex == -1 || cbxCloseTime.SelectedIndex == -1)
            {
                CreateCafe dialog = new CreateCafe();

                dialog.ShowDialog();
                if (dialog.DialogResult.HasValue && dialog.DialogResult.Value)
                    MessageBox.Show("Please fill out the remaining fields!");
            }
            else
            {
                JsonServiceClient client = new JsonServiceClient(ConfigurationManager.AppSettings["Client"]);
                var newCafe = client.Post(new CafeRequest
                {
                    Name = txtName.Text,
                    PhoneNo = txtPhoneNo.Text,
                    Zip = (string)cbxZipCode.SelectedItem,
                    Address = txtAddress.Text,
                    Description = txtDescription.Text,
                    Type = (string)cbxType.SelectedItem,
                    OpenTime = Convert.ToDateTime(DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + " " + (string)cbxOpenTime.SelectedItem),
                    CloseTime = Convert.ToDateTime(DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + " " + (string)cbxCloseTime.SelectedItem),
                    UserId = Convert.ToInt32(txtPassword.Password)
                });

                DialogResult = true;
            }
            
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void FillComboBoxes()
        {
            //Fills cbxOpenTime
            var item = DateTime.Today.AddHours(10); // 10:00:00
            while (item <= DateTime.Today.AddHours(22)) // 14:00:00
            {
                cbxOpenTime.Items.Add(item.TimeOfDay.ToString(@"hh\:mm"));
                item = item.AddMinutes(60);
            }

            //Fills cbxCloseTime
            var item2 = DateTime.Today.AddHours(10); // 10:00:00
            while (item2 <= DateTime.Today.AddHours(22)) // 14:00:00
            {
                cbxCloseTime.Items.Add(item2.TimeOfDay.ToString(@"hh\:mm"));
                item2 = item2.AddMinutes(60);
            }

            //Fills cbxZipCode
            cbxZipCode.Items.Add("9000");
            cbxZipCode.Items.Add("7500");
            cbxZipCode.Items.Add("7600");
            cbxZipCode.Items.Add("7400");

            //Fills cbxType
            cbxType.Items.Add("Casual Dining");
            cbxType.Items.Add("Premium Casual");
            cbxType.Items.Add("Family Style");
            cbxType.Items.Add("Fine Dining");
        }
    }
}
