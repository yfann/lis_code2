using Lis.Service.Dtos;
using Lis.Domain.Entities;
using Lis.EntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lis.Service.Models;
using Lis.Service.Models.Integration;

namespace Lis.Service
{
    public interface ILisService : IDisposable
    {
        #region Third part 集成接口
        /// <summary>
        /// 申请单同步 -- Third part 集成接口
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        SyncResult<RequestDto> AddOrUpdateRequest(RequestDto source);

        /// <summary>
        /// 检验报告同步 -- Third part 集成接口
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        SyncResult<ReportDto> AddOrUpdateReport(ReportDto source);

        /// <summary>
        /// 医疗机构信息同步
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        IntegrationResponse SyncMi(MiModel source);

        /// <summary>
        /// 科室信息同步
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        IntegrationResponse SyncDept(DeptModel source);

        /// <summary>
        /// 用户信息同步
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        IntegrationResponse SyncUser(UserModel source);

        /// <summary>
        /// 修改手机号
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        IntegrationResponse SyncMobile(ChangeMobileModel source);

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        IntegrationResponse SyncPassword(ChangePasswordModel source);

        /// <summary>
        /// 同步上传报告（即非送检报告，仅支持查询和下载）
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        SyncResult<ReportDto> SyncUploadReport(ReportDto source);
        #endregion

        #region 申请单

        /// <summary>
        /// 申请单接收
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        bool AcceptRequest(RequestDto source);

        /// <summary>
        /// 申请单拒绝
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        bool RefuseRequest(RequestDto source);

        /// <summary>
        /// 获取申请单及检验报告数据
        /// </summary>
        /// <param name="id">申请单ID</param>
        /// <returns></returns>
        RequestDto GetLabReportByRequestId(string id);

        /// <summary>
        /// 获取检验报告详情
        /// </summary>
        /// <param name="reportId"></param>
        /// <returns></returns>
        ReportDto GetReportDetail(string reportId);

        /// <summary>
        /// 获取所有送检报告
        /// </summary>
        /// <param name="search">搜索关键字：组合名称、患者姓名、HidID</param>
        /// <param name="miInfo">机构ID或名称</param>
        /// <param name="reqDate">申请时间</param>
        /// <param name="patientId">病人编码</param>
        /// <param name="requestNo">申请单号</param>
        /// <returns></returns>
        List<ReportDto> GetAllExamReports(string search, string miInfo, DateTime? reqDate, string patientId, string requestNo);

        /// <summary>
        /// 搜索申请单信息
        /// </summary>
        /// <param name="search">搜索的关键字，支持申请单号、病人姓名</param>
        /// <param name="from">可选参数：申请单开始日期</param>
        /// <param name="to">可选参数：申请单日期日期</param>
        /// <param name="miId">申请单所属机构ID</param>
        /// <returns></returns>
        List<RequestDto> SearchRequests(string search, DateTime? from, DateTime? to, string miId);

        /// <summary>
        /// 获取申请单详细信息
        /// </summary>
        /// <param name="id">申请单ID</param>
        /// <returns></returns>
        RequestDto GetRequestDetail(string id);

        /// <summary>
        /// 检验报告查询
        /// </summary>
        /// <param name="patientName"></param>
        /// <param name="idCard"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        List<ReportDto> SearchReports(string patientName, string idCard, DateTime? date);
        int SearchLogisticsTotal(string patientName, string idCard, DateTime? date);
        #endregion

        #region 物流信息
        /// <summary>
        /// 样本送检
        /// </summary>
        /// <param name="source"></param>
        /// <returns>返回 本次物流报表数据</returns>
        LogisticsReport SendLogistics(LogisticsModel source);

        /// <summary>
        /// 样本接收
        /// </summary>
        /// <param name="source"></param>
        /// <returns>返回 本次物流成功接收的报表数据</returns>
        LogisticsReport ReceiveLogistics(LogisticsModel source);

        /// <summary>
        /// 物流信息查询
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        List<LogisticsDto> SearchLogistics(DateTime? from, DateTime? to);
        #endregion

        #region 统计报表
        /// <summary>
        /// 样本类型统计（按样本类型、机构分组）
        /// </summary>
        /// <param name="from">申请时间 - 开始</param>
        /// <param name="to">申请时间 - 结束</param>
        /// <param name="mi">机构ID或名称</param>
        /// <param name="sampleType">样本类型ID或名称</param>
        /// <returns></returns>
        List<SampleReportModel> GetSampleReport(DateTime? from, DateTime? to, string mi, string sampleType);
        #endregion
    }
}
