using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Lis.Domain.Entities;

namespace Csh.Hcis.GC.VueScheduler.EntityFramework.Mappers
{
    public class EmployeeRoleMapper : EntityTypeConfiguration<EmployeeRole>
    {
        public EmployeeRoleMapper()
        {
            ToTable("tEmployeeRole");
            HasKey(e => e.Id);

            Property(e => e.Id).IsRequired().HasMaxLength(50);
            Property(e => e.UserId).IsRequired().HasMaxLength(50);
            Property(e => e.RoleId).IsOptional().HasMaxLength(50);
        }
    }
}