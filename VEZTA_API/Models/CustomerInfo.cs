using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VEZTA.Models
{
    public class CustomerInfo
    {
        public string Flag { get; set; }
        public string Message { get; set; }
        public List<LicenseInfo> LicenseInfo { get; set; }
        public List<FacilityInfo> FacilityInfo { get; set; }
        public List<MenuGroupInfo> MenuGroupInfo { get; set; }
        public List<MenuInfo> MenuInfo { get; set; }
        public List<PostOfficeInfo> PostOfficeInfo { get; set; }
        public List<ConfigurationInfo> ConfigurationInfo { get; set; }
    }
    public class ConfigurationInfo
    {
        public string CONFIGURATION_KEY { get; set; }
        public string CONFIGURATION_VALUE { get; set; }
    }

    public class LicenseInfo
    {
        public int ID { get; set; }
        public string CUSTOMER_KEY { get; set; }
        public string EDITION_NAME { get; set; }
        public string LICENSE_KEY { get; set; }
        public string EXPIRY_DATE { get; set; }
        public int CHANGE_LOG_ID { get; set; }
    }

    public class FacilityInfo
    {
        public int ID { get; set; }
        public string CUSTOMER_ID { get; set; }
        public string FACILITY_LICENSE { get; set; }
        public string FACILITY_NAME { get; set; }
        public string ADDRESS { get; set; }
        public string EMIRATE_ID { get; set; }
        public bool IS_INACTIVE { get; set; }
        public string LICENSE_KEY { get; set; }
        public string ENROLL_DATE { get; set; }
        public string EXPIRY_DATE { get; set; }
        public string AMC_EXPIRY_DATE { get; set; }
        public string POST_OFFICE_ID { get; set; }
        public string POST_OFFICE { get; set; }
    }

    public class MenuGroupInfo
    {
        public int ID { get; set; }
        public string MENU_GROUP { get; set; }
        public string MENU_PATH { get; set; }
        public string MENU_ICON { get; set; }
        public string MENU_ORDER { get; set; }
        public bool IS_INACTIVE { get; set; }
        public string MAIN_GROUP_ID { get; set; }
    }
    public class MenuInfo
    {
        public int ID { get; set; }
        public string MENU_GROUP_ID { get; set; }
        public string MENU_NAME { get; set; }
        public string MENU_PATH { get; set; }
        public bool IS_INACTIVE { get; set; }
        public string MENU_ORDER { get; set; }
    }

    public class PostOfficeInfo
    {
        public int ID { get; set; }
        public string POSTOFFICE { get; set; }
        public string APIURL { get; set; }
        public bool isXML { get; set; }
    }
    public class CustomerInfoInput
    {
        public string CustomerKey { get; set; }
        public string LogID { get; set; }
    }
}