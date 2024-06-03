using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для AddInstruct.xaml
    /// </summary>
    public partial class AddInstruct : Window
    {
        public AddInstruct()
        {
            InitializeComponent();
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DrivingSchool;Integrated Security=True;Connect Timeout=30;Encrypt=False;";
            string query = "INSERT INTO Instructor (Фамилия, Имя, Отчество, Номер) VALUES (@SnameStud, @NameClass, @MnameClass, @NumClass)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@SnameStud", SnameStud.Text);
                command.Parameters.AddWithValue("@NameClass", NameClass.Text);
                command.Parameters.AddWithValue("@MnameClass", MnameClass.Text);
                command.Parameters.AddWithValue("@NumClass", NumClass.Text);

                try
                {
                    connection.Open();
                    int result = command.ExecuteNonQuery();

                    if (result < 0)
                        MessageBox.Show("Ошибка при добавлении данных в БД!");
                    else
                        MessageBox.Show("Данные успешно добавлены!");
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
        }
    }
}
