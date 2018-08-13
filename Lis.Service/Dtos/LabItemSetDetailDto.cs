using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lis.Service.Dtos
{
    public class LabItemSetDetailDto
    {

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
