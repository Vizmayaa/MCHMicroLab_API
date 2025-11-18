using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VEZTA.Models
{
    public class UserLab
    {
        public int USER_ID { get; set; }
        public int DEPT_ID { get; set; }
        public string DEPT_NAME { get; set; }
        public string USER_NAME { get; set; }
        public string LOGIN_NAME { get; set; }
        public string PASSWORD { get; set; }
        public bool IS_ADMIN { get; set; }
        public bool IS_INACTIVE { get; set; }
        public bool IS_LAB_USER { get; set; }
        //public bool IS_COLLECTION_USER { get; set; }
        public bool IS_VERIFY_REPORT { get; set; }
        public bool IS_HOSPITAL_USER { get; set; }
        public string HOSPITAL_ID { get; set; }
        public string HOSPITAL_NAME { get; set; }
    }
    public class UserLabLoginResponse
    {
        public int flag { get; set; }
        public string message { get; set; }
       // public UserLab data { get; set; }
        public List<UserLab> data { get; set; }

    }
    public class UserLabVerificationInput
    {
        public string LOGIN_NAME { get; set; }
        public string PASSWORD { get; set; }
        
    }
    public class UserLabInsertInput
    {
        public int ID { get; set; }
        public int DEPT_ID { get; set; }
        public string USER_NAME { get; set; }
        public string LOGIN_NAME { get; set; }
        public string PASSWORD { get; set; }
        public bool IS_ADMIN { get; set; }
        public bool IS_LAB_USER { get; set; }
        public bool IS_HOSPITAL_USER { get; set; }
        public bool IS_INACTIVE { get; set; }
        public string HOSPITAL_ID { get; set; }
    }

    public class UserLabInsertResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
    }
    public class UserLabSelectResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
        public UserLab Data { get; set; }
    }
}