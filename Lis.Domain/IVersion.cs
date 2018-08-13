using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lis.Domain
{
    public interface IVersion
    {
        byte[] Version { get; set; }
    }
}
