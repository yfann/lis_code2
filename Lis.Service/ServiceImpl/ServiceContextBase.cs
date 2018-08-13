using Lis.Common;
using Lis.Common.Logger;
using Lis.Common.Utils;
using Lis.Domain.Entities;
using Newtonsoft.Json;
using System.Threading;
using System.Web;

namespace Lis.Service.ServiceImpl
{
    public class ServiceContextBase
    {
        private ISystemLogger logger = new SystemLogger();
        private ServiceContext serviceContext;
        public ServiceContext ServiceContext
        {
            get
            {
                if (serviceContext == null && HttpContext.Current != null)
                {
                    var cookies = HttpContext.Current.Request.Cookies;
                    var userName = Thread.CurrentPrincipal.Identity.IsAuthenticated ? Thread.CurrentPrincipal.Identity.Name : "";

                    if(cookies!=null && cookies["user"]!=null && cookies["user"].Value != null)
                    {
                        var user = JsonConvert.DeserializeObject<Employee>(HttpUtility.UrlDecode(cookies["user"].Value));
                        if (user != null)
                        {
                            serviceContext = new ServiceContext()
                            {
                                UserId = user.Id,
                                Name = user.EmName,
                                SiteId = user.SiteId,
                                LoginName = user.EmCode
                            };
                        }
                    }
                }
                return serviceContext;
            }
        }
    }
}



