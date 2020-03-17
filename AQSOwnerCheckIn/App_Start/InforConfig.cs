using System;
using System.Configuration;
using System.Web.Configuration;
using Hansen.Core.Security.WebServices;
using Hansen.Core.WebServices.Proxy.Client;
using Hansen.Core.WebServiceUtilities;
using log4net;
using log4net.Config;
using System.Data.SqlClient;
using System.Collections.Generic;
using AQSOwnerCheckIn.Services;

namespace AQSOwnerCheckIn
{
    public class InforConfig
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(InforConfig));

        public static string Ticket = "";
        public static string IpsDatabaseConnectionString = "";

        public static void LoadConfigs()
        {
            Logger.Debug("Method called.");
            IpsDatabaseConnectionString = ConfigurationManager.ConnectionStrings["IPSConnection"].ConnectionString;
        }

        public static void Register()
        {
            Logger.Debug("Method called.");
            string baseUri = WebConfigurationManager.AppSettings["WebServiceBaseURI"];
            string webServiceProvider = WebConfigurationManager.AppSettings["WebServiceProvider"];
            string ipsUsername = WebConfigurationManager.AppSettings["IPSUsername"];
            string ipsPassword = WebConfigurationManager.AppSettings["IPSPassword"];

            WebServiceProxyBase.BaseUri = baseUri;

            var loginService = new LoginService();
            var res = new WebServiceResult();

            try
            {
                res = loginService.ServiceLogin(webServiceProvider, ipsUsername, ipsPassword, ref Ticket);
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
                throw;
            }

            if (!res.IsSuccess)
            {
                var message = string.Format("Unable to get Infor Ticket from WebService. Received the following error: {0}", res.Message);
                Logger.Error(message);
                throw new Exception(message);
            }
        }
    }
}