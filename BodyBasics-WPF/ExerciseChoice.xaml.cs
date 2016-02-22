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

namespace Microsoft.Samples.Kinect.BodyBasics
{
    /// <summary>
    /// Interaction logic for ExerciseChoice.xaml
    /// </summary>
    public partial class ExerciseChoice : Window
    {
        public ExerciseChoice()
        {
            InitializeComponent();
        }

        public void GoToReach(object sender, RoutedEventArgs e)
        {
            
            ReachExercise re = new ReachExercise();
            re.Show();
        }

        public void ToDoAlert(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("We haven't implemented this feature yet!");
        }

        /*    // This will 
        public void GoToStanding(object sender, RoutedEventArgs e)
        {
            StandingExercise se = new StandingExercise();
            se.Show();
        }
        */

        public void GoToTest(object sender, RoutedEventArgs e)
        {
            MainWindow test = new MainWindow();
            test.Show();
        }
    }
}
