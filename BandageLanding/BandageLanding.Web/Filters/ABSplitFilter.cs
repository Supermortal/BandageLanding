using System.Web.Mvc;
using System.Web.Routing;
using log4net;
using Supermortal.Common.Web.Helpers.Log;

namespace BandageLanding.Filters
{
    public class ABSplitFilter : ActionFilterAttribute
    {

        private static readonly ILog Log = LogHelper.GetLogger(typeof (ABSplitFilter));

        private static readonly string[] Routes = 
            {
                "Home/Index", 
                "Home/About"       
            };

        public ABSplitFilter()
        {

        }

        private static int _actionIndex = 0;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var controllerName = filterContext.RouteData.Values["controller"].ToString();
            var actionName = filterContext.RouteData.Values["action"].ToString();

            var route = Routes[_actionIndex++];

            if (_actionIndex == Routes.Length)
                _actionIndex = 0;

            var parts = route.Split('/');
            var con = parts[0];
            var act = parts[1];

            if (con == controllerName && act == actionName)
                return;

            var rvd = new RouteValueDictionary
            {
                {"controller", con},
                {"action", act}
            };

            filterContext.Result = new RedirectToRouteResult(rvd);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
           
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            
        }

    }
}
