using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lis.Domain
{
    public interface IMutable
    {
        string CreateUserId { get; set; }
        DateTime CreateTime { get; set; }
        string LastUpdateUserId { get; set; }
        DateTime LastUpdateTime { get; set; }
    }
}
