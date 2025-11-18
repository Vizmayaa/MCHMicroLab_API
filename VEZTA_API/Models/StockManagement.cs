using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace VEZTA.Models
{
    public class StockManagement
    {
        List<Stock> lstStock = new List<Stock>();
        static StockManagement objVer = null;

        public static StockManagement getInstance()
        {
            if (objVer == null)
            {
                objVer = new StockManagement();
                return objVer;
            }
            else
            {
                return objVer;
            }
        }
        public StockResponse stockVerification(Stock varStock)
        {
            StockResponse res = new StockResponse();
            SqlConnection con = new SqlConnection();
            try
            {
                string ItemCode = varStock.productSKU;
                decimal dmlQty = ConvertToDecimal(varStock.quantity);


                string strSQL = "SELECT SUM(QTY_IN - QTY_OUT - COMMIT_QTY) FROM TB_ITEM_TRANS " +
                                "INNER JOIN TB_AC_TRANS_HEADER ON TB_ITEM_TRANS.TRANS_ID = TB_AC_TRANS_HEADER.TRANS_ID " +
                                "INNER JOIN TB_ITEMS ON TB_ITEM_TRANS.ITEM_ID = TB_ITEMS.ID " +
                                "WHERE TRANS_TYPE IN(18, 46, 47, 25, 20, 26, 49, 51, 12, 22, 56) " +
                                "AND TB_ITEM_TRANS.STORE_ID = 1 AND TB_ITEMS.BARCODE = '" + ItemCode + "'";                 
               
                con.ConnectionString = ConfigurationManager.ConnectionStrings["WSMasterConnectionString"].ConnectionString;
                con.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strSQL;
                decimal dmlStock = ConvertToDecimal(cmd.ExecuteScalar());

                if (dmlStock >= dmlQty)
                {
                    res.flag = "1";
                    res.message = "available";
                }
                else
                {
                    res.flag = "0";
                    res.message = "not available";
                }
            }
            catch (Exception ex)
            {
                throw ex;                                        
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
            return res;
        }
        public decimal ConvertToDecimal(object value)
        {
            try
            {
                return Convert.ToDecimal(value);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}