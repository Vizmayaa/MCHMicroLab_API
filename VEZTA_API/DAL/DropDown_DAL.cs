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
    public class DropDown_DAL
    {
         

        public List<DropDown> GetDropDownData(string vName)
        {
            List<DropDown> vList = new List<DropDown>();


            using (SqlConnection connection = ADO.GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_GET_MASTER_DATA";
                cmd.Parameters.AddWithValue("@NAME", vName);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tbl = new DataTable();
                da.Fill(tbl);

                foreach (DataRow dr in tbl.Rows)
                {
                    vList.Add(new DropDown
                    {
                        ID = Convert.ToInt32(dr["ID"]),
                       // CODE = Convert.ToString(dr["CODE"]),
                        DESCRIPTION = Convert.ToString(dr["DESCRIPTION"])
                    });
                }
                connection.Close();
            }
            return vList;
        }
    }
}