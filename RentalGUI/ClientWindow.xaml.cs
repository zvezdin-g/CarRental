using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.SqlClient;
using RentalCore.Utils;

namespace RentalGUI
{
    public partial class ClientWindow : Window
    {
        QueryMethods qm = new QueryMethods();
        List<ClientQh> clientsList = new List<ClientQh>();
        SqlConnection conn = DbUtils.GetDBConnection();
        public ClientWindow()
        {
            InitializeComponent();
            try
            {
                conn.Open();
                UpdateClients();
            }
            catch (Exception e)
            {
                MessageBox.Show($"Error: {e}");
            }
        }

        private void CloseSqlConnection()
        {
            conn.Close();
            conn.Dispose();
        }
        private void CarsButton_OnClick(object sender, RoutedEventArgs e)
        {
            CloseSqlConnection();
            var cw = new CarWindow();
            cw.Show();
            this.Close();
        }

        private void UpdateClients()
        {
            clientsList = qm.QueryClients(conn);
            ClientsDataGrid.ItemsSource = null;
            ClientsDataGrid.ItemsSource = clientsList;
        }
        private void SessionsButton_OnClick(object sender, RoutedEventArgs e)
        {
            CloseSqlConnection();
            var mw = new MainWindow();
            mw.Show();
            this.Close();
        }
        private void RentalButton_OnClick(object sender, RoutedEventArgs e)
        {
            var rw = new RentalWindow(conn);
            rw.ShowDialog();

        }

        private void ManagersButton_OnClick(object sender, RoutedEventArgs e)
        {
            CloseSqlConnection();
            var mw = new ManagerWindow();
            mw.Show();
            this.Close();
        }

        private void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            var add = new ClientWindow_Add(conn);
            add.ShowDialog();
            UpdateClients();
        }
    }
}
