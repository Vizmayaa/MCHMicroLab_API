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
    public class License_DAL
    {
        string conString = ConfigurationManager.ConnectionStrings["WSMasterConnectionString"].ToString();

        public List<License> GetAllLicense()
        {
            List<License> licenseList = new List<License>();

            using (SqlConnection connection = ADO.GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_LICENSE";
                cmd.Parameters.AddWithValue("ACTION", 0);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tbl = new DataTable();
                da.Fill(tbl);

                foreach (DataRow dr in tbl.Rows)
                {
                    licenseList.Add(new License
                    {
                        ID = Convert.ToInt32(dr["ID"]),
                        PRODUCT_NAME = dr["PRODUCT_NAME"].ToString(),
                        PRODUCT_ID = dr["PRODUCT_ID"].ToString(),
                        CUST_NAME = dr["CUST_NAME"].ToString(),
                        CUST_ID = dr["CUST_ID"].ToString(),
                        LICENSETYPES = dr["LICENSETYPES"].ToString(),
                        LICENSETYPE_ID = dr["LICENSETYPE_ID"].ToString(),
                        VALID_DAYS = Convert.ToString(dr["VALID_DAYS"]),
                       // SERIAL_NO = Convert.ToInt32(dr["SERIAL_NO"]),
                       LICENSE_KEY = Convert.ToString(dr["LICENSE_KEY"]),
                       // INSTALL_DATE = Convert.IsDBNull(dr["INSTALL_DATE"]) ? (DateTime?)null : Convert.ToDateTime(dr["INSTALL_DATE"]),
                       // EXPIRY_DATE = Convert.IsDBNull(dr["EXPIRY_DATE"]) ? (DateTime?)null : Convert.ToDateTime(dr["EXPIRY_DATE"])
                    });
                }
                connection.Close();
            }
            return licenseList;
        }

        public bool Insert(License license)
        {
            SqlConnection connection = ADO.GetConnection();
            SqlTransaction objtrans = connection.BeginTransaction();

            try
            { 
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.Transaction = objtrans;
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "SELECT COALESCE(MAX(SERIAL_NO),0) FROM TB_LICENSE";
                int intserial  = Convert.ToInt32(cmd.ExecuteScalar()) + 1;
                string licensekey = AzentLibrary.Library.GenerateKey(intserial.ToString("00000"));
                
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_LICENSE";
                cmd.Parameters.AddWithValue("ACTION", 1);                                       
                cmd.Parameters.AddWithValue("PRODUCT_ID", license.PRODUCT_ID);
                cmd.Parameters.AddWithValue("CUST_ID", license.CUST_ID);
                cmd.Parameters.AddWithValue("LICENSETYPE_ID", license.LICENSETYPE_ID);
                cmd.Parameters.AddWithValue("VALID_DAYS", license.VALID_DAYS);
                cmd.Parameters.AddWithValue("LICENSE_KEY", licensekey);
              
               // cmd.Parameters.AddWithValue("INSTALL_DATE", license.INSTALL_DATE);
             //   cmd.Parameters.AddWithValue("EXPIRY_DATE", license.EXPIRY_DATE);
                cmd.Parameters.AddWithValue("SERIAL_NO", intserial);


                cmd.ExecuteNonQuery();

                objtrans.Commit();
                   
                connection.Close();
                return true;
                 
            }
            catch (Exception ex)
            {
                objtrans.Rollback();
                connection.Close();
                throw ex;
            }
        }
        public License GetItems(int id)
        {
            License license = new License();
            try
            {
                string strSQL = "SELECT TB_LICENSE.ID, TB_LICENSE.PRODUCT_ID, TB_LICENSE.CUST_ID, TB_LICENSE.LICENSETYPE_ID, TB_LICENSE.VALID_DAYS, TB_LICENSE.LICENSE_KEY, TB_LICENSE.SERIAL_NO, TB_CUSTOMER.CUST_NAME, TB_PRODUCTS.PRODUCT_NAME, TB_LICENSE_TYPES.LICENSETYPES " +
"FROM TB_LICENSE " +
"INNER JOIN TB_CUSTOMER ON TB_LICENSE.CUST_ID = TB_CUSTOMER.ID " +
"INNER JOIN TB_PRODUCTS ON TB_LICENSE.PRODUCT_ID = TB_PRODUCTS.ID " +
"INNER JOIN TB_LICENSE_TYPES ON TB_LICENSE.LICENSETYPE_ID = TB_LICENSE_TYPES.ID " +
"WHERE TB_LICENSE.ID = " + id;


                DataTable tbl = ADO.GetDataTable(strSQL, "License");
                if (tbl.Rows.Count > 0)
                {
                    DataRow dr = tbl.Rows[0];

                    license.ID = Convert.ToInt32(dr["ID"]);
                    license.PRODUCT_NAME = dr["PRODUCT_NAME"].ToString();
                    license.PRODUCT_ID = dr["PRODUCT_ID"].ToString();
                    license.CUST_NAME = dr["CUST_NAME"].ToString();
                    license.CUST_ID = dr["CUST_ID"].ToString();
                    license.LICENSETYPES = dr["LICENSETYPES"].ToString();
                    license.LICENSETYPE_ID = dr["LICENSETYPE_ID"].ToString();
                    license.VALID_DAYS = Convert.ToString(dr["VALID_DAYS"]);
                    license.LICENSE_KEY = Convert.ToString(dr["LICENSE_KEY"]);
                  //  license.INSTALL_DATE = Convert.ToDateTime(dr["INSTALL_DATE"]);
                    license.SERIAL_NO = Convert.ToInt32(dr["SERIAL_NO"]);
                    //license.EXPIRY_DATE = Convert.ToDateTime(dr["EXPIRY_DATE"]);

                }
            }
            catch (Exception ex)
            {

            }

            return license;
        }
        public bool Update(License license)
        {
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_LICENSE";
                    cmd.Parameters.AddWithValue("ACTION", 3);

                    cmd.Parameters.AddWithValue("ID", license.ID);
                    cmd.Parameters.AddWithValue("VALID_DAYS", license.VALID_DAYS);
                    cmd.Parameters.AddWithValue("LICENSE_KEY", license.LICENSE_KEY);
                    cmd.Parameters.AddWithValue("CUST_ID", license.CUST_ID);
                    cmd.Parameters.AddWithValue("PRODUCT_ID", license.PRODUCT_ID);
                    cmd.Parameters.AddWithValue("LICENSETYPE_ID", license.LICENSETYPE_ID);
                   // cmd.Parameters.AddWithValue("INSTALL_DATE", license.INSTALL_DATE);
                    //cmd.Parameters.AddWithValue("EXPIRY_DATE", license.EXPIRY_DATE);
                    cmd.Parameters.AddWithValue("SERIAL_NO", license.SERIAL_NO);


                    cmd.ExecuteNonQuery();

                    connection.Close();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool DeleteLicense(int id)
        {
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_LICENSE";
                    cmd.Parameters.AddWithValue("ACTION", 4);
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.ExecuteNonQuery();

                    connection.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}