using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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
using System.Windows.Media.Animation;

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow5.xaml
    /// </summary>
    public partial class MainWindow5 : Window
    {
        string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DrivingSchool;Integrated Security=True;Connect Timeout=30;Encrypt=False;";
        SqlDataAdapter dataAdapter;
        DataTable dataTable;
        public MainWindow5()
        {

            InitializeComponent();

            dataAdapter = new SqlDataAdapter("SELECT [ФИО Студента], Дата, Время, Место, [ФИО Инструктора] FROM Exam ", connectionString);
            dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            dataGrid1.ItemsSource = dataTable.DefaultView;
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            // Закрытие текущего окна
            this.Close();
        }

        private void YrButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

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
          
        }
    }
}
