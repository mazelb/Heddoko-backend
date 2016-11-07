using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Services.Authorization.Models;

namespace Services.Authorization
{
    public class HeddokoClient
    {
        private const string ApplicationXwwwFormUrlEncoded = "application/x-www-form-urlencoded";
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
            OauthResponse token = SendPostXWwwForm<OauthResponse>(request);

            _token = token.AccessToken;
            _expiresAt = DateTime.Now.AddSeconds(token.ExpiresIn);

            return IsAuthorized;
        }

        private T SendPostXWwwForm<T>(UserRequest request)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(_oauthUrl);
            httpWebRequest.Method = "POST";
            httpWebRequest.ContentType = ApplicationXwwwFormUrlEncoded;
            try
            {
                string postData = $"grant_type=password&username={Uri.EscapeUriString(request.Username)}&password={Uri.EscapeUriString(request.Password)}";
                byte[] data = Encoding.ASCII.GetBytes(postData);

                httpWebRequest.ContentLength = data.Length;

                using (Stream stream = httpWebRequest.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                    stream.Flush();
                    stream.Close();
                }

                HttpWebResponse httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                string result;
                using (StreamReader streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }

                return JsonConvert.DeserializeObject<T>(result);
            }
            catch (WebException ex)
            {
                using (WebResponse response = ex.Response)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)response;

                    using (Stream data = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(data))
                        {
                            string text = reader.ReadToEnd();

                            string error = JsonConvert.DeserializeObject<ErrorOauth>(text).ErrorDescription;

                            Trace.TraceError("HeddokoClient.SendPostXWwwForm : Error code: {0}, error description: {1}", httpResponse.StatusCode, error);

                            throw new Exception(error);
                        }
                    }
                }
            }
        }
    }
}
