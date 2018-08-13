using Lis.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lis.Service.Dtos
{
    public class ReportDto : MutableDto, IVersion
    {
        public string Id { get; set; }
        public byte[] Version { get; set; }

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

        public string Age { get; set; }

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
        /// 检验结果信息
        /// </summary>
        public List<LabResultDto> Results { get; set; }

        /// <summary>
        /// 检验项目信息
        /// </summary>
        public List<LabInfoDto> Details { get; set; }

        /// <summary>
        /// 组合名称
        /// </summary>
        public string SetName { get; set; }

        /// <summary>
        /// 所属机构ID
        /// </summary>
        public string MiId { get; set; }

        /// <summary>
        /// 所属机构名称
        /// </summary>
        public string MiName { get; set; }

        /// <summary>
        /// 是否为上传报告（true: 表示为上传报告，即非送检报告）
        /// </summary>
        public bool IsUpload { get; set; }

        public string BirthDay { get; set; }
    }
}
