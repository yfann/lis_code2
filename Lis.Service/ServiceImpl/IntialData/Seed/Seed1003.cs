using Lis.Common;
using Lis.Common.Utils;
using Lis.Domain.Entities;
using Lis.EntityFramework.Interfaces;
using System;
using System.Linq;
using System.Data.Entity;

namespace Lis.Service.ServiceImpl.IntialData.Seed
{
    public class Seed1003 : SeedServiceBase
    {
        public Seed1003(ILisContext context)
            : base(context, Version.V1003)
        {
        }

        protected override void Initialize()
        {
            var users = _context.Set<Employee>().ToList();
            foreach(var user in users)
            {
                var passwordText = EncryptUtil.AesDecrypt(user.Password);
                if (!string.IsNullOrEmpty(passwordText))
                {
                    user.Password = EncryptUtil.Md5Hash(passwordText);
                }
            }
        }
    }
}
