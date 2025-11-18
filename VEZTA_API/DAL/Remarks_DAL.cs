using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VEZTA.Models;
using Org.BouncyCastle.Asn1.X500;
using System.Drawing;

namespace VEZTA.DAL
{
    public class Remarks_DAL
    {
        //public RemarksResponse Insert(RemarksClass remarks)
        //{
        //    RemarksResponse res = new RemarksResponse();

        //    try
        //    {
        //        using (SqlConnection connection = ADO.GetConnection()) // Ensure this method does NOT open the connection
        //        {
        //            if (connection.State == ConnectionState.Closed)
        //                connection.Open(); // Open only if it's closed

        //            string str = "INSERT INTO TB_REMARKS (REMARKS) VALUES (@REMARKS)";
        //            using (SqlCommand cmd = new SqlCommand(str, connection))
        //            {
        //                cmd.Parameters.AddWithValue("@REMARKS", remarks.REMARKS);


        //                int rowsAffected = cmd.ExecuteNonQuery();

        //                res.flag = rowsAffected > 0 ? 1 : 0;
        //                res.Message = rowsAffected > 0 ? "Success" : "Insert failed";
        //            }
        //        } // Automatically closes the connection here
        //    }
        //    catch (Exception ex)
        //    {
        //        res.flag = 0;
        //        res.Message = ex.Message;
        //    }

        //    return res;
        //}

        public RemarksResponse Insert(RemarksClass remarks)
        {
            RemarksResponse res = new RemarksResponse();
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    string str = "INSERT INTO TB_REMARKS(REMARKS) OUTPUT INSERTED.ID VALUES(@REMARKS)";
                    using (SqlCommand cmd = new SqlCommand(str, connection))
                    {
                        cmd.Parameters.AddWithValue("@REMARKS", remarks.REMARKS);
                        int insertedId = Convert.ToInt32(cmd.ExecuteScalar());

                        if (insertedId > 0)
                        {
                            // Fetch inserted record
                            string selectStr = "SELECT ID, REMARKS FROM TB_REMARKS WHERE ID = @ID";
                            using (SqlCommand selectCmd = new SqlCommand(selectStr, connection))
                            {
                                selectCmd.Parameters.AddWithValue("@ID", insertedId);
                                using (SqlDataReader rdr = selectCmd.ExecuteReader())
                                {
                                    if (rdr.Read())
                                    {
                                        res.Data = new List<RemarksClass>
                                {
                                    new RemarksClass
                                    {
                                        ID = Convert.ToInt32(rdr["ID"]),
                                        REMARKS = Convert.ToString(rdr["REMARKS"])
                                    }
                                };
                                    }
                                }
                            }

                            res.flag = 1;
                            res.Message = "Success";
                        }
                        else
                        {
                            res.flag = 0;
                            res.Message = "Insert failed";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }
            return res;
        }

        public RemarksResponse Update(RemarksClass remarks)
        {
            RemarksResponse res = new RemarksResponse();

            try
            {
                using (SqlConnection connection = ADO.GetConnection()) // Ensure this method does NOT open the connection
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open(); // Open only if it's closed

                    string str = "UPDATE TB_REMARKS SET REMARKS = @REMARKS WHERE ID=@ID" ;
                    using (SqlCommand cmd = new SqlCommand(str, connection))
                    {
                        cmd.Parameters.AddWithValue("@ID", remarks.ID);
                        cmd.Parameters.AddWithValue("@REMARKS", remarks.REMARKS);
                       

                        int rowsAffected = cmd.ExecuteNonQuery();

                        res.flag = rowsAffected > 0 ? 1 : 0;
                        res.Message = rowsAffected > 0 ? "Success" : "Update failed";
                    }
                } // Automatically closes the connection here
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }

            return res;
        }

        public RemarksClass GetRemarksById(int id)
        {
            RemarksClass remarks = new RemarksClass();

            try
            {
                string strSQL = "SELECT * FROM TB_REMARKS WHERE ID = " + id;

                DataTable tbl = ADO.GetDataTable(strSQL, "Remarks");

                if (tbl.Rows.Count > 0)
                {
                    DataRow dr = tbl.Rows[0];

                    remarks = new RemarksClass
                    {
                        ID = ADO.ToInt32(dr["ID"]),
                        REMARKS = ADO.ToString(dr["REMARKS"]),
 
                    };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return remarks;
        }

        public RemarksResponse GetLogList()
        {
            RemarksResponse res = new RemarksResponse
            {
                flag = 0,
                Message = "Failed",
                Data = new List<RemarksClass>()
            };

            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                   
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM TB_REMARKS", connection))
                    {
                        if (connection.State == ConnectionState.Closed)
                            connection.Open(); // Open only if it's closed

                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                res.Data.Add(new RemarksClass
                                {
                                    ID = Convert.ToInt32(rdr["ID"]),
                                    REMARKS = Convert.ToString(rdr["REMARKS"]),

                                });
                            }
                        }
                    }
                }

                res.flag = 1;
                res.Message = "Success";
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }

            return res;
        }

        public RemarksResponse DeleteRemarksData(int id)
        {
            RemarksResponse remarks = new RemarksResponse();

            try
            {
                SqlConnection connection = ADO.GetConnection();

                string strSQL = "DELETE FROM TB_REMARKS WHERE ID = " + id;

                SqlCommand cmd = new SqlCommand(strSQL, connection);

                int RowsEffected = cmd.ExecuteNonQuery();

                remarks.flag = RowsEffected > 0 ? 1 : 0;
                remarks.Message = RowsEffected > 0 ? "Success" : "Delete failed";
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return remarks;
        }
    }
}

