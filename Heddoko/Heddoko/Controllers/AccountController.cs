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
                User user = UoW.UserRepository.GetByEmailCached(model.Email?.Trim());
                if (user != null)
                {
                    ModelState.AddModelError(string.Empty, i18n.Resources.EmailUsed);
                }

                user = UoW.UserRepository.GetByUsernameCached(model.Username?.Trim());

                if (user != null)
                {
                    ModelState.AddModelError(string.Empty, i18n.Resources.UsernameUsed);
                }

                user = new User();
                user.Email = model.Email.Trim();
                user.Username = model.Username.Trim();
                if (model.Role == UserRoleType.User
                 || model.Role == UserRoleType.Analyst)
                {
                    user.Role = model.Role;
                }
                else
                {
                    user.Role = UserRoleType.User;
                }
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Country = model.Country;
                user.BirthDay = model.Birthday;
                user.Phone = model.Phone;
                user.Status = UserStatusType.NotActive;
                Passphrase pwd = PasswordHasher.Hash(model.Password);

                user.Password = pwd.Hash;
                user.Salt = pwd.Salt;

                user.ConfirmToken = PasswordHasher.Md5(DateTime.Now.Ticks.ToString());

                UoW.UserRepository.Create(user);

                Task.Run(() => Mailer.SendActivationEmail(user));
                BaseViewModel modelStatus = new BaseViewModel();

                modelStatus.Flash.Add(new FlashMessage()
                {
                    Type = FlashMessageType.Success,
                    Message = i18n.Resources.UserSignupMessage
                });
                return View("SignUpStatus", modelStatus);

            }
            return View(model);
        }

        [Auth]
        public ActionResult SignOut()
        {
            Forms.SignOut();
            return RedirectToAction("SignIn", "Account");
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
                            return RedirectToAction("Index", "Default");
                        case UserRoleType.Analyst:
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

                    UoW.UserRepository.Update();
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

                        UoW.UserRepository.Update();
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

                    UoW.UserRepository.Update();
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