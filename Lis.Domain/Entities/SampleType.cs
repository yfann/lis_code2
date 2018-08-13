using Lis.Domain;
using System;
namespace Lis.Domain.Entities
{
    /// <summary>
    /// 样本类型
    /// </summary>
    public class SampleType : Entity, IVersion, IMutable
    {
        public override object UniqueId { get { return Id; } }

        public string Id { get; set; }
        public byte[] Version { get; set; }
        public string CreateUserId { get; set; }
        public DateTime CreateTime { get; set; }
        public string LastUpdateUserId { get; set; }
        public DateTime LastUpdateTime { get; set; }

        /// <summary>
        /// 上级ID
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 中文名称
        /// </summary>
        public string ChtName { get; set; }

        /// <summary>
        /// 英文名称
        /// </summary>
        public string EngName { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int SeqNo { get; set; }

        /// <summary>
        /// 助记符
        /// </summary>
        public string HelpCode { get; set; }
    }
}