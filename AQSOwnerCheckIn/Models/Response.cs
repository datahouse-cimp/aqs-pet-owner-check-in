namespace AQSOwnerCheckIn.Models
{
    public class Response
    {
        public string status;
        public object data;
        public string message = "";

        public Response() { }

        public Response(string status, object data)
        {
            this.status = status;
            this.data = data;
        }

        public Response(string status, string message)
        {
            this.status = status;
            this.message = message;
        }

        public Response(string status, object data, string message)
        {
            this.status = status;
            this.data = data;
            this.message = message;
        }

        public static Response Success()
        {
            return new Response("success", new object());
        }

        public static Response Success(object data)
        {
            return new Response("success", data);
        }

        public static Response Success(string message, object data)
        {
            return new Response("success", data, message);
        }

        public static Response Warning(string message)
        {
            return new Response("warning", new object(), message);
        }

        public static Response Warning(string message, object data)
        {
            return new Response("warning", data, message);
        }

        public static Response Failure(string message)
        {
            return new Response("failure", message);
        }

        public static Response NoSession()
        {
            return new Response("noSession", "No session detected.");
        }
    }
}