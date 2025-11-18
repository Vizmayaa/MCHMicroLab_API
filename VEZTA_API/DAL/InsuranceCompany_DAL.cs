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
    public class InsuranceCompany_DAL
    {
        public List<InsuranceCompany> GetAllInsuranceCompany(int intUserID)
        {
            SqlConnection connection = ADO.GetConnection();
            
                SqlCommand cmd = new SqlCommand
                {
                    Connection = connection,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "SP_TB_INSURANCE_COMPANY"
                };
                cmd.Parameters.AddWithValue("ACTION", 0);
                cmd.Parameters.AddWithValue("UserID", intUserID);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tbl = new DataTable();
                da.Fill(tbl);

                // Use LINQ to convert DataTable to List<InsuranceCompany>
                return tbl.AsEnumerable().Select(dr => new InsuranceCompany
                {
                    ID = Convert.ToInt32(dr["id"]),
                    InsuranceID = Convert.ToString(dr["InsuranceID"]),
                    ClassificationID = Convert.ToInt32(dr["ClassificationID"]),
                    InsuranceName = Convert.ToString(dr["InsuranceName"]),
                    InsuranceShortName = Convert.ToString(dr["InsuranceShortName"]),
                    IsInActive = Convert.ToBoolean(dr["IsInActive"]),
                    IsDeleted = Convert.ToBoolean(dr["IsDeleted"]),
                    Classification = Convert.ToString(dr["Classification"])
                }).ToList();
          
        }


        //public List<InsuranceCompany> GetAllInsuranceCompany(Int32 intUserID)
        //{

        //    List<InsuranceCompany> insuranceList = new List<InsuranceCompany>();
        //    using (SqlConnection connection = ADO.GetConnection())
        //    {
        //        SqlCommand cmd = new SqlCommand();
        //        cmd.Connection = connection;
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.CommandText = "SP_TB_INSURANCE_COMPANY";
        //        cmd.Parameters.AddWithValue("ACTION", 0);
        //        cmd.Parameters.AddWithValue("UserID", intUserID);

        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        DataTable tbl = new DataTable();
        //        da.Fill(tbl);

        //        foreach (DataRow dr in tbl.Rows)
        //        {
        //            insuranceList.Add(new InsuranceCompany
        //            {
        //                ID = Convert.ToInt32(dr["id"]),
        //                InsuranceID = Convert.ToString(dr["InsuranceID"]),
        //                ClassificationID = Convert.ToInt32(dr["ClassificationID"]),
        //                InsuranceName = Convert.ToString(dr["InsuranceName"]),
        //                InsuranceShortName = Convert.ToString(dr["InsuranceShortName"]),
        //                IsInActive = Convert.ToBoolean(dr["IsInActive"]),
        //                IsDeleted = Convert.ToBoolean(dr["IsDeleted"]),
        //                Classification= Convert.ToString(dr["Classification"])

        //            });
        //        }
        //        connection.Close();
        //    }
        //    return insuranceList;
        //}
        public Int32 Insert(InsuranceCompany insuranceCompany, Int32 userID)
        
        {
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_INSURANCE_COMPANY";
                    cmd.Parameters.AddWithValue("ACTION", 1);
                   
                    cmd.Parameters.AddWithValue("InsuranceID", insuranceCompany.InsuranceID);
                    cmd.Parameters.AddWithValue("ClassificationID", insuranceCompany.ClassificationID);
                    cmd.Parameters.AddWithValue("InsuranceName", insuranceCompany.InsuranceName);
                    cmd.Parameters.AddWithValue("InsuranceShortName", insuranceCompany.InsuranceShortName);
                    cmd.Parameters.AddWithValue("IsInActive", insuranceCompany.IsInActive);
                    cmd.Parameters.AddWithValue("UserID", userID);


                    cmd.ExecuteNonQuery();

                    SqlCommand cmd1 = new SqlCommand();
                    cmd1.Connection = connection;
                    cmd1.CommandType = CommandType.Text;
                    cmd1.CommandText = "SELECT MAX(ID) FROM TB_INSURANCE_COMPANY";
                    Int32 InsuranceID = Convert.ToInt32(cmd1.ExecuteScalar());

                    return InsuranceID;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Int32 Update(InsuranceCompany insuranceCompany, Int32 userID)

        {
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_INSURANCE_COMPANY";
                    cmd.Parameters.AddWithValue("ACTION", 2);

                    cmd.Parameters.AddWithValue("ID", insuranceCompany.ID);
                    cmd.Parameters.AddWithValue("InsuranceID", insuranceCompany.InsuranceID);
                    cmd.Parameters.AddWithValue("ClassificationID", insuranceCompany.ClassificationID);
                    cmd.Parameters.AddWithValue("InsuranceName", insuranceCompany.InsuranceName);
                    cmd.Parameters.AddWithValue("InsuranceShortName", insuranceCompany.InsuranceShortName);
                    cmd.Parameters.AddWithValue("IsInActive", insuranceCompany.IsInActive);
                    cmd.Parameters.AddWithValue("UserID", userID);


                    cmd.ExecuteNonQuery();

                    SqlCommand cmd1 = new SqlCommand();
                    cmd1.Connection = connection;
                    cmd1.CommandType = CommandType.Text;
                    cmd1.CommandText = "SELECT MAX(ID) FROM TB_INSURANCE_COMPANY";
                    Int32 InsuranceID = Convert.ToInt32(cmd1.ExecuteScalar());

                    return InsuranceID;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<InsuranceCompany> GetItems(int id)
        {
            List<InsuranceCompany> insuranceCompanies = new List<InsuranceCompany>();
            try
            {
                string strSQL = "SELECT TB_INSURANCE_COMPANY.ID, TB_INSURANCE_COMPANY.InsuranceID, " +
                "TB_INSURANCE_COMPANY.ClassificationID, TB_INSURANCE_COMPANY.InsuranceName, " +
                "TB_INSURANCE_COMPANY.InsuranceShortName, TB_INSURANCE_CLASSIFICATION.Classification, " +
                "TB_INSURANCE_COMPANY.IsInActive " +
                "FROM TB_INSURANCE_COMPANY " +
                "LEFT JOIN TB_INSURANCE_CLASSIFICATION ON TB_INSURANCE_COMPANY.ClassificationID = TB_INSURANCE_CLASSIFICATION.ID " +
                "WHERE TB_INSURANCE_COMPANY.ID = " + id;



                DataTable tbl = ADO.GetDataTable(strSQL, "Clinician");

                if (tbl.Rows.Count > 0)
                {
                    DataRow dr = tbl.Rows[0];

                    insuranceCompanies.Add(new InsuranceCompany
                    {
                        ID = Convert.ToInt32(dr["id"]),
                        InsuranceID = Convert.ToString(dr["InsuranceID"]),
                        ClassificationID = Convert.ToInt32(dr["ClassificationID"]),
                        InsuranceName = Convert.ToString(dr["InsuranceName"]),
                        InsuranceShortName = Convert.ToString(dr["InsuranceShortName"]),
                        IsInActive = Convert.ToBoolean(dr["IsInActive"]),
                       
                        Classification = Convert.ToString(dr["Classification"])

                    });

                }
            }
            catch (Exception ex)
            {

            }

            return insuranceCompanies;
        }
    
        public bool DeleteInsuranceCompany(int Id, int userID)
        {
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_INSURANCE_COMPANY";
                    cmd.Parameters.AddWithValue("ACTION",4);
                    cmd.Parameters.AddWithValue("@ID", Id);
                    cmd.Parameters.AddWithValue("UserID", userID);

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