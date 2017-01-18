/**
 * @file ModelStateException.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System;
using System.Web.Http.ModelBinding;

namespace Heddoko
{
    public class ModelStateException : Exception
    {
        public ModelStateDictionary ModelState { get; set; }
    }
}