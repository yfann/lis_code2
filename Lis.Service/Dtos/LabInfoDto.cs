using Lis.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lis.Service.Dtos
{
    public class LabInfoDto : MutableDto, IVersion
    {
        public string Id { get; set; }
        public byte[] Version { get; set; }

        /// <summary>
        /// 所属申请单号ID
        /// </summary>
        public string ReId { get; set; }

        /// <summary>
        /// 对应的检验项目ID
        /// </summary>
        public string LabItemId { get; set; }

        /// <summary>
        /// 样本ID
        /// </summary>
        public string LabSampleId { get; set; }

        /// <summary>
        /// 报告ID
        /// </summary>
        public string ReportId { get; set; }

        /// <summary>
        /// 检验结果ID
        /// </summary>
        public string LabResultId { get; set; }

        /// <summary>
        /// 检验执行人（名称）
        /// </summary>
        public string Executor { get; set; }

        /// <summary>
        /// 执行时间
        /// </summary>
        public DateTime? Executetime { get; set; }

        /// <summary>
        /// 样本接收人
        /// </summary>
        public string Receiver { get; set; }

        /// <summary>
        /// 接收时间
        /// </summary>
        public DateTime? ReceiveTime { get; set; }

        /// <summary>
        /// 取消者
        /// </summary>
        public string Canceler { get; set; }

        /// <summary>
        /// 取消时间
        /// </summary>
        public DateTime? CancelTime { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        public string Operator1 { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime? OperateTime1 { get; set; }

        /// <summary>
        /// （备用字段）
        /// </summary>
        public string Operator2 { get; set; }

        /// <summary>
        /// （备用字段）
        /// </summary>
        public DateTime? OperateTime2 { get; set; }

        /// <summary>
        /// 意见、建议
        /// </summary>
        public string Opinion { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// 检验项目信息
        /// </summary>
        public LabItemDto LabItem { get; set; }

        /// <summary>
        /// 检验结果信息
        /// </summary>
        public LabResultDto LabResult { get; set; }

        /// <summary>
        /// 样本信息
        /// </summary>
        public LabSampleDto LabSample { get; set; }
    }
}
