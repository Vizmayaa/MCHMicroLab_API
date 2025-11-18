using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using VEZTA.Models;

namespace VEZTA.DAL
{
    public class PasswordLog_DAL
    {
        public Int32 Insert(PasswordLog passwordLog, Int32 userID)
        {
            try
            {
                SqlConnection connection = ADO.GetConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_INSERT_PASSWORD_LOG";
                cmd.Parameters.AddWithValue("ACTION", 0);

                cmd.Parameters.AddWithValue("PresentPassword", passwordLog.PresentPassword);
                cmd.Parameters.AddWithValue("NewPassword", passwordLog.NewPassword);
                cmd.Parameters.AddWithValue("@ChangeSource", passwordLog.ModifiedFrom);

                cmd.Parameters.AddWithValue("UserID", userID);

                 cmd.ExecuteNonQuery();

                SqlCommand cmd1 = new SqlCommand();
                cmd1.Connection = connection;
                cmd1.CommandType = CommandType.Text;
                cmd1.CommandText = "SELECT MAX(ID) FROM TB_USER_PASSWORD_LOG";
                Int32 clinicianID = Convert.ToInt32(cmd1.ExecuteScalar());

                return clinicianID;
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
   
    }
}