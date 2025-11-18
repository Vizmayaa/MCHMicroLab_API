using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace VEZTA.Models
{
    public class OrderManagement
    {
        static OrderManagement objVer = null;
        public static OrderManagement getInstance()
        {
            if (objVer == null)
            {
                objVer = new OrderManagement();
                return objVer;
            }
            else
            {
                return objVer;
            }
        }
        private decimal ConvertToDecimal(object value)
        {
            decimal dmlValue = 0;
            try
            {
                dmlValue = Convert.ToDecimal(value);
            }
            catch (Exception ex) { }

            return dmlValue;
        }
        private Int32 ConvertToInt32(object value)
        {
            Int32 intValue = 0;
            try
            {
                intValue = Convert.ToInt32(value);
            }
            catch (Exception ex) { }

            return intValue;
        }
        private string SQLString(string value)
        {
            value = value.Trim();
            value = "'" + value.Replace("'", "''") + "'";
            return value;
        }

        public bool CommitWebOrder(Order varOrder)
        {
            SqlConnection objCon = new SqlConnection();

            try
            {
                objCon.ConnectionString = ConfigurationManager.ConnectionStrings["WSMasterConnectionString"].ConnectionString;
                objCon.Open();


                DataTable tblItems = new DataTable();
                tblItems.Columns.Add("ITEM_CODE", typeof(System.String));
                tblItems.Columns.Add("QUANTITY", typeof(System.Decimal));
                tblItems.Columns.Add("PRICE", typeof(System.Decimal));
                tblItems.Columns.Add("DISCOUNT", typeof(System.Decimal));
                tblItems.Columns.Add("AMOUNT", typeof(System.Decimal));


                DataTable tblPayment = new DataTable();
                tblPayment.Columns.Add("TENDER_NAME", typeof(System.String));
                tblPayment.Columns.Add("AMOUNT", typeof(System.Decimal));
                tblPayment.Columns.Add("AMOUNT_FC", typeof(System.Decimal));
                tblPayment.Columns.Add("REFERENCE", typeof(System.String));

                foreach (Product p in varOrder.products)
                {
                    DataRow dr = tblItems.NewRow();

                    dr["ITEM_CODE"] = p.productSKU;
                    dr["QUANTITY"] = ConvertToDecimal(p.quantity);
                    dr["PRICE"] = ConvertToDecimal(p.price);
                    dr["DISCOUNT"] = ConvertToDecimal(p.discount);
                    dr["AMOUNT"] = ConvertToDecimal(p.amount);

                    tblItems.Rows.Add(dr);
                }


                foreach (Tender t in varOrder.tenders)
                {
                    DataRow dr = tblPayment.NewRow();

                    dr["TENDER_NAME"] = t.tenderName;
                    dr["AMOUNT"] = ConvertToDecimal(t.amountLocalCurrency);
                    dr["AMOUNT_FC"] = ConvertToDecimal(t.amount);
                    dr["REFERENCE"] = t.reference;

                    tblPayment.Rows.Add(dr);
                }



                SqlTransaction objTrans = objCon.BeginTransaction();

                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = objCon;
                    cmd.Transaction = objTrans;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_COMMIT_ORDER";
                    cmd.Parameters.AddWithValue("@ORDER_NO", varOrder.orderNo);
                    cmd.Parameters.AddWithValue("@ORDER_DATE", varOrder.orderDate);
                    cmd.Parameters.AddWithValue("@REF_NO", varOrder.referenceNo);
                    cmd.Parameters.AddWithValue("@IS_RETURN", ConvertToInt32(varOrder.isReturn));
                    cmd.Parameters.AddWithValue("@RECALL_ORDER_NO", varOrder.recallOrderNo);
                    cmd.Parameters.AddWithValue("@CUST_CODE", varOrder.customerAccountNo);
                    cmd.Parameters.AddWithValue("@CUST_NAME", varOrder.customerName);
                    cmd.Parameters.AddWithValue("@DEL_NAME", varOrder.shippingName);
                    cmd.Parameters.AddWithValue("@ADDRESS1", varOrder.address1);
                    cmd.Parameters.AddWithValue("@ADDRESS2", varOrder.address2);
                    cmd.Parameters.AddWithValue("@ADDRESS3", varOrder.address3);
                    cmd.Parameters.AddWithValue("@ZIP", varOrder.zip);
                    cmd.Parameters.AddWithValue("@AREA", varOrder.area);
                    cmd.Parameters.AddWithValue("@STATE", varOrder.state);
                    cmd.Parameters.AddWithValue("@COUNTRY", varOrder.country);
                    cmd.Parameters.AddWithValue("@MOBILE1", varOrder.mobile1);
                    cmd.Parameters.AddWithValue("@MOBILE2", varOrder.mobile2);
                    cmd.Parameters.AddWithValue("@EMAIL", varOrder.email);
                    cmd.Parameters.AddWithValue("@CURRENCY", varOrder.currency);
                    cmd.Parameters.AddWithValue("@GROSS_AMOUNT", ConvertToDecimal(varOrder.grossAmount));
                    cmd.Parameters.AddWithValue("@SHIPPING_CHARGE", ConvertToDecimal(varOrder.shippingCharge));
                    cmd.Parameters.AddWithValue("@NET_AMOUNT", ConvertToDecimal(varOrder.netAmount));
                    cmd.Parameters.AddWithValue("@UDT_ENTRY", tblItems);
                    cmd.Parameters.AddWithValue("@UDT_PAYMENT", tblPayment);
                    cmd.ExecuteNonQuery();

                    objTrans.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    objTrans.Rollback();
                    throw ex;
                }

            }
            catch (Exception ex)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = objCon;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO TB_ORDER_LOG(LOG_TIME, ORDER_NO, LOG_REMARKS) VALUES(GETDATE()," + SQLString(varOrder.orderNo) + "," + SQLString(ex.Message) + ")";
                cmd.ExecuteNonQuery();
            }
            finally
            {
                if (objCon.State == ConnectionState.Open)
                    objCon.Close();
            }
            return false;

        }

        public Response CancelWebOrder(CancelOrder varOrder)
        {
            SqlConnection objCon = new SqlConnection();
            Int32 lngOrderID = 0;
            Int32 lngSaleID = 0;
            bool blnCancelled = false;

            Response varResponse = new Response();

            try
            {
                objCon.ConnectionString = ConfigurationManager.ConnectionStrings["WSMasterConnectionString"].ConnectionString;
                objCon.Open();


                try
                {
                    string strSQL = "SELECT ID, SALE_ID, IS_CANCELLED FROM TB_ORDER WHERE ORDER_NO = " + SQLString(varOrder.orderNo);
                    SqlDataAdapter da = new SqlDataAdapter(strSQL, objCon);
                    DataTable tbl = new DataTable();
                    da.Fill(tbl);

                    if (tbl.Rows.Count > 0)
                    {
                        lngOrderID = ConvertToInt32(tbl.Rows[0]["ID"]);
                        lngSaleID = ConvertToInt32(tbl.Rows[0]["SALE_ID"]);
                        blnCancelled = Convert.ToBoolean(tbl.Rows[0]["IS_CANCELLED"]);

                        if (lngSaleID > 0)
                        {
                            varResponse.flag = "0";
                            varResponse.message = "Order Shipped";

                            return varResponse;
                        }
                        else if (blnCancelled == true)
                        {
                            varResponse.flag = "0";
                            varResponse.message = "Order already cancelled";

                            return varResponse;
                        }
                    }
                    else
                    {
                        varResponse.flag = "0";
                        varResponse.message = "Invalid Order No.";

                        return varResponse;
                    }
                }
                catch (Exception ex) { }

                if (lngOrderID < 1)
                {
                    varResponse.flag = "0";
                    varResponse.message = "Invalid Order No.";
                    return varResponse;
                }


                SqlTransaction objTrans = objCon.BeginTransaction();

                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = objCon;
                    cmd.Transaction = objTrans;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_CANCEL_ORDER";
                    cmd.Parameters.AddWithValue("@ORDER_ID", lngOrderID);
                    cmd.Parameters.AddWithValue("@CANCEL_DATE", varOrder.cancelDate);
                    cmd.Parameters.AddWithValue("@CANCEL_REASON", varOrder.cancelReason);
                    cmd.ExecuteNonQuery();

                    objTrans.Commit();

                    varResponse.flag = "1";
                    varResponse.message = "Success";
                    return varResponse;
                }
                catch (Exception ex)
                {
                    objTrans.Rollback();
                    throw ex;
                }

            }
            catch (Exception ex)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = objCon;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO TB_ORDER_LOG(LOG_TIME, ORDER_NO, LOG_REMARKS) VALUES(GETDATE()," + SQLString(varOrder.orderNo) + "," + "Cancel Failed." + Environment.NewLine + SQLString(ex.Message) + ")";
                cmd.ExecuteNonQuery();
            }
            finally
            {
                if (objCon.State == ConnectionState.Open)
                    objCon.Close();
            }

            varResponse.flag = "0";
            varResponse.message = "Failed";
            return varResponse;

        }
    }
}