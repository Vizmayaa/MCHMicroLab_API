using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using VEZTA.Models;

namespace VEZTA.DAL
{
    public class SecuritySettingMaster_DAL
    {
        public List<SecuritySettingsMaster> GetAllSecurityMasterSetting(Int32 intUserID)
        {

            List<SecuritySettingsMaster> securityList = new List<SecuritySettingsMaster>();
            using (SqlConnection connection = ADO.GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_SECURITY_SETTINGS_MASTER";
                cmd.Parameters.AddWithValue("ACTION", 0);
                //cmd.Parameters.AddWithValue("UserID", intUserID);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tbl = new DataTable();
                da.Fill(tbl);

                foreach (DataRow dr in tbl.Rows)
                {
                    securityList.Add(new SecuritySettingsMaster
                    {
                        AccountLockAttempt = Convert.ToString(dr["AccountLockAttempt"]),
                        AccountLockDuration = Convert.ToString(dr["AccountLockDuration"]),
                        AccountLockFailedLogin = Convert.ToString(dr["AccountLockFailedLogin"]),
                        AlertEmailOnPasswordChange = Convert.ToString(dr["AlertEmailOnPasswordChange"]),
                        AlertSMSOnPasswordChange = Convert.ToString(dr["AlertSMSOnPasswordChange"]),
                        DisableUserOnInactiveDays = Convert.ToString(dr["DisableUserOnInactiveDays"]),
                        LowercaseCharacters = Convert.ToString(dr["LowercaseCharacters"]),
                        MinimumCategoriesRequired = Convert.ToString(dr["MinimumCategoriesRequired"]),
                        MinimumLength = Convert.ToString(dr["MinimumLength"]),
                        Numbers = Convert.ToString(dr["Numbers"]),
                        OTPEmailOnPasswordChange = Convert.ToString(dr["OTPEmailOnPasswordChange"]),
                        OTPSMSOnPasswordChange = Convert.ToString(dr["OTPSMSOnPasswordChange"]),
                        PasswordAge = Convert.ToString(dr["PasswordAge"]),
                        PasswordRepeatCycle = Convert.ToString(dr["PasswordRepeatCycle"]),
                        PasswordValidationRequired = Convert.ToString(dr["PasswordValidationRequired"]),
                        SpecialCharacters = Convert.ToString(dr["SpecialCharacters"]),
                        UnauthorizedBannerMessage = Convert.ToString(dr["UnauthorizedBannerMessage"]),
                        UppercaseCharacters = Convert.ToString(dr["UppercaseCharacters"]),
                        UserMustChangePasswordOnLogin = Convert.ToString(dr["UserMustChangePasswordOnLogin"])
                    });
                }
            }
            return securityList;
        }
    }
}