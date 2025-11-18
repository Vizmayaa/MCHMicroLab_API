using System;
using System.Collections.Generic;
using System.Configuration;
using VEZTA.Models;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

namespace VEZTA.DAL
{
    public class Report_DAL
    {
        public ReportListData GetReportInitData()
        {
            ReportListData reportData = new ReportListData();
            SqlConnection con = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            SqlDataReader rdr = null;

            try
            {
                con = ADO.GetConnection();
                // Initialize lists
                reportData.WetFilm = new List<WetFilm>();
                reportData.WetFilmResult = new List<WetFilmResult>();
                reportData.GramStain = new List<GramStain>();
                reportData.GramStainResult = new List<GramStainResult>();
                reportData.Isolate = new List<Isolate>();
                reportData.Remarks = new List<Remarks>();
                reportData.Antibiotic = new List<Antibiotic>();
                reportData.Sensitivity = new List<Sensitivity>();

                // Fetch data for each table
                cmd = new SqlCommand("SELECT ID, DESCRIPTION FROM TB_WET_FILM", con);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    reportData.WetFilm.Add(new WetFilm
                    {
                        Id = Convert.ToInt32(rdr["ID"]),
                        Description = rdr["DESCRIPTION"].ToString()
                    });
                }
                rdr.Close();

                cmd = new SqlCommand("SELECT ID, DESCRIPTION FROM TB_WET_FILM_RESULT", con);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    reportData.WetFilmResult.Add(new WetFilmResult
                    {
                        Id = Convert.ToInt32(rdr["ID"]),
                        Description = rdr["DESCRIPTION"].ToString()
                    });
                }
                rdr.Close();

                cmd = new SqlCommand("SELECT ID, DESCRIPTION FROM TB_GRAM_STAIN", con);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    reportData.GramStain.Add(new GramStain
                    {
                        Id = Convert.ToInt32(rdr["ID"]),
                        Description = rdr["DESCRIPTION"].ToString()
                    });
                }
                rdr.Close();

                cmd = new SqlCommand("SELECT ID, DESCRIPTION FROM TB_GRAM_STAIN_RESULT", con);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    reportData.GramStainResult.Add(new GramStainResult
                    {
                        Id = Convert.ToInt32(rdr["ID"]),
                        Description = rdr["DESCRIPTION"].ToString()
                    });
                }
                rdr.Close();

                cmd = new SqlCommand("SELECT ID, DESCRIPTION FROM TB_ISOLATE", con);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    reportData.Isolate.Add(new Isolate
                    {
                        Id = Convert.ToInt32(rdr["ID"]),
                        Description = rdr["DESCRIPTION"].ToString()
                    });
                }
                rdr.Close();

                cmd = new SqlCommand("SELECT ID, REMARKS FROM TB_REMARKS", con);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    reportData.Remarks.Add(new Remarks
                    {
                        Id = Convert.ToInt32(rdr["ID"]),
                        Description = rdr["REMARKS"].ToString()
                    });
                }
                rdr.Close();

                cmd = new SqlCommand("SELECT TB_ANTIBIOTIC.ID, TB_ANTIBIOTIC.ANTIBIOTIC, TB_ANTIBIOTIC_GROUP.[GROUP] AS Group_Name " +
                    "  FROM TB_ANTIBIOTIC  LEFT JOIN TB_ANTIBIOTIC_GROUP  ON TB_ANTIBIOTIC.ANTIBIOTIC_GROUP = TB_ANTIBIOTIC_GROUP.ID", con);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    reportData.Antibiotic.Add(new Antibiotic
                    {
                        Id = Convert.ToInt32(rdr["ID"]),
                        Description = rdr["ANTIBIOTIC"].ToString(),
                        Group_Name = rdr["Group_Name"].ToString()
                    });
                }
                rdr.Close();

                cmd = new SqlCommand("SELECT SENSITIVITY FROM TB_SENSITIVITY", con);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    reportData.Sensitivity.Add(new Sensitivity
                    {
                        Description = rdr["SENSITIVITY"].ToString()
                    });
                }
                rdr.Close();

                reportData.flag = 1;
                reportData.message = "Success";
            }
            catch (Exception ex)
            {
                reportData.flag = 0;
                reportData.message = ex.Message;
            }
            finally
            {
                if (rdr != null) rdr.Close();
                if (cmd != null) cmd.Dispose();
                if (con != null) con.Close();
            }

            return reportData;
        }

        public SaveReportResponse Insert(Report report)
        {
            SaveReportResponse res = new SaveReportResponse();
            SqlConnection connection = new SqlConnection();
            try
            {

                connection = ADO.GetConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_REPORT";
                cmd.Parameters.AddWithValue("@ACTION", 1);

                cmd.Parameters.AddWithValue("@COLLECTION_ID", report.COLLECTION_ID);
                cmd.Parameters.AddWithValue("@ZIEHEL_STAIN", report.ZIEHEL_STAIN);
                cmd.Parameters.AddWithValue("@SPECIAL_STAIN", report.SPECIAL_STAIN);
                cmd.Parameters.AddWithValue("@CULTURE", report.CULTURE);
                cmd.Parameters.AddWithValue("@REMARKS", report.REMARKS);
                cmd.Parameters.AddWithValue("@ISOLATE1_IDENTIFICATION", report.ISOLATE1_IDENTIFICATION);
                cmd.Parameters.AddWithValue("@ISOLATE1_GROWTH_RATE", report.ISOLATE1_GROWTH_RATE);
                cmd.Parameters.AddWithValue("@ISOLATE1_COLONY_COUNT", report.ISOLATE1_COLONY_COUNT);
                cmd.Parameters.AddWithValue("@ISOLATE2_IDENTIFICATION", report.ISOLATE2_IDENTIFICATION);
                cmd.Parameters.AddWithValue("@ISOLATE2_GROWTH_RATE", report.ISOLATE2_GROWTH_RATE);
                cmd.Parameters.AddWithValue("@ISOLATE2_COLONY_COUNT", report.ISOLATE2_COLONY_COUNT);
                cmd.Parameters.AddWithValue("@ISOLATE3_IDENTIFICATION", report.ISOLATE3_IDENTIFICATION);
                cmd.Parameters.AddWithValue("@ISOLATE3_GROWTH_RATE", report.ISOLATE3_GROWTH_RATE);
                cmd.Parameters.AddWithValue("@ISOLATE3_COLONY_COUNT", report.ISOLATE3_COLONY_COUNT);
                cmd.Parameters.AddWithValue("@IS_PRELIMINERY", report.IS_PRELIMINERY);
                cmd.Parameters.AddWithValue("@IS_PUBLISHED", report.IS_PUBLISHED);
                cmd.Parameters.AddWithValue("@USER_ID", report.USER_ID);
                cmd.Parameters.AddWithValue("@PREPARED_BY", report.PREPARED_BY);
                cmd.Parameters.AddWithValue("@APPROVED_BY", report.APPROVED_BY);
                cmd.Parameters.AddWithValue("@LAB_ID", report.LAB_ID);

                DataTable tbl = new DataTable();
 
                tbl.Columns.Add("REPORT_ID", typeof(int));
                tbl.Columns.Add("ANTIBIOTIC_ID", typeof(int));
                tbl.Columns.Add("ISOLATE1", typeof(string));
                tbl.Columns.Add("ISOLATE2", typeof(string));
                tbl.Columns.Add("ISOLATE3", typeof(string));
                tbl.Columns.Add("ADDL_INFO", typeof(string));
                tbl.Columns.Add("REMARKS", typeof(string));
                foreach (ReportEntry ur in report.ReportEntry)
                {
                    DataRow dRow = tbl.NewRow();
      
                    dRow["REPORT_ID"] = ur.REPORT_ID;
                    dRow["ANTIBIOTIC_ID"] = ur.ANTIBIOTIC_ID;
                    dRow["ISOLATE1"] = ur.ISOLATE1;
                    dRow["ISOLATE2"] = ur.ISOLATE2;
                    dRow["ISOLATE3"] = ur.ISOLATE3;
                    dRow["ADDL_INFO"] = ur.ADDL_INFO;
                    dRow["REMARKS"] = ur.REMARKS;
                    tbl.Rows.Add(dRow);
                    tbl.AcceptChanges();
                }

                DataTable tbl1 = new DataTable();
             
                tbl1.Columns.Add("REPORT_ID", typeof(int));
                tbl1.Columns.Add("GRAM_STAIN", typeof(string));
                tbl1.Columns.Add("PRESENCE", typeof(string));
               
                foreach (ReportGramStain ur in report.ReportGramStain)
                {
                    DataRow dRow = tbl1.NewRow();

                    dRow["REPORT_ID"] = ur.REPORT_ID;
                    dRow["GRAM_STAIN"] = ur.GRAM_STAIN;
                    dRow["PRESENCE"] = ur.PRESENCE;
                    tbl1.Rows.Add(dRow);
                    tbl1.AcceptChanges();
                }
                DataTable tbl2 = new DataTable();
           
                tbl2.Columns.Add("REPORT_ID", typeof(int));
                tbl2.Columns.Add("WET_FILM", typeof(string));
                tbl2.Columns.Add("PRESENCE", typeof(string));

                foreach (ReportWetFilm ur in report.ReportWetFilm)
                {
                    DataRow dRow = tbl2.NewRow();
             
                    dRow["REPORT_ID"] = ur.REPORT_ID;
                    dRow["WET_FILM"] = ur.WET_FILM;
                    dRow["PRESENCE"] = ur.PRESENCE;
                    tbl2.Rows.Add(dRow);
                    tbl2.AcceptChanges();
                }

                cmd.Parameters.AddWithValue("@UDT_REPORT_ENTRY", tbl);
                cmd.Parameters.AddWithValue("@UDT_REPORT_GRAM_STAIN", tbl1);
                cmd.Parameters.AddWithValue("@UDT_REPORT_WET_FILM", tbl2);


                cmd.ExecuteNonQuery();

                //SqlCommand cmd1 = new SqlCommand();
                //cmd1.Connection = connection;
                //cmd1.CommandType = CommandType.Text;
                //cmd1.CommandText = "SELECT MAX(ID) FROM TB_REPORT";
                //Int32 ReportlID = Convert.ToInt32(cmd1.ExecuteScalar());
                
                res.flag = 1;
                res.Message = "sucess";
               

            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
            return res;
        }

        public Report GetReportById(int id)
        {
            Report report = new Report();
            List<ReportEntry> ReportEntry = new List<ReportEntry>();
            List<ReportGramStain> ReportGramStain = new List<ReportGramStain>();
            List<ReportWetFilm> ReportWetFilm = new List<ReportWetFilm>();

            try
            {
                string strSQL = "SELECT report.*, " +
                "collection.COLLECTION_NO, collection.COLLECTION_TIME, collection.REFERENCE_NO, collection.PATIENT_NAME, " +
                "collection.AGE, collection.SEX, collection.UNIT_NAME, collection.WARD, collection.DOCTOR_NAME, collection.UHID, " +
                "collection.SPECIMEN, collection.INVESTIGATION_NAME, collection.DIAGNOSIS, hospital.HOSPITAL, " +
                "remarks.REMARKS as REMARKS_NAME, report.REMARKS, report.LAB_ID, " +
                "report.PREPARED_BY, TB_USERS.USER_NAME,report.APPROVED_BY,users.USER_NAME AS APPROVED_BY_NAME " +   
                "FROM TB_REPORT report " +
                "LEFT JOIN TB_COLLECTION collection ON report.COLLECTION_ID = collection.ID " +
                "LEFT JOIN TB_HOSPITAL hospital ON collection.HOSPITAL_ID = hospital.ID " +
                "LEFT JOIN TB_REMARKS remarks ON report.REMARKS = remarks.ID " +
                "LEFT JOIN TB_USERS ON report.PREPARED_BY = TB_USERS.USER_ID " +
                "LEFT JOIN TB_USERS users ON report.APPROVED_BY = users.USER_ID " +
                "WHERE report.ID = " + id;

                DataTable tbl = ADO.GetDataTable(strSQL, "Report");

                if (tbl.Rows.Count > 0)
                {
                    DataRow dr = tbl.Rows[0];

                    report = new Report
                    {
                        ID = ADO.ToInt32(dr["ID"]),
                        COLLECTION_ID = ADO.ToInt32(dr["COLLECTION_ID"]),
                        COLLECTION_NO = ADO.ToInt32(dr["COLLECTION_NO"]),
                        COLLECTION_TIME = Convert.ToDateTime(dr["COLLECTION_TIME"]),
                        REFERENCE_NO = ADO.ToString(dr["REFERENCE_NO"]),
                        PATIENT_NAME = ADO.ToString(dr["PATIENT_NAME"]),
                        AGE = ADO.ToInt32(dr["AGE"]),
                        SEX = ADO.ToString(dr["SEX"]),
                        UNIT_NAME = ADO.ToString(dr["UNIT_NAME"]),
                        WARD = ADO.ToString(dr["WARD"]),
                        DOCTOR_NAME = ADO.ToString(dr["DOCTOR_NAME"]),
                        HOSPITAL = ADO.ToString(dr["HOSPITAL"]),
                        REMARKS_NAME = ADO.ToString(dr["REMARKS_NAME"]),
                        UHID = ADO.ToString(dr["UHID"]),
                        SPECIMEN = ADO.ToString(dr["SPECIMEN"]),
                        INVESTIGATION_NAME = ADO.ToString(dr["INVESTIGATION_NAME"]),
                        DIAGNOSIS = ADO.ToString(dr["DIAGNOSIS"]),
                        ZIEHEL_STAIN = ADO.ToString(dr["ZIEHEL_STAIN"]),
                        SPECIAL_STAIN = ADO.ToString(dr["SPECIAL_STAIN"]),
                        CULTURE = ADO.ToString(dr["CULTURE"]),
                        REMARKS = ADO.ToString(dr["REMARKS"]),
                        ISOLATE1_IDENTIFICATION = ADO.ToString(dr["ISOLATE1_IDENTIFICATION"]),
                        ISOLATE1_GROWTH_RATE = ADO.ToString(dr["ISOLATE1_GROWTH_RATE"]),
                        ISOLATE1_COLONY_COUNT = ADO.ToString(dr["ISOLATE1_COLONY_COUNT"]),
                        ISOLATE2_IDENTIFICATION = ADO.ToString(dr["ISOLATE2_IDENTIFICATION"]),
                        ISOLATE2_GROWTH_RATE = ADO.ToString(dr["ISOLATE2_GROWTH_RATE"]),
                        ISOLATE2_COLONY_COUNT = ADO.ToString(dr["ISOLATE2_COLONY_COUNT"]),
                        ISOLATE3_IDENTIFICATION = ADO.ToString(dr["ISOLATE3_IDENTIFICATION"]),
                        ISOLATE3_GROWTH_RATE = ADO.ToString(dr["ISOLATE3_GROWTH_RATE"]),
                        ISOLATE3_COLONY_COUNT = ADO.ToString(dr["ISOLATE3_COLONY_COUNT"]),
                        IS_PRELIMINERY = ADO.Toboolean(dr["IS_PRILIMINARY"]),
                        IS_PUBLISHED = ADO.Toboolean(dr["IS_PUBLISHED"]),
                        LAB_ID = ADO.ToString(dr["LAB_ID"]),
                        PREPARED_BY = ADO.ToInt32(dr["PREPARED_BY"]),
                        PREPARED_BY_NAME = ADO.ToString(dr["USER_NAME"]),
                        APPROVED_BY = ADO.ToInt32(dr["APPROVED_BY"]),
                        APPROVED_BY_NAME = ADO.ToString(dr["APPROVED_BY_NAME"]),
                    };
                }

                strSQL = "SELECT DISTINCT rw.ID,rw.REPORT_ID, COALESCE(w.ANTIBIOTIC, (SELECT ANTIBIOTIC FROM TB_ANTIBIOTIC WHERE ID = rw.ANTIBIOTIC_ID)) AS ANTIBIOTIC," +
                    "COALESCE(w.ID, (SELECT ID FROM TB_ANTIBIOTIC WHERE ID = rw.ANTIBIOTIC_ID)) AS ANTIBIOTIC_ID, rw.ISOLATE1,rw.ISOLATE2,rw.ISOLATE3,rw.ADDL_INFO,rw.REMARKS,COALESCE(ag.ID, ag2.ID, 0) AS ANTIBIOTIC_GROUP_ID,COALESCE(ag.[GROUP], ag2.[GROUP], '') AS ANTIBIOTIC_GROUP_NAME " +
                    "FROM TB_ANTIBIOTIC w LEFT JOIN TB_REPORT_ENTRY rw ON w.ID = rw.ANTIBIOTIC_ID AND rw.REPORT_ID = " + id + " LEFT JOIN TB_ANTIBIOTIC_GROUP ag ON ag.ID = w.ANTIBIOTIC_GROUP LEFT JOIN TB_ANTIBIOTIC a ON a.ID = rw.ANTIBIOTIC_ID LEFT JOIN TB_ANTIBIOTIC_GROUP ag2 ON ag2.ID = a.ANTIBIOTIC_GROUP " +
                    "UNION SELECT DISTINCT rw.ID,rw.REPORT_ID,(SELECT ANTIBIOTIC FROM TB_ANTIBIOTIC WHERE ID = rw.ANTIBIOTIC_ID) AS ANTIBIOTIC," +
                    "(SELECT ID FROM TB_ANTIBIOTIC WHERE ID = rw.ANTIBIOTIC_ID) AS ANTIBIOTIC_ID,rw.ISOLATE1,rw.ISOLATE2,rw.ISOLATE3,rw.ADDL_INFO,rw.REMARKS,  COALESCE(ag.ID, ag2.ID, 0) AS ANTIBIOTIC_GROUP_ID, " +
                    "COALESCE(ag.[GROUP], ag2.[GROUP], '') AS ANTIBIOTIC_GROUP_NAME FROM TB_REPORT_ENTRY rw LEFT JOIN TB_ANTIBIOTIC w ON w.ID = rw.ANTIBIOTIC_ID LEFT JOIN TB_ANTIBIOTIC_GROUP ag ON ag.ID = w.ANTIBIOTIC_GROUP LEFT JOIN TB_ANTIBIOTIC a ON a.ID = rw.ANTIBIOTIC_ID LEFT JOIN TB_ANTIBIOTIC_GROUP ag2 ON ag2.ID = a.ANTIBIOTIC_GROUP WHERE rw.REPORT_ID = " + id + "order by rw.ID DESC";

                //strSQL = "SELECT DISTINCT rw.ID,rw.REPORT_ID, COALESCE(w.ANTIBIOTIC, (SELECT ANTIBIOTIC FROM TB_ANTIBIOTIC WHERE ID = rw.ANTIBIOTIC_ID)) AS ANTIBIOTIC," +
                //   "COALESCE(w.ID, (SELECT ID FROM TB_ANTIBIOTIC WHERE ID = rw.ANTIBIOTIC_ID)) AS ANTIBIOTIC_ID, rw.ISOLATE1,rw.ISOLATE2,rw.ISOLATE3,rw.ADDL_INFO,rw.REMARKS FROM TB_ANTIBIOTIC w LEFT JOIN TB_REPORT_ENTRY rw ON w.ID = rw.ANTIBIOTIC_ID " +
                //   "AND rw.REPORT_ID = " + id + "UNION SELECT DISTINCT rw.ID,rw.REPORT_ID,(SELECT ANTIBIOTIC FROM TB_ANTIBIOTIC WHERE ID = rw.ANTIBIOTIC_ID) AS ANTIBIOTIC," +
                //   "(SELECT ID FROM TB_ANTIBIOTIC WHERE ID = rw.ANTIBIOTIC_ID) AS ANTIBIOTIC_ID,rw.ISOLATE1,rw.ISOLATE2,rw.ISOLATE3,rw.ADDL_INFO,rw.REMARKS FROM TB_REPORT_ENTRY rw WHERE rw.REPORT_ID = " + id + "order by rw.ID DESC";


                DataTable TblReportEntry = ADO.GetDataTable(strSQL, "ReportEntry");

                foreach (DataRow dr3 in TblReportEntry.Rows)
                {
                    ReportEntry.Add(new ReportEntry
                    {
                        ID = ADO.ToInt32(dr3["ID"]),
                        REPORT_ID = ADO.ToInt32(dr3["REPORT_ID"]),
                        ANTIBIOTIC_ID = ADO.ToInt32(dr3["ANTIBIOTIC_ID"]),
                        ANTIBIOTIC_NAME = ADO.ToString(dr3["ANTIBIOTIC"]),
                        ISOLATE1 = ADO.ToString(dr3["ISOLATE1"]),
                        ISOLATE2 = ADO.ToString(dr3["ISOLATE2"]),
                        ISOLATE3 = ADO.ToString(dr3["ISOLATE3"]),
                        ADDL_INFO = ADO.ToString(dr3["ADDL_INFO"]),
                        REMARKS = ADO.ToString(dr3["ADDL_INFO"]),
                        ANTIBIOTIC_GROUP_ID = ADO.ToInt32(dr3["ANTIBIOTIC_GROUP_ID"]),
                        ANTIBIOTIC_GROUP_NAME = ADO.ToString(dr3["ANTIBIOTIC_GROUP_NAME"])
                    });
                }

                strSQL = "SELECT DISTINCT rw.ID,rw.REPORT_ID, COALESCE(rw.GRAM_STAIN, w.DESCRIPTION) AS GRAM_STAIN," +
                    " rw.PRESENCE FROM TB_GRAM_STAIN w LEFT JOIN TB_REPORT_GRAM_STAIN rw ON w.DESCRIPTION = rw.GRAM_STAIN " +
                    "AND rw.REPORT_ID = " + id + "UNION SELECT DISTINCT rw.ID,rw.REPORT_ID, rw.GRAM_STAIN,rw.PRESENCE FROM TB_REPORT_GRAM_STAIN rw WHERE rw.REPORT_ID = " + id + "order by rw.ID DESC"; ;


                DataTable TblReportGramStain = ADO.GetDataTable(strSQL, "ReportGramStain");

                foreach (DataRow dr3 in TblReportGramStain.Rows)
                {
                    ReportGramStain.Add(new ReportGramStain
                    {
                        ID = ADO.ToInt32(dr3["ID"]),
                        REPORT_ID = ADO.ToInt32(dr3["REPORT_ID"]),
                        GRAM_STAIN = ADO.ToString(dr3["GRAM_STAIN"]),
                        PRESENCE = ADO.ToString(dr3["PRESENCE"])
                    });
                }

                strSQL = "SELECT DISTINCT rw.ID,rw.REPORT_ID, COALESCE(rw.WET_FILM, w.DESCRIPTION) AS WET_FILM," +
                    " rw.PRESENCE FROM TB_WET_FILM w LEFT JOIN TB_REPORT_WET_FILM rw ON w.DESCRIPTION = rw.WET_FILM " +
                    "AND rw.REPORT_ID = " + id + "UNION SELECT DISTINCT rw.ID,rw.REPORT_ID, rw.WET_FILM,rw.PRESENCE FROM TB_REPORT_WET_FILM rw WHERE rw.REPORT_ID = " + id + "order by rw.ID DESC"; ;

                DataTable TblReportWetFilm = ADO.GetDataTable(strSQL, "ReportWetFilm");

                foreach (DataRow dr3 in TblReportWetFilm.Rows)
                {
                    ReportWetFilm.Add(new ReportWetFilm
                    {
                        ID = ADO.ToInt32(dr3["ID"]),
                        REPORT_ID = ADO.ToInt32(dr3["REPORT_ID"]),
                        WET_FILM = ADO.ToString(dr3["WET_FILM"]),
                        PRESENCE = ADO.ToString(dr3["PRESENCE"])
                    });
                }

                report.ReportEntry = ReportEntry;
                report.ReportGramStain = ReportGramStain;
                report.ReportWetFilm = ReportWetFilm;
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return report;
        }

        public SaveReportResponse Update(Report report)
        {
            SaveReportResponse res = new SaveReportResponse();
            SqlConnection connection = new SqlConnection();
            try
            {

                connection = ADO.GetConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_REPORT";
                cmd.Parameters.AddWithValue("@ACTION", 2);
                cmd.Parameters.AddWithValue("@ID", report.ID);
                cmd.Parameters.AddWithValue("@COLLECTION_ID", report.COLLECTION_ID);
                cmd.Parameters.AddWithValue("@ZIEHEL_STAIN", report.ZIEHEL_STAIN);
                cmd.Parameters.AddWithValue("@SPECIAL_STAIN", report.SPECIAL_STAIN);
                cmd.Parameters.AddWithValue("@CULTURE", report.CULTURE);
                cmd.Parameters.AddWithValue("@REMARKS", report.REMARKS);
                cmd.Parameters.AddWithValue("@ISOLATE1_IDENTIFICATION", report.ISOLATE1_IDENTIFICATION);
                cmd.Parameters.AddWithValue("@ISOLATE1_GROWTH_RATE", report.ISOLATE1_GROWTH_RATE);
                cmd.Parameters.AddWithValue("@ISOLATE1_COLONY_COUNT", report.ISOLATE1_COLONY_COUNT);
                cmd.Parameters.AddWithValue("@ISOLATE2_IDENTIFICATION", report.ISOLATE2_IDENTIFICATION);
                cmd.Parameters.AddWithValue("@ISOLATE2_GROWTH_RATE", report.ISOLATE2_GROWTH_RATE);
                cmd.Parameters.AddWithValue("@ISOLATE2_COLONY_COUNT", report.ISOLATE2_COLONY_COUNT);
                cmd.Parameters.AddWithValue("@ISOLATE3_IDENTIFICATION", report.ISOLATE3_IDENTIFICATION);
                cmd.Parameters.AddWithValue("@ISOLATE3_GROWTH_RATE", report.ISOLATE3_GROWTH_RATE);
                cmd.Parameters.AddWithValue("@ISOLATE3_COLONY_COUNT", report.ISOLATE3_COLONY_COUNT);
                cmd.Parameters.AddWithValue("@IS_PRELIMINERY", report.IS_PRELIMINERY);
                cmd.Parameters.AddWithValue("@IS_PUBLISHED", report.IS_PUBLISHED);
                cmd.Parameters.AddWithValue("@USER_ID", report.USER_ID);
                cmd.Parameters.AddWithValue("@PREPARED_BY", report.PREPARED_BY);
                cmd.Parameters.AddWithValue("@APPROVED_BY", report.APPROVED_BY);
                cmd.Parameters.AddWithValue("@LAB_ID", report.LAB_ID);

                DataTable tbl = new DataTable();

                tbl.Columns.Add("REPORT_ID", typeof(int));
                tbl.Columns.Add("ANTIBIOTIC_ID", typeof(int));
                tbl.Columns.Add("ISOLATE1", typeof(string));
                tbl.Columns.Add("ISOLATE2", typeof(string));
                tbl.Columns.Add("ISOLATE3", typeof(string));
                tbl.Columns.Add("ADDL_INFO", typeof(string));
                tbl.Columns.Add("REMARKS", typeof(string));
                foreach (ReportEntry ur in report.ReportEntry)
                {
                    DataRow dRow = tbl.NewRow();

                    dRow["REPORT_ID"] = ur.REPORT_ID;
                    dRow["ANTIBIOTIC_ID"] = ur.ANTIBIOTIC_ID;
                    dRow["ISOLATE1"] = ur.ISOLATE1;
                    dRow["ISOLATE2"] = ur.ISOLATE2;
                    dRow["ISOLATE3"] = ur.ISOLATE3;
                    dRow["ADDL_INFO"] = ur.ADDL_INFO;
                    dRow["REMARKS"] = ur.REMARKS;
                    tbl.Rows.Add(dRow);
                    tbl.AcceptChanges();
                }

                DataTable tbl1 = new DataTable();

                tbl1.Columns.Add("REPORT_ID", typeof(int));
                tbl1.Columns.Add("GRAM_STAIN", typeof(string));
                tbl1.Columns.Add("PRESENCE", typeof(string));

                foreach (ReportGramStain ur in report.ReportGramStain)
                {
                    DataRow dRow = tbl1.NewRow();

                    dRow["REPORT_ID"] = ur.REPORT_ID;
                    dRow["GRAM_STAIN"] = ur.GRAM_STAIN;
                    dRow["PRESENCE"] = ur.PRESENCE;
                    tbl1.Rows.Add(dRow);
                    tbl1.AcceptChanges();
                }
                DataTable tbl2 = new DataTable();

                tbl2.Columns.Add("REPORT_ID", typeof(int));
                tbl2.Columns.Add("WET_FILM", typeof(string));
                tbl2.Columns.Add("PRESENCE", typeof(string));

                foreach (ReportWetFilm ur in report.ReportWetFilm)
                {
                    DataRow dRow = tbl2.NewRow();

                    dRow["REPORT_ID"] = ur.REPORT_ID;
                    dRow["WET_FILM"] = ur.WET_FILM;
                    dRow["PRESENCE"] = ur.PRESENCE;
                    tbl2.Rows.Add(dRow);
                    tbl2.AcceptChanges();
                }

                cmd.Parameters.AddWithValue("@UDT_REPORT_ENTRY", tbl);
                cmd.Parameters.AddWithValue("@UDT_REPORT_GRAM_STAIN", tbl1);
                cmd.Parameters.AddWithValue("@UDT_REPORT_WET_FILM", tbl2);


                cmd.ExecuteNonQuery();

                //SqlCommand cmd1 = new SqlCommand();
                //cmd1.Connection = connection;
                //cmd1.CommandType = CommandType.Text;
                //cmd1.CommandText = "SELECT MAX(ID) FROM TB_REPORT";
                //Int32 ReportlID = Convert.ToInt32(cmd1.ExecuteScalar());

                res.flag = 1;
                res.Message = "sucess";


            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
            return res;
        }
    }
}

