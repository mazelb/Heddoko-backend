/**
 * @file ForgotPasswordEmailViewModel.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 12 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
namespace Services.MailSending.Models
{
    public class ForgotPasswordEmailViewModel : EmailViewModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public string ResetPasswordUrl { get; set; }
    }
}