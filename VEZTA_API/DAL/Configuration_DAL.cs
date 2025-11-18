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
    public class Configuration_DAL
    {
        
        public string GetConfigurationValue(string Key)
        {
            try
            {
                string strSQL = "SELECT ConfigurationValue FROM TB_CONFIGURATION WHERE ConfigurationKey = " + ADO.SQLString(Key);

                return ADO.ExecuteScalar(strSQL).ToString();
            }
            catch (Exception ex)
            {
                return "";
            }

        }
         
    }
}