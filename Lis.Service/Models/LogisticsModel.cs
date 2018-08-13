using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lis.Service.Models
{
    public class LogisticsModel
    {
        /// <summary>
        /// 本次接收到的样本条码集合
        /// </summary>
        public List<string> BarCodes { get; set; }

        /// <summary>
        /// 送检人ID
        /// </summary>
        public string SendEmId { get; set; }

        /// <summary>
        /// 物流接收人ID
        /// </summary>
        public string LsEmId { get; set; }

        /// <summary>
        /// 检验中心接收人ID
        /// </summary>
        public string CenterEmId { get; set; }
    }
}
