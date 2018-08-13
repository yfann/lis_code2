using Lis.Domain;
using System;
namespace Lis.Domain.Entities
{
    /// <summary>
    /// 部门
    /// </summary>
    public class Department : Entity, IVersion, IMutable
    {
        public override object UniqueId { get { return Id; } }

        public string Id { get; set; }
        public byte[] Version { get; set; }
        public string CreateUserId { get; set; }
        public DateTime CreateTime { get; set; }
        public string LastUpdateUserId { get; set; }
        public DateTime LastUpdateTime { get; set; }

        /// <summary>
        /// 所属医疗机构ID
        /// </summary>
        public string SiteId { get; set; }

        /// <summary>
        /// 科室编码
        /// </summary>
        public string DeptCode { get; set; }

        /// <summary>
        /// 科室名称
        /// </summary>
        public string DeptName { get; set; }

        /// <summary>
        /// 简介描述
        /// </summary>
        public string Desc { get; set; }
    }
}