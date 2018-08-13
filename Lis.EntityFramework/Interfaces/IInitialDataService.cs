using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lis.EntityFramework.Interfaces
{
    public interface IInitialDataService
    {
        void InitialData(ILisContext context);
    }
}
