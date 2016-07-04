using DAL;
using DAL.Models;
using Heddoko.Helpers.Auth;
using Heddoko.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Description;
using System.Web.Mvc;

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
                    else
                    {
                        if (user.IsBanned)
                        {
                            ModelState.AddModelError(string.Empty, i18n.Resources.UserIsBanned);
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, i18n.Resources.UserIsNotActive);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, i18n.Resources.WrongUsernameOrPassword);
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
                    else
                    {
                        user.Status = UserStatusType.Active;
                        user.InviteToken = null;
                        UoW.Save();
                        UoW.UserRepository.SetCache(user);
                    }

                    if (user.Organization.UserID == user.ID)
                    {
                        return RedirectToAction("Index", "Organization");
                    }
                }
                else
                {
                    model.Flash.Add(new FlashMessage()
                    {
                        Type = FlashMessageType.Error,
                        Message = i18n.Resources.WrongConfirmationToken
                    });
                }
            }
            else
            {
                model.Flash.Add(new FlashMessage()
                {
                    Type = FlashMessageType.Error,
                    Message = i18n.Resources.EmptyConfirmationToken
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
                     && user.ID != userUsed.ID)
                    {
                        ModelState.AddModelError(string.Empty, i18n.Resources.UsernameUsed);
                    }
                    else
                    {
                        user.FirstName = model.FirstName?.Trim();
                        user.LastName = model.LastName?.Trim();
                        user.Country = model.Country?.Trim();
                        user.BirthDay = model.Birthday;
                        user.Phone = model.Phone;
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
                            message = i18n.Resources.UserSignupOrganizationMessage;
                        }
                        else
                        {
                            if (user.LicenseID.HasValue)
                            {
                                message = i18n.Resources.UserSignupUserOrganizationMessage;
                            }
                            else
                            {
                                message = i18n.Resources.UserSignupUserNonLicenseOrganizationMessage;
                            }
                        }

                        modelStatus.Flash.Add(new FlashMessage()
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
                    ModelState.AddModelError(string.Empty, i18n.Resources.OrganizationNameUsed);
                }
                else
                {

                    User user = UoW.UserRepository.GetByEmailCached(model.Email?.Trim());
                    if (user != null)
                    {
                        ModelState.AddModelError(string.Empty, i18n.Resources.EmailUsed);
                    }
                    else
                    {

                        user = UoW.UserRepository.GetByUsernameCached(model.Username?.Trim());

                        if (user != null)
                        {
                            ModelState.AddModelError(string.Empty, i18n.Resources.UsernameUsed);
                        }
                        else
                        {

                            organization = new Organization();
                            organization.Name = model.OrganizationName.Trim();
                            organization.Phone = model.Phone.Trim();
                            organization.Address = model.Address.Trim();
                            organization.Status = OrganizationStatusType.Active;


                            user = new User();
                            user.Email = model.Email.ToLower().Trim();
                            user.Username = model.Username.ToLower().Trim();
                            user.Role = UserRoleType.LicenseAdmin;
                            user.FirstName = model.FirstName.Trim(); ;
                            user.LastName = model.LastName.Trim(); ;
                            user.Country = model.Country;
                            user.BirthDay = model.Birthday;
                            user.Phone = model.Phone.Trim(); ;
                            user.Status = UserStatusType.NotActive;

                            Passphrase pwd = PasswordHasher.Hash(model.Password);

                            user.Password = pwd.Hash;
                            user.Salt = pwd.Salt;

                            user.ConfirmToken = PasswordHasher.Md5(DateTime.Now.Ticks.ToString());

                            UoW.UserRepository.Create(user);

                            organization.User = user;
                            UoW.OrganizationRepository.Create(organization);

                            user.Organization = organization;
                            UoW.Save();

                            Task.Run(() => Mailer.SendActivationEmail(user));
                            BaseViewModel modelStatus = new BaseViewModel();

                            modelStatus.Flash.Add(new FlashMessage()
                            {
                                Type = FlashMessageType.Success,
                                Message = i18n.Resources.UserSignupMessage
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
                User userUsed = UoW.UserRepository.GetByUsernameCached(model.Username?.Trim());

                if (userUsed != null
                 && CurrentUser.ID != userUsed.ID)
                {
                    ModelState.AddModelError(string.Empty, i18n.Resources.UsernameUsed);
                }
                else
                {

                    CurrentUser.FirstName = model.FirstName;
                    CurrentUser.Username = model.Username.ToLower();
                    CurrentUser.LastName = model.LastName;
                    CurrentUser.Phone = model.Phone;
                    CurrentUser.Country = model.Country;
                    CurrentUser.BirthDay = model.Birthday;

                    if (!string.IsNullOrEmpty(model.NewPassord))
                    {

                        if (!PasswordHasher.Equals(model.OldPassword?.Trim(), CurrentUser.Salt, CurrentUser.Password))
                        {
                            Passphrase pwd = PasswordHasher.Hash(model.NewPassord);

                            CurrentUser.Password = pwd.Hash;
                            CurrentUser.Salt = pwd.Salt;
                        }

                    }


                    if (CurrentUser.Role == UserRoleType.LicenseAdmin)
                    {
                        CurrentUser.Organization.Address = model.Address;
                    }
                    UoW.Save();
                    UoW.UserRepository.SetCache(CurrentUser);

                    model.Flash.Add(new FlashMessage()
                    {
                        Type = FlashMessageType.Success,
                        Message = i18n.Resources.ProfileSaveMessage
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
            else
            {
                if (IsAuth)
                {
                    switch (CurrentUser.Role)
                    {
                        case UserRoleType.Admin:
                            return RedirectToAction("Index", "Admin");
                        case UserRoleType.Analyst:
                            return RedirectToAction("Index", "Analyst");
                        case UserRoleType.LicenseAdmin:
                            return RedirectToAction("Index", "Organization");
                        case UserRoleType.User:
                        default:
                            return RedirectToAction("Index", "Default");
                    }
                }
                return RedirectToAction("Signin", "Account");
            }
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

                    Task.Run(() => Mailer.SendActivatedEmail(user));

                    model.Flash.Add(new FlashMessage()
                    {
                        Type = FlashMessageType.Success,
                        Message = i18n.Resources.Confirmed
                    });

                }
                else
                {
                    model.Flash.Add(new FlashMessage()
                    {
                        Type = FlashMessageType.Error,
                        Message = i18n.Resources.WrongConfirmationToken
                    });
                }
            }
            else
            {
                model.Flash.Add(new FlashMessage()
                {
                    Type = FlashMessageType.Error,
                    Message = i18n.Resources.EmptyConfirmationToken
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
                     && user.ForgotExpiration.Value >= DateTime.Now)
                    {
                        ForgotViewModel forgetModel = new ForgotViewModel();
                        forgetModel.ForgetToken = token?.Trim();
                        return View("Forgot", forgetModel);
                    }
                    else
                    {
                        model.Flash.Add(new FlashMessage()
                        {
                            Type = FlashMessageType.Error,
                            Message = i18n.Resources.ExpiredForgotToken
                        });
                    }

                }
                else
                {
                    model.Flash.Add(new FlashMessage()
                    {
                        Type = FlashMessageType.Error,
                        Message = i18n.Resources.WrongForgotToken
                    });
                }

            }
            else
            {
                model.Flash.Add(new FlashMessage()
                {
                    Type = FlashMessageType.Warning,
                    Message = i18n.Resources.EmptyForgotToken
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
                     && user.ForgotExpiration.Value >= DateTime.Now)
                    {
                        Passphrase pwd = PasswordHasher.Hash(model.Password?.Trim());

                        user.Password = pwd.Hash;
                        user.Salt = pwd.Salt;
                        user.ForgotToken = null;
                        user.ForgotExpiration = null;

                        UoW.Save();
                        UoW.UserRepository.SetCache(user);

                        baseModel.Flash.Add(new FlashMessage()
                        {
                            Type = FlashMessageType.Success,
                            Message = i18n.Resources.PasswordSuccessufullyChanged
                        });
                    }
                    else
                    {
                        baseModel.Flash.Add(new FlashMessage()
                        {
                            Type = FlashMessageType.Error,
                            Message = i18n.Resources.ExpiredForgotToken
                        });
                    }

                }
                else
                {
                    baseModel.Flash.Add(new FlashMessage()
                    {
                        Type = FlashMessageType.Error,
                        Message = i18n.Resources.WrongForgotToken
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

                    Task.Run(() => Mailer.SendForgotPasswordEmail(user));

                    baseModel.Flash.Add(new FlashMessage()
                    {
                        Type = FlashMessageType.Success,
                        Message = i18n.Resources.PasswordSuccessufullySent
                    });

                }
                else
                {
                    baseModel.Flash.Add(new FlashMessage()
                    {
                        Type = FlashMessageType.Error,
                        Message = i18n.Resources.WrongEmailForgotPassword
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


                    Task.Run(() => Mailer.SendForgotUsernameEmail(user));

                    baseModel.Flash.Add(new FlashMessage()
                    {
                        Type = FlashMessageType.Success,
                        Message = i18n.Resources.UsernameSuccessufullySent
                    });

                }
                else
                {
                    baseModel.Flash.Add(new FlashMessage()
                    {
                        Type = FlashMessageType.Error,
                        Message = i18n.Resources.WrongEmailForgotPassword
                    });
                }

                return View("ForgotStatus", baseModel);
            }

            return View(model);
        }
    }
}