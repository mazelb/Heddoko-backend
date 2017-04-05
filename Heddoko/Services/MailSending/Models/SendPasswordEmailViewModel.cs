/**
 * @file SendPasswordEmailViewModel.cs
 * @brief Functionalities required to operate it.
 * @author Ben Bailey (ben@heddoko.com)
 * @date 4 2017
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
namespace Services.MailSending.Models
{
    public class SendPasswordEmailViewModel : EmailViewModel
    {
        public string FirstName { get; set; }

        public string OrganizationName { get; set; }

        public string Password { get; set; }

        public string Username { get; set; }
    }
}
