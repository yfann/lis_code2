using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Lis.Domain.Entities;

namespace Csh.Hcis.GC.VueScheduler.EntityFramework.Mappers
{
    public class SampleTypeMapper : EntityTypeConfiguration<SampleType>
    {
        public SampleTypeMapper()
        {
            ToTable("tSampleType");
            HasKey(e => e.Id);

            Property(e => e.Id).IsRequired().HasMaxLength(50);
            Property(e => e.Version).IsRequired().IsRowVersion();
            Property(e => e.CreateUserId).IsRequired().HasMaxLength(50);
            Property(e => e.CreateTime).IsRequired();
            Property(e => e.LastUpdateUserId).IsRequired().HasMaxLength(50);
            Property(e => e.LastUpdateTime).IsRequired();

            Property(e => e.ParentId).IsOptional().HasMaxLength(50);
            Property(e => e.Code).IsOptional().HasMaxLength(50);
            Property(e => e.ChtName).IsRequired().HasMaxLength(255);
            Property(e => e.EngName).IsOptional().HasMaxLength(255);
            Property(e => e.SeqNo).IsRequired();
            Property(e => e.HelpCode).IsOptional().HasMaxLength(50);
        }
    }
}