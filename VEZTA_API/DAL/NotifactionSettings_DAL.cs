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
    public class NotifactionSettings_DAL
    {
        public List<NotificationSettings> GetAllNotificationSettings(Int32 intUserID)
        {

            List<NotificationSettings> notificationSettings = new List<NotificationSettings>();
            SqlConnection connection = ADO.GetConnection();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_TB_NOTIFICATION_SETTINGS";
            cmd.Parameters.AddWithValue("ACTION", 0);
            cmd.Parameters.AddWithValue("UserID", intUserID);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable tbl = new DataTable();
            da.Fill(tbl);

            foreach (DataRow dr in tbl.Rows)
            {
                notificationSettings.Add(new NotificationSettings
                {
                    EmailSenderID = ADO.ToString(dr["EmailSenderID"]),
                    EmailSenderName = ADO.ToString(dr["EmailSenderName"]),

                    EmailSenderPassword = AzentLibrary.Library.DecryptString(dr["EmailSenderPassword"].ToString()),
                    EmailSMTPHost = ADO.ToString(dr["EmailSMTPHost"]),
                    EmailSMTPPort = ADO.ToInt32(dr["EmailSMTPPort"]),
                    EmailEnableSSL = ADO.Toboolean(dr["EmailEnableSSL"]),
                    EmailIsInactive = ADO.Toboolean(dr["EmailIsInactive"]),
                    SMSProviderURL = ADO.ToString(dr["SMSProviderURL"]),
                    SMSUserID = ADO.ToString(dr["SMSUserID"]),
                    SMSPassword = ADO.ToString(dr["SMSPassword"]),
                    SMSMobileNo = ADO.ToString(dr["SMSMobileNo"]),
                    SMSIsInactive = ADO.Toboolean(dr["SMSIsInactive"]),
                    WhatsappSource = ADO.ToString(dr["WhatsappSource"]),
                    WhatsappNumber = ADO.ToString(dr["WhatsappNumber"]),
                    WhatsappIsInactive = ADO.Toboolean(dr["WhatsappIsInactive"])
                });
            }
            return notificationSettings;
        }
        public Int32 Save(NotificationSettings notificationSettings, Int32 userID)
        {
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_NOTIFICATION_SETTINGS";
                    cmd.Parameters.AddWithValue("ACTION", 1);

                    cmd.Parameters.AddWithValue("EmailSenderID", ADO.ToString(notificationSettings.EmailSenderID));
                    cmd.Parameters.AddWithValue("EmailSenderName", ADO.ToString(notificationSettings.EmailSenderName));
                    cmd.Parameters.AddWithValue("EmailSenderPassword", ADO.ToString(AzentLibrary.Library.EncryptString(notificationSettings.EmailSenderPassword)));
                    cmd.Parameters.AddWithValue("EmailSMTPHost", ADO.ToString(notificationSettings.EmailSMTPHost));
                    cmd.Parameters.AddWithValue("EmailSMTPPort", ADO.ToString(notificationSettings.EmailSMTPPort));
                    cmd.Parameters.AddWithValue("EmailEnableSSL", ADO.ToString(notificationSettings.EmailEnableSSL));
                    cmd.Parameters.AddWithValue("EmailIsInactive", ADO.ToString(notificationSettings.EmailIsInactive));
                    cmd.Parameters.AddWithValue("SMSProviderURL", ADO.ToString(notificationSettings.SMSProviderURL));
                    cmd.Parameters.AddWithValue("SMSUserID", ADO.ToString(notificationSettings.SMSUserID));
                    cmd.Parameters.AddWithValue("SMSPassword", ADO.ToString(AzentLibrary.Library.EncryptString(notificationSettings.SMSPassword)));
                    cmd.Parameters.AddWithValue("SMSMobileNo", ADO.ToString(notificationSettings.SMSMobileNo));
                    cmd.Parameters.AddWithValue("SMSIsInactive", ADO.ToString(notificationSettings.SMSIsInactive));
                    cmd.Parameters.AddWithValue("WhatsappSource", ADO.ToString(notificationSettings.WhatsappSource));
                    cmd.Parameters.AddWithValue("WhatsappNumber", ADO.ToString(notificationSettings.WhatsappNumber));
                    cmd.Parameters.AddWithValue("WhatsappIsInactive", ADO.ToString(notificationSettings.WhatsappIsInactive));

                    cmd.Parameters.AddWithValue("UserID", userID);

                    cmd.ExecuteNonQuery();

                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       
        public NotificationTemplateResponse GetTemplatekeyWord(Int32 intUserID)
        {
            NotificationTemplateResponse response = new NotificationTemplateResponse();
            response.data = new List<NotificationTemplate>();
            response.keywords = new List<ConfigurationValue>();

            SqlConnection connection = ADO.GetConnection();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT TB_NOTIFICATION_TEMPLATES.*, TB_NOTIFICATION_TYPES.NotificationType AS NOTIFICATION FROM TB_NOTIFICATION_TEMPLATES " +
                "LEFT JOIN TB_NOTIFICATION_TYPES ON TB_NOTIFICATION_TEMPLATES.NotificationType = TB_NOTIFICATION_TYPES.ID " +
                "select ConfigurationValue from TB_CONFIGURATION where ConfigurationKey = 'EmailTemplate'";


            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            // SecuritySettings data
            DataTable tbl = ds.Tables[0];
            foreach (DataRow dr in tbl.Rows)
            {
                response.data.Add(new NotificationTemplate
                {
                    NotificationType = ADO.ToInt32(dr["NotificationType"]),
                    SendSMS = ADO.Toboolean(dr["SendSMS"]),
                    SMSTemplate = ADO.ToString(dr["SMSTemplate"]),
                    SendWhatsapp = ADO.Toboolean(dr["SendWhatsapp"]),
                    WhatsappTemplate = ADO.ToString(dr["WhatsappTemplate"]),
                    SendEmail = ADO.Toboolean(dr["SendEmail"]),
                    EmailSubject = ADO.ToString(dr["EmailSubject"]),
                    EmailMessage = ADO.ToString(dr["EmailMessage"]),
                    Notification = ADO.ToString(dr["NOTIFICATION"])
                });
            }
            // config value
            if (ds.Tables.Count > 1)
            {
                DataTable tbl1 = ds.Tables[1];
                foreach (DataRow dr1 in tbl1.Rows)
                {
                    response.keywords.Add(new ConfigurationValue
                    {
                        EmailValue = ADO.ToString(dr1["ConfigurationValue"])
                    });
                }
            }          
            return response;
        }
        public Int32 UpdateNotification(NotificationTemplate notificationtemplate, Int32 userID)
        {
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_NOTIFICATION_TEMPLATES";
                    cmd.Parameters.AddWithValue("ACTION", 1);

                    cmd.Parameters.AddWithValue("NotificationType", ADO.ToString(notificationtemplate.NotificationType));
                    cmd.Parameters.AddWithValue("SendSMS", ADO.ToString(notificationtemplate.SendSMS));
                    
                    cmd.Parameters.AddWithValue("SMSTemplate", ADO.ToString(notificationtemplate.SMSTemplate));
                    cmd.Parameters.AddWithValue("SendWhatsapp", ADO.ToString(notificationtemplate.SendWhatsapp));
                    cmd.Parameters.AddWithValue("WhatsappTemplate", ADO.ToString(notificationtemplate.WhatsappTemplate));
                    cmd.Parameters.AddWithValue("SendEmail", ADO.ToString(notificationtemplate.SendEmail));
                    cmd.Parameters.AddWithValue("EmailSubject", ADO.ToString(notificationtemplate.EmailSubject));
                    cmd.Parameters.AddWithValue("EmailMessage", ADO.ToString(notificationtemplate.EmailMessage));
                   
                   
                    cmd.Parameters.AddWithValue("UserID", userID);

                    cmd.ExecuteNonQuery();

                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
   
    }
}