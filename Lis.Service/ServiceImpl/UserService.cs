using AutoMapper;
using Lis.EntityFramework.Interfaces;
using Lis.Service.Dtos;
using Lis.Service.Utils;
using Lis.Domain.Entities;
using Lis.EntityFramework.Models;
using Lis.Common.Logger;
using Lis.Common.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using Lis.Common;
using Lis.Service.Models;

namespace Lis.Service.ServiceImpl
{
    public class UserService : DisposableServiceBase, IUserService
    {
        private readonly ILisContext _dbContext;
        private readonly ISystemLogger _logger;
        private readonly ISystemService _systemService;

        public UserService(ILisContext dbContext, ISystemLogger logger, ISystemService systemService)
        {
            _dbContext = dbContext;
            _logger = logger;
            _systemService = systemService;
        }

        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="code">工号</param>
        /// <param name="password">密码</param>
        /// <returns>登录结果</returns>
        public LoginValidateModel Login(string code, string password)
        {
            var encryptPwd = EncryptUtil.Md5Hash(password);
            var user = _dbContext.Set<Employee>().Where(s => s.EmCode == code && (s.Password == encryptPwd || s.Password == password) && s.Disabled != true).FirstOrDefault();
            if (user != null)
            {
                var userDto = Mapper.Map<Employee, EmployeeDto>(user);
                userDto.Password = "";
                var token = EncryptUtil.AesEncrypt(string.Format("{0}|{1}", user.Id, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                return new LoginValidateModel()
                {
                    Valid = true,
                    Token = token,
                    User = userDto,
                    Message = "Success",
                };
            }
            else
            {
                return new LoginValidateModel()
                {
                    Valid = false,
                    Token = "",
                    User = null,
                    Message = "账号或密码不正确",
                };
            }
        }

        /// <summary>
        /// Token验证
        /// </summary>
        /// <param name="token"></param>
        /// <returns>true:验证通过，false:验证失败</returns>
        public bool ValidateToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                return false;
            }
            var tokenObj = EncryptUtil.AesDecrypt(token);
            if (!string.IsNullOrEmpty(tokenObj))
            {
                var strs = tokenObj.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                if (strs.Length == 2)
                {
                    var createOn = strs[1];
                    DateTime createOnDate;
                    if (DateTime.TryParse(createOn, out createOnDate))
                    {
                        var expireMinutes = System.Configuration.ConfigurationManager.AppSettings["SessionExpireMinutes"] ?? "120";
                        if (createOnDate.AddMinutes(double.Parse(expireMinutes)) > DateTime.Now)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="srcPwd">原密码</param>
        /// <param name="newPwd">新密码</param>
        /// <returns></returns>
        public bool ChangePassword(string userId, string srcPwd, string newPwd)
        {
            if (string.IsNullOrEmpty(newPwd))
            {
                throw new Exception("新密码不能为空！");
            }

            var user = _dbContext.Set<Employee>().Where(s => s.Id == userId).FirstOrDefault();
            if (user == null)
            {
                throw new Exception("用户不存在！");
            }

            if (EncryptUtil.Md5Hash(srcPwd) == user.Password)
            {
                user.Password = EncryptUtil.Md5Hash(newPwd);
                _dbContext.SaveChanges();
                return true;
            }
            else
            {
                throw new Exception("原密码错误！");
            }
        }


    }
}
