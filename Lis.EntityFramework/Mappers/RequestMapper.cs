using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Lis.Domain.Entities;

namespace Csh.Hcis.GC.VueScheduler.EntityFramework.Mappers
{
    public class RequestMapper : EntityTypeConfiguration<Requests>
    {
        public RequestMapper()
        {
            ToTable("tRequest");
            HasKey(e => e.Id);

            Property(e => e.Id).IsRequired().HasMaxLength(50);
            Property(e => e.Version).IsRequired().IsRowVersion();
            Property(e => e.CreateUserId).IsRequired().HasMaxLength(50);
            Property(e => e.CreateTime).IsRequired();
            Property(e => e.LastUpdateUserId).IsRequired().HasMaxLength(50);
            Property(e => e.LastUpdateTime).IsRequired();

            Property(e => e.RequestNo).IsRequired().HasMaxLength(50);
            Property(e => e.EmId).IsRequired().HasMaxLength(50);
            Property(e => e.MiId).IsRequired().HasMaxLength(50);

            Property(e => e.PatientId).IsRequired().HasMaxLength(50);
            Property(e => e.LabCategoryId).IsRequired().HasMaxLength(50);
            Property(e => e.ReqTime).IsRequired();
            Property(e => e.Desc).IsOptional().HasMaxLength(null);
            Property(e => e.Charge).IsRequired().HasPrecision(18, 2);
            Property(e => e.ChiefComplaint).IsOptional().HasMaxLength(null);
            Property(e => e.MedicalHistory).IsOptional().HasMaxLength(null);
            Property(e => e.Diagnosis).IsOptional().HasMaxLength(null);
            Property(e => e.FamilyHistory).IsOptional().HasMaxLength(null);
            Property(e => e.Syndrome).IsOptional().HasMaxLength(null);
            Property(e => e.Other).IsOptional().HasMaxLength(null);
            Property(e => e.RejectReason).IsOptional().HasMaxLength(null);

            Property(e => e.ReStatus).IsRequired();
            Property(e => e.IsUpload).IsRequired();
        }
    }
}