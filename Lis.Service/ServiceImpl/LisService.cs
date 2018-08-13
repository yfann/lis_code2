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
using Lis.Service.Cache;
using Lis.Service.Models.Integration;
using Newtonsoft.Json;

namespace Lis.Service.ServiceImpl
{
    public class LisService : DisposableServiceBase, ILisService
    {
        private readonly ILisContext _dbContext;
        private readonly ISystemLogger _logger;
        private readonly ISystemService _systemService;

        public LisService(ILisContext dbContext, ISystemLogger logger, ISystemService systemService)
        {
            _dbContext = dbContext;
            _logger = logger;
            _systemService = systemService;
        }

        #region Third part 集成接口
        /// <summary>
        /// 申请单同步
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public SyncResult<RequestDto> AddOrUpdateRequest(RequestDto source)
        {
            try
            {
                if (source == null)
                {
                    return new SyncResult<RequestDto>()
                    {
                        Code = 101,
                        Message = "参数不能为空"
                    };
                }
                else if (source.LabInfos == null || source.LabInfos.Count == 0)
                {
                    return new SyncResult<RequestDto>()
                    {
                        Code = 102,
                        Message = "LabInfos不能为空：需要包含至少一条检验项目信息"
                    };
                }
                else if (source.Patient == null)
                {
                    return new SyncResult<RequestDto>()
                    {
                        Code = 103,
                        Message = "Patient不能为空：应提供该申请单相关的患者信息"
                    };
                }
                else if (source.Patient.Id == null)
                {
                    return new SyncResult<RequestDto>()
                    {
                        Code = 104,
                        Message = "Patient.Id患者ID不能为空"
                    };
                }
                if (string.IsNullOrEmpty(source.Id))
                {
                    return new SyncResult<RequestDto>()
                    {
                        Code = 105,
                        Message = "参数ID不能为空值：申请单ID"
                    };
                }
                else if (string.IsNullOrEmpty(source.EmId))
                {
                    return new SyncResult<RequestDto>()
                    {
                        Code = 106,
                        Message = "EmId不能为空值"
                    };
                }
                else if (!_dbContext.Set<Employee>().Where(s => s.Id == source.EmId).Any())
                {
                    return new SyncResult<RequestDto>()
                    {
                        Code = 107,
                        Message = string.Format("EmId“{0}”不正确：申请员工ID", source.EmId)
                    };
                }
                else if (string.IsNullOrEmpty(source.MiId))
                {
                    return new SyncResult<RequestDto>()
                    {
                        Code = 108,
                        Message = "MiId申请机构ID不能为空值"
                    };
                }
                else if (!_dbContext.Set<MedicalInstitution>().Where(s => s.Id == source.MiId).Any())
                {
                    return new SyncResult<RequestDto>()
                    {
                        Code = 109,
                        Message = string.Format("MiId“{0}”不正确：机构ID", source.MiId)
                    };
                }
                else if (string.IsNullOrEmpty(source.LabCategoryId))
                {
                    return new SyncResult<RequestDto>()
                    {
                        Code = 110,
                        Message = "LabCategoryId不能为空值：检验分类ID"
                    };
                }
                else if (!_dbContext.Set<LabCategory>().Where(s => s.Id == source.LabCategoryId).Any())
                {
                    return new SyncResult<RequestDto>()
                    {
                        Code = 111,
                        Message = string.Format("LabCategoryId“{0}”不正确：检验分类ID", source.LabCategoryId)
                    };
                }
                else if (source.ReqTime == DateTime.MinValue)
                {
                    return new SyncResult<RequestDto>()
                    {
                        Code = 112,
                        Message = "ReqTime无效: 申请时间"
                    };
                }
                source.PatientId = source.Patient.Id;

                var infos = source.LabInfos;
                foreach (var info in infos)
                {
                    if (!_dbContext.Set<LabItem>().Where(s => s.Id == info.LabItemId).Any())
                    {
                        return new SyncResult<RequestDto>()
                        {
                            Code = 113,
                            Message = "LabItemId无效: 检验项目ID"
                        };
                    }

                    // 【tLabSample】
                    string sampleId = null;
                    if (info.LabSample != null)
                    {
                        sampleId = info.LabSample.Id;

                        info.LabSample.ReId = source.Id;
                        var sample = _dbContext.Set<LabSample>().Where(s => s.Id == info.LabSample.Id).FirstOrDefault();
                        if (sample == null)
                        {
                            var sampleEntity = Mapper.Map<LabSampleDto, LabSample>(info.LabSample);
                            if (string.IsNullOrEmpty(sampleEntity.Id))
                            {
                                sampleId = Guid.NewGuid().ToString();
                                sampleEntity.Id = sampleId;
                            }
                            _dbContext.Set<LabSample>().Add(sampleEntity);
                        }
                        else
                        {
                            Mapper.Map(info.LabSample, sample);
                        }
                    }

                    info.LabSampleId = sampleId;

                    // 【tLabInfo】
                    var targetInfo = _dbContext.Set<LabInfo>().Where(s => s.LabItemId == info.LabItemId).FirstOrDefault();
                    if (targetInfo != null)
                    {
                        Mapper.Map(info, targetInfo);
                    }
                    else
                    {
                        var infoEntity = Mapper.Map<LabInfoDto, LabInfo>(info);
                        infoEntity.ReId = source.Id;
                        if (string.IsNullOrEmpty(infoEntity.Id))
                        {
                            infoEntity.Id = Guid.NewGuid().ToString();
                        }
                        _dbContext.Set<LabInfo>().Add(infoEntity);
                    }
                }
                // 【tPatient】
                var patient = _dbContext.Set<Patient>().Where(s => s.Id == source.Patient.Id).FirstOrDefault();
                if (patient == null)
                {
                    if (string.IsNullOrEmpty(source.Patient.Id))
                    {
                        source.Patient.Id = Guid.NewGuid().ToString();
                    }
                    var patientEntity = Mapper.Map<PatientDto, Patient>(source.Patient);
                    _dbContext.Set<Patient>().Add(patientEntity);
                }
                else
                {
                    Mapper.Map(source.Patient, patient);
                }

                // 【tRequest】
                var request = _dbContext.Set<Requests>().Where(s => s.Id == source.Id).FirstOrDefault();
                if (request == null)
                {
                    if (string.IsNullOrEmpty(source.Id))
                    {
                        source.Id = Guid.NewGuid().ToString();
                    }
                    var requestEntity = Mapper.Map<RequestDto, Requests>(source);
                    _dbContext.Set<Requests>().Add(requestEntity);
                }
                else
                {
                    Mapper.Map(source, request);
                }

                _dbContext.SaveChanges();

                // 清除受影响的用户的可访问患者ID信息
                // 收影响的用户
                var usersInCurrentMi = _dbContext.Set<EmployeeMi>().Where(s => s.MiId == source.MiId).Select(s => s.EmployeeId).ToList();
                foreach (var userId in usersInCurrentMi)
                {
                    DefaultCache.ClearUserPatientCache(userId);
                }

                var data = GetRequestDetail(source.Id);
                return new SyncResult<RequestDto>()
                {
                    Code = 0,
                    Message = "Success",
                    Data = data
                };
            }
            catch (Exception ex)
            {
                _logger.Error("AddOrUpdateRequest error: " + ex.Message, ex);
                return new SyncResult<RequestDto>()
                {
                    Code = -1,
                    Message = "Exception occurs",
                    Data = null
                };
            }
        }

        /// <summary>
        /// 检验报告同步
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public SyncResult<ReportDto> AddOrUpdateReport(ReportDto source)
        {
            if (source == null)
            {
                return new SyncResult<ReportDto>()
                {
                    Code = 101,
                    Message = "参数不能为空"
                };
            }
            else if (source.Results == null || source.Results.Count == 0)
            {
                return new SyncResult<ReportDto>()
                {
                    Code = 102,
                    Message = "Results不能为空：需要包含检验结果信息"
                };
            }

            if (string.IsNullOrEmpty(source.ReId))
            {
                return new SyncResult<ReportDto>()
                {
                    Code = 103,
                    Message = "ReId不能为空：申请单ID不能为空"
                };
            }
            var requestExists = _dbContext.Set<Requests>().Where(s => s.Id == source.ReId).Any();
            if (!requestExists)
            {
                return new SyncResult<ReportDto>()
                {
                    Code = 104,
                    Message = string.Format("ReId无效（{0}）：申请单ID不存在", source.ReId)
                };
            }
            if (string.IsNullOrEmpty(source.LabItemSetId) || !_dbContext.Set<LabItemSet>().Where(s => s.Id == source.LabItemSetId).Any())
            {
                return new SyncResult<ReportDto>()
                {
                    Code = 105,
                    Message = string.Format("检验组合ID不存在或无效（检验组合ID:{0}）", source.LabItemSetId)
                };
            }
            if (string.IsNullOrEmpty(source.PatientId) || !_dbContext.Set<Patient>().Where(s => s.Id == source.PatientId).Any())
            {
                return new SyncResult<ReportDto>()
                {
                    Code = 106,
                    Message = string.Format("患者ID不存在或无效（患者ID:{0}）", source.PatientId)
                };
            }
            foreach (var temp in source.Results)
            {
                if (temp.LmId == null)
                {
                    return new SyncResult<ReportDto>()
                    {
                        Code = 107,
                        Message = "LmId：至少一个检验结果未关联到检验项目"
                    };
                }
                temp.ReId = source.ReId;
            }

            try
            {
                // 根据申请单ID + 检验组合ID判断该报告是否已经同步过： 若是，则更新记录，否则新建记录
                var target = _dbContext.Set<Report>().Where(s => s.ReId == source.ReId && s.LabItemSetId == source.LabItemSetId).FirstOrDefault();
                if (target == null)
                {
                    // 新建检验报告
                    var report = Mapper.Map<ReportDto, Report>(source);
                    report.RemoteId = source.Id;
                    report.Id = Guid.NewGuid().ToString();
                    source.Id = report.Id;
                    _dbContext.Set<Report>().Add(report);

                    // 录入检验结果
                    foreach (var result in source.Results)
                    {
                        var resultEntity = Mapper.Map<LabResultDto, LabResult>(result);
                        resultEntity.Id = Guid.NewGuid().ToString();
                        _dbContext.Set<LabResult>().Add(resultEntity);

                        // 关联到检验项目业务表
                        var labInfo = _dbContext.Set<LabInfo>().Where(s => s.ReId == source.ReId && s.LabItemId == result.LmId).FirstOrDefault();
                        if (labInfo == null)
                        {
                            return new SyncResult<ReportDto>()
                            {
                                Code = 108,
                                Message = string.Format("未能在申请单{0}中找到该检验结果相关的检验项目（项目ID为：{1}）", source.ReId, result.LmId)
                            };
                        }
                        labInfo.LabResultId = resultEntity.Id;
                        labInfo.ReportId = target.Id;

                        // 记录警报值
                        var crisis = _dbContext.Set<Crisis>().Where(s => s.LmId == result.LmId).FirstOrDefault();
                        if (crisis != null)
                        {
                            decimal currentVal;
                            if (decimal.TryParse(result.ResultValue, out currentVal))
                            {
                                if (currentVal >= crisis.CrisisUpper || currentVal <= crisis.CrisisLow)
                                {
                                    var reportCrisis = new ReportCrisis();
                                    reportCrisis.Id = Guid.NewGuid().ToString();
                                    reportCrisis.LmId = result.LmId;
                                    reportCrisis.CreateTime = DateTime.Now;
                                    reportCrisis.ResultValue = result.ResultValue;
                                    reportCrisis.CrisisRange = crisis.CrisisLow + "-" + crisis.CrisisUpper;
                                    reportCrisis.ReId = result.ReId;
                                    reportCrisis.PatientId = source.PatientId;
                                    reportCrisis.PatientName = source.PatientName;
                                    reportCrisis.Reporter = source.Inspector;
                                    reportCrisis.ReportTime = source.ReportTime;
                                    _dbContext.Set<ReportCrisis>().Add(reportCrisis);
                                }
                            }
                        }
                    }
                }
                else
                {
                    // 更新检验报告
                    Mapper.Map(source, target);

                    // 更新检验结果
                    foreach (var result in source.Results)
                    {
                        var existsResult = _dbContext.Set<LabResult>().Where(s => s.LmId == result.LmId && s.ReId == source.ReId).FirstOrDefault();
                        if (existsResult != null)
                        {
                            _dbContext.Set<LabResult>().Remove(existsResult);
                        }

                        var resultEntity = Mapper.Map<LabResultDto, LabResult>(result);
                        resultEntity.Id = Guid.NewGuid().ToString();
                        _dbContext.Set<LabResult>().Add(resultEntity);

                        // 关联到检验项目业务表
                        var labInfo = _dbContext.Set<LabInfo>().Where(s => s.ReId == source.ReId && s.LabItemId == result.LmId).FirstOrDefault();
                        if (labInfo == null)
                        {
                            return new SyncResult<ReportDto>()
                            {
                                Code = 108,
                                Message = string.Format("未能在申请单{0}中找到该检验结果相关的检验项目（项目ID为：{1}）", source.ReId, result.LmId)
                            };
                        }
                        labInfo.LabResultId = resultEntity.Id;
                        labInfo.ReportId = target.Id;

                        // 记录警报值
                        var exists = _dbContext.Set<ReportCrisis>().Where(s => s.ReId == source.ReId && s.LmId == result.LmId).FirstOrDefault();
                        if (exists != null)
                        {
                            _dbContext.Set<ReportCrisis>().Remove(exists);
                        }
                        var crisis = _dbContext.Set<Crisis>().Where(s => s.LmId == result.LmId).FirstOrDefault();
                        if (crisis != null)
                        {
                            decimal currentVal;
                            if (decimal.TryParse(result.ResultValue, out currentVal))
                            {
                                if (currentVal >= crisis.CrisisUpper || currentVal <= crisis.CrisisLow)
                                {
                                    var reportCrisis = new ReportCrisis();
                                    reportCrisis.Id = Guid.NewGuid().ToString();
                                    reportCrisis.CreateTime = DateTime.Now;
                                    reportCrisis.ResultValue = result.ResultValue;
                                    reportCrisis.CrisisRange = crisis.CrisisLow + "-" + crisis.CrisisUpper;
                                    reportCrisis.ReId = result.ReId;
                                    reportCrisis.PatientId = source.PatientId;
                                    reportCrisis.PatientName = source.PatientName;
                                    reportCrisis.Reporter = source.Inspector;
                                    reportCrisis.ReportTime = source.ReportTime;
                                    _dbContext.Set<ReportCrisis>().Add(reportCrisis);
                                }
                            }
                        }
                    }
                }
                _dbContext.SaveChanges();

                var returnData = _dbContext.Set<Report>().Where(s => s.Id == source.Id).FirstOrDefault();
                var mapped = Mapper.Map<Report, ReportDto>(returnData);
                return new SyncResult<ReportDto>()
                {
                    Code = 0,
                    Message = "Success",
                    Data = mapped
                };
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
                return new SyncResult<ReportDto>()
                {
                    Code = -1,
                    Message = string.Format("接口调用出错：{0}，{1}", e.Message, e.StackTrace)
                };
            }
        }

        /// <summary>
        /// 同步上传报告（即非送检报告，仅能查询和下载）
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public SyncResult<ReportDto> SyncUploadReport(ReportDto source)
        {
            var newReqId = Guid.NewGuid().ToString();
            if (source == null)
            {
                return new SyncResult<ReportDto>()
                {
                    Code = 101,
                    Message = "参数不能为空"
                };
            }
            else if (source.Results == null || source.Results.Count == 0)
            {
                return new SyncResult<ReportDto>()
                {
                    Code = 102,
                    Message = "Results不能为空：需要包含检验结果信息"
                };
            }
            else if (string.IsNullOrEmpty(source.Id))
            {
                return new SyncResult<ReportDto>()
                {
                    Code = 103,
                    Message = "报告Id不能为空"
                };
            }

            // 上传的报告只有患者信息和检验结果，无申请单信息。 此处申请单ID由系统重新生成
            source.ReId = newReqId;
            source.IsUpload = true; // 报告类型：上传报告

            if (string.IsNullOrEmpty(source.LabItemSetId) || !_dbContext.Set<LabItemSet>().Where(s => s.Id == source.LabItemSetId).Any())
            {
                return new SyncResult<ReportDto>()
                {
                    Code = 105,
                    Message = string.Format("检验组合ID不存在或无效（检验组合ID:{0}）", source.LabItemSetId)
                };
            }
            if (string.IsNullOrEmpty(source.PatientId) || !_dbContext.Set<Patient>().Where(s => s.Id == source.PatientId).Any())
            {
                return new SyncResult<ReportDto>()
                {
                    Code = 106,
                    Message = string.Format("患者ID不存在或无效（患者ID:{0}）", source.PatientId)
                };
            }
            foreach (var temp in source.Results)
            {
                if (temp.LmId == null)
                {
                    return new SyncResult<ReportDto>()
                    {
                        Code = 107,
                        Message = "LmId：至少一个检验结果未关联到检验项目"
                    };
                }
                temp.ReId = source.ReId;
            }

            try
            {
                // 根据报告ID判断该报告是否已经同步过： 若是，则先删除原有数据，新数据覆盖之；
                var target = _dbContext.Set<Report>().Where(s => s.Id == source.Id).FirstOrDefault();
                if (target != null)
                {
                    _dbContext.Set<Report>().Remove(target);

                    var req = _dbContext.Set<Requests>().Where(s => s.Id == target.ReId).FirstOrDefault();
                    if (req != null)
                    {
                        _dbContext.Set<Requests>().Remove(req);
                    }

                    var labInfos = _dbContext.Set<LabInfo>().Where(s => s.ReportId == source.Id).ToList();
                    _dbContext.Set<LabInfo>().RemoveRange(labInfos);

                    var labResults = _dbContext.Set<LabResult>().Where(s => s.ReId == target.ReId).ToList();
                    _dbContext.Set<LabResult>().RemoveRange(labResults);
                }

                // 新建检验报告
                var report = Mapper.Map<ReportDto, Report>(source);
                report.RemoteId = source.Id;
                report.Id = source.Id;
                _dbContext.Set<Report>().Add(report);

                // 申请单
                var newReq = new Requests();
                newReq.Id = newReqId;
                newReq.EmId = Consts.SuperUserId;
                newReq.MiId = Consts.DefaultSiteId;
                newReq.PatientId = source.PatientId;
                newReq.LabCategoryId = "";
                newReq.ReqTime = DateTime.Now;
                newReq.IsUpload = true;
                newReq.RequestNo = "";   // 上传报告无申请单号
                _dbContext.Set<Requests>().Add(newReq);

                // 录入检验结果
                foreach (var result in source.Results)
                {
                    result.ReId = newReqId;

                    var resultEntity = Mapper.Map<LabResultDto, LabResult>(result);
                    resultEntity.Id = Guid.NewGuid().ToString();
                    _dbContext.Set<LabResult>().Add(resultEntity);

                    // 检验项目业务表
                    var labInfo = new LabInfo()
                    {
                        Id = Guid.NewGuid().ToString(),
                        ReportId = source.Id,
                        LabItemId = result.LmId,
                        ReId = source.ReId,
                        LabResultId = resultEntity.Id,
                    };
                    _dbContext.Set<LabInfo>().Add(labInfo);

                    labInfo.LabResultId = resultEntity.Id;
                    labInfo.ReportId = source.Id;
                    labInfo.ReId = newReqId;

                    // 记录警报值
                    var crisis = _dbContext.Set<Crisis>().Where(s => s.LmId == result.LmId).FirstOrDefault();
                    if (crisis != null)
                    {
                        decimal currentVal;
                        if (decimal.TryParse(result.ResultValue, out currentVal))
                        {
                            if (currentVal >= crisis.CrisisUpper || currentVal <= crisis.CrisisLow)
                            {
                                var reportCrisis = new ReportCrisis();
                                reportCrisis.Id = Guid.NewGuid().ToString();
                                reportCrisis.LmId = result.LmId;
                                reportCrisis.CreateTime = DateTime.Now;
                                reportCrisis.ResultValue = result.ResultValue;
                                reportCrisis.CrisisRange = crisis.CrisisLow + "-" + crisis.CrisisUpper;
                                reportCrisis.ReId = result.ReId;
                                reportCrisis.PatientId = source.PatientId;
                                reportCrisis.PatientName = source.PatientName;
                                reportCrisis.Reporter = source.Inspector;
                                reportCrisis.ReportTime = source.ReportTime;
                                _dbContext.Set<ReportCrisis>().Add(reportCrisis);
                            }
                        }
                    }
                }

                _dbContext.SaveChanges();

                var returnData = _dbContext.Set<Report>().Where(s => s.Id == source.Id).FirstOrDefault();
                var mapped = Mapper.Map<Report, ReportDto>(returnData);
                return new SyncResult<ReportDto>()
                {
                    Code = 0,
                    Message = "Success",
                    Data = mapped
                };
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
                return new SyncResult<ReportDto>()
                {
                    Code = -1,
                    Message = string.Format("接口调用出错：{0}，{1}", e.Message, e.StackTrace)
                };
            }
        }

        /// <summary>
        /// 医疗机构信息同步
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public IntegrationResponse SyncMi(MiModel source)
        {
            if (source == null)
            {
                return IntegrationResponse.IntegrationResponse10002;
            }
            try
            {
                _logger.Info("Begin SyncMi with data:" + JsonConvert.SerializeObject(source));

                if (source.operate == "I")
                {
                    var dto = source.ConvertToDto();
                    var mi = Mapper.Map<MedicalInstitutionDto, MedicalInstitution>(dto);
                    _dbContext.Set<MedicalInstitution>().Add(mi);
                }
                else if (source.operate == "M")
                {
                    var target = _dbContext.Set<MedicalInstitution>().Where(s => s.Id == source.hospId).FirstOrDefault();
                    if (target == null)
                    {
                        return IntegrationResponse.IntegrationResponse10002;
                    }
                    var dto = source.ConvertToDto();
                    Mapper.Map(dto, target);
                }
                else if (source.operate == "D")
                {
                    var target = _dbContext.Set<MedicalInstitution>().Where(s => s.Id == source.hospId).FirstOrDefault();
                    if (target == null)
                    {
                        return IntegrationResponse.IntegrationResponse10002;
                    }
                    _dbContext.Set<MedicalInstitution>().Remove(target);
                }
                else
                {
                    _logger.Error("SyncMi error: invalid operate! operate is " + source.operate);
                    return IntegrationResponse.IntegrationResponse10002;
                }

                _dbContext.SaveChanges();
                return IntegrationResponse.IntegrationResponse200;
            }
            catch (Exception ex)
            {
                _logger.Error("SyncMi error:" + ex.Message, ex);
                return IntegrationResponse.IntegrationResponse500;
            }
        }

        /// <summary>
        /// 科室信息同步
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public IntegrationResponse SyncDept(DeptModel source)
        {
            if (source == null)
            {
                return IntegrationResponse.IntegrationResponse10002;
            }
            try
            {
                _logger.Info("Begin SyncDept with data:" + JsonConvert.SerializeObject(source));

                if (source.operate == "I")
                {
                    var dto = source.ConvertToDto();
                    var mi = Mapper.Map<DepartmentDto, Department>(dto);
                    _dbContext.Set<Department>().Add(mi);
                }
                else if (source.operate == "M")
                {
                    var target = _dbContext.Set<Department>().Where(s => s.Id == source.deptId).FirstOrDefault();
                    if (target == null)
                    {
                        return IntegrationResponse.IntegrationResponse10002;
                    }
                    var dto = source.ConvertToDto();
                    Mapper.Map(dto, target);
                }
                else if (source.operate == "D")
                {
                    var target = _dbContext.Set<Department>().Where(s => s.Id == source.deptId).FirstOrDefault();
                    if (target == null)
                    {
                        return IntegrationResponse.IntegrationResponse10002;
                    }
                    _dbContext.Set<Department>().Remove(target);
                }
                else
                {
                    _logger.Error("SyncDept error: invalid operate! operate is " + source.operate);
                    return IntegrationResponse.IntegrationResponse10002;
                }

                _dbContext.SaveChanges();
                return IntegrationResponse.IntegrationResponse200;
            }
            catch (Exception ex)
            {
                _logger.Error("SyncDept error:" + ex.Message, ex);
                return IntegrationResponse.IntegrationResponse500;
            }
        }

        /// <summary>
        /// 用户信息同步
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public IntegrationResponse SyncUser(UserModel source)
        {
            if (source == null)
            {
                return IntegrationResponse.IntegrationResponse10002;
            }
            try
            {
                _logger.Info("Begin SyncUser with data:" + JsonConvert.SerializeObject(source));

                if (source.hasFlag == "1" || source.hasFlag == "0")
                {
                    var dto = source.ConvertToDto();
                    var target = _dbContext.Set<Employee>().Where(s => s.Id == dto.Id).FirstOrDefault();
                    if (target == null)
                    {
                        var entity = Mapper.Map<EmployeeDto, Employee>(dto);
                        _dbContext.Set<Employee>().Add(entity);
                    }
                    {
                        Mapper.Map(dto, target);
                    }
                }
                else
                {
                    _logger.Error("SyncUser error: invalid hasFlag! hasFlag is " + source.hasFlag);
                    return IntegrationResponse.IntegrationResponse10002;
                }

                _dbContext.SaveChanges();
                return IntegrationResponse.IntegrationResponse200;
            }
            catch (Exception ex)
            {
                _logger.Error("SyncUser error:" + ex.Message, ex);
                return IntegrationResponse.IntegrationResponse500;
            }
        }

        /// <summary>
        /// 修改手机号
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public IntegrationResponse SyncMobile(ChangeMobileModel source)
        {
            if (source == null)
            {
                return IntegrationResponse.IntegrationResponse10002;
            }
            try
            {
                _logger.Info("Begin SyncMobile with data:" + JsonConvert.SerializeObject(source));
                var target = _dbContext.Set<Employee>().Where(s => s.EmName == source.userName && s.Phone == source.oldTelphone).FirstOrDefault();
                if (target == null)
                {
                    _logger.Error("SyncMobile error: invalid userName or oldTelphone! userName is " + source.userName + ", oldTelphone is " + source.oldTelphone);
                    return IntegrationResponse.IntegrationResponse10002;
                }
                else
                {
                    if (string.IsNullOrEmpty(source.telphone))
                    {
                        _logger.Error("SyncMobile error: invalid telphone! telphone is " + source.telphone);
                        return IntegrationResponse.IntegrationResponse10002;
                    }
                    target.Phone = source.telphone;
                    _dbContext.SaveChanges();
                    return IntegrationResponse.IntegrationResponse200;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("SyncMobile error:" + ex.Message, ex);
                return IntegrationResponse.IntegrationResponse500;
            }
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public IntegrationResponse SyncPassword(ChangePasswordModel source)
        {
            if (source == null)
            {
                return IntegrationResponse.IntegrationResponse10002;
            }
            try
            {
                _logger.Info("Begin SyncPassword with data:" + JsonConvert.SerializeObject(source));
                var target = _dbContext.Set<Employee>().Where(s => s.EmName == source.userName && s.Password == source.oldPassword).FirstOrDefault();
                if (target == null)
                {
                    _logger.Error("SyncPassword error: invalid userName or oldPassword! userName is " + source.userName + ", oldPassword is " + source.oldPassword);
                    return IntegrationResponse.IntegrationResponse10002;
                }
                else
                {
                    if (string.IsNullOrEmpty(source.password))
                    {
                        _logger.Error("SyncPassword error: invalid password! password is " + source.password);
                        return IntegrationResponse.IntegrationResponse10002;
                    }
                    target.Password = source.password;
                    _dbContext.SaveChanges();
                    return IntegrationResponse.IntegrationResponse200;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("SyncPassword error:" + ex.Message, ex);
                return IntegrationResponse.IntegrationResponse500;
            }
        }
        #endregion

        /// <summary>
        /// 申请单接收
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public bool AcceptRequest(RequestDto source)
        {
            var target = _dbContext.Set<Requests>().Where(s => s.Id == source.Id).FirstOrDefault();
            if (target == null)
            {
                CommonFunc.ThrowExceptionIfRecordNotExists(EntityNames.Request, source.Id, OperateType.Update, _logger);
            }

            else if (!Enumerable.SequenceEqual(source.Version, target.Version))
            {
                var modifiedUser = _systemService.GetEmployeeDetail(target.LastUpdateUserId);
                CommonFunc.ThrowExceptionIfRecordHasBeenModified(EntityNames.Request, source.Id, modifiedUser.EmName, target.LastUpdateTime, OperateType.Update, _logger);
            }

            if (target.ReStatus != (int)RequestStatus.Submitted)
            {
                throw new Exception("接收失败！ 当前申请单已接收或已拒绝！");
            }
            target.ReStatus = (int)RequestStatus.Accepted;
            var affected = _dbContext.SaveChanges();
            return affected == 1;
        }

        /// <summary>
        /// 搜索申请单信息
        /// </summary>
        /// <param name="search">搜索的关键字，支持申请单号、病人姓名</param>
        /// <param name="from">可选参数：申请单开始日期</param>
        /// <param name="to">可选参数：申请单日期日期</param>
        /// <param name="miId">申请单所属机构ID</param>
        /// <returns></returns>
        public List<RequestDto> SearchRequests(string search, DateTime? from, DateTime? to, string miId)
        {
            var query = (from req in _dbContext.Set<Requests>()
                         join pat in _dbContext.Set<Patient>() on req.PatientId equals pat.Id
                         join mi in _dbContext.Set<MedicalInstitution>() on req.MiId equals mi.Id into mis
                         from mi in mis.DefaultIfEmpty()
                         join em in _dbContext.Set<Employee>() on req.EmId equals em.Id into ems
                         from em in ems.DefaultIfEmpty()
                         where !req.IsUpload
                         select new { req, pat, mi.MiName, em.EmName }).AsNoTracking().AsQueryable();
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(s => s.req.RequestNo.Contains(search) || s.pat.PtName.Contains(search));
            }
            if (from.HasValue)
            {
                var fromDate = from.Value.Date;
                query = query.Where(s => s.req.ReqTime >= fromDate);
            }
            if (to.HasValue)
            {
                var toDate = to.Value.Date.AddDays(1);
                query = query.Where(s => s.req.ReqTime < toDate);
            }
            if (!string.IsNullOrEmpty(miId))
            {
                query = query.Where(s => s.req.MiId == miId);
            }

            // 当前用户的可访问机构
            var visitMiIds = DefaultCache.GetUserMi(ServiceContext.UserId, _dbContext);
            query = query.Where(s => visitMiIds.Contains(s.req.MiId));

            var result = new List<RequestDto>();
            foreach (var temp in query)
            {
                var dto = Mapper.Map<Requests, RequestDto>(temp.req);
                dto.Patient = Mapper.Map<Patient, PatientDto>(temp.pat);
                dto.MiName = temp.MiName;
                dto.EmName = temp.EmName;
                result.Add(dto);
            }
            return result;
        }

        /// <summary>
        /// 获取申请单详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RequestDto GetRequestDetail(string id)
        {
            // 申请单信息
            var target = _dbContext.Set<Requests>().Where(s => s.Id == id).FirstOrDefault();
            if (target == null)
            {
                return null;
            }
            var result = Mapper.Map<Requests, RequestDto>(target);

            // 病人信息
            var patient = _dbContext.Set<Patient>().Where(s => s.Id == result.PatientId).FirstOrDefault();
            if (patient != null)
            {
                result.Patient = Mapper.Map<Patient, PatientDto>(patient);
            }

            // 检查项目信息 (含检查结果)
            var labInfos = (from labInfo in _dbContext.Set<LabInfo>()
                            join labItem in _dbContext.Set<LabItem>() on labInfo.LabItemId equals labItem.Id
                            join labResult in _dbContext.Set<LabResult>() on labInfo.LabItemId equals labResult.LmId into labResults
                            from labResult in labResults.DefaultIfEmpty()
                            where labInfo.ReId == result.Id
                            select new { labInfo, labItem, labResult }).ToList();

            result.LabInfos = new List<LabInfoDto>();
            foreach (var temp in labInfos)
            {
                var dto = Mapper.Map<LabInfo, LabInfoDto>(temp.labInfo);
                dto.LabItem = Mapper.Map<LabItem, LabItemDto>(temp.labItem);
                dto.LabResult = Mapper.Map<LabResult, LabResultDto>(temp.labResult);

                var sample = _dbContext.Set<LabSample>().Where(s => s.Id == dto.LabSampleId).FirstOrDefault();
                if (sample != null)
                {
                    var sampleDto = Mapper.Map<LabSample, LabSampleDto>(sample);
                    var logistics = _dbContext.Set<Logistics>().Where(s => s.Id == sample.LogisticsId).FirstOrDefault();
                    if (logistics != null)
                    {
                        var logisticsDto = Mapper.Map<Logistics, LogisticsDto>(logistics);
                        if (logisticsDto.SendEmId != null)
                        {
                            logisticsDto.SendEmName = _dbContext.Set<Employee>().Where(s => s.Id == logisticsDto.SendEmId).Select(s => s.EmName).FirstOrDefault();
                        }
                        if (logisticsDto.LsEmId != null)
                        {
                            logisticsDto.LsEmName = _dbContext.Set<Employee>().Where(s => s.Id == logisticsDto.LsEmId).Select(s => s.EmName).FirstOrDefault();
                        }
                        if (logisticsDto.CenterEmId != null)
                        {
                            logisticsDto.CenterEmName = _dbContext.Set<Employee>().Where(s => s.Id == logisticsDto.CenterEmId).Select(s => s.EmName).FirstOrDefault();
                        }
                        sampleDto.Logistics = logisticsDto;
                    }
                    dto.LabSample = sampleDto;
                }
                result.LabInfos.Add(dto);
            }

            // 申请机构名称
            result.MiName = _dbContext.Set<MedicalInstitution>().Where(s => s.Id == result.MiId).Select(s => s.MiName).FirstOrDefault();

            // 申请员工名称
            result.EmName = _dbContext.Set<Employee>().Where(s => s.Id == result.EmId).Select(s => s.EmName).FirstOrDefault();

            // 报告信息
            var reports = _dbContext.Set<Report>().Where(s => s.ReId == id).ToList().Select(Mapper.Map<Report, ReportDto>).ToList();
            foreach (var report in reports)
            {
                report.Results = result.LabInfos.Where(s => s.ReportId == report.Id).Select(s => s.LabResult).ToList();
                report.SetName = _dbContext.Set<LabItemSet>().Where(s => s.Id == report.LabItemSetId).Select(s => s.LisName).FirstOrDefault();
            }
            result.Reports = reports;

            return result;
        }

        /// <summary>
        /// 获取检验报告数据
        /// </summary>
        /// <param name="id">申请单ID</param>
        /// <returns></returns>
        public RequestDto GetLabReportByRequestId(string id)
        {
            var query = _dbContext.Set<Requests>().Where(s => s.Id == id).FirstOrDefault();
            if (query == null)
            {
                throw new Exception("该申请单不存在，可能已被删除！");
            }

            return GetRequestDetail(id);
        }

        /// <summary>
        /// 申请单拒绝
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public bool RefuseRequest(RequestDto source)
        {
            var target = _dbContext.Set<Requests>().Where(s => s.Id == source.Id).FirstOrDefault();
            if (target == null)
            {
                CommonFunc.ThrowExceptionIfRecordNotExists(EntityNames.Request, source.Id, OperateType.Update, _logger);
            }

            else if (!Enumerable.SequenceEqual(source.Version, target.Version))
            {
                var modifiedUser = _systemService.GetEmployeeDetail(target.LastUpdateUserId);
                CommonFunc.ThrowExceptionIfRecordHasBeenModified(EntityNames.Request, source.Id, modifiedUser.EmName, target.LastUpdateTime, OperateType.Update, _logger);
            }

            if (target.ReStatus != (int)RequestStatus.Submitted)
            {
                throw new Exception("拒绝申请单失败！ 当前申请单已接收或已拒绝！");
            }
            target.ReStatus = (int)RequestStatus.Refused;
            var affected = _dbContext.SaveChanges();
            return affected == 1;
        }


        #region 物流信息
        /// <summary>
        /// 样本送检
        /// </summary>
        /// <param name="source"></param>
        /// <returns>返回 本次物流报表数据</returns>
        public LogisticsReport SendLogistics(LogisticsModel source)
        {
            if (string.IsNullOrEmpty(source.SendEmId))
            {
                throw new Exception("请选择送检人！");
            }
            if (string.IsNullOrEmpty(source.LsEmId))
            {
                throw new Exception("请选择物流接收人！");
            }

            // 记录本次物流信息
            var logisticsId = Guid.NewGuid().ToString();
            var logistic = new Logistics();
            logistic.Id = logisticsId;
            logistic.SendEmId = source.SendEmId;
            logistic.SendTime = DateTime.Now;
            logistic.LsEmId = source.LsEmId;
            logistic.LsReceiveTime = DateTime.Now;
            logistic.LsStatus = 1; // 物流已接收
            _dbContext.Set<Logistics>().Add(logistic);

            // 更新有效样本的物流ID
            // 依据： 提供的条形码是否属于系统中的已有样本
            var sampels = _dbContext.Set<LabSample>().Where(s => source.BarCodes.Contains(s.BarCode)).ToList();
            var acceptedBarCodes = new List<string>();
            foreach (var sample in sampels)
            {
                sample.LogisticsId = logisticsId;
                if (!acceptedBarCodes.Contains(sample.BarCode))
                {
                    acceptedBarCodes.Add(sample.BarCode);
                }
            }

            _dbContext.SaveChanges();

            var result = new LogisticsReport();
            result.SendEmName = _dbContext.Set<Employee>().Where(s => s.Id == source.SendEmId).Select(s => s.EmName).FirstOrDefault();
            result.LsEmName = _dbContext.Set<Employee>().Where(s => s.Id == source.LsEmId).Select(s => s.EmName).FirstOrDefault();
            result.ReceiveTime = DateTime.Now;
            result.LabSamples = _dbContext.Set<LabSample>().Where(s => acceptedBarCodes.Contains(s.BarCode)).ToList().Select(Mapper.Map<LabSample, LabSampleDto>).ToList();
            return result;
        }

        /// <summary>
        /// 样本接收
        /// </summary>
        /// <param name="source"></param>
        /// <returns>返回 本次物流成功接收的报表数据</returns>
        public LogisticsReport ReceiveLogistics(LogisticsModel source)
        {
            if (string.IsNullOrEmpty(source.CenterEmId))
            {
                throw new Exception("必须选择中心接收人员！");
            }

            // 更新物流状态为 中心接收
            // 依据： 提供的条形码是否属于系统中的已有样本
            var sampels = _dbContext.Set<LabSample>().Where(s => source.BarCodes.Contains(s.BarCode)).ToList();
            var acceptedBarCodes = new List<string>();
            foreach (var sample in sampels)
            {
                var logisticsId = sample.LogisticsId;
                if (logisticsId != null)
                {
                    var logstics = _dbContext.Set<Logistics>().Where(s => s.Id == logisticsId).FirstOrDefault();
                    if (logstics != null)
                    {
                        logstics.LsStatus = 2;  // 中心已接收
                        logstics.CenterReceiveTime = DateTime.Now;
                        logstics.CenterEmId = source.CenterEmId;

                        if (!acceptedBarCodes.Contains(sample.BarCode))
                        {
                            acceptedBarCodes.Add(sample.BarCode);
                        }
                    }
                }
            }

            _dbContext.SaveChanges();
            var result = new LogisticsReport();
            result.CenterEmName = _dbContext.Set<Employee>().Where(s => s.Id == source.CenterEmId).Select(s => s.EmName).FirstOrDefault();
            result.ReceiveTime = DateTime.Now;
            result.LabSamples = _dbContext.Set<LabSample>().Where(s => acceptedBarCodes.Contains(s.BarCode)).ToList().Select(Mapper.Map<LabSample, LabSampleDto>).ToList();
            return result;
        }

        /// <summary>
        /// 物流信息查询
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public List<LogisticsDto> SearchLogistics(DateTime? from, DateTime? to)
        {
            var query = (from logistics in _dbContext.Set<Logistics>()
                         join em1 in _dbContext.Set<Employee>() on logistics.SendEmId equals em1.Id into em1s
                         from em1 in em1s.DefaultIfEmpty()
                         join em2 in _dbContext.Set<Employee>() on logistics.LsEmId equals em2.Id into em2s
                         from em2 in em2s.DefaultIfEmpty()
                         join em3 in _dbContext.Set<Employee>() on logistics.CenterEmId equals em3.Id into em3s
                         from em3 in em3s.DefaultIfEmpty()
                         select new
                         {
                             logistics = logistics,
                             SendEmName = em1.EmName,
                             LsEmName = em2.EmName,
                             CenterEmName = em3.EmName
                         }).AsQueryable();
            if (from.HasValue)
            {
                var fromDate = from.Value.Date;
                query = query.Where(s => s.logistics.CenterReceiveTime >= fromDate);
            }
            if (to.HasValue)
            {
                var toDate = to.Value.Date.AddDays(1);
                query = query.Where(s => s.logistics.CenterReceiveTime < toDate);
            }
            var result = new List<LogisticsDto>();
            foreach (var temp in query)
            {
                var dto = Mapper.Map<Logistics, LogisticsDto>(temp.logistics);
                dto.SendEmName = temp.SendEmName;
                dto.LsEmName = temp.LsEmName;
                dto.CenterEmName = temp.CenterEmName;
                result.Add(dto);
            }
            return result;
        }
        #endregion

        #region 统计报表
        /// <summary>
        /// 样本类型统计（按样本类型、机构分组）
        /// </summary>
        /// <param name="fromDate">申请时间 - 开始</param>
        /// <param name="toDate">申请时间 - 结束</param>
        /// <param name="miInfo">机构ID或名称</param>
        /// <param name="sampleTypeInfo">样本类型ID或名称</param>
        /// <returns></returns>
        public List<SampleReportModel> GetSampleReport(DateTime? fromDate, DateTime? toDate, string miInfo, string sampleTypeInfo)
        {
            var query = from sample in _dbContext.Set<LabSample>()
                        join req in _dbContext.Set<Requests>() on sample.ReId equals req.Id
                        join sampleType in _dbContext.Set<SampleType>() on sample.SampleTypeId equals sampleType.Id
                        join mi in _dbContext.Set<MedicalInstitution>() on req.MiId equals mi.Id
                        select new { sampleId = sample.Id, reqTime = req.ReqTime, reId = req.Id, sampleTypeId = sampleType.Id, sampleTypeName = sampleType.ChtName, miId = mi.Id, miName = mi.MiName }
                        ;
            if (fromDate.HasValue)
            {
                var start = fromDate.Value.Date;
                query = query.Where(s => s.reqTime >= start);
            };
            if (toDate.HasValue)
            {
                var to = toDate.Value.Date.AddDays(1);
                query = query.Where(s => s.reqTime < to);
            }

            if (!string.IsNullOrWhiteSpace(miInfo))
            {
                query = query.Where(s => s.miId == miInfo || s.miName == miInfo);
            }

            if (!string.IsNullOrWhiteSpace(sampleTypeInfo))
            {
                query = query.Where(s => s.sampleTypeId == sampleTypeInfo || s.sampleTypeName == sampleTypeInfo);
            }

            var result = new List<SampleReportModel>();
            var list = query.GroupBy(s => new { s.sampleTypeId, s.miId, s.sampleTypeName, s.miName })
                .Select(s => new { s.Key.miName, s.Key.sampleTypeName, data = s.ToList() });
            foreach (var temp in list)
            {
                result.Add(new SampleReportModel()
                {
                    MiName = temp.miName,
                    SampleTypeName = temp.sampleTypeName,
                    RequestCount = temp.data.Select(s => s.reId).Distinct().Count()
                });
            }

            return result;
        }

        /// <summary>
        /// 获取检验报告详情
        /// </summary>
        /// <param name="reportId"></param>
        /// <returns></returns>
        public ReportDto GetReportDetail(string reportId)
        {
            var report = _dbContext.Set<Report>().Where(s => s.Id == reportId).FirstOrDefault();
            if (report != null)
            {
                var dto = Mapper.Map<Report, ReportDto>(report);

                // 查询年龄
                var patient = _dbContext.Set<Patient>().Where(s => s.Id == dto.PatientId).FirstOrDefault();
                if (patient != null)
                {
                    dto.Age = patient.Age;
                }

                var infos = _dbContext.Set<LabInfo>().Where(s => s.ReportId == report.Id).ToList().Select(Mapper.Map<LabInfo, LabInfoDto>).ToList();
                foreach (var info in infos)
                {
                    var labItem = _dbContext.Set<LabItem>().Where(s => s.Id == info.LabItemId).FirstOrDefault();
                    info.LabItem = Mapper.Map<LabItem, LabItemDto>(labItem);

                    var result = _dbContext.Set<LabResult>().Where(s => s.Id == info.LabResultId).FirstOrDefault();
                    info.LabResult = Mapper.Map<LabResult, LabResultDto>(result);

                    var reId = info.LabResult.ReId;
                    info.LabResult.MiId = _dbContext.Set<Requests>().Where(s => s.Id == reId).Select(s => s.MiId).FirstOrDefault();
                    info.LabResult.MiName = _dbContext.Set<MedicalInstitution>().Where(s => s.Id == info.LabResult.MiId).Select(s => s.MiName).FirstOrDefault();
                }
                dto.Details = infos;

                // 检验报告的检验组合名称
                dto.SetName = _dbContext.Set<LabItemSet>().Where(s => s.Id == dto.LabItemSetId).Select(s => s.LisName).FirstOrDefault();
                return dto;
            }
            return null;
        }

        /// <summary>
        /// 获取所有送检报告
        /// </summary>
        /// <param name="search">搜索关键字：组合名称、患者姓名、HidID</param>
        /// <param name="miInfo">机构ID或名称</param>
        /// <param name="reqDate">申请时间</param>
        /// <param name="patientId">病人编码</param>
        /// <param name="requestNo">申请单号</param>
        /// <returns></returns>
        public List<ReportDto> GetAllExamReports(string search, string miInfo, DateTime? reqDate, string patientId, string requestNo)
        {
            var reports = (from rpt in _dbContext.Set<Report>()
                           join set in _dbContext.Set<LabItemSet>() on rpt.LabItemSetId equals set.Id
                           join req in _dbContext.Set<Requests>() on rpt.ReId equals req.Id
                           join patient in _dbContext.Set<Patient>() on req.PatientId equals patient.Id
                           join mi in _dbContext.Set<MedicalInstitution>() on req.MiId equals mi.Id
                           where !rpt.IsUpload
                           select new { rpt, set.LisName, miId = mi.Id, mi.MiName, req.ReqTime, reqId = req.Id, req.RequestNo, pid = patient.Id, patient.InnerId });
            if (!string.IsNullOrWhiteSpace(search))
            {
                reports = reports.Where(s => s.LisName.Contains(search) || s.rpt.PatientName.Contains(search) || s.rpt.HisId.Contains(search));
            }
            if (!string.IsNullOrWhiteSpace(miInfo))
            {
                reports = reports.Where(s => s.MiName == miInfo || s.miId == miInfo);
            }
            if (!string.IsNullOrWhiteSpace(patientId))
            {
                reports = reports.Where(s => s.pid == patientId || s.InnerId == patientId);
            }
            if (!string.IsNullOrWhiteSpace(requestNo))
            {
                reports = reports.Where(s => s.RequestNo == requestNo || s.reqId == requestNo);
            }
            if (reqDate.HasValue)
            {
                var from = reqDate.Value.Date;
                var to = reqDate.Value.Date.AddDays(1);
                reports = reports.Where(s => s.ReqTime >= from && s.ReqTime < to);
            }

            //  只要这个病人在可访问机构内有数据，那么这个病人的所有数据（不管在哪个机构）都可以访问
            var canViewPatientIds = DefaultCache.GetUserPatient(ServiceContext.UserId, _dbContext);
            reports = reports.Where(s => canViewPatientIds.Contains(s.pid));

            var result = new List<ReportDto>();
            foreach (var report in reports)
            {
                var dto = Mapper.Map<Report, ReportDto>(report.rpt);

                // 检验报告的检验组合名称
                dto.SetName = report.LisName;
                dto.MiId = report.miId;
                dto.MiName = report.MiName;

                result.Add(dto);
            }
            return result;
        }

        /// <summary>
        /// 检验报告查询
        /// </summary>
        /// <param name="patientName"></param>
        /// <param name="idCard"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public List<ReportDto> SearchReports(string patientName, string idCard, DateTime? date)
        {
            var query = from rpt in _dbContext.Set<Report>()
                        join labset in _dbContext.Set<LabItemSet>() on rpt.LabItemSetId equals labset.Id
                        join pat in _dbContext.Set<Patient>() on rpt.PatientId equals pat.Id
                        select new { rpt, labset.LisName, rpt.PatientName, pat.BirthDay, pat.InnerId, rpt.MiId };

            if (!string.IsNullOrEmpty(patientName))
            {
                query = query.Where(s => s.PatientName == patientName);
            }
            if (!string.IsNullOrEmpty(idCard))
            {
                query = query.Where(s => s.InnerId == idCard);
            }
            if (date.HasValue)
            {
                var fromDate = date.Value.Date;
                var toDate = date.Value.Date.AddDays(1);
                query = query.Where(s => s.rpt.ApplicationTime >= fromDate && s.rpt.ApplicationTime < toDate);
            }

            var miDic = new Dictionary<string, string>();
            var mis = _dbContext.Set<MedicalInstitution>().Select(s => new { s.Id, s.MiName }).ToList();
            foreach(var temp in mis)
            {
                if (!miDic.ContainsKey(temp.Id))
                {
                    miDic.Add(temp.Id, temp.MiName);
                }
            }

            var ret = query.ToList();
            var result = new List<ReportDto>();
            foreach (var temp in ret)
            {
                var dto = Mapper.Map<Report, ReportDto>(temp.rpt);
                dto.SetName = temp.LisName;
                dto.MiName = miDic.ContainsKey(temp.MiId) ? miDic[temp.MiId] : string.Empty;
                dto.BirthDay = temp.BirthDay.HasValue ? temp.BirthDay.Value.ToString("yyyy-MM-dd") : string.Empty;
                result.Add(dto);
            }
            return result;
        }


        public int SearchLogisticsTotal(string patientName, string idCard, DateTime? date)
        {
            var query = from rpt in _dbContext.Set<Report>()
                        join labset in _dbContext.Set<LabItemSet>() on rpt.LabItemSetId equals labset.Id
                        join pat in _dbContext.Set<Patient>() on rpt.PatientId equals pat.Id
                        select new { rpt, labset.LisName, rpt.PatientName, pat.BirthDay, pat.InnerId, rpt.MiId };

            if (!string.IsNullOrEmpty(patientName))
            {
                query = query.Where(s => s.PatientName == patientName);
            }
            if (!string.IsNullOrEmpty(idCard))
            {
                query = query.Where(s => s.InnerId == idCard);
            }
            if (date.HasValue)
            {
                var fromDate = date.Value.Date;
                var toDate = date.Value.Date.AddDays(1);
                query = query.Where(s => s.rpt.ApplicationTime >= fromDate && s.rpt.ApplicationTime < toDate);
            }
            var result = query.Count();
            return result;
        }

        #endregion
    }
}
