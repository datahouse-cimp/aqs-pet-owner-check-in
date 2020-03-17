using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using AQSOwnerCheckIn.Extensions;
using AQSOwnerCheckIn.Models;
using AQSOwnerCheckIn.Services;
using Hansen.CDR.Use;
using log4net;
using log4net.Config;
using Newtonsoft.Json;
using ILog = log4net.ILog;

namespace AQSOwnerCheckIn.Controllers
{
    public class SampleController : ApiController
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(SampleController));

        public class SearchCriteria
        {
            [JsonProperty(PropertyName = "criteria")]
            public string Criteria { get; set; }
        }

        [HttpGet]
        [ActionName("ContactSearch")]
        public async Task<Response> Search([FromUri] SearchCriteria s)
        {
            Logger.Debug("Method called.");
            var user = UserSession.GetCurrent();

            //if (user.IsValid())
            {
                Logger.Info(string.Format("Called by ({0},{1})", user.Username, user.IpsUserKey));

                var taskResult = await SampleService.SampleQuery(s);

                if (taskResult.Result.HasFailed)
                {
                    Logger.Error(string.Format("({0},{1}) [{2}] {3} Stack: {4}", user.Username, user.IpsUserKey, taskResult.Result.Code, taskResult.Result.Message, taskResult.Result.Stack));
                    Logger.Info("200 Success response sent with failure message.");
                    return Response.Failure(taskResult.Result.Message);
                }

                Logger.Info("200 Success response sent.");
                return Response.Success(taskResult.Data);
            }

            Logger.Info("401 Unauthorized response sent.");
            throw new HttpResponseException(HttpStatusCode.Unauthorized);
        }
    }
}