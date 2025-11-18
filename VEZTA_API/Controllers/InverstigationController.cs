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

    [RoutePrefix("api/investigation")]

    public class InvestigationController : ApiController
    {
    
        [HttpPost]
        [Route("insert")]
        public InvestigationResponse Insert(InvestigationClass investigation)
        {
            InvestigationResponse res = new InvestigationResponse();
            
            try
            {
                Investigation_DAL dbhandle = new Investigation_DAL();
                res =  dbhandle.Insert(investigation);

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
        public InvestigationResponse Update(InvestigationClass investigation)
        {
            InvestigationResponse res = new InvestigationResponse();

            try
            {
                Investigation_DAL dbhandle = new Investigation_DAL();
                res = dbhandle.Update(investigation);

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
        public InvestigationClass select(int id)
        {
            InvestigationClass res = new InvestigationClass();
            InvestigationResponse response = new InvestigationResponse();
            try
            {
                Investigation_DAL dbhandle = new Investigation_DAL();
                res = dbhandle.GetInvestigationById(id);
                if (res.INVESTIGATION== null) {
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
        public InvestigationResponse InvestigationLogList()
        {
   
            InvestigationResponse res = new InvestigationResponse();
            try
            {
                Investigation_DAL _dbhandle = new Investigation_DAL();
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
        public InvestigationResponse delete(int id)
        {

            InvestigationResponse response = new InvestigationResponse();
            try
            {
                Investigation_DAL dbhandle = new Investigation_DAL();
                response = dbhandle.DeleteInvestigationData(id);
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