using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VEZTA.Models;
using Org.BouncyCastle.Asn1.X500;
using System.Drawing;

namespace VEZTA.DAL
{
    public class Antibiotic_DAL
    {
        public AntibioticResponse Insert(AntibioticClass vInput)
        {
            AntibioticResponse res = new AntibioticResponse();

            try
            {
                
                string strSQL = "INSERT INTO TB_ANTIBIOTIC (CLASS_ID, ANTIBIOTIC, IS_INACTIVE, DISPLAY_ORDER,ANTIBIOTIC_GROUP) VALUES (" + vInput.CLASS_ID + "," +
                                    ADO.SQLString(vInput.ANTIBIOTIC) + "," + Convert.ToByte(vInput.IS_INACTIVE) + "," + vInput.DISPLAY_ORDER + "," + vInput.ANTIBIOTIC_GROUP_ID + ")";
                ADO.ExecuteNonQuery(strSQL);

                res.flag = 1;
                res.Message = "success";
                
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }

            return res;
        }


        public AntibioticResponse Update(AntibioticClass vInput)
        {
            AntibioticResponse res = new AntibioticResponse();

            try
            {
                string strSQL = "UPDATE TB_ANTIBIOTIC SET ANTIBIOTIC = " + ADO.SQLString(vInput.ANTIBIOTIC) + "," +
                                "CLASS_ID = " + vInput.CLASS_ID + "," +
                                "DISPLAY_ORDER = " + vInput.DISPLAY_ORDER + "," +
                                "IS_INACTIVE = " + Convert.ToByte(vInput.IS_INACTIVE) + "," +
                                "ANTIBIOTIC_GROUP = " + vInput.ANTIBIOTIC_GROUP_ID +
                                " WHERE ID = " + vInput.ID;
                  
                ADO.ExecuteNonQuery(strSQL);

                res.flag = 1;
                res.Message = "success";

            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }

            return res;
        }
        public AntibioticResponse Delete(Int32 id)
        {
            AntibioticResponse res = new AntibioticResponse();

            try
            {
                string strSQL = "DELETE FROM TB_ANTIBIOTIC WHERE ID = " + id;

                ADO.ExecuteNonQuery(strSQL);

                res.flag = 1;
                res.Message = "success";

            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }

            return res;
        }

        public AntibioticResponse GetItemsById(int id)
        {
            AntibioticResponse res = new AntibioticResponse();
            List<AntibioticClass> LstAnti = new List<AntibioticClass>();
            try
            {
                string strSQL = "SELECT TB_ANTIBIOTIC.*, TB_ANTIBIOTIC_CLASS.CLASS_NAME,TB_ANTIBIOTIC_GROUP.[GROUP]" +
                                "FROM TB_ANTIBIOTIC " +
                                "INNER JOIN TB_ANTIBIOTIC_CLASS ON TB_ANTIBIOTIC.CLASS_ID = TB_ANTIBIOTIC_CLASS.ID " +
                                "LEFT JOIN TB_ANTIBIOTIC_GROUP ON TB_ANTIBIOTIC.ANTIBIOTIC_GROUP = TB_ANTIBIOTIC_GROUP.ID " +
                                " WHERE TB_ANTIBIOTIC.ID = " + id;
 
                DataTable tbl = ADO.GetDataTable(strSQL, "Antibiotic");

                if (tbl.Rows.Count > 0)
                {
                    DataRow dr = tbl.Rows[0];
                    LstAnti.Add(new AntibioticClass
                    {
                        ID = ADO.ToInt32(dr["ID"]),
                        CLASS_ID = ADO.ToInt32(dr["CLASS_ID"]),
                        CLASS_NAME = ADO.ToString(dr["CLASS_NAME"]),
                        ANTIBIOTIC = ADO.ToString(dr["ANTIBIOTIC"]),
                        IS_INACTIVE = Convert.ToBoolean(dr["IS_INACTIVE"]),
                        DISPLAY_ORDER = ADO.ToInt32(dr["DISPLAY_ORDER"]),
                        ANTIBIOTIC_GROUP_ID = ADO.ToInt32(dr["ANTIBIOTIC_GROUP"]),
                        ANTIBIOTIC_GROUP_NAME = ADO.ToString(dr["GROUP"])

                    });
                }
                res.Data = LstAnti.ToList();
                res.flag = 1;
                res.Message = "Success";
               
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }

            return res;
        }

        public AntibioticResponse GetList()
        {
            AntibioticResponse res = new AntibioticResponse();
            
            try
            {

                List<AntibioticClass> LstAnti = new List<AntibioticClass>();

                string strSQL ="SELECT TB_ANTIBIOTIC.*, TB_ANTIBIOTIC_CLASS.CLASS_NAME, TB_ANTIBIOTIC_GROUP.[GROUP] " +
                 "FROM TB_ANTIBIOTIC " +
                 "INNER JOIN TB_ANTIBIOTIC_CLASS ON TB_ANTIBIOTIC.CLASS_ID = TB_ANTIBIOTIC_CLASS.ID " +
                 "LEFT JOIN TB_ANTIBIOTIC_GROUP ON TB_ANTIBIOTIC.ANTIBIOTIC_GROUP = TB_ANTIBIOTIC_GROUP.ID " +
                 "ORDER BY TB_ANTIBIOTIC_CLASS.DISPLAY_ORDER, TB_ANTIBIOTIC.DISPLAY_ORDER";
                DataTable tbl = ADO.GetDataTable(strSQL);
                if (tbl.Rows.Count > 0)
                {
                    
                    foreach (DataRow dr in tbl.Rows)
                    {
                        LstAnti.Add(new AntibioticClass
                        {
                            ID = ADO.ToInt32(dr["ID"]),
                            CLASS_ID = ADO.ToInt32(dr["CLASS_ID"]),
                            CLASS_NAME = ADO.ToString(dr["CLASS_NAME"]),
                            ANTIBIOTIC = ADO.ToString(dr["ANTIBIOTIC"]),
                            IS_INACTIVE = Convert.ToBoolean(dr["IS_INACTIVE"]),
                            DISPLAY_ORDER = ADO.ToInt32(dr["DISPLAY_ORDER"]),
                            ANTIBIOTIC_GROUP_ID = ADO.ToInt32(dr["ANTIBIOTIC_GROUP"]),
                            ANTIBIOTIC_GROUP_NAME = ADO.ToString(dr["GROUP"]),

                        });
                    }
                   
                }
 

                res.flag = 1;
                res.Message = "Success";
                res.Data = LstAnti.ToList();
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }

            return res;
        }

         
    }
}

