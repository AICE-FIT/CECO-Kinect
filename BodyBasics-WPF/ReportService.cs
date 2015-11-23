using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Microsoft.Samples.Kinect.BodyBasics
{
    public class ReportService
    {
   
        private static readonly string connectionString;

        static ReportService()
        {
            string returnValue = null;

            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["md_connection"];


            if (settings != null)
                returnValue = settings.ConnectionString;

            connectionString = returnValue;
        } 

     
        public ReportData getPatientReport(int patientID)
        {
            ReportData reportData = new ReportData();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT * FROM patient where patientID=@pID";
                cmd.Prepare();
                cmd.Parameters.Add("@pID", MySqlDbType.Int32).Value = patientID;

                using (MySqlDataReader rdr = cmd.ExecuteReader())
                {

                    while (rdr.Read())
                    {
                        reportData.PatientData.PatientID = rdr.GetInt32("patientID");
                        reportData.PatientData.FirstName = rdr.GetString("firstName");
                        reportData.PatientData.LastName = rdr.GetString("lastName");
                        reportData.PatientData.DateAdmitted = rdr.GetDateTime("dateAdmitted");
                    }
                }

                cmd.CommandText = "SELECT * FROM exercise where patientID=@pID";
                cmd.Prepare();
                
                using (MySqlDataReader rdr = cmd.ExecuteReader())
                {

                    while (rdr.Read())
                    {
                        ExerciseData exerciseData = new ExerciseData();
                        exerciseData.ExerciseName = rdr.GetString("exerciseName");
                        exerciseData.PatientID = rdr.GetInt32("patientID");
                        exerciseData.MeasurementA = rdr.GetDouble("measurementA");
                        exerciseData.MeasurementB = rdr.GetDouble("measurementB");
                        exerciseData.MeasurementC = rdr.GetDouble("measurementC");
                        reportData.ExerciseDataList.Add(exerciseData);
                    }
                }

            }

            return reportData;

        }
    }
}
