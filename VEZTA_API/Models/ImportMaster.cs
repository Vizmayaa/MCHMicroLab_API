using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VEZTA.Models
{
    public class ImportMasterType
    {
        public int ID { get; set; }
        public string Master { get; set; }
    }
    public class ImportMasterColumn
    {
        public int ID { get; set; }
        public int MasterID { get; set; }
        public string ColumnName { get; set; }
        public string ColumnTitle { get; set; }
        public bool IsNumeric { get; set; }
        public bool IsMandatory { get; set; }
        public int MaxLength { get; set; }
        public string LOVTableName { get; set; }
        public string LOVColumnName { get; set; }
    }
    //public class ImportClinicianMaster
    //{
    //    public int ID { get; set; }
    //    public int MasterID { get; set; }
    //    public string ColumnName { get; set; }
    //    public string ColumnTitle { get; set; }
    //    public bool IsNumeric { get; set; }
    //    public bool IsMandatory { get; set; }
    //    public int MaxLength { get; set; }
    //    public string LOVTableName { get; set; }
    //    public string LOVColumnName { get; set; }
    //}
    //public class ImportDenialMaster
    //{
    //    public int ID { get; set; }
    //    public int MasterID { get; set; }
    //    public string ColumnName { get; set; }
    //    public string ColumnTitle { get; set; }
    //    public bool IsNumeric { get; set; }
    //    public bool IsMandatory { get; set; }
    //    public int MaxLength { get; set; }
    //    public string LOVTableName { get; set; }
    //    public string LOVColumnName { get; set; }
    //}
    //public class ImportInsuranceMaster
    //{
    //    public int ID { get; set; }
    //    public int MasterID { get; set; }
    //    public string ColumnName { get; set; }
    //    public string ColumnTitle { get; set; }
    //    public bool IsNumeric { get; set; }
    //    public bool IsMandatory { get; set; }
    //    public int MaxLength { get; set; }
    //    public string LOVTableName { get; set; }
    //    public string LOVColumnName { get; set; }
    //}
    //public class ImportCptMaster
    //{
    //    public int ID { get; set; }
    //    public int MasterID { get; set; }
    //    public string ColumnName { get; set; }
    //    public string ColumnTitle { get; set; }
    //    public bool IsNumeric { get; set; }
    //    public bool IsMandatory { get; set; }
    //    public int MaxLength { get; set; }
    //    public string LOVTableName { get; set; }
    //    public string LOVColumnName { get; set; }
    //}
    public class ImportMasterResponse
    {
        public int flag { get; set; }
        public string message { get; set; }
        public List<ImportLog> data { get; set; }

         
    }
    public class ImportMasterInput
    {
        public int ID { get; set; }
        public int SerialNo { get; set; }
        public int DocNo { get; set; }
        public int MasterID { get; set; }
        public int UserID { get; set; }
        public DateTime ImportTime { get; set; }
        public bool NewRecordOnly { get; set; }
        public string UserName { get; set; }
        public string Master { get; set; }
        public string BatchNo { get; set; }
        public int Action { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<ImportLogClinician> import_clinician { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<ImportLogDenial> import_Denial { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<ImportLogInsurance> import_Insurance { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<ImportLogCpt> import_Cpt { get; set; }
    }
    public class ImportTypeResponse
    {
        public int flag { get; set; }
        public string message { get; set; }
        public List<ImportMasterType> Master { get; set; }
        public List<ImportMasterColumn> Clinician { get; set; }
        public List<ImportMasterColumn> Denial { get; set; }
        public List<ImportMasterColumn> Insurance { get; set; }
        public List<ImportMasterColumn> Cpt { get; set; }

    }
    public class ImportLogClinician
    {
        public int ID { get; set; }
        public int LogID { get; set; }
        public string ClinicianLicense { get; set; }
        public string ClinicianName { get; set; }
        public string ClinicianShortName { get; set; }
        public string Speciality { get; set; }
        public string ClinicianMajor { get; set; }
        public string ClinicianProfession { get; set; }
        public string ClinicianCategory { get; set; }
        public string Gender { get; set; }
    }
    public class ImportLogDenial
    {
        public int ID { get; set; }
        public int LogID { get; set; }
        public string DenialCode { get; set; }
        public string DenialName { get; set; }
        public string DenialType { get; set; }
        public string DenialCategory { get; set; }
        public string Description { get; set; }
    }
    public class ImportLogInsurance
    {
        public int ID { get; set; }
        public int LogID { get; set; }
        public string InsuranceID { get; set; }
        public string InsuranceName { get; set; }
        public string InsuranceShortName { get; set; }
        public string Classification { get; set; }
    }
    public class ImportLogCpt
    {
        public int ID { get; set; }
        public int LogID { get; set; }
        public string CPTCode { get; set; }
        public string CPTShortName { get; set; }
        public string CPTName { get; set; }
        public string CPTType { get; set; }
        public string Description { get; set; }

    }
    public class ImportLog
    {
        public int ID { get; set; }
        public int MasterID { get; set; }
        public int SerialNo { get; set; }
        public int DocNo { get; set; }
        public int UserID { get; set; }
        public DateTime ImportTime { get; set; }
        public bool NewRecordOnly { get; set; }
        public string UserName { get; set; }
        public string Master { get; set; }
        public List<ImportLogCpt> import_Cpt { get; set; }
        public List<ImportLogClinician> import_clinician { get; set; }
        public List<ImportLogDenial> import_Denial { get; set; }
        public List<ImportLogInsurance> import_Insurance { get; set; }
    }


}
