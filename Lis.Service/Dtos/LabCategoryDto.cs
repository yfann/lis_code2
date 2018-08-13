using Lis.Domain;
using Lis.Service.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lis.Service.Dtos
{
    public class LabCategoryDto : MutableDto, IVersion
    {
        public string Id { get; set; }
        public byte[] Version { get; set; }

        /// <summary>
        /// 检验类别代码
        /// </summary>
        public string LcCode { get; set; }

        /// <summary>
        /// 检验类别名称
        /// </summary>
        public string LcName { get; set; }

        /// <summary>
        /// 条码前缀
        /// </summary>
        public string BarcodePre { get; set; }

        /// <summary>
        /// 外部代码
        /// </summary>
        public string ExternalCode { get; set; }

        /// <summary>
        /// 颜色
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// 1: 每个收费项目单独做抽血 确认 0: 同一个检验类别捆在一起
        /// </summary>
        public int? BooldAlone { get; set; }

        /// <summary>
        /// 检验次数
        /// </summary>
        public int? ExamNum { get; set; }
    }
}
