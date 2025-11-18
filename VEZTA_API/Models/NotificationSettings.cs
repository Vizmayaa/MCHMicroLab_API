using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VEZTA.Models
{
    public class NotificationSettings
    {
        public string EmailSenderID { get; set; }
        public string EmailSenderName { get; set; }
        public string EmailSenderPassword { get; set; }
        public string EmailSMTPHost { get; set; }
        public int EmailSMTPPort { get; set; }
        public bool EmailEnableSSL { get; set; }
        public bool EmailIsInactive { get; set; }
        public string SMSProviderURL { get; set; }
        public string SMSUserID { get; set; }
        public string SMSPassword { get; set; }
        public string SMSMobileNo { get; set; }
        public bool SMSIsInactive { get; set; }
        public string WhatsappSource { get; set; }
        public string WhatsappNumber { get; set; }
        public bool WhatsappIsInactive { get; set; }
    }
    public class NotificationSettingsResponse
    {
        public string flag { get; set; }
        public string message { get; set; }
        public List<NotificationSettings> data { get; set; }
    }
    public class NotificationTemplate
    {
        public int NotificationType { get; set; }
        public bool SendSMS { get; set; }
        public string SMSTemplate { get; set; }
        public bool SendWhatsapp { get; set; }
        public string WhatsappTemplate { get; set; }
        public bool SendEmail { get; set; }
        public string EmailSubject { get; set; }
        public string EmailMessage { get; set; }
        public string Notification { get; set; }


    }
    public class NotificationTemplateResponse
    {
        public string flag { get; set; }
        public string message { get; set; }
        public List<NotificationTemplate> data { get; set; }
        public List<ConfigurationValue> keywords { get; set; }
        //public List<string> keywords { get; set; }
    }
    public class ConfigurationValue
    {
        public string EmailValue { get; set; }
    }
}