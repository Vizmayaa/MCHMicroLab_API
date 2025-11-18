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
    public class FacilityRegion_DAL
    {

        public List<FacilityRegions> GetAllFacilityRegion(int intUserID)
        {
            SqlConnection connection = ADO.GetConnection();
            
                SqlCommand cmd = new SqlCommand
                {
                    Connection = connection,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "SP_TB_FACILITY_REGION"
                };
                cmd.Parameters.AddWithValue("ACTION", 0);
                cmd.Parameters.AddWithValue("UserID", intUserID);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tbl = new DataTable();
                da.Fill(tbl);

                // Use LINQ to convert DataTable to List<FacilityRegions>
                return tbl.AsEnumerable().Select(dr => new FacilityRegions
                {
                    ID = Convert.ToInt32(dr["ID"]),
                    FacilityRegion = Convert.ToString(dr["FacilityRegion"]),
                    Description = Convert.ToString(dr["Description"]),
                    IsInactive = Convert.ToBoolean(dr["IsInactive"]),
                    IsDeleted = Convert.ToBoolean(dr["IsDeleted"])
                    // Add more properties as needed
                }).ToList();
          
        }


        //public List<FacilityRegions> GetAllFacilityRegion(Int32 intUserID)
        //{

        //    List<FacilityRegions> FacilityRegionList = new List<FacilityRegions>();
        //    using (SqlConnection connection = ADO.GetConnection())
        //    {
        //        SqlCommand cmd = new SqlCommand();
        //        cmd.Connection = connection;
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.CommandText = "SP_TB_FACILITY_REGION";
        //        cmd.Parameters.AddWithValue("ACTION", 0);
        //        cmd.Parameters.AddWithValue("UserID", intUserID);

        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        DataTable tbl = new DataTable();
        //        da.Fill(tbl);

        //        foreach (DataRow dr in tbl.Rows)
        //        {
        //            FacilityRegionList.Add(new FacilityRegions
        //            {
        //                ID = Convert.ToInt32(dr["ID"]),
        //                FacilityRegion = Convert.ToString(dr["FacilityRegion"]),
        //                Description = Convert.ToString(dr["Description"]),
        //                IsInactive = Convert.ToBoolean(dr["IsInactive"]),
        //                IsDeleted = Convert.ToBoolean(dr["IsDeleted"])
        //                // Add more properties as needed
        //            });
        //        }
        //    }

        //    return FacilityRegionList;
        //}

        public Int32 Insert(FacilityRegions FacilityRegion, Int32 userID)
        {
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_FACILITY_REGION";
                    cmd.Parameters.AddWithValue("ACTION", 1);

                    cmd.Parameters.AddWithValue("FacilityRegion", FacilityRegion.FacilityRegion);
                    cmd.Parameters.AddWithValue("Description", FacilityRegion.Description);
                    cmd.Parameters.AddWithValue("IsInactive", FacilityRegion.IsInactive);
                    cmd.Parameters.AddWithValue("UserID", userID);

                    cmd.ExecuteNonQuery();

                    SqlCommand cmd1 = new SqlCommand();
                    cmd1.Connection = connection;
                    cmd1.CommandType = CommandType.Text;
                    cmd1.CommandText = "SELECT MAX(ID) FROM TB_FACILITY_REGION";
                    Int32 facilityID = Convert.ToInt32(cmd1.ExecuteScalar());

                    return facilityID;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Int32 Update(FacilityRegions FacilityRegion, Int32 userID)
        {
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_FACILITY_REGION";
                    cmd.Parameters.AddWithValue("ACTION", 2);

                    cmd.Parameters.AddWithValue("ID", FacilityRegion.ID);
                    cmd.Parameters.AddWithValue("FacilityRegion", FacilityRegion.FacilityRegion);
                    cmd.Parameters.AddWithValue("Description", FacilityRegion.Description);
                    cmd.Parameters.AddWithValue("IsInactive", FacilityRegion.IsInactive);
                    cmd.Parameters.AddWithValue("UserID", userID);

                    cmd.ExecuteNonQuery();

                    SqlCommand cmd1 = new SqlCommand();
                    cmd1.Connection = connection;
                    cmd1.CommandType = CommandType.Text;
                    cmd1.CommandText = "SELECT MAX(ID) FROM TB_FACILITY_REGION";
                    Int32 facilityID = Convert.ToInt32(cmd1.ExecuteScalar());

                    return facilityID;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<FacilityRegions> GetItems(int id)
        {
            List<FacilityRegions> FacilityRegionList = new List<FacilityRegions>();
            try
            {
                string strSQL = "SELECT ID,FacilityRegion,Description,IsInactive" +
                            "  FROM TB_FACILITY_REGION" +
                               " WHERE TB_FACILITY_REGION.ID = " + id;


                DataTable tbl = ADO.GetDataTable(strSQL, "FacilityRegionList");

                if (tbl.Rows.Count > 0)
                {
                    DataRow dr = tbl.Rows[0];

                    FacilityRegionList.Add(new FacilityRegions
                    {
                        ID = Convert.ToInt32(dr["ID"]),
                        FacilityRegion = Convert.ToString(dr["FacilityRegion"]),
                        Description = Convert.ToString(dr["Description"]),
                        IsInactive = Convert.ToBoolean(dr["IsInactive"])
                        
                    });

                }
            }
            catch (Exception ex)
            {

            }

            return FacilityRegionList;
        }
        public void DeleteFacilityRegion(int Id, int userID)
        {
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_FACILITY_REGION";
                    cmd.Parameters.AddWithValue("ACTION", 4);
                    cmd.Parameters.AddWithValue("@ID", Id);
                    cmd.Parameters.AddWithValue("UserID", userID);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }





    }
}