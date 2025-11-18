using System;
using System.Collections.Generic;
using System.Web.Http;
using VEZTA.Models;
using VEZTA.DAL;

namespace VEZTA.Controllers
{

    [RoutePrefix("api/report")]
    public class ReportController : ApiController
    {
        [HttpPost]
        [Route("initData")]
        public ReportListData InitData()
        {
            ReportListData res = new ReportListData();
            try
            {
                Report_DAL dbhandle = new Report_DAL();
                res = dbhandle.GetReportInitData();
                res.flag = 1;
                res.message = "Success";

            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.message = ex.Message;
            }

            return res;
        }

        [HttpPost]
        [Route("insert")]
        public SaveReportResponse SaveReport(Report report )
        {
            SaveReportResponse res = new SaveReportResponse();



            try
            {
                Report_DAL dbhandle = new Report_DAL();
                res = dbhandle.Insert(report);
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
        [Route("select/{id:int}")]
        public Report select(int id)
        {
            Report res = new Report();
            try
            {
                Report_DAL dbhandle = new Report_DAL();
                res = dbhandle.GetReportById(id);
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
        [Route("update")]
        public SaveReportResponse Update(Report Data)
        {
            SaveReportResponse res = new SaveReportResponse();

            try
            {
                Report_DAL dbhandle = new Report_DAL();
                res=dbhandle.Update(Data);
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

    }
}
