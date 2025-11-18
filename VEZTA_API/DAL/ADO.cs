using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Xml;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using System.Globalization;

namespace VEZTA.DAL
{
    public class ADO
    {
        public static string ConnectionString = "";
        ////public static bool VerifyConnection()
        ////{
        ////    try
        ////    {
        ////        SqlConnection objCon = new SqlConnection();
        ////        objCon.ConnectionString = ConfigurationManager.ConnectionStrings["WSMasterConnectionString"].ConnectionString;
        ////        objCon.Open();
        ////        if (objCon.State == System.Data.ConnectionState.Open)
        ////        {
        ////            objCon.Close();
        ////            return true;
        ////        }
        ////    }
        ////    catch (Exception ex)
        ////    {

        ////    }
        ////    return false;
        ////}
        private static void ReadConnectionString()
        {

            string FileName = HttpContext.Current.Server.MapPath("~/" + "dbconnection.xml");
            XmlDocument objDoc = new XmlDocument();
            XmlNodeList objNodeList;
            try
            {

                objDoc.Load(FileName);
                objNodeList = objDoc.SelectNodes("settings");
                foreach (XmlNode objNode in objNodeList)
                {
                    string mvarSERVER_NAME = objNode.ChildNodes.Item(0).InnerText.ToString();
                    string mvarDATABASE_NAME = objNode.ChildNodes.Item(1).InnerText.ToString();

                    string mvarUSER_NAME = DecryptString(objNode.ChildNodes.Item(2).InnerText.ToString());
                    string mvarPASSWORD = DecryptString(objNode.ChildNodes.Item(3).InnerText.ToString());
                    
                    ConnectionString = "Data Source=" + mvarSERVER_NAME + ";Initial Catalog=" + mvarDATABASE_NAME + ";User ID=" + mvarUSER_NAME + ";Password=" + mvarPASSWORD + "; max pool size=5000";

                }

            }
            catch (Exception ex)
            {

            }
        }
        public static DataTable GetDataTable(string strSQL, string TableName = "TBL")
        {
            DataTable tbl = new DataTable();
            tbl.TableName = TableName;
            SqlConnection objCon = new SqlConnection();
            try
            {


                objCon = GetConnection();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = objCon;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strSQL;
               

                SqlDataAdapter sqlDA = new SqlDataAdapter(cmd);
                sqlDA.Fill(tbl);

                tbl.TableName = TableName;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (objCon.State == ConnectionState.Open)
                    objCon.Close();
            }
            return tbl;
        }
        public static DataTable GetDataTable(string strSQL, SqlConnection objCon, SqlTransaction objTrans, string TableName = "TBL")
        {
            DataTable tbl = new DataTable();
            tbl.TableName = TableName;
            try
            {
                

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = objCon;
                cmd.Transaction = objTrans;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strSQL;


                SqlDataAdapter sqlDA = new SqlDataAdapter(cmd);
                sqlDA.Fill(tbl);

                tbl.TableName = TableName;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                 
            }
            return tbl;
        }
        public static SqlConnection GetConnection()
        {
            try
            {
                if (ConnectionString == "")
                {
                    ReadConnectionString();
                }

                if (ConnectionString != "")
                {
                    SqlConnection objCon = new SqlConnection();
                    objCon.ConnectionString = ConnectionString; // ConfigurationManager.ConnectionStrings["WSMasterConnectionString"].ConnectionString;
                    objCon.Open();
                    return objCon;
                }
                else
                {
                    throw new Exception("Database connection failed");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
             
        }
        private static string DecryptString(string Value)
        {

            SymmetricAlgorithm mCSP;
            mCSP = SetEnc();
            string iv = "PenS8UCVF7s=";
            mCSP.IV = Convert.FromBase64String(iv);
            string key = SetLengthString("12345678", 32);
            mCSP.Key = Convert.FromBase64String(key);
            ICryptoTransform ct;
            MemoryStream ms;
            CryptoStream cs;
            Byte[] byt = new byte[64];
            try
            {
                ct = mCSP.CreateDecryptor(mCSP.Key, mCSP.IV);

                byt = Convert.FromBase64String(Value);

                ms = new MemoryStream();
                cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
                cs.Write(byt, 0, byt.Length);
                cs.FlushFinalBlock();

                cs.Close();

                return Encoding.UTF8.GetString(ms.ToArray());
            }
            catch (Exception ex)
            {
                //throw (new Exception("An error occurred while decrypting string"));
            }
            return Value;
        }
        private string SetLengthString(object p, int v)
        {
            throw new NotImplementedException();
        }
        private static SymmetricAlgorithm SetEnc()
        {
            return new TripleDESCryptoServiceProvider();
        }
        private static string SetLengthString(string str, int length)
        {
            while (length > str.Length)
            {
                str += str;
            }
            if (str.Length > length)
            {
                str = str.Remove(length);
            }
            return str;
        }
        public static string SQLString(object vInput)
        {
            try
            {
               
                return     "N'" + vInput.ToString().Replace("'", "''") + "'";
            }
            catch (Exception ex)
            {
                return "''";
            }
        }
        public static string SQLDate(string vInput)
        {
            try 
            {
                if (vInput != "")
                {
                    DateTime dt = DateTime.ParseExact(vInput, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    return "'" + dt.ToString("yyyy-MM-dd") + "'";
                }
                else
                {
                    return "NULL";
                }
            }
            catch 
            { return "NULL"; }
           
        }         
        public static String ExecuteScalar(string strSQL)
        {
            try
            {
                SqlConnection objCon = GetConnection();
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = objCon;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL;
                    object value = cmd.ExecuteScalar();

                    try
                    {
                        return Convert.ToString(value);
                    }
                    catch (Exception ex)
                    {
                        return "";
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    objCon.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public static bool ExecuteNonQuery(string strSQL, SqlConnection objCon, SqlTransaction objTrans)
        {
            
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = objCon;
                cmd.Transaction = objTrans;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strSQL;
                 

                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
               
           
        }
        public static bool ExecuteNonQuery(string strSQL)
        {
            SqlConnection objCon = new SqlConnection();
            try
            {
                objCon = GetConnection();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = objCon;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strSQL;


                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (objCon.State == ConnectionState.Open)
                    objCon.Close();
            }

        }
        public static bool ExecuteNonQueryIgnoreException(string strSQL)
        {
            SqlConnection objCon = new SqlConnection();
            try
            {
                objCon = GetConnection();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = objCon;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strSQL;


                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                 
            }
            finally
            {
                if (objCon.State == ConnectionState.Open)
                    objCon.Close();
            }
            return false;

        }
        public static bool LogError(Exception ex, string vModule, string vActivity)
        {
            try
            {
                string strSQL = "INSERT INTO TB_ERROR_LOG (MODULE, ACTIVITY, ERROR_MESSAGE, ERROR_TIME)" +
                                "VALUES (" + SQLString(vModule) + "," + SQLString(vActivity) + "," +
                                SQLString(ex.Message) + ", GETDATE())";

                ExecuteNonQuery(strSQL);

            }
            catch (Exception x)
            {
            }
            return true;

        }
        public static Int32 ToInt32(object value)
        {
            try
            {
                if (value != null)
                    return Convert.ToInt32(value);
                else
                    return 0;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public static string ToString(object value)
        {
            try
            {
                if (value != null)
                    return Convert.ToString(value);
                else
                    return "";
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        public static decimal ToDecimal(object value)
        {
            try
            {
                if (value != null)
                    return Convert.ToDecimal(value);
                else
                    return 0;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public static bool Toboolean(object value)
        {
            try
            {
                if (value != null)
                    return Convert.ToBoolean(value);
                else
                    return true;
            }
            catch (Exception ex)
            {
                return true;
            }
        }
   
    }
    
}