using DAL;
using DAL.Models;
using Heddoko.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace Heddoko.Controllers.API
{
    [RoutePrefix("api/v1/users")]
    public class UsersAPIController : BaseAPIController
    {
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("{id:int?}")]
        [HttpGet]
        [AuthAPI(Roles = Constants.Roles.All)]
        public User Get(int? id = null)
        {
            if (id.HasValue)
            {
                User user = UoW.UserRepository.GetIDCached(id.Value);
                if (user != null)
                {
                    if (CurrentUser.ID == id.Value)
                    {
                        return user;
                    }
                    else
                    {
                        throw new APIException(ErrorAPIType.WrongObjectAccess, ErrorAPIType.WrongObjectAccess.GetDisplayName());
                    }
                }
                else
                {
                    throw new APIException(ErrorAPIType.UserNotFound, ErrorAPIType.UserNotFound.GetDisplayName());
                }
            }
            return CurrentUser;
        }

        /// <summary>
        /// Get profile of current user
        /// </summary>
        [Route("profile")]
        [HttpGet]
        [AuthAPI(Roles = Constants.Roles.All)]
        public User Profile()
        {
            return CurrentUser;
        }

        /// <summary>
        /// Sign in user
        /// </summary>
        /// <param name="username">The username of user.</param>
        /// <param name="password">The password of user.</param>
        [Route("signin")]
        [HttpPost]
        public User Signin(SignInAPIViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = UoW.UserRepository.GetByUsernameFull(model.Username?.Trim());
                if (user != null)
                {
                    if (user.IsActive)
                    {
                        if (PasswordHasher.Equals(model.Password?.Trim(), user.Salt, user.Password))
                        {
                            if (!user.AllowToken())
                            {
                                if (user.License == null)
                                {
                                    throw new APIException(ErrorAPIType.LicenseIsNotReady, i18n.Resources.UserIsBanned);
                                }

                                if (user.License.Validate())
                                {
                                    UoW.Save();
                                    UoW.UserRepository.ClearCache(user);
                                }

                                if (!user.License.IsActive)
                                {
                                    if (user.License.Status == LicenseStatusType.Expired)
                                    {
                                        throw new APIException(ErrorAPIType.LicenseIsNotReady, i18n.Resources.WrongLicenseExpiration);
                                    }

                                    if (user.License.Status == LicenseStatusType.Inactive)
                                    {
                                        throw new APIException(ErrorAPIType.LicenseIsNotReady, i18n.Resources.WrongLicenseActive);
                                    }

                                    if (user.License.Status == LicenseStatusType.Deleted)
                                    {
                                        throw new APIException(ErrorAPIType.LicenseIsNotReady, i18n.Resources.WrongLicenseDeleted);
                                    }

                                    if (user.License.ExpirationAt < DateTime.Now)
                                    {
                                        throw new APIException(ErrorAPIType.LicenseIsNotReady, i18n.Resources.WrongLicenseExpiration);
                                    }
                                }


                                user.Tokens.Add(new AccessToken()
                                {
                                    Token = user.GenerateToken()
                                });
                                UoW.Save();
                                user.AllowToken();
                                UoW.UserRepository.SetCache(user);
                            }
                            return user;
                        }
                        else
                        {
                            throw new APIException(ErrorAPIType.EmailOrPassword, i18n.Resources.WrongUsernameOrPassword);
                        }
                    }
                    else
                    {
                        if (user.IsBanned)
                        {
                            throw new APIException(ErrorAPIType.UserIsBanned, i18n.Resources.UserIsBanned);
                        }
                        else
                        {
                            throw new APIException(ErrorAPIType.UserIsNotActive, i18n.Resources.UserIsNotActive);
                        }
                    }
                }
                else
                {
                    throw new APIException(ErrorAPIType.EmailOrPassword, i18n.Resources.WrongUsernameOrPassword);
                }
            }
            else
            {
                throw new ModelStateException()
                {
                    ModelState = ModelState
                };
            }
        }

        /// <summary>
        /// Check if token is valid
        /// </summary>
        [Route("check")]
        [HttpGet]
        [AuthAPI(Roles = Constants.Roles.All)]
        public object Check()
        {
            return new
            {
                result = true
            };
        }
    }
}
