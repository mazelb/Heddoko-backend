using System.Web.Mvc;
using DAL.Models;

namespace Heddoko
{
    public class ContextTempData
    {
        private const string FlashMessageKey = "FlashMessage";

        public ContextTempData(TempDataDictionary tempData)
        {
            TempData = tempData;
        }

        private TempDataDictionary TempData { get; }

        public FlashMessage FlashMessage
        {
            get { return TempData.Get<FlashMessage>(FlashMessageKey); }
            set { TempData.Set(FlashMessageKey, value); }
        }
    }
}