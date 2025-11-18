using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using VEZTA.Models;

namespace VEZTA.DAL
{
    public class UserLab_DAL
    {
        public List<UserLab> GetAllUsers()
        {
            List<UserLab> userList = new List<UserLab>();
            using (SqlConnection connection = ADO.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("SP_TB_LIST", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ACTION", 0);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable tbl = new DataTable();
                        da.Fill(tbl);

                        if (tbl.Rows.Count > 0)
                        {
                            userList = tbl.AsEnumerable().Select(dr => new UserLab
                            {
                                USER_ID = ADO.ToInt32(dr["USER_ID"]),
                                DEPT_ID = ADO.ToInt32(dr["DEPT_ID"]),
                                DEPT_NAME = ADO.ToString(dr["DEPT_NAME"]),
                                USER_NAME = ADO.ToString(dr["USER_NAME"]),
                                LOGIN_NAME = ADO.ToString(dr["LOGIN_NAME"]),
                                PASSWORD = AzentLibrary.Library.DecryptString(ADO.ToString(dr["PASSWORD"])),
                                IS_ADMIN = ADO.Toboolean(dr["IS_ADMIN"]),
                                IS_INACTIVE = ADO.Toboolean(dr["IS_INACTIVE"])
                            }).ToList();
                        }
                    }
                }
            }
            return userList;
        }

        public UserLabLoginResponse VerifyLogin(UserLabVerificationInput vLoginInput)
        {
            var response = new UserLabLoginResponse
            {
                data = new List<UserLab>()
            };

            if (string.IsNullOrWhiteSpace(vLoginInput.LOGIN_NAME) || string.IsNullOrWhiteSpace(vLoginInput.PASSWORD))
            {
                response.flag = 0;
                response.message = "Username and password are required.";
                return response;
            }

            try
            {
                using (var connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (var cmd = new SqlCommand("SP_VERIFY_LOGIN", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@LOGIN_NAME", vLoginInput.LOGIN_NAME);
                        cmd.Parameters.AddWithValue("@PASSWORD", AzentLibrary.Library.EncryptString(vLoginInput.PASSWORD ?? ""));

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                response.flag = reader["FLAG"] != DBNull.Value ? Convert.ToInt32(reader["FLAG"]) : 0;
                                response.message = reader["MESSAGE"]?.ToString();
                            }

                            if (response.flag == 1 && reader.NextResult())
                            {
                                while (reader.Read())
                                {
                                    response.data.Add(new UserLab
                                    {
                                        USER_ID = reader["USER_ID"] != DBNull.Value ? Convert.ToInt32(reader["USER_ID"]) : 0,
                                        USER_NAME = reader["USER_NAME"]?.ToString(),
                                        LOGIN_NAME = reader["LOGIN_NAME"]?.ToString(),
                                        PASSWORD = AzentLibrary.Library.DecryptString(reader["PASSWORD"]?.ToString() ?? ""),
                                        IS_ADMIN = reader["IS_ADMIN"] != DBNull.Value && Convert.ToBoolean(reader["IS_ADMIN"]),
                                        IS_LAB_USER = reader["IS_LAB_USER"] != DBNull.Value && Convert.ToBoolean(reader["IS_LAB_USER"]),
                                        IS_HOSPITAL_USER = reader["IS_HOSPITAL_USER"] != DBNull.Value && Convert.ToBoolean(reader["IS_HOSPITAL_USER"]),
                                        IS_VERIFY_REPORT = reader["IS_VERIFY_REPORT"] != DBNull.Value && Convert.ToBoolean(reader["IS_VERIFY_REPORT"]),
                                        IS_INACTIVE = reader["IS_INACTIVE"] != DBNull.Value && Convert.ToBoolean(reader["IS_INACTIVE"]),
                                        HOSPITAL_ID = reader["HOSPITAL_ID"]?.ToString(),
                                        HOSPITAL_NAME = reader["HOSPITAL_NAME"]?.ToString()
                                    });
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.flag = 0;
                response.message = ex.Message;
            }

            return response;
        }




        public UserLabInsertResponse Insert(UserLabInsertInput user)
        {
            UserLabInsertResponse res = new UserLabInsertResponse();
            SqlConnection connection = new SqlConnection();

            try
            {
                connection = ADO.GetConnection();
                SqlCommand cmd = new SqlCommand
                {
                    Connection = connection,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "SP_TB_USER"
                };

                // Encrypt password before saving
                string encryptedPassword = AzentLibrary.Library.EncryptString(user.PASSWORD ?? string.Empty);

                cmd.Parameters.AddWithValue("@ACTION", 1);
                cmd.Parameters.AddWithValue("@USER_ID", 0);
                cmd.Parameters.AddWithValue("@DEPT_ID", user.DEPT_ID);
                cmd.Parameters.AddWithValue("@USER_NAME", user.USER_NAME ?? string.Empty);
                cmd.Parameters.AddWithValue("@LOGIN_NAME", user.LOGIN_NAME ?? string.Empty);
                cmd.Parameters.AddWithValue("@PASSWORD", encryptedPassword);
                cmd.Parameters.AddWithValue("@IS_ADMIN", user.IS_ADMIN);
                cmd.Parameters.AddWithValue("@IS_LAB_USER", user.IS_LAB_USER);
                cmd.Parameters.AddWithValue("@IS_HOSPITAL_USER", user.IS_HOSPITAL_USER);
                cmd.Parameters.AddWithValue("@IS_INACTIVE", user.IS_INACTIVE);
                cmd.Parameters.AddWithValue("@HOSPITAL_ID", user.HOSPITAL_ID ?? string.Empty);

                // Execute Insert
                cmd.ExecuteNonQuery();

                res.flag = 1;
                res.Message = "Success";
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }

            return res;
        }

        public UserLabInsertResponse Update(UserLabInsertInput user)
        {
            UserLabInsertResponse res = new UserLabInsertResponse();
            SqlConnection connection = new SqlConnection();

            try
            {
                connection = ADO.GetConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_USER";
                string encryptedPassword = AzentLibrary.Library.EncryptString(user.PASSWORD ?? string.Empty);


                cmd.Parameters.AddWithValue("@ACTION", 2);
                cmd.Parameters.AddWithValue("@USER_ID", user.ID);
                cmd.Parameters.AddWithValue("@DEPT_ID", user.DEPT_ID);
                cmd.Parameters.AddWithValue("@USER_NAME", user.USER_NAME ?? string.Empty);
                cmd.Parameters.AddWithValue("@LOGIN_NAME", user.LOGIN_NAME ?? string.Empty);
                cmd.Parameters.AddWithValue("@PASSWORD", encryptedPassword ?? string.Empty);
                cmd.Parameters.AddWithValue("@IS_ADMIN", user.IS_ADMIN);
                cmd.Parameters.AddWithValue("@IS_LAB_USER", user.IS_LAB_USER);
                cmd.Parameters.AddWithValue("@IS_HOSPITAL_USER", user.IS_HOSPITAL_USER);
                cmd.Parameters.AddWithValue("@IS_INACTIVE", user.IS_INACTIVE);
                cmd.Parameters.AddWithValue("@HOSPITAL_ID", user.HOSPITAL_ID ?? string.Empty);

                // Execute Update
                cmd.ExecuteNonQuery();

                res.flag = 1;
                res.Message = "Success";
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }

            return res;
        }

        //public UserLabSelectResponse GetUserLabById(int id)
        //{
        //    UserLabSelectResponse response = new UserLabSelectResponse();
        //    try
        //    {
        //        string strSQL = @"
        //    SELECT u.USER_ID, u.DEPT_ID, d.DEPT_NAME, 
        //           u.USER_NAME, u.LOGIN_NAME, u.PASSWORD,
        //           u.IS_ADMIN, u.IS_INACTIVE, u.IS_LAB_USER, 
        //           u.IS_HOSPITAL_USER, u.HOSPITAL_ID, h.HOSPITAL AS HOSPITAL_NAME
        //    FROM TB_USERS u
        //    LEFT JOIN TB_DEPARTMENT d ON u.DEPT_ID = d.ID
        //    LEFT JOIN TB_HOSPITAL h ON u.HOSPITAL_ID = h.ID
        //    WHERE u.IS_DELETED = 0 AND u.USER_ID = " + id;

        //        DataTable tbl = ADO.GetDataTable(strSQL, "UserLab");
        //        if (tbl.Rows.Count > 0)
        //        {
        //            DataRow dr = tbl.Rows[0];
        //            string encryptedPwd = ADO.ToString(dr["PASSWORD"]);
        //            string decryptedPwd;

        //            try
        //            {
        //                // First try your normal decrypt method
        //                decryptedPwd = AzentLibrary.Library.DecryptString(encryptedPwd);

        //                // If decrypt returns empty or same as encrypted, try Base64 decode
        //                if (string.IsNullOrEmpty(decryptedPwd) || decryptedPwd == encryptedPwd)
        //                {
        //                    byte[] data = Convert.FromBase64String(encryptedPwd);
        //                    decryptedPwd = System.Text.Encoding.UTF8.GetString(data);
        //                }
        //            }
        //            catch
        //            {
        //                decryptedPwd = "[Unable to decrypt]";
        //            }

        //            response.Data = new UserLab
        //            {
        //                USER_ID = ADO.ToInt32(dr["USER_ID"]),
        //                DEPT_ID = ADO.ToInt32(dr["DEPT_ID"]),
        //                DEPT_NAME = ADO.ToString(dr["DEPT_NAME"]),
        //                USER_NAME = ADO.ToString(dr["USER_NAME"]),
        //                LOGIN_NAME = ADO.ToString(dr["LOGIN_NAME"]),
        //                PASSWORD = decryptedPwd,
        //                IS_ADMIN = ADO.Toboolean(dr["IS_ADMIN"]),
        //                IS_INACTIVE = ADO.Toboolean(dr["IS_INACTIVE"]),
        //                IS_LAB_USER = ADO.Toboolean(dr["IS_LAB_USER"]),
        //                IS_HOSPITAL_USER = ADO.Toboolean(dr["IS_HOSPITAL_USER"]),
        //                HOSPITAL_ID = ADO.ToString(dr["HOSPITAL_ID"]),
        //                HOSPITAL_NAME = ADO.ToString(dr["HOSPITAL_NAME"])
        //            };
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //    return response;
        //}
        public UserLabSelectResponse GetUserLabById(int id)
        {
            UserLabSelectResponse response = new UserLabSelectResponse();

            try
            {
                string strSQL = @"
            SELECT u.USER_ID, u.DEPT_ID, d.DEPT_NAME, u.USER_NAME, u.LOGIN_NAME, 
                   u.PASSWORD, u.IS_ADMIN, u.IS_INACTIVE, u.IS_LAB_USER, 
                   u.IS_HOSPITAL_USER, u.HOSPITAL_ID,
                   STUFF((SELECT ', ' + h.HOSPITAL 
                          FROM TB_HOSPITAL h 
                          WHERE ',' + u.HOSPITAL_ID + ',' LIKE '%,' + CAST(h.ID AS VARCHAR(10)) + ',%'
                          FOR XML PATH('')), 1, 2, '') AS HOSPITAL_NAME
            FROM TB_USERS u
            LEFT JOIN TB_DEPARTMENT d ON u.DEPT_ID = d.ID
            WHERE u.IS_DELETED = 0 AND u.USER_ID = " + id;

                DataTable tbl = ADO.GetDataTable(strSQL, "UserLab");

                if (tbl.Rows.Count > 0)
                {
                    DataRow dr = tbl.Rows[0];
                    string encryptedPwd = ADO.ToString(dr["PASSWORD"]);
                    string decryptedPwd;

                    try
                    {
                        // First try your normal decrypt method
                        decryptedPwd = AzentLibrary.Library.DecryptString(encryptedPwd);

                        // If decrypt returns empty or same as encrypted, try Base64 decode
                        if (string.IsNullOrEmpty(decryptedPwd) || decryptedPwd == encryptedPwd)
                        {
                            byte[] data = Convert.FromBase64String(encryptedPwd);
                            decryptedPwd = System.Text.Encoding.UTF8.GetString(data);
                        }
                    }
                    catch
                    {
                        decryptedPwd = "[Unable to decrypt]";
                    }

                    response.Data = new UserLab
                    {
                        USER_ID = ADO.ToInt32(dr["USER_ID"]),
                        DEPT_ID = ADO.ToInt32(dr["DEPT_ID"]),
                        DEPT_NAME = ADO.ToString(dr["DEPT_NAME"]),
                        USER_NAME = ADO.ToString(dr["USER_NAME"]),
                        LOGIN_NAME = ADO.ToString(dr["LOGIN_NAME"]),
                        PASSWORD = decryptedPwd,
                        IS_ADMIN = ADO.Toboolean(dr["IS_ADMIN"]),
                        IS_INACTIVE = ADO.Toboolean(dr["IS_INACTIVE"]),
                        IS_LAB_USER = ADO.Toboolean(dr["IS_LAB_USER"]),
                        IS_HOSPITAL_USER = ADO.Toboolean(dr["IS_HOSPITAL_USER"]),
                        HOSPITAL_ID = ADO.ToString(dr["HOSPITAL_ID"]),
                        HOSPITAL_NAME = ADO.ToString(dr["HOSPITAL_NAME"])
                    };

                    response.flag = 1;
                    response.Message = "Success";
                }
                else
                {
                    response.flag = 0;
                    response.Message = "User not found";
                }
            }
            catch (Exception ex)
            {
                response.flag = 0;
                response.Message = "Error retrieving user: " + ex.Message;
            }

            return response;
        }

        public UserLabInsertResponse Delete(int id)
        {
            UserLabInsertResponse userLab = new UserLabInsertResponse();
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_TB_USER", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTION", 3); 
                        cmd.Parameters.AddWithValue("@USER_ID", id);

                        int rowsEffected = cmd.ExecuteNonQuery();

                        userLab.flag = rowsEffected > 0 ? 1 : 0;
                        userLab.Message = rowsEffected > 0 ? "Success" : "Delete failed";
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return userLab;
        }



    }
}