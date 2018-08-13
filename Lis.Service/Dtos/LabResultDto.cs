using Lis.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lis.Service.Dtos
{
    public class LabResultDto : MutableDto, IVersion
    {
        public string Id { get; set; }
        public byte[] Version { get; set; }

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

        /// <summary>
        /// 所属检验机构ID
        /// </summary>
        public string MiId { get; set; }

        /// <summary>
        /// 所属检验机构名称
        /// </summary>
        public string MiName { get; set; }
    }
}
