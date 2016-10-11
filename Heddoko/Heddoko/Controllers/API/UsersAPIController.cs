using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using DAL;
using DAL.Models;
using Heddoko.Models;
using i18n;

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
            if (!id.HasValue)
            {
                return CurrentUser;
            }

            User user = UoW.UserRepository.GetIDCached(id.Value);
            if (user == null)
            {
                throw new APIException(ErrorAPIType.UserNotFound, ErrorAPIType.UserNotFound.GetDisplayName());
            }

            if (CurrentUser.ID == id.Value)
            {
                return user;
            }

            throw new APIException(ErrorAPIType.WrongObjectAccess, ErrorAPIType.WrongObjectAccess.GetDisplayName());
        }

        /// <summary>
        ///     Get profile of current user
        /// </summary>
        [Route("profile")]
        [HttpGet]
        [AuthAPI(Roles = Constants.Roles.All)]
        public User Profile()
        {
            CurrentUser.AllowLicenseInfoToken();
            return CurrentUser;
        }

        /// <summary>
        ///     Sign in user
        /// </summary>
        /// <param name="username">The username of user.</param>
        /// <param name="password">The password of user.</param>
        [Route("signin")]
        [HttpPost]
        public User Signin(SignInAPIViewModel model)
        {
            if (!ModelState.IsValid)
            {
                throw new ModelStateException
                {
                    ModelState = ModelState
                };
            }

            User user = UoW.UserRepository.GetByUsernameFull(model.Username?.Trim());
            if (user == null)
            {
                throw new APIException(ErrorAPIType.EmailOrPassword, Resources.WrongUsernameOrPassword);
            }

            if (user.IsBanned)
            {
                throw new APIException(ErrorAPIType.UserIsBanned, Resources.UserIsBanned);
            }

            if (user.IsNotApproved)
            {
                throw new APIException(ErrorAPIType.UserIsNotApproved, Resources.UserIsNotApproved);
            }

            if (user.IsActive)
            {
                //if (!PasswordHasher.Equals(model.Password?.Trim(), user.Salt, user.Password))
                //{
                //    throw new APIException(ErrorAPIType.EmailOrPassword, Resources.WrongUsernameOrPassword);
                //}

                if (user.License == null)
                {
                    throw new APIException(ErrorAPIType.LicenseIsNotReady, Resources.LicenseIsNotReady);
                }

                if (user.License.Validate())
                {
                    UoW.Save();
                    UoW.UserRepository.ClearCache(user);
                }

                if (!user.License.IsActive)
                {
                    switch (user.License.Status)
                    {
                        case LicenseStatusType.Expired:
                            throw new APIException(ErrorAPIType.LicenseIsNotReady, Resources.WrongLicenseExpiration);
                        case LicenseStatusType.Inactive:
                            throw new APIException(ErrorAPIType.LicenseIsNotReady, Resources.WrongLicenseActive);
                        case LicenseStatusType.Deleted:
                            throw new APIException(ErrorAPIType.LicenseIsNotReady, Resources.WrongLicenseDeleted);
                    }

                    if (user.License.ExpirationAt < DateTime.Now)
                    {
                        throw new APIException(ErrorAPIType.LicenseIsNotReady, Resources.WrongLicenseExpiration);
                    }
                }

                if (user.AllowToken())
                {
                    user.AllowLicenseInfoToken();
                    return user;
                }

                user.Tokens.Add(new AccessToken
                {
                    Token = user.GenerateToken()
                });
                UoW.Save();
                user.AllowToken();
                UoW.UserRepository.SetCache(user);

                user.AllowLicenseInfoToken();

                return user;
            }

            throw new APIException(ErrorAPIType.UserIsNotActive, Resources.UserIsNotActive);
        }

        /// <summary>
        ///     Check if token is valid
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

        /// <summary>
        ///     List of users by organization
        /// </summary>
        /// <param name="take">The amount of take entries</param>
        /// <param name="skip">The amoun of skip entries</param>
        [Route("list/{take:int}/{skip:int?}")]
        [HttpGet]
        [AuthAPI(Roles = Constants.Roles.Analyst)]
        public ListAPIViewModel<User> List(int take = 100, int? skip = 0)
        {
            if (!CurrentUser.OrganizationID.HasValue)
            {
                throw new APIException(ErrorAPIType.WrongObjectAccess, $"{Resources.NonAssigned} organization");
            }

            if (!CurrentUser.TeamID.HasValue)
            {
                throw new APIException(ErrorAPIType.WrongObjectAccess, $"{Resources.NonAssigned} team");
            }

            return new ListAPIViewModel<User>()
            {
                Collection = UoW.UserRepository.GetByOrganizationAPI(CurrentUser.OrganizationID.Value, CurrentUser.TeamID.Value, take, skip).ToList(),
                TotalCount = UoW.UserRepository.GetByOrganizationAPICount(CurrentUser.OrganizationID.Value, CurrentUser.TeamID.Value)
            };
        }
    }
}