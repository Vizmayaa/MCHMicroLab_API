using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using VEZTA.Models;

namespace VEZTA.DAL
{
    public class User_DAL
    {
        string conString = ConfigurationManager.ConnectionStrings["WSMasterConnectionString"].ToString();
        Notifaction_DAL notifaction_DAL = new Notifaction_DAL();
        public List<User> GetAllUsers()
        {
            List<User> userList = new List<User>();
            SqlConnection connection = ADO.GetConnection();       
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_USER";
                cmd.Parameters.AddWithValue("ACTION", 0);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tbl = new DataTable();
                da.Fill(tbl);

                foreach (DataRow dr in tbl.Rows)
                {
                    userList.Add(new User
                    {
                        UserID = ADO.ToInt32(dr["UserID"]),
                        IsClinician = ADO.Toboolean(dr["IsClinician"]),
                        ClinicianID = ADO.ToInt32(dr["ClinicianID"]),
                        UserName = ADO.ToString(dr["UserName"]),
                        LoginName = ADO.ToString(dr["LoginName"]),
                        Password = dr["LoginPassword"] != DBNull.Value ? AzentLibrary.Library.DecryptString(dr["LoginPassword"].ToString()) : string.Empty,
                        UserRoleID = ADO.ToInt32(dr["UserRoleID"]),
                        UserRoleName = ADO.ToString(dr["UserRole"]),
                        DateofBirth = dr["DateofBirth"] != DBNull.Value ? Convert.ToDateTime(dr["DateofBirth"]) : DateTime.MinValue,
                        GenderID = ADO.ToInt32(dr["GenderID"]),
                        Email = ADO.ToString(dr["EMAIL"]),
                        Mobile = ADO.ToString(dr["MOBILE"]),
                        Whatsapp = ADO.ToString(dr["Whatsapp"]),
                        LoginExpiryDate = dr["LoginExpiryDate"] != DBNull.Value ? Convert.ToDateTime(dr["LoginExpiryDate"]) : DateTime.MinValue,
                        LoginExpiryReason = ADO.ToString(dr["LoginExpiryReason"]),
                        IsInactive = ADO.Toboolean(dr["IsInactive"]),
                        InactiveReason = ADO.ToString(dr["InactiveReason"]),
                        IsLocked = ADO.Toboolean(dr["IsLocked"]),
                        LockDateFrom = dr["LockDateFrom"] != DBNull.Value ? Convert.ToDateTime(dr["LockDateFrom"]) : DateTime.MinValue,
                        LockDateTo = dr["LockDateTo"] != DBNull.Value ? Convert.ToDateTime(dr["LockDateTo"]) : DateTime.MinValue,
                        LockReason = ADO.ToString(dr["LockReason"]),
                        IsActiveDirectoryUser = ADO.Toboolean(dr["IsActiveDirectoryUser"]),
                        PhotoFile = ADO.ToString(dr["PhotoFile"]),

                Gender = ADO.ToString(dr["Gender"]),
                        ChangePasswordOnLogin = ADO.Toboolean(dr["ChangePasswordOnLogin"])
                    });
                }
                connection.Close();        
            return userList;
        }
        public Int32 Insert(User user)
        {
            try
            {
                SqlConnection connection = ADO.GetConnection();
                SqlTransaction objtrans = connection.BeginTransaction();
                try
                {
                    DataTable tbl = new DataTable();
                    tbl.Columns.Add("ID", typeof(Int32));
                    tbl.Columns.Add("FacilityID", typeof(Int32));

                    foreach (UserFacility ur in user.user_facility)
                    {
                        DataRow dRow = tbl.NewRow();

                        dRow["ID"] = ur.ID;
                        dRow["FacilityID"] = ur.FacilityID;

                        tbl.Rows.Add(dRow);
                        tbl.AcceptChanges();
                    }
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.Transaction = objtrans;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_USER";

                    cmd.Parameters.AddWithValue("ACTION", 1);

                    
                    cmd.Parameters.AddWithValue("UserName", user.UserName ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("LoginName", user.LoginName ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("LoginPassword", user.Password != null ? AzentLibrary.Library.EncryptString(user.Password) : (object)DBNull.Value);

                    cmd.Parameters.AddWithValue("UserRoleID", user.UserRoleID);
                    cmd.Parameters.AddWithValue("DateofBirth", user.DateofBirth != default(DateTime) ? (object)user.DateofBirth : DBNull.Value);
                    cmd.Parameters.AddWithValue("GenderID", user.GenderID != 0 ? (object)user.GenderID : DBNull.Value);
                    cmd.Parameters.AddWithValue("Email", user.Email ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("Mobile", user.Mobile ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("Whatsapp", user.Whatsapp ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("LoginExpiryDate", user.LoginExpiryDate != default(DateTime) ? (object)user.LoginExpiryDate : DBNull.Value);
                    cmd.Parameters.AddWithValue("LoginExpiryReason", user.LoginExpiryReason ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("IsInactive", user.IsInactive);
                    cmd.Parameters.AddWithValue("InactiveReason", user.InactiveReason ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("IsLocked", user.IsLocked);
                    cmd.Parameters.AddWithValue("LockDateFrom", user.LockDateFrom != default(DateTime) ? (object)user.LockDateFrom : DBNull.Value);
                    cmd.Parameters.AddWithValue("LockDateTo", user.LockDateTo != default(DateTime) ? (object)user.LockDateTo : DBNull.Value);
                    cmd.Parameters.AddWithValue("LockReason", user.LockReason ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("IsActiveDirectoryUser", user.IsActiveDirectoryUser);
                    cmd.Parameters.AddWithValue("PhotoFile", user.PhotoFile ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("ChangePasswordOnLogin", user.ChangePasswordOnLogin);
                    cmd.Parameters.AddWithValue("@UDT_TB_USER_FACILITIES", tbl);


                    Int32 UserID = Convert.ToInt32(cmd.ExecuteScalar());

                    objtrans.Commit();


                    // Notification handling logic
                    bool SentSMS = false;
                    bool SentWhatsapp = false;
                    bool SentEmail = false;
                    string smsTemplate = "";
                    string WhatsappTemplate = "";
                    string EmailSubject = "";
                    string EmailMessage = "";

                    string selectquery = "SELECT SendSMS, SendWhatsapp, SendEmail, SMSTemplate, WhatsappTemplate, EmailSubject, EmailMessage FROM TB_NOTIFICATION_TEMPLATES WHERE ID = 1";
                    DataTable tbl1 = ADO.GetDataTable(selectquery);

                    if (tbl1.Rows.Count > 0)
                    {
                        SentSMS = Convert.ToBoolean(tbl1.Rows[0]["SendSMS"]);
                        SentWhatsapp = Convert.ToBoolean(tbl1.Rows[0]["SendWhatsapp"]);
                        SentEmail = Convert.ToBoolean(tbl1.Rows[0]["SendEmail"]);
                        smsTemplate = tbl1.Rows[0]["SMSTemplate"].ToString();
                        WhatsappTemplate = tbl1.Rows[0]["WhatsappTemplate"].ToString();
                        EmailSubject = tbl1.Rows[0]["EmailSubject"].ToString();
                        EmailMessage = tbl1.Rows[0]["EmailMessage"].ToString();
                    }

                    // Assuming notifaction_DAL is defined elsewhere and handles sending emails
                    SentEmail = notifaction_DAL.SendEmail(UserID, user.Email, EmailSubject, EmailMessage);

                    if (!SentEmail)
                    {
                        throw new Exception("Failed to send OTP email. Please try again.");
                    }

                    return UserID;

                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
  
        public Int32 Update(User user)
        {
            try
            {
                SqlConnection connection = ADO.GetConnection();
                SqlTransaction objtrans = connection.BeginTransaction();
                try
                {
                    DataTable tbl = new DataTable();
                    tbl.Columns.Add("ID", typeof(Int32));
                    tbl.Columns.Add("FacilityID", typeof(Int32));

                    foreach (UserFacility ur in user.user_facility)
                    {
                        DataRow dRow = tbl.NewRow();

                        dRow["ID"] = ur.ID;
                        dRow["FacilityID"] = ur.FacilityID;

                        tbl.Rows.Add(dRow);
                        tbl.AcceptChanges();
                    }
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.Transaction = objtrans;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_USER";

                    cmd.Parameters.AddWithValue("ACTION", 2);

                    cmd.Parameters.AddWithValue("UserID", user.UserID);
                    cmd.Parameters.AddWithValue("UserName", user.UserName ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("LoginName", user.LoginName ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("LoginPassword", user.Password != null ? AzentLibrary.Library.EncryptString(user.Password) : (object)DBNull.Value);

                    cmd.Parameters.AddWithValue("UserRoleID", user.UserRoleID);
                    cmd.Parameters.AddWithValue("DateofBirth", user.DateofBirth != default(DateTime) ? (object)user.DateofBirth : DBNull.Value);
                    cmd.Parameters.AddWithValue("GenderID", user.GenderID != 0 ? (object)user.GenderID : DBNull.Value);
                    cmd.Parameters.AddWithValue("Email", user.Email ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("Mobile", user.Mobile ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("Whatsapp", user.Whatsapp ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("LoginExpiryDate", user.LoginExpiryDate != default(DateTime) ? (object)user.LoginExpiryDate : DBNull.Value);
                    cmd.Parameters.AddWithValue("LoginExpiryReason", user.LoginExpiryReason ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("IsInactive", user.IsInactive);
                    cmd.Parameters.AddWithValue("InactiveReason", user.InactiveReason ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("IsLocked", user.IsLocked);
                    cmd.Parameters.AddWithValue("LockDateFrom", user.LockDateFrom != default(DateTime) ? (object)user.LockDateFrom : DBNull.Value);
                    cmd.Parameters.AddWithValue("LockDateTo", user.LockDateTo != default(DateTime) ? (object)user.LockDateTo : DBNull.Value);
                    cmd.Parameters.AddWithValue("LockReason", user.LockReason ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("IsActiveDirectoryUser", user.IsActiveDirectoryUser);
                    cmd.Parameters.AddWithValue("PhotoFile", user.PhotoFile ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@UDT_TB_USER_FACILITIES", tbl);


                    Int32 UserID = Convert.ToInt32(cmd.ExecuteScalar());

                    objtrans.Commit();

                    return UserID;

                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public User GetItems(int id)
        {
            User user = new User();
            List<UserFacility> userFacilities = new List<UserFacility>();
            try
            {
                string strSQL = "SELECT TB_USER.*, TB_USER_ROLES.UserRole, TB_GENDER.Gender " +
                  "FROM TB_USER " +
                  "LEFT JOIN TB_USER_ROLES ON TB_USER.UserRoleID = TB_USER_ROLES.ID " +
                  "LEFT JOIN TB_GENDER ON TB_USER.GenderID = TB_GENDER.ID " +
                  "WHERE TB_USER.IsDeleted = 0 AND TB_USER.UserID = " + id;



                DataTable tbl = ADO.GetDataTable(strSQL, "User");

                if (tbl.Rows.Count > 0)
                {
                    DataRow dr = tbl.Rows[0];

                    user.UserID = dr["UserID"] != DBNull.Value ? Convert.ToInt32(dr["UserID"]) : 0;
                        user.IsClinician = dr["IsClinician"] != DBNull.Value && Convert.ToBoolean(dr["IsClinician"]);
                        user.ClinicianID = dr["ClinicianID"] != DBNull.Value ? Convert.ToInt32(dr["ClinicianID"]) : 0;
                        user.UserName = dr["UserName"] != DBNull.Value ? dr["UserName"].ToString() : string.Empty;
                        user.LoginName = dr["LoginName"] != DBNull.Value ? dr["LoginName"].ToString() : string.Empty;
                        user.Password = dr["LoginPassword"] != DBNull.Value ? AzentLibrary.Library.DecryptString(dr["LoginPassword"].ToString()) : string.Empty;
                        user.UserRoleID = dr["UserRoleID"] != DBNull.Value ? Convert.ToInt32(dr["UserRoleID"]) : 0;
                        user.UserRoleName = dr["UserRole"] != DBNull.Value ? dr["UserRole"].ToString() : string.Empty;
                       user.DateofBirth = dr["DateofBirth"] != DBNull.Value ? Convert.ToDateTime(dr["DateofBirth"]) : (DateTime?)null;

                    user.GenderID = dr["GenderID"] != DBNull.Value ? Convert.ToInt32(dr["GenderID"]) : 0;
                        user.Email = dr["EMAIL"] != DBNull.Value ? dr["EMAIL"].ToString() : string.Empty;
                        user.Mobile = dr["MOBILE"] != DBNull.Value ? dr["MOBILE"].ToString() : string.Empty;
                        user.Whatsapp = dr["Whatsapp"] != DBNull.Value ? dr["Whatsapp"].ToString() : string.Empty;
                        
                    user.LoginExpiryDate = dr["LoginExpiryDate"] != DBNull.Value ? Convert.ToDateTime(dr["LoginExpiryDate"]) : (DateTime?)null;

                    user.LoginExpiryReason = dr["LoginExpiryReason"] != DBNull.Value ? dr["LoginExpiryReason"].ToString() : string.Empty;
                        user.IsInactive = dr["IsInactive"] != DBNull.Value && Convert.ToBoolean(dr["IsInactive"]);
                        user.InactiveReason = dr["InactiveReason"] != DBNull.Value ? dr["InactiveReason"].ToString() : string.Empty;
                        user.IsLocked = dr["IsLocked"] != DBNull.Value && Convert.ToBoolean(dr["IsLocked"]);
                      
                    user.LockDateFrom = dr["LockDateFrom"] != DBNull.Value ? Convert.ToDateTime(dr["LockDateFrom"]) : (DateTime?)null;


                   
                    user.LockDateTo = dr["LockDateTo"] != DBNull.Value ? Convert.ToDateTime(dr["LockDateTo"]) : (DateTime?)null;

                    user.LockReason = dr["LockReason"] != DBNull.Value ? dr["LockReason"].ToString() : string.Empty;
                        user.IsActiveDirectoryUser = dr["IsActiveDirectoryUser"] != DBNull.Value && Convert.ToBoolean(dr["IsActiveDirectoryUser"]);
                    user.PhotoFile = dr["PhotoFile"] != DBNull.Value ? dr["PhotoFile"].ToString() : string.Empty;
                    user.Gender = dr["Gender"] != DBNull.Value ? dr["Gender"].ToString() : string.Empty;
                }

                // Query to get User facility
                strSQL = "SELECT * FROM TB_USER_FACILITIES " +
                       "WHERE TB_USER_FACILITIES.UserID = " + id;

                DataTable UserFacility = ADO.GetDataTable(strSQL, "UserFaclity");

                foreach (DataRow dr2 in UserFacility.Rows)
                {
                    userFacilities.Add(new UserFacility
                    {
                        ID = Convert.ToInt32(dr2["ID"]),
                        FacilityID = Convert.ToInt32(dr2["FacilityID"])
                       
                    });
                }
                user.user_facility = userFacilities;

            }
            catch (Exception ex)
            {

            }

            return user;
        }
        public bool DeleteUser(int id)
        {
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_USER";
                    cmd.Parameters.AddWithValue("ACTION", 4);
                    cmd.Parameters.AddWithValue("@UserID", id);
                    cmd.ExecuteNonQuery();

                    connection.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private string GenerateToken(string localip, string systemtime, int userId)
        {
            string cleanedLocalIp = localip.Replace("-", "").Replace(":", "").Replace(".", "");
            string timePart = DateTime.Parse(systemtime).ToString("HHmmss");
            string token = cleanedLocalIp + timePart + userId.ToString();
            return token;
        }  
        public int Logout(UserLogout logout)
        {            
            try
            {
                string strSQL = "UPDATE TB_USER_LOGIN SET LogoutTimeUTC = GETUTCDATE() " +
                                "WHERE Token = " + ADO.SQLString(logout.Token);
                ADO.ExecuteNonQuery(strSQL);

                return 1;
            }
            catch (Exception ex)
            {
                ADO.LogError(ex, "Users", "Logout()");                
            }
            return 0;
        }
        public Int32 GetUserIDWithToken(string Token)
        {
            Int32 intUserID = 0;

            try
            {
                string strSQL = "SELECT UserID FROM TB_USER_LOGIN WHERE LogoutTimeUTC IS NULL " +
                                "AND Token = " + ADO.SQLString(Token);

                intUserID = Convert.ToInt32( ADO.ExecuteScalar(strSQL));

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return intUserID;
        }

        public UserLoginResponse VerifyLogin(UserVerificationInput vInput)
        {
            UserLoginResponse result = new UserLoginResponse();
            SqlConnection objCon = new SqlConnection();
            try
            {
                List<UserMenu> userMenus = new List<UserMenu>();
                User usr = new User();

                objCon = ADO.GetConnection();
                SqlCommand cmd = new SqlCommand
                {
                    Connection = objCon,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "SP_VERIFY_LOGIN"
                };

                cmd.Parameters.AddWithValue("@LOGIN_NAME", vInput.LoginName);
                cmd.Parameters.AddWithValue("@LOGIN_PASSWORD", AzentLibrary.Library.EncryptString(vInput.Password));
                cmd.Parameters.AddWithValue("@LOCAL_IP", vInput.LocalIP);
                cmd.Parameters.AddWithValue("@COMPUTER_NAME", vInput.ComputerName);
                cmd.Parameters.AddWithValue("@DOMAIN_NAME", vInput.DomainName);
                cmd.Parameters.AddWithValue("@COMPUTER_USER", vInput.ComputerUser);
                cmd.Parameters.AddWithValue("@INTERNET_IP", vInput.InternetIP);
                cmd.Parameters.AddWithValue("@SYSTEM_TIME_UTC", vInput.SystemTimeUTC);
                cmd.Parameters.AddWithValue("@FORCE_LOGIN", Convert.ToByte(vInput.ForceLogin));
                cmd.CommandTimeout = 0;

                DataSet ds = new DataSet();
                SqlDataAdapter sqlDA = new SqlDataAdapter(cmd);
                sqlDA.Fill(ds);

                if (ds.Tables.Count > 0)
                {
                    result.flag = ds.Tables[0].Rows[0]["FLAG"].ToString();
                    result.message = ds.Tables[0].Rows[0]["MESSAGE"].ToString();

                    if (result.flag == "0")
                    {
                        // Login failed, return the appropriate message
                        return result;
                    }

                    // If login is successful
                    if (result.flag == "1")
                    {
                        int userID = Convert.ToInt32(ds.Tables[1].Rows[0]["UserID"]);
                        string vLocalIP = vInput.LocalIP != "" ? vInput.LocalIP : "192.168.0.1";
                        string vTime = vInput.SystemTimeUTC;

                        string token = GenerateToken(vLocalIP, vTime, userID);
                        token = Convert.ToBase64String(Encoding.UTF8.GetBytes(token));

                        usr.UserID = ds.Tables[1].Rows[0]["UserID"] != DBNull.Value ? Convert.ToInt32(ds.Tables[1].Rows[0]["UserID"]) : 0;
                        usr.UserName = ds.Tables[1].Rows[0]["UserName"] != DBNull.Value ? ds.Tables[1].Rows[0]["UserName"].ToString() : string.Empty;
                        usr.LoginName = ds.Tables[1].Rows[0]["LoginName"] != DBNull.Value ? ds.Tables[1].Rows[0]["LoginName"].ToString() : string.Empty;
                        
                        string encryptedPassword = ds.Tables[1].Rows[0]["LoginPassword"] != DBNull.Value ? ds.Tables[1].Rows[0]["LoginPassword"].ToString() : string.Empty;
                        usr.Password = !string.IsNullOrEmpty(encryptedPassword) ? AzentLibrary.Library.DecryptString(encryptedPassword) : string.Empty;

                        usr.Email = ds.Tables[1].Rows[0]["Email"] != DBNull.Value ? ds.Tables[1].Rows[0]["Email"].ToString() : string.Empty;
                        usr.Mobile = ds.Tables[1].Rows[0]["Mobile"] != DBNull.Value ? ds.Tables[1].Rows[0]["Mobile"].ToString() : string.Empty;
                        usr.UserRoleName = ds.Tables[1].Rows[0]["UserRole"] != DBNull.Value ? ds.Tables[1].Rows[0]["UserRole"].ToString() : string.Empty;
                        usr.IsInactive = ds.Tables[1].Rows[0]["IsInactive"] != DBNull.Value && Convert.ToBoolean(ds.Tables[1].Rows[0]["IsInactive"]);
                        usr.ChangePasswordOnLogin = ds.Tables[1].Rows[0]["ChangePasswordOnLogin"] != DBNull.Value && Convert.ToBoolean(ds.Tables[1].Rows[0]["ChangePasswordOnLogin"]);
                        usr.PhotoFile= ds.Tables[1].Rows[0]["PhotoFile"] != DBNull.Value ? ds.Tables[1].Rows[0]["PhotoFile"].ToString() : string.Empty;

                        usr.Token = token;

                        // Get user menus
                        List<UserMenu> menuGroups = new List<UserMenu>();
                        if (ds.Tables.Count > 2)
                        {
                            foreach (DataRow dr2 in ds.Tables[2].Rows)
                            {
                                menuGroups.Add(new UserMenu
                                {
                                    id = dr2["ID"].ToString(),
                                    GroupID = dr2["GroupID"].ToString(),
                                    text = dr2["MenuName"].ToString(),
                                    path = dr2["MenuPath"].ToString(),
                                    icon = dr2["MenuIcon"].ToString()
                                });
                            }
                        }

                        // Log the successful login
                        string strSQL = "INSERT INTO TB_USER_LOGIN(LoginName, LoginPassword, LocalIP, " +
                                        "ComputerName, DomainName, ComputerUser, InternetIP, SystemTimeUTC, " +
                                        "LoginSuccess, LoginFailReason, ForceLogin, userID, Token) VALUES (" +
                                        ADO.SQLString(vInput.LoginName) + "," +
                                        ADO.SQLString(vInput.Password) + "," +
                                        ADO.SQLString(vInput.LocalIP) + "," +
                                        ADO.SQLString(vInput.ComputerName) + "," +
                                        ADO.SQLString(vInput.DomainName) + "," +
                                        ADO.SQLString(vInput.ComputerUser) + "," +
                                        ADO.SQLString(vInput.InternetIP) + "," +
                                        ADO.SQLString(vInput.SystemTimeUTC) + ", 1, '', " +
                                        Convert.ToByte(vInput.ForceLogin) + "," + userID.ToString() + "," +
                                        ADO.SQLString(token) + ")";

                        ADO.ExecuteNonQuery(strSQL);

                        result.data = usr;
                        result.menus = menuGroups;  // Assign the menu groups to the result
                    }
                }
            }
            catch (Exception ex)
            {
                result.flag = "0";
                result.message = "Login failed due to system error: " + ex.Message;
            }
            finally
            {
                objCon.Close();
            }
            return result;
        }

        public Int32 InserActivityLogin(UserActivityLoglnput useractivity)
        {
            try
            {
                SqlConnection connection = ADO.GetConnection();
                SqlTransaction objtrans = connection.BeginTransaction();
               
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.Transaction = objtrans;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_USER_ACTIVITY_LOG";

                    cmd.Parameters.AddWithValue("@UserID", useractivity.USER_ID);
                    cmd.Parameters.AddWithValue("@Menu", useractivity.TITLE);
                    cmd.Parameters.AddWithValue("@Action", useractivity.ACTION);
                    cmd.Parameters.AddWithValue("@Token", useractivity.TOKEN);

                    // Execute the command
                    cmd.ExecuteNonQuery(); // Use ExecuteNonQuery since the procedure does not return a value

                    objtrans.Commit();
                    return useractivity.USER_ID; // Return the UserID or another appropriate value
               
              
            }
            catch (Exception ex)
            {
                // Log or handle general exceptions
                throw new Exception("An error occurred while logging user activity.", ex);
            }
        }

    }

}