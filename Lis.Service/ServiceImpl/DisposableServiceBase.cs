using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lis.Service.ServiceImpl
{
    using Lis.Common.Utils;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class DisposableServiceBase : ServiceContextBase, IDisposable
    {
        public DisposableServiceBase()
        {
            this.DisposableObjectList = new List<IDisposable>();
        }

        protected void AddDisposableObject(object obj)
        {
            IDisposable item = obj as IDisposable;
            if ((item != null) && !this.DisposableObjectList.Contains<object>(obj))
            {
                this.DisposableObjectList.Add(item);
            }
        }

        public void Dispose()
        {
            foreach (IDisposable disposable in this.DisposableObjectList)
            {
                if (disposable != null)
                {
                    disposable.Dispose();
                }
            }
        }

        public IList<IDisposable> DisposableObjectList { get; private set; }
    }
}
