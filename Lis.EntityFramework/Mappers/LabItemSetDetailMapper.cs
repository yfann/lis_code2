using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Lis.Domain.Entities;

namespace Csh.Hcis.GC.VueScheduler.EntityFramework.Mappers
{
    public class LabItemSetDetailMapper : EntityTypeConfiguration<LabItemSetDetail>
    {
        public LabItemSetDetailMapper()
        {
            ToTable("tLabItemSetDetail");
            HasKey(e => e.Id);

            Property(e => e.Id).IsRequired().HasMaxLength(50);
            Property(e => e.LmId).IsRequired().HasMaxLength(50);
            Property(e => e.SetId).IsRequired().HasMaxLength(50);
        }
    }
}