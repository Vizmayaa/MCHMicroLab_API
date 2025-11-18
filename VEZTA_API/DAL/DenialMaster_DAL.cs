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
    public class DenialMaster_DAL
    {
       
            string conString = ConfigurationManager.ConnectionStrings["WSMasterConnectionString"].ToString();

        public List<DenialMaster> GetAllDenial(int intUserID)
        {
            List<DenialMaster> denialList;

            SqlConnection connection = ADO.GetConnection();
            
                SqlCommand cmd = new SqlCommand
                {
                    Connection = connection,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "SP_TB_DENIAL_MASTER"
                };
                cmd.Parameters.AddWithValue("ACTION", 0);
                cmd.Parameters.AddWithValue("UserID", intUserID);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tbl = new DataTable();
                da.Fill(tbl);

                // Use LINQ to convert DataTable to List<DenialMaster>
                denialList = tbl.AsEnumerable().Select(dr => new DenialMaster
                {
                    ID = Convert.ToInt32(dr["ID"]),
                    DenialCode = dr["DenialCode"].ToString(),
                    DenialCategoryID = Convert.ToInt32(dr["DenialCategoryID"]),
                    DenialCategory = dr["DenialCategory"].ToString(),
                    DenialTypeID = Convert.ToInt32(dr["DenialTypeID"]),
                    DenialType = dr["DenialType"].ToString(),
                    Description = dr["Description"].ToString(),
                    DenialName = dr["DenialName"].ToString(),
                    IsInactive = Convert.ToBoolean(dr["IsInactive"]),
                }).ToList();
            
            return denialList;
        }

        public Int32 Insert(DenialMaster denialMaster, Int32 userID)


        {
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_DENIAL_MASTER";
                    cmd.Parameters.AddWithValue("ACTION", 1);

                    cmd.Parameters.AddWithValue("DenialCode", denialMaster.DenialCode);
                    cmd.Parameters.AddWithValue("Description", denialMaster.Description);     
                    cmd.Parameters.AddWithValue("DenialCategoryID", denialMaster.DenialCategoryID);
                    cmd.Parameters.AddWithValue("DenialTypeID", denialMaster.DenialTypeID);
                    cmd.Parameters.AddWithValue("IsInactive", denialMaster.IsInactive);
                    cmd.Parameters.AddWithValue("DenialName", denialMaster.DenialName);
                    cmd.Parameters.AddWithValue("UserID", userID);

                    cmd.ExecuteNonQuery();

                    SqlCommand cmd1 = new SqlCommand();
                    cmd1.Connection = connection;
                    cmd1.CommandType = CommandType.Text;
                    cmd1.CommandText = "SELECT MAX(ID) FROM TB_DENIAL_MASTER";
                    Int32 DenialID = Convert.ToInt32(cmd1.ExecuteScalar());

                    return DenialID;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<DenialMaster> GetItems(int id)
        {
            List<DenialMaster> denialmaster = new List<DenialMaster>();

            try
            {
                string strSQL = "SELECT TB_DENIAL_MASTER.ID,TB_DENIAL_MASTER.DenialCode, TB_DENIAL_MASTER.Description, " +
                    "TB_DENIAL_MASTER.DenialName, TB_DENIAL_MASTER.DenialTypeID, TB_DENIAL_MASTER.DenialCategoryID, TB_DENIAL_CATEGORY.DenialCategory, " +
                    "TB_DENIAL_TYPES.DenialType " +
                "FROM TB_DENIAL_MASTER " +
                "INNER JOIN TB_DENIAL_TYPES ON TB_DENIAL_MASTER.DenialTypeID = TB_DENIAL_TYPES.ID " +
                "INNER JOIN TB_DENIAL_CATEGORY ON TB_DENIAL_MASTER.DenialCategoryID = TB_DENIAL_CATEGORY.ID " +
                "WHERE TB_DENIAL_MASTER.ID = " + id;

                DataTable tbl = ADO.GetDataTable(strSQL, "DenialMaster");

                if (tbl.Rows.Count > 0)
                {
                    DataRow dr = tbl.Rows[0];

                    denialmaster.Add(new DenialMaster
                    {
                        ID = Convert.ToInt32(dr["ID"]),
                        DenialCode = dr["DenialCode"].ToString(),
                        DenialCategoryID = Convert.ToInt32(dr["DenialCategoryID"]),
                        DenialCategory = dr["DenialCategory"].ToString(),
                        DenialTypeID = Convert.ToInt32(dr["DenialTypeID"]),
                        DenialType = dr["DenialType"].ToString(),
                        Description = dr["Description"].ToString(),
                        DenialName = dr["DenialName"].ToString()
                      
                    });

                }
            }
            catch (Exception ex)
            {

            }

            return denialmaster;
        }
        public Int32 Update(DenialMaster denialMaster, Int32 userID)


        {
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_DENIAL_MASTER";
                    cmd.Parameters.AddWithValue("ACTION", 3);

                    cmd.Parameters.AddWithValue("ID", denialMaster.ID);
                    cmd.Parameters.AddWithValue("DenialCode", denialMaster.DenialCode);
                    cmd.Parameters.AddWithValue("Description", denialMaster.Description);
                    cmd.Parameters.AddWithValue("DenialCategoryID", denialMaster.DenialCategoryID);
                    cmd.Parameters.AddWithValue("DenialTypeID", denialMaster.DenialTypeID);
                    cmd.Parameters.AddWithValue("IsInactive", denialMaster.IsInactive);
                    cmd.Parameters.AddWithValue("DenialName", denialMaster.DenialName);
                    cmd.Parameters.AddWithValue("UserID", userID);

                    cmd.ExecuteNonQuery();

                    SqlCommand cmd1 = new SqlCommand();
                    cmd1.Connection = connection;
                    cmd1.CommandType = CommandType.Text;
                    cmd1.CommandText = "SELECT MAX(ID) FROM TB_DENIAL_MASTER";
                    Int32 DenialID = Convert.ToInt32(cmd1.ExecuteScalar());

                    return DenialID;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool DeleteDenialMaster(int Id, int userID)
        {
                try
                {
                    using (SqlConnection connection = ADO.GetConnection())
                    {
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = connection;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "SP_TB_DENIAL_MASTER";
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