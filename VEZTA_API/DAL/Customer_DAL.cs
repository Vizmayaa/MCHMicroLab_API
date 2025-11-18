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
    public class Customer_DAL
    {
        string conString = ConfigurationManager.ConnectionStrings["WSMasterConnectionString"].ToString();

        public List<Customer> GetAllCustomers()
        {
            List<Customer>customerList = new List<Customer>();
            using (SqlConnection connection = ADO.GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_CUSTOMER";
                cmd.Parameters.AddWithValue("ACTION", 0);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tbl = new DataTable();
                da.Fill(tbl);

                foreach (DataRow dr in tbl.Rows)
                {
                    customerList.Add(new Customer
                    {
                        ID = Convert.ToInt32(dr["ID"]),
                        CUST_CODE = Convert.ToString(dr["CUST_CODE"]),
                        CUST_NAME = Convert.ToString(dr["CUST_NAME"]),
                        CONTACT_NAME = Convert.ToString(dr["CONTACT_NAME"]),
                        ADDRESS1 = Convert.ToString(dr["ADDRESS1"]),
                        ADDRESS2 = Convert.ToString(dr["ADDRESS2"]),
                        ADDRESS3 = Convert.ToString(dr["ADDRESS3"]),
                        ZIP = Convert.ToString(dr["ZIP"]),
                        CITY = Convert.ToString(dr["CITY"]),
                        STATE = Convert.ToString(dr["STATE"]),
                        COUNTRY_NAME = dr["COUNTRY_NAME"].ToString(),
                        COUNTRY_ID = dr["COUNTRY_ID"].ToString(),
                        PHONE = Convert.ToString(dr["PHONE"]),
                        EMAIL = Convert.ToString(dr["EMAIL"]),
                        REGD_DATE = Convert.IsDBNull(dr["REGD_DATE"]) ? (DateTime?)null : Convert.ToDateTime(dr["REGD_DATE"]),
                        
                        VAT_REGN_NO = Convert.ToString(dr["VAT_REGN_NO"]),
                    });
                }
                connection.Close();
            }
            return customerList;
        }

        //Insert Customers
        public bool Insert(Customer customer)
        {
            SqlConnection connection = ADO.GetConnection();
            SqlTransaction objtrans = connection.BeginTransaction();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.Transaction = objtrans;
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "DECLARE @NEXTSERIAL INT" +
                    "SELECT @NEXTSERIAL = COALESCE(MAX(SERIAL_NO), 000) + 1 FROM TB_CUSTOMER" +
                    "IF @NEXTSERIAL IS NULL" +
                    "SET @NEXTSERIAL = 1" +
                    "SET @CUST_CODE = 'C' + RIGHT( '000' +  CAST(@NEXTSERIAL AS VARCHAR), 4)";

                cmd.CommandText = "SELECT COALESCE(MAX(SERIAL_NO),0) FROM TB_CUSTOMER";

                int intserial = Convert.ToInt32(cmd.ExecuteScalar()) + 1;

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_CUSTOMER";
                    cmd.Parameters.AddWithValue("ACTION", 1);

                    cmd.Parameters.AddWithValue("CUST_CODE", customer.CUST_CODE);
                    cmd.Parameters.AddWithValue("CUST_NAME", customer.CUST_NAME);
                    cmd.Parameters.AddWithValue("CONTACT_NAME", customer.CONTACT_NAME);
                    cmd.Parameters.AddWithValue("ADDRESS1", customer.ADDRESS1);
                    cmd.Parameters.AddWithValue("ADDRESS2", customer.ADDRESS2);
                    cmd.Parameters.AddWithValue("ADDRESS3", customer.ADDRESS3);
                    cmd.Parameters.AddWithValue("ZIP", customer.ZIP);
                    cmd.Parameters.AddWithValue("CITY", customer.CITY);
                    cmd.Parameters.AddWithValue("STATE", customer.STATE);
                    cmd.Parameters.AddWithValue("COUNTRY_ID", customer.COUNTRY_ID);
                    cmd.Parameters.AddWithValue("PHONE", customer.PHONE);
                    cmd.Parameters.AddWithValue("EMAIL", customer.EMAIL);
                    cmd.Parameters.AddWithValue("REGD_DATE", customer.REGD_DATE);
                    cmd.Parameters.AddWithValue("VAT_REGN_NO", customer.VAT_REGN_NO);
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
        public Customer GetItems(int id)
        {
            Customer customer = new Customer();
            try
            {
                string strSQL = "SELECT TB_CUSTOMER.ID,TB_CUSTOMER.CUST_CODE,TB_CUSTOMER.CUST_NAME,TB_CUSTOMER.CONTACT_NAME," +
                    "TB_CUSTOMER.ADDRESS1,TB_CUSTOMER.ADDRESS2,TB_CUSTOMER.ADDRESS3,TB_CUSTOMER.ZIP," +
                    "TB_CUSTOMER.CITY,TB_CUSTOMER.STATE,TB_CUSTOMER.COUNTRY_ID," +
                    "TB_CUSTOMER.PHONE,TB_CUSTOMER.EMAIL,TB_CUSTOMER.REGD_DATE,TB_CUSTOMER.VAT_REGN_NO," +
                                "TB_COUNTRY.COUNTRY_NAME " +

                                "FROM TB_CUSTOMER INNER JOIN TB_COUNTRY ON TB_CUSTOMER.COUNTRY_ID = TB_COUNTRY.ID " +
                                " WHERE TB_CUSTOMER.ID = " + id;
              
                DataTable tbl = ADO.GetDataTable(strSQL, "Customers");
                if (tbl.Rows.Count > 0)
                {
                    DataRow dr = tbl.Rows[0];

                    customer.ID = Convert.ToInt32(dr["ID"]);
                    customer.CUST_CODE = Convert.ToString(dr["CUST_CODE"]);
                    customer.CUST_NAME = Convert.ToString(dr["CUST_NAME"]);
                    customer.CONTACT_NAME = Convert.ToString(dr["CONTACT_NAME"]);
                    customer.ADDRESS1 = Convert.ToString(dr["ADDRESS1"]);
                    customer.ADDRESS2 = Convert.ToString(dr["ADDRESS2"]);
                    customer.ADDRESS3 = Convert.ToString(dr["ADDRESS3"]);
                    customer.ZIP = Convert.ToString(dr["ZIP"]);
                    customer.CITY = Convert.ToString(dr["CITY"]);
                    customer.STATE = Convert.ToString(dr["STATE"]);
                    customer.PHONE = Convert.ToString(dr["PHONE"]);
                    customer.EMAIL = Convert.ToString(dr["EMAIL"]);
                    customer.VAT_REGN_NO = Convert.ToString(dr["VAT_REGN_NO"]);
                    customer.REGD_DATE = Convert.ToDateTime(dr["REGD_DATE"]);
                    customer.COUNTRY_NAME = dr["COUNTRY_NAME"].ToString();
                    customer.COUNTRY_ID = dr["COUNTRY_ID"].ToString();
                }
            }
            catch (Exception ex)
            {

            }
            return customer;
        }
        public bool Update(Customer customer)
        {
            SqlConnection connection = ADO.GetConnection();
            SqlTransaction objtrans = connection.BeginTransaction();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.Transaction = objtrans;
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "DECLARE @NEXTSERIAL INT" +
                    "SELECT @NEXTSERIAL = COALESCE(MAX(SERIAL_NO), 000) + 1 FROM TB_CUSTOMER" +
                    "IF @NEXTSERIAL IS NULL" +
                    "SET @NEXTSERIAL = 1" +
                    "SET @CUST_CODE = 'C' + RIGHT( '000' +  CAST(@NEXTSERIAL AS VARCHAR), 4)";

                cmd.CommandText = "SELECT COALESCE(MAX(SERIAL_NO),0) FROM TB_CUSTOMER";

                int intserial = Convert.ToInt32(cmd.ExecuteScalar()) + 1;

                cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_CUSTOMER";
                    cmd.Parameters.AddWithValue("ACTION", 3);

                    cmd.Parameters.AddWithValue("ID", customer.ID);
                    cmd.Parameters.AddWithValue("CUST_CODE", customer.CUST_CODE);
                    cmd.Parameters.AddWithValue("CUST_NAME", customer.CUST_NAME);
                    cmd.Parameters.AddWithValue("CONTACT_NAME", customer.CONTACT_NAME);
                    cmd.Parameters.AddWithValue("ADDRESS1", customer.ADDRESS1);
                    cmd.Parameters.AddWithValue("ADDRESS2", customer.ADDRESS2);
                    cmd.Parameters.AddWithValue("ADDRESS3", customer.ADDRESS3);
                    cmd.Parameters.AddWithValue("ZIP", customer.ZIP);
                    cmd.Parameters.AddWithValue("CITY", customer.CITY);
                    cmd.Parameters.AddWithValue("STATE", customer.STATE);
                    cmd.Parameters.AddWithValue("COUNTRY_ID", customer.COUNTRY_ID);
                    cmd.Parameters.AddWithValue("PHONE", customer.PHONE);
                    cmd.Parameters.AddWithValue("EMAIL", customer.EMAIL);
                    cmd.Parameters.AddWithValue("REGD_DATE", customer.REGD_DATE);
                    cmd.Parameters.AddWithValue("VAT_REGN_NO", customer.VAT_REGN_NO);

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
        public bool DeleteCustomer(int id)
        {
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_CUSTOMER";
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