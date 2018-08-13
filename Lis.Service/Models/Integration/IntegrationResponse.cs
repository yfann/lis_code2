using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lis.Service.Models.Integration
{
    public class IntegrationResponse
    {
        /// <summary>
        /// 200:操作成功, 500:操作失败, 10002:传入参数错误
        /// </summary>
        public string Code { get; set; }

        public string Message { get; set; }

        /// <summary>
        /// 200:操作成功
        /// </summary>
        public static readonly IntegrationResponse IntegrationResponse200 = new IntegrationResponse() { Code = "200", Message = "操作成功" };

        /// <summary>
        /// 500:操作失败
        /// </summary>
        public static readonly IntegrationResponse IntegrationResponse500 = new IntegrationResponse() { Code = "500", Message = "操作失败" };

        /// <summary>
        /// 10002:传入参数错误
        /// </summary>
        public static readonly IntegrationResponse IntegrationResponse10002 = new IntegrationResponse() { Code = "10002", Message = "传入参数错误" };
    }
}
