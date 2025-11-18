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
    public class InsuranceClassification_DAL
    {
        public List<InsuranceClassification> GetAllInsuranceClassification(int intUserID)
        {
            SqlConnection connection = ADO.GetConnection();
            
                SqlCommand cmd = new SqlCommand
                {
                    Connection = connection,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "SP_TB_INSURANCE_CLASSIFICATION"
                };
                cmd.Parameters.AddWithValue("ACTION", 0);
                cmd.Parameters.AddWithValue("UserID", intUserID);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tbl = new DataTable();
                da.Fill(tbl);

                // Use LINQ to convert DataTable to List<InsuranceClassification>
                return tbl.AsEnumerable().Select(dr => new InsuranceClassification
                {
                    ID = Convert.ToInt32(dr["ID"]),
                    Classification = Convert.ToString(dr["Classification"]),
                    Description = Convert.ToString(dr["Description"]),
                    IsInactive = Convert.ToBoolean(dr["IsInactive"]),
                    IsDeleted = Convert.ToBoolean(dr["IsDeleted"])
                }).ToList();

        }


        //public List<InsuranceClassification> GetAllInsuranceClassification(Int32 intUserID)
        //{

        //    List<InsuranceClassification> insuranceclassList = new List<InsuranceClassification>();
        //    using (SqlConnection connection = ADO.GetConnection())
        //    {
        //        SqlCommand cmd = new SqlCommand();
        //        cmd.Connection = connection;
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.CommandText = "SP_TB_INSURANCE_CLASSIFICATION";
        //        cmd.Parameters.AddWithValue("ACTION", 0);
        //        cmd.Parameters.AddWithValue("UserID", intUserID);

        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        DataTable tbl = new DataTable();
        //        da.Fill(tbl);

        //        foreach (DataRow dr in tbl.Rows)
        //        {
        //            insuranceclassList.Add(new InsuranceClassification
        //            {
        //                ID = Convert.ToInt32(dr["ID"]),
        //                Classification = Convert.ToString(dr["Classification"]),
        //                Description = Convert.ToString(dr["Description"]),
        //                IsInactive = Convert.ToBoolean(dr["IsInactive"]),
        //                IsDeleted = Convert.ToBoolean(dr["IsDeleted"])

        //            });
        //        }
        //    }

        //    return insuranceclassList;
        //}

        public Int32 Insert(InsuranceClassification insuranceclassList, Int32 userID)
        {
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_INSURANCE_CLASSIFICATION";
                    cmd.Parameters.AddWithValue("ACTION", 1);

                    cmd.Parameters.AddWithValue("Classification", insuranceclassList.Classification);
                    cmd.Parameters.AddWithValue("Description", insuranceclassList.Description);
                    cmd.Parameters.AddWithValue("IsInactive", insuranceclassList.IsInactive);
                    cmd.Parameters.AddWithValue("UserID", userID);

                    cmd.ExecuteNonQuery();

                    SqlCommand cmd1 = new SqlCommand();
                    cmd1.Connection = connection;
                    cmd1.CommandType = CommandType.Text;
                    cmd1.CommandText = "SELECT MAX(ID) FROM TB_INSURANCE_CLASSIFICATION";
                    Int32 InsuranceClassificationID = Convert.ToInt32(cmd1.ExecuteScalar());

                    return InsuranceClassificationID;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Int32 Update(InsuranceClassification insuranceclassList, Int32 userID)
        {
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_INSURANCE_CLASSIFICATION";
                    cmd.Parameters.AddWithValue("ACTION", 2);

                    cmd.Parameters.AddWithValue("ID", insuranceclassList.ID);
                    cmd.Parameters.AddWithValue("Classification", insuranceclassList.Classification);
                    cmd.Parameters.AddWithValue("Description", insuranceclassList.Description);
                    cmd.Parameters.AddWithValue("IsInactive", insuranceclassList.IsInactive);
                    cmd.Parameters.AddWithValue("UserID", userID);

                    cmd.ExecuteNonQuery();

                    SqlCommand cmd1 = new SqlCommand();
                    cmd1.Connection = connection;
                    cmd1.CommandType = CommandType.Text;
                    cmd1.CommandText = "SELECT MAX(ID) FROM TB_INSURANCE_CLASSIFICATION";
                    Int32 InsuranceClassificationID = Convert.ToInt32(cmd1.ExecuteScalar());

                    return InsuranceClassificationID;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<InsuranceClassification> GetItems(int id)
        {
            List<InsuranceClassification> InsuranceClassificationList = new List<InsuranceClassification>();
            try
            {
                string strSQL = "SELECT ID,Classification,Description,IsInactive" +
                            "  FROM TB_INSURANCE_CLASSIFICATION" +
                               " WHERE TB_INSURANCE_CLASSIFICATION.ID = " + id;

                DataTable tbl = ADO.GetDataTable(strSQL, "FacilityGroupList");

                if (tbl.Rows.Count > 0)
                {
                    DataRow dr = tbl.Rows[0];

                    InsuranceClassificationList.Add(new InsuranceClassification
                    {
                        ID = Convert.ToInt32(dr["ID"]),
                        Classification = Convert.ToString(dr["Classification"]),
                        Description = Convert.ToString(dr["Description"]),
                        IsInactive = Convert.ToBoolean(dr["IsInactive"])
                        
                    });

                }
            }
            catch (Exception ex)
            {

            }

            return InsuranceClassificationList;
        }
        public void DeleteInsuranceclassification(int Id, int userID)
        {
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_INSURANCE_CLASSIFICATION";
                    cmd.Parameters.AddWithValue("ACTION", 4);
                    cmd.Parameters.AddWithValue("@ID", Id);
                    cmd.Parameters.AddWithValue("UserID", userID);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}