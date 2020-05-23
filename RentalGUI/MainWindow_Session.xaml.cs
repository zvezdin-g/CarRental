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
    public partial class MainWindow_Session : Window
    {
        QueryMethods qm = new QueryMethods();
        SqlConnection connection = new SqlConnection();
        List<ClientQh> clients = new List<ClientQh>();
        List<CarQh> available_cars = new List<CarQh>();
        SessionQh selectedSession = new SessionQh();
        ClientQh selectedClient = new ClientQh();
        CarQh selectedCar = new CarQh();
        RentalQh start = new RentalQh();
        RentalQh end = new RentalQh();
        ManagerQh selectedManager = new ManagerQh();
        public MainWindow_Session(SqlConnection conn, SessionQh session)
        {
            InitializeComponent();
            connection = conn;
            selectedSession = session;
            
            if (selectedSession != null)
            {
                var title = "Show session";
                Title = title;
                TitleTextBlock.Text = title;
                
                ClientButton.IsEnabled = false;
                ClientComboBox.ItemsSource = null;
                var client = new List<ClientQh>(){ qm.QueryClients(connection).Find(x => x.Client_ID == selectedSession.Client_ID) };
                ClientComboBox.ItemsSource = client;
                ClientComboBox.SelectedIndex = 0;
                ClientComboBox.IsEnabled = false;

                CarButton.IsEnabled = false;
                CarComboBox.ItemsSource = null;
                var car = new List<CarQh>() {qm.QueryCars(connection).Find(x => x.Car_ID == selectedSession.Car_ID)};
                CarComboBox.ItemsSource = car;
                CarComboBox.SelectedIndex = 0;
                CarComboBox.IsEnabled = false;

                StartButton.IsEnabled = false;
                StartComboBox.ItemsSource = null;
                var strt = new List<RentalQh>() { qm.QueryRentals(connection).Find(x => x.Rental_ID == selectedSession.Start_location_ID) };
                StartComboBox.ItemsSource = strt;
                StartComboBox.SelectedIndex = 0;
                StartComboBox.IsEnabled = false;

                EndButton.IsEnabled = false;
                EndComboBox.ItemsSource = null;
                var end_rental = new List<RentalQh>() { qm.QueryRentals(connection).Find(x => x.Rental_ID == selectedSession.End_location_ID) };
                EndComboBox.ItemsSource = end_rental;
                EndComboBox.SelectedIndex = 0;
                EndComboBox.IsEnabled = false;

                ManagerButton.IsEnabled = false;
                ManagerComboBox.ItemsSource = null;
                var manager = new List<ManagerQh>() { qm.QueryManagers(connection).Find(x => x.Manager_ID == selectedSession.Manager_ID) };
                ManagerComboBox.ItemsSource = manager;
                ManagerComboBox.SelectedIndex = 0;
                ManagerComboBox.IsEnabled = false;
            }
            else
            {
                var title = "Add session";
                Title = title;
                TitleTextBlock.Text = title;
                clients = qm.QueryClients(connection);
                ClientComboBox.ItemsSource = null;
                ClientComboBox.ItemsSource = clients;
            }

            
        }

        private void ClientButton_OnClick(object sender, RoutedEventArgs e)
        {
            selectedClient = (ClientQh) ClientComboBox.SelectedItem;
            if (selectedClient == null)
            {
                MessageBox.Show("Choose client to continue");
                return;
            }

            available_cars = qm.QueryAvailableCars(connection, selectedClient.Driving_experience);
            CarComboBox.IsEnabled = true;
            CarButton.IsEnabled = true;
            CarComboBox.ItemsSource = null;
            CarComboBox.ItemsSource = available_cars;
            ClientComboBox.IsEnabled = false;
            ClientButton.IsEnabled = false;
        }

        private void CarButton_OnClick(object sender, RoutedEventArgs e)
        {
            selectedCar = (CarQh)CarComboBox.SelectedItem;
            if (selectedCar == null)
            {
                MessageBox.Show("Choose car to continue");
                return;
            }

            StartComboBox.IsEnabled = true;
            StartButton.IsEnabled = true;
            StartComboBox.ItemsSource = null;
            StartComboBox.ItemsSource = qm.QueryRentals(connection);
            CarComboBox.IsEnabled = false;
            CarButton.IsEnabled = false;
        }

        private void StartButton_OnClick(object sender, RoutedEventArgs e)
        {
            start = (RentalQh)StartComboBox.SelectedItem;
            if (start == null)
            {
                MessageBox.Show("Choose rental to continue");
                return;
            }

            EndComboBox.IsEnabled = true;
            EndButton.IsEnabled = true;
            EndComboBox.ItemsSource = null;
            EndComboBox.ItemsSource = qm.QueryRentals(connection);
            StartComboBox.IsEnabled = false;
            StartButton.IsEnabled = false;
        }

        private void EndButton_OnClick(object sender, RoutedEventArgs e)
        {
            end = (RentalQh)EndComboBox.SelectedItem;
            if (end == null)
            {
                MessageBox.Show("Choose rental to continue");
                return;
            }

            ManagerComboBox.IsEnabled = true;
            ManagerButton.IsEnabled = true;
            ManagerComboBox.ItemsSource = null;
            ManagerComboBox.ItemsSource = qm.QueryManagers(connection);
            EndComboBox.IsEnabled = false;
            EndButton.IsEnabled = false;
        }

        private void ManagerButton_OnClick(object sender, RoutedEventArgs e)
        {
            selectedManager = (ManagerQh)ManagerComboBox.SelectedItem;
            if (selectedManager == null)
            {
                MessageBox.Show("Choose manager to continue");
                return;
            }

            ManagerComboBox.IsEnabled = false;
            ManagerButton.IsEnabled = false;

            qm.QueryAddSession(connection, selectedClient.Client_ID, selectedCar.Car_ID, start.Rental_ID, end.Rental_ID, selectedManager.Manager_ID);
            this.Close();
        }
    }
}
