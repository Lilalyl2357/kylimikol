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
    /// Логика взаимодействия для EditWindow1.xaml
    /// </summary>
    public partial class EditWindow1 : Window
    {
        private string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DrivingSchool;Integrated Security=True;Connect Timeout=30;Encrypt=False;";

        public EditWindow1()
        {
            InitializeComponent();
            LoadTeacherLastNames();
        }

        private void LoadTeacherLastNames()
        {
            string query = "SELECT DISTINCT [ФИО Студента] FROM Exam"; 

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            TeacherLastNameListBox.Items.Add(reader["ФИО Студента"].ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Произошла ошибка: " + ex.Message);
                }
            }
        }

        private void TeacherLastNameListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (TeacherLastNameListBox.SelectedItem != null)
            {
                string selectedLastName = TeacherLastNameListBox.SelectedItem.ToString();
                LoadData(selectedLastName);
            }
        }

        private void LoadData(string lastName)
        {
            string query = "SELECT [ФИО Студента], Дата, Время, Место, [ФИО Инструктора] FROM Exam WHERE [ФИО Студента] = @FioStud"; 

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@FioStud", lastName);
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            FioStud.Text = reader["ФИО Студента"].ToString();
                            DateClass.Text = reader["Дата"].ToString();
                            TimeClass.Text = reader["Время"].ToString();
                            RoomClass.Text = reader["Место"].ToString();
                            FioTeach.Text = reader["ФИО Инструктора"].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Произошла ошибка: " + ex.Message);
                }
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            UpdateData();
        }

        private void UpdateData()
        {
            string query = "UPDATE Exam SET [ФИО Студента] = @FioStud, Дата = @DateClass, Время = @TimeClass, Место = @RoomClass, [ФИО Инструктора] = @FioTeach WHERE [ФИО Студента] = @FioStud";

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
                        MessageBox.Show("Данные успешно обновлены!");
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Произошла ошибка: " + ex.Message);
                }
            }
            Close();
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
