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

    [RoutePrefix("api/userlab")]

    public class UserLabController : ApiController
    {
        [HttpPost]
        [Route("login")]
        public UserLabLoginResponse VerifyLogin(UserLabVerificationInput vLoginInput)
        {
            UserLabLoginResponse res = new UserLabLoginResponse();
            try
            {
                UserLab_DAL dbhandle = new UserLab_DAL();
                res = dbhandle.VerifyLogin(vLoginInput);
            }
            catch (Exception ex)
            {

            }

            return res;
        }

        [HttpPost]
        [Route("list")]
        public UserLabLoginResponse List()
        {
            UserLabLoginResponse res = new UserLabLoginResponse();
            try
            {
                UserLab_DAL dbhandle = new UserLab_DAL();
                var userList = dbhandle.GetAllUsers();
                res.flag = 1;
                res.message = "Success";
                res.data = userList; 
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.message = ex.Message;
                res.data = new List<UserLab>(); 
            }
            return res;
        }

        [HttpPost]
        [Route("insert")]
        public UserLabInsertResponse Insert(UserLabInsertInput user)
        {
            UserLabInsertResponse res = new UserLabInsertResponse();



            try
            {
                UserLab_DAL dbhandle = new UserLab_DAL();
                res = dbhandle.Insert(user);

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
        public UserLabInsertResponse Update(UserLabInsertInput user)
        {
            UserLabInsertResponse res = new UserLabInsertResponse();



            try
            {
                UserLab_DAL dbhandle = new UserLab_DAL();
                res = dbhandle.Update(user);

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
        public UserLabSelectResponse select(int id)
        {
            UserLabSelectResponse res = new UserLabSelectResponse();
            try
            {
                UserLab_DAL dbhandle = new UserLab_DAL();
                res = dbhandle.GetUserLabById(id);
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
        public UserLabInsertResponse delete(int id)
        {
            UserLabInsertResponse response = new UserLabInsertResponse();
            try
            {
                UserLab_DAL dbhandle = new UserLab_DAL();
                response = dbhandle.Delete(id);
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