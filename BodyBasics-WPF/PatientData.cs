using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Samples.Kinect.BodyBasics
{
    public class PatientData
    {
        private int patientID;
        private string firstName;
        private string lastName;
        private DateTime dateAdmitted;

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

        public string FirstName
        {
            get { return firstName; }

            set
            {
                if (value != firstName)
                {
                    firstName = value;

                }
            }
        }

        public string LastName
        {
            get { return lastName; }

            set
            {
                if (value != lastName)
                {
                    lastName = value;

                }
            }
        }

        public DateTime DateAdmitted
        {
            get { return dateAdmitted; }

            set
            {
                if (value != dateAdmitted)
                {
                    dateAdmitted = value;

                }
            }
        }

    }
}
