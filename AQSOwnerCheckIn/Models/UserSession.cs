using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using AQSOwnerCheckIn.Extensions;
using Newtonsoft.Json;

namespace AQSOwnerCheckIn.Models
{
    [Serializable]
    public class UserSession
    {
        [JsonProperty(PropertyName = "contactKey")]
        public int ContactKey { get; set; }

        [JsonProperty(PropertyName = "identityKey")]
        public int IdentityKey { get; set; }
        
        [JsonProperty(PropertyName = "webUserKey")]
        public int WebUserKey { get; set; }

        [JsonProperty(PropertyName = "ipsUserKey")]
        public int IpsUserKey { get; set; }
        
        [JsonProperty(PropertyName = "username")]
        public string Username { get; set; }
        
        [JsonProperty(PropertyName = "firstName")]
        public string FirstName { get; set; }
        
        [JsonProperty(PropertyName = "lastName")]
        public string LastName { get; set; }
        
        [JsonProperty(PropertyName = "ticket")]
        public string Ticket { get; set; }

        [JsonProperty(PropertyName = "access")]
        public string[] Access 
        {
            get
            {
                var access = new List<string>();
                // Add access
                return access.ToArray();
            } 
        }

        [JsonProperty(PropertyName = "defaultState")]
        public string DefaultState
        {
            get
            {
                return "public.login";
            }
        }

        public UserSessionRole[] Roles { get; set; }
        
        public UserSession()
        {
            Username = "";
        }

        public static UserSession GetCurrent()
        {
            UserSession currentSession;

            try
            {
                currentSession = (UserSession)HttpContext.Current.Session["user"];
            }
            catch (Exception e)
            {
                currentSession = new UserSession();
            }

            if (currentSession == null) currentSession = new UserSession();

            return currentSession;
        }

        public void Save(HttpContext currentHttpContext)
        {
            currentHttpContext.Session.Add("user", this);
        }

        public bool IsValid()
        {
            return Username.Trim() != string.Empty;
        }

        public void EndSession()
        {
            HttpContext.Current.Session.Remove("user");
        }
    }
}