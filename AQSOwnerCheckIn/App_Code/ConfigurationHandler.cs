using System.Web;

namespace AQSOwnerCheckIn
{
    public class ConfigurationHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            HttpResponse response = context.Response;
        
            response.ContentType = "text/javascript";
            var responseBody = 
                "var __angularConfig = {};";

            response.Write(responseBody);
        }

        public bool IsReusable
        {
            // To enable pooling, return true here.
            // This keeps the handler in memory.
            get { return false; }
        }
    }
}
