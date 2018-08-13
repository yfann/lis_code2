using Lis.Domain;
using System;
namespace Lis.Domain.Entities
{
    public class EmployeeMi : Entity
    {
        public override object UniqueId { get { return Id; } }

        public string Id { get; set; }

        /// <summary>
        /// 员工ID
        /// </summary>
        public string EmployeeId { get; set; }

        /// <summary>
        /// 可访问机构ID
        /// </summary>
        public string MiId { get; set; }
    }
}