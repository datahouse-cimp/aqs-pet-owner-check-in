using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hansen.Core;
using Hansen.Core.WebServiceUtilities;

namespace AQSOwnerCheckIn.Extensions
{
    public static class WebServiceResultExtension
    {
        public static WebServiceResult FromException(this WebServiceResult webServiceResult, Exception e)
        {
            return new WebServiceResult
            {
                Code = 0,
                Date = DateTime.Now,
                HasFailed = true,
                IsSuccess = false,
                Message = e.Message,
                IsNoDataFound = false,
                Reference = "",
                Severity = ResultSeverity.Fault,
                Stack = e.StackTrace
            };
        }
    }
}