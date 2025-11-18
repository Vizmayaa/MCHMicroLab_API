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
    public class DenialCategory_DAL
    {
        public List<DenialCategory> GetAllDenialCategorys(int intUserID)
        {
            List<DenialCategory> denialcategoryList;

            SqlConnection connection = ADO.GetConnection();
            
                SqlCommand cmd = new SqlCommand
                {
                    Connection = connection,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "SP_TB_DENIAL_CATEGORY"
                };
                cmd.Parameters.AddWithValue("ACTION", 0);
                cmd.Parameters.AddWithValue("UserID", intUserID);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tbl = new DataTable();
                da.Fill(tbl);

                // Use LINQ to convert DataTable to List<DenialCategory> without explicit loop
                denialcategoryList = tbl.AsEnumerable().Select(dr => new DenialCategory
                {
                    ID = Convert.ToInt32(dr["ID"]),
                    DenialCategorys = Convert.ToString(dr["DenialCategory"]),
                    Description = Convert.ToString(dr["Description"]),
                    IsInactive = Convert.ToBoolean(dr["IsInactive"]),
                    IsDeleted = Convert.ToBoolean(dr["IsDeleted"])
                }).ToList();
           
            return denialcategoryList;
        }




        //public List<DenialCategory> GetAllDenialCategorys(Int32 intUserID)
        //{

        //    List<DenialCategory> denialcategoryList = new List<DenialCategory>();
        //    using (SqlConnection connection = ADO.GetConnection())
        //    {

        //        SqlCommand cmd = new SqlCommand();
        //        cmd.Connection = connection;
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.CommandText = "SP_TB_DENIAL_CATEGORY";
        //        cmd.Parameters.AddWithValue("ACTION", 0);
        //        cmd.Parameters.AddWithValue("UserID", intUserID);

        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        DataTable tbl = new DataTable();
        //        da.Fill(tbl);

        //        foreach (DataRow dr in tbl.Rows)
        //        {
        //            denialcategoryList.Add(new DenialCategory
        //            {
        //                ID = Convert.ToInt32(dr["ID"]),
        //                DenialCategorys = Convert.ToString(dr["DenialCategory"]),
        //                Description = Convert.ToString(dr["Description"]),
        //                IsInactive = Convert.ToBoolean(dr["IsInactive"]),
        //                IsDeleted = Convert.ToBoolean(dr["IsDeleted"])

        //            });
        //        }
        //        connection.Close();
        //    }
        //    return denialcategoryList;
        //}
        public Int32 Insert(DenialCategory denialCategorys, Int32 userID)
        {
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_DENIAL_CATEGORY";
                    cmd.Parameters.AddWithValue("ACTION", 1);

                    cmd.Parameters.AddWithValue("DenialCategory", denialCategorys.DenialCategorys);
                    cmd.Parameters.AddWithValue("Description", denialCategorys.Description);
                    cmd.Parameters.AddWithValue("IsInactive", denialCategorys.IsInactive);
                   
                    cmd.Parameters.AddWithValue("UserID", userID);

                    cmd.ExecuteNonQuery();

                    SqlCommand cmd1 = new SqlCommand();
                    cmd1.Connection = connection;
                    cmd1.CommandType = CommandType.Text;
                    cmd1.CommandText = "SELECT MAX(ID) FROM TB_DENIAL_CATEGORY";
                    Int32 DenialCategoryID = Convert.ToInt32(cmd1.ExecuteScalar());

                    return DenialCategoryID;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Int32 Update(DenialCategory denialCategorys, Int32 userID)
        {
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_DENIAL_CATEGORY";
                    cmd.Parameters.AddWithValue("ACTION", 2);

                    cmd.Parameters.AddWithValue("ID", denialCategorys.ID);
                    cmd.Parameters.AddWithValue("DenialCategory", denialCategorys.DenialCategorys);
                    cmd.Parameters.AddWithValue("Description", denialCategorys.Description);
                    cmd.Parameters.AddWithValue("IsInactive", denialCategorys.IsInactive);
                    //cmd.Parameters.AddWithValue("CreatedUserID", facilityGroup.CreatedUserID);
                    cmd.Parameters.AddWithValue("UserID", userID);

                    cmd.ExecuteNonQuery();

                    SqlCommand cmd1 = new SqlCommand();
                    cmd1.Connection = connection;
                    cmd1.CommandType = CommandType.Text;
                    cmd1.CommandText = "SELECT MAX(ID) FROM TB_DENIAL_CATEGORY";
                    Int32 DenialCategoryID = Convert.ToInt32(cmd1.ExecuteScalar());

                    return DenialCategoryID;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<DenialCategory> GetItems(int id)
        {
            List<DenialCategory> denialCategories = new List<DenialCategory>();

            try
            {
                string strSQL = "SELECT ID,DenialCategory,Description,IsInactive" +
                            "  FROM TB_DENIAL_CATEGORY" +
                               " WHERE TB_DENIAL_CATEGORY.ID = " + id;

                DataTable tbl = ADO.GetDataTable(strSQL, "DenialCategory");

                if (tbl.Rows.Count > 0)
                {
                    DataRow dr = tbl.Rows[0];

                    denialCategories.Add(new DenialCategory
                    {
                        ID = dr["ID"] != DBNull.Value ? Convert.ToInt32(dr["ID"]) : 0,
                        DenialCategorys = dr["DenialCategory"] != DBNull.Value ? Convert.ToString(dr["DenialCategory"]) : string.Empty,
                        Description = dr["Description"] != DBNull.Value ? Convert.ToString(dr["Description"]) : string.Empty,
                        IsInactive = dr["IsInactive"] != DBNull.Value ? Convert.ToBoolean(dr["IsInactive"]) : false
                        


                    });

                }
            }
            catch (Exception ex)
            {

            }

            return denialCategories;
        }
        public bool DeleteDenialCategory(int Id, int userID)
        {


            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_DENIAL_CATEGORY";
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