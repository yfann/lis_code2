using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lis.Domain.Entities
{
    public class InitialDataHistory : Entity
    {
        public override object UniqueId { get { return Id; } }
        public string Id { get; set; }
        public string Version { get; set; }
        public bool IsUpdated { get; set; }
        public string LastEditUserId { get; set; }
        public DateTime LastEditTime { get; set; }
        public string Description { get; set; }
    }
}
