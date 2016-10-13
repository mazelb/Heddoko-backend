using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using DAL;
using DAL.Models;
using Heddoko.Helpers.Auth;
using Heddoko.Models;
using i18n;
using Services;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;

namespace Heddoko.Controllers
{
    [Auth(Roles = DAL.Constants.Roles.All)]
    public class AccountController : BaseController
    {
        public AccountController() : base() { }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, UnitOfWork uow)
            : base(userManager, signInManager, uow)
        {
        }

        [AllowAnonymous]
        public ActionResult SignIn(string returnUrl)
        {
            SignInAccountViewModel model = new SignInAccountViewModel
            {
                ReturnUrl = returnUrl
            };
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SignIn(SignInAccountViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            User user = await UserManager.FindAsync(model.Username, model.Password);

            if (!user.IsActive)
            {
                if (user.IsBanned)
                {
                    ModelState.AddModelError(string.Empty, Resources.UserIsBanned);
                }
                else if (user.IsNotApproved)
                {
                    ModelState.AddModelError(string.Empty, Resources.UserIsNotApproved);
                }
                else
                {
                    if (!UserManager.IsEmailConfirmed(user.Id))
                    {
                        ModelState.AddModelError(string.Empty, Resources.WrongConfirm);
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, Resources.UserIsNotActive);
                    }
                }
            }

            SignInStatus result = await SignInManager.PasswordSignInAsync(model.Username, model.Password, false, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    ModelState.AddModelError(string.Empty, Resources.WrongLockedOut);
                    return View(model);
                case SignInStatus.RequiresVerification:
                    ModelState.AddModelError(string.Empty, Resources.RequiresVerification);
                    return View(model);
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError(string.Empty, Resources.WrongUsernameOrPassword);
                    return View(model);
            }
        }

        [AllowAnonymous]
        public ActionResult SignUpOrganization(string token)
        {
            BaseViewModel model = new BaseViewModel();

            if (!string.IsNullOrEmpty(token?.Trim()))
            {
                User user = UoW.UserRepository.GetByInviteToken(token?.Trim());
                if (user != null)
                {
                    bool isNew = string.IsNullOrEmpty(user.PasswordHash);

                    if (isNew)
                    {
                        SignUpAccountViewModel signup = new SignUpAccountViewModel
                        {
                            Organization = user.Organization,
                            InviteToken = user.InviteToken,
                            Email = user.Email.ToLower(),
                            Username = user.UserName.ToLower(),
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            Phone = user.PhoneNumber,
                            Country = user.Country,
                            Birthday = user.BirthDay,
                            OrganizationName = user.Organization.Name,
                            Address = user.Organization.Address
                        };


                        return View(signup);
                    }
                    user.Status = UserStatusType.Active;
                    user.InviteToken = null;
                    UoW.Save();
                    UoW.UserRepository.SetCache(user);

                    if (user.Organization.UserID == user.Id)
                    {
                        return RedirectToAction("Index", "Organization");
                    }
                }
                else
                {
                    model.Flash.Add(new FlashMessage
                    {
                        Type = FlashMessageType.Error,
                        Message = Resources.WrongConfirmationToken
                    });
                }
            }
            else
            {
                model.Flash.Add(new FlashMessage
                {
                    Type = FlashMessageType.Error,
                    Message = Resources.EmptyConfirmationToken
                });
            }

            return View("ConfirmStatus", model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult SignUpOrganization(SignUpAccountViewModel model)
        {
            User user = UoW.UserRepository.GetByInviteToken(model.InviteToken?.Trim());

            if (ModelState.IsValid)
            {
                if (user != null)
                {
                    User userUsed = UoW.UserRepository.GetByUsernameCached(model.Username?.Trim());

                    if (userUsed != null
                        &&
                        user.Id != userUsed.Id)
                    {
                        ModelState.AddModelError(string.Empty, Resources.UsernameUsed);
                    }
                    else
                    {
                        user.FirstName = model.FirstName?.Trim();
                        user.LastName = model.LastName?.Trim();
                        user.Country = model.Country?.Trim();
                        user.BirthDay = model.Birthday;
                        user.PhoneNumber = model.Phone?.Trim();
                        user.Status = UserStatusType.Active;
                        user.InviteToken = null;
                        Passphrase pwd = DAL.PasswordHasher.Hash(model.Password?.Trim());
                        //TODO Identity
                        //user.Password = pwd.Hash;
                        //user.Salt = pwd.Salt;

                        UoW.Save();
                        UoW.UserRepository.SetCache(user);

                        BaseViewModel modelStatus = new BaseViewModel();

                        string message = null;

                        if (user.Role == UserRoleType.LicenseAdmin)
                        {
                            message = Resources.UserSignupOrganizationMessage;
                        }
                        else
                        {
                            if (user.LicenseID.HasValue)
                            {
                                message = Resources.UserSignupUserOrganizationMessage;
                            }
                            else
                            {
                                message = Resources.UserSignupUserNonLicenseOrganizationMessage;
                            }
                        }

                        modelStatus.Flash.Add(new FlashMessage
                        {
                            Type = FlashMessageType.Success,
                            Message = message
                        });
                        return View("SignUpStatus", modelStatus);
                    }
                }
            }

            model.Organization = user?.Organization;
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult SignUp()
        {
            SignUpAccountViewModel model = new SignUpAccountViewModel();
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SignUp(SignUpAccountViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            Organization organization = UoW.OrganizationRepository.GetByName(model.OrganizationName?.Trim());

            if (organization != null)
            {
                ModelState.AddModelError(string.Empty, Resources.OrganizationNameUsed);
                return View(model);
            }

            User user = UserManager.FindByEmailCached(model.Email?.Trim());
            if (user != null)
            {
                ModelState.AddModelError(string.Empty, Resources.EmailUsed);
                return View(model);
            }

            user = UserManager.FindByNameCached(model.Username?.Trim());

            if (user != null)
            {
                ModelState.AddModelError(string.Empty, Resources.UsernameUsed);
                return View(model);
            }

            organization = new Organization
            {
                Name = model.OrganizationName?.Trim(),
                Phone = model.Phone?.Trim(),
                Address = model.Address.Trim(),
                Status = OrganizationStatusType.Pending
            };


            user = new User
            {
                Email = model.Email?.ToLower().Trim(),
                UserName = model.Username?.ToLower().Trim(),
                //TODO REMOVE THAT AFTER Migration all accounts
                Role = UserRoleType.LicenseAdmin,
                FirstName = model.FirstName.Trim(),
                LastName = model.LastName.Trim(),
                Country = model.Country,
                BirthDay = model.Birthday,
                PhoneNumber = model.Phone?.Trim(),
                Status = UserStatusType.Pending
            };

            IdentityResult result = await UserManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                UserManager.AddToRole(user.Id, DAL.Constants.Roles.LicenseAdmin);
                organization.User = user;
                UoW.OrganizationRepository.Create(organization);
                user.Organization = organization;
                UoW.Save();

                string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                //TODO Identity
                //await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                BaseViewModel modelStatus = new BaseViewModel();

                modelStatus.Flash.Add(new FlashMessage
                {
                    Type = FlashMessageType.Success,
                    Message = Resources.UserSignupMessage
                });

                return View("SignUpStatus", modelStatus);
            }

            AddErrors(result);

            return View(model);
        }

        [Auth]
        public ActionResult SignOut()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("SignIn", "Account");
        }

        [Auth]
        public ActionResult Me()
        {
            ProfileAccountViewModel model = new ProfileAccountViewModel
            {
                Organization = CurrentUser.Organization,
                Email = CurrentUser.Email,
                Username = CurrentUser.UserName.ToLower(),
                FirstName = CurrentUser.FirstName,
                LastName = CurrentUser.LastName,
                Phone = CurrentUser.PhoneNumber,
                Country = CurrentUser.Country,
                Birthday = CurrentUser.BirthDay,
                OrganizationName = CurrentUser.Organization?.Name,
                Address = CurrentUser.Organization?.Address
            };

            return View(model);
        }

        [Auth]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Me(ProfileAccountViewModel model)
        {
            if (ModelState.IsValid)
            {
                User userUsed = UoW.UserRepository.GetByUsernameFull(model.Username?.Trim());

                if (userUsed != null
                    &&
                    CurrentUser.Id != userUsed.Id)
                {
                    ModelState.AddModelError(string.Empty, Resources.UsernameUsed);
                }
                else
                {
                    CurrentUser.FirstName = model.FirstName?.Trim();
                    CurrentUser.UserName = model.Username?.Trim().ToLower();
                    CurrentUser.LastName = model.LastName?.Trim();
                    CurrentUser.PhoneNumber = model.Phone?.Trim();
                    CurrentUser.Country = model.Country?.Trim();
                    CurrentUser.BirthDay = model.Birthday;

                    if (!string.IsNullOrEmpty(model.NewPassord))
                    {
                        //TODO Identity
                        //if (PasswordHasher.Equals(model.OldPassword?.Trim(), CurrentUser.Salt, CurrentUser.Password))
                        //{
                        //    Passphrase pwd = PasswordHasher.Hash(model.NewPassord);

                        //    CurrentUser.Password = pwd.Hash;
                        //    CurrentUser.Salt = pwd.Salt;
                        //}
                        //else
                        //{
                        //    ModelState.AddModelError(string.Empty, Resources.WrongOldPassword);
                        //    model.Organization = CurrentUser.Organization;
                        //    return View(model);
                        //}
                    }


                    if (CurrentUser.Role == UserRoleType.LicenseAdmin)
                    {
                        CurrentUser.Organization.Address = model.Address;
                    }
                    UoW.Save();
                    UoW.UserRepository.SetCache(CurrentUser);

                    model.Flash.Add(new FlashMessage
                    {
                        Type = FlashMessageType.Success,
                        Message = Resources.ProfileSaveMessage
                    });
                }
            }

            model.Organization = CurrentUser.Organization;

            return View(model);
        }

        private ActionResult RedirectToLocal(string returnUrl = null)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            switch (CurrentUser.Role)
            {
                case UserRoleType.Admin:
                    return RedirectToAction("Index", "License");
                case UserRoleType.Analyst:
                    return RedirectToAction("Index", "Default");
                case UserRoleType.LicenseAdmin:
                    return RedirectToAction("Index", "Organization");
                case UserRoleType.User:
                case UserRoleType.Worker:
                default:
                    return RedirectToAction("Index", "Default");
            }
        }

        [AllowAnonymous]
        public async Task<ActionResult> Confirm(int userId, string token)
        {
            BaseViewModel model = new BaseViewModel();

            if (!string.IsNullOrEmpty(token?.Trim()))
            {
                var result = await UserManager.ConfirmEmailAsync(userId, token);
                if (result.Succeeded)
                {
                    //TODO Identity
                    //  Task.Run(() => Mailer.SendActivatedEmail(user));
                    model.Flash.Add(new FlashMessage
                    {
                        Type = FlashMessageType.Success,
                        Message = Resources.Confirmed
                    });
                }
                else
                {
                    model.Flash.Add(new FlashMessage
                    {
                        Type = FlashMessageType.Error,
                        Message = Resources.WrongConfirmationToken
                    });
                }
            }
            else
            {
                model.Flash.Add(new FlashMessage
                {
                    Type = FlashMessageType.Error,
                    Message = Resources.EmptyConfirmationToken
                });
            }

            return View("ConfirmStatus", model);
        }

        [AllowAnonymous]
        public ActionResult Forgot(string token)
        {
            BaseViewModel model = new BaseViewModel();

            if (!string.IsNullOrEmpty(token?.Trim()))
            {
                User user = UoW.UserRepository.GetByForgetToken(token?.Trim());
                if (user != null)
                {
                    if (user.ForgotExpiration.HasValue
                        &&
                        user.ForgotExpiration.Value >= DateTime.Now)
                    {
                        ForgotViewModel forgetModel = new ForgotViewModel();
                        forgetModel.ForgetToken = token?.Trim();
                        return View("Forgot", forgetModel);
                    }
                    model.Flash.Add(new FlashMessage
                    {
                        Type = FlashMessageType.Error,
                        Message = Resources.ExpiredForgotToken
                    });
                }
                else
                {
                    model.Flash.Add(new FlashMessage
                    {
                        Type = FlashMessageType.Error,
                        Message = Resources.WrongForgotToken
                    });
                }
            }
            else
            {
                model.Flash.Add(new FlashMessage
                {
                    Type = FlashMessageType.Warning,
                    Message = Resources.EmptyForgotToken
                });
            }

            return View("ForgotStatus", model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Forgot(ForgotViewModel model)
        {
            BaseViewModel baseModel = new BaseViewModel();

            if (ModelState.IsValid)
            {
                User user = UoW.UserRepository.GetByForgetToken(model.ForgetToken?.Trim());
                if (user != null)
                {
                    if (user.ForgotExpiration.HasValue
                        &&
                        user.ForgotExpiration.Value >= DateTime.Now)
                    {
                        Passphrase pwd = DAL.PasswordHasher.Hash(model.Password?.Trim());
                        //TODO Identity
                        //user.Password = pwd.Hash;
                        //user.Salt = pwd.Salt;
                        user.ForgotToken = null;
                        user.ForgotExpiration = null;

                        UoW.Save();
                        UoW.UserRepository.SetCache(user);

                        baseModel.Flash.Add(new FlashMessage
                        {
                            Type = FlashMessageType.Success,
                            Message = Resources.PasswordSuccessufullyChanged
                        });
                    }
                    else
                    {
                        baseModel.Flash.Add(new FlashMessage
                        {
                            Type = FlashMessageType.Error,
                            Message = Resources.ExpiredForgotToken
                        });
                    }
                }
                else
                {
                    baseModel.Flash.Add(new FlashMessage
                    {
                        Type = FlashMessageType.Error,
                        Message = Resources.WrongForgotToken
                    });
                }
                return View("ForgotStatus", baseModel);
            }

            return View("Forgot", model);
        }

        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            ForgotPasswordViewModel model = new ForgotPasswordViewModel();

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            BaseViewModel baseModel = new BaseViewModel();

            if (ModelState.IsValid)
            {
                User user = await UserManager.FindByNameAsync(model.Email?.Trim());
                if (user != null)
                {
                    //user.ForgotExpiration = DateTime.Now.AddHours(Config.EmailForgotTokenExpiration);
                    //user.ForgotToken = PasswordHasher.Md5(user.ForgotExpiration.Value.Ticks.ToString());

                    //UoW.Save();
                    //UoW.UserRepository.SetCache(user);

                    //Task.Run(() => Mailer.SendForgotPasswordEmail(user));
                    //TODO Identity

                    baseModel.Flash.Add(new FlashMessage
                    {
                        Type = FlashMessageType.Success,
                        Message = Resources.PasswordSuccessufullySent
                    });
                }
                else
                {
                    baseModel.Flash.Add(new FlashMessage
                    {
                        Type = FlashMessageType.Error,
                        Message = Resources.WrongEmailForgotPassword
                    });
                }

                return View("ForgotStatus", baseModel);
            }

            return View(model);
        }

        [AllowAnonymous]
        public ActionResult ForgotUsername()
        {
            ForgotPasswordViewModel model = new ForgotPasswordViewModel();

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotUsername(ForgotPasswordViewModel model)
        {
            BaseViewModel baseModel = new BaseViewModel();

            if (ModelState.IsValid)
            {
                User user = await UserManager.FindByNameAsync(model.Email?.Trim());
                if (user != null)
                {
                    Task.Run(() => Mailer.SendForgotUsernameEmail(user));

                    baseModel.Flash.Add(new FlashMessage
                    {
                        Type = FlashMessageType.Success,
                        Message = Resources.UsernameSuccessufullySent
                    });
                }
                else
                {
                    baseModel.Flash.Add(new FlashMessage
                    {
                        Type = FlashMessageType.Error,
                        Message = Resources.WrongEmailForgotPassword
                    });
                }

                return View("ForgotStatus", baseModel);
            }

            return View(model);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error);
            }
        }
    }
}