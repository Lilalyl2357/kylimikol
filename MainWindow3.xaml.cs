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
using System.Data.SqlClient;
using System.Data;

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow3.xaml
    /// </summary>
    public partial class MainWindow3 : Window
    {
        string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DrivingSchool;Integrated Security=True;Connect Timeout=30;Encrypt=False;";
        SqlDataAdapter dataAdapter;
        DataTable dataTable;
        public MainWindow3()
        {

            InitializeComponent();

            dataAdapter = new SqlDataAdapter("SELECT [ФИО Студента], Дата, Время, Место, [ФИО Инструктора] FROM Exam ", connectionString);
            dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            dataGrid.ItemsSource = dataTable.DefaultView;
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            // Закрытие текущего окна
            this.Close();
        }

        private void YrButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow2 mainWindow2 = new MainWindow2();
            mainWindow2.Show();

            // Закрытие текущего окна
            this.Close();
        }
        private void EditButton2_Click(object sender, RoutedEventArgs e)
        {
            EditWindow1 editWindow = new EditWindow1();
            editWindow.Show();
        }

        private void AddButton2_Click(object sender, RoutedEventArgs e)
        {
            AddWindow1 addWindow = new AddWindow1();
            addWindow.Show();
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
        private void RepitButton_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            string query = "SELECT [ФИО Студента], Дата, Время, Место, [ФИО Инструктора] FROM Exam"; 

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    dataGrid.ItemsSource = dataTable.DefaultView;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Произошла ошибка: " + ex.Message);
                }
            }
        }
        private void DellButton_Click(object sender, RoutedEventArgs e)
        {
            DataRowView selectedRow = (DataRowView)dataGrid.SelectedItem;

            // Проверяем, выбрана ли какая-либо строка
            if (selectedRow != null)
            {
                // Получаем значение ключевого столбца (предположим, это ФИО_УЧ)
                string fio = selectedRow["ФИО Студента"].ToString();

                // Формируем SQL-запрос DELETE
                string query = $"DELETE FROM Exam WHERE [ФИО Студента] = N'{fio}'";

                // Устанавливаем соединение с базой данных
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        // Открываем соединение
                        connection.Open();

                        // Создаем команду для выполнения SQL-запроса
                        SqlCommand command = new SqlCommand(query, connection);

                        // Выполняем SQL-запрос
                        int rowsAffected = command.ExecuteNonQuery();

                        // Если удаление прошло успешно, обновляем данные в DataGrid
                        if (rowsAffected > 0)
                        {
                            LoadData(); // Перезагрузка данных в DataGrid
                        }
                        else
                        {
                            MessageBox.Show("Не удалось удалить запись.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Произошла ошибка при удалении записи: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите запись для удаления.");
            }
        }

        private void LivButton_Click(Object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
    }
}
