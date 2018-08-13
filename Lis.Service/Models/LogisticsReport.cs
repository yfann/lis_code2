using Lis.Service.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lis.Service.Models
{
    public class LogisticsReport
    {
        /// <summary>
        /// 送检人名称
        /// </summary>
        public string SendEmName { get; set; }

        /// <summary>
        /// 物流接收人名称
        /// </summary>
        public string LsEmName { get; set; }

        /// <summary>
        /// 中心接收人名称
        /// </summary>
        public string CenterEmName { get; set; }

        /// <summary>
        /// 送检或接收时间（用于本次报表记录）
        /// </summary>
        public DateTime ReceiveTime { get; set; }

        /// <summary>
        /// 本次样本
        /// </summary>
        public List<LabSampleDto> LabSamples { get; set; }
    }
}
