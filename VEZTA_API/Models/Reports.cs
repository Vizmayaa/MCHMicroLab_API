using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VEZTA.Models
{
    public class ReportParameters
    {
        public string userid { get; set; }
        public string SearchOn { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public string EncounterType { get; set; }
        public string Facility { get; set; }
        public string Payer { get; set; }
        public string PayerID { get; set; }
        public string ReceiverID { get; set; }
        public string Clinician { get; set; }
        public string OrderingClinician { get; set; }
        public string AsOnDate { get; set; }
        public string BasedOnInitialNetAmount { get; set; }
        public string ClaimNumber { get; set; }
        public string PatientID { get; set; }
        public string ResubmissionType { get; set; }
        public string DenialCode { get; set; }
        public string CptType { get; set; }
        public string MemberID { get; set; }
        public string ClaimStatus { get; set; }
        public string PaymentStatus { get; set; }
        public List<ReportAdvanceFilterColumns> AdvanceFilter { get; set; }
    }

    public class ReportColumns
    {
        public string Title { get; set; }
        public string Name { get; set; }
        public string ToolTip { get; set; }
        public string Type { get; set; }
        public bool Visibility { get; set; }
        public bool Group { get; set; }
        public bool Summary { get; set; }
    }

    public class ClaimDetailData
    {
        public string FacilityID { get; set; }
        public string FacilityName { get; set; }
        public string ClaimNumber { get; set; }
        public string ReceiverID { get; set; }
        public string ReceiverShortName { get; set; }
        public string ReceiverName { get; set; }
        public string PayerID { get; set; }
        public string PayerShortName { get; set; }
        public string PayerName { get; set; }
        public string Clinician { get; set; }
        public string ClinicianShortName { get; set; }
        public string ClinicianName { get; set; }
        public string EncounterType { get; set; }
        public object ClaimedCount { get; set; }
        public object ClaimedAmount { get; set; }
        public object RemittedCount { get; set; }
        public object RemittedAmount { get; set; }
        public object RejectedCount { get; set; }
        public object RejectedAmount { get; set; }
        public object PaidCount { get; set; }
        public object PaidAmount { get; set; }
        public object BalanceCount { get; set; }
        public object BalanceAmount { get; set; }
        public object LastResubmissionDate { get; set; }
        public object LastRemittanceDate { get; set; }
        public string RemittanceStatus { get; set; }
        public string CPTCode { get; set; }
        public string CPTName { get; set; }
        public string CPTGroup { get; set; }
        public object EncounterStartDate { get; set; }
        public object EncounterEndDate { get; set; }
    }
    public class RptClaimDetailWithActivityData
    {
        public object FacilityGroupID { get; set; }
        public object HealthAuthority { get; set; }
        public object FacilityID { get; set; }
        public object FacilityName { get; set; }
        public object ClaimNumber { get; set; }
        public object PatientID { get; set; }
        public object TransactionDate { get; set; }
        public object ActivityStartDate { get; set; }
        public object EncounterType { get; set; }
        public object EncounterStartDate { get; set; }
        public object EncounterEndDate { get; set; }
        public object ClaimActivityNumber { get; set; }
        public object CPTCode { get; set; }
        public object CPTCategory { get; set; }
        public object CPTName { get; set; }
        public object CPTType { get; set; }
        public object Quantity { get; set; }
        public object OrderingClinician { get; set; }
        public object OrderingClinicianName { get; set; }
        public object Clinician { get; set; }
        public object ClinicianName { get; set; }
        public object ReceiverID { get; set; }
        public object ReceiverName { get; set; }
        public object PayerID { get; set; }
        public object PayerName { get; set; }
        public object MemberID { get; set; }
        public object EmiratesIDNumber { get; set; }
        public object IDPayer { get; set; }
        public object PaymentReference { get; set; }
        public object PriorAuthorizationID { get; set; }
        public object NetAmt { get; set; }
        public object InitialNetAmt { get; set; }
        public object Diagnosis { get; set; }
        public object PrimaryDiagnosis { get; set; }
        public object PrimaryDiagnosisDescription { get; set; }
        public object LastResubmissionDate { get; set; }
        public object FirstRemittanceDate { get; set; }
        public object LastRemittanceDate { get; set; }
        public object RemittedAmt { get; set; }
        public object LastRemittedAmount { get; set; }
        public object InitialRejectedAmt { get; set; }
        public object RejectedAmt { get; set; }
        public object UnprocessedAmt { get; set; }
        public object RejectionPercentage { get; set; }
        public object WriteOffAmt { get; set; }

        public object WriteOffStatus { get; set; }
        public object WriteOffComment { get; set; }
        public object DenialCode { get; set; }
        public object DenialComment { get; set; }
        public object DenialCategory { get; set; }
        public object DenialType { get; set; }
        public object InitialDenialCode { get; set; }
        public object InitialDenialComment { get; set; }
        public object InitialDenialCategory { get; set; }
        public object InitialDenialType { get; set; }
        public object ResubmissionCount { get; set; }
        public object RemittanceCount { get; set; }
        public object RemittanceComment { get; set; }
        public object ResubmissionComment { get; set; }
        public object ClaimYear { get; set; }
        public object ClaimMonth { get; set; }
        public object AllSubmissionFiles { get; set; }
        public object SubmissionAllTransactionIds { get; set; }
        public object LastSubmissionFile { get; set; }
        public object LastSubmissionTransactionId { get; set; }
        public object LastRemittanceFile { get; set; }
        public object LastRemittanceTransctionID { get; set; }

        public object ObservationValue { get; set; }
        public object OfflineAuthorizationValue { get; set; }
        public object SettledAmt { get; set; }
        public object ReceiptStatus { get; set; }
        public object InitialDateSettlement { get; set; }
        public object ClaimStatus { get; set; }
        public object PaymentStatus { get; set; }

    }
    public class RptClaimDetailData
    {
        public object FacilityGroupID { get; set; }
        public object HealthAuthority { get; set; }
        public object FacilityID { get; set; }
        public object FacilityName { get; set; }
        public object ClaimNumber { get; set; }
        public object ReceiverID { get; set; }
        public object ReceiverName { get; set; }
        public object PayerID { get; set; }
        public object PayerName { get; set; }
        public object PatientID { get; set; }
        public object EncounterStartDate { get; set; }
        public object EncounterEndDate { get; set; }
        public object EncounterType { get; set; }
        public object StartType { get; set; }
        public object EndType { get; set; }
        public object MemberID { get; set; }
        public object EmiratesIDNumber { get; set; }
        public object IDPayer { get; set; }
        public object SubmissionDate { get; set; }
        public object OrderingClinician { get; set; }
        public object OrderingClinicianName { get; set; }
        public object Clinician { get; set; }
        public object ClinicianName { get; set; }
        public object GrossAmt { get; set; }
        public object NetAmt { get; set; }
        public object InitialNetAmt { get; set; }
        public object ClaimPatientShareAmt { get; set; }
        public object PaymentAmt { get; set; }
        public object RejectedAmt { get; set; }
        public object InitialRejectedAmt { get; set; }
        public object WriteOffAmt { get; set; }
        public object RemittanceStatus { get; set; }
        public object ClaimStatus { get; set; }
        public object PaymentReference { get; set; }
        public object ResubmissionType { get; set; }
        public object LastRemittanceDate { get; set; }
        public object LastSubmissionDate { get; set; }
        public object LastSubmissionFile { get; set; }
        public object LastSubmissionTransactionId { get; set; }
        public object AllSubmissionFiles { get; set; }
        public object SubmissionAllTransactionIds { get; set; }
        public object LastRemittanceFile { get; set; }
        public object LastRemittanceTransactionId { get; set; }
        public object AllRemittanceFiles { get; set; }
        public object RemittanceAllTransactionIds { get; set; }
        public object RemittanceComment { get; set; }

        public object ResubmissionComment { get; set; }
        public object ResubmissionCount { get; set; }
        public object SettledAmt { get; set; }
        public object ReceiptStatus { get; set; }
        public object InitialDateSettlement { get; set; }
        public object PaymentStatus { get; set; }
        

    }
    public class ClaimDetailReport
    {
        public string flag { get; set; }
        public string message { get; set; }
        public string ReportID { get; set; }
        public List<ReportColumns> ReportColumns { get; set; }
        public List<ClaimDetailData> ReportData { get; set; }
        public List<PersonalReport> PersonalReports { get; set; }
        public List<ReportAdvanceFilterColumns> AdvanceFilter { get; set; }

    }
    public class RptClaimDetailsOutput
    {
        public string flag { get; set; }
        public string message { get; set; }
        public string ReportID { get; set; }
        public List<ReportColumns> ReportColumns { get; set; }
        public List<RptClaimDetailData> ReportData { get; set; }
        public List<PersonalReport> PersonalReports { get; set; }
        public List<ReportAdvanceFilterColumns> AdvanceFilter { get; set; }

    }
    public class RptClaimDetailWithActivityOutput
    {
        public string flag { get; set; }
        public string message { get; set; }
        public string ReportID { get; set; }
        public List<ReportColumns> ReportColumns { get; set; }
        public List<RptClaimDetailWithActivityData> ReportData { get; set; }
        public List<PersonalReport> PersonalReports { get; set; }
        public List<ReportAdvanceFilterColumns> AdvanceFilter { get; set; }

    }
    public class PersonalReport
    {
        public string id { get; set; }
        public string name { get; set; }
        public List<ReportColumns> Columns { get; set; }
        public ReportParameters Parameters { get; set; }

    }
    public class ReportParameterData
    {
        public string ID { get; set; }
        public string Name { get; set; }

    }
    public class ReportAdvanceFilterColumns
    {
        [JsonIgnore]
        public int ID { get; set; }
        public string dataField { get; set; }
        public string caption { get; set; }

        [JsonIgnore]
        public string values { get; set; }

    }

    public class FacilityData
    {
        public string ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string GroupID { get; set; }
        public string GroupName { get; set; }
    }
    public class ReportParameterValues
    {
        public string flag { get; set; }
        public string message { get; set; }
        public List<FacilityData> facility { get; set; }

        public List<ReportParameterData> SearchOn { get; set; }
        public List<ReportParameterData> EncounterType { get; set; }
        public List<ReportParameterData> ReceiverID { get; set; }
        public List<ReportParameterData> PayerID { get; set; }
        public List<ReportParameterData> Payer { get; set; }
        public List<ReportParameterData> Clinician { get; set; }
        public List<ReportParameterData> OrderingClinician { get; set; }
        public List<ReportParameterData> ClaimStatus { get; set; }
        public List<ReportParameterData> ResubmissionType { get; set; }
        public List<ReportParameterData> PaymentStatus { get; set; }


        public List<ReportAdvanceFilterColumns> AdvanceFilter { get; set; }


    }
    public class ReportParameterInput
    {
        public string UserID { get; set; }

    }
    public class ClaimDetailInput
    {
        public string FacilityID { get; set; }
        public string ClaimNumber { get; set; }
    }
    public class RptClaimDetail
    {
        public string flag { get; set; }
        public string message { get; set; }
        public string ReportID { get; set; }
        public List<RptClaimDetailSummary> Summary { get; set; }
        public List<RptClaimDetailTransaction> Transaction { get; set; }
        public List<RptClaimDetailActivity> Activity { get; set; }
        public List<RptClaimDetailDiagnosis> Diagnosis { get; set; }

        public List<ReportColumns> ClaimColumns { get; set; }
        public List<ReportColumns> TransactionColumns { get; set; }
        public List<ReportColumns> ActivityColumns { get; set; }
        public List<ReportColumns> DiagnosisColumns { get; set; }

    }
    public class RptClaimDetailSummary
    {
        public string ClaimNumber { get; set; }
        public string EmiratesIDNumber { get; set; }
        public string ReceiverID { get; set; }
        public string PayerID { get; set; }
        public string IDPayer { get; set; }
        public string MemberID { get; set; }
        public object ClaimCount { get; set; }
        public object ClaimAmount { get; set; }
        public object RemittanceCount { get; set; }
        public object RemittanceAmount { get; set; }
    }
    public class RptClaimDetailTransaction
    {
        public string ClaimNumber { get; set; }
        public object ClaimRemittanceHeaderUID { get; set; }
        public object ClaimRemittanceUID { get; set; }
        public object SerialNumber { get; set; }
        public object TransactionType { get; set; }
        public object TransactionDate { get; set; }
        public object GrossAmt { get; set; }
        public object PatientShareAmt { get; set; }
        public object NetAmt { get; set; }
        public object VATAmt { get; set; }
        public object ListAmt { get; set; }
        public object PaymentAmt { get; set; }
        public object RejectedAmt { get; set; }
        public object PaymentReference { get; set; }
        public object DateSettlement { get; set; }
        public object Comments { get; set; }
        public object XMLFileName { get; set; }
        public object DenialCode { get; set; }
        public object IsActive { get; set; }
        public object HasAttachment { get; set; }
        public object ClaimType { get; set; }
        public object DispensedID { get; set; }
        public object ReferenceNumber { get; set; }
        public object DateOfBirth { get; set; }
        public object Gender { get; set; }
        public object ReceiverID { get; set; }
        public object PayerID { get; set; }

    }
    public class RptClaimDetailActivity
    {
        public string ClaimNumber { get; set; }
        public object ClaimRemittanceHeaderUID { get; set; }
        public object ClaimRemittanceUID { get; set; }
        public object SerialNumber { get; set; }
        public object ClaimActivityNumber { get; set; }
        public object StartDate { get; set; }
        public object CPTType { get; set; }
        public object CPTCode { get; set; }
        public object Quantity { get; set; }
        public object NetAmt { get; set; }
        public object ListAmt { get; set; }
        public object GrossAmt { get; set; }
        public object PatientShareAmt { get; set; }
        public object PaymentAmt { get; set; }
        public object RejectedAmt { get; set; }
        public object VATAmt { get; set; }
        public object VATPercent { get; set; }
        public object OrderingClinician { get; set; }
        public object Clinician { get; set; }
        public object PriorAuthorizationID { get; set; }
        public object DenialCode { get; set; }
        public object DenialText { get; set; }
        public object HasAttachment { get; set; }
        public object Location { get; set; }
        public object PatientShare { get; set; }
        public object Duration { get; set; }
        public object DispensedActivityID { get; set; }
        public object ActivityPenalty { get; set; }
        public object ActivityComments { get; set; }
        public object TransactionType { get; set; }
        public object IsWriteOff { get; set; }
        public object WriteOffAmt { get; set; }
        public object WriteOffComments { get; set; }


    }
    public class RptClaimDetailDiagnosis
    {
        public string ClaimNumber { get; set; }
        public object ClaimRemittanceHeaderUID { get; set; }
        public object ClaimRemittanceUID { get; set; }
        public object SerialNumber { get; set; }

        public object ICDCode { get; set; }
        public object ICDName { get; set; }
        public object ICDType { get; set; }

    }

}