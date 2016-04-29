﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;
using System.Data.SqlClient;
using System.Diagnostics;
using Heddoko.Models;

namespace Heddoko.Helpers.Error
{
    public class ErrorMessage
    {
        public static string Get(string message)
        {
            if (message.Contains("IX_Identifier", StringComparison.InvariantCultureIgnoreCase))
            {
                return $"{i18n.Resources.CannotAddDuplicate} {i18n.Resources.Identifier}";
            }
            return message;

        }

        public static List<ErrorAPIViewModel> Get(Exception exception, Guid guid, bool isOriginal = true, string key = null)
        {
            List<ErrorAPIViewModel> errors = new List<ErrorAPIViewModel>();
            if (exception != null)
            {
                Exception ex = exception;
                if (isOriginal)
                {
                    ex = exception.GetOriginalException();
                }

                if (ex is ModelStateException)
                {
                    foreach (var item in ((ModelStateException)ex).ModelState)
                    {
                        foreach (var error in item.Value.Errors)
                        {
                            Trace.TraceError("ErrorMessage.Get.ModelStateException.{0} Code:{1} Message:{2}", guid, item.Key, error.ErrorMessage);
                            if (!string.IsNullOrEmpty(error.ErrorMessage))
                            {
                                errors.Add(new ErrorAPIViewModel()
                                {
                                    Code = item.Key,
                                    Message = Get(error.ErrorMessage)
                                });
                            }
                            if (error.Exception != null)
                            {
                                errors.AddRange(Get(error.Exception, guid, false, item.Key));
                            }
                        }
                    }
                }
                else if (ex is SqlException)
                {
                    Trace.TraceError("ErrorMessage.Get.SqlException.{0} Code:{1} Message:{2}", guid, key, ex.Message);
                    errors.Add(new ErrorAPIViewModel()
                    {
                        Code = key,
                        Message = Get(ex.Message)
                    });
                }
                else if (ex is APIException)
                {
                    Trace.TraceError("ErrorMessage.Get.APIException.{0} Code:{1} Message:{2}", guid, ((APIException)ex).Code.ToString(), ex.Message);
                    errors.Add(new ErrorAPIViewModel()
                    {
                        Code = ((APIException)ex).Code.ToString(),
                        Message = Get(ex.Message)
                    });
                }
                else
                {
                    Trace.TraceError("ErrorMessage.Get.{0} Code:{1} Message:{2}", guid, key, ex.Message);
                    errors.Add(new ErrorAPIViewModel()
                    {
                        Code = key,
                        Message = Get(ex.Message)
                    });
                }

            }
            return errors;
        }
    }
}