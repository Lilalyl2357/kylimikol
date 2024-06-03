using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using System.Data.Entity;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Media.Animation;


namespace WpfApp2
{
    public partial class MainWindow : Window
    {
        
        string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DrivingSchool;Integrated Security=True;Connect Timeout=30;Encrypt=False;";
        SqlDataAdapter dataAdapter;
        DataTable dataTable;
        public MainWindow()
        {

            InitializeComponent();

            dataAdapter = new SqlDataAdapter("SELECT [ФИО Студента], Дата, Время, Место, [ФИО Инструктора] FROM Class ", connectionString);
            dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            dataGrid.ItemsSource = dataTable.DefaultView;
        }


        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            // Закрытие текущего окна
            this.Close();
        }

        private void GaiButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow5 mainWindow5 = new MainWindow5();
            mainWindow5.Show();

            this.Close();
 
        }

        private void AutoButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow4 mainWindow4 = new MainWindow4();
            mainWindow4.Show();

            // Закрытие текущего окна
            this.Close();
        }

       private void ScunButton_Click(object sender, RoutedEventArgs e)
        {
            string query = ScanTextBox.Text;

            // Очистка предыдущих результатов поиска
            dataTable.DefaultView.RowFilter = string.Empty;

            // Фильтрация данных по запросу пользователя
            if (!string.IsNullOrEmpty(query))
            {
                dataTable.DefaultView.RowFilter = $"[ФИО Студента] LIKE '%{query}%' OR [ФИО Инструктора] LIKE '%{query}%'";
            }
            
            
            //{dataTable.DefaultView.RowFilter = $"[ФИО Инструктора] LIKE '%{query}%'";}
        }
    }


}
