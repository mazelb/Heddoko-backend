/**
 * @file IsCurrentAction.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
namespace System.Web.Mvc
{
    public static class IsCurrentAction
    {
        public static MvcHtmlString IsCurrentActionLink(this HtmlHelper html, string action, string controller)
        {
            ViewContext context = html.ViewContext;

            if (context.Controller.ControllerContext.IsChildAction)
            {
                context = html.ViewContext.ParentActionViewContext;
            }
            string currentAction = (context.RouteData.Values["action"] ?? "").ToString().ToLower();
            string currentController = (context.RouteData.Values["controller"] ?? "").ToString().ToLower();
            string currentArea = (context.RouteData.DataTokens["area"] ?? "").ToString().ToLower();

            string str =
                currentAction.Equals(action.ToLower(), StringComparison.InvariantCulture)
                && currentController.Equals(controller.ToLower(), StringComparison.InvariantCulture)
                    ? " active "
                    : string.Empty;

            return new MvcHtmlString(str);
        }
    }
}