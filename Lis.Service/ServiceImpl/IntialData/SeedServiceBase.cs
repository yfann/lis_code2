using Lis.EntityFramework.Interfaces;
using Lis.Domain.Entities;
using Lis.Service.ServiceImpl.IntialData;
using Lis.Common.Logger;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Linq;
using Lis.Common;

namespace Lis.Service.ServiceImpl
{
    public abstract class SeedServiceBase
    {
        protected readonly ISystemLogger _logger = new SystemLogger();
        protected ILisContext _context { get; private set; }
        protected Version _version { get; private set; }

        protected SeedServiceBase(ILisContext context, Version version)
        {
            _context = context;
            _version = version;
        }

        protected abstract void Initialize();

        public void InitData()
        {
            var version = _context.Set<InitialDataHistory>().FirstOrDefault(s => s.Id == _version.VersionId);
            if (version == null || !version.IsUpdated)
            {
                #region Initialize

                // Each app's initial data go here
                Initialize();

                #endregion

                var target = _context.Set<InitialDataHistory>().Where(s => s.Id == _version.VersionId).FirstOrDefault();
                if (target != null)
                {
                    target.IsUpdated = true;
                }
                else
                {
                    var initialHistory = new InitialDataHistory
                    {
                        Id = _version.VersionId,
                        Version = _version.VersionNo,
                        Description = _version.Description,
                        IsUpdated = true,
                        LastEditUserId = Consts.SuperUserId.ToString(),
                        LastEditTime = System.DateTime.Now
                    };
                    _context.Set<InitialDataHistory>().Add(initialHistory);
                }

                try
                {
                    _context.SaveChanges();
                    _logger.Info("===============>    Seed_" + _version.VersionNo + " executed successful!");
                }
                catch (System.Exception e)
                {
                    _logger.Log(LogLevel.Error, "Initial data error: " + e.Message);
                }
            }
        }

    }
}
