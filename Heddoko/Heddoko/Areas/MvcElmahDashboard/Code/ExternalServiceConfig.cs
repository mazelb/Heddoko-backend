/**
 * @file ExternalServiceConfig.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 12 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;

namespace Heddoko.Areas.MvcElmahDashboard.Code
{
    public class ExternalServiceConfig : DynamicObject
    {
        private Dictionary<string, string> config = new Dictionary<string, string>();

        public ExternalServiceConfig(string configuration)
        {
            if (configuration != null)
            { 
                foreach(var pair in configuration.Split(';').Select(p => p.Split(new char[] {'='}, 2)))
                {
                    config[pair[0]] = pair[1];
                }
            }
        }

        public string this[string property]
        {
            get
            {
                string value;
                if (config.TryGetValue(property, out value))
                {
                    return value;
                }
                else
                {
                    return null;
                }
            }
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = this[binder.Name];
            return true;
        }
    }
}