using Lis.Domain;
using Lis.Service.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lis.Service.Dtos
{
    public class CrisisDto : MutableDto, IVersion
    {
        public string Id { get; set; }
        public byte[] Version { get; set; }

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

        /// <summary>
        /// 检验项目
        /// </summary>
        public LabItemDto LabItem { get; set; }
    }
}
