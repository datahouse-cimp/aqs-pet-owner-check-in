using System;
using Hansen.Core;
using Hansen.Core.WebServiceUtilities;

namespace AQSOwnerCheckIn.Models
{
    public class TaskResult
    {
        public WebServiceResult Result { get; set; }
        public object Data { get; set; }

        public static TaskResult Failure(string message, string stackTrace)
        {
            var res = new WebServiceResult()
            {
                Date = DateTime.Now,
                HasFailed = true,
                IsNoDataFound = false,
                IsSuccess = false,
                Message = message,
                Severity = ResultSeverity.Error,
                Stack = stackTrace
            };

            return new TaskResult() {Data = null, Result = res};
        }

        public static TaskResult Failure(string message)
        {
            return Failure(message, "Stack trace unavailable.");
        }

        public static TaskResult Warning(object data, string message)
        {
            var res = new WebServiceResult()
            {
                Date = DateTime.Now,
                HasFailed = false,
                IsNoDataFound = false,
                IsSuccess = false,
                Message = message,
                Severity = ResultSeverity.Warning,
                Stack = ""
            };

            return new TaskResult() {Data = data, Result = res};
        }

        public static TaskResult Warning(string message)
        {
            return Warning(null, message);
        }

        public static TaskResult Success(object data)
        {
            var res = new WebServiceResult()
            {
                Date = DateTime.Now,
                HasFailed = false,
                IsNoDataFound = false,
                IsSuccess = true,
                Message = "",
                Severity = ResultSeverity.Success,
                Stack = ""
            };

            return new TaskResult() { Data = data, Result = res };
        }

        public static TaskResult Success()
        {
            return Success(null);
        }

        public static TaskResult FromResult(WebServiceResult res)
        {
            return new TaskResult() {Data = null, Result = res};
        }
    }
}