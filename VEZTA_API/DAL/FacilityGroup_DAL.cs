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
    public class FacilityGroup_DAL
    {
        public List<FacilityGroups> GetAllFacilityGroup(int intUserID)
        {
            List<FacilityGroups> facilityGroupList;

            SqlConnection connection = ADO.GetConnection();

            SqlCommand cmd = new SqlCommand
            {
                Connection = connection,
                CommandType = CommandType.StoredProcedure,
                CommandText = "SP_TB_FACILITY_GROUP"
            };
            cmd.Parameters.AddWithValue("ACTION", 0);
            cmd.Parameters.AddWithValue("UserID", intUserID);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable tbl = new DataTable();
            da.Fill(tbl);

            // Use LINQ to convert DataTable to List<FacilityGroups>
            facilityGroupList = tbl.AsEnumerable().Select(dr => new FacilityGroups
            {
                ID = Convert.ToInt32(dr["ID"]),
                FacilityGroup = Convert.ToString(dr["FacilityGroup"]),
                Description = Convert.ToString(dr["Description"]),
                IsInactive = Convert.ToBoolean(dr["IsInactive"]),
                IsDeleted = Convert.ToBoolean(dr["IsDeleted"]),
                // Add more properties as needed
            }).ToList();

            return facilityGroupList;
        }


        //public List<FacilityGroups> GetAllFacilityGroup(Int32 intUserID)
        //{

        //    List<FacilityGroups> facilitygroupList = new List<FacilityGroups>();
        //    using (SqlConnection connection = ADO.GetConnection())
        //    {
        //        SqlCommand cmd = new SqlCommand();
        //        cmd.Connection = connection;
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.CommandText = "SP_TB_FACILITY_GROUP";
        //        cmd.Parameters.AddWithValue("ACTION", 0);
        //        cmd.Parameters.AddWithValue("UserID", intUserID);

        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        DataTable tbl = new DataTable();
        //        da.Fill(tbl);

        //        foreach (DataRow dr in tbl.Rows)
        //        {
        //            facilitygroupList.Add(new FacilityGroups
        //            {
        //                ID = Convert.ToInt32(dr["ID"]),
        //                FacilityGroup = Convert.ToString(dr["FacilityGroup"]),
        //                Description = Convert.ToString(dr["Description"]),
        //                IsInactive = Convert.ToBoolean(dr["IsInactive"]),
        //                IsDeleted = Convert.ToBoolean(dr["IsDeleted"])
        //                // Add more properties as needed
        //            });
        //        }
        //    }

        //    return facilitygroupList;
        //}

        public Int32 Insert(FacilityGroups facilityGroup, Int32 userID)
        {
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_FACILITY_GROUP";
                    cmd.Parameters.AddWithValue("ACTION", 1);

                    cmd.Parameters.AddWithValue("FacilityGroup", facilityGroup.FacilityGroup);
                    cmd.Parameters.AddWithValue("Description", facilityGroup.Description);
                    cmd.Parameters.AddWithValue("IsInactive", facilityGroup.IsInactive);
                    cmd.Parameters.AddWithValue("UserID", userID);

                    cmd.ExecuteNonQuery();

                    SqlCommand cmd1 = new SqlCommand();
                    cmd1.Connection = connection;
                    cmd1.CommandType = CommandType.Text;
                    cmd1.CommandText = "SELECT MAX(ID) FROM TB_FACILITY_GROUP";
                    Int32 facilityID = Convert.ToInt32(cmd1.ExecuteScalar());

                    return facilityID;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Int32 Update(FacilityGroups facilityGroup, Int32 userID)
        {
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_FACILITY_GROUP";
                    cmd.Parameters.AddWithValue("ACTION", 2);

                    cmd.Parameters.AddWithValue("ID", facilityGroup.ID);
                    cmd.Parameters.AddWithValue("FacilityGroup", facilityGroup.FacilityGroup);
                    cmd.Parameters.AddWithValue("Description", facilityGroup.Description);
                    cmd.Parameters.AddWithValue("IsInactive", facilityGroup.IsInactive);
                    cmd.Parameters.AddWithValue("UserID", userID);

                    cmd.ExecuteNonQuery();

                    SqlCommand cmd1 = new SqlCommand();
                    cmd1.Connection = connection;
                    cmd1.CommandType = CommandType.Text;
                    cmd1.CommandText = "SELECT MAX(ID) FROM TB_FACILITY_GROUP";
                    Int32 facilityID = Convert.ToInt32(cmd1.ExecuteScalar());

                    return facilityID;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<FacilityGroups> GetItems(int id)
        {
            List<FacilityGroups> FacilityGroupList = new List<FacilityGroups>();
            try
            {
                string strSQL = "SELECT ID,FacilityGroup,Description,IsInactive" +
                            "  FROM TB_FACILITY_GROUP" +
                               " WHERE TB_FACILITY_GROUP.ID = " + id;


                DataTable tbl = ADO.GetDataTable(strSQL, "FacilityGroupList");

                if (tbl.Rows.Count > 0)
                {
                    DataRow dr = tbl.Rows[0];

                    FacilityGroupList.Add(new FacilityGroups
                    {
                        ID = Convert.ToInt32(dr["ID"]),
                        FacilityGroup = Convert.ToString(dr["FacilityGroup"]),
                        Description = Convert.ToString(dr["Description"]),
                        IsInactive = Convert.ToBoolean(dr["IsInactive"])
                        
                    });

                }
            }
            catch (Exception ex)
            {

            }

            return FacilityGroupList;
        }
        public void DeleteFacilityGroup(int Id, int userID)
        {
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_FACILITY_GROUP";
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