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
    public class Department_DAL
    {
        public DepartmentResponse Insert(DepartmentClass departmentClass)
        {
            DepartmentResponse res = new DepartmentResponse();

            try
            {
                using (SqlConnection connection = ADO.GetConnection()) // Ensure this method does NOT open the connection
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open(); // Open only if it's closed

                    string str = "INSERT INTO TB_DEPARTMENT (DEPT_NAME, IS_INACTIVE) VALUES (@DEPT_NAME, @IS_INACTIVE)";
                    using (SqlCommand cmd = new SqlCommand(str, connection))
                    {
                        cmd.Parameters.AddWithValue("@DEPT_NAME", departmentClass.DEPARTMENT);
                        cmd.Parameters.AddWithValue("@IS_INACTIVE", departmentClass.IS_INACTIVE);

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


        public DepartmentResponse Update(DepartmentClass departmentClass)
        {
            DepartmentResponse res = new DepartmentResponse();

            try
            {
                using (SqlConnection connection = ADO.GetConnection()) // Ensure this method does NOT open the connection
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open(); // Open only if it's closed

                    string str = "UPDATE TB_DEPARTMENT SET DEPT_NAME = @DEPT_NAME,IS_INACTIVE = @IS_INACTIVE WHERE ID=@ID";
                    using (SqlCommand cmd = new SqlCommand(str, connection))
                    {
                        cmd.Parameters.AddWithValue("@ID", departmentClass.ID);
                        cmd.Parameters.AddWithValue("@DEPT_NAME", departmentClass.DEPARTMENT);
                        cmd.Parameters.AddWithValue("@IS_INACTIVE", departmentClass.IS_INACTIVE);

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

        public DepartmentClass GetDepartmentById(int id)
        {
            DepartmentClass department = new DepartmentClass();

            try
            {
                string strSQL = "SELECT * FROM TB_DEPARTMENT WHERE ID = " + id;

                DataTable tbl = ADO.GetDataTable(strSQL, "Department");

                if (tbl.Rows.Count > 0)
                {
                    DataRow dr = tbl.Rows[0];

                    department = new DepartmentClass
                    {
                        ID = ADO.ToInt32(dr["ID"]),
                        DEPARTMENT = ADO.ToString(dr["DEPT_NAME"]),
                        IS_INACTIVE = ADO.Toboolean(dr["IS_INACTIVE"])
                    };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return department;
        }

        public DepartmentResponse GetLogList()
        {
            DepartmentResponse res = new DepartmentResponse
            {
                flag = 0,
                Message = "Failed",
                Data = new List<DepartmentClass>()
            };

            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                   
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM TB_DEPARTMENT", connection))
                    {
                        if (connection.State == ConnectionState.Closed)
                            connection.Open(); // Open only if it's closed

                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                res.Data.Add(new DepartmentClass
                                {
                                    ID = Convert.ToInt32(rdr["ID"]),
                                    DEPARTMENT = Convert.ToString(rdr["DEPT_NAME"]),
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

        public DepartmentResponse DeleteDepartmentData(int id)
        {
            DepartmentResponse department = new DepartmentResponse();

            try
            {
                SqlConnection connection = ADO.GetConnection();

                string strSQL = "DELETE FROM TB_DEPARTMENT WHERE ID = " + id;

                SqlCommand cmd = new SqlCommand(strSQL, connection);

                int RowsEffected = cmd.ExecuteNonQuery();

                department.flag = RowsEffected > 0 ? 1 : 0;
                department.Message = RowsEffected > 0 ? "Success" : "Delete failed";
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return department;
        }
    }
}

