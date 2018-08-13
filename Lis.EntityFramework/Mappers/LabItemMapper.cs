using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Lis.Domain.Entities;

namespace Csh.Hcis.GC.VueScheduler.EntityFramework.Mappers
{
    public class LabItemMapper : EntityTypeConfiguration<LabItem>
    {
        public LabItemMapper()
        {
            ToTable("tLabItem");
            HasKey(e => e.Id);

            Property(e => e.Id).IsRequired().HasMaxLength(50);
            Property(e => e.Version).IsRequired().IsRowVersion();
            Property(e => e.CreateUserId).IsRequired().HasMaxLength(50);
            Property(e => e.CreateTime).IsRequired();
            Property(e => e.LastUpdateUserId).IsRequired().HasMaxLength(50);
            Property(e => e.LastUpdateTime).IsRequired();

            Property(e => e.LcId).IsRequired().HasMaxLength(50);
            Property(e => e.ItemCode).IsOptional().HasMaxLength(255);
            Property(e => e.StandardCode).IsOptional().HasMaxLength(255);
            Property(e => e.ItemName).IsRequired().HasMaxLength(255);
            Property(e => e.Category).IsOptional().HasMaxLength(255);
            Property(e => e.ResultType).IsOptional();
            Property(e => e.Unit).IsOptional().HasMaxLength(50);
            Property(e => e.LifeLimit).IsOptional().HasPrecision(5, 2);
            Property(e => e.DefValue).IsRequired().HasMaxLength(50);
            Property(e => e.TypeCode1).IsOptional().HasMaxLength(50);
            Property(e => e.TypeCode2).IsOptional().HasMaxLength(50);
            Property(e => e.Important).IsOptional();
            Property(e => e.Associated).IsOptional();
            Property(e => e.ConditionAudit).IsOptional();
            Property(e => e.Comment).IsOptional();
            Property(e => e.Display).IsRequired();
            Property(e => e.Precision).IsOptional().HasPrecision(14, 4);
            Property(e => e.Price).IsOptional().HasPrecision(18,2);
            Property(e => e.CanZero).IsOptional();
            Property(e => e.CanLessZero).IsOptional();
            Property(e => e.MeanOfclinic).IsOptional().HasMaxLength(null);
            Property(e => e.Desc).IsOptional().HasMaxLength(null);
        }
    }
}