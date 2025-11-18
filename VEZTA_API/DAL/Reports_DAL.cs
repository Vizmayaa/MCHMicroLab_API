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
    public class Reports_DAL
    {


        public ReportParameterValues GetReportParameterValues(string UserID)
        {

            ReportParameterValues vReport = new ReportParameterValues();

            try
            {

                List<FacilityData> Lstfac = new List<FacilityData>();


                //Facility 
                string strSQL = "SELECT CAST(TB_FACILITY.ID AS VARCHAR) AS ID, FacilityLicense, FacilityLicense + '-' + FacilityName AS NAME, " +
                                "CAST(CASE WHEN TB_FACILITY_GROUP.ID IS NULL THEN 0 ELSE TB_FACILITY_GROUP.ID END AS VARCHAR) FacilityGroupID, " +
                                "CASE WHEN TB_FACILITY_GROUP.ID IS NULL THEN 'None' ELSE FacilityGroup END FacilityGroup  FROM " +
                                "TB_FACILITY LEFT JOIN TB_FACILITY_GROUP ON TB_FACILITY.FacilityGroupID = TB_FACILITY_GROUP.ID " +
                                "where TB_FACILITY.IsDeleted=0";

                DataTable tblFacility = ADO.GetDataTable(strSQL, "TB_FACILITY");
                IList<FacilityData> lstcols = tblFacility.AsEnumerable().Select(row => new FacilityData
                {
                    ID = row.Field<string>("ID"),
                    Code = row.Field<string>("FacilityLicense"),
                    Name = row.Field<string>("NAME"),
                    GroupID = row.Field<string>("FacilityGroupID"),
                    GroupName = row.Field<string>("FacilityGroup"),

                }).ToList();


                vReport.facility = lstcols.ToList();

                List<ReportParameterData> vSearchOn = new List<ReportParameterData>();
                vSearchOn.Add(new ReportParameterData
                {
                    ID = "EncounterStartDate",
                    Name = "EncounterStartDate"
                });
                vSearchOn.Add(new ReportParameterData
                {
                    ID = "SubmissionTransactionDate",
                    Name = "SubmissionTransactionDate"

                });
                vSearchOn.Add(new ReportParameterData
                {
                    ID = "RemittanceTransactionDate",
                    Name = "RemittanceTransactionDate"

                });
                vSearchOn.Add(new ReportParameterData
                {
                    ID = "EncounterEndDate",
                    Name = "EncounterEndDate"

                });
                vReport.SearchOn = vSearchOn;


                List<ReportParameterData> LstEncounterType = new List<ReportParameterData>();
                LstEncounterType.Add(new ReportParameterData
                {
                    ID = "",
                    Name = "All"

                });
                LstEncounterType.Add(new ReportParameterData
                {
                    ID = "IP",
                    Name = "IP"

                });
                LstEncounterType.Add(new ReportParameterData
                {
                    ID = "OP",
                    Name = "OP"
                });
                vReport.EncounterType = LstEncounterType;


                strSQL = "SELECT TOP 500 CAST(ID AS VARCHAR) AS ID, InsuranceID + '-' + InsuranceName as NAME FROM TB_INSURANCE_COMPANY WHERE IsDeleted =0";
                DataTable tblInsu = ADO.GetDataTable(strSQL);

                IList<ReportParameterData> LstInsu = tblInsu.AsEnumerable().Select(row => new ReportParameterData
                {
                    ID = row.Field<string>("ID"),
                    Name = row.Field<string>("NAME")
                }).ToList();

                vReport.ReceiverID = LstInsu.ToList();
                vReport.PayerID = LstInsu.ToList();


                List<ReportParameterData> vPayer = new List<ReportParameterData>();
                vPayer.Add(new ReportParameterData
                {
                    ID = "1",
                    Name = "Insurance"

                });
                vPayer.Add(new ReportParameterData
                {
                    ID = "2",
                    Name = "Self"

                });
                vPayer.Add(new ReportParameterData
                {
                    ID = "0",
                    Name = "ALL"
                });
                vReport.Payer = vPayer;



                strSQL = "SELECT TOP 500 CAST(ID AS VARCHAR) AS ID, ClinicianLicense + '-' + ClinicianName as NAME FROM TB_CLINICIAN WHERE IsDeleted =0";
                DataTable tblClinician = ADO.GetDataTable(strSQL);
                IList<ReportParameterData> LstClinician = tblClinician.AsEnumerable().Select(row => new ReportParameterData
                {
                    ID = row.Field<string>("ID"),
                    Name = row.Field<string>("NAME")
                }).ToList();
                vReport.Clinician = LstClinician.ToList();
                vReport.OrderingClinician = LstClinician.ToList();


                List<ReportParameterData> LstClaimStatus = new List<ReportParameterData>();
                LstClaimStatus.Add(new ReportParameterData
                {
                    ID = "1",
                    Name = "Initial Not Remitted"

                });
                LstClaimStatus.Add(new ReportParameterData
                {
                    ID = "2",
                    Name = "Resub Not Remitted"

                });
                LstClaimStatus.Add(new ReportParameterData
                {
                    ID = "3",
                    Name = "Remitted"
                });
                vReport.ClaimStatus = LstClaimStatus;

                List<ReportParameterData> LstResubmisonType = new List<ReportParameterData>();
                LstResubmisonType.Add(new ReportParameterData
                {
                    ID = "0",
                    Name = "All"

                });
                LstResubmisonType.Add(new ReportParameterData
                {
                    ID = "1",
                    Name = "Correction"

                });
                LstResubmisonType.Add(new ReportParameterData
                {
                    ID = "2",
                    Name = "Internal Complaint"
                });
                LstResubmisonType.Add(new ReportParameterData
                {
                    ID = "3",
                    Name = "Reconciliation"
                });
                LstResubmisonType.Add(new ReportParameterData
                {
                    ID = "4",
                    Name = "Legacy"
                });
                vReport.ResubmissionType = LstResubmisonType;

                List<ReportParameterData> LstPaymentStatus = new List<ReportParameterData>();
                LstPaymentStatus.Add(new ReportParameterData
                {
                    ID = "0",
                    Name = "All"
                });
                LstPaymentStatus.Add(new ReportParameterData
                {
                    ID = "1",
                    Name = "Fully Paid"
                });
                LstPaymentStatus.Add(new ReportParameterData
                {
                    ID = "2",
                    Name = "Fully Rejected"
                });
                LstPaymentStatus.Add(new ReportParameterData
                {
                    ID = "3",
                    Name = "Partially Paid"
                });
                vReport.PaymentStatus = LstPaymentStatus;


                List<ReportAdvanceFilterColumns> LstAdvFiler = new List<ReportAdvanceFilterColumns>();

                LstAdvFiler.Add(new ReportAdvanceFilterColumns { dataField = "ClaimNumber", caption = "Claim Number", values = "" });
                LstAdvFiler.Add(new ReportAdvanceFilterColumns { dataField = "ReceiverID", caption = "Receiver ID", values = "" });
                LstAdvFiler.Add(new ReportAdvanceFilterColumns { dataField = "PayerID", caption = "Payer ID", values = "" });
                LstAdvFiler.Add(new ReportAdvanceFilterColumns { dataField = "Clinician", caption = "Clinician", values = "" });
                LstAdvFiler.Add(new ReportAdvanceFilterColumns { dataField = "OrderingClinician", caption = "Ordering Clinician", values = "" });

                vReport.AdvanceFilter = LstAdvFiler;










                vReport.flag = "1";
                vReport.message = "Success";
            }
            catch (Exception ex)
            {
                vReport.flag = "0";
                vReport.message = ex.Message;
            }
            return vReport;
        }
        public ReportParameterValues GetReportParameterValues(string UserID, string ReportID)
        {

            ReportParameterValues vReport = new ReportParameterValues();

            try
            {
                if (UserID == "" || ReportID == "")
                {
                    vReport.flag = "0";
                    vReport.message = "Input missing";
                    return vReport;
                }


                //Facility 
                string strSQL = "SELECT CAST(TB_FACILITY.ID AS VARCHAR) AS ID, FacilityLicense, FacilityLicense + '-' + FacilityName AS NAME, " +
                                 "CAST(CASE WHEN TB_FACILITY_GROUP.ID IS NULL THEN 0 ELSE TB_FACILITY_GROUP.ID END AS VARCHAR) FacilityGroupID, " +
                                 "CASE WHEN TB_FACILITY_GROUP.ID IS NULL THEN 'None' ELSE FacilityGroup END FacilityGroup  FROM " +
                                 "TB_FACILITY LEFT JOIN TB_FACILITY_GROUP ON TB_FACILITY.FacilityGroupID = TB_FACILITY_GROUP.ID " +
                                 "where TB_FACILITY.IsDeleted=0";

                DataTable tblFacility = ADO.GetDataTable(strSQL, "TB_FACILITY");
                IList<FacilityData> LstFacility = tblFacility.AsEnumerable().Select(row => new FacilityData
                {
                    ID = row.Field<string>("ID"),
                    Code = row.Field<string>("FacilityLicense"),
                    Name = row.Field<string>("NAME"),
                    GroupID = row.Field<string>("FacilityGroupID"),
                    GroupName = row.Field<string>("FacilityGroup"),

                }).ToList();
                vReport.facility = LstFacility.ToList();



                List<ReportParameterData> vSearchOn = new List<ReportParameterData>();
                vSearchOn.Add(new ReportParameterData
                {
                    ID = "EncounterStartDate",
                    Name = "EncounterStartDate"
                });
                vSearchOn.Add(new ReportParameterData
                {
                    ID = "SubmissionTransactionDate",
                    Name = "SubmissionTransactionDate"

                });
                vSearchOn.Add(new ReportParameterData
                {
                    ID = "RemittanceTransactionDate",
                    Name = "RemittanceTransactionDate"

                });
                vSearchOn.Add(new ReportParameterData
                {
                    ID = "EncounterEndDate",
                    Name = "EncounterEndDate"

                });
                vReport.SearchOn = vSearchOn;


                List<ReportParameterData> LstEncounterType = new List<ReportParameterData>();
                LstEncounterType.Add(new ReportParameterData
                {
                    ID = "",
                    Name = "All"

                });
                LstEncounterType.Add(new ReportParameterData
                {
                    ID = "IP",
                    Name = "IP"

                });
                LstEncounterType.Add(new ReportParameterData
                {
                    ID = "OP",
                    Name = "OP"
                });
                vReport.EncounterType = LstEncounterType;


                strSQL = "SELECT TOP 500 CAST(ID AS VARCHAR) AS ID, InsuranceID + '-' + InsuranceName as NAME FROM TB_INSURANCE_COMPANY WHERE IsDeleted =0";
                DataTable tblInsu = ADO.GetDataTable(strSQL);

                IList<ReportParameterData> LstInsu = tblInsu.AsEnumerable().Select(row => new ReportParameterData
                {
                    ID = row.Field<string>("ID"),
                    Name = row.Field<string>("NAME")
                }).ToList();

                vReport.ReceiverID = LstInsu.ToList();
                vReport.PayerID = LstInsu.ToList();



                List<ReportParameterData> vPayer = new List<ReportParameterData>();
                vPayer.Add(new ReportParameterData
                {
                    ID = "1",
                    Name = "Insurance"

                });
                vPayer.Add(new ReportParameterData
                {
                    ID = "2",
                    Name = "Self"

                });
                vPayer.Add(new ReportParameterData
                {
                    ID = "0",
                    Name = "ALL"
                });
                vReport.Payer = vPayer;


                strSQL = "SELECT TOP 500 CAST(ID AS VARCHAR) AS ID, ClinicianLicense + '-' + ClinicianName as NAME FROM TB_CLINICIAN WHERE IsDeleted =0";
                DataTable tblClinician = ADO.GetDataTable(strSQL);
                IList<ReportParameterData> LstClinician = tblClinician.AsEnumerable().Select(row => new ReportParameterData
                {
                    ID = row.Field<string>("ID"),
                    Name = row.Field<string>("NAME")
                }).ToList();
                vReport.Clinician = LstClinician.ToList();
                vReport.OrderingClinician = LstClinician.ToList();



                List<ReportParameterData> LstClaimStatus = new List<ReportParameterData>();
                LstClaimStatus.Add(new ReportParameterData
                {
                    ID = "1",
                    Name = "Initial Not Remitted"

                });
                LstClaimStatus.Add(new ReportParameterData
                {
                    ID = "2",
                    Name = "Resub Not Remitted"

                });
                LstClaimStatus.Add(new ReportParameterData
                {
                    ID = "3",
                    Name = "Remitted"
                });
                vReport.ClaimStatus = LstClaimStatus.ToList();

                List<ReportParameterData> LstResubmisonType = new List<ReportParameterData>();
                LstResubmisonType.Add(new ReportParameterData
                {
                    ID = "0",
                    Name = "All"

                });
                LstResubmisonType.Add(new ReportParameterData
                {
                    ID = "1",
                    Name = "Correction"

                });
                LstResubmisonType.Add(new ReportParameterData
                {
                    ID = "2",
                    Name = "Internal Complaint"
                });
                LstResubmisonType.Add(new ReportParameterData
                {
                    ID = "3",
                    Name = "Reconciliation"
                });
                LstResubmisonType.Add(new ReportParameterData
                {
                    ID = "4",
                    Name = "Legacy"
                });
                vReport.ResubmissionType = LstResubmisonType.ToList();

                List<ReportParameterData> LstPaymentStatus = new List<ReportParameterData>();
                LstPaymentStatus.Add(new ReportParameterData
                {
                    ID = "0",
                    Name = "All"

                });

                LstPaymentStatus.Add(new ReportParameterData
                {
                    ID = "1",
                    Name = "Fully Paid"

                });
                LstPaymentStatus.Add(new ReportParameterData
                {
                    ID = "2",
                    Name = "Fully Rejected"

                });
                LstPaymentStatus.Add(new ReportParameterData
                {
                    ID = "3",
                    Name = "Partially Paid"

                });
                vReport.PaymentStatus = LstPaymentStatus.ToList();


                List<ReportAdvanceFilterColumns> LstAdvFiler = new List<ReportAdvanceFilterColumns>();

                LstAdvFiler.Add(new ReportAdvanceFilterColumns { dataField = "ClaimNumber", caption = "Claim Number", values = "" });
                LstAdvFiler.Add(new ReportAdvanceFilterColumns { dataField = "ReceiverID", caption = "Receiver ID", values = "" });
                LstAdvFiler.Add(new ReportAdvanceFilterColumns { dataField = "PayerID", caption = "Payer ID", values = "" });
                LstAdvFiler.Add(new ReportAdvanceFilterColumns { dataField = "Clinician", caption = "Clinician", values = "" });
                LstAdvFiler.Add(new ReportAdvanceFilterColumns { dataField = "OrderingClinician", caption = "Ordering Clinician", values = "" });

                vReport.AdvanceFilter = LstAdvFiler;










                vReport.flag = "1";
                vReport.message = "Success";
            }
            catch (Exception ex)
            {
                vReport.flag = "0";
                vReport.message = ex.Message;
            }
            return vReport;
        }
        //////public ClaimDetailReport GetClaimDetails(ReportParameters vInput)
        //////{
        //////    ClaimDetailReport vReport = new ClaimDetailReport();
        //////    SqlConnection conn = new SqlConnection();
        //////    try
        //////    {
        //////        conn = ADO.GetConnection();

        //////        List<ClaimDetailData> ReportData = new List<ClaimDetailData>();
        //////        List<ReportColumns> LstColumns = new List<ReportColumns>();
        //////        List<ReportAdvanceFilterColumns> LstAdvFiler = new List<ReportAdvanceFilterColumns>();

        //////        // First Stored Procedure
        //////        DataTable tbl = new DataTable();

        //////        SqlCommand cmd = new SqlCommand();
        //////        cmd.Connection = conn;
        //////        cmd.CommandType = CommandType.StoredProcedure;
        //////        cmd.CommandText = "SP_RPT_CLAIM_SUMMARY";


        //////        cmd.Parameters.AddWithValue("@SearchOn", vInput.SearchOn);
        //////        cmd.Parameters.AddWithValue("@DateFrom", Convert.ToDateTime(vInput.DateFrom));
        //////        cmd.Parameters.AddWithValue("@DateTo", Convert.ToDateTime(vInput.DateTo));
        //////        cmd.Parameters.AddWithValue("@EncounterType", vInput.EncounterType);
        //////        cmd.Parameters.AddWithValue("@FacilityID", vInput.Facility);
        //////        cmd.Parameters.AddWithValue("@ReceiverID", vInput.ReceiverID == null ? "": vInput.ReceiverID.ToString());
        //////        cmd.Parameters.AddWithValue("@PayerID", vInput.PayerID == null ? "": vInput.PayerID.ToString());
        //////        cmd.Parameters.AddWithValue("@Payer", vInput.Payer == null ? "" : vInput.Payer.ToString());
        //////        cmd.Parameters.AddWithValue("@OrderingClinician", vInput.OrderingClinician == null ? "" : vInput.OrderingClinician.ToString());

        //////        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //////        da.Fill(tbl);



        //////        if (tbl.Rows.Count > 0)
        //////        {
        //////            vReport.flag = "1";
        //////            vReport.message = "Success";
        //////            vReport.ReportID = "claim-summary-date-page";

        //////            foreach (DataColumn col in tbl.Columns)
        //////            {
        //////                LstColumns.Add(new ReportColumns
        //////                {
        //////                    Title = col.Caption,
        //////                    Name = col.Caption,
        //////                    ToolTip = col.Caption,
        //////                    Type = col.DataType.Name.ToString(),
        //////                    Visibility = true,
        //////                    Group = true,
        //////                    Summary = true
        //////                });
        //////            }

        //////            IList<ClaimDetailData> lstt = tbl.AsEnumerable().Select(row => new ClaimDetailData
        //////            {

        //////                FacilityID = row.Field<string>("FacilityID"),
        //////                FacilityName = row.Field<string>("FacilityName"),
        //////                ClaimNumber = row.Field<string>("ClaimNumber"),
        //////                ReceiverID = row.Field<string>("ReceiverID"),
        //////                ReceiverShortName = row.Field<string>("ReceiverShortName"),
        //////                ReceiverName = row.Field<string>("ReceiverName"),
        //////                PayerID = row.Field<string>("PayerID"),
        //////                PayerShortName = row.Field<string>("PayerShortName"),
        //////                PayerName = row.Field<string>("PayerName"),
        //////                Clinician = row.Field<string>("Clinician"),
        //////                ClinicianShortName = row.Field<string>("ClinicianShortName"),
        //////                ClinicianName = row.Field<string>("ClinicianName"),
        //////                EncounterType = row.Field<string>("EncounterType"),
        //////                ClaimedCount = row.Field<object>("ClaimedCount"),
        //////                ClaimedAmount = row.Field<object>("ClaimedAmount"),
        //////                RemittedCount = row.Field<object>("RemittedCount"),
        //////                RemittedAmount = row.Field<object>("RemittedAmount"),
        //////                RejectedCount = row.Field<object>("RejectedCount"),
        //////                RejectedAmount = row.Field<object>("RejectedAmount"),
        //////                PaidCount = row.Field<object>("PaidCount"),
        //////                PaidAmount = row.Field<object>("PaidAmount"),
        //////                BalanceCount = row.Field<object>("BalanceCount"),
        //////                BalanceAmount = row.Field<object>("BalanceAmount"),
        //////                LastResubmissionDate = row.Field<object>("LastResubmissionDate"),
        //////                LastRemittanceDate = row.Field<object>("LastRemittanceDate"),
        //////                RemittanceStatus = row.Field<string>("RemittanceStatus"),
        //////                CPTCode = row.Field<string>("CPTCode"),
        //////                CPTName = row.Field<string>("CPTName"),
        //////                CPTGroup = row.Field<string>("CPTGroup"),
        //////                EncounterStartDate = row.Field<object>("EncounterStartDate"),
        //////                EncounterEndDate = row.Field<object>("EncounterEndDate"),
        //////            }).ToList();

        //////            vReport.ReportColumns = LstColumns.ToList();
        //////            vReport.ReportData = lstt.ToList();

        //////        }

        //////        vReport.PersonalReports = GetPeronalReports(vInput.userid, vReport.ReportID);

        //////        LstAdvFiler.Add(new ReportAdvanceFilterColumns { dataField = "ClaimNumber", caption = "Claim Number", values = "" });
        //////        LstAdvFiler.Add(new ReportAdvanceFilterColumns { dataField = "ReceiverID", caption = "Receiver ID", values = "" });
        //////        LstAdvFiler.Add(new ReportAdvanceFilterColumns { dataField = "PayerID", caption = "Payer ID", values = "" });
        //////        LstAdvFiler.Add(new ReportAdvanceFilterColumns { dataField = "Clinician", caption = "Clinician", values = "" });
        //////        LstAdvFiler.Add(new ReportAdvanceFilterColumns { dataField = "OrderingClinician", caption = "Ordering Clinician", values = "" });

        //////        vReport.AdvanceFilter = LstAdvFiler;


        //////    }
        //////    catch (Exception ex)
        //////    {
        //////        vReport.flag = "0";
        //////        vReport.message = ex.Message;
        //////    }
        //////    finally
        //////    {
        //////        if (conn.State == ConnectionState.Open)
        //////            conn.Close();

        //////    }
        //////    return vReport;
        //////}
        public RptClaimDetailWithActivityOutput GetClaimDetailsWithActivity(ReportParameters vInput)
        {
            RptClaimDetailWithActivityOutput vReport = new RptClaimDetailWithActivityOutput();

            SqlConnection conn = new SqlConnection();
            try
            {
                conn = ADO.GetConnection();

                List<RptClaimDetailWithActivityData> ReportData = new List<RptClaimDetailWithActivityData>();
                List<ReportAdvanceFilterColumns> LstAdvFiler = new List<ReportAdvanceFilterColumns>();

                // First Stored Procedure
                DataTable tbl = new DataTable();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_RPT_BI_ACTIVITY_CLAIM_DEAIL";
                cmd.Parameters.AddWithValue("@UserID", vInput.userid);
                cmd.Parameters.AddWithValue("@SearchOn", vInput.SearchOn);
                cmd.Parameters.AddWithValue("@DateFrom", Convert.ToDateTime(vInput.DateFrom));
                cmd.Parameters.AddWithValue("@DateTo", Convert.ToDateTime(vInput.DateTo));
                cmd.Parameters.AddWithValue("@EncounterType", vInput.EncounterType == null ? "" : vInput.EncounterType); ;
                cmd.Parameters.AddWithValue("@FacilityID", vInput.Facility == null ? "" : vInput.Facility);
                cmd.Parameters.AddWithValue("@ReceiverID", vInput.ReceiverID == null ? "" : vInput.ReceiverID.ToString());
                cmd.Parameters.AddWithValue("@PayerID", vInput.PayerID == null ? "" : vInput.PayerID.ToString());


                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tbl);


                vReport.ReportID = "claim-detail-activity";
                if (tbl.Rows.Count > 0)
                {

                    IList<RptClaimDetailWithActivityData> lstt = tbl.AsEnumerable().Select(row => new RptClaimDetailWithActivityData
                    {

                        FacilityGroupID = row.Field<object>("FacilityGroupID"),
                        HealthAuthority = row.Field<object>("HealthAuthority"),
                        FacilityID = row.Field<object>("FacilityID"),
                        FacilityName = row.Field<object>("FacilityName"),
                        ClaimNumber = row.Field<object>("ClaimNumber"),
                        PatientID = row.Field<object>("PatientID"),
                        TransactionDate = row.Field<object>("TransactionDate"),
                        ActivityStartDate = row.Field<object>("ActivityStartDate"),
                        EncounterType = row.Field<object>("EncounterType"),
                        EncounterStartDate = row.Field<object>("EncounterStartDate"),
                        EncounterEndDate = row.Field<object>("EncounterEndDate"),
                        ClaimActivityNumber = row.Field<object>("ClaimActivityNumber"),
                        CPTCode = row.Field<object>("CPTCode"),
                        CPTCategory = row.Field<object>("CPTCategory"),
                        CPTName = row.Field<object>("CPTName"),
                        CPTType = row.Field<object>("CPTType"),
                        Quantity = row.Field<object>("Quantity"),
                        OrderingClinician = row.Field<object>("OrderingClinician"),
                        OrderingClinicianName = row.Field<object>("OrderingClinicianName"),
                        Clinician = row.Field<object>("Clinician"),
                        ClinicianName = row.Field<object>("ClinicianName"),
                        ReceiverID = row.Field<object>("ReceiverID"),
                        ReceiverName = row.Field<object>("ReceiverName"),
                        PayerID = row.Field<object>("PayerID"),
                        PayerName = row.Field<object>("PayerName"),
                        MemberID = row.Field<object>("MemberID"),
                        EmiratesIDNumber = row.Field<object>("EmiratesIDNumber"),
                        IDPayer = row.Field<object>("IDPayer"),
                        PaymentReference = row.Field<object>("PaymentReference"),
                        PriorAuthorizationID = row.Field<object>("PriorAuthorizationID"),
                        NetAmt = row.Field<object>("NetAmt"),

                        InitialNetAmt = row.Field<object>("InitialNetAmt"),
                        Diagnosis = row.Field<object>("Diagnosis"),
                        PrimaryDiagnosis = row.Field<object>("PrimaryDiagnosis"),
                        PrimaryDiagnosisDescription = row.Field<object>("PrimaryDiagnosisDescription"),
                        LastResubmissionDate = row.Field<object>("LastResubmissionDate"),
                        FirstRemittanceDate = row.Field<object>("FirstRemittanceDate"),
                        LastRemittanceDate = row.Field<object>("LastRemittanceDate"),
                        RemittedAmt = row.Field<object>("RemittedAmt"),
                        LastRemittedAmount = row.Field<object>("LastRemittedAmount"),
                        InitialRejectedAmt = row.Field<object>("InitialRejectedAmt"),
                        RejectedAmt = row.Field<object>("RejectedAmt"),
                        UnprocessedAmt = row.Field<object>("UnprocessedAmt"),
                        RejectionPercentage = row.Field<object>("RejectionPercentage"),
                        WriteOffAmt = row.Field<object>("WriteOffAmt"),
                        WriteOffStatus = row.Field<object>("WriteOffStatus"),
                        WriteOffComment = row.Field<object>("WriteOffComment"),
                        DenialCode = row.Field<object>("DenialCode"),
                        DenialComment = row.Field<object>("DenialComment"),
                        DenialCategory = row.Field<object>("DenialCategory"),
                        DenialType = row.Field<object>("DenialType"),
                        InitialDenialCode = row.Field<object>("InitialDenialCode"),
                        InitialDenialComment = row.Field<object>("InitialDenialComment"),
                        InitialDenialCategory = row.Field<object>("InitialDenialCategory"),
                        InitialDenialType = row.Field<object>("InitialDenialType"),
                        ResubmissionCount = row.Field<object>("ResubmissionCount"),
                        RemittanceCount = row.Field<object>("RemittanceCount"),
                        RemittanceComment = row.Field<object>("RemittanceComment"),
                        ResubmissionComment = row.Field<object>("ResubmissionComment"),
                        ClaimYear = row.Field<object>("ClaimYear"),
                        ClaimMonth = row.Field<object>("ClaimMonth"),
                        AllSubmissionFiles = row.Field<object>("AllSubmissionFiles"),
                        SubmissionAllTransactionIds = row.Field<object>("SubmissionAllTransactionIds"),
                        LastSubmissionFile = row.Field<object>("LastSubmissionFile"),
                        LastSubmissionTransactionId = row.Field<object>("LastSubmissionTransactionId"),
                        LastRemittanceFile = row.Field<object>("LastRemittanceFile"),
                        LastRemittanceTransctionID = row.Field<object>("LastRemittanceTransctionID"),
                        ObservationValue = row.Field<object>("ObservationValue"),
                        OfflineAuthorizationValue = row.Field<object>("OfflineAuthorizationValue"),
                        SettledAmt = row.Field<object>("SettledAmt"),
                        ReceiptStatus = row.Field<object>("ReceiptStatus"),
                        InitialDateSettlement = row.Field<object>("InitialDateSettlement"),
                        ClaimStatus = row.Field<object>("ClaimStatus"),
                        PaymentStatus = row.Field<object>("PaymentStatus")



                    }).ToList();
                    vReport.ReportData = lstt.ToList();
                }
                else
                {
                    List<RptClaimDetailWithActivityData> lstdata = new List<RptClaimDetailWithActivityData>();
                    vReport.ReportData = lstdata.ToList();
                }


                string strSQL = "SELECT ColTitle, ColName, ColToolTip, ColType, ColIsVisible, ColIsGroup, ColIsSummary FROM " +
                                "TB_REPORT_COLUMNS WHERE ReportID = " + ADO.SQLString(vReport.ReportID) + " ORDER BY ID";

                DataTable tblCols = ADO.GetDataTable(strSQL);

                IList<ReportColumns> lstcols = tblCols.AsEnumerable().Select(row => new ReportColumns
                {
                    Title = row.Field<string>("ColTitle"),
                    Name = row.Field<string>("ColName"),
                    ToolTip = row.Field<string>("ColToolTip"),
                    Type = row.Field<string>("ColType"),
                    Visibility = row.Field<bool>("ColIsVisible"),
                    Summary = row.Field<bool>("ColIsSummary"),
                    Group = row.Field<bool>("ColIsGroup"),

                }).ToList();


                vReport.ReportColumns = lstcols.ToList();                    
                vReport.flag = "1";
                vReport.message = "Success";               
                vReport.PersonalReports = GetPeronalReports(vInput.userid, vReport.ReportID);

                LstAdvFiler.Add(new ReportAdvanceFilterColumns { dataField = "ClaimNumber", caption = "Claim Number", values = "" });
                LstAdvFiler.Add(new ReportAdvanceFilterColumns { dataField = "ReceiverID", caption = "Receiver ID", values = "" });
                LstAdvFiler.Add(new ReportAdvanceFilterColumns { dataField = "PayerID", caption = "Payer ID", values = "" });
                LstAdvFiler.Add(new ReportAdvanceFilterColumns { dataField = "Clinician", caption = "Clinician", values = "" });
                LstAdvFiler.Add(new ReportAdvanceFilterColumns { dataField = "OrderingClinician", caption = "Ordering Clinician", values = "" });

                vReport.AdvanceFilter = LstAdvFiler;


            }
            catch (Exception ex)
            {
                vReport.flag = "0";
                vReport.message = ex.Message;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();

            }
            return vReport;
        }
        public RptClaimDetailsOutput GetClaimDetailReport(ReportParameters vInput)
        {
            RptClaimDetailsOutput vReport = new RptClaimDetailsOutput();
            SqlConnection conn = new SqlConnection();
            try
            {
                conn = ADO.GetConnection();

                List<RptClaimDetailWithActivityData> ReportData = new List<RptClaimDetailWithActivityData>();
                List<ReportAdvanceFilterColumns> LstAdvFiler = new List<ReportAdvanceFilterColumns>();

                // First Stored Procedure
                DataTable tbl = new DataTable();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_RPT_BI_CLAIM_DEAIL";
                cmd.Parameters.AddWithValue("@UserID", vInput.userid);
                cmd.Parameters.AddWithValue("@SearchOn", vInput.SearchOn);
                cmd.Parameters.AddWithValue("@DateFrom", Convert.ToDateTime(vInput.DateFrom));
                cmd.Parameters.AddWithValue("@DateTo", Convert.ToDateTime(vInput.DateTo));
                cmd.Parameters.AddWithValue("@EncounterType", vInput.EncounterType == null ? "" : vInput.EncounterType); ;
                cmd.Parameters.AddWithValue("@FacilityID", vInput.Facility == null ? "" : vInput.Facility);
                cmd.Parameters.AddWithValue("@ReceiverID", vInput.ReceiverID == null ? "" : vInput.ReceiverID.ToString());
                cmd.Parameters.AddWithValue("@PayerID", vInput.PayerID == null ? "" : vInput.PayerID.ToString());


                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tbl);

                vReport.ReportID = "claim-detail-report";
                if (tbl.Rows.Count > 0)
                {


                    IList<RptClaimDetailData> lstt = tbl.AsEnumerable().Select(row => new RptClaimDetailData
                    {

                        FacilityGroupID = row.Field<object>("FacilityGroupID"),
                        HealthAuthority = row.Field<object>("HealthAuthority"),
                        FacilityID = row.Field<object>("FacilityID"),
                        FacilityName = row.Field<object>("FacilityName"),
                        ClaimNumber = row.Field<object>("ClaimNumber"),
                        ReceiverID = row.Field<object>("ReceiverID"),
                        ReceiverName = row.Field<object>("ReceiverName"),
                        PayerID = row.Field<object>("PayerID"),
                        PayerName = row.Field<object>("PayerName"),
                        PatientID = row.Field<object>("PatientID"),
                        EncounterStartDate = row.Field<object>("EncounterStartDate"),
                        EncounterEndDate = row.Field<object>("EncounterEndDate"),
                        EncounterType = row.Field<object>("EncounterType"),
                        StartType = row.Field<object>("StartType"),
                        EndType = row.Field<object>("EndType"),
                        MemberID = row.Field<object>("MemberID"),
                        EmiratesIDNumber = row.Field<object>("EmiratesIDNumber"),
                        IDPayer = row.Field<object>("IDPayer"),
                        SubmissionDate = row.Field<object>("SubmissionDate"),
                        OrderingClinician = row.Field<object>("OrderingClinician"),
                        OrderingClinicianName = row.Field<object>("OrderingClinicianName"),
                        Clinician = row.Field<object>("Clinician"),
                        ClinicianName = row.Field<object>("ClinicianName"),
                        GrossAmt = row.Field<object>("GrossAmt"),
                        NetAmt = row.Field<object>("NetAmt"),
                        InitialNetAmt = row.Field<object>("InitialNetAmt"),
                        ClaimPatientShareAmt = row.Field<object>("ClaimPatientShareAmt"),
                        PaymentAmt = row.Field<object>("PaymentAmt"),
                        RejectedAmt = row.Field<object>("RejectedAmt"),
                        InitialRejectedAmt = row.Field<object>("InitialRejectedAmt"),
                        WriteOffAmt = row.Field<object>("WriteOffAmt"),
                        RemittanceStatus = row.Field<object>("RemittanceStatus"),
                        ClaimStatus = row.Field<object>("ClaimStatus"),
                        PaymentReference = row.Field<object>("PaymentReference"),
                        ResubmissionType = row.Field<object>("ResubmissionType"),
                        LastRemittanceDate = row.Field<object>("LastRemittanceDate"),
                        LastSubmissionDate = row.Field<object>("LastSubmissionDate"),
                        LastSubmissionFile = row.Field<object>("LastSubmissionFile"),
                        LastSubmissionTransactionId = row.Field<object>("LastSubmissionTransactionId"),
                        AllSubmissionFiles = row.Field<object>("AllSubmissionFiles"),
                        SubmissionAllTransactionIds = row.Field<object>("SubmissionAllTransactionIds"),
                        LastRemittanceFile = row.Field<object>("LastRemittanceFile"),
                        LastRemittanceTransactionId = row.Field<object>("LastRemittanceTransactionId"),
                        AllRemittanceFiles = row.Field<object>("AllRemittanceFiles"),
                        RemittanceAllTransactionIds = row.Field<object>("RemittanceAllTransactionIds"),
                        RemittanceComment = row.Field<object>("RemittanceComment"),
                        ResubmissionComment = row.Field<object>("ResubmissionComment"),
                        ResubmissionCount = row.Field<object>("ResubmissionCount"),
                        SettledAmt = row.Field<object>("SettledAmt"),
                        ReceiptStatus = row.Field<object>("ReceiptStatus"),
                        InitialDateSettlement = row.Field<object>("InitialDateSettlement"),
                        PaymentStatus = row.Field<object>("PaymentStatus")

                    }).ToList();
                    vReport.ReportData = lstt.ToList();
                }
                else
                {
                    List <RptClaimDetailData> LstData = new List<RptClaimDetailData>();
                    vReport.ReportData = LstData.ToList();
                }

                string strSQL = "SELECT ColTitle, ColName, ColToolTip, ColType, ColIsVisible, ColIsGroup, ColIsSummary FROM " +
                                "TB_REPORT_COLUMNS WHERE ReportID = " + ADO.SQLString(vReport.ReportID) + " ORDER BY ID";

                DataTable tblCols = ADO.GetDataTable(strSQL);

                IList<ReportColumns> lstcols = tblCols.AsEnumerable().Select(row => new ReportColumns
                {
                    Title = row.Field<string>("ColTitle"),
                    Name = row.Field<string>("ColName"),
                    ToolTip = row.Field<string>("ColToolTip"),
                    Type = row.Field<string>("ColType"),
                    Visibility = row.Field<bool>("ColIsVisible"),
                    Summary = row.Field<bool>("ColIsSummary"),
                    Group = row.Field<bool>("ColIsGroup"),

                }).ToList();



                
                

                vReport.PersonalReports = GetPeronalReports(vInput.userid, vReport.ReportID);

                LstAdvFiler.Add(new ReportAdvanceFilterColumns { dataField = "ClaimNumber", caption = "Claim Number", values = "" });
                LstAdvFiler.Add(new ReportAdvanceFilterColumns { dataField = "ReceiverID", caption = "Receiver ID", values = "" });
                LstAdvFiler.Add(new ReportAdvanceFilterColumns { dataField = "PayerID", caption = "Payer ID", values = "" });
                LstAdvFiler.Add(new ReportAdvanceFilterColumns { dataField = "Clinician", caption = "Clinician", values = "" });
                LstAdvFiler.Add(new ReportAdvanceFilterColumns { dataField = "OrderingClinician", caption = "Ordering Clinician", values = "" });

                vReport.AdvanceFilter = LstAdvFiler;
                vReport.ReportColumns = lstcols.ToList();
                
                vReport.flag = "1";
                vReport.message = "Success";

            }
            catch (Exception ex)
            {
                vReport.flag = "0";
                vReport.message = ex.Message;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();

            }
            return vReport;
        }
        public RptClaimDetail GetClaimDetails(string ClaimNumber, string FacilityID)
        {
            SqlConnection conn = new SqlConnection();
            RptClaimDetail vReport = new RptClaimDetail();

            List<RptClaimDetailSummary> LstSummary = new List<RptClaimDetailSummary>();



            DataSet ds = new DataSet();
            try
            {
                conn = ADO.GetConnection();
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_RPT_CLAIM_DETAILS";
                cmd.Parameters.AddWithValue("@FacilityID", FacilityID);
                cmd.Parameters.AddWithValue("@ClaimNumber", ClaimNumber);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                if (ds.Tables.Count > 0)
                {


                    DataTable tblSummary = ds.Tables[0];
                    if (tblSummary.Rows.Count > 0)
                    {
                        IList<RptClaimDetailSummary> Lst = tblSummary.AsEnumerable().Select(row => new RptClaimDetailSummary
                        {
                            ClaimNumber = row.Field<string>("ClaimNumber"),
                            EmiratesIDNumber = row.Field<string>("EmiratesIDNumber"),
                            ReceiverID = row.Field<string>("ReceiverID"),
                            PayerID = row.Field<string>("PayerID"),
                            IDPayer = row.Field<string>("IDPayer"),
                            MemberID = row.Field<string>("MemberID"),
                            ClaimCount = row.Field<object>("ClaimCount"),
                            ClaimAmount = row.Field<object>("ClaimAmount"),
                            RemittanceCount = row.Field<object>("RemittanceCount"),
                            RemittanceAmount = row.Field<object>("RemittanceAmount"),

                        }).ToList();

                        vReport.Summary = Lst.ToList();
                    }


                    DataTable tblTransaction = ds.Tables[1];
                    DataTable tblActivity = ds.Tables[2];
                    DataTable tblDiagnosis = ds.Tables[3];


                    if (tblTransaction.Rows.Count > 0)
                    {
                        IList<RptClaimDetailTransaction> Lst = tblTransaction.AsEnumerable().Select(row => new RptClaimDetailTransaction
                        {
                            ClaimNumber = row.Field<string>("ClaimNumber"),
                            ClaimRemittanceHeaderUID = row.Field<object>("ClaimRemittanceHeaderUID"),
                            ClaimRemittanceUID = row.Field<object>("ClaimRemittanceUID"),
                            SerialNumber = row.Field<object>("SerialNumber"),
                            TransactionType = row.Field<object>("TransactionType"),
                            TransactionDate = row.Field<object>("TransactionDate"),
                            GrossAmt = row.Field<object>("GrossAmt"),
                            PatientShareAmt = row.Field<object>("PatientShareAmt"),
                            NetAmt = row.Field<object>("NetAmt"),
                            VATAmt = row.Field<object>("VATAmt"),
                            ListAmt = row.Field<object>("ListAmt"),
                            PaymentAmt = row.Field<object>("PaymentAmt"),
                            RejectedAmt = row.Field<object>("RejectedAmt"),
                            PaymentReference = row.Field<object>("PaymentReference"),
                            DateSettlement = row.Field<object>("DateSettlement"),
                            Comments = row.Field<object>("Comments"),
                            XMLFileName = row.Field<object>("XMLFileName"),
                            DenialCode = row.Field<object>("DenialCode"),
                            IsActive = row.Field<object>("IsActive"),
                            HasAttachment = row.Field<object>("HasAttachment"),
                            ClaimType = row.Field<object>("ClaimType"),
                            DispensedID = row.Field<object>("DispensedID"),
                            ReferenceNumber = row.Field<object>("ReferenceNumber"),
                            DateOfBirth = row.Field<object>("DateOfBirth"),
                            Gender = row.Field<object>("Gender"),
                            ReceiverID = row.Field<object>("ReceiverID"),
                            PayerID = row.Field<object>("PayerID")
                        }).ToList();

                        vReport.Transaction = Lst.ToList();
                    }

                    if (tblActivity.Rows.Count > 0)
                    {
                        IList<RptClaimDetailActivity> Lst = tblActivity.AsEnumerable().Select(row => new RptClaimDetailActivity
                        {
                            ClaimNumber = row.Field<string>("ClaimNumber"),
                            ClaimRemittanceHeaderUID = row.Field<object>("ClaimRemittanceHeaderUID"),
                            ClaimRemittanceUID = row.Field<object>("ClaimRemittanceUID"),
                            SerialNumber = row.Field<object>("SerialNumber"),
                            ClaimActivityNumber = row.Field<object>("ClaimActivityNumber"),
                            StartDate = row.Field<object>("StartDate"),
                            CPTType = row.Field<object>("CPTType"),
                            CPTCode = row.Field<object>("CPTCode"),
                            Quantity = row.Field<object>("Quantity"),
                            NetAmt = row.Field<object>("NetAmt"),
                            ListAmt = row.Field<object>("ListAmt"),
                            GrossAmt = row.Field<object>("GrossAmt"),
                            PatientShareAmt = row.Field<object>("PatientShareAmt"),
                            PaymentAmt = row.Field<object>("PaymentAmt"),
                            RejectedAmt = row.Field<object>("RejectedAmt"),
                            VATAmt = row.Field<object>("VATAmt"),
                            VATPercent = row.Field<object>("VATPercent"),
                            OrderingClinician = row.Field<object>("OrderingClinician"),
                            Clinician = row.Field<object>("Clinician"),
                            PriorAuthorizationID = row.Field<object>("PriorAuthorizationID"),
                            DenialCode = row.Field<object>("DenialCode"),
                            DenialText = row.Field<object>("DenialText"),
                            HasAttachment = row.Field<object>("HasAttachment"),
                            Location = row.Field<object>("Location"),
                            PatientShare = row.Field<object>("PatientShare"),
                            Duration = row.Field<object>("Duration"),
                            DispensedActivityID = row.Field<object>("DispensedActivityID"),
                            ActivityPenalty = row.Field<object>("ActivityPenalty"),
                            ActivityComments = row.Field<object>("ActivityComments"),
                            TransactionType = row.Field<object>("TransactionType"),
                            IsWriteOff = row.Field<object>("IsWriteOff"),
                            WriteOffAmt = row.Field<object>("WriteOffAmt"),
                            WriteOffComments = row.Field<object>("WriteOffComments")
                        }).ToList();
                        vReport.Activity = Lst.ToList();
                    }

                    if (tblDiagnosis.Rows.Count > 0)
                    {
                        IList<RptClaimDetailDiagnosis> Lst = tblDiagnosis.AsEnumerable().Select(row => new RptClaimDetailDiagnosis
                        {
                            ClaimNumber = row.Field<string>("ClaimNumber"),
                            ClaimRemittanceHeaderUID = row.Field<object>("ClaimRemittanceHeaderUID"),
                            ClaimRemittanceUID = row.Field<object>("ClaimRemittanceUID"),
                            SerialNumber = row.Field<object>("SerialNumber"),
                            ICDCode = row.Field<object>("ICDCode"),
                            ICDName = row.Field<object>("ICDName"),
                            ICDType = row.Field<object>("ICDType")

                        }).ToList();
                        vReport.Diagnosis = Lst.ToList();
                    }


                    ////List<RptClaimDetailTransaction> LstTransaction = new List<RptClaimDetailTransaction>();
                    ////if (tblTransaction.Rows.Count > 0)
                    ////{
                    ////    foreach (DataRow row in tblTransaction.Rows)
                    ////    {
                    ////        RptClaimDetailTransaction objTran = new RptClaimDetailTransaction();

                    ////        objTran.ClaimNumber = row.Field<string>("ClaimNumber");
                    ////        objTran.ClaimRemittanceHeaderUID = row.Field<object>("ClaimRemittanceHeaderUID");
                    ////        objTran.ClaimRemittanceUID = row.Field<object>("ClaimRemittanceUID");
                    ////        objTran.SerialNumber = row.Field<object>("SerialNumber");
                    ////        objTran.TransactionType = row.Field<object>("TransactionType");
                    ////        objTran.TransactionDate = row.Field<object>("TransactionDate");
                    ////        objTran.GrossAmt = row.Field<object>("GrossAmt");
                    ////        objTran.PatientShareAmt = row.Field<object>("PatientShareAmt");
                    ////        objTran.NetAmt = row.Field<object>("NetAmt");
                    ////        objTran.VATAmt = row.Field<object>("VATAmt");
                    ////        objTran.ListAmt = row.Field<object>("ListAmt");
                    ////        objTran.PaymentAmt = row.Field<object>("PaymentAmt");
                    ////        objTran.RejectedAmt = row.Field<object>("RejectedAmt");
                    ////        objTran.PaymentReference = row.Field<object>("PaymentReference");
                    ////        objTran.DateSettlement = row.Field<object>("DateSettlement");
                    ////        objTran.Comments = row.Field<object>("Comments");
                    ////        objTran.XMLFileName = row.Field<object>("XMLFileName");
                    ////        objTran.DenialCode = row.Field<object>("DenialCode");
                    ////        objTran.IsActive = row.Field<object>("IsActive");
                    ////        objTran.HasAttachment = row.Field<object>("HasAttachment");
                    ////        objTran.ClaimType = row.Field<object>("ClaimType");
                    ////        objTran.DispensedID = row.Field<object>("DispensedID");
                    ////        objTran.ReferenceNumber = row.Field<object>("ReferenceNumber");
                    ////        objTran.DateOfBirth = row.Field<object>("DateOfBirth");
                    ////        objTran.Gender = row.Field<object>("Gender");
                    ////        objTran.ReceiverID = row.Field<object>("ReceiverID");
                    ////        objTran.PayerID = row.Field<object>("PayerID");

                    ////        List<RptClaimDetailActivity> LstActivity = new List<RptClaimDetailActivity>();
                    ////        DataRow[] drActivity = tblActive.Select("ClaimRemittanceHeaderUID = " + objTran.ClaimRemittanceHeaderUID.ToString());
                    ////        foreach (DataRow row1 in drActivity)
                    ////        {
                    ////            RptClaimDetailActivity objActivity = new RptClaimDetailActivity();

                    ////            objActivity.ClaimNumber = row1.Field<string>("ClaimNumber");
                    ////            objActivity.ClaimRemittanceHeaderUID = row1.Field<object>("ClaimRemittanceHeaderUID");
                    ////            objActivity.ClaimRemittanceUID = row1.Field<object>("ClaimRemittanceUID");
                    ////            objActivity.SerialNumber = row1.Field<object>("SerialNumber");
                    ////            objActivity.ClaimActivityNumber = row1.Field<object>("ClaimActivityNumber");
                    ////            objActivity.StartDate = row1.Field<object>("StartDate");
                    ////            objActivity.CPTType = row1.Field<object>("CPTType");
                    ////            objActivity.CPTCode = row1.Field<object>("CPTCode");
                    ////            objActivity.Quantity = row1.Field<object>("Quantity");
                    ////            objActivity.NetAmt = row1.Field<object>("NetAmt");
                    ////            objActivity.ListAmt = row1.Field<object>("ListAmt");
                    ////            objActivity.GrossAmt = row1.Field<object>("GrossAmt");
                    ////            objActivity.PatientShareAmt = row1.Field<object>("PatientShareAmt");
                    ////            objActivity.PaymentAmt = row1.Field<object>("PaymentAmt");
                    ////            objActivity.RejectedAmt = row1.Field<object>("RejectedAmt");
                    ////            objActivity.VATAmt = row1.Field<object>("VATAmt");
                    ////            objActivity.VATPercent = row1.Field<object>("VATPercent");
                    ////            objActivity.OrderingClinician = row1.Field<object>("OrderingClinician");
                    ////            objActivity.Clinician = row1.Field<object>("Clinician");
                    ////            objActivity.PriorAuthorizationID = row1.Field<object>("PriorAuthorizationID");
                    ////            objActivity.DenialCode = row1.Field<object>("DenialCode");
                    ////            objActivity.DenialText = row1.Field<object>("DenialText");
                    ////            objActivity.HasAttachment = row1.Field<object>("HasAttachment");
                    ////            objActivity.Location = row1.Field<object>("Location");
                    ////            objActivity.PatientShare = row1.Field<object>("PatientShare");
                    ////            objActivity.Duration = row1.Field<object>("Duration");
                    ////            objActivity.DispensedActivityID = row1.Field<object>("DispensedActivityID");
                    ////            objActivity.ActivityPenalty = row1.Field<object>("ActivityPenalty");
                    ////            objActivity.ActivityComments = row1.Field<object>("ActivityComments");
                    ////            objActivity.TransactionType = row1.Field<object>("TransactionType");
                    ////            objActivity.IsWriteOff = row1.Field<object>("IsWriteOff");
                    ////            objActivity.WriteOffAmt = row1.Field<object>("WriteOffAmt");
                    ////            objActivity.WriteOffComments = row1.Field<object>("WriteOffComments");


                    ////            List<RptClaimDetailDiagnosis> LstDiag = new List<RptClaimDetailDiagnosis>();
                    ////            DataRow[] drDiagnosis = tblDiagnosis.Select("ClaimRemittanceHeaderUID = " + objActivity.ClaimRemittanceHeaderUID.ToString() + " AND SerialNumber = " + objActivity.SerialNumber.ToString() );
                    ////            foreach(DataRow row2 in drDiagnosis)
                    ////            {
                    ////                RptClaimDetailDiagnosis objDiag = new RptClaimDetailDiagnosis();

                    ////                objDiag.ClaimNumber = row2.Field<string>("ClaimNumber");
                    ////                objDiag.ClaimRemittanceHeaderUID = row2.Field<object>("ClaimRemittanceHeaderUID");
                    ////                objDiag.ClaimRemittanceUID = row2.Field<object>("ClaimRemittanceUID");
                    ////                objDiag.SerialNumber = row2.Field<object>("SerialNumber");
                    ////                objDiag.ICDCode = row2.Field<object>("ICDCode");
                    ////                objDiag.ICDName = row2.Field<object>("ICDName");
                    ////                objDiag.ICDType = row2.Field<object>("ICDType");

                    ////                LstDiag.Add(objDiag);
                    ////            }

                    ////            objActivity.Diagnosis = LstDiag.ToList();

                    ////            LstActivity.Add(objActivity);

                    ////        }

                    ////        objTran.Activities = LstActivity.ToList();
                    ////        LstTransaction.Add(objTran);
                    ////    }
                    ////    vReport.Transaction = LstTransaction.ToList();







                    string strSQL = "SELECT ColTitle, ColName, ColToolTip, ColType, ColIsVisible, ColIsGroup, ColIsSummary FROM " +
                    "TB_REPORT_COLUMNS WHERE ReportID = 'claim-detail-activity-claim' ORDER BY ID";

                    DataTable tblCols = ADO.GetDataTable(strSQL);

                    IList<ReportColumns> lstcols = tblCols.AsEnumerable().Select(row => new ReportColumns
                    {
                        Title = row.Field<string>("ColTitle"),
                        Name = row.Field<string>("ColName"),
                        ToolTip = row.Field<string>("ColToolTip"),
                        Type = row.Field<string>("ColType"),
                        Visibility = row.Field<bool>("ColIsVisible"),
                        Summary = row.Field<bool>("ColIsSummary"),
                        Group = row.Field<bool>("ColIsGroup"),

                    }).ToList();

                    string strSQL1 = "SELECT ColTitle, ColName, ColToolTip, ColType, ColIsVisible, ColIsGroup, ColIsSummary FROM " +
                        "TB_REPORT_COLUMNS WHERE ReportID = 'claim-detail-activity-transaction' ORDER BY ID";

                    DataTable tblCols1 = ADO.GetDataTable(strSQL1);

                    IList<ReportColumns> lstcols1 = tblCols1.AsEnumerable().Select(row => new ReportColumns
                    {
                        Title = row.Field<string>("ColTitle"),
                        Name = row.Field<string>("ColName"),
                        ToolTip = row.Field<string>("ColToolTip"),
                        Type = row.Field<string>("ColType"),
                        Visibility = row.Field<bool>("ColIsVisible"),
                        Summary = row.Field<bool>("ColIsSummary"),
                        Group = row.Field<bool>("ColIsGroup"),

                    }).ToList();

                    string strSQL2 = "SELECT ColTitle, ColName, ColToolTip, ColType, ColIsVisible, ColIsGroup, ColIsSummary FROM " +
                    "TB_REPORT_COLUMNS WHERE ReportID = 'claim-detail-activity-activities' ORDER BY ID";

                    DataTable tblCols2 = ADO.GetDataTable(strSQL2);

                    IList<ReportColumns> lstcols2 = tblCols2.AsEnumerable().Select(row => new ReportColumns
                    {
                        Title = row.Field<string>("ColTitle"),
                        Name = row.Field<string>("ColName"),
                        ToolTip = row.Field<string>("ColToolTip"),
                        Type = row.Field<string>("ColType"),
                        Visibility = row.Field<bool>("ColIsVisible"),
                        Summary = row.Field<bool>("ColIsSummary"),
                        Group = row.Field<bool>("ColIsGroup"),

                    }).ToList();

                    string strSQL3 = "SELECT ColTitle, ColName, ColToolTip, ColType, ColIsVisible, ColIsGroup, ColIsSummary FROM " +
                    "TB_REPORT_COLUMNS WHERE ReportID = 'claim-detail-activity-diagnosis' ORDER BY ID";

                    DataTable tblCols3 = ADO.GetDataTable(strSQL3);

                    IList<ReportColumns> lstcols3 = tblCols3.AsEnumerable().Select(row => new ReportColumns
                    {
                        Title = row.Field<string>("ColTitle"),
                        Name = row.Field<string>("ColName"),
                        ToolTip = row.Field<string>("ColToolTip"),
                        Type = row.Field<string>("ColType"),
                        Visibility = row.Field<bool>("ColIsVisible"),
                        Summary = row.Field<bool>("ColIsSummary"),
                        Group = row.Field<bool>("ColIsGroup"),

                    }).ToList();

                    vReport.flag = "1";
                    vReport.message = "success";
                    vReport.ReportID = "claim-detail";
                    vReport.ClaimColumns = lstcols.ToList();
                    vReport.TransactionColumns = lstcols1.ToList();
                    vReport.ActivityColumns = lstcols2.ToList();
                    vReport.DiagnosisColumns = lstcols3.ToList();
                    
                }
            }
            catch (Exception ex)
            {
                vReport.flag = "0";
                vReport.message = ex.Message;
                vReport.ReportID = "claim-detail";
            }
            return vReport;
        }
        public List<PersonalReport> GetPeronalReports(string UserID, string ReportID)
        {
            List<PersonalReport> LstPeroanlReport = new List<PersonalReport>();

            SqlConnection conn = new SqlConnection();
            try
            {
                conn = ADO.GetConnection();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_GET_USER_REPORT";
                cmd.Parameters.AddWithValue("@USER_ID", UserID);
                cmd.Parameters.AddWithValue("@REPORT_ID", ReportID);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    //Header Info of user report
                    PersonalReport prpt = new PersonalReport();
                    prpt.id = dr["ID"].ToString(); ;
                    prpt.name = dr["USER_REPORT_NAME"].ToString();

                    //Parameters for the selcted user report
                    ReportParameters rptparam = new ReportParameters();
                    DataRow drParam = ds.Tables[1].Select("USER_REPORT_ID = " + prpt.id).FirstOrDefault();
                    if (drParam != null)
                    {
                        rptparam.SearchOn = fToString(drParam["SEARCH_ON"]);
                        rptparam.DateFrom = fToString(drParam["START_DATE"]);
                        rptparam.DateTo = fToString(drParam["END_DATE"]);
                        rptparam.EncounterType = fToString(drParam["ENCOUNTER_TYPE"]);
                        rptparam.Facility = fToString(drParam["FACILITY_ID"]);
                        //rptparam.SenderID = fToString(drParam["SENDER_ID"]);
                        rptparam.ReceiverID = fToString(drParam["RECEIVER_ID"]);
                        rptparam.Clinician = fToString(drParam["CLINICIAN"]);
                    }
                    prpt.Parameters = rptparam;

                    //COlumns for the selected user reprot
                    List<ReportColumns> LstCols = new List<ReportColumns>();
                    DataRow[] drColumns = ds.Tables[2].Select("USER_REPORT_ID = " + prpt.id);
                    foreach (DataRow drCols in drColumns)
                    {
                        ReportColumns col = new ReportColumns();
                        col.Visibility = Convert.ToBoolean(drCols["IS_VISIBLE"]);
                        col.Title = fToString(drCols["COLOUMN_TITLE"]);
                        col.Name = fToString(drCols["COLOUMN_NAME"]);
                        col.ToolTip = fToString(drCols["COLOUMN_TOOLTIP"]);
                        col.Type = fToString(drCols["COLOUMN_TYPE"]);
                        col.Summary = Convert.ToBoolean(drCols["CAN_SUMMARY"]);
                        col.Group = Convert.ToBoolean(drCols["CAN_GROUP"]);

                        LstCols.Add(col);
                    }

                    prpt.Columns = LstCols;

                    LstPeroanlReport.Add(prpt);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }

            return LstPeroanlReport;
        }
        public string fToDecimalString(object vInput)
        {
            try
            {
                return Convert.ToDecimal(vInput).ToString("0.00");
            }
            catch (Exception ex)
            {
                return "0.00";
            }
        }
        public string fToString(object vInput)
        {
            try
            {
                return Convert.ToString(vInput);
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        public string fToDateString(object vInput)
        {
            try
            {
                return Convert.ToDateTime(vInput).ToShortDateString();
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        public decimal fToDecimal(object vInput)
        {
            try
            {
                return Convert.ToDecimal(vInput);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public string fToBool(object vInput)
        {
            try
            {
                if (vInput != null)
                {
                    // Check if the value is 0 or 1 and return the appropriate string
                    return vInput.ToString() == "0" ? "false" : vInput.ToString() == "1" ? "true" : Convert.ToString(vInput);
                }
                return "";
            }
            catch (Exception ex)
            {
                return "";
            }
        }

    }
}