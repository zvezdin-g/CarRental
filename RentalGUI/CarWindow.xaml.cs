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
using RentalCore.Utils;
using System.Data.SqlClient;

namespace RentalGUI
{
    
    public partial class CarWindow : Window
    {
        QueryMethods qm = new QueryMethods();
        List<CarQh> carsList = new List<CarQh>();
        SqlConnection conn = DbUtils.GetDBConnection();
        public CarWindow()
        {
            InitializeComponent();
            try
            {
                conn.Open();
                UpdateCars();
            }
            catch (Exception e)
            {
                MessageBox.Show($"Error: {e}");
            }
        }

        private void UpdateCars()
        {
            carsList = qm.QueryCars(conn);
            CarsDataGrid.ItemsSource = null;
            CarsDataGrid.ItemsSource = carsList;
        }
        private void CloseSqlConnection()
        {
            conn.Close();
            conn.Dispose();
        }

        private void SessionsButton_OnClick(object sender, RoutedEventArgs e)
        {
            CloseSqlConnection();
            var mw = new MainWindow();
            mw.Show();
            this.Close();
        }
    }
}
