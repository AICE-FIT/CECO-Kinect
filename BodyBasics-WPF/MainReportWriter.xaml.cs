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
    /// Interaction logic for MainReportWriter.xaml
    /// </summary>
    public partial class MainReportWriter : Window
    {
        public MainReportWriter()
        {
            InitializeComponent();
        }

        private void gnerateReport_Click(object sender, RoutedEventArgs e)
        {
           
            try
            {
                int pID = int.Parse(this.patientID.Text);
                ReportWindow rw = new ReportWindow(pID);
                rw.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please Enter A Valid ID");
            }

        }
    }
}
