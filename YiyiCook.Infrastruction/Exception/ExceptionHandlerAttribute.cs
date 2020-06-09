using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace YiyiCook.Infrastruction.Exception
{
    public class ExceptionHandlerAttribute : ExceptionFilterAttribute
    {
        /// <summary>
        /// 异常处理
        /// </summary>
        public override void OnException(ExceptionContext context)
        {
            var exInfo = new StringBuilder();
            if (context.HttpContext.Request.QueryString != null)
            {
                exInfo.AppendLine("QueryString：");
                exInfo.AppendLine(context.HttpContext.Request.QueryString.ToString());
            }
            //if (context.HttpContext.Request.Form != null)
            //{
            //    exInfo.AppendLine("Form：");
            //    foreach (var key in context.HttpContext.Request.Form.Keys)
            //    {
            //        exInfo.AppendLine(key + ":" + context.HttpContext.Request.Form[key]);
            //    }
            //}
            var result = ExceptionHandlerHelper.OnException(context.Exception, exInfo.ToString());
            context.ExceptionHandled = true;
            context.HttpContext.Response.StatusCode = 200;
            context.Result = new JsonResult(result);
        }
    }
}
