using Newtonsoft.Json;

namespace Services.Authorization.Models
{
    public class ErrorOauth
    {
        public string Error { get; set; }

        [JsonProperty(PropertyName = "error_description")]
        public string ErrorDescription { get; set; }
    }
}
