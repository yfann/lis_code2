using Lis.Domain;
using System.Collections.Generic;

namespace Lis.Service.Dtos
{
    public class LabItemSetDto : MutableDto, IVersion
    {
        public string Id { get; set; }
        public byte[] Version { get; set; }

        /// <summary>
        /// 组合项目代码
        /// </summary>
        public string LisCode { get; set; }

        /// <summary>
        /// 组合项目名称
        /// </summary>
        public string LisName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// 组合内包含的检验项目列表
        /// </summary>
        public List<LabItemDto> LabItems { get; set; }

        /// <summary>
        /// 检验项目列表名称组合（以，隔开拼起来）
        /// </summary>
        public string LabItemString { get; set; }
    }
}
