using Lis.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lis.EntityFramework.Mappers
{
    public class InitialDataHistoryMapper : EntityTypeConfiguration<InitialDataHistory>
    {
        public InitialDataHistoryMapper()
        {
            this.ToTable("dbo.tInitialDataHistory");
            this.HasKey(u => u.Id);

            this.Property(u => u.Id).IsRequired().HasMaxLength(50);
            this.Property(u => u.Version).IsRequired().HasMaxLength(50);
            this.Property(u => u.Description).IsOptional().HasMaxLength(500);
            this.Property(u => u.IsUpdated).IsRequired();
            this.Property(u => u.LastEditUserId).IsRequired().HasMaxLength(50);
            this.Property(u => u.LastEditTime).IsRequired();
        }
    }
}
