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
        private int employeeID;
        private int sessionID;

        //TODO specific to reach, will abstract later
        private string hands;
        private double angle;
        private DateTime date;

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

        public int EmployeeID
        {
            get { return employeeID; }

            set
            {
                if (value != employeeID)
                {
                    employeeID = value;

                }
            }
        }

        public int SessionID
        {
            get { return sessionID; }

            set
            {
                if (value != sessionID)
                {
                    sessionID = value;

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

        //TODO specific to reach, will abstract later
        public string Hands
        {
            get { return hands; }

            set
            {
                if (value != hands)
                {
                    hands = value;

                }
            }
        }

        public double Angle
        {
            get { return angle; }

            set
            {
                if (value != angle)
                {
                    angle = value;

                }
            }
        }

        public DateTime Date
        {
            get { return date; }

            set
            {
                if (value != date)
                {
                    date = value;

                }
            }
        }


    }
}
