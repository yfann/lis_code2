using Lis.Domain;
using System;
namespace Lis.Domain.Entities
{
    /// <summary>
    /// 检验报告表
    /// </summary>
    public class Report : Entity, IVersion, IMutable
    {
        public override object UniqueId { get { return Id; } }

        public string Id { get; set; }
        public byte[] Version { get; set; }
        public string CreateUserId { get; set; }
        public DateTime CreateTime { get; set; }
        public string LastUpdateUserId { get; set; }
        public DateTime LastUpdateTime { get; set; }

        /// <summary>
        /// 所属申请单号ID
        /// </summary>
        public string ReId { get; set; }

        /// <summary>
        /// 检验组合ID
        /// </summary>
        public string LabItemSetId { get; set; }

        /// <summary>
        /// 患者ID
        /// </summary>
        public string PatientId { get; set; }

        /// <summary>
        /// 患者姓名
        /// </summary>
        public string PatientName { get; set; }

        /// <summary>
        /// 患者病历号
        /// </summary>
        public string HisId { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// 科室
        /// </summary>
        public string Dept { get; set; }

        /// <summary>
        /// 床号
        /// </summary>
        public string BedNo { get; set; }

        /// <summary>
        /// 申请医生
        /// </summary>
        public string ApplicationDoctor { get; set; }

        /// <summary>
        /// 检验人
        /// </summary>
        public string Inspector { get; set; }

        /// <summary>
        /// 审核人
        /// </summary>
        public string Approvaler { get; set; }

        /// <summary>
        /// 申请时间
        /// </summary>
        public DateTime? ApplicationTime { get; set; }

        /// <summary>
        /// 送检时间
        /// </summary>
        public DateTime? SendTime { get; set; }

        /// <summary>
        /// 报告时间
        /// </summary>
        public DateTime? ReportTime { get; set; }

        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime? ApprovalTime { get; set; }

        /// <summary>
        /// 诊断
        /// </summary>
        public string Analysis { get; set; }

        /// <summary>
        /// 申请序号
        /// </summary>
        public int OrderNo { get; set; }

        /// <summary>
        /// 检验类别
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// 报告状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// 第三方系统中的报告ID（同步用）
        /// </summary>
        public string RemoteId { get; set; }

        /// <summary>
        /// 是否为上传报告（true: 表示为上传报告，即非送检报告）
        /// </summary>
        public bool IsUpload { get; set; }

        /// <summary>
        /// 所属机构ID
        /// </summary>
        public string MiId { get; set; }
    }
}