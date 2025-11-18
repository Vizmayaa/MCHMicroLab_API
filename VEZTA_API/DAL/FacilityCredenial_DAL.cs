using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using VEZTA.Models;


namespace VEZTA.DAL
{
    public class FacilityCredenial_DAL
    {
        public List<FacilityMain> GetAllFaclityMain()
        {
            string APIKey = "";
            User_DAL userDAL = new User_DAL();
           // Int32 intUserID = userDAL.GetUserIDWithToken(APIKey);

            List<FacilityMain> insuranceList = new List<FacilityMain>();
            SqlConnection connection = ADO.GetConnection();
            
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_FACILITY_CREDENTIALS";
                cmd.Parameters.AddWithValue("Action", 0);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tbl = new DataTable();
                da.Fill(tbl);

                foreach (DataRow dr in tbl.Rows)
                {
                    insuranceList.Add(new FacilityMain
                    {
                        ID = Convert.ToInt32(dr["ID"]),
                  
                        FacilityLicense = dr["FacilityLicense"] != DBNull.Value ? Convert.ToString(dr["FacilityLicense"]) : string.Empty,                 
                        FacilityName = dr["FacilityName"] != DBNull.Value ? Convert.ToString(dr["FacilityName"]) : string.Empty,
                        PostOfficeID = dr["PostOfficeID"] != DBNull.Value ? Convert.ToInt32(dr["PostOfficeID"]) : (int?)null,
                        Postoffice = dr["Postoffice"] != DBNull.Value ? Convert.ToString(dr["Postoffice"]) : string.Empty,
                        LoginName = dr["LoginName"] != DBNull.Value ? Convert.ToString(dr["LoginName"]) : string.Empty,
                        Password = dr["Password"] != DBNull.Value ? Convert.ToString(dr["Password"]) : string.Empty,
                        LastModified_Time = dr["LastModifiedTime"] != DBNull.Value ? Convert.ToDateTime(dr["LastModifiedTime"]) :(DateTime?)null

                        //IsInactive = Convert.ToBoolean(dr["IsInactive"]),
                        //Flag = Convert.ToInt32(dr["Flag"])

                    });
                }
                connection.Close();
           
            return insuranceList;
        }

        public Int32 Update(FacilityCredentials facilityCredentials, Int32 userID)
        {
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_FACILITY_CREDENTIALS";

                    // Add parameters
                    cmd.Parameters.AddWithValue("@Action", 1); // Action 1 for update/insert
                    cmd.Parameters.AddWithValue("@FacilityID", facilityCredentials.FacilityID);
                    cmd.Parameters.AddWithValue("@PostOfficeID", facilityCredentials.PostOfficeID);
                    cmd.Parameters.AddWithValue("@LoginName", facilityCredentials.LoginName ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Password", facilityCredentials.Password ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("UserID", userID);

                    // connection.Open();
                    cmd.ExecuteNonQuery();

                    // Since your stored procedure does not return a value directly, you may need another query to retrieve relevant data.
                    // Example: If you want to get the ID of the last updated record in TB_FACILITY_CREDENTIALS, you can use the following:

                    SqlCommand cmdGetMaxID = new SqlCommand();
                    cmdGetMaxID.Connection = connection;
                    cmdGetMaxID.CommandType = CommandType.Text;
                    cmdGetMaxID.CommandText = "SELECT MAX(ID) FROM TB_FACILITY_CREDENTIALS";

                    // Execute scalar to get the result
                    Int32 maxID = Convert.ToInt32(cmdGetMaxID.ExecuteScalar());

                    return maxID;
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately
                Console.WriteLine("Error in Update method: " + ex.Message);
                throw; // Re-throw exception to propagate it up the call stack
            }
        }

    }
}