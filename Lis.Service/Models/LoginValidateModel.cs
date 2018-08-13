using Lis.Service.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lis.Service.Models
{
    public class LoginValidateModel
    {
        /// <summary>
        /// 登录是否成功： true:登录成功，false:登录失败
        /// </summary>
        public bool Valid { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Session token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 成功时，返回的用户对象
        /// </summary>
        public EmployeeDto User { get; set; }
    }
}
