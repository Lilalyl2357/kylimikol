using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using System.Data.Entity;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow4 : Window
    {
        string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DrivingSchool;Integrated Security=True;Connect Timeout=30;Encrypt=False;";
        SqlDataAdapter dataAdapter;
        DataTable dataTable;
        public MainWindow4()
        {
            InitializeComponent();


        }
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            dataAdapter = new SqlDataAdapter($"SELECT * FROM Users WHERE Login = '{UsernameTextBox.Text}' AND Password = '{PasswordTextBox.Password}'", connectionString);
            dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            if (dataTable.Rows.Count > 0) // если такая запись существует       
            {
                MessageBox.Show("Авторизация успешна. Добро пожаловть!"); // говорим, что авторизовался
                MainWindow2 mainWindow2 = new MainWindow2(); mainWindow2.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Пользователь не найден, проверьте Логин и Пароль"); // говорим, что не авторизовался         }

            }
        }
        private void BekButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            this.Close();
        }

        
        
    }
}