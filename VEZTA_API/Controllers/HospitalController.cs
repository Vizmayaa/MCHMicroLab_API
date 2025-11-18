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
    using DAL;
    using System.Web.Http.Cors;

    [RoutePrefix("api/hospital")]

    public class HospitalController : ApiController
    {
    
        [HttpPost]
        [Route("insert")]
        public HospitalResponse Insert(HospitalClass hospital)
        {
            HospitalResponse res = new HospitalResponse();
            


            try
            {
                Hospital_DAL dbhandle = new Hospital_DAL();
                res =  dbhandle.Insert(hospital);

            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
                
            }

            return res;
        }
   

        [HttpPost]
        [Route("update")]
        public HospitalResponse Update(HospitalClass collection)
        {
            HospitalResponse res = new HospitalResponse();

            try
            {
                Hospital_DAL dbhandle = new Hospital_DAL();
                res = dbhandle.Update(collection);

            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;

            }

            return res;
        }

        [HttpPost]
        [Route("select/{id:int}")]
        public HospitalClass select(int id)
        {
            HospitalClass res = new HospitalClass();
            HospitalResponse response = new HospitalResponse();
            try
            {
                Hospital_DAL dbhandle = new Hospital_DAL();
                res = dbhandle.GetHospitalById(id);
                if (res.HOSPITAL== null) {
                response.flag = 0;
                response.Message = "No Data found";
                }
                else
                {
                    
                }
            }
            catch (Exception ex)
            {
                response.flag = 0;
                response.Message = ex.Message;

            }
            return res;

        }



        [HttpPost]
        [Route("list")]
        public HospitalResponse HospitalLogList()
        {
   
            HospitalResponse res = new HospitalResponse();
            try
            {
                Hospital_DAL _dbhandle = new Hospital_DAL();
                res = _dbhandle.GetLogList();
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

        [HttpPost]
        [Route("delete/{id:int}")]
        public HospitalResponse delete(int id)
        {

            HospitalResponse response = new HospitalResponse();
            try
            {
                Hospital_DAL dbhandle = new Hospital_DAL();
                response = dbhandle.DeleteHospitalData(id);
            }
            catch (Exception ex)
            {
                response.flag = 0;
                response.Message = ex.Message;

            }
            return response;

        }




    }
}