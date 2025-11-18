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
    public class Hospital_DAL
    {
        public HospitalResponse Insert(HospitalClass collection)
        {
            HospitalResponse res = new HospitalResponse();

            try
            {
                using (SqlConnection connection = ADO.GetConnection()) // Ensure this method does NOT open the connection
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open(); // Open only if it's closed

                    string str = "INSERT INTO TB_HOSPITAL (HOSPITAL, IS_INACTIVE) VALUES (@HOSPITAL, @IS_INACTIVE)";
                    using (SqlCommand cmd = new SqlCommand(str, connection))
                    {
                        cmd.Parameters.AddWithValue("@HOSPITAL", collection.HOSPITAL);
                        cmd.Parameters.AddWithValue("@IS_INACTIVE", collection.IS_INACTIVE);

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


        public HospitalResponse Update(HospitalClass collection)
        {
            HospitalResponse res = new HospitalResponse();

            try
            {
                using (SqlConnection connection = ADO.GetConnection()) // Ensure this method does NOT open the connection
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open(); // Open only if it's closed

                    string str = "UPDATE TB_HOSPITAL SET HOSPITAL = @HOSPITAL,IS_INACTIVE = @IS_INACTIVE WHERE ID=@ID" ;
                    using (SqlCommand cmd = new SqlCommand(str, connection))
                    {
                        cmd.Parameters.AddWithValue("@ID", collection.ID);
                        cmd.Parameters.AddWithValue("@HOSPITAL", collection.HOSPITAL);
                        cmd.Parameters.AddWithValue("@IS_INACTIVE", collection.IS_INACTIVE);

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

        public HospitalClass GetHospitalById(int id)
        {
            HospitalClass hospital = new HospitalClass();

            try
            {
                string strSQL = "SELECT * FROM TB_HOSPITAL WHERE ID = " + id;

                DataTable tbl = ADO.GetDataTable(strSQL, "Hospital");

                if (tbl.Rows.Count > 0)
                {
                    DataRow dr = tbl.Rows[0];

                    hospital = new HospitalClass
                    {
                        ID = ADO.ToInt32(dr["ID"]),
                        HOSPITAL = ADO.ToString(dr["HOSPITAL"]),
                        IS_INACTIVE = ADO.Toboolean(dr["IS_INACTIVE"])
                    };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return hospital;
        }

        public HospitalResponse GetLogList()
        {
            HospitalResponse res = new HospitalResponse
            {
                flag = 0,
                Message = "Failed",
                Data = new List<HospitalClass>()
            };

            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                   
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM TB_HOSPITAL", connection))
                    {
                        if (connection.State == ConnectionState.Closed)
                            connection.Open(); // Open only if it's closed

                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                res.Data.Add(new HospitalClass
                                {
                                    ID = Convert.ToInt32(rdr["ID"]),
                                    HOSPITAL = Convert.ToString(rdr["HOSPITAL"]),
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

        public HospitalResponse DeleteHospitalData(int id)
        {
            HospitalResponse hospital = new HospitalResponse();

            try
            {
                SqlConnection connection = ADO.GetConnection();

                string strSQL = "DELETE FROM TB_HOSPITAL WHERE ID = " + id;

                SqlCommand cmd = new SqlCommand(strSQL, connection);

                int RowsEffected = cmd.ExecuteNonQuery();

                hospital.flag = RowsEffected > 0 ? 1 : 0;
                hospital.Message = RowsEffected > 0 ? "Success" : "Delete failed";
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return hospital;
        }
    }
}

