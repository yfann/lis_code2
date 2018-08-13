using Lis.Common.Logger;
using Lis.EntityFramework.Interfaces;
using Lis.Service.ServiceImpl.IntialData.Seed;


namespace Lis.Service.ServiceImpl
{
    public class InitialDataService : IInitialDataService
    {
        private static readonly ISystemLogger _logger = new SystemLogger();
        public void InitialData(ILisContext context)
        {
            new Seed1001(context).InitData();
            new Seed1002(context).InitData();
            new Seed1003(context).InitData();
        }
    }
}
