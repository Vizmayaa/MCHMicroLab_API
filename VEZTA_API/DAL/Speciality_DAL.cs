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
    public class Speciality_DAL
    {

        public List<Speciality> GetAllSpeciality(int intUserID)
        {
            List<Speciality> specialityList = new List<Speciality>();
            SqlConnection connection = ADO.GetConnection();
            
                SqlCommand cmd = new SqlCommand
                {
                    Connection = connection,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "SP_TB_SPECIALITY"
                };
                cmd.Parameters.AddWithValue("ACTION", 0);
                cmd.Parameters.AddWithValue("UserID", intUserID);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tbl = new DataTable();
                da.Fill(tbl);

                  // Use LINQ to convert DataTable to List<Speciality>
                 specialityList = tbl.AsEnumerable().Select(dr => new Speciality
             
                {
                    ID = Convert.ToInt32(dr["ID"]),
                    SpecialityCode = Convert.ToString(dr["SpecialityCode"]),
                    SpecialityName = Convert.ToString(dr["SpecialityName"]),
                    SpecialityShortName = Convert.ToString(dr["SpecialityShortName"]),
                    Description = Convert.ToString(dr["Description"]),
                    IsActive = Convert.ToBoolean(dr["IsActive"])
                }).ToList();
                return specialityList;
            
        }


        //public List<Speciality> GetAllSpeciality(Int32 intUserID)
        //{
        //    List<Speciality> specialities = new List<Speciality>();
        //    using (SqlConnection connection = ADO.GetConnection())
        //    {
        //        SqlCommand cmd = new SqlCommand();
        //        cmd.Connection = connection;
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.CommandText = "SP_TB_SPECIALITY";
        //        cmd.Parameters.AddWithValue("ACTION", 0);
        //        cmd.Parameters.AddWithValue("UserID", intUserID);
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        DataTable tbl = new DataTable();
        //        da.Fill(tbl);

        //        foreach (DataRow dr in tbl.Rows)
        //        {
        //            specialities.Add(new Speciality
        //            {
        //                ID = Convert.ToInt32(dr["ID"]),
        //                SpecialityCode = Convert.ToString(dr["SpecialityCode"]),
        //                SpecialityName = Convert.ToString(dr["SpecialityName"]),
        //                SpecialityShortName = Convert.ToString(dr["SpecialityShortName"]),
        //                Description= Convert.ToString(dr["Description"]),
        //                IsActive = Convert.ToBoolean(dr["IsActive"])
        //            });
        //        }
        //        connection.Close();
        //    }
        //    return specialities;
        //}      

        public Int32 Insert(Speciality speciality, Int32 userID)
        {
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_SPECIALITY";
                    cmd.Parameters.AddWithValue("ACTION", 1);

                    cmd.Parameters.AddWithValue("SpecialityCode", speciality.SpecialityCode);
                    cmd.Parameters.AddWithValue("SpecialityName", speciality.SpecialityName);
                    cmd.Parameters.AddWithValue("SpecialityShortName", speciality.SpecialityShortName);
                    cmd.Parameters.AddWithValue("Description", speciality.Description);
                    cmd.Parameters.AddWithValue("IsActive", speciality.IsActive);
                    cmd.Parameters.AddWithValue("UserID", userID);

                    cmd.ExecuteNonQuery();

                    SqlCommand cmd1 = new SqlCommand();
                    cmd1.Connection = connection;
                    cmd1.CommandType = CommandType.Text;
                    cmd1.CommandText = "SELECT MAX(ID) FROM TB_SPECIALITY";
                    Int32 SpecialityID = Convert.ToInt32(cmd1.ExecuteScalar());

                    return SpecialityID;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Int32 Update(Speciality speciality, Int32 userID)
        {
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_SPECIALITY";
                    cmd.Parameters.AddWithValue("ACTION", 2);

                    cmd.Parameters.AddWithValue("ID", speciality.ID);
                    cmd.Parameters.AddWithValue("SpecialityCode", speciality.SpecialityCode);
                    cmd.Parameters.AddWithValue("SpecialityName", speciality.SpecialityName);
                    cmd.Parameters.AddWithValue("SpecialityShortName", speciality.SpecialityShortName);
                    cmd.Parameters.AddWithValue("Description", speciality.Description);
                    cmd.Parameters.AddWithValue("IsActive", speciality.IsActive);
                    cmd.Parameters.AddWithValue("UserID", userID);


                    cmd.ExecuteNonQuery();

                    SqlCommand cmd1 = new SqlCommand();
                    cmd1.Connection = connection;
                    cmd1.CommandType = CommandType.Text;
                    cmd1.CommandText = "SELECT MAX(ID) FROM TB_SPECIALITY";
                    Int32 InsuranceID = Convert.ToInt32(cmd1.ExecuteScalar());

                    return InsuranceID;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Speciality> GetItems(int id)
        {
            List<Speciality> specialities = new List<Speciality>();

            try
            {
                string strSQL = "SELECT ID,SpecialityCode,SpecialityName,SpecialityShortName,Description,IsActive FROM TB_SPECIALITY" +
                               " WHERE TB_SPECIALITY.ID = " + id;

                DataTable tbl = ADO.GetDataTable(strSQL, "Speciality");

                if (tbl.Rows.Count > 0)
                {
                    DataRow dr = tbl.Rows[0];

                    specialities.Add(new Speciality
                    {
                        ID = Convert.ToInt32(dr["ID"]),
                        SpecialityCode = Convert.ToString(dr["SpecialityCode"]),
                        SpecialityName = Convert.ToString(dr["SpecialityName"]),
                        SpecialityShortName = Convert.ToString(dr["SpecialityShortName"]),
                        Description = Convert.ToString(dr["Description"]),
                        IsActive = Convert.ToBoolean(dr["IsActive"])

                    });

                }
            }
            catch (Exception ex)
            {

            }

            return specialities;
        }

        public bool DeleteSpeciality(int id, int userID)
        {
            try
            {
                using (SqlConnection connection = ADO.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_TB_SPECIALITY";
                    cmd.Parameters.AddWithValue("ACTION", 3);
                    cmd.Parameters.AddWithValue("@ID", id);
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