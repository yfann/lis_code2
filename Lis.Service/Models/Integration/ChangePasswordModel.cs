using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lis.Service.Models.Integration
{
    /// <summary>
    /// 接口同步：修改密码
    /// </summary>
    public class ChangePasswordModel
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
        /// 用户姓名
        /// </summary>
        public string userName { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string telphone { get; set; }

        /// <summary>
        /// 用户原密码
        /// </summary>
        public string oldPassword { get; set; }

        /// <summary>
        /// 用户新密码
        /// </summary>
        public string password { get; set; }
    }
}
