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

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для Instruct.xaml
    /// </summary>
    public partial class Instruct : Window
    {
        string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DrivingSchool;Integrated Security=True;Connect Timeout=30;Encrypt=False;";
        SqlDataAdapter dataAdapter;
        DataTable dataTable;
        public Instruct()
        {
            InitializeComponent();

            dataAdapter = new SqlDataAdapter("SELECT Фамилия,Имя,Отчество,Номер FROM Instructor", connectionString);
            dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            dataGrid.ItemsSource = dataTable.DefaultView;
        }

        private void LoadData()
        {
            string query = "SELECT Фамилия,Имя,Отчество,Номер FROM Instructor"; // Замените на ваш SQL-запрос для получения данных

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
                string fio = selectedRow["Фамилия"].ToString();

                // Формируем SQL-запрос DELETE
                string query = $"DELETE FROM Instructor WHERE Фамилия = N'{fio}'";

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

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddInstruct addInstruct = new AddInstruct();
            addInstruct.Show();
        }
        private void RepitButton_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            // Закрытие текущего окна
            this.Close();
        }
    }
}
