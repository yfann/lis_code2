using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Lis.Domain.Entities;

namespace Csh.Hcis.GC.VueScheduler.EntityFramework.Mappers
{
    public class DepartmentMapper : EntityTypeConfiguration<Department>
    {
        public DepartmentMapper()
        {
            ToTable("tDepartment");
            HasKey(e => e.Id);

            Property(e => e.Id).IsRequired().HasMaxLength(50);
            Property(e => e.Version).IsRequired().IsRowVersion();
            Property(e => e.CreateUserId).IsRequired().HasMaxLength(50);
            Property(e => e.CreateTime).IsRequired();
            Property(e => e.LastUpdateUserId).IsRequired().HasMaxLength(50);
            Property(e => e.LastUpdateTime).IsRequired();
            Property(e => e.SiteId).IsRequired().HasMaxLength(50);

            Property(e => e.DeptCode).IsOptional().HasMaxLength(50);
            Property(e => e.DeptName).IsRequired().HasMaxLength(255);
            Property(e => e.Desc).IsRequired().HasMaxLength(null);
        }
    }
}