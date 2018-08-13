using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Lis.Domain.Entities;

namespace Csh.Hcis.GC.VueScheduler.EntityFramework.Mappers
{
    public class MedicalInstitutionMapper : EntityTypeConfiguration<MedicalInstitution>
    {
        public MedicalInstitutionMapper()
        {
            ToTable("tMedicalInstitution");
            HasKey(e => e.Id);

            Property(e => e.Id).IsRequired().HasMaxLength(50);
            Property(e => e.Version).IsRequired().IsRowVersion();
            Property(e => e.CreateUserId).IsRequired().HasMaxLength(50);
            Property(e => e.CreateTime).IsRequired();
            Property(e => e.LastUpdateUserId).IsRequired().HasMaxLength(50);
            Property(e => e.LastUpdateTime).IsRequired();

            Property(e => e.MiCode).IsOptional().HasMaxLength(50);
            Property(e => e.MiName).IsRequired().HasMaxLength(255);

            Property(e => e.MiCategory).IsOptional().HasMaxLength(50);
            Property(e => e.AreaCode).IsOptional().HasMaxLength(50);
            Property(e => e.Address).IsOptional().HasMaxLength(500);
            Property(e => e.Desc).IsOptional().HasMaxLength(null);
            Property(e => e.ParentId).IsOptional().HasMaxLength(50);
            Property(e => e.ParentName).IsOptional().HasMaxLength(255);
        }
    }
}