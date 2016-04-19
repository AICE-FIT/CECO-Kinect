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
using MySql.Data.MySqlClient;
using System.Configuration;

namespace Microsoft.Samples.Kinect.BodyBasics
{

	
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private bool login = false;
        private string connectionString = "";

        public Window1()
        {
            InitializeComponent();
            string returnValue = null;

            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["md_connection"];


            if (settings != null)
                returnValue = settings.ConnectionString;

            connectionString = returnValue;
        }

        

		public void DoLogin() {
			string username = "";
			string password = ""; //add input logic here. Messagebox should work


			
			// Schema as follows. Make it in your DB:
			//		User
			//			- id
			//			- name
			//			- password
			//			- hint
			using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT * FROM user where password=@pass AND name=@user";
                cmd.Prepare();
                cmd.Parameters.Add("@pass", MySqlDbType.Text).Value = password;
				cmd.Parameters.Add("@user", MySqlDbType.Text).Value = username;

                using (MySqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
						login = true; // will be true if theres anything to read
                    }
                }
            }
		}
		
		public void RegisterUser() {
			// Add input data here
			string id, name, pass, hint;
			
			using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO user (id,name,password,hint) VALUES (@id,@name,@pass,@hint,)";
                cmd.Parameters.AddWithValue("@pid", id);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@pass", pass);
                cmd.Parameters.AddWithValue("@hint", hint);			
                cmd.ExecuteNonQuery();

            }
		}
		
        public void GoToExercises(object sender, RoutedEventArgs e)
        {
			DoLogin();
			if(login) {
				ExerciseChoice ec = new ExerciseChoice();
				ec.Show();
			} else {
				MessageBox.Show("Username or Password is incorrect");
				// add hint here if we want to?
			}
        }

        public void ToDoAlert(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("We haven't implemented this feature yet!");
        }

        public void GoToReport(object sender, RoutedEventArgs e)
        {
            MainReportWriter mrw = new MainReportWriter();
            mrw.Show();
        }
    }
}

