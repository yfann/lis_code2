using Lis.Domain.Entities;
using Lis.Service.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lis.Service.Models.Integration
{
    /// <summary>
    /// 接口同步：医疗机构
    /// </summary>
    public class MiModel
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
        /// 医院名称
        /// </summary>
        public string hospName { get; set; }

        /// <summary>
        /// 医院编号
        /// </summary>
        public string hospId { get; set; }

        /// <summary>
        /// 上级医院名称
        /// </summary>
        public string parentName { get; set; }

        /// <summary>
        /// 上级医院编号
        /// </summary>
        public string parentId { get; set; }

        /// <summary>
        /// 医院等级:   10 一级甲等 11 一级乙等 20 二级甲等 21 二级乙等 30 三级甲等 31 三级乙等 99 未评等级
        /// </summary>
        public string grade { get; set; }

        /// <summary>
        /// 公立私立医院: 0：公立   1：私立
        /// </summary>
        public string isPublic { get; set; }

        /// <summary>
        /// 床位数
        /// </summary>
        public string beadNum { get; set; }

        /// <summary>
        /// 地区编码
        /// </summary>
        public string areaCode { get; set; }

        /// <summary>
        /// 详细地址
        /// </summary>
        public string address { get; set; }

        /// <summary>
        /// 医院简介
        /// </summary>
        public string hospDesc { get; set; }

        public MedicalInstitutionDto ConvertToDto()
        {
            var mi = new MedicalInstitutionDto();
            mi.Id = this.hospId;
            //mi.MiCode = this.hospId;
            mi.MiName = this.hospName;
            mi.AreaCode = this.areaCode;
            mi.MiCategory = this.grade;
            mi.Address = this.address;
            mi.ParentId = this.parentId;
            mi.ParentName = this.parentName;
            mi.Desc = this.hospDesc;
            return mi;
        }
    }
}
