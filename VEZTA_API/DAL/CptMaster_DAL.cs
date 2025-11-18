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
    public class CptMaster_DAL
    {

        public List<CptMaster> GetAllCptMasters(int intUserID)
        {
            List<CptMaster> cptmenuList;

            SqlConnection connection = ADO.GetConnection();
            
                SqlCommand cmd = new SqlCommand
                {
                    Connection = connection,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "SP_TB_CPT_MASTER"
                };
                cmd.Parameters.AddWithValue("ACTION", 0);
                cmd.Parameters.AddWithValue("UserID", intUserID);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tbl = new DataTable();
                da.Fill(tbl);

                // Use LINQ to convert DataTable to List<CptMaster> without explicit loop
                cptmenuList = tbl.AsEnumerable().Select(dr => new CptMaster
                {
                    ID = Convert.ToInt32(dr["ID"]),
                    CPTTypeID = Convert.ToInt32(dr["CPTTypeID"]),
                    CPTType = Convert.ToString(dr["CPTType"]),
                    CPTCode = Convert.ToString(dr["CPTCode"]),
                    Description = Convert.ToString(dr["Description"]),
                    CPTShortName = Convert.ToString(dr["CPTShortName"]),
                    CPTName = Convert.ToString(dr["CPTName"]),
                    IsInactive = Convert.ToBoolean(dr["IsInactive"])
                    // Uncomment the line below if you need to include IsDeleted in the model
                    // IsDeleted = Convert.ToBoolean(dr["IsDeleted"])
                }).ToList();
            
            return cptmenuList;
        }


        //public List<CptMaster> GetAllCptMasters(Int32 intUserID)
        //{


        //    List<CptMaster> cptmenuList = new List<CptMaster>();
        //    using (SqlConnection connection = ADO.GetConnection())
        //    {

        //        SqlCommand cmd = new SqlCommand();
        //        cmd.Connection = connection;
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.CommandText = "SP_TB_CPT_MASTER";
        //        cmd.Parameters.AddWithValue("ACTION", 0);
        //        cmd.Parameters.AddWithValue("UserID", intUserID);

        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        DataTable tbl = new DataTable();
        //        da.Fill(tbl);

        //        foreach (DataRow dr in tbl.Rows)
        //        {
        //            cptmenuList.Add(new CptMaster
        //            {
        //                ID = Convert.ToInt32(dr["ID"]),
        //                CPTTypeID = Convert.ToInt32(dr["CPTTypeID"]),
        //                CPTType = Convert.ToString(dr["CPTType"]),
        //                CPTCode = Convert.ToString(dr["CPTCode"]),
        //                Description = Convert.ToString(dr["Description"]),
        //                CPTShortName= Convert.ToString(dr["CPTShortName"]),
        //                CPTName = Convert.ToString(dr["CPTName"]),
        //                IsInactive = Convert.ToBoolean(dr["IsInactive"])
        //                //IsDeleted = Convert.ToBoolean(dr["IsDeleted"])

        //            });
        //        }
        //        connection.Close();
        //    }
        //    return cptmenuList;
        //}
        public Int32 Insert(CptMaster cptmaster, Int32 userID)
        {


            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_CPT_MASTER";
                    cmd.Parameters.AddWithValue("ACTION", 1);

                    cmd.Parameters.AddWithValue("CPTTypeID", cptmaster.CPTTypeID);
                    cmd.Parameters.AddWithValue("CPTCode", cptmaster.CPTCode);
                    cmd.Parameters.AddWithValue("CPTShortName", cptmaster.CPTShortName);
                    cmd.Parameters.AddWithValue("CPTName", cptmaster.CPTName);
                    cmd.Parameters.AddWithValue("Description", cptmaster.Description);
                    cmd.Parameters.AddWithValue("IsInactive", cptmaster.IsInactive);
                    cmd.Parameters.AddWithValue("UserID", userID);

                    cmd.ExecuteNonQuery();

                    SqlCommand cmd1 = new SqlCommand();
                    cmd1.Connection = connection;
                    cmd1.CommandType = CommandType.Text;
                    cmd1.CommandText = "SELECT MAX(ID) FROM TB_CPT_MASTER";
                    Int32 CptMasterID = Convert.ToInt32(cmd1.ExecuteScalar());

                    return CptMasterID;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Int32 Update(CptMaster cptmaster, Int32 userID)
        {


            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_CPT_MASTER";
                    cmd.Parameters.AddWithValue("ACTION", 3);

                    cmd.Parameters.AddWithValue("ID", cptmaster.ID);
                    cmd.Parameters.AddWithValue("CPTTypeID", cptmaster.CPTTypeID);
                    cmd.Parameters.AddWithValue("CPTCode", cptmaster.CPTCode);
                    cmd.Parameters.AddWithValue("CPTShortName", cptmaster.CPTShortName);
                    cmd.Parameters.AddWithValue("CPTName", cptmaster.CPTName);
                    cmd.Parameters.AddWithValue("Description", cptmaster.Description);
                    cmd.Parameters.AddWithValue("IsInactive", cptmaster.IsInactive);
                    cmd.Parameters.AddWithValue("UserID", userID);

                    cmd.ExecuteNonQuery();

                    SqlCommand cmd1 = new SqlCommand();
                    cmd1.Connection = connection;
                    cmd1.CommandType = CommandType.Text;
                    cmd1.CommandText = "SELECT MAX(ID) FROM TB_CPT_MASTER";
                    Int32 CptMasterID = Convert.ToInt32(cmd1.ExecuteScalar());

                    return CptMasterID;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CptMaster> GetItems(int id)
        {
            List<CptMaster> CptMasterList = new List<CptMaster>();
            try
            {
               

                string strSQL = "SELECT TB_CPT_MASTER.ID, TB_CPT_MASTER.CPTTypeID," +
                    " TB_CPT_MASTER.CPTCode, " +
                "TB_CPT_MASTER.CPTShortName, TB_CPT_MASTER.CPTName, " +
                "TB_CPT_MASTER.Description, TB_CPT_MASTER.IsInactive,  " +
                "TB_CPT_MASTER.IsDeleted, " +
                "TB_CPT_TYPES.CPTType " +
                "FROM TB_CPT_MASTER " +
                "LEFT JOIN TB_CPT_TYPES ON TB_CPT_MASTER.CPTTypeID = TB_CPT_TYPES.ID " +

                "WHERE TB_CPT_MASTER.ID = " + id;


                DataTable tbl = ADO.GetDataTable(strSQL, "CptMaster");

                if (tbl.Rows.Count > 0)
                {
                    DataRow dr = tbl.Rows[0];

                    CptMasterList.Add(new CptMaster
                    {
                        ID = Convert.ToInt32(dr["ID"]),
                        CPTTypeID = Convert.ToInt32(dr["CPTTypeID"]),
                        CPTType = Convert.ToString(dr["CPTType"]),
                        CPTCode = Convert.ToString(dr["CPTCode"]),
                        Description = Convert.ToString(dr["Description"]),
                        CPTShortName = Convert.ToString(dr["CPTShortName"]),
                        CPTName = Convert.ToString(dr["CPTName"]),
                        IsInactive = Convert.ToBoolean(dr["IsInactive"])
                    });

                }
            }
            catch (Exception ex)
            {

            }

            return CptMasterList;
        }
        public bool DeleteCptMaster(int Id, int userID)
        {


            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_CPT_MASTER";
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