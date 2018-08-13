using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lis.Service.Models.Integration
{
    /// <summary>
    /// 接口同步：修改手机号
    /// </summary>
    public class ChangeMobileModel
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
        /// 原手机号
        /// </summary>
        public string oldTelphone { get; set; }

        /// <summary>
        /// 新手机号
        /// </summary>
        public string telphone { get; set; }

        /// <summary>
        /// 医院id
        /// </summary>
        public string hospId { get; set; }
    }
}
