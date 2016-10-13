using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class EmailServiceActivator
    {
        public static EmailService Create()
        {
            return new EmailService(new DemoSender());
        }
    }
}
