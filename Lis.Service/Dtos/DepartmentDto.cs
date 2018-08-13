using Lis.Domain;

namespace Lis.Service.Dtos
{
    public class DepartmentDto : MutableDto, IVersion
    {
        public string Id { get; set; }
        public byte[] Version { get; set; }

        /// <summary>
        /// 所属医疗机构ID
        /// </summary>
        public string SiteId { get; set; }

        /// <summary>
        /// 科室编码
        /// </summary>
        public string DeptCode { get; set; }

        /// <summary>
        /// 科室名称
        /// </summary>
        public string DeptName { get; set; }

        /// <summary>
        /// 简介描述
        /// </summary>
        public string Desc { get; set; }

        /// <summary>
        /// 所属医疗机构名称
        /// </summary>
        public string MiName { get; set; }
    }
}
