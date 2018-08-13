using Lis.Service.Dtos;
using Lis.Domain.Entities;
using Lis.EntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lis.Service.Models;

namespace Lis.Service
{
    public interface IUserService : IDisposable
    {
        #region 登录相关
        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="code">工号</param>
        /// <param name="password">密码</param>
        /// <returns>登录结果</returns>
        LoginValidateModel Login(string code, string password);

        /// <summary>
        /// Token验证
        /// </summary>
        /// <param name="token"></param>
        /// <returns>true:验证通过，false:验证失败</returns>
        bool ValidateToken(string token);

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="srcPwd">原密码</param>
        /// <param name="newPwd">新密码</param>
        /// <returns></returns>
        bool ChangePassword(string userId, string srcPwd, string newPwd);
        #endregion
    }
}
