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
    public class WetFilm_DAL
    {
        public WetFilmResponse Insert(WetFilmMaster model)
        {
            WetFilmResponse res = new WetFilmResponse();
            SqlConnection connection = new SqlConnection();

            try
            {
                connection = ADO.GetConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_WET_FILM_RESULT";

                cmd.Parameters.AddWithValue("@ACTION", 1);
                cmd.Parameters.AddWithValue("@DESCRIPTION", model.DESCRIPTION);

                cmd.ExecuteNonQuery();

                res.flag = 1;
                res.Message = "Inserted Successfully";
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
        public WetFilmResponse Update(WetFilmMaster model)
        {
            WetFilmResponse res = new WetFilmResponse();
            SqlConnection connection = new SqlConnection();

            try
            {
                connection = ADO.GetConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_WET_FILM_RESULT";

                cmd.Parameters.AddWithValue("@ACTION", 2);
                cmd.Parameters.AddWithValue("@ID", model.ID);
                cmd.Parameters.AddWithValue("@DESCRIPTION", model.DESCRIPTION);

                cmd.ExecuteNonQuery();

                res.flag = 1;
                res.Message = "Updated Successfully";
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

        public WetFilmResponse Delete(int id)
        {
            WetFilmResponse res = new WetFilmResponse();
            SqlConnection connection = new SqlConnection();

            try
            {
                connection = ADO.GetConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_WET_FILM_RESULT";

                cmd.Parameters.AddWithValue("@ACTION", 3);
                cmd.Parameters.AddWithValue("@ID", id);

                cmd.ExecuteNonQuery();

                res.flag = 1;
                res.Message = "Deleted Successfully";
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

        public WetFilmResponse GetById(int id)
        {
            WetFilmResponse res = new WetFilmResponse();
            res.Data = new List<WetFilmMaster>();
            SqlConnection connection = new SqlConnection();

            try
            {
                connection = ADO.GetConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_WET_FILM_RESULT";

                cmd.Parameters.AddWithValue("@ACTION", 0);
                cmd.Parameters.AddWithValue("@ID", id);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    WetFilmMaster obj = new WetFilmMaster();
                    obj.ID = Convert.ToInt32(reader["ID"]);
                    obj.DESCRIPTION = reader["DESCRIPTION"].ToString();

                    res.Data.Add(obj);
                }

                res.flag = 1;
                res.Message = "Success";
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
        public WetFilmResponse GetWetFilmResultList()
        {
            WetFilmResponse res = new WetFilmResponse();
            res.Data = new List<WetFilmMaster>();
            SqlConnection connection = new SqlConnection();

            try
            {
                connection = ADO.GetConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_TB_WET_FILM_RESULT";

                cmd.Parameters.AddWithValue("@ACTION", 0);
                cmd.Parameters.AddWithValue("@ID", 0);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    WetFilmMaster obj = new WetFilmMaster();
                    obj.ID = Convert.ToInt32(reader["ID"]);
                    obj.DESCRIPTION = reader["DESCRIPTION"].ToString();

                    res.Data.Add(obj);
                }

                res.flag = 1;
                res.Message = "Success";
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






    }
}

