using Lis.Common;
using Lis.Common.Utils;
using Lis.Domain.Entities;
using Lis.EntityFramework.Interfaces;
using System;

namespace Lis.Service.ServiceImpl.IntialData.Seed
{
    public class Seed1001 : SeedServiceBase
    {
        public Seed1001(ILisContext context)
            : base(context, Version.V1001)
        {
        }

        protected override void Initialize()
        {
        }
    }
}
