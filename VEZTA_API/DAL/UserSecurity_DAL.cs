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
    public class UserSecurity_DAL
    {

        public UserSecurityResponse GetAllUserSecurity(Int32 intUserID)
        {
            UserSecurityResponse response = new UserSecurityResponse();
            response.data = new List<UserSecurity>();
            response.Login = new List<UserSecurityLogin>();

              SqlConnection connection = ADO.GetConnection();
           
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_USER_SECURITY";
                cmd.Parameters.AddWithValue("ACTION", 0);
              

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                // SecuritySettings data
                DataTable tbl = ds.Tables[0];
                foreach (DataRow dr in tbl.Rows)
                {
                    response.data.Add(new UserSecurity
                    {
                        Numbers = dr["Numbers"] != DBNull.Value && Convert.ToInt32(dr["Numbers"]) == 1,
                        SpecialCharacters = dr["SpecialCharacters"] != DBNull.Value && Convert.ToInt32(dr["SpecialCharacters"]) == 1,                     
                        LowercaseCharacters = dr["LowercaseCharacters"] != DBNull.Value ? Convert.ToBoolean(dr["LowercaseCharacters"]) : false,                     
                        UppercaseCharacters = dr["UppercaseCharacters"] != DBNull.Value && Convert.ToInt32(dr["UppercaseCharacters"]) == 1,
                        MinimumLength = dr["MinimumLength"] != DBNull.Value ? Convert.ToInt32(dr["MinimumLength"]) : 0,
                        PasswordValidationRequired=ADO.Toboolean(dr["PasswordValidationRequired"])
                    });
                }

                // Login name
                if (ds.Tables.Count > 1)
                {
                    DataTable tbl1 = ds.Tables[1];
                    foreach (DataRow dr1 in tbl1.Rows)
                    {
                        response.Login.Add(new UserSecurityLogin
                        {
                            LoginName = dr1["LoginName"] != DBNull.Value ? Convert.ToString(dr1["LoginName"]) : null                         
                        });
                    }
                }
                response.flag = 1;
                response.message = "Success";
            
            return response;
        }

    }
}