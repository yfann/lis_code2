using Lis.Domain;
using System;
namespace Lis.Domain.Entities
{
    /// <summary>
    /// 检验项目危机值
    /// </summary>
    public class ReportCrisis : Entity
    {
        public override object UniqueId { get { return Id; } }

        public string Id { get; set; }
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 申请单ID
        /// </summary>
        public string ReId { get; set; }

        /// <summary>
        /// 检验项目ID
        /// </summary>
        public string LmId { get; set; }

        /// <summary>
        /// 结果值
        /// </summary>
        public string ResultValue { get; set; }

        /// <summary>
        /// 危急值范围
        /// </summary>
        public string CrisisRange { get; set; }

        /// <summary>
        /// 患者ID
        /// </summary>
        public string PatientId { get; set; }

        /// <summary>
        /// 患者姓名
        /// </summary>
        public string PatientName { get; set; }

        /// <summary>
        /// 报告人
        /// </summary>
        public string Reporter { get; set; }

        /// <summary>
        /// 报告时间
        /// </summary>
        public DateTime? ReportTime { get; set; }

    }
}