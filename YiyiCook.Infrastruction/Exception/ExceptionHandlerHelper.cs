using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Text;
using TgnetAbp.Api;


namespace YiyiCook.Infrastruction.Exception
{
    public static class ExceptionHandlerHelper
    {
        public static Result OnException(System.Exception exception, string requestInfo)
        {
            Result result;
            if (!ExceptionHandler.OnException(exception, out result))
            {
                var message = String.Empty;
                if (typeof(FaultException<>).IsInstanceOfType(exception))
                {
                    dynamic faultEx = exception;
                    message = faultEx.Detail.ToString();
                    exception = new System.Exception(message);
                }
                else
                {
                    message = exception.Message;
                }
                message = (message ?? String.Empty).Trim();
                if (message.Contains("参数错误"))
                {
                    result = new Result
                    {
                        state_code = ErrorCode.输入的数据格式错误.Code,
                        message = message
                    };
                }
                else
                {
                    LogException(requestInfo, exception);
                    //message = message.Length > 30 ? "网络异常，请重试" : message;
                    result = new Result
                    {
                        state_code = ErrorCode.服务器错误.Code,
                        message =message
                    };
                }
            }
            return result;
        }

        internal static void LogException(string additional, System.Exception ex)
        {
            try
            {
               // TgnetAbp.Log.LoggerResolver.Current.Error(additional, ex);
            }
            catch (System.Exception)
            {
            }
        }
    }
}
