using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Web.Http.ModelBinding;
using DAL;
using Heddoko.Models;
using i18n;

namespace Heddoko.Helpers.Error
{
    public static class ErrorMessage
    {
        private static string Get(string message)
        {
            if (message.Contains("IX_Identifier", StringComparison.InvariantCultureIgnoreCase))
            {
                return $"{Resources.CannotAddDuplicate} {Resources.Identifier}";
            }
            if (message.Contains("FK_dbo.Equipments_dbo.Materials_MaterialID", StringComparison.InvariantCultureIgnoreCase))
            {
                return $"{Resources.CannotRemove} {Resources.Equipment} {Resources.Use} {Resources.Material}";
            }
            if (message.Contains("FK_dbo.Materials_dbo.MaterialTypes_MaterialTypeID", StringComparison.InvariantCultureIgnoreCase))
            {
                return $"{Resources.CannotRemove} {Resources.Material} {Resources.Use} {Resources.MaterialType}";
            }
            if (message.Contains("IX_Name", StringComparison.InvariantCultureIgnoreCase))
            {
                return $"{Resources.CannotAddDuplicate} {Resources.Name}";
            }
            if (message.Contains("IX_PartNo", StringComparison.InvariantCultureIgnoreCase))
            {
                return $"{Resources.CannotAddDuplicate} {Resources.PartNo}";
            }
            if (message.Contains("IX_MacAddress", StringComparison.InvariantCultureIgnoreCase))
            {
                return $"{Resources.CannotAddDuplicate} {Resources.MacAddress}";
            }
            if (message.Contains("IX_SerialNo", StringComparison.InvariantCultureIgnoreCase))
            {
                return $"{Resources.CannotAddDuplicate} {Resources.SerialNo}";
            }
            if (message.Contains("idx_label_notnull", StringComparison.InvariantCultureIgnoreCase))
            {
                return $"{Resources.CannotAddDuplicate} {Resources.Label}";
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
                    foreach (KeyValuePair<string, ModelState> item in ((ModelStateException) ex).ModelState)
                    {
                        foreach (ModelError error in item.Value.Errors)
                        {
                            Trace.TraceError("ErrorMessage.Get.ModelStateException.{0} Code:{1} Message:{2}", guid, item.Key, error.ErrorMessage);
                            if (!string.IsNullOrEmpty(error.ErrorMessage))
                            {
                                errors.Add(new ErrorAPIViewModel
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
                    errors.Add(new ErrorAPIViewModel
                    {
                        Code = key,
                        Message = Get(ex.Message)
                    });
                }
                else if (ex is APIException)
                {
                    Trace.TraceError("ErrorMessage.Get.APIException.{0} Code:{1} Message:{2}", guid, ((APIException) ex).Code, ex.Message);
                    errors.Add(new ErrorAPIViewModel
                    {
                        Code = ((APIException) ex).Code.ToString(),
                        Message = Get(ex.Message)
                    });
                }
                else
                {
                    Trace.TraceError("ErrorMessage.Get.{0} Code:{1} Message:{2}", guid, key, ex.Message);
                    errors.Add(new ErrorAPIViewModel
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