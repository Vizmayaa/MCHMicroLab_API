using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using VEZTA.Models;

namespace VEZTA.DAL
{
    public class ImportMaster_DAL
    {
        public ImportTypeResponse GetImportList(Int32 intUserID)
        {
            ImportTypeResponse response = new ImportTypeResponse();
            response.Master = new List<ImportMasterType>();
            response.Clinician = new List<ImportMasterColumn>();
            response.Denial = new List<ImportMasterColumn>();
            response.Insurance = new List<ImportMasterColumn>();
            response.Cpt = new List<ImportMasterColumn>();

            SqlConnection connection = ADO.GetConnection();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_IMPORT_MASTER_LIST";
            cmd.Parameters.AddWithValue("ACTION",0);
           
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            // SecuritySettings data
            DataTable tbl = ds.Tables[0];
            foreach (DataRow dr in tbl.Rows)
            {
                response.Master.Add(new ImportMasterType
                {
                    ID=ADO.ToInt32(dr["ID"]),
                    Master = ADO.ToString(dr["MasterTable"])
                });
            }

            // Clinicain
            if (ds.Tables.Count > 1)
            {
                DataTable tbl1 = ds.Tables[1];
                foreach (DataRow dr1 in tbl1.Rows)
                {
                    response.Clinician.Add(new ImportMasterColumn
                    {
                        ID = ADO.ToInt32(dr1["ID"]),
                        MasterID = ADO.ToInt32(dr1["MasterID"]),
                        ColumnName = ADO.ToString(dr1["ColumnName"]),
                        ColumnTitle = ADO.ToString(dr1["ColumnTitle"]),
                        IsNumeric = ADO.Toboolean(dr1["IsNumeric"]),
                        IsMandatory = ADO.Toboolean(dr1["IsMandatory"]),
                        MaxLength = ADO.ToInt32(dr1["MaxLength"]),
                        LOVTableName = ADO.ToString(dr1["LOVTableName"]),
                        LOVColumnName = ADO.ToString(dr1["LOVColumnName"])

                    });
                }
            }
            // Denial
            if (ds.Tables.Count > 2)
            {
                DataTable tbl2 = ds.Tables[2];
                foreach (DataRow dr2 in tbl2.Rows)
                {
                    response.Denial.Add(new ImportMasterColumn
                    {
                        ID = ADO.ToInt32(dr2["ID"]),
                        MasterID = ADO.ToInt32(dr2["MasterID"]),
                        ColumnName = ADO.ToString(dr2["ColumnName"]),
                        ColumnTitle = ADO.ToString(dr2["ColumnTitle"]),
                        IsNumeric = ADO.Toboolean(dr2["IsNumeric"]),
                        IsMandatory = ADO.Toboolean(dr2["IsMandatory"]),
                        MaxLength = ADO.ToInt32(dr2["MaxLength"]),
                        LOVTableName = ADO.ToString(dr2["LOVTableName"]),
                        LOVColumnName = ADO.ToString(dr2["LOVColumnName"])

                    });
                }
            }
            //Insurance
            if (ds.Tables.Count > 3)
            {
                DataTable tbl3 = ds.Tables[3];
                foreach (DataRow dr3 in tbl3.Rows)
                {
                    response.Insurance.Add(new ImportMasterColumn
                    {
                        ID = ADO.ToInt32(dr3["ID"]),
                        MasterID = ADO.ToInt32(dr3["MasterID"]),
                        ColumnName = ADO.ToString(dr3["ColumnName"]),
                        ColumnTitle = ADO.ToString(dr3["ColumnTitle"]),
                        IsNumeric = ADO.Toboolean(dr3["IsNumeric"]),
                        IsMandatory = ADO.Toboolean(dr3["IsMandatory"]),
                        MaxLength = ADO.ToInt32(dr3["MaxLength"]),
                        LOVTableName = ADO.ToString(dr3["LOVTableName"]),
                        LOVColumnName = ADO.ToString(dr3["LOVColumnName"])

                    });
                }
            }
            //Cpt
            if (ds.Tables.Count > 4)
            {
                DataTable tbl4 = ds.Tables[4];
                foreach (DataRow dr4 in tbl4.Rows)
                {
                    response.Cpt.Add(new ImportMasterColumn
                    {
                        ID = ADO.ToInt32(dr4["ID"]),
                        MasterID = ADO.ToInt32(dr4["MasterID"]),
                        ColumnName = ADO.ToString(dr4["ColumnName"]),
                        ColumnTitle = ADO.ToString(dr4["ColumnTitle"]),
                        IsNumeric = ADO.Toboolean(dr4["IsNumeric"]),
                        IsMandatory = ADO.Toboolean(dr4["IsMandatory"]),
                        MaxLength = ADO.ToInt32(dr4["MaxLength"]),
                        LOVTableName = ADO.ToString(dr4["LOVTableName"]),
                        LOVColumnName = ADO.ToString(dr4["LOVColumnName"])

                    });
                }
            }
            response.flag = 1;
            response.message = "Success";

            return response;
        }
        public List<ImportLog> GetAllList(Int32 intUserID)
        {


            List<ImportLog> ClinicianList = new List<ImportLog>();
            using (SqlConnection connection = ADO.GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_IMPORT_MASTER_LIST";
                cmd.Parameters.AddWithValue("ACTION", 1);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tbl = new DataTable();
                da.Fill(tbl);

                foreach (DataRow dr in tbl.Rows)
                {
                    ClinicianList.Add(new ImportLog
                    {
                        ID = ADO.ToInt32(dr["ID"]),
                        MasterID = ADO.ToInt32(dr["MasterID"]),
                        SerialNo = ADO.ToInt32(dr["SerialNo"]),
                        DocNo = ADO.ToInt32(dr["SerialNo"]),
                        UserID = ADO.ToInt32(dr["ImportUserID"]),
                        ImportTime= Convert.ToDateTime(dr["ImportTime"]),
                        NewRecordOnly = ADO.Toboolean(dr["NewRecordOnly"]),
                        UserName=ADO.ToString(dr["UserName"]),
                        Master = ADO.ToString(dr["MasterTable"])
                    });
                }
                connection.Close();
            }
            return ClinicianList;
        }
     
        public ImportLog GetItems(int id)
        {
            ImportLog import = new ImportLog();        
            List<ImportLogClinician> importclinician = new List<ImportLogClinician>();
            List<ImportLogDenial> importdenials = new List<ImportLogDenial>();
            List<ImportLogInsurance> importinsurance = new List<ImportLogInsurance>();
            List<ImportLogCpt> importcpt= new List<ImportLogCpt>();
            try
            {
                string strSQL = " SELECT IL.*, IM.MasterTable, U.UserName FROM TB_IMPORT_LOG IL " +
                    "LEFT JOIN TB_IMPORT_MASTER IM ON IL.MasterID = IM.ID " +
                    "LEFT JOIN TB_USER U ON IL.ImportUserID = U.UserID WHERE IL.ID = " + id;

                DataTable tbl = ADO.GetDataTable(strSQL, "Import");

                if (tbl.Rows.Count > 0)
                {
                    DataRow dr = tbl.Rows[0];

                    import = new ImportLog
                    {
                        ID = ADO.ToInt32(dr["ID"]),
                        MasterID = ADO.ToInt32(dr["MasterID"]),
                        SerialNo = ADO.ToInt32(dr["SerialNo"]),
                        DocNo = ADO.ToInt32(dr["SerialNo"]),
                        UserID = ADO.ToInt32(dr["ImportUserID"]),
                        ImportTime = Convert.ToDateTime(dr["ImportTime"]),
                        NewRecordOnly = ADO.Toboolean(dr["NewRecordOnly"]),
                        UserName = ADO.ToString(dr["UserName"]),
                        Master = ADO.ToString(dr["MasterTable"])
                    };
                }
                // Query to get item aliases
                string strSQL1 = "SELECT * FROM TB_IMPORT_LOG_CLINICIAN WHERE TB_IMPORT_LOG_CLINICIAN.LogID = " + id;

                DataTable tbl1 = ADO.GetDataTable(strSQL1, "ImportClinician");

                foreach (DataRow dr1 in tbl1.Rows)
                {
                    importclinician.Add(new ImportLogClinician
                    {
                        ID = ADO.ToInt32(dr1["ID"]),
                        LogID = ADO.ToInt32(dr1["LogID"]),
                        ClinicianLicense = ADO.ToString(dr1["ClinicianLicense"]),
                        ClinicianName = ADO.ToString(dr1["ClinicianName"]),
                        ClinicianShortName = ADO.ToString(dr1["ClinicianShortName"]),
                        Speciality = ADO.ToString(dr1["Speciality"]),
                        ClinicianMajor = ADO.ToString(dr1["ClinicianMajor"]),
                        ClinicianProfession = ADO.ToString(dr1["ClinicianProfession"]),
                        ClinicianCategory = ADO.ToString(dr1["ClinicianCategory"]),
                        Gender = ADO.ToString(dr1["Gender"])
                    });
                }
                string strSQL2 = "SELECT * FROM TB_IMPORT_LOG_DENIAL WHERE TB_IMPORT_LOG_DENIAL.LogID = " + id;

                DataTable tbl2 = ADO.GetDataTable(strSQL2, "ImportDenials");

                foreach (DataRow dr2 in tbl2.Rows)
                {
                    importdenials.Add(new ImportLogDenial
                    {
                        ID = ADO.ToInt32(dr2["ID"]),
                        LogID = ADO.ToInt32(dr2["LogID"]),
                        DenialCode = ADO.ToString(dr2["DenialCode"]),
                        DenialName = ADO.ToString(dr2["DenialName"]),
                        DenialType = ADO.ToString(dr2["DenialType"]),
                        DenialCategory = ADO.ToString(dr2["DenialCategory"]),
                        Description = ADO.ToString(dr2["Description"]),
                    });
                }
                string strSQL3 = "SELECT * FROM TB_IMPORT_LOG_INSURANCE WHERE TB_IMPORT_LOG_INSURANCE.LogID = " + id;

                DataTable tbl3 = ADO.GetDataTable(strSQL3, "ImportInsurance");

                foreach (DataRow dr3 in tbl3.Rows)
                {
                    importinsurance.Add(new ImportLogInsurance
                    {
                        ID = ADO.ToInt32(dr3["ID"]),
                        LogID = ADO.ToInt32(dr3["LogID"]),
                        InsuranceID = ADO.ToString(dr3["InsuranceID"]),
                        InsuranceName = ADO.ToString(dr3["InsuranceName"]),
                        InsuranceShortName = ADO.ToString(dr3["InsuranceShortName"]),
                        Classification = ADO.ToString(dr3["Classification"])
                   
                    });
                }
                string strSQL4 = "SELECT * FROM TB_IMPORT_LOG_CPT WHERE TB_IMPORT_LOG_CPT.LogID = " + id;

                DataTable tbl4 = ADO.GetDataTable(strSQL4, "ImportCpt");

                foreach (DataRow dr4 in tbl4.Rows)
                {
                    importcpt.Add(new ImportLogCpt
                    {
                        ID = ADO.ToInt32(dr4["ID"]),
                        LogID = ADO.ToInt32(dr4["LogID"]),
                        CPTCode = ADO.ToString(dr4["CPTCode"]),
                        CPTShortName = ADO.ToString(dr4["CPTShortName"]),
                        CPTName = ADO.ToString(dr4["CPTName"]),
                        CPTType = ADO.ToString(dr4["CPTType"]),
                        Description = ADO.ToString(dr4["Description"])
                        
                    });
                }
                import.import_clinician = importclinician;
                import.import_Denial = importdenials;
                import.import_Insurance = importinsurance;
                import.import_Cpt = importcpt;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return import;
        }
        public bool Insert(ImportMasterInput items)
        {
            SqlConnection connection = ADO.GetConnection();
            SqlTransaction objtrans = connection.BeginTransaction();
            try
            {
                // Create DataTables for each category
                DataTable tblClinician = CreateClinicianDataTable(items);
                DataTable tblDenial = CreateDenialDataTable(items);
                DataTable tblInsurance = CreateInsuranceDataTable(items);
                DataTable tblCpt = CreateCptDataTable(items);

                // SqlCommand setup
                SqlCommand cmd = new SqlCommand
                {
                    Connection = connection,
                    Transaction = objtrans,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "SP_IMPORT_MASTER",
                    CommandTimeout = 3000
                };

                cmd.Parameters.AddWithValue("@MasterID", items.MasterID);
                cmd.Parameters.AddWithValue("@UserID", items.UserID);
                cmd.Parameters.AddWithValue("@BatchNo", items.BatchNo);
                cmd.Parameters.AddWithValue("@NewRecordOnly", items.NewRecordOnly);
                cmd.Parameters.AddWithValue("@Action", items.Action);

                cmd.Parameters.AddWithValue("@UDT_TB_IMPORT_LOG_CLINICIAN", tblClinician);
                cmd.Parameters.AddWithValue("@UDT_TB_IMPORT_LOG_DENIAL", tblDenial);
                cmd.Parameters.AddWithValue("@UDT_TB_IMPORT_LOG_INSURANCE", tblInsurance);
                cmd.Parameters.AddWithValue("@UDT_TB_IMPORT_LOG_CPT", tblCpt);

                cmd.ExecuteNonQuery();
                objtrans.Commit();
                connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                objtrans.Rollback();
                connection.Close();
                throw ex;
            }
        }

        // Create DataTable for Clinician
        private DataTable CreateClinicianDataTable(ImportMasterInput items)
        {
            DataTable tbl = new DataTable();
            tbl.Columns.Add("ClinicianLicense", typeof(string));
            tbl.Columns.Add("ClinicianName", typeof(string));
            tbl.Columns.Add("ClinicianShortName", typeof(string));
            tbl.Columns.Add("Speciality", typeof(string));
            tbl.Columns.Add("ClinicianMajor", typeof(string));
            tbl.Columns.Add("ClinicianProfession", typeof(string));
            tbl.Columns.Add("ClinicianCategory", typeof(string));
            tbl.Columns.Add("Gender", typeof(string));

            items.import_clinician?.ForEach(ur => tbl.Rows.Add(
                ur.ClinicianLicense, ur.ClinicianName, ur.ClinicianShortName, ur.Speciality,
                ur.ClinicianMajor, ur.ClinicianProfession, ur.ClinicianCategory, ur.Gender
            ));

            tbl.AcceptChanges();
            return tbl;
        }

        // Create DataTable for Denial
        private DataTable CreateDenialDataTable(ImportMasterInput items)
        {
            DataTable tbl = new DataTable();
            tbl.Columns.Add("DenialCode", typeof(string));
            tbl.Columns.Add("DenialName", typeof(string));
            tbl.Columns.Add("DenialType", typeof(string));
            tbl.Columns.Add("DenialCategory", typeof(string));
            tbl.Columns.Add("Description", typeof(string));

            items.import_Denial?.ForEach(ur => tbl.Rows.Add(
                ur.DenialCode, ur.DenialName, ur.DenialType, ur.DenialCategory, ur.Description
            ));

            tbl.AcceptChanges();
            return tbl;
        }

        // Create DataTable for Insurance
        private DataTable CreateInsuranceDataTable(ImportMasterInput items)
        {
            DataTable tbl = new DataTable();
            tbl.Columns.Add("InsuranceID", typeof(string));
            tbl.Columns.Add("InsuranceName", typeof(string));
            tbl.Columns.Add("InsuranceShortName", typeof(string));
            tbl.Columns.Add("Classification", typeof(string));

            items.import_Insurance?.ForEach(ur => tbl.Rows.Add(
                ur.InsuranceID, ur.InsuranceName, ur.InsuranceShortName, ur.Classification
            ));

            tbl.AcceptChanges();
            return tbl;
        }

        // Create DataTable for CPT
        private DataTable CreateCptDataTable(ImportMasterInput items)
        {
            DataTable tbl = new DataTable();
            tbl.Columns.Add("CPTCode", typeof(string));
            tbl.Columns.Add("CPTShortName", typeof(string));
            tbl.Columns.Add("CPTName", typeof(string));
            tbl.Columns.Add("CPTType", typeof(string));
            tbl.Columns.Add("Description", typeof(string));

            items.import_Cpt?.ForEach(ur => tbl.Rows.Add(
                ur.CPTCode, ur.CPTShortName, ur.CPTName, ur.CPTType, ur.Description
            ));

            tbl.AcceptChanges();
            return tbl;
        }



    }
}