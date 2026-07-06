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

    [RoutePrefix("api/GramStain")]

    public class GramStainController : ApiController
    {
    
        [HttpPost]
        [Route("insert")]
        public GramStainResponse Insert(GramStainMaster model)
        {

            GramStainResponse res = new GramStainResponse();
            try
            {
                GramStain_DAL dbhandle = new GramStain_DAL();
                res =  dbhandle.Insert(model);

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
        public GramStainResponse Update(GramStainMaster model)
        {
            GramStainResponse res = new GramStainResponse();
            try
            {
                GramStain_DAL dbhandle = new GramStain_DAL();
                res = dbhandle.Update(model);

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
        public GramStainResponse GetById(int id)
        {

            GramStainResponse res = new GramStainResponse();
            try
            {
                GramStain_DAL dbhandle = new GramStain_DAL();
                res = dbhandle.GetById(id);
                
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;

            }
            return res;

        }



        [HttpPost]
        [Route("list")]
        public GramStainResponse List()
        {

            GramStainResponse res = new GramStainResponse();
            try
            {
                GramStain_DAL dbhandle = new GramStain_DAL();
                res = dbhandle.GetWetFilmResultList();

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
        public GramStainResponse delete(int id)
        {
            GramStainResponse res = new GramStainResponse();
            try
            {
                GramStain_DAL dbhandle = new GramStain_DAL();
                res = dbhandle.Delete(id);

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