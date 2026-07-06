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
    public class SpecimenNature_DAL
    {

        public SpecimenNatureResponse Insert(SpecimenNature model)
        {
            SpecimenNatureResponse res = new SpecimenNatureResponse();
            SqlConnection connection = new SqlConnection();

            try
            {
                connection = ADO.GetConnection();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_SPECIMEN_NATURE";

                cmd.Parameters.AddWithValue("@ACTION", 1);
                cmd.Parameters.AddWithValue("@ID", 0);
                cmd.Parameters.AddWithValue("@SPECIMEN_NAME", model.SPECIMEN_NAME);

                cmd.ExecuteNonQuery();

                res.flag = 1;
                res.Message = "Specimen Nature inserted successfully";
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }

            return res;
        }
        public SpecimenNatureResponse Update(SpecimenNature model)
        {
            SpecimenNatureResponse res = new SpecimenNatureResponse();
            SqlConnection connection = new SqlConnection();

            try
            {
                connection = ADO.GetConnection();
                SqlCommand cmd = new SqlCommand("SP_TB_SPECIMEN_NATURE", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ACTION", 2);
                cmd.Parameters.AddWithValue("@ID", model.ID);
                cmd.Parameters.AddWithValue("@SPECIMEN_NAME", model.SPECIMEN_NAME);

                cmd.ExecuteNonQuery();

                res.flag = 1;
                res.Message = "Specimen Nature updated successfully";
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
            return res;
        }
        public SpecimenNatureResponse Delete(int id)
        {
            SpecimenNatureResponse res = new SpecimenNatureResponse();
            SqlConnection connection = new SqlConnection();

            try
            {
                connection = ADO.GetConnection();
                SqlCommand cmd = new SqlCommand("SP_TB_SPECIMEN_NATURE", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ACTION", 3);
                cmd.Parameters.AddWithValue("@ID", id);

                cmd.ExecuteNonQuery();

                res.flag = 1;
                res.Message = "Specimen Nature deleted successfully";
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
            return res;
        }
        public List<SpecimenNature> GetAll()
        {
            List<SpecimenNature> list = new List<SpecimenNature>();
            SqlConnection connection = ADO.GetConnection();

            try
            {
                SqlCommand cmd = new SqlCommand("SP_TB_SPECIMEN_NATURE", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ACTION", 0);
                cmd.Parameters.AddWithValue("@ID", 0);

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    list.Add(new SpecimenNature
                    {
                        ID = Convert.ToInt32(dr["ID"]),
                        SPECIMEN_NAME = dr["SPECIMEN_NAME"].ToString()
                    });
                }
                dr.Close();
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }

            return list;
        }
        public SpecimenNature GetById(int id)
        {
            SpecimenNature model = new SpecimenNature();
            SqlConnection connection = ADO.GetConnection();

            try
            {
                SqlCommand cmd = new SqlCommand("SP_TB_SPECIMEN_NATURE", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ACTION", 4);
                cmd.Parameters.AddWithValue("@ID", id);

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    model.ID = Convert.ToInt32(dr["ID"]);
                    model.SPECIMEN_NAME = dr["SPECIMEN_NAME"].ToString();
                }
                dr.Close();
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }

            return model;
        }


    }
}