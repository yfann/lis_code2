using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lis.Service.Dtos
{
    public class InitialDataHistoryDto
    {
        public string Id { get; set; }
        public string Version { get; set; }
        public bool IsUpdated { get; set; }
        public string LastEditUserId { get; set; }
        public DateTime LastEditTime { get; set; }
    }
}
