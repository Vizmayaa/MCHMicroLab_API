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

    [RoutePrefix("api/wetfilm")]

    public class WetFilmController : ApiController
    {
    
        [HttpPost]
        [Route("insert")]
        public WetFilmResponse Insert(WetFilmMaster model)
        {

            WetFilmResponse res = new WetFilmResponse();
            try
            {
                WetFilm_DAL dbhandle = new WetFilm_DAL();
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
        public WetFilmResponse Update(WetFilmMaster model)
        {
            WetFilmResponse res = new WetFilmResponse();
            try
            {
                WetFilm_DAL dbhandle = new WetFilm_DAL();
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
        public WetFilmResponse GetById(int id)
        {

            WetFilmResponse res = new WetFilmResponse();
            try
            {
                WetFilm_DAL dbhandle = new WetFilm_DAL();
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
        public WetFilmResponse List()
        {

            WetFilmResponse res = new WetFilmResponse();
            try
            {
                WetFilm_DAL dbhandle = new WetFilm_DAL();
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
        public WetFilmResponse delete(int id)
        {
            WetFilmResponse res = new WetFilmResponse();
            try
            {
                WetFilm_DAL dbhandle = new WetFilm_DAL();
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