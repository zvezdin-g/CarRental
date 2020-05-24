using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using RentalCore.Utils;
using System.Data.SqlClient;

namespace RentalGUI
{
    public partial class MainWindow : Window
    {
        QueryMethods qm = new QueryMethods();
        List<SessionQh> currentSessionsList = new List<SessionQh>();
        List<SessionQh> pastOrdersList = new List<SessionQh>();
        List<FinancesQh> financesList = new List<FinancesQh>();
        SqlConnection conn = DbUtils.GetDBConnection();
        public MainWindow()
        {
            InitializeComponent();
            try
            {
                conn.Open();
                UpdateAll();
            }
            catch (Exception e)
            {
                MessageBox.Show($"Error: {e}");
            }
        }
        private void UpdateCurrentSessions()
        {
            currentSessionsList = qm.QueryCurrentSessions(conn);
            CurrentSessionsDataGrid.ItemsSource = null;
            CurrentSessionsDataGrid.ItemsSource = currentSessionsList;
        }

        private void UpdatePastSessions()
        {
            pastOrdersList = qm.QueryPastSessions(conn);
            PastSessionsDataGrid.ItemsSource = null;
            PastSessionsDataGrid.ItemsSource = pastOrdersList;
        }

        private void UpdateFinances()
        {
            financesList = qm.QueryFinances(conn);
            FinancesDataGrid.ItemsSource = null;
            FinancesDataGrid.ItemsSource = financesList;
        }
        private void UpdateAll()
        {
            UpdateCurrentSessions();
            UpdatePastSessions();
            UpdateFinances();
        }
        private void ConfirmDatesButton_OnClick(object sender, RoutedEventArgs e)
        {
            var start = StartDatePicker.SelectedDate;
            var end = EndDatePicker.SelectedDate;
            if (start != null && end != null)
            {
                start = Convert.ToDateTime(start);
                end = Convert.ToDateTime(end).AddHours(23).AddMinutes(59);
                if (start <= end)
                {
                    var datedOrders =
                        pastOrdersList.FindAll(item => item.Start_datetime >= start && item.End_datetime
                                                       <= end);
                    PastSessionsDataGrid.ItemsSource = null;
                    PastSessionsDataGrid.ItemsSource = datedOrders;
                }
                else
                {
                    MessageBox.Show("Start should be earlier than end!");
                }
            }
            else
            {
                MessageBox.Show("Choose dates!");
            }
        }
        private void FConfirmDatesButton_OnClick(object sender, RoutedEventArgs e)
        {
            var start = FStartDatePicker.SelectedDate;
            var end = FEndDatePicker.SelectedDate;
            if (start != null && end != null)
            {
                start = Convert.ToDateTime(start);
                end = Convert.ToDateTime(end).AddHours(23).AddMinutes(59);
                if (start <= end)
                {
                    var datedFinances =
                        financesList.FindAll(item => item.DateTime >= start && item.DateTime
                                                       <= end);
                    FinancesDataGrid.ItemsSource = null;
                    FinancesDataGrid.ItemsSource = datedFinances;
                }
                else
                {
                    MessageBox.Show("Start should be earlier than end!");
                }
            }
            else
            {
                MessageBox.Show("Choose dates!");
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

        private void RentalButton_OnClick(object sender, RoutedEventArgs e)
        {
            var rw = new RentalWindow(conn);
            rw.ShowDialog();

        }

        private void CloseSessionButton_OnClick(object sender, RoutedEventArgs e)
        {
            SessionQh selectedSession = (SessionQh)CurrentSessionsDataGrid.SelectedItem;
            if (selectedSession == null)
            {
                MessageBox.Show("Choose session to close");
                return;
            }
            var closeWindow = new MainWindow_CloseSession(conn, selectedSession);
            closeWindow.ShowDialog();
            UpdateAll();
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

        private void ShowSessionButton_OnClick(object sender, RoutedEventArgs e)
        {
            SessionQh selectedSession = (SessionQh)CurrentSessionsDataGrid.SelectedItem;
            if (selectedSession == null)
            {
                MessageBox.Show("Choose session to show");
                return;
            }
            var show = new MainWindow_Session(conn, selectedSession);
            show.ShowDialog();
            UpdateAll();
        }
        private void AddSessionButton_OnClick(object sender, RoutedEventArgs e)
        {
            var add = new MainWindow_Session(conn, null);
            add.ShowDialog();
            UpdateAll();
        }
    }
}
