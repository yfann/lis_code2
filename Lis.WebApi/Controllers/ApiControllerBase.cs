using Lis.Common;
using Lis.Common.Utils;
using System.Threading;
using System.Web;
using System.Web.Http;

namespace Lis.WebApi.Controllers
{
    [AllowAnonymous]
    public class ApiControllerBase : ApiController
    {
        public ApiControllerBase()
        {
        }

        private ServiceContext serviceContext;
        public ServiceContext ServiceContext
        {
            get
            {
                if (serviceContext == null && HttpContext.Current != null)
                {
                    var cookies = HttpContext.Current.Request.Cookies;
                    var userName = Thread.CurrentPrincipal.Identity.IsAuthenticated ? Thread.CurrentPrincipal.Identity.Name : "";
                    serviceContext = new ServiceContext()
                    {
                        UserId = Consts.SuperUserId,
                        Name = "系统管理员",
                        SiteId = Consts.DefaultSiteId,
                        LoginName = "admin"
                    };
                }
                return serviceContext;
            }
        }
    }
}