using DAL.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class EmailService : IIdentityMessageService, IDisposable
    {
        public EmailService(IMailSender sender)
        {
            Sender = sender;
        }

        private IMailSender Sender { get; set; }

        public Task SendAsync(IdentityMessage message)
        {
            return Task.FromResult(0);
        }

        public void Dispose()
        {

        }
    }
}
