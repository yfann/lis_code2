using Lis.Domain;
using System;
namespace Lis.Domain.Entities
{
    /// <summary>
    /// User-Role mapping
    /// </summary>
    public class EmployeeRole : Entity
    {
        public override object UniqueId { get { return Id; } }

        public string Id { get; set; }
        public string UserId { get; set; }
        public string RoleId { get; set; }
    }
}