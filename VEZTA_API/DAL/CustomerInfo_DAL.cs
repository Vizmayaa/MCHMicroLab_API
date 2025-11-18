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
    public class CustomerInfo_DAL
    {
        string conString = ConfigurationManager.ConnectionStrings["WSMasterConnectionString"].ToString();

        public CustomerInfoInput GetCustomerInfoInput()
        {
            
            try
            {
                
                CustomerInfoInput vInput = new CustomerInfoInput();
                string strSQL = "SELECT CustomerKey, ChangeLogID FROM TB_LICENSE";
                DataTable tbl = ADO.GetDataTable(strSQL);

                if (tbl.Rows.Count > 0)
                {
                    vInput.CustomerKey = tbl.Rows[0]["CustomerKey"].ToString();
                    vInput.LogID = tbl.Rows[0]["ChangeLogID"].ToString();
                                       
                }
                else
                {
                    vInput.CustomerKey = "";
                    vInput.LogID = "";

                }
                return vInput;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        //Insert Customers
        public bool UpdateCustomerInfo(CustomerInfo vInfo)
        {

            string strSQL = "";
            try
            {
                SqlConnection objCon = ADO.GetConnection();
                SqlTransaction objTrans = objCon.BeginTransaction();

                try
                {
                    strSQL = "SELECT ID, MenuGroup FROM TB_MENU_GROUPS";
                    DataTable tblMenuGroup = ADO.GetDataTable(strSQL, objCon, objTrans);
                    tblMenuGroup.Constraints.Add("PK", tblMenuGroup.Columns["ID"], true);

                    //Menu Group
                    foreach (MenuGroupInfo menuGroup in vInfo.MenuGroupInfo )
                    {
                        DataRow dr = tblMenuGroup.Rows.Find(menuGroup.ID);
                        if (dr != null)
                        {
                            strSQL = "UPDATE TB_MENU_GROUPS SET MenuGroup = " + ADO.SQLString(menuGroup.MENU_GROUP) + "," +
                                     "MenuOrder = " + menuGroup.MENU_ORDER + ", MenuPath = " + ADO.SQLString(menuGroup.MENU_PATH) + ","  +
                                     "MenuIcon = " + ADO.SQLString(menuGroup.MENU_ICON) + ","  +
                                     "IsInactive = " + Convert.ToByte(menuGroup.IS_INACTIVE) + "," +
                                     "MainGroupID =" +  menuGroup.MAIN_GROUP_ID +   
                                     " WHERE ID = " + menuGroup.ID;

                            ADO.ExecuteNonQuery(strSQL, objCon, objTrans);
                        }
                        else
                        {
                            strSQL = "INSERT INTO TB_MENU_GROUPS(ID, MenuGroup, MenuPath, MenuIcon, MenuOrder, IsInactive, MainGroupID) VALUES (" +
                                   menuGroup.ID + "," + ADO.SQLString(menuGroup.MENU_GROUP) + "," + ADO.SQLString(menuGroup.MENU_PATH) + "," +
                                   ADO.SQLString(menuGroup.MENU_ICON) + "," + menuGroup.MENU_ORDER + "," + Convert.ToByte(menuGroup.IS_INACTIVE) + "," + 
                                   menuGroup.MAIN_GROUP_ID + ")";

                            ADO.ExecuteNonQuery(strSQL, objCon, objTrans);
                        }
                    }

                    //Menu
                    strSQL = "SELECT ID, MenuName FROM TB_MENUS";
                    DataTable tblMenu = ADO.GetDataTable(strSQL, objCon, objTrans);
                    tblMenu.Constraints.Add("PK", tblMenu.Columns["ID"], true);

                    foreach (MenuInfo vMenu in vInfo.MenuInfo)
                    {
                        DataRow dr = tblMenu.Rows.Find(vMenu.ID);
                        if (dr != null)
                        {
                            strSQL = "UPDATE TB_MENUS SET MenuName = " + ADO.SQLString(vMenu.MENU_NAME) + "," +
                                    "MenuGroupID = " + vMenu.MENU_GROUP_ID + ", MenuPath = " + ADO.SQLString(vMenu.MENU_PATH) + "," +
                                    "MenuOrder = " + vMenu.MENU_ORDER + ", IsInactive = " + Convert.ToByte(vMenu.IS_INACTIVE) +
                                    " WHERE ID = " + vMenu.ID;

                            ADO.ExecuteNonQuery(strSQL, objCon, objTrans);
                        }
                        else
                        {
                            strSQL = "INSERT INTO TB_MENUS(ID, MenuName, MenuGroupID, MenuPath, MenuOrder, IsInactive) VALUES (" +
                                    vMenu.ID + "," + ADO.SQLString(vMenu.MENU_NAME) + "," + vMenu.MENU_GROUP_ID + "," +
                                    ADO.SQLString(vMenu.MENU_PATH) + "," + vMenu.MENU_ORDER + "," + Convert.ToByte(vMenu.IS_INACTIVE) + ")";

                            ADO.ExecuteNonQuery(strSQL, objCon, objTrans);
                        }
                    }

                    //Post office
                    strSQL = "SELECT ID FROM TB_POST_OFFICE";
                    DataTable tblPost = ADO.GetDataTable(strSQL, objCon, objTrans);
                    tblPost.Constraints.Add("PK", tblPost.Columns["ID"], true);

                    foreach(PostOfficeInfo vPost in vInfo.PostOfficeInfo)
                    {
                        DataRow dr = tblPost.Rows.Find(vPost.ID);
                        if (dr != null)
                        {
                            strSQL = "UPDATE TB_POST_OFFICE SET PostOffice = " + ADO.SQLString(vPost.POSTOFFICE) + "," +
                                    "APIURL = " + ADO.SQLString(vPost.APIURL) + "," +
                                    "isXML = " + Convert.ToByte(vPost.isXML) +
                                    " WHERE ID = " + vPost.ID;
                            
                            ADO.ExecuteNonQuery(strSQL, objCon, objTrans);
                        }
                        else
                        {
                            strSQL = "INSERT INTO TB_POST_OFFICE( ID, PostOffice, APIURL, isXML) VALUES (" +
                                   vPost.ID + "," + ADO.SQLString(vPost.POSTOFFICE) + "," + ADO.SQLString(vPost.APIURL) + "," +
                                   Convert.ToByte(vPost.isXML) + ")";

                            ADO.ExecuteNonQuery(strSQL, objCon, objTrans);
                        }
                    }


                    //Facility
                    strSQL = "SELECT ID, FacilityLicense FROM TB_FACILITY";
                    DataTable tblFacility = ADO.GetDataTable(strSQL, objCon, objTrans);
                    tblFacility.Constraints.Add("PK", tblFacility.Columns["ID"], true);

                    foreach (FacilityInfo vFacility in vInfo.FacilityInfo)
                    {
                        DataRow dr = tblFacility.Rows.Find(vFacility.ID);
                        if (dr != null)
                        {
                            strSQL = "UPDATE TB_FACILITY SET FacilityLicense = " + ADO.SQLString(vFacility.FACILITY_LICENSE) + "," +
                                    "PostOfficeID = " + vFacility.POST_OFFICE_ID + "," +
                                    "IsInactive = " + Convert.ToByte(vFacility.IS_INACTIVE) +
                                    " WHERE ID = " + vFacility.ID;

                            ADO.ExecuteNonQuery(strSQL, objCon, objTrans);

                            strSQL = "UPDATE TB_LICENSE_FACILITIES SET " + 
                                     "ExpiryDate = " + ADO.SQLString(vFacility.EXPIRY_DATE) + "," +
                                     "AMCExpiryDate = " + ADO.SQLString( vFacility.AMC_EXPIRY_DATE) +
                                     " WHERE FacilityID = " + vFacility.ID;

                            ADO.ExecuteNonQuery(strSQL, objCon, objTrans);
                            
                        }
                        else
                        {
                            strSQL = "INSERT INTO TB_FACILITY(ID, FacilityLicense, FacilityName, PostOfficeID, IsInactive, IsDeleted) " +
                                    "VALUES (" + vFacility.ID + "," + ADO.SQLString(vFacility.FACILITY_LICENSE) + "," + ADO.SQLString(vFacility.FACILITY_NAME) + "," +
                                    vFacility.POST_OFFICE_ID + "," + Convert.ToByte(vFacility.IS_INACTIVE) + ", 0)";

                            ADO.ExecuteNonQuery(strSQL, objCon, objTrans);


                            strSQL = "INSERT INTO TB_LICENSE_FACILITIES (FacilityID, EnrollDate, ExpiryDate, AMCExpiryDate) VALUES (" +
                                    vFacility.ID + "," + ADO.SQLString(vFacility.ENROLL_DATE) + "," +
                                    ADO.SQLString(vFacility.EXPIRY_DATE) + "," +
                                    ADO.SQLString(vFacility.AMC_EXPIRY_DATE) + ")";

                            ADO.ExecuteNonQuery(strSQL, objCon, objTrans);
                        }
                    }

                    //cONFIGURATION
                    strSQL = "SELECT ConfigurationKey FROM TB_CONFIGURATION";
                    DataTable tblConf = ADO.GetDataTable(strSQL, objCon, objTrans);
                    tblConf.Constraints.Add("PK", tblConf.Columns["ConfigurationKey"], true);

                    foreach(ConfigurationInfo vconf in vInfo.ConfigurationInfo )
                    {
                        DataRow dr = tblConf.Rows.Find(vconf.CONFIGURATION_KEY);

                        if (dr != null)
                        {
                            strSQL = "UPDATE TB_CONFIGURATION SET ConfigurationValue = " + ADO.SQLString(vconf.CONFIGURATION_VALUE) +
                                    " WHERE ConfigurationKey = " + ADO.SQLString(vconf.CONFIGURATION_KEY);

                            ADO.ExecuteNonQuery(strSQL, objCon, objTrans);
                        }
                        else
                        {
                            strSQL = "INSERT INTO TB_CONFIGURATION (ConfigurationKey, ConfigurationValue) VALUES (" +
                                    ADO.SQLString(vconf.CONFIGURATION_KEY) + "," + ADO.SQLString(vconf.CONFIGURATION_VALUE) + ")";

                            ADO.ExecuteNonQuery(strSQL, objCon, objTrans);
                        }
                    }

                    
                    //License Info
                    LicenseInfo vLicense = vInfo.LicenseInfo[0];

                    strSQL = "UPDATE TB_LICENSE SET LicenseExpiryDate = " + ADO.SQLString(vLicense.EXPIRY_DATE) + "," +
                             "ChangeLogID = " + vLicense.CHANGE_LOG_ID;

                    ADO.ExecuteNonQuery(strSQL, objCon, objTrans);


                    strSQL = "INSERT INTO TB_INITIALIZE_LOG(InitializeTime, InitializeStatus, FailureReason) VALUES (" +
                            "GETDATE(), 1, '')";

                    ADO.ExecuteNonQuery(strSQL, objCon, objTrans);

                    objTrans.Commit();
                }
                catch (Exception ex)
                {
                    objTrans.Rollback();
                    throw ex;
                }
                finally
                {
                    objCon.Close();
                }
            }
            catch (Exception ex)
            {
                strSQL = "INSERT INTO TB_INITIALIZE_LOG(InitializeTime, InitializeStatus, FailureReason) VALUES (" +
                            "GETDATE(), 0, " + ADO.SQLString(ex.Message + Environment.NewLine + strSQL) +  ")";

                ADO.ExecuteNonQueryIgnoreException(strSQL);
            }
            return true;
            
        }

    }
}