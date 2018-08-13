using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Lis.Domain.Entities;

namespace Csh.Hcis.GC.VueScheduler.EntityFramework.Mappers
{
    public class ReportCrisisMapper : EntityTypeConfiguration<ReportCrisis>
    {
        public ReportCrisisMapper()
        {
            ToTable("tReportCrisis");
            HasKey(e => e.Id);

            Property(e => e.Id).IsRequired().HasMaxLength(50);

            Property(e => e.ReId).IsRequired().HasMaxLength(50);
            Property(e => e.LmId).IsRequired().HasMaxLength(50);
            Property(e => e.ResultValue).IsOptional().HasMaxLength(255);
            Property(e => e.CrisisRange).IsOptional().HasMaxLength(255);
            Property(e => e.PatientId).IsOptional().HasMaxLength(50);
            Property(e => e.PatientName).IsOptional().HasMaxLength(255);
            Property(e => e.Reporter).IsOptional().HasMaxLength(255);
            Property(e => e.ReportTime).IsOptional();
            Property(e => e.CreateTime).IsRequired();
        }
    }
}