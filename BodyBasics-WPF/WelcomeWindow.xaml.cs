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

        int id;

        public Window1(int id)
        {
            InitializeComponent();
            this.id = id;
        }
        			
        public void GoToExercises(object sender, RoutedEventArgs e)
        {
			
				ExerciseChoice ec = new ExerciseChoice(this.id);
				ec.Show();
			
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

