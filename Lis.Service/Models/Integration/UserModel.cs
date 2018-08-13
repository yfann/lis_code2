using Lis.Domain.Entities;
using Lis.Service.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lis.Service.Models.Integration
{
    /// <summary>
    /// 接口同步：人员
    /// </summary>
    public class UserModel
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
        /// 系统平台标志: 1:新增平台权限或修改信息； 0:无平台权限或删除平台权限
        /// </summary>
        public string hasFlag { get; set; }

        /// <summary>
        /// 医生姓名
        /// </summary>
        public string doctorName { get; set; }

        /// <summary>
        /// 医生编号
        /// </summary>
        public string doctroId { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string idCard { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string telphone { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string hospId { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string deptId { get; set; }

        /// <summary>
        /// 职称：01	主任医师、02	副主任医师、03	主治医师、04	住院医师、05	助理医师
        /// </summary>
        public string profTitle { get; set; }

        /// <summary>
        /// 医生类型：1:医生  2：护士  3：其他
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// 医生描述：不超过2000字
        /// </summary>
        public string doctorDetail { get; set; }

        /// <summary>
        /// 用户密码：MD5加密，新增时必填
        /// </summary>
        public string password { get; set; }

        /// <summary>
        /// 角色类型：系统权限标志为1时必填
        /// </summary>
        public string roleType { get; set; }


        public EmployeeDto ConvertToDto()
        {
            var result = new EmployeeDto();
            result.Id = this.doctroId;
            result.SiteId = this.hospId;
            result.DeptId = this.deptId;
            result.EmCode = this.doctroId;
            result.EmName = this.doctorName;
            result.IDNumber = this.idCard;
            result.Phone = this.telphone;
            result.TitleId = this.profTitle;
            result.TitleName = this.GetTitleName();
            result.Password = this.password;
            result.Desc = this.type;
            result.Disabled = this.hasFlag == "0";
            return result;
        }

        private string GetTitleName()
        {
            switch (this.profTitle)
            {
                case "01":
                    return "主任医师";
                case "02":
                    return "副主任医师";
                case "03":
                    return "主治医师";
                case "04":
                    return "住院医师";
                case "05":
                    return "助理医师";
                default:
                    return null;
            }
        }
    }
}
