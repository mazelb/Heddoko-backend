/**
 * @file ErrorViewModel.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System;

namespace Heddoko.Models
{
    public class ErrorViewModel : BaseViewModel
    {
        public Exception Ex { get; set; }

        public string ExMessage => Ex?.Message ?? Message;

        public string Url { get; set; }
        public string Message { get; set; }
    }
}