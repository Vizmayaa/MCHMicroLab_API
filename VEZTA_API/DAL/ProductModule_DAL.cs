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
    public class ProductModule_DAL
    {
        string conString = ConfigurationManager.ConnectionStrings["WSMasterConnectionString"].ToString();

        public List<ProductModule> GetAllProductModules()
        {
            List<ProductModule> userList = new List<ProductModule>();


            using (SqlConnection connection = ADO.GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_PRODUCTMODULES";
                cmd.Parameters.AddWithValue("ACTION", 0);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tbl = new DataTable();
                da.Fill(tbl);

                foreach (DataRow dr in tbl.Rows)
                {
                    userList.Add(new ProductModule
                    {
                        ID = Convert.ToInt32(dr["ID"]),
                        MODULE_CODE = Convert.ToString(dr["MODULE_CODE"]),
                        MODULE_NAME = Convert.ToString(dr["MODULE_NAME"]),
                        PRODUCT_NAME = dr["PRODUCT_NAME"].ToString(),
                        PRODUCT_ID = dr["PRODUCT_ID"].ToString()
                    });
                }
                connection.Close();
            }
            return userList;
        }

        public bool Insert(ProductModule productModule)
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
                    "SELECT @NEXTSERIAL = COALESCE(MAX(SERIAL_NO), 000) + 1 FROM TB_PRODUCT_MODULES" +
                    "IF @NEXTSERIAL IS NULL" +
                    "SET @NEXTSERIAL = 1" +
                    "SET @MODULE_CODE = 'VZ' + RIGHT('000' + CAST(@NEXTSERIAL AS VARCHAR), 3)";

                cmd.CommandText = "SELECT COALESCE(MAX(SERIAL_NO),0) FROM TB_PRODUCT_MODULES";

                int intserial = Convert.ToInt32(cmd.ExecuteScalar()) + 1;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_PRODUCTMODULES";
                cmd.Parameters.AddWithValue("ACTION", 1);
                cmd.Parameters.AddWithValue("MODULE_CODE", productModule.MODULE_CODE);
                cmd.Parameters.AddWithValue("MODULE_NAME", productModule.MODULE_NAME);
                cmd.Parameters.AddWithValue("PRODUCT_ID", productModule.PRODUCT_ID);
                // cmd.Parameters.AddWithValue("SERIAL_NO", intserial);

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

        public ProductModule GetItems(int id)
        {
            ProductModule module = new ProductModule();
            try
            {

                string strSQL = "SELECT TB_PRODUCT_MODULES.ID, TB_PRODUCT_MODULES.MODULE_CODE,TB_PRODUCT_MODULES.MODULE_NAME," +
                                "TB_PRODUCT_MODULES.PRODUCT_ID,TB_PRODUCTS.PRODUCT_NAME " +
                                "FROM TB_PRODUCT_MODULES INNER JOIN TB_PRODUCTS ON TB_PRODUCT_MODULES.PRODUCT_ID = TB_PRODUCTS.ID " +
                                " WHERE TB_PRODUCT_MODULES.ID = " + id;

                DataTable tbl = ADO.GetDataTable(strSQL, "ProductModules");
                if (tbl.Rows.Count > 0)
                {
                    DataRow dr = tbl.Rows[0];

                    module.ID = Convert.ToInt32(dr["ID"]);
                    module.MODULE_CODE = Convert.ToString(dr["MODULE_CODE"]);
                    module.MODULE_NAME = Convert.ToString(dr["MODULE_NAME"]);

                    module.PRODUCT_NAME = dr["PRODUCT_NAME"].ToString();
                    module.PRODUCT_ID = dr["PRODUCT_ID"].ToString();
                  
                }
            }
            catch (Exception ex)
            {

            }

            return module;
        }
        public bool Update(ProductModule productModule)
        {
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_PRODUCTMODULES";
                    cmd.Parameters.AddWithValue("ACTION", 3);

                    cmd.Parameters.AddWithValue("ID", productModule.ID);
                    cmd.Parameters.AddWithValue("MODULE_CODE", productModule.MODULE_CODE);
                    cmd.Parameters.AddWithValue("MODULE_NAME", productModule.MODULE_NAME);
                    cmd.Parameters.AddWithValue("PRODUCT_ID", productModule.PRODUCT_ID);
      
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

        public bool DeleteProductModule(int id)
        {
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_PRODUCTMODULES";
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