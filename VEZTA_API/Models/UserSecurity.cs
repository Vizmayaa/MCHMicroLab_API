using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VEZTA.Models
{
    public class UserSecurity
    {
        public bool Numbers { get; set; }
        public bool UppercaseCharacters { get; set; }
        public bool LowercaseCharacters { get; set; }
        public bool SpecialCharacters { get; set; }
        public int MinimumLength { get; set; }
        public bool PasswordValidationRequired { get; set; }

        //public int PasswordRepeatCycle { get; set; }
    }
    public class UserSecurityLogin
    {
        public string LoginName { get; set; }
    }

    public class UserSecurityResponse
    {
        public int flag { get; set; }
        public string message { get; set; }
        public List<UserSecurity> data { get; set; }
        public List<UserSecurityLogin> Login { get; set; }
    }
}