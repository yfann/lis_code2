using Lis.Domain;
using System;
namespace Lis.Domain.Entities
{
    /// <summary>
    /// 部门
    /// </summary>
    public class Requests : Entity, IVersion, IMutable
    {
        public override object UniqueId { get { return Id; } }

        public string Id { get; set; }
        public byte[] Version { get; set; }
        public string CreateUserId { get; set; }
        public DateTime CreateTime { get; set; }
        public string LastUpdateUserId { get; set; }
        public DateTime LastUpdateTime { get; set; }

        /// <summary>
        /// 申请单号
        /// </summary>
        public string RequestNo { get; set; }

        /// <summary>
        /// 申请员工ID
        /// </summary>
        public string EmId { get; set; }

        /// <summary>
        /// 申请机构ID
        /// </summary>
        public string MiId { get; set; }

        /// <summary>
        /// 患者ID
        /// </summary>
        public string PatientId { get; set; }

        /// <summary>
        /// 检验类别ID
        /// </summary>
        public string LabCategoryId { get; set; }

        /// <summary>
        /// 申请时间
        /// </summary>
        public DateTime ReqTime { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Desc { get; set; }

        /// <summary>
        /// 收费
        /// </summary>
        public decimal Charge { get; set; }

        /// <summary>
        /// 主诉
        /// </summary>
        public string ChiefComplaint { get; set; }

        /// <summary>
        /// 病史
        /// </summary>
        public string MedicalHistory { get; set; }

        /// <summary>
        /// 诊断
        /// </summary>
        public string Diagnosis { get; set; }

        /// <summary>
        /// 家族病史
        /// </summary>
        public string FamilyHistory { get; set; }

        /// <summary>
        /// 并发症
        /// </summary>
        public string Syndrome { get; set; }
        public string Other { get; set; }

        /// <summary>
        /// 申请单状态 1.申请单已提交 2.申请单已拒绝 3.申请单已接收 4.中心样本已接收 5.检验中 6.检验已完成 7.检验报告已完成 8.检验报告已审核 9.检验报告已上传中心
        /// </summary>
        public int ReStatus { get; set; }

        /// <summary>
        /// 拒绝原因
        /// </summary>
        public string RejectReason { get; set; }

        /// <summary>
        /// 是否为上传报告（true: 表示为上传报告，即非送检报告）
        /// </summary>
        public bool IsUpload { get; set; }
    }
}