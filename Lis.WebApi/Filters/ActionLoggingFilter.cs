using Lis.Common.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using Lis.WebApi.Utils;

namespace Lis.WebApi.Filters
{
    public class ActionLoggingFilter : ActionFilterAttribute
    {
        private ISystemLogger _Logger;

        public ActionLoggingFilter(ISystemLogger logger)
        {
            _Logger = logger;
        }

        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            _Logger.Log(LogLevel.Info, String.Format("Executed action named {0} for request {1} by {2} from {3}.",
                actionContext.ActionDescriptor.ActionName,
                actionContext.Request.RequestUri,
                "User",
                actionContext.Request.GetClientIp()));
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            var actionContext = actionExecutedContext.ActionContext;

            _Logger.Log(LogLevel.Info, String.Format("Executing action named {0} for request {1} by {2} from {3}",
                actionContext.ActionDescriptor.ActionName,
                actionContext.Request.RequestUri,
                "User",
                actionContext.Request.GetClientIp()));
        }
    }
}