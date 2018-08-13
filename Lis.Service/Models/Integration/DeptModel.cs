using Lis.Domain.Entities;
using Lis.Service.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lis.Service.Models.Integration
{
    /// <summary>
    /// 接口同步：科室
    /// </summary>
    public class DeptModel
    {
        /// <summary>
        /// 调用接口凭证
        /// </summary>
        public string access_token { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        public string timestamp { get; set; }

        /// <summary>
        /// 操作类型: I:新增；M:修改 D:删除
        /// </summary>
        public string operate { get; set; }

        /// <summary>
        /// 科室名称
        /// </summary>
        public string deptName { get; set; }

        /// <summary>
        /// 科室id
        /// </summary>
        public string deptId { get; set; }

        /// <summary>
        /// 医院id
        /// </summary>
        public string hospId { get; set; }

        public DepartmentDto ConvertToDto()
        {
            var result = new DepartmentDto();
            result.Id = this.deptId;
            result.SiteId = this.hospId;
            //result.DeptCode = this.deptId;
            result.DeptName = this.deptName;
            result.Desc = null;
            return result;
        }
    }
}
