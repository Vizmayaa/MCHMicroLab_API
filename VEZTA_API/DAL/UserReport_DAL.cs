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
    public class UserReport_DAL
    {
        
        public List<UserReport> GetAllUserReport()
        {
            List<UserReport> reportList = new List<UserReport>();
            using (SqlConnection connection = ADO.GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_USER_REPORTS";
                cmd.Parameters.AddWithValue("ACTION", 0);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tbl = new DataTable();
                da.Fill(tbl);

                foreach (DataRow dr in tbl.Rows)
                {
                    UserReport userReport = new UserReport();
                  
                    userReport.columns.Add(new USERREPORT_COLUMNS
                    {

                       
                        Name = Convert.ToString(dr["COLOUMN_NAME"]),
                        Visibility = Convert.ToString(dr["IS_VISIBLE"]),
                        Title = Convert.ToString(dr["COLOUMN_TITLE"]),
                        ToolTip = Convert.ToString(dr["COLOUMN_TOOLTIP"]),
                        Type = Convert.ToString(dr["COLOUMN_TYPE"]),
                        Group = Convert.ToString(dr["CAN_GROUP"]),
                        Summary = Convert.ToString(dr["CAN_SUMMARY"])
                    });

                    reportList.Add(userReport);
                }

                connection.Close();
            }
            return reportList;
        }
        public List<ReportColumns> GetUserReportColumns(string Reportid, string userid)
        {
            List<ReportColumns> LstReportColumn = new List<ReportColumns>();

            using (SqlConnection connection = ADO.GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_USER_REPORTS";
                cmd.Parameters.AddWithValue("ACTION", 0);
                cmd.Parameters.AddWithValue("@USER_ID", userid);
                cmd.Parameters.AddWithValue("@REPORT_ID", Reportid);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tbl = new DataTable();
                da.Fill(tbl);

                foreach (DataRow dr in tbl.Rows)
                {
                    LstReportColumn.Add(new ReportColumns
                    {

                        Name = Convert.ToString(dr["COLOUMN_NAME"]),
                        Visibility = Convert.ToBoolean(dr["IS_VISIBLE"]),
                        Title = Convert.ToString(dr["COLOUMN_TITLE"]),
                        ToolTip = Convert.ToString(dr["COLOUMN_TOOLTIP"]),
                        Type = Convert.ToString(dr["COLOUMN_TYPE"]),
                        Group = Convert.ToBoolean(dr["CAN_GROUP"]),
                        Summary = Convert.ToBoolean(dr["CAN_SUMMARY"])
                    });

                  
                }

                //connection.Close();
            }

            return LstReportColumn;
        }
        public UserReportResponse Insert(UserReport userReport)
        {
            UserReportResponse vResponse = new UserReportResponse();

            string strSQL = "";
            try
            {
                
                 strSQL = " SELECT COUNT(ID) FROM TB_USER_REPORTS WHERE USER_ID =" + userReport.USER_ID +
                                " AND REPORT_ID = " + ADO.SQLString(userReport.REPORT_ID) +
                                " AND USER_REPORT_NAME = " + ADO.SQLString(userReport.USER_REPORT_NAME) +
                                " AND ID <> " + userReport.ID;

                Int32 vID = ADO.ToInt32( ADO.ExecuteScalar(strSQL));
                if (vID > 0)
                {
                    vResponse.flag = "0";
                    vResponse.message = "Memorize name already exists.";
                    return vResponse;
                }
            }
            catch (Exception ex)
            {
                vResponse.flag = "0";
                vResponse.message = ex.Message;
            }

            SqlConnection connection = ADO.GetConnection();
            SqlTransaction objtrans = connection.BeginTransaction();

            try
            {
                DataTable tbl = new DataTable();

                tbl.Columns.Add("ID", typeof(Int32));
                tbl.Columns.Add("COLOUMN_NAME", typeof(String));
                tbl.Columns.Add("IS_VISIBLE", typeof(bool));
                tbl.Columns.Add("COLOUMN_TITLE", typeof(String));
                tbl.Columns.Add("COLOUMN_TOOLTIP", typeof(String));
                tbl.Columns.Add("COLOUMN_TYPE", typeof(String));
                tbl.Columns.Add("CAN_GROUP", typeof(bool));
                tbl.Columns.Add("CAN_SUMMARY", typeof(bool));
               
                if (userReport.columns != null && userReport.columns.Any())
                {
                    foreach (USERREPORT_COLUMNS ur in userReport.columns)
                    {
                        DataRow dRow = tbl.NewRow();
                        dRow["ID"] = ur.ID;
                        dRow["COLOUMN_NAME"] = ur.Name;
                        dRow["IS_VISIBLE"] = ur.Visibility;
                        dRow["COLOUMN_TITLE"] = ur.Title;
                        dRow["COLOUMN_TOOLTIP"] = ur.ToolTip;
                        dRow["COLOUMN_TYPE"] = ur.Type;
                        dRow["CAN_GROUP"] = ur.Group;
                        dRow["CAN_SUMMARY"] = ur.Summary;
                        
                        tbl.Rows.Add(dRow);
                        tbl.AcceptChanges();
                    }
                }

                DataTable tbl1 = new DataTable();

                tbl1.Columns.Add("ID", typeof(Int32));
                tbl1.Columns.Add("SEARCH_ON", typeof(String));
                tbl1.Columns.Add("START_DATE", typeof(String));
                tbl1.Columns.Add("END_DATE", typeof(String));
                tbl1.Columns.Add("ENCOUNTER_TYPE", typeof(String));
                tbl1.Columns.Add("FACILITY_ID", typeof(String));
                tbl1.Columns.Add("SENDER_ID", typeof(String));
                tbl1.Columns.Add("RECEIVER_ID", typeof(String));
                tbl1.Columns.Add("CLINICIAN", typeof(String));

                if (userReport.parameters != null && userReport.parameters.Any())
                {
                    foreach (USERREPORT_PARAMETERS ur in userReport.parameters)
                    {
                        DataRow dRow = tbl1.NewRow();
                        dRow["ID"] = ur.ID;
                        dRow["SEARCH_ON"] = ur.SEARCH_ON;
                        dRow["START_DATE"] = ur.START_DATE;
                        dRow["END_DATE"] = ur.END_DATE;
                        dRow["ENCOUNTER_TYPE"] = ur.ENCOUNTER_TYPE;
                        dRow["FACILITY_ID"] = ur.FACILITY_ID;
                        dRow["SENDER_ID"] = ur.SENDER_ID;
                        dRow["RECEIVER_ID"] = ur.RECEIVER_ID;
                        dRow["CLINICIAN"] = ur.CLINICIAN;

                        tbl1.Rows.Add(dRow);
                        tbl1.AcceptChanges();
                    }
                }

                DataTable tbl2 = new DataTable();

                tbl2.Columns.Add("ID", typeof(Int32));
                tbl2.Columns.Add("COLUMN_NAME", typeof(String));
                tbl2.Columns.Add("COLUMN_CAPTION", typeof(String));
                tbl2.Columns.Add("COLUMN_VALUES", typeof(String));

                if (userReport.advancefilter != null && userReport.advancefilter.Any())
                {
                    foreach (ReportAdvanceFilterColumns ur in userReport.advancefilter)
                    {
                        DataRow dRow = tbl2.NewRow();
                        dRow["ID"] = ur.ID;
                        dRow["COLUMN_NAME"] = ur.dataField;
                        dRow["COLUMN_CAPTION"] = ur.caption;
                        dRow["COLUMN_VALUES"] = ur.values;

                        tbl2.Rows.Add(dRow);
                        tbl2.AcceptChanges();
                    }
                }

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.Transaction = objtrans; 
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_USER_REPORTS";

                cmd.Parameters.AddWithValue("ACTION",  1);
                //cmd.Parameters.AddWithValue("ID", userReport.ID);
                cmd.Parameters.AddWithValue("USER_ID", userReport.USER_ID);
                cmd.Parameters.AddWithValue("REPORT_ID", userReport.REPORT_ID);
               // cmd.Parameters.AddWithValue("CREATED_TIME", userReport.CREATED_TIME);
               // cmd.Parameters.AddWithValue("MODIFIED_TIME", userReport.MODIFIED_TIME);
                cmd.Parameters.AddWithValue("USER_REPORT_NAME", userReport.USER_REPORT_NAME);
                cmd.Parameters.AddWithValue("@UDT_TB_USER_REPORT_COLUMN", tbl);
                cmd.Parameters.AddWithValue("@UDT_TB_USER_REPORT_PARAMETER", tbl1);
                cmd.Parameters.AddWithValue("@UDT_TB_USER_REPORT_ADV_FILTER", tbl2);


                cmd.ExecuteNonQuery();
                objtrans.Commit();
                connection.Close();

                vResponse.flag = "1";
                vResponse.message = "Saved successfully.";
            }
            catch (Exception ex)
            {
                objtrans.Rollback();
                connection.Close();

                vResponse.flag = "0";
                vResponse.message = ex.Message;
            }

            return vResponse;
        }

       
    }
}