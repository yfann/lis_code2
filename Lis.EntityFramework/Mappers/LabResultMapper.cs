using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Lis.Domain.Entities;

namespace Csh.Hcis.GC.VueScheduler.EntityFramework.Mappers
{
    public class LabResultMapper : EntityTypeConfiguration<LabResult>
    {
        public LabResultMapper()
        {
            ToTable("tLabResult");
            HasKey(e => e.Id);

            Property(e => e.Id).IsRequired().HasMaxLength(50);
            Property(e => e.Version).IsRequired().IsRowVersion();
            Property(e => e.CreateUserId).IsRequired().HasMaxLength(50);
            Property(e => e.CreateTime).IsRequired();
            Property(e => e.LastUpdateUserId).IsRequired().HasMaxLength(50);
            Property(e => e.LastUpdateTime).IsRequired();

            Property(e => e.LmId).IsRequired().HasMaxLength(50);
            Property(e => e.ReId).IsRequired().HasMaxLength(50);
            Property(e => e.EnglishName).IsRequired().HasMaxLength(50);
            Property(e => e.ChtName).IsRequired().HasMaxLength(50);
            Property(e => e.ResultValue).IsOptional().HasMaxLength(50);
            Property(e => e.RefLo).IsRequired().HasMaxLength(50);
            Property(e => e.RefHi).IsRequired().HasMaxLength(50);
            Property(e => e.RefRange).IsRequired().HasMaxLength(50);
            Property(e => e.ResultFlag).IsOptional().HasMaxLength(50);
            Property(e => e.Unit).IsOptional().HasMaxLength(50);
            Property(e => e.SeqNo).IsRequired();
            Property(e => e.Hint).IsOptional().HasMaxLength(50);
            Property(e => e.GermResult).IsOptional().HasMaxLength(50);
        }
    }
}