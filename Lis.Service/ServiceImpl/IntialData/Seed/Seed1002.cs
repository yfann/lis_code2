using Lis.Common;
using Lis.Common.Utils;
using Lis.Domain.Entities;
using Lis.EntityFramework.Interfaces;
using System;

namespace Lis.Service.ServiceImpl.IntialData.Seed
{
    public class Seed1002 : SeedServiceBase
    {
        public Seed1002(ILisContext context)
            : base(context, Version.V1002)
        {
        }

        protected override void Initialize()
        {
            // 添加默认用户：管理员
            var admin = new Employee();
            admin.Id = Guid.NewGuid().ToString();
            admin.CreateUserId = Consts.SuperUserId.ToString();
            admin.LastUpdateUserId = Consts.SuperUserId.ToString();
            admin.CreateTime = DateTime.Now;
            admin.LastUpdateTime = DateTime.Now;
            admin.EmCode = "admin";
            admin.EmName = "管理员";
            admin.SiteId = Consts.DefaultSiteId;
            admin.Password = EncryptUtil.AesEncrypt("666666");
            _context.Set<Employee>().Add(admin);
        }
    }
}
