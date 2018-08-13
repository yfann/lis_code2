using Lis.Domain;
using System;
namespace Lis.Domain.Entities
{
    /// <summary>
    /// 物流信息
    /// </summary>
    public class Logistics : Entity, IVersion, IMutable
    {
        public override object UniqueId { get { return Id; } }

        public string Id { get; set; }
        public byte[] Version { get; set; }
        public string CreateUserId { get; set; }
        public DateTime CreateTime { get; set; }
        public string LastUpdateUserId { get; set; }
        public DateTime LastUpdateTime { get; set; }

        /// <summary>
        /// 送检人
        /// </summary>
        public string SendEmId { get; set; }

        /// <summary>
        /// 送检时间
        /// </summary>
        public DateTime SendTime { get; set; }

        /// <summary>
        /// 物流接收人员
        /// </summary>
        public string LsEmId { get; set; }

        /// <summary>
        /// 物流接收时间
        /// </summary>
        public DateTime? LsReceiveTime { get; set; }

        /// <summary>
        /// 检验中心接收人员
        /// </summary>
        public string CenterEmId { get; set; }

        /// <summary>
        /// 检验中心接收时间
        /// </summary>
        public DateTime? CenterReceiveTime { get; set; }

        /// <summary>
        /// 物流状态 1.物流已接收  2.检验中心已接收
        /// </summary>
        public int LsStatus { get; set; }
    }
}