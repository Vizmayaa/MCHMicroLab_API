using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VEZTA.Models
{
    public class User
    {
        public int UserID { get; set; }
        public bool IsClinician { get; set; }
        public int ClinicianID { get; set; }      
        public string UserName { get; set; }
        public string LoginName { get; set; }
        public string Password { get; set; }
        public int UserRoleID { get; set; }
        public string UserRoleName { get; set; }
        public DateTime? DateofBirth { get; set; }
        public int GenderID { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Whatsapp { get; set; }
        public DateTime? LoginExpiryDate { get; set; }
        public string LoginExpiryReason { get; set; }
        public bool IsInactive { get; set; }
        public string InactiveReason { get; set; }    
        public bool IsLocked { get; set; }
        public DateTime? LockDateFrom { get; set; }
        public DateTime? LockDateTo { get; set; }
        public string LockReason { get; set; }
        public string Token { get; set; }
        public bool IsActiveDirectoryUser { get; set; }
        public string PhotoFile { get; set; }
       
        public string Gender { get; set; }
        public bool ChangePasswordOnLogin { get; set; }
        public List<UserFacility> user_facility { get; set; }

    }
    public class UserFacility
    {
        public int ID { get; set; }
        public int FacilityID { get; set; }

    }
    public class UserResponse
    {
        public string flag { get; set; }
        public string message { get; set; }
        public User data { get; set; }
        
    }
    public class UserLoginResponse
    {
        public string flag { get; set; }
        public string message { get; set; }         
        public User data { get; set; }
        public List<UserMenu> menus { get; set; }
        
    }
    public class UserVerificationInput
    {
        public string LoginName { get; set; }
        public string Password { get; set; }
        public string LocalIP { get; set; }
        public string ComputerName { get; set; }
        public string DomainName { get; set; }
        public string ComputerUser { get; set; }
        public string InternetIP { get; set; }
        public string SystemTimeUTC { get; set; }
        public bool ForceLogin { get; set; }


    }
    public class UserLogout
    {
        public string Token { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }   
    public class UserMenu
    {
        public string id { get; set; }
        public string GroupID { get; set; }
        public string text { get; set; }
        public string icon { get; set; }
        public string path { get; set; }
       // public List<UserMenuItem> items { get; set; }
    }
    public class UserMenuItem
    {
        public string id { get; set; }
        public string text { get; set; }
        public string path { get; set; }
    }
    
    public class UserActivityLoglnput
    {
        public int ID { get; set; }
        public int USER_ID { get; set; }
        public string TITLE { get; set; }
        public int ACTION { get; set; }
        public string TOKEN { get; set; }

    }
}