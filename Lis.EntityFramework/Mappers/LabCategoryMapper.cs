using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Lis.Domain.Entities;

namespace Csh.Hcis.GC.VueScheduler.EntityFramework.Mappers
{
    public class LabCategoryMapper : EntityTypeConfiguration<LabCategory>
    {
        public LabCategoryMapper()
        {
            ToTable("tLabCategory");
            HasKey(e => e.Id);

            Property(e => e.Id).IsRequired().HasMaxLength(50);
            Property(e => e.Version).IsRequired().IsRowVersion();
            Property(e => e.CreateUserId).IsRequired().HasMaxLength(50);
            Property(e => e.CreateTime).IsRequired();
            Property(e => e.LastUpdateUserId).IsRequired().HasMaxLength(50);
            Property(e => e.LastUpdateTime).IsRequired();

            Property(e => e.LcCode).IsOptional().HasMaxLength(50);
            Property(e => e.LcName).IsRequired().HasMaxLength(255);
            Property(e => e.BarcodePre).IsOptional().HasMaxLength(50);
            Property(e => e.ExternalCode).IsOptional().HasMaxLength(255);
            Property(e => e.Color).IsOptional().HasMaxLength(50);
            Property(e => e.BooldAlone).IsOptional();
            Property(e => e.ExamNum).IsOptional();
        }
    }
}