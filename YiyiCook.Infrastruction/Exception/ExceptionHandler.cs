using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using TgnetAbp.Api;

namespace YiyiCook.Infrastruction.Exception
{
    public static class ExceptionHandler
    {
        public static Result OnException(string additional, System.Exception exception)
        {
            Result result;
            if(!OnException(exception, out result))
            {
                var ex = exception;
                var message = ex.Message;
                if (typeof(FaultException<>).IsInstanceOfType(exception))
                {
                    dynamic faultEx = exception;
                    ex = new System.Exception(message = faultEx.Detail.ToString());
                }
                LogException(additional, ex);
                result = new Result
                {
                    state_code = ErrorCode.服务器错误.Code,
                    message = message
                };
            }
            return result;
        }

        public static bool OnException(System.Exception exception, out Result result)
        {
            result = null;
            var error = ErrorCode.服务器错误.Code;
            var message = exception.Message;
            if (exception is FaultException<ErrorResponseType>)
            {
                var innerError = exception as FaultException<ErrorResponseType>;
                if (innerError.Detail == ErrorResponseType.invalid_grant)
                {
                    error = ErrorCode.未登录.Code;
                }
                message = innerError.Detail.ToString();
            }
            else if (exception is FaultException<ErrorCode>)
            {
                var innerError = exception as FaultException<ErrorCode>;
                error = innerError.Detail.Code;
                message = innerError.Detail.Message;
            }
            else if (exception is ExceptionWithErrorCode)
            {
                error = ((ExceptionWithErrorCode)exception).ErrorCode.Code;
                message = exception.Message;
            }
            else if (exception is ArgumentException)
            {
                error = ErrorCode.输入的数据格式错误.Code;
                message = exception.Message;
            }
            else
            {
                return false;
            }
            result = new Result
            {
                state_code = error,
                message = message,
            };
            return true;
        }

        internal static void LogException(string additional, System.Exception ex)
        {
            try
            {
                //TgnetAbp.Log.LoggerResolver.Current .Error(Newtonsoft.Json.JsonConvert.SerializeObject(ex));
            }
            catch (System.Exception)
            {
            }
        }
    }
}
