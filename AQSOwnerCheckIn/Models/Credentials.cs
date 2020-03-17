using Newtonsoft.Json;

namespace AQSOwnerCheckIn.Models
{
    public class Credentials
    {
        [JsonProperty(PropertyName = "username")]
        public string Username;

        [JsonProperty(PropertyName = "password")]
        public string Password;

        public Credentials() { }
    }
}