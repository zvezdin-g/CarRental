using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
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
  
    public partial class RentalWindow : Window
    {
        QueryMethods qm = new QueryMethods();
        List<RentalQh> rentalList = new List<RentalQh>();
        SqlConnection connection = new SqlConnection();
        public RentalWindow(SqlConnection conn)
        {
            connection = conn;
            InitializeComponent();
            UpdateRentals();
        }
        private void UpdateRentals()
        {
            rentalList = qm.QueryRentals(connection);
            RentalsDataGrid.ItemsSource = null;
            RentalsDataGrid.ItemsSource = rentalList;
        }
    }
}
