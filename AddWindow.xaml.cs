using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
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
    /// Логика взаимодействия для AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DrivingSchool;Integrated Security=True;Connect Timeout=30;Encrypt=False;";
        public AddWindow()
        {
            InitializeComponent();
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {

            string query = "INSERT INTO Class ([ФИО Студента], Дата, Время, Место, [ФИО Инструктора]) " +
                           "VALUES (@FioStud, @DateClass, @TimeClass, @RoomClass, @FioTeach)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@FioStud", FioStud.Text);
                        command.Parameters.AddWithValue("@DateClass", DateClass.Text);
                        command.Parameters.AddWithValue("@TimeClass", TimeClass.Text);
                        command.Parameters.AddWithValue("@RoomClass", RoomClass.Text);
                        command.Parameters.AddWithValue("@FioTeach", FioTeach.Text);

                        command.ExecuteNonQuery();
                    }
                    MessageBox.Show("Данные успешно добавлены!");
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Произошла ошибка: " + ex.Message);
                }
            }
            Close();
        }
    }
}
