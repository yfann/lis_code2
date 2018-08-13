using System;
using System.Collections.Generic;
using System.Text;
using Lis.Service.Dtos;
using Lis.Domain;
using Newtonsoft.Json;

namespace Lis.Service.Dtos
{
    public class LabSampleDto : MutableDto, IVersion
    {
        public string Id { get; set; }
        public byte[] Version { get; set; }

        /// <summary>
        /// 所属申请单ID
        /// </summary>
        public string ReId { get; set; }

        /// <summary>
        /// 样本类型ID
        /// </summary>
        public string SampleTypeId { get; set; }

        /// <summary>
        /// 样本物流ID
        /// </summary>
        public string LogisticsId { get; set; }

        /// <summary>
        /// 采样人ID
        /// </summary>
        public string EmId { get; set; }

        /// <summary>
        /// 采样日期
        /// </summary>
        public DateTime SampleTime { get; set; }

        /// <summary>
        /// 就诊记录号
        /// </summary>
        public string ClinicalId { get; set; }

        /// <summary>
        /// 原始条码号
        /// </summary>
        public string BarCode { get; set; }

        /// <summary>
        /// 分装数量
        /// </summary>
        public int SubSampleCount { get; set; }

        /// <summary>
        /// 位置
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// 手术方案
        /// </summary>
        public string Surgery { get; set; }

        /// <summary>
        /// 放疗信息
        /// </summary>
        public string RadiationTherapy { get; set; }

        /// <summary>
        /// 化疗信息
        /// </summary>
        public string Chemotherapy { get; set; }

        /// <summary>
        /// 用药情况
        /// </summary>
        public string Pharmacy { get; set; }

        /// <summary>
        /// 样本状态 0：未送达， 1：已接收，2：已拒绝
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 拒绝理由
        /// </summary>
        public string RejectReason { get; set; }

        /// <summary>
        /// 其它
        /// </summary>
        public string Other { get; set; }

        /// <summary>
        /// 样本物流信息
        /// </summary>
        public LogisticsDto Logistics { get; set; }
    }
}