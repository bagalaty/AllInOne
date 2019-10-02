using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Core.Logger
{
    public class LoggerManager
    {
        private static readonly object IsLogFileInUse = new object();

        public static void LogRequest(StringBuilder request)
        {
            Debug.Write(request.ToString());
            bool traceRequests;
            bool.TryParse(Utilities.GetConfig_Section().TraceRequests, out traceRequests);
            if (traceRequests)
            {
                var directoryPath = "errors/";
                var filePath = directoryPath + DateTime.Now.Year + "-" + DateTime.Now.Month.ToString("00") + "-" + DateTime.Now.Day.ToString("00") + "_" + DateTime.Now.Hour.ToString("00") + "_Trace.txt";

                if (!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);
                if (!File.Exists(filePath))
                {
                    var stream = File.Create(filePath);
                    stream.Dispose();
                }


                lock (IsLogFileInUse)
                {
                    File.AppendAllText(filePath, request.ToString());
                }
            }
        }
        public static void LogException(Exception ex, string data = null)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("DateTime: " + DateTime.Now.ToLongTimeString());
            if (string.IsNullOrEmpty(data))
                builder.AppendLine($"Data = {data}");
            builder.AppendLine("Message: " + ex.Message);
            builder.AppendLine("Stack Trace:" + ex.StackTrace);

            while (ex.InnerException != null)
            {
                builder.AppendLine("Inner Exception:");
                builder.AppendLine("Message: " + ex.Message);
                builder.AppendLine("Stack Trace:" + ex.StackTrace);
                ex = ex.InnerException;
            }
            var directoryPath = "errors/";
            var filePath = directoryPath + DateTime.Now.Year + "-" + DateTime.Now.Month.ToString("00") + "-" + DateTime.Now.Day.ToString("00") + "_" + DateTime.Now.Hour.ToString("00") + "_Errors.txt";

            if (!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);
            if (!File.Exists(filePath))
            {
                var stream = File.Create(filePath);
                stream.Dispose();
            }


            lock (IsLogFileInUse)
            {
                File.AppendAllText(filePath, builder.ToString());
            }
        }        
    }
}
