using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Lis.Domain.Entities;

namespace Csh.Hcis.GC.VueScheduler.EntityFramework.Mappers
{
    public class PatientMapper : EntityTypeConfiguration<Patient>
    {
        public PatientMapper()
        {
            ToTable("tPatient");
            HasKey(e => e.Id);

            Property(e => e.Id).IsRequired().HasMaxLength(50);
            Property(e => e.Version).IsRequired().IsRowVersion();
            Property(e => e.CreateUserId).IsRequired().HasMaxLength(50);
            Property(e => e.CreateTime).IsRequired();
            Property(e => e.LastUpdateUserId).IsRequired().HasMaxLength(50);
            Property(e => e.LastUpdateTime).IsRequired();

            Property(e => e.InnerId).IsOptional().HasMaxLength(50);
            Property(e => e.PtName).IsRequired().HasMaxLength(255);
            Property(e => e.ParentName).IsOptional().HasMaxLength(255);
            Property(e => e.Sex).IsOptional().HasMaxLength(50);
            Property(e => e.BirthDay).IsOptional();
            Property(e => e.Age).IsOptional().HasMaxLength(50);
            Property(e => e.Phone).IsOptional().HasMaxLength(50);
            Property(e => e.Address).IsOptional().HasMaxLength(255);
            Property(e => e.Desc).IsOptional().HasMaxLength(null);
            Property(e => e.Other1).IsOptional().HasMaxLength(64);
            Property(e => e.Other2).IsOptional().HasMaxLength(64);
            Property(e => e.Other3).IsOptional().HasMaxLength(64);
            Property(e => e.CreateDt).IsRequired();
        }
    }
}