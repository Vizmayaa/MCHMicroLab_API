using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VEZTA.Controllers
{
    using Models;
    using DAL;
    using System.Web.Http;

    [RoutePrefix("api/dropdown")]

    public class DropDownController : ApiController
    {
        [HttpPost]
        public List<DropDown> ListData(DropDownInput vInput)
        {
            List<DropDown> vData = new List<DropDown>();
            try
            {
                DropDown_DAL dbhandle = new DropDown_DAL();
                vData = dbhandle.GetDropDownData(vInput.NAME);
            }
            catch (Exception ex)
            {

            }
            return vData.ToList();
        }
    }
}