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
using System.Configuration;
using MySql.Data.MySqlClient;
using DevOne.Security.Cryptography.BCrypt;

namespace Microsoft.Samples.Kinect.BodyBasics
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
    
        private string connectionString = "";

        public Login()
        {
            InitializeComponent();
            string returnValue = null;

            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["md_connection"];


            if (settings != null)
                returnValue = settings.ConnectionString;

            connectionString = returnValue;

        }

        public void OnLogin(object sender, RoutedEventArgs e)
        {
            string inputUsername = this.loginUsername.Text;
            string inputPassword = this.loginPasswordBox.Password;
            string password = "";
            int empolyeeID = -1;




            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT password, employeeID FROM employee where username=@user";
                cmd.Prepare();
                cmd.Parameters.Add("@user", MySqlDbType.Text).Value = inputUsername;

                using (MySqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        password = rdr.GetString("password");
                        empolyeeID = rdr.GetInt32("employeeID");

                    }
                }

                if (password != "" && BCryptHelper.CheckPassword(inputPassword, password))
                {
                    Window1 welcome = new Window1(empolyeeID);
                    welcome.Show();
                    this.Close();
                }
                else
                {
                    this.loginPasswordBox.Clear();
                    MessageBox.Show("Username or Password is Incorrect");
                }
            }

            
        }

        public void RegisterUser(object sender, RoutedEventArgs e)
        {
            // Add input data here
            string name = this.registerUsername.Text;
            string pass = this.registerPasswordBox.Password;
            string hint = this.registerHint.Text;
            string salt = BCryptHelper.GenerateSalt();
            string hash = BCryptHelper.HashPassword(pass, salt);
            

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO employee (username,password,hint, salt) VALUES (@name,@pass,@hint, @salt)";
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@pass", hash);
                cmd.Parameters.AddWithValue("@hint", hint);
                cmd.Parameters.AddWithValue("@salt", salt);
                cmd.ExecuteNonQuery();

            }

            this.registerUsername.Clear();
            this.registerPasswordBox.Clear();
            this.registerHint.Clear();
            MessageBox.Show("User was registered!");
        }
    }
}
