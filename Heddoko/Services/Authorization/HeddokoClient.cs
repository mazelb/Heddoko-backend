using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using Services.Authorization.Models;

namespace Services.Authorization
{
    public class HeddokoClient
    {
        private const string Bearer = "Bearer ";

        private readonly string _oauthUrl;
        private string _token;

        private DateTime _expiresAt;

        public HeddokoClient(string oauthUrl)
        {
            _oauthUrl = oauthUrl;
        }

        public string Token => Bearer + _token;

        public bool IsAuthorized => !string.IsNullOrWhiteSpace(_token) && _expiresAt > DateTime.Now;

        public bool SignIn(UserRequest request)
        {
            request.GrantType = "password";

            OauthResponse token = SendPostXWwwForm<OauthResponse>(request).Result;

            if (token != null)
            {
                _token = token.AccessToken;
                _expiresAt = DateTime.Now.AddSeconds(token.ExpiresIn);
            }

            return IsAuthorized;
        }

        private async Task<T> SendPostXWwwForm<T>(UserRequest request)
        {
            using (var client = new HttpClient())
            {
                var postData = new Dictionary<string, string>
                {
                    { "grant_type", "password" },
                    { "username", Uri.EscapeUriString(request.Username) },
                    { "password", Uri.EscapeUriString(request.Password) }
                };

                var content = new FormUrlEncodedContent(postData);
                HttpResponseMessage response = await client.PostAsync(_oauthUrl, content).ConfigureAwait(false);

                T result = default(T);

                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsAsync<T>().ConfigureAwait(false);
                }
                else
                {
                    ErrorOauth error = await response.Content.ReadAsAsync<ErrorOauth>();
                    Trace.TraceError("HeddokoClient.SendPostXWwwForm : Error code: {0}, error description: {1}", response.StatusCode, error);
                }

                return result;
            }
        }
    }
}
