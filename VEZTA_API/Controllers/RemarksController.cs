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

    [RoutePrefix("api/remarks")]

    public class RemarksController : ApiController
    {
    
        [HttpPost]
        [Route("insert")]
        public RemarksResponse Insert(RemarksClass remarks)
        {
            RemarksResponse res = new RemarksResponse();
            


            try
            {
                Remarks_DAL dbhandle = new Remarks_DAL();
                res =  dbhandle.Insert(remarks);

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
        public RemarksResponse Update(RemarksClass remarks)
        {
            RemarksResponse res = new RemarksResponse();

            try
            {
                Remarks_DAL dbhandle = new Remarks_DAL();
                res = dbhandle.Update(remarks);

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
        public RemarksClass select(int id)
        {
            RemarksClass res = new RemarksClass();
            RemarksResponse response = new RemarksResponse();
            try
            {
                Remarks_DAL dbhandle = new Remarks_DAL();
                res = dbhandle.GetRemarksById(id);
                if (res.REMARKS== null) {
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
        public RemarksResponse RemarksLogList()
        {

            RemarksResponse res = new RemarksResponse();
            try
            {
                Remarks_DAL _dbhandle = new Remarks_DAL();
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
        public RemarksResponse delete(int id)
        {

            RemarksResponse response = new RemarksResponse();
            try
            {
                Remarks_DAL dbhandle = new Remarks_DAL();
                response = dbhandle.DeleteRemarksData(id);
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