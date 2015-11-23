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
    /// Interaction logic for ReportWindow.xaml
    /// </summary>
    public partial class ReportWindow : Window
    {
        private List<ExerciseData> exerciseDataList;
        private PatientData patientData;
        

        public ReportWindow(int pID)
        {
            this.DataContext = this;
            InitializeComponent();

            ReportService rs = new ReportService();

            ReportData report = rs.getPatientReport(pID);

            exerciseDataList = report.ExerciseDataList;
            patientData = report.PatientData;

           patientNameTextBox.DataContext = PatientData;
           patientIDTextBox.DataContext = PatientData;
           exerciseListBox.ItemsSource = ExerciseDataList;

            
        }

        public List<ExerciseData> ExerciseDataList
        {
            get { return exerciseDataList; }

            set { exerciseDataList = value; }
        }

        public PatientData PatientData
        {
            get { return patientData; }

            set { patientData= value; }
        }

    }
}
