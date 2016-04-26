using System;
using System.Web.Http.ModelBinding;

namespace Heddoko
{
    public class ModelStateException : Exception
    {
        public ModelStateDictionary ModelState { get; set; }
    }
}