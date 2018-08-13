using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Lis.Domain.Entities;

namespace Csh.Hcis.GC.VueScheduler.EntityFramework.Mappers
{
    public class LogisticsMapper : EntityTypeConfiguration<Logistics>
    {
        public LogisticsMapper()
        {
            ToTable("tLogistics");
            HasKey(e => e.Id);

            Property(e => e.Id).IsRequired().HasMaxLength(50);
            Property(e => e.Version).IsRequired().IsRowVersion();
            Property(e => e.CreateUserId).IsRequired().HasMaxLength(50);
            Property(e => e.CreateTime).IsRequired();
            Property(e => e.LastUpdateUserId).IsRequired().HasMaxLength(50);
            Property(e => e.LastUpdateTime).IsRequired();

            Property(e => e.SendEmId).IsRequired().HasMaxLength(50);
            Property(e => e.SendTime).IsRequired();
            Property(e => e.LsEmId).IsOptional().HasMaxLength(50);
            Property(e => e.LsReceiveTime).IsOptional();
            Property(e => e.CenterEmId).IsOptional().HasMaxLength(50);
            Property(e => e.CenterReceiveTime).IsOptional();

            Property(e => e.LsStatus).IsRequired();
        }
    }
}