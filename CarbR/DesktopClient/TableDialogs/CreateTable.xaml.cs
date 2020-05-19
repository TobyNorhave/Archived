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

namespace DesktopClient.TableDialogs
{
    /// <summary>
    /// Interaction logic for CreateTable.xaml
    /// </summary>
    public partial class CreateTable : Window
    {
        public CreateTable()
        {
            InitializeComponent();

            FillComboBox();
        }

        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            if (cbxTableNo.SelectedIndex == -1 || cbxNoOfSeats.SelectedIndex == -1)
            {
                CreateTable dialog = new CreateTable();

                dialog.ShowDialog();
                if (dialog.DialogResult.HasValue && dialog.DialogResult.Value)
                    MessageBox.Show("Please select TableNo and NoOfSeats!");
            }
            else
            {
                JsonServiceClient client = new JsonServiceClient(ConfigurationManager.AppSettings["Client"]);
                var newCafeTable = client.Post(new CafeTableRequest
                {
                    TableNo = (int)cbxTableNo.SelectedItem,
                    NoOfSeats = (int)cbxNoOfSeats.SelectedItem,
                    CafeId = 4
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
            for (int i = 1; i < 50; i++)
            {
                cbxTableNo.Items.Add(i);
            }

            cbxNoOfSeats.Items.Add(2);
            cbxNoOfSeats.Items.Add(4);
            cbxNoOfSeats.Items.Add(6);
            cbxNoOfSeats.Items.Add(8);
        }
    }
}
