using Lis.Domain;
using System;
namespace Lis.Domain.Entities
{
    /// <summary>
    /// 检验项目结果
    /// </summary>
    public class LabResult : Entity, IVersion, IMutable
    {
        public override object UniqueId { get { return Id; } }

        public string Id { get; set; }
        public byte[] Version { get; set; }
        public string CreateUserId { get; set; }
        public DateTime CreateTime { get; set; }
        public string LastUpdateUserId { get; set; }
        public DateTime LastUpdateTime { get; set; }

        /// <summary>
        /// 检验项目ID
        /// </summary>
        public string LmId { get; set; }

        /// <summary>
        /// 所属申请单ID
        /// </summary>
        public string ReId { get; set; }

        /// <summary>
        /// 英文名
        /// </summary>
        public string EnglishName { get; set; }

        /// <summary>
        /// 中文名
        /// </summary>
        public string ChtName { get; set; }

        /// <summary>
        /// 检验结果值
        /// </summary>
        public string ResultValue { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string RefLo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string RefHi { get; set; }

        /// <summary>
        /// 结果范围值
        /// </summary>
        public string RefRange { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ResultFlag { get; set; }

        /// <summary>
        /// 检验结果单位
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int SeqNo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Hint { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string GermResult { get; set; }
    }
}