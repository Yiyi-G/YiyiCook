using Abp.AspNetCore.Mvc.Controllers;
using Abp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TgnetAbp.Api;
using TgnetAbp.Web;
using YiyiCook.Core;
using YiyiCook.Infrastruction.Exception;

namespace YiyiCook.Web.Controllers
{
    [DontWrapResult,ExceptionHandler]
    public abstract class YiyiCookControllerBase: AbpController
    {
        public string Authorize { get; set; }
        protected YiyiCookControllerBase()
        {
            LocalizationSourceName = YiyiCookConsts.LocalizationSourceName;
        }
        [NonAction]
        public string GetCurrentIP()
        {
            return this.Request.GetCurrentIP();
        }
        [NonAction]
        public ActionResult JsonApiResult(Result result)
        {
            var jsonResult = new JsonResult(result);
            jsonResult.SerializerSettings = new JsonSerializerSettings() { DateFormatString = "yyyy-MM-dd HH:mm:ss" };
            return jsonResult;
        }

        [NonAction]
        public ActionResult JsonApiResult<T>(Result<T> result)
        {
            var jsonResult = new JsonResult(result);
            jsonResult.SerializerSettings = new JsonSerializerSettings() { DateFormatString = "yyyy-MM-dd HH:mm:ss" };
            return jsonResult;
        }

        [NonAction]
        public ActionResult JsonApiResult(ErrorCode error, object jsonObject)
        {
            return JsonApiResult(new Result<object>
            {
                data = jsonObject,
                state_code = error.Code,
                message = error.Message
            });
        }
        [NonAction]
        public ActionResult JsonApiResult(object jsonObject)
        {
            return JsonApiResult(ErrorCode.None, jsonObject);
        }
        [NonAction]
        public ActionResult JsonApiResult(ErrorCode error)
        {
            return JsonApiResult(new Result() { state_code = error.Code, message = error.Message });
        }
    }
}