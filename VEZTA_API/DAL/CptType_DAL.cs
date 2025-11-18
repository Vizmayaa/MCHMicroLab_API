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
    public class CptType_DAL
    {

        public List<CptTypes> GetAllCptTypes(int intUserID)
        {
            List<CptTypes> cpttypeList;

            SqlConnection connection = ADO.GetConnection();
            
                SqlCommand cmd = new SqlCommand
                {
                    Connection = connection,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "SP_TB_CPT_TYPE"
                };
                cmd.Parameters.AddWithValue("ACTION", 0);
                cmd.Parameters.AddWithValue("UserID", intUserID);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tbl = new DataTable();
                da.Fill(tbl);

                // Use LINQ to convert DataTable to List<CptTypes> without explicit loop
                cpttypeList = tbl.AsEnumerable().Select(dr => new CptTypes
                {
                    ID = Convert.ToInt32(dr["ID"]),
                    CptType = Convert.ToString(dr["CptType"]),
                    Description = Convert.ToString(dr["Description"]),
                    IsInactive = Convert.ToBoolean(dr["IsInactive"]),
                    IsDeleted = Convert.ToBoolean(dr["IsDeleted"])
                }).ToList();
            
            return cpttypeList;
        }




        //public List<CptTypes> GetAllCptTypes(Int32 intUserID)
        //{


        //    List<CptTypes> cpttypeList = new List<CptTypes>();
        //    using (SqlConnection connection = ADO.GetConnection())
        //    {

        //        SqlCommand cmd = new SqlCommand();
        //        cmd.Connection = connection;
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.CommandText = "SP_TB_CPT_TYPE";
        //        cmd.Parameters.AddWithValue("ACTION", 0);
        //        cmd.Parameters.AddWithValue("UserID", intUserID);

        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        DataTable tbl = new DataTable();
        //        da.Fill(tbl);

        //        foreach (DataRow dr in tbl.Rows)
        //        {
        //            cpttypeList.Add(new CptTypes
        //            {
        //                ID = Convert.ToInt32(dr["ID"]),
        //                CptType = Convert.ToString(dr["CptType"]),
        //                Description = Convert.ToString(dr["Description"]),
        //                IsInactive = Convert.ToBoolean(dr["IsInactive"]),
        //                IsDeleted = Convert.ToBoolean(dr["IsDeleted"])

        //            });
        //        }
        //        connection.Close();
        //    }
        //    return cpttypeList;
        //}
        public Int32 Insert(CptTypes cptTypes, Int32 userID)
        {
           

            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_CPT_TYPE";
                    cmd.Parameters.AddWithValue("ACTION", 1);

                    cmd.Parameters.AddWithValue("CptType", cptTypes.CptType);
                    cmd.Parameters.AddWithValue("Description", cptTypes.Description);
                    cmd.Parameters.AddWithValue("IsInactive", cptTypes.IsInactive);
                    //cmd.Parameters.AddWithValue("CreatedUserID", facilityGroup.CreatedUserID);
                    cmd.Parameters.AddWithValue("UserID", userID);

                    cmd.ExecuteNonQuery();

                    SqlCommand cmd1 = new SqlCommand();
                    cmd1.Connection = connection;
                    cmd1.CommandType = CommandType.Text;
                    cmd1.CommandText = "SELECT MAX(ID) FROM TB_CPT_TYPES";
                    Int32 CptTypeID = Convert.ToInt32(cmd1.ExecuteScalar());

                    return CptTypeID;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Int32 Update(CptTypes cptTypes, Int32 userID)
        {
            
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_CPT_TYPE";
                    cmd.Parameters.AddWithValue("ACTION", 2);

                    cmd.Parameters.AddWithValue("ID", cptTypes.ID);
                    cmd.Parameters.AddWithValue("CptType", cptTypes.CptType);
                    cmd.Parameters.AddWithValue("Description", cptTypes.Description);
                    cmd.Parameters.AddWithValue("IsInactive", cptTypes.IsInactive);
                    //cmd.Parameters.AddWithValue("CreatedUserID", facilityGroup.CreatedUserID);
                    cmd.Parameters.AddWithValue("UserID", userID);

                    cmd.ExecuteNonQuery();

                    SqlCommand cmd1 = new SqlCommand();
                    cmd1.Connection = connection;
                    cmd1.CommandType = CommandType.Text;
                    cmd1.CommandText = "SELECT MAX(ID) FROM TB_CPT_TYPES";
                    Int32 CptTypeID = Convert.ToInt32(cmd1.ExecuteScalar());

                    return CptTypeID;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CptTypes> GetItems(int id)
        {
            List<CptTypes> cptTypes = new List<CptTypes>();
            
            try
            {
                string strSQL = "SELECT ID,CptType,Description,IsInactive" +
                            "  FROM TB_CPT_TYPES" +
                               " WHERE TB_CPT_TYPES.ID = " + id;
                DataTable tbl = ADO.GetDataTable(strSQL, "Facility");

                if (tbl.Rows.Count > 0)
                {
                    DataRow dr = tbl.Rows[0];

                    cptTypes.Add(new CptTypes
                    {
                        ID = Convert.ToInt32(dr["ID"]),
                        CptType = Convert.ToString(dr["CptType"]),
                        Description = Convert.ToString(dr["Description"]),
                        IsInactive = Convert.ToBoolean(dr["IsInactive"])
                      
                    });

                }
            }
            catch (Exception ex)
            {

            }

            return cptTypes;
        }

        public bool DeleteCptType(int Id, int userID)
        {
           

            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_CPT_TYPE";
                    cmd.Parameters.AddWithValue("ACTION", 4);
                    cmd.Parameters.AddWithValue("@ID", Id);
                    cmd.Parameters.AddWithValue("UserID", userID);

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