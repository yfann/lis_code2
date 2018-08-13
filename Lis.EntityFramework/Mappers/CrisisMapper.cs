using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Lis.Domain.Entities;

namespace Lis.EntityFramework.Mappers
{
    public class CrisisMapper : EntityTypeConfiguration<Crisis>
    {
        public CrisisMapper()
        {
            ToTable("tCrisis");
            HasKey(e => e.Id);

            Property(e => e.Id).IsRequired().HasMaxLength(50);
            Property(e => e.Version).IsRequired().IsRowVersion();
            Property(e => e.CreateUserId).IsRequired().HasMaxLength(50);
            Property(e => e.CreateTime).IsRequired();
            Property(e => e.LastUpdateUserId).IsRequired().HasMaxLength(50);
            Property(e => e.LastUpdateTime).IsRequired();

            Property(e => e.LmId).IsRequired().HasMaxLength(50);
            Property(e => e.NormalUpper).IsRequired().HasPrecision(18, 2);
            Property(e => e.NormalLow).IsRequired().HasPrecision(18, 2);
            Property(e => e.CrisisUpper).IsRequired().HasPrecision(18, 2);
            Property(e => e.CrisisLow).IsRequired().HasPrecision(18, 2);
            Property(e => e.CrisisClinical).IsOptional().HasMaxLength(null);
            Property(e => e.ClinicasSignificance).IsOptional().HasMaxLength(null);
        }
    }
}