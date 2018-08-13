using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Lis.Domain.Entities;

namespace Csh.Hcis.GC.VueScheduler.EntityFramework.Mappers
{
    public class LabInfoMapper : EntityTypeConfiguration<LabInfo>
    {
        public LabInfoMapper()
        {
            ToTable("tLabInfo");
            HasKey(e => e.Id);

            Property(e => e.Id).IsRequired().HasMaxLength(50);
            Property(e => e.Version).IsRequired().IsRowVersion();
            Property(e => e.CreateUserId).IsRequired().HasMaxLength(50);
            Property(e => e.CreateTime).IsRequired();
            Property(e => e.LastUpdateUserId).IsRequired().HasMaxLength(50);
            Property(e => e.LastUpdateTime).IsRequired();

            Property(e => e.ReId).IsRequired().HasMaxLength(50);
            Property(e => e.LabItemId).IsRequired().HasMaxLength(50);
            Property(e => e.LabSampleId).IsOptional().HasMaxLength(50);
            Property(e => e.ReportId).IsOptional().HasMaxLength(50);
            Property(e => e.LabResultId).IsOptional().HasMaxLength(50);
            Property(e => e.Executor).IsOptional().HasMaxLength(50);
            Property(e => e.Executetime).IsOptional();
            Property(e => e.Receiver).IsOptional().HasMaxLength(50);
            Property(e => e.ReceiveTime).IsOptional();
            Property(e => e.Canceler).IsOptional().HasMaxLength(50);
            Property(e => e.CancelTime).IsOptional();
            Property(e => e.Operator1).IsOptional().HasMaxLength(50);
            Property(e => e.OperateTime1).IsOptional();
            Property(e => e.Operator2).IsOptional().HasMaxLength(50);
            Property(e => e.OperateTime2).IsOptional();
            Property(e => e.Opinion).IsOptional().HasMaxLength(null);
            Property(e => e.Comment).IsOptional().HasMaxLength(null);
        }
    }
}