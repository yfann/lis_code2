using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Lis.Domain.Entities;

namespace Csh.Hcis.GC.VueScheduler.EntityFramework.Mappers
{
    public class EmployeeMapper : EntityTypeConfiguration<Employee>
    {
        public EmployeeMapper()
        {
            ToTable("tEmployee");
            HasKey(e => e.Id);

            Property(e => e.Id).IsRequired().HasMaxLength(50);
            Property(e => e.Version).IsRequired().IsRowVersion();
            Property(e => e.CreateUserId).IsRequired().HasMaxLength(50);
            Property(e => e.CreateTime).IsRequired();
            Property(e => e.LastUpdateUserId).IsRequired().HasMaxLength(50);
            Property(e => e.LastUpdateTime).IsRequired();
            Property(e => e.SiteId).IsRequired().HasMaxLength(50);

            Property(e => e.DeptId).IsOptional().HasMaxLength(50);
            Property(e => e.EmCode).IsOptional().HasMaxLength(50);
            Property(e => e.EmName).IsRequired().HasMaxLength(50);
            Property(e => e.IDNumber).IsOptional().HasMaxLength(50);
            Property(e => e.Phone).IsOptional().HasMaxLength(50);
            Property(e => e.TitleId).IsOptional().HasMaxLength(50);
            Property(e => e.TitleName).IsOptional().HasMaxLength(50);
            Property(e => e.Password).IsOptional().HasMaxLength(128);
            Property(e => e.Desc).IsOptional().HasMaxLength(null);
            Property(e => e.Disabled).IsOptional();
        }
    }
}