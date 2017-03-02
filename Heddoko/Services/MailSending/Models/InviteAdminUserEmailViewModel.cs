/**
 * @file InviteAdminUserEmailViewModel.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 12 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
namespace Services.MailSending.Models
{
    public class InviteAdminUserEmailViewModel : EmailViewModel
    {
        public string FirstName { get; set; }

        public string OrganizationName { get; set; }

        public string Token { get; set; }

        public string ActivationUrl { get; set; }
    }
}