using ServiceStack.ServiceClient.Web;
using System;
using System.Collections;
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
    /// Interaction logic for GetTable.xaml
    /// </summary>
    public partial class GetTable : Window
    {
        public GetTable()
        {
            InitializeComponent();

            FillComboBox();
        }

        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            if (cbxTableIDs.SelectedIndex == -1)
            {
                GetTable dialog = new GetTable();

                dialog.ShowDialog();
                if (dialog.DialogResult.HasValue && dialog.DialogResult.Value)
                    MessageBox.Show("Please select an ID!");
            }
            else
            {
                JsonServiceClient client = new JsonServiceClient(ConfigurationManager.AppSettings["Client"]);
                var tables = client.Get(new CafeTableRequest
                {
                    Id = (int)cbxTableIDs.SelectedItem
                });
                foreach (var item in tables.Result)
                {
                    txtTableId.Text = cbxTableIDs.SelectedItem.ToString();
                    txtTableNo.Text = item.TableNo.ToString();
                    txtNoOfSeats.Text = item.NoOfSeats.ToString();
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
            var tables = client.Get(new GetAllTablesInCafeRequest
            {
                CafeId = 4
            });

            foreach (var item in tables.Result)
            {
                cbxTableIDs.Items.Add(item.Id);
            }
        }
    }
}
