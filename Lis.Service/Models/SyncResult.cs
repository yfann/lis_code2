using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lis.Service.Models
{
    public class SyncResult<T> where T : class
    {
        public T Data { get; set; }
        public int Code { get; set; }
        public string Message { get; set; }
    }
}
