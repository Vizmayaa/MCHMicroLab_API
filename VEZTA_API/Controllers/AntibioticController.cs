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

    [RoutePrefix("api/antibiotic")]

    public class AntibioticController : ApiController
    {
    
        [HttpPost]
        [Route("insert")]
        public AntibioticResponse Insert(AntibioticClass vInput)
        {

            AntibioticResponse res = new AntibioticResponse();
            try
            {
                Antibiotic_DAL dbhandle = new Antibiotic_DAL();
                res =  dbhandle.Insert(vInput);

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
        public AntibioticResponse Update(AntibioticClass vInput)
        {
            AntibioticResponse res = new AntibioticResponse();
            try
            {
                Antibiotic_DAL dbhandle = new Antibiotic_DAL();
                res = dbhandle.Update(vInput);

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
        public AntibioticResponse select(int id)
        {

            AntibioticResponse res = new AntibioticResponse();
            try
            {
                Antibiotic_DAL dbhandle = new Antibiotic_DAL();
                res = dbhandle.GetItemsById(id);
                
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
        public AntibioticResponse List()
        {

            AntibioticResponse res = new AntibioticResponse();
            try
            {
                Antibiotic_DAL dbhandle = new Antibiotic_DAL();
                res = dbhandle.GetList();

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
        public AntibioticResponse delete(int id)
        {
            AntibioticResponse res = new AntibioticResponse();
            try
            {
                Antibiotic_DAL dbhandle = new Antibiotic_DAL();
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