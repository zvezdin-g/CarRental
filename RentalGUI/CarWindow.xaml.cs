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

        private void ManagersButton_OnClick(object sender, RoutedEventArgs e)
        {
            CloseSqlConnection();
            var mw = new ManagerWindow();
            mw.Show();
            this.Close();
        }

        private void AddCarButton_OnClick(object sender, RoutedEventArgs e)
        {
            var add = new CarWindow_Car(conn, null);
            add.ShowDialog();
            UpdateCars();
        }

        private void ShowCarButton_OnClick(object sender, RoutedEventArgs e)
        {
            CarQh selectedCar = (CarQh)CarsDataGrid.SelectedItem;
            if (selectedCar == null)
            {
                MessageBox.Show("Choose car to show");
                return;
            }
            var show = new CarWindow_Car(conn, selectedCar);
            show.ShowDialog();
            UpdateCars();
        }
    }
}
