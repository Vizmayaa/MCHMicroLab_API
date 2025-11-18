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
    public class LicenseType_DAL
    {
        string conString = ConfigurationManager.ConnectionStrings["WSMasterConnectionString"].ToString();

        public List<LicenseTypes> GetAllLicensetypes()
        {
            List<LicenseTypes> licenseList = new List<LicenseTypes>();
            using (SqlConnection connection = ADO.GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_LICENSETYPES";
                cmd.Parameters.AddWithValue("ACTION", 0);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tbl = new DataTable();
                da.Fill(tbl);

                foreach (DataRow dr in tbl.Rows)
                {
                    licenseList.Add(new LicenseTypes
                    {
                        ID = Convert.ToInt32(dr["ID"]),
                        LICENSETYPES = Convert.ToString(dr["LICENSETYPES"])
                 
                    });
                }
                connection.Close();
            }
            return licenseList;
        }
        public bool Insert(LicenseTypes licensetypes)
        {
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_LICENSETYPES";
                    cmd.Parameters.AddWithValue("ACTION", 1);

                    cmd.Parameters.AddWithValue("LICENSETYPES", licensetypes.LICENSETYPES);
                    

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
        public LicenseTypes GetItems(int id)
        {
            LicenseTypes licenseTypes = new LicenseTypes();
            try
            {
                string strSQL = "SELECT ID,LICENSETYPES FROM TB_LICENSE_TYPES WHERE TB_LICENSE_TYPES.ID =" + id;

                DataTable tbl = ADO.GetDataTable(strSQL, "LicenseTypes");
                if (tbl.Rows.Count > 0)
                {
                    DataRow dr = tbl.Rows[0];

                    licenseTypes.ID = Convert.ToInt32(dr["ID"]);
                    licenseTypes.LICENSETYPES= Convert.ToString(dr["LICENSETYPES"]);
                }
            }
            catch (Exception ex)
            {

            }
            return licenseTypes;
        }
        public bool Update(LicenseTypes licenseTypes)
        {
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_LICENSETYPES";
                    cmd.Parameters.AddWithValue("ACTION", 3);

                    cmd.Parameters.AddWithValue("ID", licenseTypes.ID);
                    cmd.Parameters.AddWithValue("LICENSETYPES", licenseTypes.LICENSETYPES);
                   

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
        public bool DeleteProduct(int id)
        {
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_LICENSETYPES";
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