using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Lis.Domain.Entities;

namespace Csh.Hcis.GC.VueScheduler.EntityFramework.Mappers
{
    public class LabItemSetMapper : EntityTypeConfiguration<LabItemSet>
    {
        public LabItemSetMapper()
        {
            ToTable("tLabItemSet");
            HasKey(e => e.Id);

            Property(e => e.Id).IsRequired().HasMaxLength(50);
            Property(e => e.Version).IsRequired().IsRowVersion();
            Property(e => e.CreateUserId).IsRequired().HasMaxLength(50);
            Property(e => e.CreateTime).IsRequired();
            Property(e => e.LastUpdateUserId).IsRequired().HasMaxLength(50);
            Property(e => e.LastUpdateTime).IsRequired();

            Property(e => e.LisCode).IsRequired().HasMaxLength(50);
            Property(e => e.LisName).IsRequired().HasMaxLength(50);
            Property(e => e.Comment).IsOptional().HasMaxLength(null);
        }
    }
}