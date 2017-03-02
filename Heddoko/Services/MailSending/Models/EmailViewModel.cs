/**
 * @file EmailViewModel.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 12 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
namespace Services.MailSending.Models
{
    public class EmailViewModel
    {
        public string Email { get; set; }

        public string Body { get; set; }
    }
}