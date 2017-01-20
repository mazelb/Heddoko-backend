/**
 * @file FlashMessagesViewModel.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System.Collections.Generic;
using DAL.Models;

namespace Heddoko.Models
{
    public class FlashMessagesViewModel
    {
        public FlashMessagesViewModel()
        {
            FlashMessages = new List<FlashMessage>();
        }

        public List<FlashMessage> FlashMessages { get; set; }

        public void Add(FlashMessage message)
        {
            FlashMessages.Add(message);
        }
    }
}