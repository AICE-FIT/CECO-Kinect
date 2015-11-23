using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Samples.Kinect.BodyBasics
{
    public class ExerciseData
    {
        private string exerciseName;
        private int patientID;
        private double measurementA;
        private double measurementB;
        private double measurementC;

        public string ExerciseName
        {
            get { return exerciseName; }

            set
            {
                if (value != exerciseName)
                {
                    exerciseName = value;

                }
            }
        }

        public int PatientID
        {
            get { return patientID; }

            set
            {
                if (value != patientID)
                {
                    patientID = value;

                }
            }
        }

        public double MeasurementA
        {
            get { return measurementA; }

            set
            {
                if (value != measurementA)
                {
                    measurementA = value;

                }
            }
        }

        public double MeasurementB
        {
            get { return measurementB; }

            set
            {
                if (value != measurementB)
                {
                    measurementB = value;

                }
            }
        }

        public double MeasurementC
        {
            get { return measurementC; }

            set
            {
                if (value != measurementC)
                {
                    measurementC = value;

                }
            }
        }




    }
}
