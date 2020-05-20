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

    public partial class MainWindow_CloseSession : Window
    {
        QueryMethods qm = new QueryMethods();
        List<FineQh> finesList = new List<FineQh>();
        private SqlConnection connection = new SqlConnection();
        SessionQh _session = new SessionQh();
        public MainWindow_CloseSession(SqlConnection conn, SessionQh session)
        {
            connection = conn;
            _session = session;
            InitializeComponent();
            finesList = qm.QueryFines(connection);
            var descs = new List<string>();
            foreach (var fine in finesList)
            {
                descs.Add(fine.Fine_description);
            }
            FinesComboBox.ItemsSource = descs;
        }

        private void CloseSessionButton_OnClick(object sender, RoutedEventArgs e)
        {
            var selectedItem = (string)FinesComboBox.SelectedItem;
            if (selectedItem != null)
            {
                var fine = finesList.Find(x => x.Fine_description == selectedItem);
                qm.QueryCloseOrder(connection, _session, fine);
            }
            else
            {
                qm.QueryCloseOrder(connection, _session, null);
            }
            this.Close();
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
