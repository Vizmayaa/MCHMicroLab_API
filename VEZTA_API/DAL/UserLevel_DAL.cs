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
    public class UserLevel_DAL
    {
        string conString = ConfigurationManager.ConnectionStrings["WSMasterConnectionString"].ToString();

        public List<UserLevels> GetAllUserLevel()
        {
            List<UserLevels> userlevelList = new List<UserLevels>();


            using (SqlConnection connection = ADO.GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_USER_LEVEL";
                cmd.Parameters.AddWithValue("ACTION", 0);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tbl = new DataTable();
                da.Fill(tbl);

                foreach (DataRow dr in tbl.Rows)
                {
                    userlevelList.Add(new UserLevels
                    {
                        ID = Convert.ToInt32(dr["ID"]),
                        LEVEL_NAME = Convert.ToString(dr["LEVEL_NAME"]),
                        IS_INACTIVE = Convert.ToBoolean(dr["IS_INACTIVE"]),

                    });
                }
                connection.Close();
            }
            return userlevelList;
        }

        public bool Insert(UserLevels userLevel)
        {
            try
            {
                DataTable tbl = new DataTable();
                tbl.Columns.Add("MODULE_ID", typeof(Int32));
                tbl.Columns.Add("CAN_ADD", typeof(bool));
                tbl.Columns.Add("CAN_VIEW", typeof(bool));
                tbl.Columns.Add("CAN_MODIFY", typeof(bool));
                tbl.Columns.Add("CAN_COMMIT", typeof(bool));
                tbl.Columns.Add("CAN_DELETE", typeof(bool));
                tbl.Columns.Add("CAN_PRINT", typeof(bool));

                foreach (UserRight ur in userLevel.rights)
                {
                    DataRow dRow = tbl.NewRow();
                    dRow["MODULE_ID"] = ur.MODULE_ID;
                    dRow["CAN_ADD"] = ur.CAN_ADD;
                    dRow["CAN_VIEW"] = ur.CAN_VIEW;
                    dRow["CAN_MODIFY"] = ur.CAN_MODIFY;
                    dRow["CAN_COMMIT"] = ur.CAN_COMMIT;
                    dRow["CAN_DELETE"] = ur.CAN_DELETE;
                    dRow["CAN_PRINT"] = ur.CAN_PRINT;

                    tbl.Rows.Add(dRow);
                    tbl.AcceptChanges();
                }
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_USER_LEVEL";
                    cmd.Parameters.AddWithValue("ACTION", 1);
                    cmd.Parameters.AddWithValue("LEVEL_NAME", userLevel.LEVEL_NAME);
                    cmd.Parameters.AddWithValue("IS_INACTIVE", userLevel.IS_INACTIVE);
                    cmd.Parameters.AddWithValue("@UDT_TB_USER_RIGHTS", tbl);                     
                    cmd.ExecuteNonQuery();

                    connection.Close();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public UserLevels GetItems(int id)
        {
            UserLevels userlevel = new UserLevels();
            List<UserRight> rights = new List<UserRight>();
            try
            {

                string strSQL = "SELECT * from TB_USER_LEVEL WHERE TB_USER_LEVEL.ID = " + id;
                DataTable tbl = ADO.GetDataTable(strSQL, "UserLevel");
                if (tbl.Rows.Count > 0)
                {
                    DataRow dr = tbl.Rows[0];

                    userlevel.ID = Convert.ToInt32(dr["ID"]);
                    userlevel.LEVEL_NAME = Convert.ToString(dr["LEVEL_NAME"]);
                    userlevel.IS_INACTIVE = Convert.ToBoolean(dr["IS_INACTIVE"]);

                    strSQL = "SELECT * FROM TB_USER_RIGHTS WHERE USER_LEVEL_ID = " + id;
                    DataTable tblDetail = ADO.GetDataTable(strSQL, "UserRights");
                    if (tblDetail.Rows.Count > 0)
                    {
                        foreach (DataRow dr1 in tblDetail.Rows)
                        {
                            rights.Add(new UserRight
                            {
                                MODULE_ID = Convert.ToInt32(dr1["ID"]),
                                CAN_ADD = Convert.ToBoolean(dr1["CAN_ADD"]),
                                CAN_VIEW = Convert.ToBoolean(dr1["CAN_VIEW"]),
                                CAN_MODIFY = Convert.ToBoolean(dr1["CAN_MODIFY"]),
                                CAN_COMMIT = Convert.ToBoolean(dr1["CAN_COMMIT"]),
                                CAN_DELETE = Convert.ToBoolean(dr1["CAN_DELETE"]),
                                CAN_PRINT = Convert.ToBoolean(dr1["CAN_PRINT"])
                            });
                        }
                    }           
                    userlevel.rights = rights;
                }
            }
            catch (Exception ex)
            {

            }

            return userlevel;
        }

        public bool Update(UserLevels userLevel)
        {
            try
            {
                DataTable tbl = new DataTable();
                tbl.Columns.Add("MODULE_ID", typeof(Int32));
                tbl.Columns.Add("CAN_ADD", typeof(bool));
                tbl.Columns.Add("CAN_VIEW", typeof(bool));
                tbl.Columns.Add("CAN_MODIFY", typeof(bool));
                tbl.Columns.Add("CAN_COMMIT", typeof(bool));
                tbl.Columns.Add("CAN_DELETE", typeof(bool));
                tbl.Columns.Add("CAN_PRINT", typeof(bool));

                foreach (UserRight ur in userLevel.rights)
                {
                    DataRow dRow = tbl.NewRow();
                    dRow["MODULE_ID"] = ur.MODULE_ID;
                    dRow["CAN_ADD"] = ur.CAN_ADD;
                    dRow["CAN_VIEW"] = ur.CAN_VIEW;
                    dRow["CAN_MODIFY"] = ur.CAN_MODIFY;
                    dRow["CAN_COMMIT"] = ur.CAN_COMMIT;
                    dRow["CAN_DELETE"] = ur.CAN_DELETE;
                    dRow["CAN_PRINT"] = ur.CAN_PRINT;

                    tbl.Rows.Add(dRow);
                    tbl.AcceptChanges();
                }

                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_USER_LEVEL";
                    cmd.Parameters.AddWithValue("ACTION", 3);
                    cmd.Parameters.AddWithValue("ID", userLevel.ID);
                    cmd.Parameters.AddWithValue("LEVEL_NAME", userLevel.LEVEL_NAME);
                    cmd.Parameters.AddWithValue("IS_INACTIVE", userLevel.IS_INACTIVE);
                    cmd.Parameters.AddWithValue("@UDT_TB_USER_RIGHTS", tbl);
                    cmd.ExecuteNonQuery();

                    connection.Close();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteUserLevel(int id)
        {
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_USER_LEVEL";
                    cmd.Parameters.AddWithValue("ACTION", 4);
                    cmd.Parameters.AddWithValue("@ID", id);
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