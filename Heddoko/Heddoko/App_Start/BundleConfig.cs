﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace Heddoko
{
    public class BundleConfig
    {

        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.UseCdn = false;

            bundles.Add(new ScriptBundle("~/Bundles/jQuery")
                   .Include(
                "~/Scripts/kendo/jquery.min.js"
            ));

            Bundle commonStylesBundle = new Bundle("~/Bundles/CommonCss");
            commonStylesBundle.Include(
                 "~/Content/css/styles.css",
                 "~/Content/bootstrap-datepicker3.min.css",
                 "~/Content/css/app.css",
                 "~/Content/css/general.css"
            );

            bundles.Add(commonStylesBundle);

            Bundle angularStylesBundle = new Bundle("~/Bundles/AngularCss");
            angularStylesBundle.Include(
                 "~/Content/css/styles.css"
            );

            bundles.Add(angularStylesBundle);

            ScriptBundle commonScriptsBundle = new ScriptBundle("~/Bundles/CommonJs");
            commonScriptsBundle.Include(
               "~/Scripts/lodash.min.js",
               "~/Scripts/bootstrap.min.js",
               "~/Scripts/bootstrap-datepicker.min.js",
               "~/Scripts/js/general.js"
            );
            bundles.Add(commonScriptsBundle);


            ScriptBundle angularScriptsBundle = new ScriptBundle("~/Bundles/AngularJs");
            angularScriptsBundle.Include(
               "~/Scripts/js/scripts.js"
            );
            bundles.Add(angularScriptsBundle);

            ScriptBundle validateScriptsBundle = new ScriptBundle("~/Bundles/ValidateJs");
            validateScriptsBundle.Include(
               "~/Scripts/jquery.validate.min.js",
               "~/Scripts/jquery.validate.unobtrusive.min.js",
               "~/Scripts/jquery.validate.unobtrusive.bootstrap.min.js"
            );
            bundles.Add(validateScriptsBundle);


            Bundle kendoStylesBundle = new Bundle("~/Bundles/KendoCss");
            kendoStylesBundle.Include(
               "~/Content/bootstrap.min.css",
               "~/Content/kendo/kendo.common.min.css",
               "~/Content/kendo/kendo.default.min.css",
               "~/Content/kendo/kendo.default.mobile.min.css",
               "~/Content/css/kendo.css"
            );

            bundles.Add(kendoStylesBundle);

            Bundle kendoScriptsBundle = new Bundle("~/Bundles/KendoJs");
            kendoScriptsBundle.Include(
                "~/Scripts/kendo/kendo.all.min.js",
                "~/Scripts/js/admin/format.js",
                "~/Scripts/js/admin/notification.js",
                "~/Scripts/js/admin/kendo.js",
                "~/Scripts/js/admin/ajax.js",
                "~/Scripts/js/admin/validator.js",
                "~/Scripts/js/admin/grid/users.js",
                "~/Scripts/js/admin/grid/licenses.js",
                "~/Scripts/js/admin/grid/organization.js"
            );

            bundles.Add(kendoScriptsBundle);

            Bundle adminScriptsBundle = new Bundle("~/Bundles/AdminJs");
            adminScriptsBundle.Include(
             "~/Scripts/js/admin/grid/organizations.js",
             "~/Scripts/js/admin/grid/usersAdmin.js",
             "~/Scripts/js/admin/grid/pantsOctopi.js"
          );

            bundles.Add(adminScriptsBundle);

            bundles.Add(new ScriptBundle("~/Bundles/i18nEn")
              .Include(
               "~/Scripts/i18n/Resources.js"
            ));

            BundleTable.EnableOptimizations = false;
        }
    }
}