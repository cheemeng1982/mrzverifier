using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Net.Http;
using System.Web.Mvc;
using Newtonsoft.Json;
using PassportValidator;
using System.Json;

namespace MRZVerifier.Controllers
{
    public class IndexController : Controller
    {
        [HttpPost]
        [ActionName("CheckPassportData")]
        public JsonResult CheckPassportData(string inputString)
        {
            PassportValidationResult validationResult = null;
            try
            {
                var pi = JsonConvert.DeserializeObject<PassportInput>(HttpUtility.UrlDecode(inputString));

                PassportManager pm = new PassportManager();
                validationResult = pm.ValidatePassportData(pi);
            }
            catch(PassportDataException ex)
            {
                return Json(JsonResponseFactory.ErrorResponse(ex.Message));
            }
            catch(Exception ex)
            {
                return Json(JsonResponseFactory.ErrorResponse(string.Format("{0}  |  {1}", ex.Message, ex.StackTrace)));
            }

            return Json(JsonResponseFactory.SuccessResponse(JsonConvert.SerializeObject(validationResult)));
        }
    }
}
