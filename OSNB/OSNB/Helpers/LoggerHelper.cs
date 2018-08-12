using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OSNB.Models;
using OSNB.ViewModels;

namespace OSNB.Helpers
{
    public class LoggerHelper
    {
        private AppDbContext _db = new AppDbContext();

        public void LoggerError()
        {
            System.Exception ex = System.Web.HttpContext.Current.Server.GetLastError();
            LoggerError(ex);
        }

        public void LoggerError(Exception ex)
        {
            var currentContext = HttpContext.Current;

            string logSummery, logDetails, logFilePath, logUrl, filePath = "No file path found.", url = "No url found to be reported.";

            if (currentContext != null)
            {
                filePath = currentContext.Request.FilePath;
                url = currentContext.Request.Url.AbsoluteUri;
            }
            
        }
    }
}