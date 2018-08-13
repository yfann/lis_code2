using Lis.Common.Logger;
using Lis.Service;
using Lis.Service.Dtos;
using Lis.Service.Models;
using Lis.Service.Models.Integration;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Net.Http;

namespace Lis.WebApi.Controllers
{
    public class LisController : ApiControllerBase
    {
        private readonly ISystemService _systemService;
        private readonly ILisService _lisService;
        private readonly ISystemLogger _logger;
        public LisController(ISystemLogger logger, ISystemService systemService, ILisService lisService)
        {
            _logger = logger;
            _systemService = systemService;
            _lisService = lisService;
        }

        #region 集成接口
        /// <summary>
        /// 申请单同步
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("requests")]
        [AllowAnonymous]
        public SyncResult<RequestDto> SyncRequest([FromBody]RequestDto source)
        {
            var result = _lisService.AddOrUpdateRequest(source);
            return result;
        }
        /// <summary>
        /// 检验报告同步
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("reports")]
        [AllowAnonymous]
        public SyncResult<ReportDto> SyncReport([FromBody]ReportDto source)
        {
            var result = _lisService.AddOrUpdateReport(source);
            return result;
        }

        /// <summary>
        /// 上传报告（非送检）同步
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("uploadreports")]
        [AllowAnonymous]
        public SyncResult<ReportDto> SyncUploadReport([FromBody]ReportDto source)
        {
            var result = _lisService.SyncUploadReport(source);
            return result;
        }

        /// <summary>
        /// 医疗机构信息同步
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("syncmi")]
        [AllowAnonymous]
        public IntegrationResponse SyncMi([FromBody]MiModel source)
        {
            var result = _lisService.SyncMi(source);
            return result;
        }

        /// <summary>
        /// 科室信息同步
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("syncdept")]
        [AllowAnonymous]
        public IntegrationResponse SyncDept([FromBody]DeptModel source)
        {
            var result = _lisService.SyncDept(source);
            return result;
        }

        /// <summary>
        /// 用户信息同步
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("syncuser")]
        [AllowAnonymous]
        public IntegrationResponse SyncUser([FromBody]UserModel source)
        {
            var result = _lisService.SyncUser(source);
            return result;
        }

        /// <summary>
        /// 修改手机号
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("syncmobile")]
        [AllowAnonymous]
        public IntegrationResponse SyncMobile([FromBody]ChangeMobileModel source)
        {
            var result = _lisService.SyncMobile(source);
            return result;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("syncpassword")]
        [AllowAnonymous]
        public IntegrationResponse SyncPassword([FromBody]ChangePasswordModel source)
        {
            var result = _lisService.SyncPassword(source);
            return result;
        }
        #endregion

        /// <summary>
        /// 搜索申请单信息
        /// </summary>
        /// <param name="search">搜索的关键字，支持申请单号、病人姓名</param>
        /// <param name="from">可选参数：申请单开始日期</param>
        /// <param name="to">可选参数：申请单日期日期</param>
        /// <param name="miId">申请单所属机构ID</param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("requests")]
        public List<RequestDto> SearchRequests(string search, DateTime? from, DateTime? to, string miId)
        {
            var result = _lisService.SearchRequests(search, from, to, miId);
            return result;
        }

        /// <summary>
        /// 获取申请单详细信息
        /// </summary>
        /// <param name="id">申请单ID</param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("requestdetail")]
        public RequestDto GetRequestDetail(string id)
        {
            var result = _lisService.GetRequestDetail(id);
            return result;
        }

        /// <summary>
        /// 申请单接收
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("requestaccept")]
        public bool AcceptRequest([FromBody]RequestDto source)
        {
            var result = _lisService.AcceptRequest(source);
            return result;
        }

        /// <summary>
        /// 申请单拒绝
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("requestrefuse")]
        public bool RefuseRequest(RequestDto source)
        {
            var result = _lisService.RefuseRequest(source);
            return result;
        }

        /// <summary>
        /// 获取检验报告数据
        /// </summary>
        /// <param name="id">申请单ID</param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("requestreports")]
        public RequestDto GetLabReport(string id)
        {
            var result = _lisService.GetLabReportByRequestId(id);
            return result;
        }

        /// <summary>
        /// 获取检验报告详情
        /// </summary>
        /// <param name="id">检验报告ID</param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("reportsdetail")]
        public ReportDto GetReport(string id)
        {
            var result = _lisService.GetReportDetail(id);
            return result;
        }

        /// <summary>
        /// 获取所有送检报告
        /// </summary>
        /// <param name="search">搜索关键字：组合名称、患者姓名、HidID</param>
        /// <param name="miInfo">机构ID或名称</param>
        /// <param name="reqDate">申请时间</param>
        /// <param name="patientId">病人编码</param>
        /// <param name="requestNo">申请单号</param>
        [HttpGet]
        [ActionName("reports")]
        public List<ReportDto> GetAllExamReports(string search, string miInfo, DateTime? reqDate, string patientId, string requestNo)
        {
            var result = _lisService.GetAllExamReports(search, miInfo, reqDate, patientId, requestNo);
            return result;
        }

        /// <summary>
        /// 样本送检
        /// </summary>
        /// <param name="source"></param>
        /// <returns>返回 本次物流报表数据</returns>
        [HttpPost]
        [ActionName("sendlogistics")]
        public LogisticsReport SendLogistics(LogisticsModel source)
        {
            var result = _lisService.SendLogistics(source);
            return result;
        }

        /// <summary>
        /// 样本接收
        /// </summary>
        /// <param name="source"></param>
        /// <returns>返回 本次物流报表数据</returns>
        [HttpPost]
        [ActionName("receivelogistics")]
        public LogisticsReport ReceiveLogistics(LogisticsModel source)
        {
            var result = _lisService.ReceiveLogistics(source);
            return result;
        }

        /// <summary>
        /// 物流信息查询
        /// </summary>
        /// <param name="from">开始日期（中心接收时间）</param>
        /// <param name="to">结束日期（中心接收时间）</param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("logistics")]
        public List<LogisticsDto> SearchLogistics(DateTime? from, DateTime? to)
        {
            var result = _lisService.SearchLogistics(from, to);
            return result;
        }

        /// <summary>
        /// 样本类型统计（按样本类型、机构分组）
        /// </summary>
        /// <param name="from">申请时间 - 开始</param>
        /// <param name="to">申请时间 - 结束</param>
        /// <param name="mi">机构ID或名称</param>
        /// <param name="sampleType">样本类型ID或名称</param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("samplereport")]
        public List<SampleReportModel> GetSampleReport(DateTime? from, DateTime? to, string mi, string sampleType)
        {
            var result = _lisService.GetSampleReport(from, to, mi, sampleType);
            return result;
        }

        /// <summary>
        /// 检验报告查询
        /// </summary>
        /// <param name="patientName"></param>
        /// <param name="idCard"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("reportsearch")]
        public List<ReportDto> SearchLogistics(string patientName, string idCard, DateTime? date)
        {
            var result = _lisService.SearchReports(patientName, idCard, date);
            return result;
        }

        [HttpGet]
        [ActionName("reportsearchtotal")]
        public int SearchLogisticsTotal(string patientName, string idCard, DateTime? date)
        {
            var result = _lisService.SearchLogisticsTotal(patientName, idCard, date);
            return result;
        }
    }
}