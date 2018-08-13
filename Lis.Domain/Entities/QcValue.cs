using Lis.Domain;
using System;
namespace Lis.Domain.Entities
{
    /// <summary>
    /// 质检值
    /// </summary>
    public class QcValue : Entity, IVersion, IMutable
    {
        public override object UniqueId { get { return Id; } }

        public string Id { get; set; }
        public byte[] Version { get; set; }
        public string CreateUserId { get; set; }
        public DateTime CreateTime { get; set; }
        public string LastUpdateUserId { get; set; }
        public DateTime LastUpdateTime { get; set; }

        /// <summary>
        /// 质控检验项目ID - LabItem ID
        /// </summary>
        public string LmId { get; set; }

        /// <summary>
        /// 质控机构ID
        /// </summary>
        public string MiId { get; set; }

        /// <summary>
        /// 仪器ID
        /// </summary>
        public string InstrumentId { get; set; }

        /// <summary>
        /// 仪器名称
        /// </summary>
        public string InstrumentName { get; set; }

        /// <summary>
        /// 质控操作员
        /// </summary>
        public string Qcer { get; set; }

        /// <summary>
        /// 质控时间
        /// </summary>
        public DateTime QcTime { get; set; }

        /// <summary>
        /// 质控次数
        /// </summary>
        public int QcNum { get; set; }

        /// <summary>
        /// 质控值
        /// </summary>
        public decimal Value { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Comment { get; set; }

        public string Other1 { get; set; }
        public string Other2 { get; set; }
        public string Other3 { get; set; }
        public string Other4 { get; set; }
        public string Other5 { get; set; }
        public string Other6 { get; set; }
    }
}