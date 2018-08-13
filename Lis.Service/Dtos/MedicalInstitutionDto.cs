using Lis.Domain;
using Lis.Service.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lis.Service.Dtos
{
    public class MedicalInstitutionDto : MutableDto, IVersion
    {
        public string Id { get; set; }
        public byte[] Version { get; set; }

        /// <summary>
        /// 机构编码
        /// </summary>
        public string MiCode { get; set; }

        /// <summary>
        /// 机构名称
        /// </summary>
        public string MiName { get; set; }

        /// <summary>
        /// 机构类别
        /// </summary>
        public string MiCategory { get; set; }

        /// <summary>
        /// 所在地区编码
        /// </summary>
        public string AreaCode { get; set; }

        /// <summary>
        /// 机构地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 描述简介
        /// </summary>
        public string Desc { get; set; }

        /// <summary>
        /// 上级机构ID
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 上级机构名称
        /// </summary>
        public string ParentName { get; set; }
    }
}
