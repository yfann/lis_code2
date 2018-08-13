using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Lis.Domain.Entities;

namespace Csh.Hcis.GC.VueScheduler.EntityFramework.Mappers
{
    public class ReportMapper : EntityTypeConfiguration<Report>
    {
        public ReportMapper()
        {
            ToTable("tReport");
            HasKey(e => e.Id);

            Property(e => e.Id).IsRequired().HasMaxLength(50);
            Property(e => e.Version).IsRequired().IsRowVersion();
            Property(e => e.CreateUserId).IsRequired().HasMaxLength(50);
            Property(e => e.CreateTime).IsRequired();
            Property(e => e.LastUpdateUserId).IsRequired().HasMaxLength(50);
            Property(e => e.LastUpdateTime).IsRequired();

            Property(e => e.RemoteId).IsOptional().HasMaxLength(50);
            Property(e => e.ReId).IsRequired().HasMaxLength(50);
            Property(e => e.LabItemSetId).IsRequired().HasMaxLength(50);
            Property(e => e.PatientId).IsRequired().HasMaxLength(50);
            Property(e => e.PatientName).IsRequired().HasMaxLength(255);
            Property(e => e.HisId).IsOptional().HasMaxLength(50);
            Property(e => e.Gender).IsOptional().HasMaxLength(50);
            Property(e => e.Dept).IsOptional().HasMaxLength(50);
            Property(e => e.BedNo).IsOptional().HasMaxLength(50);
            Property(e => e.ApplicationDoctor).IsOptional().HasMaxLength(255);
            Property(e => e.Inspector).IsOptional().HasMaxLength(255);
            Property(e => e.Approvaler).IsOptional().HasMaxLength(255);
            Property(e => e.ApplicationTime).IsOptional();
            Property(e => e.SendTime).IsOptional();
            Property(e => e.ReportTime).IsOptional();
            Property(e => e.ApprovalTime).IsOptional();
            Property(e => e.Analysis).IsOptional().HasMaxLength(1000);
            Property(e => e.OrderNo).IsRequired();
            Property(e => e.Category).IsOptional().HasMaxLength(50);
            Property(e => e.Status).IsOptional();
            Property(e => e.Comment).IsOptional().HasMaxLength(null);
            Property(e => e.IsUpload).IsRequired();
            Property(e => e.MiId).IsOptional().HasMaxLength(50);
        }
    }
}