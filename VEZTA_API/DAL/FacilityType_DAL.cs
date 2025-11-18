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
    public class FacilityType_DAL
    {

        public List<FacilityTypes> GetAllFacilityTypes(int intUserID)
        {
            SqlConnection connection = ADO.GetConnection();
            
                SqlCommand cmd = new SqlCommand
                {
                    Connection = connection,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "SP_TB_FACILITY_TYPE"
                };
                cmd.Parameters.AddWithValue("ACTION", 0);
                cmd.Parameters.AddWithValue("UserID", intUserID);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tbl = new DataTable();
                da.Fill(tbl);

                // Use LINQ to convert DataTable to List<FacilityTypes>
                return tbl.AsEnumerable().Select(dr => new FacilityTypes
                {
                    ID = Convert.ToInt32(dr["ID"]),
                    FacilityType = Convert.ToString(dr["FacilityType"]),
                    Description = Convert.ToString(dr["Description"]),
                    IsInactive = Convert.ToBoolean(dr["IsInactive"]),
                    IsDeleted = Convert.ToBoolean(dr["IsDeleted"])
                    
                }).ToList();
           
        }


        //public List<FacilityTypes> GetAllFacilityTypes(Int32 intUserID)
        //{

        //    List<FacilityTypes> facilitytypesList = new List<FacilityTypes>();
        //    using (SqlConnection connection = ADO.GetConnection())
        //    {

        //        SqlCommand cmd = new SqlCommand();
        //        cmd.Connection = connection;
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.CommandText = "SP_TB_FACILITY_TYPE";
        //        cmd.Parameters.AddWithValue("ACTION", 0);
        //        cmd.Parameters.AddWithValue("UserID", intUserID);

        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        DataTable tbl = new DataTable();
        //        da.Fill(tbl);

        //        foreach (DataRow dr in tbl.Rows)
        //        {
        //            facilitytypesList.Add(new FacilityTypes
        //            {
        //                ID = Convert.ToInt32(dr["ID"]),
        //                FacilityType = Convert.ToString(dr["FacilityType"]),
        //                Description = Convert.ToString(dr["Description"]),
        //                IsInactive = Convert.ToBoolean(dr["IsInactive"]),
        //                IsDeleted = Convert.ToBoolean(dr["IsDeleted"])
        //                //CreatedDate = dr["CreatedDate"] != DBNull.Value ? DateTime.SpecifyKind(Convert.ToDateTime(dr["CreatedDate"]), DateTimeKind.Utc) : DateTime.MinValue,
        //                //CreatedUserID = dr["CreatedUserID"] != DBNull.Value ? Convert.ToInt32(dr["CreatedUserID"]) : 0,
        //                //LastModifiedTime = dr["LastModifiedTime"] != DBNull.Value ? DateTime.SpecifyKind(Convert.ToDateTime(dr["LastModifiedTime"]), DateTimeKind.Utc) : DateTime.MinValue,
        //                //LastModifiedUserID = dr["LastModifiedUserID"] != DBNull.Value ? Convert.ToInt32(dr["LastModifiedUserID"]) : 0,

        //            });
        //        }
        //        connection.Close();
        //    }
        //    return facilitytypesList;
        //}

        public Int32 Insert(FacilityTypes facilityTypes, Int32 userID)
        {
            
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_FACILITY_TYPE";
                    cmd.Parameters.AddWithValue("ACTION", 1);

                    cmd.Parameters.AddWithValue("FacilityType", facilityTypes.FacilityType);
                    cmd.Parameters.AddWithValue("Description", facilityTypes.Description);
                    cmd.Parameters.AddWithValue("IsInactive", facilityTypes.IsInactive);
                    //cmd.Parameters.AddWithValue("CreatedUserID", facilityGroup.CreatedUserID);
                    cmd.Parameters.AddWithValue("UserID", userID);

                    cmd.ExecuteNonQuery();

                    SqlCommand cmd1 = new SqlCommand();
                    cmd1.Connection = connection;
                    cmd1.CommandType = CommandType.Text;
                    cmd1.CommandText = "SELECT MAX(ID) FROM TB_FACILITY_TYPE";
                    Int32 FacilityTypeID = Convert.ToInt32(cmd1.ExecuteScalar());

                    return FacilityTypeID;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Int32 Update(FacilityTypes facilitytypes, Int32 userID)
        {
           
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_FACILITY_TYPE";
                    cmd.Parameters.AddWithValue("ACTION", 2);

                    cmd.Parameters.AddWithValue("ID", facilitytypes.ID);
                    cmd.Parameters.AddWithValue("FacilityType", facilitytypes.FacilityType);
                    cmd.Parameters.AddWithValue("Description", facilitytypes.Description);
                    cmd.Parameters.AddWithValue("IsInactive", facilitytypes.IsInactive);
                    //cmd.Parameters.AddWithValue("CreatedUserID", facilityGroup.CreatedUserID);
                    cmd.Parameters.AddWithValue("UserID", userID);

                    cmd.ExecuteNonQuery();

                    SqlCommand cmd1 = new SqlCommand();
                    cmd1.Connection = connection;
                    cmd1.CommandType = CommandType.Text;
                    cmd1.CommandText = "SELECT MAX(ID) FROM TB_FACILITY_TYPE";
                    Int32 FacilityTypeID = Convert.ToInt32(cmd1.ExecuteScalar());

                    return FacilityTypeID;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<FacilityTypes> GetItems(int id)
        {
            List<FacilityTypes> FacilityTypeList = new List<FacilityTypes>();
            try
            {
                string strSQL = "SELECT ID,FacilityType,Description,IsInactive" +
                            "  FROM TB_FACILITY_TYPE" +
                               " WHERE TB_FACILITY_TYPE.ID = " + id;


                DataTable tbl = ADO.GetDataTable(strSQL, "FacilityTypeList");

                if (tbl.Rows.Count > 0)
                {
                    DataRow dr = tbl.Rows[0];

                    FacilityTypeList.Add(new FacilityTypes
                    {
                        ID = Convert.ToInt32(dr["ID"]),
                        FacilityType = Convert.ToString(dr["FacilityType"]),
                        Description = Convert.ToString(dr["Description"]),
                        IsInactive = Convert.ToBoolean(dr["IsInactive"])
                       
                    });

                }
            }
            catch (Exception ex)
            {

            }

            return FacilityTypeList;
        }
        public void DeleteFacilityType(int Id, int userID)
        {
           
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_FACILITY_TYPE";
                    cmd.Parameters.AddWithValue("ACTION", 4);
                    cmd.Parameters.AddWithValue("@ID", Id);
                    cmd.Parameters.AddWithValue("UserID", userID);

                    cmd.ExecuteNonQuery();

                    connection.Close();
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}