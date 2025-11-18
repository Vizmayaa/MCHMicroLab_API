using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MXERPService.Controllers
{
    using Models;
    public class verifyStockController : ApiController
    {
        public StockResponse verifyStock(Stock varStock)
        {
            StockResponse varRes = new StockResponse();             
            try
            {
                string varAuthKey = ActionContext.Request.Headers.Authorization.Parameter.ToString();

                if (varAuthKey == null)
                {
                    varRes.flag = "401";
                    varRes.message = "Unauthorized";                    
                    return varRes;
                }

                if (varAuthKey.ToString() != "")
                {
                    var DecodeToken = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(varAuthKey));
                    var arrUserNameandPassword = DecodeToken.Split(':');
                    if (arrUserNameandPassword[0] != "hexeam" || arrUserNameandPassword[1] != "P@$$w0rd.1")
                    {
                        varRes.flag = "401";
                        varRes.message = "Unauthorized";                        
                        return varRes;
                    }
                }
            }
            catch (Exception ex)
            {
                varRes.flag = "401";
                varRes.message = "Unauthorized";                
                return varRes;
            }

            try
            {
                varRes = StockManagement.getInstance().stockVerification(varStock);                 
            }
            catch (Exception ex)
            {
                varRes.flag = "0";
                varRes.message = ex.Message;                 
            }

            return varRes;
        }
         
    }
}
