using Lis.Domain;
using System;
namespace Lis.Domain.Entities
{
    public class Employee : Entity, IVersion, IMutable
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
        /// 所属部门ID
        /// </summary>
        public string DeptId { get; set; }

        /// <summary>
        /// 员工编码
        /// </summary>
        public string EmCode { get; set; }

        /// <summary>
        /// 员工名称
        /// </summary>
        public string EmName { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string IDNumber { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 职称ID
        /// </summary>
        public string TitleId { get; set; }

        /// <summary>
        /// 职称名称
        /// </summary>
        public string TitleName { get; set; }

        /// <summary>
        /// 登陆密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 简介描述
        /// </summary>
        public string Desc { get; set; }

        /// <summary>
        /// 是否已禁用
        /// </summary>
        public bool? Disabled { get; set; }
    }
}