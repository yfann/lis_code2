using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lis.Common
{
    /// <summary>
    /// 申请单状态 1.申请单已提交 2.申请单已拒绝 3.申请单已接收 4.中心样本已接收 5.检验中 6.检验已完成 7.检验报告已完成 8.检验报告已审核 9.检验报告已上传中心
    /// </summary>
    public enum RequestStatus
    {
        /// <summary>
        /// 申请单已提交
        /// </summary>
        Submitted = 1,

        /// <summary>
        /// 申请单已拒绝
        /// </summary>
        Refused = 2,

        /// <summary>
        /// 申请单已接收
        /// </summary>
        Accepted = 3,

        /// <summary>
        /// 中心样本已接收
        /// </summary>
        SampleAccepted = 4,

        /// <summary>
        /// 检验中
        /// </summary>
        InChecking = 5,

        /// <summary>
        /// 检验已完成
        /// </summary>
        CheckingCompleted = 6,

        /// <summary>
        /// 检验报告已完成
        /// </summary>
        ReportCompleted = 7,

        /// <summary>
        /// 检验报告已审核
        /// </summary>
        ReportApprovaled = 8,

        /// <summary>
        /// 检验报告已上传中心
        /// </summary>
        ReportUploaded = 9
    }
}
