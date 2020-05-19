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
    /// Interaction logic for UpdateCafe.xaml
    /// </summary>
    public partial class UpdateCafe : Window
    {
        public UpdateCafe()
        {
            InitializeComponent();

            FillCbx();
        }

        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            if (cbxId.SelectedIndex == -1 || txtName.Text.Length == 0 || txtPhoneNo.Text.Length == 0 || cbxZipCode.SelectedIndex == -1 || 
                txtAddress.Text.Length == 0 || txtDescription.Text.Length == 0 || cbxType.SelectedIndex == -1 || cbxOpenTime.SelectedIndex == -1 || 
                cbxCloseTime.SelectedIndex == -1)
            {
                UpdateCafe dialog = new UpdateCafe();

                dialog.ShowDialog();
                if (dialog.DialogResult.HasValue && dialog.DialogResult.Value)
                    MessageBox.Show("Please fill out the remaining fields!");
            }
            else
            {
                JsonServiceClient client = new JsonServiceClient(ConfigurationManager.AppSettings["Client"]);
                var updatedCafe = client.Put(new CafeRequest
                {
                    Id = (int)cbxId.SelectedItem,
                    Name = txtName.Text,
                    PhoneNo = txtPhoneNo.Text,
                    Zip = (string)cbxZipCode.SelectedItem,
                    Address = txtAddress.Text,
                    Description = txtDescription.Text,
                    Type = (string)cbxType.SelectedItem,
                    OpenTime = Convert.ToDateTime(DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + " " + (string)cbxOpenTime.SelectedItem),
                    CloseTime = Convert.ToDateTime(DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + " " + (string)cbxCloseTime.SelectedItem)
                });

                DialogResult = true;
            }

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void FillCbx()
        {
            //Fill cbxId
            cbxId.Items.Add(4);

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

        private void cbxId_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            JsonServiceClient client = new JsonServiceClient(ConfigurationManager.AppSettings["Client"]);
            var cafes = client.Get(new CafeRequest
            {
                Id = (int)cbxId.SelectedItem
            });
            foreach (var item in cafes.Result)
            {
                txtName.Text = item.Name;
                txtPhoneNo.Text = item.PhoneNo;
                cbxZipCode.SelectedValue = item.Zip;
                txtAddress.Text = item.Address;
                txtDescription.Text = item.Description;
                cbxType.SelectedValue = item.Type;
                cbxOpenTime.SelectedValue = item.OpenTime;
                cbxCloseTime.SelectedValue = item.CloseTime;
            }
        }
    }
}
