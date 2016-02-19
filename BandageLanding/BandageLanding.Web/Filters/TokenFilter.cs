using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using BandageLanding.Infrastructure.Abstract;
using Supermortal.Common.PCL.Helpers;
using Supermortal.Common.Web.Helpers.Log;

namespace BandageLanding.Filters
{
    public class TokenFilter : ActionFilterAttribute
    {

        private static readonly log4net.ILog Log = LogHelper.GetLogger
            (typeof(TokenFilter));

        private readonly ISessionService _ss;

        public TokenFilter()
        {
            _ss = (ISessionService)IoCHelper.Instance.GetService<ISessionService>();
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnActionExecuted(actionExecutedContext);
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            try
            {
                var attributes = actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>();
                if (attributes.Count > 0)
                {
                    base.OnActionExecuting(actionContext);
                    return;
                }

                var token = actionContext.Request.Headers.SingleOrDefault(h => h.Key == "FL-AUTH-TOKEN").Value.FirstOrDefault();

                if (string.IsNullOrEmpty(token) || (bool)!_ss.ValidateToken(token))
                    throw new Exception("Unauthorized access: " + actionContext.Request.RequestUri);

                base.OnActionExecuting(actionContext);
            }
            catch (Exception ex)
            {
                Log.Error("Unauthorized access: " + actionContext.Request.RequestUri + " Exception: " + ex.Message, ex);
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }
        }

    }
}