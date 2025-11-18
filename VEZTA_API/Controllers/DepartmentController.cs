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

    [RoutePrefix("api/department")]

    public class DepartmentController : ApiController
    {
    
        [HttpPost]
        [Route("insert")]
        public DepartmentResponse Insert(DepartmentClass department)
        {
            DepartmentResponse res = new DepartmentResponse();
            


            try
            {
                Department_DAL dbhandle = new Department_DAL();
                res =  dbhandle.Insert(department);

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
        public DepartmentResponse Update(DepartmentClass depInput)
        {
            DepartmentResponse res = new DepartmentResponse();

            try
            {
                Department_DAL dbhandle = new Department_DAL();
                res = dbhandle.Update(depInput);

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
        public DepartmentClass select(int id)
        {
            DepartmentClass res = new DepartmentClass();
            DepartmentResponse response = new DepartmentResponse();
            try
            {
                Department_DAL dbhandle = new Department_DAL();
                res = dbhandle.GetDepartmentById(id);
                if (res.DEPARTMENT== null) {
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
        public DepartmentResponse DepartmentLogList()
        {

            DepartmentResponse res = new DepartmentResponse();
            try
            {
                Department_DAL _dbhandle = new Department_DAL();
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
        public DepartmentResponse delete(int id)
        {
            DepartmentResponse response = new DepartmentResponse();
            try
            {
                Department_DAL dbhandle = new Department_DAL();
                response = dbhandle.DeleteDepartmentData(id);
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