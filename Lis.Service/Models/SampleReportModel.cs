using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lis.Service.Models
{
    /// <summary>
    /// 样本类别统计报表
    /// </summary>
    public class SampleReportModel
    {
        /// <summary>
        /// 样本类型
        /// </summary>
        public string SampleTypeName { get; set; }

        /// <summary>
        /// 机构名称
        /// </summary>
        public string MiName { get; set; }

        /// <summary>
        /// 申请单数量
        /// </summary>
        public int RequestCount { get; set; }
    }
}
