/**
 * @file FlashMessage.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
namespace DAL.Models
{
    public class FlashMessage
    {
        public FlashMessageType Type { get; set; }

        public string Message { get; set; }
    }
}