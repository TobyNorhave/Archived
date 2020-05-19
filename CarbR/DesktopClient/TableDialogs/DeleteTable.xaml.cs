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
    /// Interaction logic for DeleteTable.xaml
    /// </summary>
    public partial class DeleteTable : Window
    {
        public DeleteTable()
        {
            InitializeComponent();

            FillComboBox();
        }

        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            if (cbxTableIDs.SelectedIndex == -1)
            {
                DeleteTable dialog = new DeleteTable();

                dialog.ShowDialog();
                if (dialog.DialogResult.HasValue && dialog.DialogResult.Value)
                    MessageBox.Show("Please enter a valid a ID!");
            }
            else
            {
                JsonServiceClient client = new JsonServiceClient(ConfigurationManager.AppSettings["Client"]);
                client.Delete(new DeleteTableRequest
                {
                    Id = (int)cbxTableIDs.SelectedItem
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
        }
    }
}
