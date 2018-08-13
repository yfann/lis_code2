using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lis.Service.Dtos
{
    public class EmployeeMiDto
    {
        public string Id { get; set; }

        /// <summary>
        /// 员工ID
        /// </summary>
        public string EmployeeId { get; set; }

        /// <summary>
        /// 可访问机构ID
        /// </summary>
        public string MiId { get; set; }

        public string MiName { get; set; }
    }
}
