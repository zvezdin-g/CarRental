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
    public partial class ClientWindow_Add : Window
    {
        QueryMethods qm = new QueryMethods();
        SqlConnection connection = new SqlConnection();
        private string ln;
        private string fn;
        private DateTime dob_date;
        private DateTime doi_date;
        private int insurance;
        private string phone;
        private string licence;
        public ClientWindow_Add(SqlConnection conn)
        {
            InitializeComponent();
            connection = conn;
            var title = "Add client";
            Title = title;
            TitleTextBlock.Text = title;
            InsuranceComboBox.ItemsSource = null;
            InsuranceComboBox.ItemsSource = new List<int>()
            {
                0,
                1,
                2,
                3,
                4,
                5
            };
        }

        private void DOBButton_OnClick(object sender, RoutedEventArgs e)
        {
            var picker = DOB.SelectedDate;
            if (picker == null)
            {
                MessageBox.Show("Choose date to continue");
                return;
            }

            dob_date = (DateTime) picker;

            DOB.IsEnabled = false;
            DOBButton.IsEnabled = false;

            DOI.IsEnabled = true;
            DOIButton.IsEnabled = true;
        }
        private void DOIButton_OnClick(object sender, RoutedEventArgs e)
        {
            var picker = DOI.SelectedDate;
            if (picker == null)
            {
                MessageBox.Show("Choose date to continue");
                return;
            }

            doi_date = (DateTime) picker;

            DOI.IsEnabled = false;
            DOIButton.IsEnabled = false;

            InsuranceComboBox.IsEnabled = true;
            InsuranceButton.IsEnabled = true;
        }
        private void LNButton_OnClick(object sender, RoutedEventArgs e)
        {
            ln = LNTextBox.Text;
            if (String.IsNullOrEmpty(ln))
            {
                MessageBox.Show("Enter last name to continue");
                return;
            }

            LNTextBox.IsEnabled = false;
            LNButton.IsEnabled = false;
            FNTextBox.IsEnabled = true;
            FNButton.IsEnabled = true;

        }

        private void FNButton_OnClick(object sender, RoutedEventArgs e)
        {
            fn = FNTextBox.Text;
            if (String.IsNullOrEmpty(fn))
            {
                MessageBox.Show("Enter first name to continue");
                return;
            }

            FNTextBox.IsEnabled = false;
            FNButton.IsEnabled = false;
            DOB.IsEnabled = true;
            DOBButton.IsEnabled = true;
        }

        private void InsuranceButton_OnClick(object sender, RoutedEventArgs e)
        {
            var ins = (object) InsuranceComboBox.SelectedItem;
            if (ins == null)
            {
                MessageBox.Show("Choose model to continue");
                return;
            }

            insurance = (int) ins;

            InsuranceComboBox.IsEnabled = false;
            InsuranceButton.IsEnabled = false;

            PhoneTextBox.IsEnabled = true;
            PhoneButton.IsEnabled = true;
        }

        private void PhoneButton_OnClick(object sender, RoutedEventArgs e)
        {
            phone = PhoneTextBox.Text;
            if (String.IsNullOrEmpty(phone))
            {
                MessageBox.Show("Enter phone to continue");
                return;
            }

            PhoneTextBox.IsEnabled = false;
            PhoneButton.IsEnabled = false;
            LicenceTextBox.IsEnabled = true;
            LicenceButton.IsEnabled = true;
        }

        private void LicenceButton_OnClick(object sender, RoutedEventArgs e)
        {
            licence = LicenceTextBox.Text;
            if (String.IsNullOrEmpty(licence))
            {
                MessageBox.Show("Enter licence to continue");
                return;
            }

            LicenceTextBox.IsEnabled = false;
            LicenceButton.IsEnabled = false;

            qm.QueryAddClient(connection, ln, fn, dob_date, doi_date, insurance, phone, licence);
            this.Close();
        }
    }
}
