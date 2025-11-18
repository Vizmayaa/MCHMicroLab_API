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
    using Newtonsoft.Json;
    using System.Text;

    [RoutePrefix("api/reports")]
    public class ReportsController : ApiController
    {
        [HttpGet]
        public Response Test()
        {
            Response res = new Response();
            res.flag = "1";
            res.message = "Sucess";
            return res;
        }

        [HttpPost]
        [Route("parametervalues")]
        public ReportParameterValues GetReportParameterValues(ReportParameterInput vInput)
        {
            ReportParameterValues vReport = new ReportParameterValues();


            try
            {
                Reports_DAL dbhandle = new Reports_DAL();
                vReport = dbhandle.GetReportParameterValues(vInput.UserID, vInput.ReportID);


            }
            catch (Exception ex)
            {
                vReport.flag = "0";
                vReport.message = ex.Message;
            }
            return vReport;
        }

        [HttpPost]
        [Route("claimdetails")]
        public ClaimDetailReport ClaimDetails(ReportParameters vInput)
        {
            ClaimDetailReport rpt = new ClaimDetailReport();
            


            try
            {
                Reports_DAL dbhandle = new Reports_DAL();
                rpt = dbhandle.GetClaimDetails(vInput);
              
                 

            }
            catch (Exception ex)
            {
               
            }
            return rpt;

        }
        [HttpPost]
        [Route("claimdetailswithactivity")]
        public ClaimDetailWithActivity ClaimDetailsWithActivity(ReportParameters vInput)
        {
            ClaimDetailWithActivity rpt = new ClaimDetailWithActivity();



            try
            {
                Reports_DAL dbhandle = new Reports_DAL();
                rpt = dbhandle.GetClaimDetailsWithActivity(vInput);



            }
            catch (Exception ex)
            {

            }
            return rpt;

        }


    }
}