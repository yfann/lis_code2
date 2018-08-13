using Lis.Domain;
using System;
namespace Lis.Domain.Entities
{
    /// <summary>
    /// 患者信息
    /// </summary>
    public class Patient : Entity, IVersion, IMutable
    {
        public override object UniqueId { get { return Id; } }

        public string Id { get; set; }
        public byte[] Version { get; set; }
        public string CreateUserId { get; set; }
        public DateTime CreateTime { get; set; }
        public string LastUpdateUserId { get; set; }
        public DateTime LastUpdateTime { get; set; }

        /// <summary>
        /// 院内对应ID
        /// </summary>
        public string InnerId { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string PtName { get; set; }

        /// <summary>
        /// 父母名
        /// </summary>
        public string ParentName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string Sex { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public DateTime? BirthDay { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public string Age { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 住址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Desc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Other1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Other2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Other3 { get; set; }

        /// <summary>
        /// 记录创建时间
        /// </summary>
        public DateTime CreateDt { get; set; }
    }
}