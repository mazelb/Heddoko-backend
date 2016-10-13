using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using DAL;
using DAL.Models;
using Hangfire;
using Heddoko.Helpers.Auth;
using Heddoko.Models;
using i18n;

namespace Heddoko.Controllers
{
    public class AccountController : BaseController
    {
        public ActionResult SignIn(string returnUrl)
        {
            Forms.SignOut();

            SignInAccountViewModel model = new SignInAccountViewModel();
            FlashMessage message = ContextTempData.FlashMessage;
            if (message != null)
            {
                model.Flash.Add(message);
            }
            model.ReturnUrl = returnUrl;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignIn(SignInAccountViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = Forms.SignIn(UoW, model.Username, model.Password);
                if (user != null)
                {
                    if (user.IsActive)
                    {
                        CurrentUser = Forms.Authorize(user);

                        return RedirectToLocal(model.ReturnUrl);
                    }
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
                        ModelState.AddModelError(string.Empty, Resources.UserIsNotActive);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, Resources.WrongUsernameOrPassword);
                }
            }
            return View(model);
        }

        public ActionResult SignUpOrganization(string token)
        {
            BaseViewModel model = new BaseViewModel();

            if (!string.IsNullOrEmpty(token?.Trim()))
            {
                User user = UoW.UserRepository.GetByInviteToken(token?.Trim());
                if (user != null)
                {
                    bool isNew = string.IsNullOrEmpty(user.Password);

                    if (isNew)
                    {
                        SignUpAccountViewModel signup = new SignUpAccountViewModel();

                        signup.Organization = user.Organization;
                        signup.InviteToken = user.InviteToken;
                        signup.Email = user.Email.ToLower();
                        signup.Username = user.Username.ToLower();
                        signup.FirstName = user.FirstName;
                        signup.LastName = user.LastName;
                        signup.Phone = user.Phone;
                        signup.Country = user.Country;
                        signup.Birthday = user.BirthDay;
                        signup.OrganizationName = user.Organization.Name;
                        signup.Address = user.Organization.Address;

                        return View(signup);
                    }
                    user.Status = UserStatusType.Active;
                    user.InviteToken = null;
                    UoW.Save();
                    UoW.UserRepository.SetCache(user);

                    if (user.Organization.UserID == user.ID)
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
        [ValidateAntiForgeryToken]
        public ActionResult SignUpOrganization(SignUpAccountViewModel model)
        {
            Forms.SignOut();

            User user = UoW.UserRepository.GetByInviteToken(model.InviteToken?.Trim());

            if (ModelState.IsValid)
            {
                if (user != null)
                {
                    User userUsed = UoW.UserRepository.GetByUsernameCached(model.Username?.Trim());

                    if (userUsed != null
                        &&
                        user.ID != userUsed.ID)
                    {
                        ModelState.AddModelError(string.Empty, Resources.UsernameUsed);
                    }
                    else
                    {
                        user.FirstName = model.FirstName?.Trim();
                        user.LastName = model.LastName?.Trim();
                        user.Country = model.Country?.Trim();
                        user.BirthDay = model.Birthday;
                        user.Phone = model.Phone?.Trim();
                        user.Status = UserStatusType.Active;
                        user.InviteToken = null;
                        Passphrase pwd = PasswordHasher.Hash(model.Password?.Trim());

                        user.Password = pwd.Hash;
                        user.Salt = pwd.Salt;

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

        public ActionResult SignUp()
        {
            Forms.SignOut();

            SignUpAccountViewModel model = new SignUpAccountViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(SignUpAccountViewModel model)
        {
            if (ModelState.IsValid)
            {
                Organization organization = UoW.OrganizationRepository.GetByName(model.OrganizationName?.Trim());

                if (organization != null)
                {
                    ModelState.AddModelError(string.Empty, Resources.OrganizationNameUsed);
                }
                else
                {
                    User user = UoW.UserRepository.GetByEmailCached(model.Email?.Trim());
                    if (user != null)
                    {
                        ModelState.AddModelError(string.Empty, Resources.EmailUsed);
                    }
                    else
                    {
                        user = UoW.UserRepository.GetByUsernameCached(model.Username?.Trim());

                        if (user != null)
                        {
                            ModelState.AddModelError(string.Empty, Resources.UsernameUsed);
                        }
                        else
                        {
                            organization = new Organization();
                            organization.Name = model.OrganizationName.Trim();
                            organization.Phone = model.Phone?.Trim();
                            organization.Address = model.Address.Trim();
                            organization.Status = OrganizationStatusType.Pending;


                            user = new User();
                            user.Email = model.Email.ToLower().Trim();
                            user.Username = model.Username.ToLower().Trim();
                            user.Role = UserRoleType.LicenseAdmin;
                            user.FirstName = model.FirstName.Trim();
                            ;
                            user.LastName = model.LastName.Trim();
                            ;
                            user.Country = model.Country;
                            user.BirthDay = model.Birthday;
                            user.Phone = model.Phone.Trim();
                            ;
                            user.Status = UserStatusType.Pending;

                            Passphrase pwd = PasswordHasher.Hash(model.Password);

                            user.Password = pwd.Hash;
                            user.Salt = pwd.Salt;

                            user.ConfirmToken = PasswordHasher.Md5(DateTime.Now.Ticks.ToString());

                            UoW.UserRepository.Create(user);

                            organization.User = user;
                            UoW.OrganizationRepository.Create(organization);

                            user.Organization = organization;
                            UoW.Save();

                            BaseViewModel modelStatus = new BaseViewModel();

                            modelStatus.Flash.Add(new FlashMessage
                            {
                                Type = FlashMessageType.Success,
                                Message = Resources.UserSignupMessage
                            });
                            return View("SignUpStatus", modelStatus);
                        }
                    }
                }
            }
            return View(model);
        }

        [Auth]
        public ActionResult SignOut()
        {
            Forms.SignOut();
            return RedirectToAction("SignIn", "Account");
        }

        [Auth]
        public ActionResult Me()
        {
            ProfileAccountViewModel model = new ProfileAccountViewModel();

            model.Organization = CurrentUser.Organization;
            model.Email = CurrentUser.Email;
            model.Username = CurrentUser.Username.ToLower();
            model.FirstName = CurrentUser.FirstName;
            model.LastName = CurrentUser.LastName;
            model.Phone = CurrentUser.Phone;
            model.Country = CurrentUser.Country;
            model.Birthday = CurrentUser.BirthDay;
            model.OrganizationName = CurrentUser.Organization?.Name;
            model.Address = CurrentUser.Organization?.Address;

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
                    CurrentUser.ID != userUsed.ID)
                {
                    ModelState.AddModelError(string.Empty, Resources.UsernameUsed);
                }
                else
                {
                    CurrentUser.FirstName = model.FirstName?.Trim();
                    CurrentUser.Username = model.Username?.Trim().ToLower();
                    CurrentUser.LastName = model.LastName?.Trim();
                    CurrentUser.Phone = model.Phone?.Trim();
                    CurrentUser.Country = model.Country?.Trim();
                    CurrentUser.BirthDay = model.Birthday;

                    if (!string.IsNullOrEmpty(model.NewPassord))
                    {
                        if (PasswordHasher.Equals(model.OldPassword?.Trim(), CurrentUser.Salt, CurrentUser.Password))
                        {
                            Passphrase pwd = PasswordHasher.Hash(model.NewPassord);

                            CurrentUser.Password = pwd.Hash;
                            CurrentUser.Salt = pwd.Salt;
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, Resources.WrongOldPassword);
                            model.Organization = CurrentUser.Organization;
                            return View(model);
                        }
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
            if (IsAuth)
            {
                switch (CurrentUser.Role)
                {
                    case UserRoleType.Admin:
                        return RedirectToAction("Index", "License");
                    case UserRoleType.Analyst:
                        return RedirectToAction("Index", "Default");
                    case UserRoleType.LicenseAdmin:
                        return RedirectToAction("Index", "Organization");
                    case UserRoleType.User:
                    default:
                        return RedirectToAction("Index", "Default");
                }
            }
            return RedirectToAction("Signin", "Account");
        }

        public ActionResult Confirm(string token)
        {
            BaseViewModel model = new BaseViewModel();

            if (!string.IsNullOrEmpty(token?.Trim()))
            {
                User user = UoW.UserRepository.GetByConfirmToken(token?.Trim());
                if (user != null)
                {
                    user.Status = UserStatusType.Active;
                    user.ConfirmToken = null;

                    UoW.Save();
                    UoW.UserRepository.SetCache(user);

                    BackgroundJob.Enqueue(() => Services.EmailManager.SendActivatedEmail(user));

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
                        Passphrase pwd = PasswordHasher.Hash(model.Password?.Trim());

                        user.Password = pwd.Hash;
                        user.Salt = pwd.Salt;
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

        public ActionResult ForgotPassword()
        {
            ForgotPasswordViewModel model = new ForgotPasswordViewModel();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(ForgotPasswordViewModel model)
        {
            BaseViewModel baseModel = new BaseViewModel();

            if (ModelState.IsValid)
            {
                User user = UoW.UserRepository.GetByEmail(model.Email?.Trim());
                if (user != null)
                {
                    user.ForgotExpiration = DateTime.Now.AddHours(Config.EmailForgotTokenExpiration);
                    user.ForgotToken = PasswordHasher.Md5(user.ForgotExpiration.Value.Ticks.ToString());

                    UoW.Save();
                    UoW.UserRepository.SetCache(user);

                    BackgroundJob.Enqueue(() => Services.EmailManager.SendForgotPasswordEmail(user));

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

        public ActionResult ForgotUsername()
        {
            ForgotPasswordViewModel model = new ForgotPasswordViewModel();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotUsername(ForgotPasswordViewModel model)
        {
            BaseViewModel baseModel = new BaseViewModel();

            if (ModelState.IsValid)
            {
                User user = UoW.UserRepository.GetByEmail(model.Email?.Trim());
                if (user != null)
                {
                    BackgroundJob.Enqueue(() => Services.EmailManager.SendForgotUsernameEmail(user));

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
    }
}