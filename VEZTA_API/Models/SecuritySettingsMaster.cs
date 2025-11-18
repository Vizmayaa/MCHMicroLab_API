using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VEZTA.Models
{
    //public class SecuritySettingsMaster
    //{
    //    public int AccountLockAttempt { get; set; }
    //    public int AccountLockDuration { get; set; }
    //    public int AccountLockFailedLogin { get; set; }
    //    public int AlertEmailOnPasswordChange { get; set; }
    //    public int AlertSMSOnPasswordChange { get; set; }
    //    public int DisableUserOnInactiveDays { get; set; }
    //    public int LowercaseCharacters { get; set; }
    //    public int MinimumCategoriesRequired { get; set; }
    //    public int MinimumLength { get; set; }
    //    public int Numbers { get; set; }
    //    public int OTPEmailOnPasswordChange { get; set; }
    //    public int OTPSMSOnPasswordChange { get; set; }
    //    public int PasswordAge { get; set; }
    //    public int PasswordRepeatCycle { get; set; }
    //    public int PasswordValidationRequired { get; set; }
    //    public int SpecialCharacters { get; set; }
    //    public string UnauthorizedBannerMessage { get; set; }
    //    public int UppercaseCharacters { get; set; }
    //    public int UserMustChangePasswordOnLogin { get; set; }
    //}
    public class SecuritySettingMasterResponse
    {
        public string flag { get; set; }
        public string message { get; set; }
        public List<SecuritySettingsMaster> data { get; set; }
    }
}