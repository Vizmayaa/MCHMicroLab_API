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
    public class Products_DAL
    {
        string conString = ConfigurationManager.ConnectionStrings["WSMasterConnectionString"].ToString();

        public List<Products> GetAllProducts()
        {
            List<Products> productList = new List<Products>();
            using (SqlConnection connection = ADO.GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_PRODUCTS";
                cmd.Parameters.AddWithValue("ACTION", 0);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tbl = new DataTable();
                da.Fill(tbl);

                foreach (DataRow dr in tbl.Rows)
                {
                    productList.Add(new Products
                    {
                        ID = Convert.ToInt32(dr["ID"]),
                        PRODUCT_CODE = Convert.ToString(dr["PRODUCT_CODE"]),
                        PRODUCT_NAME = Convert.ToString(dr["PRODUCT_NAME"]),
                        PRODUCT_KEY = Convert.ToString(dr["PRODUCT_KEY"]),

                    });
                }
                connection.Close();
            }
            return productList;
        }


        public bool Insert(Products products)
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
                    "SELECT @NEXTSERIAL = COALESCE(MAX(SERIAL_NO), 000) + 1 FROM TB_PRODUCTS" +
                    "IF @NEXTSERIAL IS NULL" +
                    "SET @NEXTSERIAL = 1" +
                    "SET @PRODUCT_CODE = 'VZ' + RIGHT('000' + CAST(@NEXTSERIAL AS VARCHAR), 3)";

                cmd.CommandText = "SELECT COALESCE(MAX(SERIAL_NO),0) FROM TB_PRODUCTS";

                 int intserial = Convert.ToInt32(cmd.ExecuteScalar()) + 1;
                string productkey = AzentLibrary.Library.GenerateKey(intserial.ToString("00000"));


                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_PRODUCTS";
                cmd.Parameters.AddWithValue("ACTION", 1);
                cmd.Parameters.AddWithValue("PRODUCT_CODE", products.PRODUCT_CODE);
                cmd.Parameters.AddWithValue("PRODUCT_NAME", products.PRODUCT_NAME);
                cmd.Parameters.AddWithValue("PRODUCT_KEY", productkey);
                cmd.Parameters.AddWithValue("SERIAL_NO",intserial);
       
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






        public Products GetItems(int id)
        {
            Products products = new Products();
            try
            {
                string strSQL = "SELECT ID,PRODUCT_CODE,PRODUCT_NAME,PRODUCT_KEY FROM TB_PRODUCTS WHERE TB_PRODUCTS.ID =" + id;

                DataTable tbl = ADO.GetDataTable(strSQL, "Products");
                if (tbl.Rows.Count > 0)
                {
                    DataRow dr = tbl.Rows[0];

                    products.ID = Convert.ToInt32(dr["ID"]);
                    products.PRODUCT_CODE = Convert.ToString(dr["PRODUCT_CODE"]);
                    products.PRODUCT_NAME = Convert.ToString(dr["PRODUCT_NAME"]);
                    products.PRODUCT_KEY = Convert.ToString(dr["PRODUCT_KEY"]);
                   
                }
            }
            catch (Exception ex)
            {

            }
            return products;
        }
        public bool Update(Products products)
        {
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_PRODUCTS";
                    cmd.Parameters.AddWithValue("ACTION", 3);

                    cmd.Parameters.AddWithValue("ID", products.ID);
                    cmd.Parameters.AddWithValue("PRODUCT_CODE", products.PRODUCT_CODE);
                    cmd.Parameters.AddWithValue("PRODUCT_NAME", products.PRODUCT_NAME);
                    cmd.Parameters.AddWithValue("PRODUCT_KEY", products.PRODUCT_KEY);

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
        public bool DeleteProduct(int id)
        {
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_PRODUCTS";
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