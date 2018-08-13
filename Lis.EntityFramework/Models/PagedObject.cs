using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lis.EntityFramework.Models
{
    public class PagedObject<T>
    {
        public List<T> Data { get; set; }
        public int Count { get; set; }
    }
}
