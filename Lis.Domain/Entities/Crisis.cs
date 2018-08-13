using Lis.Domain;
using System;
namespace Lis.Domain.Entities
{
    /// <summary>
    /// 检验项目危机值
    /// </summary>
    public class Crisis : Entity, IVersion, IMutable
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
        /// 正常上限
        /// </summary>
        public decimal NormalUpper { get; set; }

        /// <summary>
        /// 正常下限
        /// </summary>
        public decimal NormalLow { get; set; }

        /// <summary>
        /// 危急上限
        /// </summary>
        public decimal CrisisUpper { get; set; }

        /// <summary>
        /// 危急下限
        /// </summary>
        public decimal CrisisLow { get; set; }

        /// <summary>
        /// 危急临床提示
        /// </summary>
        public string CrisisClinical { get; set; }

        /// <summary>
        /// 临床意义
        /// </summary>
        public string ClinicasSignificance { get; set; }
    }
}