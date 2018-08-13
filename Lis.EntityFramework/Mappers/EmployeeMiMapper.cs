using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Lis.Domain.Entities;

namespace Csh.Hcis.GC.VueScheduler.EntityFramework.Mappers
{
    public class EmployeeMiMapper : EntityTypeConfiguration<EmployeeMi>
    {
        public EmployeeMiMapper()
        {
            ToTable("tEmployeeMi");
            HasKey(e => e.Id);

            Property(e => e.Id).IsRequired().HasMaxLength(50);
            Property(e => e.EmployeeId).IsRequired().HasMaxLength(50);
            Property(e => e.MiId).IsRequired().HasMaxLength(50);
        }
    }
}