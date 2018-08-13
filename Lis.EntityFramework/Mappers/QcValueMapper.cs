using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Lis.Domain.Entities;

namespace Csh.Hcis.GC.VueScheduler.EntityFramework.Mappers
{
    public class QcValueMapper : EntityTypeConfiguration<QcValue>
    {
        public QcValueMapper()
        {
            ToTable("tQcValue");
            HasKey(e => e.Id);

            Property(e => e.Id).IsRequired().HasMaxLength(50);
            Property(e => e.Version).IsRequired().IsRowVersion();
            Property(e => e.CreateUserId).IsRequired().HasMaxLength(50);
            Property(e => e.CreateTime).IsRequired();
            Property(e => e.LastUpdateUserId).IsRequired().HasMaxLength(50);
            Property(e => e.LastUpdateTime).IsRequired();

            Property(e => e.LmId).IsRequired().HasMaxLength(50);
            Property(e => e.MiId).IsRequired().HasMaxLength(50);

            Property(e => e.InstrumentId).IsRequired().HasMaxLength(50);
            Property(e => e.InstrumentName).IsOptional().HasMaxLength(500);
            Property(e => e.Qcer).IsRequired().HasMaxLength(50);
            Property(e => e.QcTime).IsRequired();

            Property(e => e.QcNum).IsRequired();
            Property(e => e.Value).IsRequired();
            Property(e => e.Comment).IsOptional().HasMaxLength(null);
            Property(e => e.Other1).IsOptional().HasMaxLength(null);
            Property(e => e.Other2).IsOptional().HasMaxLength(null);
            Property(e => e.Other3).IsOptional().HasMaxLength(null);
            Property(e => e.Other4).IsOptional().HasMaxLength(null);
            Property(e => e.Other5).IsOptional().HasMaxLength(null);
            Property(e => e.Other6).IsOptional().HasMaxLength(null);
            
        }
    }
}