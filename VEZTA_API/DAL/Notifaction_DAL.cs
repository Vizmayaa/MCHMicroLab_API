using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using VEZTA.Models;
using System.Net;
using System.Net.Mail;
namespace VEZTA.DAL
{
    public class Notifaction_DAL
    {
        public bool SendEmail(Int32 varUserID, string varEmailID, string varSubject, string varMessage)
        {
            try
            {
                string strSQL = "SELECT EmailSenderID, EmailSenderName, EmailSenderPassword, EmailSMTPHost, EmailSMTPPort, EmailEnableSSL FROM TB_NOTIFICATION_SETTINGS";
                DataTable tbl = ADO.GetDataTable(strSQL);
                if (tbl.Rows.Count > 0)
                {
                    string varSenderID = tbl.Rows[0]["EmailSenderID"].ToString();
                    string varSenderName = tbl.Rows[0]["EmailSenderName"].ToString();
                    string varSenderPassword = AzentLibrary.Library.DecryptString(tbl.Rows[0]["EmailSenderPassword"].ToString());
                    string varSMTPHost = tbl.Rows[0]["EmailSMTPHost"].ToString();
                    Int32 varSMTPPort = ADO.ToInt32(tbl.Rows[0]["EmailSMTPPort"]);
                    bool varSSL = Convert.ToBoolean(tbl.Rows[0]["EmailEnableSSL"]);

                    SmtpClient SmtpServer = new SmtpClient();
                    MailMessage mail = new MailMessage();
                    SmtpServer.Credentials = new System.Net.NetworkCredential(varSenderID, varSenderPassword);
                    SmtpServer.Port = varSMTPPort;
                    SmtpServer.Host = varSMTPHost;
                    SmtpServer.EnableSsl = varSSL;

                    mail.From = new MailAddress(varSenderID, varSenderName);
                    mail.To.Add(varEmailID);
                    mail.Subject = varSubject;
                    mail.IsBodyHtml = true;
                    mail.Body = varMessage;
                    SmtpServer.Send(mail);
                    mail.Dispose();

                    strSQL = "INSERT INTO TB_EMAIL_LOG(MailTo, MailSubject, MailMessage, MailSentSuccess, MailErrorDescription, MailTime, MailUserID) VALUES (" +
                            ADO.SQLString(varEmailID) + "," + ADO.SQLString(varSubject) + "," + ADO.SQLString(varMessage) + ", 1, '', GETUTCDATE(), " + varUserID + ")";

                    ADO.ExecuteNonQueryIgnoreException(strSQL);
                    return true;
                }

            }
            catch (Exception ex)
            {
                string strSQL = "INSERT INTO TB_EMAIL_LOG(MailTo, MailSubject, MailMessage, MailSentSuccess, MailErrorDescription, MailTime, MailUserID) VALUES (" +
                            ADO.SQLString(varEmailID) + "," + ADO.SQLString(varSubject) + "," + ADO.SQLString(varMessage) + ", 0, " +
                            ADO.SQLString(ex.Message) + ", GETUTCDATE(), " + varUserID + ")";

                ADO.ExecuteNonQueryIgnoreException(strSQL);

            }

            return false;
        }
       
        public bool SendSMS(Int32 varUserID, string vMobileNo, string varMessage)
        {
            return true;

        }
        public bool SendWhatsapp(Int32 varUserID, string vMobileNo, string varMessage)
        {
            return true;

        }
    }
}