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

    public partial class CarWindow_Car : Window
    {
        QueryMethods qm = new QueryMethods();
        SqlConnection connection = new SqlConnection();
        CarQh selectedCar = new CarQh();
        private string cpn;
        private string colour;
        ModelQh selectedModel = new ModelQh();

        public CarWindow_Car(SqlConnection conn, CarQh car)
        {
            InitializeComponent();
            connection = conn;
            selectedCar = car;

            if (selectedCar != null)
            {
                var title = "Show car";
                Title = title;
                TitleTextBlock.Text = title;
                CPNTextBox.Text = selectedCar.Car_Plate_Number;
                CPNTextBox.IsEnabled = false;
                CPNButton.IsEnabled = false;
                ColourComboBox.ItemsSource = null;
                ColourComboBox.ItemsSource = new List<string>() { selectedCar.Colour };
                ColourComboBox.SelectedIndex = 0;
                ColourComboBox.IsEnabled = false;
                ModelComboBox.ItemsSource = null;
                ModelComboBox.ItemsSource = new List<ModelQh>()
                    {qm.QueryModels(connection).Find(x => x.Model_ID == selectedCar.Model_ID)};
                ModelComboBox.SelectedIndex = 0;
                NumOfSessionsTextBlock.Text = $"Number of sessions, where this car was used: {qm.QueryNumOfSessions(connection, selectedCar.Car_ID)}";

            }
            else
            {
                var title = "Add car";
                Title = title;
                TitleTextBlock.Text = title;
                ColourComboBox.ItemsSource = null;
                ColourComboBox.ItemsSource = new List<string>()
                {
                    "Black",
                    "Green",
                    "White",
                    "Blue",
                    "Red",
                    "Yellow",
                    "Silver",
                    "Purple",
                    "Brown",
                    "Grey",
                    "Orange",
                    "Cyan",
                    "Graphite",
                    "Mixed colour"
                };
            }
        }

        private void CPNButton_OnClick(object sender, RoutedEventArgs e)
        {
            cpn = CPNTextBox.Text;
            if (String.IsNullOrEmpty(cpn))
            {
                MessageBox.Show("Enter Car Plate Number to continue");
                return;
            }

            ColourComboBox.IsEnabled = true;
            ColourButton.IsEnabled = true;
            CPNTextBox.IsEnabled = false;
            CPNButton.IsEnabled = false;
        }

        private void ColourButton_OnClick(object sender, RoutedEventArgs e)
        {
            colour = (string) ColourComboBox.SelectedItem;
            if (colour == null)
            {
                MessageBox.Show("Choose colour to continue");
                return;
            }

            ModelComboBox.IsEnabled = true;
            ModelButton.IsEnabled = true;
            ModelComboBox.ItemsSource = null;
            ModelComboBox.ItemsSource = qm.QueryModels(connection);
            ColourComboBox.IsEnabled = false;
            ColourButton.IsEnabled = false;
            
        }

        private void ModelButton_OnClick(object sender, RoutedEventArgs e)
        {
            selectedModel = (ModelQh) ModelComboBox.SelectedItem;
            if (selectedModel == null)
            {
                MessageBox.Show("Choose model to continue");
                return;
            }

            ModelComboBox.IsEnabled = false;
            ModelButton.IsEnabled = false;
            qm.QueryAddCar(connection, cpn, colour, selectedModel.Model_ID);
            this.Close();
        }
    }
}
