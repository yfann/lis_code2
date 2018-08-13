using Lis.Domain;
using System;
namespace Lis.Domain.Entities
{
    /// <summary>
    /// 检验项目组合
    /// </summary>
    public class LabItemSetDetail : Entity
    {
        public override object UniqueId { get { return Id; } }

        public string Id { get; set; }

        /// <summary>
        /// 检验项目ID
        /// </summary>
        public string LmId { get; set; }

        /// <summary>
        /// 组合项目ID
        /// </summary>
        public string SetId { get; set; }
    }
}