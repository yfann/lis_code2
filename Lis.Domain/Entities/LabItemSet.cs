using Lis.Domain;
using System;
namespace Lis.Domain.Entities
{
    /// <summary>
    /// 检验项目组合
    /// </summary>
    public class LabItemSet : Entity, IVersion, IMutable
    {
        public override object UniqueId { get { return Id; } }

        public string Id { get; set; }
        public byte[] Version { get; set; }
        public string CreateUserId { get; set; }
        public DateTime CreateTime { get; set; }
        public string LastUpdateUserId { get; set; }
        public DateTime LastUpdateTime { get; set; }

        /// <summary>
        /// 组合项目代码
        /// </summary>
        public string LisCode { get; set; }

        /// <summary>
        /// 组合项目名称
        /// </summary>
        public string LisName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Comment { get; set; }
    }
}