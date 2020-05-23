using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using RentalCore.Utils;

namespace RentalGUI
{
    public partial class ManagerWindow : Window
    {
        QueryMethods qm = new QueryMethods();
        List<ManagerQh> managers = new List<ManagerQh>();
        SqlConnection conn = DbUtils.GetDBConnection();
        public ManagerWindow()
        {
            InitializeComponent();
            try
            {
                conn.Open();
                UpdateManagers();
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

        private void UpdateManagers()
        {
            managers = qm.QueryManagers(conn);
            ManagersDataGrid.ItemsSource = null;
            ManagersDataGrid.ItemsSource = managers;
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

        private void ClientsButton_OnClick(object sender, RoutedEventArgs e)
        {
            CloseSqlConnection();
            var cw = new ClientWindow();
            cw.Show();
            this.Close();
        }
    }
}
