extern alias HansenCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
using AQSOwnerCheckIn.Extensions;
using AQSOwnerCheckIn.Models;
using Hansen.Core;
using Hansen.Core.AccessControl;
using Hansen.Core.Security.WebServices;
using Hansen.Core.WebServices.Proxy.Client;
using Hansen.Core.WebServiceUtilities;
using Hansen.DynamicPortal;
using Hansen.Resources;
using log4net;
using Hansen.Core.AccessControl.WebServices;
using HansenCore::Hansen.Core.Security;

namespace AQSOwnerCheckIn.Services
{
    public class AuthenticationService
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(AuthenticationService));

        // Login method for IPS User accounts.
        public static async Task<Response> LoginIpsUser(Credentials credentials)
        {
            Logger.Info("Method called.");

            var service = new AccessUserService {Ticket = InforConfig.Ticket};
            var user = new AccessUser {UserName = credentials.Username};

            try
            {
                var ticket = "";

                WebServiceResult res = service.LoadByID(ref user);

                if (res.HasFailed)
                {
                    Logger.Warn(string.Format("Incorrect username specified for login attempt: {0}", credentials.Username));
                    return Response.Failure("Failed to authenticate.");
                }

                bool passwordsMatch = false;
                res = service.ComparePassword(ref user, credentials.Password, out passwordsMatch);

                if (res.HasFailed || !passwordsMatch)
                {
                    Logger.Warn(string.Format("Incorrect password specified for login attempt: {0}", credentials.Username));
                    return Response.Failure("Failed to authenticate.");
                }

                // Set authorization code to password for logging user in.
                user.AuthorizationCode = credentials.Password;

                res = service.GetTicket(ref user, out ticket);

                if (res.HasFailed)
                {
                    Logger.Warn(string.Format("Unable to get ticket for user: {0}", credentials.Username));
                    return Response.Failure("Failed to authenticate.");
                }

                // Login was a success
                Logger.Info(string.Format("IPS User {0} logged in successfully.", credentials.Username));

                var userSession = new UserSession();

                userSession.Username = credentials.Username;
                userSession.Ticket = ticket;
                userSession.IpsUserKey = user.UserKey;

                HttpContext.Current.Session.Add("user", userSession);

                var data = new
                {
                    username = credentials.Username,
                    access = userSession.Access,
                    defaultState = userSession.DefaultState
                };

                return Response.Success(data);
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
            }

            return Response.Failure("Failed to authenticate.");
        }
    }
}