using Lis.Domain;
using Lis.Service.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lis.Service.Dtos
{
    public class SampleTypeDto : MutableDto, IVersion
    {
        public string Id { get; set; }
        public byte[] Version { get; set; }

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
