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

                //TODO new reach specific, fix later
                cmd.CommandText = "SELECT * FROM reach where patientID=@pID";
                cmd.Prepare();

                using (MySqlDataReader rdr = cmd.ExecuteReader())
                {

                    while (rdr.Read())
                    {
                        ExerciseData exerciseData = new ExerciseData();
                        exerciseData.ExerciseName = "reach";
                        exerciseData.PatientID = rdr.GetInt32("patientID");
                        exerciseData.EmployeeID = rdr.GetInt32("employeeID");
                        exerciseData.SessionID = rdr.GetInt32("sessionID");

                        /*
                        exerciseData.MeasurementA = rdr.GetDouble("measurementA");
                        exerciseData.MeasurementB = rdr.GetDouble("measurementB");
                        exerciseData.MeasurementC = rdr.GetDouble("measurementC");
                        */

                        exerciseData.Hands = rdr.GetString("hands");
                        exerciseData.Angle = rdr.GetDouble("angle");
                        exerciseData.Date = rdr.GetDateTime("exerciseDate");

                        reportData.ExerciseDataList.Add(exerciseData);
                    }
                }

            }

            return reportData;

        }

        public void writeReachData(int pid, int eid, int sid, string hands, double angle, DateTime date)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO reach (patientID,employeeID,sessionID,hands,angle,exerciseDate) VALUES (@pid,@eid,@sid,@hands,@angle,@date)";
                cmd.Parameters.AddWithValue("@pid", pid);
                cmd.Parameters.AddWithValue("@eid", eid);
                cmd.Parameters.AddWithValue("@sid", sid);
                cmd.Parameters.AddWithValue("@hands", hands);
                cmd.Parameters.AddWithValue("@angle", angle);
                cmd.Parameters.AddWithValue("@date", date.ToString("yyyy/MM/dd"));
                cmd.ExecuteNonQuery();

            }
        }
    }
}
