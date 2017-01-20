/**
 * @file ContextTempData.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
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