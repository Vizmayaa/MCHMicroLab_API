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
    public class DenialType_DAL
    {
        public List<DenialTypes> GetAllDenialTypes(int intUserID)
        {
            List<DenialTypes> denialTypeList;

            SqlConnection connection = ADO.GetConnection();
            
                SqlCommand cmd = new SqlCommand
                {
                    Connection = connection,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "SP_TB_DENIAL_TYPE"
                };
                cmd.Parameters.AddWithValue("ACTION", 0);
                cmd.Parameters.AddWithValue("UserID", intUserID);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tbl = new DataTable();
                da.Fill(tbl);

                // Use LINQ to convert DataTable to List<DenialTypes>
                denialTypeList = tbl.AsEnumerable().Select(dr => new DenialTypes
                {
                    ID = Convert.ToInt32(dr["ID"]),
                    DenialType = Convert.ToString(dr["DenialType"]),
                    Description = Convert.ToString(dr["Description"]),
                    IsInactive = Convert.ToBoolean(dr["IsInactive"]),
                    IsDeleted = Convert.ToBoolean(dr["IsDeleted"])
                   
                }).ToList();
            
            return denialTypeList;
        }

        public Int32 Insert(DenialTypes denialTypes, Int32 userID)
        {
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_DENIAL_TYPE";
                    cmd.Parameters.AddWithValue("ACTION", 1);

                    cmd.Parameters.AddWithValue("DenialType", denialTypes.DenialType);
                    cmd.Parameters.AddWithValue("Description", denialTypes.Description);
                    cmd.Parameters.AddWithValue("IsInactive", denialTypes.IsInactive);
                    //cmd.Parameters.AddWithValue("CreatedUserID", facilityGroup.CreatedUserID);
                    cmd.Parameters.AddWithValue("UserID", userID);

                    cmd.ExecuteNonQuery();

                    SqlCommand cmd1 = new SqlCommand();
                    cmd1.Connection = connection;
                    cmd1.CommandType = CommandType.Text;
                    cmd1.CommandText = "SELECT MAX(ID) FROM TB_DENIAL_TYPES";
                    Int32 DenialTypeID = Convert.ToInt32(cmd1.ExecuteScalar());

                    return DenialTypeID;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Int32 Update(DenialTypes denialTypes, Int32 userID)
        {

            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_DENIAL_TYPE";
                    cmd.Parameters.AddWithValue("ACTION", 2);

                    cmd.Parameters.AddWithValue("ID", denialTypes.ID);
                    cmd.Parameters.AddWithValue("DenialType", denialTypes.DenialType);
                    cmd.Parameters.AddWithValue("Description", denialTypes.Description);
                    cmd.Parameters.AddWithValue("IsInactive", denialTypes.IsInactive);
                    //cmd.Parameters.AddWithValue("CreatedUserID", facilityGroup.CreatedUserID);
                    cmd.Parameters.AddWithValue("UserID", userID);

                    cmd.ExecuteNonQuery();

                    SqlCommand cmd1 = new SqlCommand();
                    cmd1.Connection = connection;
                    cmd1.CommandType = CommandType.Text;
                    cmd1.CommandText = "SELECT MAX(ID) FROM TB_DENIAL_TYPES";
                    Int32 DenialTypeID = Convert.ToInt32(cmd1.ExecuteScalar());

                    return DenialTypeID;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<DenialTypes> GetItems(int id)
        {
            List<DenialTypes> denialTypes = new List<DenialTypes>();

            try
            {
                string strSQL = "SELECT ID,DenialType,Description,IsInactive" +
                            "  FROM TB_DENIAL_TYPES" +
                               " WHERE TB_DENIAL_TYPES.ID = " + id;
                DataTable tbl = ADO.GetDataTable(strSQL, "DenialTypes");

                if (tbl.Rows.Count > 0)
                {
                    DataRow dr = tbl.Rows[0];

                    denialTypes.Add(new DenialTypes
                    {
                        ID = Convert.ToInt32(dr["ID"]),
                        DenialType = Convert.ToString(dr["DenialType"]),
                        Description = Convert.ToString(dr["Description"]),
                        IsInactive = Convert.ToBoolean(dr["IsInactive"])
                      
                    });

                }
            }
            catch (Exception ex)
            {

            }

            return denialTypes;
        }
        public bool DeleteDenialType(int Id, int userID)
        {


            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_DENIAL_TYPE";
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