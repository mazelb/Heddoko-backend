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