using Lis.Domain;
using Lis.Service.Dtos;
using System;
using System.Collections.Generic;

namespace Lis.Service.Dtos
{
    public class LabItemDto : MutableDto, IVersion
    {
        public string Id { get; set; }
        public byte[] Version { get; set; }

        /// <summary>
        /// 检验项目类别ID
        /// </summary>
        public string LcId { get; set; }

        /// <summary>
        /// 代码
        /// </summary>
        public string ItemCode { get; set; }

        /// <summary>
        /// 标准代码
        /// </summary>
        public string StandardCode { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        /// 检验项目类别名称
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// 结果类型
        /// </summary>
        public int? ResultType { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// 变化率
        /// </summary>
        public decimal? LifeLimit { get; set; }

        /// <summary>
        /// 默认值
        /// </summary>
        public string DefValue { get; set; }

        /// <summary>
        /// 标准输入码1
        /// </summary>
        public string TypeCode1 { get; set; }


        /// <summary>
        /// 标准输入码2
        /// </summary>
        public string TypeCode2 { get; set; }

        /// <summary>
        /// 重要项目标志
        /// </summary>
        public bool? Important { get; set; }

        /// <summary>
        /// 关联标志
        /// </summary>
        public bool? Associated { get; set; }

        /// <summary>
        /// 条件审核标志
        /// </summary>
        public bool? ConditionAudit { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Comment { get; set; }


        /// <summary>
        /// 显示状态标志
        /// </summary>
        public int Display { get; set; }

        /// <summary>
        /// 精度
        /// </summary>
        public decimal? Precision { get; set; }

        /// <summary>
        /// 项目金额
        /// </summary>
        public decimal? Price { get; set; }

        /// <summary>
        /// 允许等于零
        /// </summary>
        public bool? CanZero { get; set; }

        /// <summary>
        /// 允许小于零
        /// </summary>
        public bool? CanLessZero { get; set; }

        /// <summary>
        /// 临床意义
        /// </summary>
        public string MeanOfclinic { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Desc { get; set; }

        /// <summary>
        /// 所属分类名称
        /// </summary>
        public string CategoryName { get; set; }
    }
}
