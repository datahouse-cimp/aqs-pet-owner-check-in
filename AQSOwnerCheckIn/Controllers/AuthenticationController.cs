using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.SessionState;
using AQSOwnerCheckIn.Models;
using AQSOwnerCheckIn.Services;
using log4net;
using log4net.Config;

namespace AQSOwnerCheckIn.Controllers
{
    public class AuthenticationController : ApiController
    {
        // Authentication methods
        private static readonly ILog Logger = LogManager.GetLogger(typeof(AuthenticationController));
        
        [HttpPost]
        [ActionName("Login")]
        // Log in user using credentials
        public async Task<Response> Login([FromBody] Credentials credentials)
        {
            Logger.Debug("Method called.");
            Logger.Info(string.Format("Login attempt made by username: {0}.", credentials.Username));

            return await AuthenticationService.LoginIpsUser(credentials);
        }

        [HttpGet]
        [ActionName("Logout")]
        // End the user's session
        public async Task<Response> Logout()
        {
            Logger.Debug("Method called.");
            var userSession = UserSession.GetCurrent();

            Logger.Info(string.Format("Session ended for user: {0}", userSession.Username));

            userSession.EndSession();

            return Response.Success();
        }

        [HttpGet]
        [ActionName("LoggedInUser")]
        // Get the logged in user's server-side session information. Used also to check if the user is logged in.
        public async Task<Response> GetLoggedInUser()
        {
            Logger.Debug("Method called.");
            var userSession = UserSession.GetCurrent();

            if (userSession.IsValid())
            {
                var data = new
                {
                    username = userSession.Username,
                    access = userSession.Access,
                    defaultState = userSession.DefaultState
                };

                return Response.Success(data);
            }
            else
            {
                return Response.Failure("User not logged in.");
            }
        }
    }
}