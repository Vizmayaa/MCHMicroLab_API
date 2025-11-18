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
    public class Investigation_DAL
    {
        public InvestigationResponse Insert(InvestigationClass investigation)
        {
            InvestigationResponse res = new InvestigationResponse();

            try
            {
                using (SqlConnection connection = ADO.GetConnection()) // Ensure this method does NOT open the connection
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open(); // Open only if it's closed

                    string str = "INSERT INTO TB_INVESTIGATION (INVESTIGATION, IS_INACTIVE) VALUES (@INVESTIGATION, @IS_INACTIVE)";
                    using (SqlCommand cmd = new SqlCommand(str, connection))
                    {
                        cmd.Parameters.AddWithValue("@INVESTIGATION", investigation.INVESTIGATION);
                        cmd.Parameters.AddWithValue("@IS_INACTIVE", investigation.IS_INACTIVE);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        res.flag = rowsAffected > 0 ? 1 : 0;
                        res.Message = rowsAffected > 0 ? "Success" : "Insert failed";
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


        public InvestigationResponse Update(InvestigationClass investigation)
        {
            InvestigationResponse res = new InvestigationResponse();

            try
            {
                using (SqlConnection connection = ADO.GetConnection()) // Ensure this method does NOT open the connection
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open(); // Open only if it's closed

                    string str = "UPDATE TB_INVESTIGATION SET INVESTIGATION = @INVESTIGATION,IS_INACTIVE = @IS_INACTIVE WHERE ID=@ID" ;
                    using (SqlCommand cmd = new SqlCommand(str, connection))
                    {
                        cmd.Parameters.AddWithValue("@ID", investigation.ID);
                        cmd.Parameters.AddWithValue("@INVESTIGATION", investigation.INVESTIGATION);
                        cmd.Parameters.AddWithValue("@IS_INACTIVE", investigation.IS_INACTIVE);

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

        public InvestigationClass GetInvestigationById(int id)
        {
            InvestigationClass investigation = new InvestigationClass();

            try
            {
                string strSQL = "SELECT * FROM TB_INVESTIGATION WHERE ID = " + id;

                DataTable tbl = ADO.GetDataTable(strSQL, "Investigation");

                if (tbl.Rows.Count > 0)
                {
                    DataRow dr = tbl.Rows[0];

                    investigation = new InvestigationClass
                    {
                        ID = ADO.ToInt32(dr["ID"]),
                        INVESTIGATION = ADO.ToString(dr["INVESTIGATION"]),
                        IS_INACTIVE = ADO.Toboolean(dr["IS_INACTIVE"])
                    };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return investigation;
        }

        public InvestigationResponse GetLogList()
        {
            InvestigationResponse res = new InvestigationResponse
            {
                flag = 0,
                Message = "Failed",
                Data = new List<InvestigationClass>()
            };

            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                   
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM TB_INVESTIGATION", connection))
                    {
                        if (connection.State == ConnectionState.Closed)
                            connection.Open(); // Open only if it's closed

                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                res.Data.Add(new InvestigationClass
                                {
                                    ID = Convert.ToInt32(rdr["ID"]),
                                    INVESTIGATION = Convert.ToString(rdr["INVESTIGATION"]),
                                    IS_INACTIVE = Convert.ToBoolean(rdr["IS_INACTIVE"]),
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

        public InvestigationResponse DeleteInvestigationData(int id)
        {
            InvestigationResponse investigation = new InvestigationResponse();

            try
            {
                SqlConnection connection = ADO.GetConnection();

                string strSQL = "DELETE FROM TB_INVESTIGATION WHERE ID = " + id;

                SqlCommand cmd = new SqlCommand(strSQL, connection);

                int RowsEffected = cmd.ExecuteNonQuery();

                investigation.flag = RowsEffected > 0 ? 1 : 0;
                investigation.Message = RowsEffected > 0 ? "Success" : "Delete failed";
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return investigation;
        }
    }
}

