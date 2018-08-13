using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Lis.Domain.Entities;

namespace Csh.Hcis.GC.VueScheduler.EntityFramework.Mappers
{
    public class LabSampleMapper : EntityTypeConfiguration<LabSample>
    {
        public LabSampleMapper()
        {
            ToTable("tLabSample");
            HasKey(e => e.Id);

            Property(e => e.Id).IsRequired().HasMaxLength(50);
            Property(e => e.Version).IsRequired().IsRowVersion();
            Property(e => e.CreateUserId).IsRequired().HasMaxLength(50);
            Property(e => e.CreateTime).IsRequired();
            Property(e => e.LastUpdateUserId).IsRequired().HasMaxLength(50);
            Property(e => e.LastUpdateTime).IsRequired();

            Property(e => e.ReId).IsRequired().HasMaxLength(50);
            Property(e => e.SampleTypeId).IsRequired().HasMaxLength(50);
            Property(e => e.LogisticsId).IsOptional().HasMaxLength(50);
            Property(e => e.EmId).IsRequired().HasMaxLength(50);
            Property(e => e.SampleTime).IsRequired();
            Property(e => e.ClinicalId).IsOptional().HasMaxLength(50);
            Property(e => e.BarCode).IsOptional().HasMaxLength(50);
            Property(e => e.SubSampleCount).IsRequired();
            Property(e => e.Location).IsOptional().HasMaxLength(50);

            Property(e => e.Surgery).IsOptional().HasMaxLength(null);
            Property(e => e.RadiationTherapy).IsOptional().HasMaxLength(null);
            Property(e => e.Chemotherapy).IsOptional().HasMaxLength(null);
            Property(e => e.Pharmacy).IsOptional().HasMaxLength(null);

            Property(e => e.Status).IsRequired();
            Property(e => e.RejectReason).IsOptional().HasMaxLength(50);
            Property(e => e.Other).IsOptional().HasMaxLength(50);
        }
    }
}