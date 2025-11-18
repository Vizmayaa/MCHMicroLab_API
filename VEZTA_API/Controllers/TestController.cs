using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.SqlClient;
using System.Configuration;
namespace VEZTA.Controllers
{
    using Models;
    public class TestController : ApiController
    {

        [HttpGet]
        public Response testAPI()
        {
            Response varRes = new Response();
            try
            {
                

                SqlConnection objCon = new SqlConnection();
                objCon = DAL.ADO.GetConnection();
                  
                if (objCon.State == System.Data.ConnectionState.Open)
                {
                    varRes.flag = "1";
                    varRes.message = "Success";
                }                
                else
                {
                    varRes.flag = "0";
                    varRes.message = "failed";
                }
                objCon.Close();
            }
            catch (Exception ex)
            {
                varRes.flag = "0";
                varRes.message = "failed";
            }

            return varRes;
        }
    }
}
