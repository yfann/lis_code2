using Lis.Domain;
using Lis.Service.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lis.Service.Dtos
{
    public class LogisticsDto : MutableDto, IVersion
    {
        public string Id { get; set; }
        public byte[] Version { get; set; }

        /// <summary>
        /// 送检人
        /// </summary>
        public string SendEmId { get; set; }
        public string SendEmName { get; set; }

        /// <summary>
        /// 送检时间
        /// </summary>
        public DateTime SendTime { get; set; }

        /// <summary>
        /// 物流接收人员
        /// </summary>
        public string LsEmId { get; set; }
        public string LsEmName { get; set; }

        /// <summary>
        /// 物流接收时间
        /// </summary>
        public DateTime? LsReceiveTime { get; set; }

        /// <summary>
        /// 检验中心接收人员
        /// </summary>
        public string CenterEmId { get; set; }
        public string CenterEmName { get; set; }

        /// <summary>
        /// 检验中心接收时间
        /// </summary>
        public DateTime? CenterReceiveTime { get; set; }

        /// <summary>
        /// 物流状态 1.物流已接收  2.检验中心已接收
        /// </summary>
        public int LsStatus { get; set; }
    }
}
