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
    public class Facility_DAL
    {
        string conString = ConfigurationManager.ConnectionStrings["WSMasterConnectionString"].ToString();

        public List<Facility> GetAllFacilities(Int32 intUserID)
        {
            List<Facility> facilities = new List<Facility>();
            using (SqlConnection connection = ADO.GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_FACILITY";
                cmd.Parameters.AddWithValue("ACTION", 0);
                cmd.Parameters.AddWithValue("UserID", intUserID);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tbl = new DataTable();
                da.Fill(tbl);

                foreach (DataRow dr in tbl.Rows)
                {
                    facilities.Add(new Facility
                    {
                        ID = Convert.ToInt32(dr["ID"]),
                        FacilityLicense = ADO.ToString(dr["FacilityLicense"]),
                        FacilityName = ADO.ToString(dr["FacilityName"]),
                        FacilityRegionID = ADO.ToInt32(dr["FacilityRegionID"]),
                        FacilityRegion = ADO.ToString(dr["FacilityRegion"]),
                        FacilityTypeID = ADO.ToInt32(dr["FacilityTypeID"]),
                        FacilityType = ADO.ToString(dr["FacilityType"]),

                        FacilityGroupID = ADO.ToInt32(dr["FacilityGroupID"]),
                        FacilityGroup = ADO.ToString(dr["FacilityGroup"]),
                        FacilityAddress = ADO.ToString(dr["FacilityAddress"]),

                        PostOfficeID = ADO.ToInt32(dr["PostOfficeID"]),
                        Postoffice = ADO.ToString(dr["Postoffice"]),
                        IsDeleted = Convert.ToString(dr["IsDeleted"])

                    }); ;
                }
                connection.Close();
            }
            return facilities;
        }
        public Int32 Update(Facility facility, Int32 userID)
        {
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_FACILITY";
                    cmd.Parameters.AddWithValue("ACTION", 3);

                    cmd.Parameters.AddWithValue("ID", facility.ID);
                    cmd.Parameters.AddWithValue("FacilityLicense", facility.FacilityLicense);
                    cmd.Parameters.AddWithValue("FacilityName", facility.FacilityName);
                    cmd.Parameters.AddWithValue("FacilityRegionID", facility.FacilityRegionID);
                    cmd.Parameters.AddWithValue("FacilityTypeID", facility.FacilityTypeID);
                    cmd.Parameters.AddWithValue("FacilityGroupID", facility.FacilityGroupID);
                    cmd.Parameters.AddWithValue("FacilityAddress", facility.FacilityAddress);
                    cmd.Parameters.AddWithValue("PostOfficeID", facility.PostOfficeID);
                    cmd.Parameters.AddWithValue("UserID", userID);

                    cmd.ExecuteNonQuery();

                    SqlCommand cmd1 = new SqlCommand();
                    cmd1.Connection = connection;
                    cmd1.CommandType = CommandType.Text;
                    cmd1.CommandText = "SELECT MAX(ID) FROM TB_FACILITY";
                    Int32 facilityID = Convert.ToInt32(cmd1.ExecuteScalar());

                    return facilityID;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Facility> GetItems(int id)
        {
            List<Facility> FacilityList = new List<Facility>();
            try
            {
                string strSQL = "SELECT TB_FACILITY.ID, TB_FACILITY.FacilityLicense, TB_FACILITY.FacilityName, " +
                 "TB_FACILITY.FacilityRegionID, TB_FACILITY_REGION.FacilityRegion, TB_FACILITY.FacilityTypeID, " +
                 "TB_FACILITY.FacilityGroupID, TB_FACILITY.FacilityAddress, TB_FACILITY.PostOfficeID, " +
                 "TB_FACILITY.IsDeleted, " +
                 "TB_FACILITY_TYPE.FacilityType, TB_FACILITY_GROUP.FacilityGroup, TB_POST_OFFICE.Postoffice " +
                 "FROM TB_FACILITY " +
                 "LEFT JOIN TB_FACILITY_TYPE ON TB_FACILITY.FacilityTypeID = TB_FACILITY_TYPE.ID " +
                 "LEFT JOIN TB_FACILITY_GROUP ON TB_FACILITY.FacilityGroupID = TB_FACILITY_GROUP.ID " +
                 "LEFT JOIN TB_FACILITY_REGION ON TB_FACILITY.FacilityRegionID = TB_FACILITY_REGION.ID " +
                 "LEFT JOIN TB_POST_OFFICE ON TB_FACILITY.PostOfficeID = TB_POST_OFFICE.ID " +
                 "WHERE TB_FACILITY.ID = " + id;


                DataTable tbl = ADO.GetDataTable(strSQL, "Facility");

                if (tbl.Rows.Count > 0)
                {
                    DataRow dr = tbl.Rows[0];

                    FacilityList.Add(new Facility
                    {
                        ID = Convert.ToInt32(dr["ID"]),
                        FacilityLicense = dr["FacilityLicense"] != DBNull.Value ? Convert.ToString(dr["FacilityLicense"]) : null,
                        FacilityName = dr["FacilityName"] != DBNull.Value ? Convert.ToString(dr["FacilityName"]) : null,
                        FacilityRegionID = ADO.ToInt32(dr["FacilityRegionID"]),
                        FacilityRegion = ADO.ToString(dr["FacilityRegion"]),
                        FacilityTypeID = dr["FacilityTypeID"] != DBNull.Value ? Convert.ToInt32(dr["FacilityTypeID"]) : 0,
                        FacilityType = dr["FacilityType"] != DBNull.Value ? Convert.ToString(dr["FacilityType"]) : null,

                        FacilityGroupID = dr["FacilityGroupID"] != DBNull.Value ? Convert.ToInt32(dr["FacilityGroupID"]) : 0,
                        FacilityGroup = dr["FacilityGroup"] != DBNull.Value ? Convert.ToString(dr["FacilityGroup"]) : null,
                        FacilityAddress = dr["FacilityAddress"] != DBNull.Value ? Convert.ToString(dr["FacilityAddress"]) : null,

                        PostOfficeID = dr["PostOfficeID"] != DBNull.Value ? Convert.ToInt32(dr["PostOfficeID"]) : 0,
                        Postoffice = dr["Postoffice"] != DBNull.Value ? Convert.ToString(dr["Postoffice"]) : null,
                        IsDeleted = dr["IsDeleted"] != DBNull.Value ? Convert.ToString(dr["IsDeleted"]) : null

                    });

                }
            }
            catch (Exception ex)
            {

            }

            return FacilityList;
        }

        public bool DeleteFacility(int id, int userID)
        {
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_FACILITY";
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


        public LicenseFacInfo GetFaclityLicenseInfo()
        {
            LicenseFacInfo response = new LicenseFacInfo();
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                using (SqlCommand cmd = new SqlCommand("SP_LICENSEFACILITY_INFO", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    // Check the first result set for validity
                    if (ds.Tables.Count > 0)
                    {
                        DataTable tblStatus = ds.Tables[0];
                        if (tblStatus.Rows.Count > 0)
                        {
                            DataRow statusRow = tblStatus.Rows[0];

                            // Assign the status row fields
                            response.CustomerName = statusRow["CustomerName"].ToString();
                            response.ProductKey = statusRow["ProductKey"].ToString();

                            // LICENSE
                            List<FacInfo> facilityDetailsInfos = new List<FacInfo>();
                            if (ds.Tables.Count > 1)
                            {
                                DataTable tblFacilities = ds.Tables[1];
                                foreach (DataRow dr in tblFacilities.Rows)
                                {
                                    facilityDetailsInfos.Add(new FacInfo
                                    {
                                        ID = dr["ID"] != DBNull.Value ? Convert.ToInt32(dr["ID"]) : 0,
                                        FacilityLicense = dr["FacilityLicense"] != DBNull.Value ? dr["FacilityLicense"].ToString() : string.Empty,
                                        FacilityName = dr["FacilityName"] != DBNull.Value ? dr["FacilityName"].ToString() : string.Empty,
                                        status = dr["Status"] != DBNull.Value ? dr["Status"].ToString() : string.Empty,
                                        FacilityRegion = dr["FacilityRegion"] != DBNull.Value ? dr["FacilityRegion"].ToString() : string.Empty,
                                        PostOffice = dr["PostOffice"] != DBNull.Value ? dr["PostOffice"].ToString() : string.Empty,
                                        //Expiry_Date= dr["ExpiryDate"] != DBNull.Value ? dr["ExpiryDate"].ToString() : string.Empty,
                                        Expiry_Date = dr["ExpiryDate"] != DBNull.Value ? AzentLibrary.Library.DecryptString(dr["ExpiryDate"].ToString()) : string.Empty
                                });
                                }
                            }

                            response.data = facilityDetailsInfos;
                        }
                        else
                        {
                            response.Flag = 0;
                            response.Message = "No status information available.";
                        }
                    }
                    else
                    {
                        response.Flag = 0;
                        response.Message = "Error: Dataset does not contain the expected tables.";
                    }
                }
            }
            catch (Exception ex)
            {
                response.Flag = 0; // Error flag
                response.Message = "Error: " + ex.Message; // Error message
            }

            return response;
        }

      

    }
}