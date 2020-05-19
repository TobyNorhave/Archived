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
    /// Interaction logic for UpdateTable.xaml
    /// </summary>
    public partial class UpdateTable : Window
    {
        public UpdateTable()
        {
            InitializeComponent();

            FillComboBox();
        }

        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            if (cbxTableIDs.SelectedIndex == -1)
            {
                UpdateTable dialog = new UpdateTable();

                dialog.ShowDialog();
                if (dialog.DialogResult.HasValue && dialog.DialogResult.Value)
                    MessageBox.Show("Please select an ID!");
            }
            else
            {
                JsonServiceClient client = new JsonServiceClient(ConfigurationManager.AppSettings["Client"]);
                client.Put(new CafeTableRequest
                {
                    Id = (int)cbxTableIDs.SelectedItem,
                    TableNo = (int)cbxTableNo.SelectedItem,
                    NoOfSeats = (int)cbxNoOfSeats.SelectedItem,
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
            var tables = client.Get(new GetAllTablesInCafeRequest
            {
                CafeId = 4
            });
            foreach (var table in tables.Result)
            {
                cbxTableIDs.Items.Add(table.Id);
            }

            for (int i = 1; i < 50; i++)
            {
                cbxTableNo.Items.Add(i);
            }

            cbxNoOfSeats.Items.Add(2);
            cbxNoOfSeats.Items.Add(4);
            cbxNoOfSeats.Items.Add(6);
            cbxNoOfSeats.Items.Add(8);
        }

        private void cbxTableIDs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            JsonServiceClient client = new JsonServiceClient(ConfigurationManager.AppSettings["Client"]);
            var tables = client.Get(new CafeTableRequest
            {
                Id = (int)cbxTableIDs.SelectedItem
            });
            foreach (var item in tables.Result)
            {
                cbxTableNo.SelectedItem = item.TableNo;
                cbxNoOfSeats.SelectedItem = item.NoOfSeats;
            }
        }

    }
}
