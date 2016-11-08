using System;
using Services.Authorization.Models;

namespace Services.Authorization
{
    public class AuthorizationManager
    {
        private readonly HeddokoClient _heddokoClient;
        private readonly OauthConfig _config;

        private static readonly Lazy<AuthorizationManager> LazyAuthorizationManager =
            new Lazy<AuthorizationManager>(
                () =>
                    new AuthorizationManager(new HeddokoClient(Config.OauthUrl),
                        new OauthConfig
                        {
                            Password = Config.Password,
                            Username = Config.Username,
                            OauthUrl = Config.OauthUrl
                        }));
        public static AuthorizationManager Instance => LazyAuthorizationManager.Value;

        private AuthorizationManager(HeddokoClient heddokoClient, OauthConfig config)
        {
            _heddokoClient = heddokoClient;
            _config = config;
        }

        public bool IsAuthorized => _heddokoClient.IsAuthorized;

        public string GetToken()
        {
            if (!_heddokoClient.IsAuthorized)
            {
                _heddokoClient.SignIn(new UserRequest { Username = _config.Username, Password = _config.Password });
            }

            return _heddokoClient.Token;
        }
    }
}
