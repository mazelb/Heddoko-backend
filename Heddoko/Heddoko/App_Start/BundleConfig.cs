﻿/**
 * @file BundleConfig.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System.Web.Optimization;

namespace Heddoko
{
    public static class BundleConfig
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
                "~/Content/font-awesome.min.css",
                "~/Content/bootstrap.min.css",
                "~/Content/bootstrap-datepicker3.min.css",
                "~/Content/css/general.css",
                "~/Content/bootstrap-heddoko.css"
                );

            bundles.Add(commonStylesBundle);
            
            ScriptBundle commonScriptsBundle = new ScriptBundle("~/Bundles/CommonJs");
            commonScriptsBundle.Include(
                "~/Scripts/lodash.min.js",
                "~/Scripts/bootstrap.min.js",
                "~/Scripts/bootstrap-datepicker.min.js",
                "~/Scripts/jquery.slimscroll.min.js",
                "~/Scripts/jquery.mask.min.js",
                "~/Scripts/js/menu.js",
                "~/Scripts/js/general.js",
                "~/Scripts/js/account/mask.js"
                );
            bundles.Add(commonScriptsBundle);

            ScriptBundle validateScriptsBundle = new ScriptBundle("~/Bundles/ValidateJs");
            validateScriptsBundle.Include(
                "~/Scripts/jquery.validate.min.js",
                "~/Scripts/jquery.validate.unobtrusive.min.js",
                "~/Scripts/jquery.validate.unobtrusive.bootstrap.min.js"
                );
            bundles.Add(validateScriptsBundle);


            Bundle kendoStylesBundle = new Bundle("~/Bundles/KendoCss");
            kendoStylesBundle.Include(
                "~/Content/kendo/kendo.common.min.css",
                "~/Content/kendo/kendo.default.min.css",
                "~/Content/kendo/kendo.default.mobile.min.css",
                "~/Content/css/backend.css",
                "~/Content/kendo/kendo.heddoko.css",
                "~/Content/css/heddoko.css"
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
                "~/Scripts/js/admin/grid/kits.js",
                "~/Scripts/js/admin/grid/organization.js",
                "~/Scripts/js/admin/grid/organizationKits.js",
                "~/Scripts/js/admin/grid/teams.js",
                "~/Scripts/js/admin/grid/applications.js",             
                "~/Scripts/js/account/ergoscore.js"           
            );

            bundles.Add(kendoScriptsBundle);

            Bundle adminScriptsBundle = new Bundle("~/Bundles/AdminJs");
            adminScriptsBundle.Include(
                "~/Scripts/js/admin/grid/organizations.js",
                "~/Scripts/js/admin/grid/usersAdmin.js",
                "~/Scripts/js/admin/grid/equipments.js",
                "~/Scripts/js/admin/grid/databoards.js",
                "~/Scripts/js/admin/grid/powerboards.js",
                "~/Scripts/js/admin/grid/brainpacks.js",
                "~/Scripts/js/admin/grid/firmwares.js",
                "~/Scripts/js/admin/grid/pantsOctopi.js",
                "~/Scripts/js/admin/grid/pants.js",
                "~/Scripts/js/admin/grid/shirtsOctopi.js",
                "~/Scripts/js/admin/grid/shirts.js",
                "~/Scripts/js/admin/grid/components.js",
                "~/Scripts/js/admin/grid/sensors.js",
                "~/Scripts/js/admin/grid/sensorSets.js",
                "~/Scripts/js/admin/grid/historyPopup.js",
                "~/Scripts/js/admin/grid/applicationsApprove.js"
            );

            bundles.Add(adminScriptsBundle);

            Bundle analystScriptsBundle = new Bundle("~/Bundles/ErgoScoreJs");
            analystScriptsBundle.Include(
                "~/Scripts/js/ergoscore/ergoscores.js"
            );

            bundles.Add(analystScriptsBundle);

            bundles.Add(new ScriptBundle("~/Bundles/i18nEn")
                .Include(
                    "~/Scripts/i18n/Resources.js"
                ));

            BundleTable.EnableOptimizations = false;
        }
    }
}