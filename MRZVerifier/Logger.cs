using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;
using System.Text;

namespace MRZVerifier
{
    public static class Logger
    {
        private static readonly ILog fileLogger = LogManager.GetLogger("FileLogger");
     

        public static void LogContent(string content)
        {
            content = string.Format("[Requester : {0}]{1}{2}", GetRequestIpAddress(), Environment.NewLine, content);
            fileLogger.Info(content);
        }

        public static void LogContent(Exception ex)
        {
            var content = string.Format("[Requester : {0}]{1}{2}", GetRequestIpAddress(), Environment.NewLine, FlattenException(ex));
            fileLogger.Info(content);
        }

        private static string FlattenException(Exception exception)
        {
            var stringBuilder = new StringBuilder();

            while (exception != null)
            {
                stringBuilder.AppendLine(exception.Message);
                stringBuilder.AppendLine(exception.StackTrace);

                exception = exception.InnerException;
            }

            return stringBuilder.ToString();
        }

        private static string GetRequestIpAddress()
        {
            String ip = string.Empty;
            if (HttpContext.Current != null && HttpContext.Current.Request != null)
            {
                ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (string.IsNullOrEmpty(ip))
                {
                    ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }
                else
                {
                    string[] addresses = ip.Split(',');
                    if (addresses.Length != 0)
                    {
                        ip = addresses[0];
                    }
                }

                ip = ip == "::1" ? "localhost" : ip;
            }
            return ip;
        }
    }
}