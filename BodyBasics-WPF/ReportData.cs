using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Samples.Kinect.BodyBasics
{
    public class ReportData
    {
        private PatientData patientData = new PatientData();
        private List<ExerciseData> exerciseDataList = new List<ExerciseData>();

        public PatientData PatientData
        {
            get { return patientData; }

            set { patientData = value; }
        }
    

        public List<ExerciseData> ExerciseDataList
        {
             get { return exerciseDataList; }

            set { exerciseDataList = value; }
        }
    } 
}
