using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Heddoko
{
    public class ContextTempData
    {
        private const string FlashMessageKey = "FlashMessage";
        public TempDataDictionary TempData { get; protected set; }

        public ContextTempData(TempDataDictionary tempData)
        {
            TempData = tempData;
        }

        public FlashMessage FlashMessage
        {
            get
            {
                return TempData.Get<FlashMessage>(FlashMessageKey);
            }
            set
            {
                TempData.Set(FlashMessageKey, value);
            }
        }
    }
}
