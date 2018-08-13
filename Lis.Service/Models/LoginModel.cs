using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lis.Service.Models
{
    /// <summary>
    /// 登录参数
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// 员工工号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        public string Password { get; set; }
    }
}
