using Lis.Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lis.Service.Dtos
{
    public abstract class MutableDto
    {
        public string CreateUserId { get; set; }
        public DateTime CreateTime { get; set; }
        public string LastUpdateUserId { get; set; }
        public DateTime LastUpdateTime { get; set; }

        public void SetWithCreate(string userId)
        {
            this.CreateUserId = userId;
            this.CreateTime = DateTime.Now;
            SetWithUpdate(userId);
        }

        public void SetWithUpdate(string userId)
        {
            this.LastUpdateUserId = userId;
            this.LastUpdateTime = DateTime.Now;
        }
    }
}
