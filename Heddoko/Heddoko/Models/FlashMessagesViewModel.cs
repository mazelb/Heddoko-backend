using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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