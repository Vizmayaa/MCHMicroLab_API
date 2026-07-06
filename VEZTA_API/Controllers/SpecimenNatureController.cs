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

    [RoutePrefix("api/SpecimenNature")]

    public class SpecimenNatureController : ApiController
    {
    
        [HttpPost]
        [Route("insert")]
        public SpecimenNatureResponse Insert(SpecimenNature model)
        {

            SpecimenNatureResponse res = new SpecimenNatureResponse();
            try
            {
                SpecimenNature_DAL dbhandle = new SpecimenNature_DAL();
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
        public SpecimenNatureResponse Update(SpecimenNature model)
        {
            SpecimenNatureResponse res = new SpecimenNatureResponse();
            try
            {
                SpecimenNature_DAL dbhandle = new SpecimenNature_DAL();
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
        public SpecimenNatureResponse Select(int id)
        {
            SpecimenNatureResponse res = new SpecimenNatureResponse();
            try
            {
                SpecimenNature_DAL dbhandle = new SpecimenNature_DAL();

                SpecimenNature item = dbhandle.GetById(id);   

                res.Data = new List<SpecimenNature>();        
                if (item != null)
                    res.Data.Add(item);                      

                res.flag = 1;
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
        public SpecimenNatureResponse List()
        {
            SpecimenNatureResponse res = new SpecimenNatureResponse();
            try
            {
                SpecimenNature_DAL dbhandle = new SpecimenNature_DAL();
                res.Data = dbhandle.GetAll();
                res.flag = 1;
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
        public SpecimenNatureResponse Delete(int id)
        {
            SpecimenNatureResponse res = new SpecimenNatureResponse();
            try
            {
                SpecimenNature_DAL dbhandle = new SpecimenNature_DAL();
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