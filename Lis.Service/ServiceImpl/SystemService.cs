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
using Lis.Service.Cache;

namespace Lis.Service.ServiceImpl
{
    public class SystemService : DisposableServiceBase, ISystemService
    {
        private readonly ILisContext _dbContext;
        private readonly ISystemLogger _logger;

        public SystemService(ILisContext dbContext, ISystemLogger logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        #region 医疗机构
        /// <summary>
        /// 医疗机构信息维护
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public MedicalInstitutionDto AddOrUpdateMedicalInstitution(MedicalInstitutionDto source)
        {
            var isAddNew = string.IsNullOrEmpty(source.Id);
            if (isAddNew)
            {
                source.Id = Guid.NewGuid().ToString();
                var entity = Mapper.Map<MedicalInstitutionDto, MedicalInstitution>(source);
                _dbContext.Set<MedicalInstitution>().Add(entity);
            }
            else
            {
                var target = _dbContext.Set<MedicalInstitution>().Where(s => s.Id == source.Id).FirstOrDefault();
                if (target == null)
                {
                    CommonFunc.ThrowExceptionIfRecordNotExists(EntityNames.MedicalInstitution, source.Id, OperateType.Update, _logger);
                }

                else if (!Enumerable.SequenceEqual(source.Version, target.Version))
                {
                    var modifiedUser = GetEmployeeDetail(target.LastUpdateUserId);
                    CommonFunc.ThrowExceptionIfRecordHasBeenModified(EntityNames.MedicalInstitution, source.Id, modifiedUser.EmName, target.LastUpdateTime, OperateType.Update, _logger);
                }
                Mapper.Map(source, target);
            }
            _dbContext.SaveChanges();
            return GetMedicalInstitutionDetail(source.Id);
        }

        /// <summary>
        /// 删除医疗机构
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public bool DeleteMedicalInstitution(MedicalInstitutionDto source)
        {
            var target = _dbContext.Set<MedicalInstitution>().Where(s => s.Id == source.Id).FirstOrDefault();
            if (target == null)
            {
                CommonFunc.ThrowExceptionIfRecordNotExists(EntityNames.MedicalInstitution, source.Id, OperateType.Delete, _logger);
            }

            else if (!Enumerable.SequenceEqual(source.Version, target.Version))
            {
                var modifiedUser = GetEmployeeDetail(target.LastUpdateUserId);
                CommonFunc.ThrowExceptionIfRecordHasBeenModified(EntityNames.MedicalInstitution, source.Id, modifiedUser.EmName, target.LastUpdateTime, OperateType.Delete, _logger);
            }
            _dbContext.Set<MedicalInstitution>().Remove(target);
            var affected = _dbContext.SaveChanges();
            return affected == 1;
        }

        /// <summary>
        /// 医疗机构信息查询
        /// </summary>
        /// <param name="search">搜索关键字</param>
        /// <param name="parent">上级机构ID或者上级机构名称</param>
        /// <returns></returns>
        public List<MedicalInstitutionDto> GetAllMedicalInstitutions(string search, string parent)
        {
            var query = (from mi in _dbContext.Set<MedicalInstitution>() select mi).AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(s => s.MiName.Contains(search) || s.MiCode.Contains(search));
            }
            if (!string.IsNullOrWhiteSpace(parent))
            {
                query = query.Where(s => s.ParentId == parent || s.ParentName == parent);
            }
            return query.ToList().Select(Mapper.Map<MedicalInstitution, MedicalInstitutionDto>).ToList();
        }

        /// <summary>
        /// 获取医疗机构明细信息
        /// </summary>
        /// <param name="id">医疗机构ID</param>
        /// <returns></returns>
        public MedicalInstitutionDto GetMedicalInstitutionDetail(string id)
        {
            var target = _dbContext.Set<MedicalInstitution>().Where(s => s.Id == id).FirstOrDefault();
            if(target == null)
            {
                return null;
            }
            var result = Mapper.Map<MedicalInstitution, MedicalInstitutionDto>(target);
            return result;
        }
        #endregion

        #region 部门
        /// <summary>
        /// 部门信息维护
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public DepartmentDto AddOrUpdateDepartment(DepartmentDto source)
        {
            var isAddNew = string.IsNullOrEmpty(source.Id);
            if (isAddNew)
            {
                source.Id = Guid.NewGuid().ToString();
                var entity = Mapper.Map<DepartmentDto, Department>(source);
                _dbContext.Set<Department>().Add(entity);
            }
            else
            {
                var target = _dbContext.Set<Department>().Where(s => s.Id == source.Id).FirstOrDefault();
                if (target == null)
                {
                    CommonFunc.ThrowExceptionIfRecordNotExists(EntityNames.Dept, source.Id, OperateType.Update, _logger);
                }

                else if (!Enumerable.SequenceEqual(source.Version, target.Version))
                {
                    var modifiedUser = GetEmployeeDetail(target.LastUpdateUserId);
                    CommonFunc.ThrowExceptionIfRecordHasBeenModified(EntityNames.Dept, source.Id, modifiedUser.EmName, target.LastUpdateTime, OperateType.Update, _logger);
                }
                Mapper.Map(source, target);
            }
            _dbContext.SaveChanges();
            return GetDepartmentDetail(source.Id);
        }

        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public bool DeleteDepartment(DepartmentDto source)
        {
            var target = _dbContext.Set<Department>().Where(s => s.Id == source.Id).FirstOrDefault();
            if (target == null)
            {
                CommonFunc.ThrowExceptionIfRecordNotExists(EntityNames.Dept, source.Id, OperateType.Delete, _logger);
            }

            else if (!Enumerable.SequenceEqual(source.Version, target.Version))
            {
                var modifiedUser = GetEmployeeDetail(target.LastUpdateUserId);
                CommonFunc.ThrowExceptionIfRecordHasBeenModified(EntityNames.Dept, source.Id, modifiedUser.EmName, target.LastUpdateTime, OperateType.Delete, _logger);
            }
            _dbContext.Set<Department>().Remove(target);
            var affected = _dbContext.SaveChanges();
            return affected == 1;
        }

        /// <summary>
        /// 部门信息查询
        /// </summary>
        /// <param name="search">搜索关键字</param>
        /// <param name="org">所属医疗机构ID或者名称</param>
        /// <returns></returns>
        public List<DepartmentDto> GetAllDepartments(string search, string org)
        {
            var query = (from dept in _dbContext.Set<Department>()
                         join mi in _dbContext.Set<MedicalInstitution>() on dept.SiteId equals mi.Id
                         select new { dept, mi.MiName }).AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(s => s.dept.DeptCode.Contains(search) || s.dept.DeptName.Contains(search));
            }
            if (!string.IsNullOrWhiteSpace(org))
            {
                query = query.Where(s => s.dept.SiteId == org || s.MiName == org);
            }

            var result = new List<DepartmentDto>();
            foreach (var temp in query)
            {
                var dto = Mapper.Map<Department, DepartmentDto>(temp.dept);
                dto.MiName = temp.MiName;
                result.Add(dto);
            }
            return result;
        }

        /// <summary>
        /// 获取部门明细信息
        /// </summary>
        /// <param name="id">部门ID</param>
        /// <returns></returns>
        public DepartmentDto GetDepartmentDetail(string id)
        {
            var target = _dbContext.Set<Department>().Where(s => s.Id == id).FirstOrDefault();
            if (target == null)
            {
                return null;
            }
            var result = Mapper.Map<Department, DepartmentDto>(target);
            result.MiName = _dbContext.Set<MedicalInstitution>().Where(s => s.Id == result.SiteId).Select(s => s.MiName).FirstOrDefault();            
            return result;
        }
        #endregion

        #region 用户管理
        /// <summary>
        /// 用户信息维护
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public EmployeeDto AddOrUpdateEmployee(EmployeeDto source)
        {
            var isAddNew = string.IsNullOrEmpty(source.Id);
            if (isAddNew)
            {
                source.Id = Guid.NewGuid().ToString();
                source.Password = EncryptUtil.Md5Hash(source.Password);
                var entity = Mapper.Map<EmployeeDto, Employee>(source);
                _dbContext.Set<Employee>().Add(entity);

                // 可访问机构
                if (source.VisitMis != null && source.VisitMis.Count > 0)
                {
                    foreach (var temp in source.VisitMis)
                    {
                        var emi = new EmployeeMi();
                        emi.Id = Guid.NewGuid().ToString();
                        emi.EmployeeId = source.Id;
                        emi.MiId = temp.MiId;
                        _dbContext.Set<EmployeeMi>().Add(emi);
                    }
                }
            }
            else
            {
                var target = _dbContext.Set<Employee>().Where(s => s.Id == source.Id).FirstOrDefault();
                if (target == null)
                {
                    CommonFunc.ThrowExceptionIfRecordNotExists(EntityNames.User, source.Id, OperateType.Update, _logger);
                }
                else if (!Enumerable.SequenceEqual(source.Version, target.Version))
                {
                    var modifiedUser = GetEmployeeDetail(target.LastUpdateUserId);
                    CommonFunc.ThrowExceptionIfRecordHasBeenModified(EntityNames.User, source.Id, modifiedUser.EmName, target.LastUpdateTime, OperateType.Update, _logger);
                }

                //若密码变更，重新加密
                if (source.Password != target.Password)
                {
                    source.Password = EncryptUtil.Md5Hash(source.Password);
                }

                Mapper.Map(source, target);

                // 可访问机构
                var existsEmis = _dbContext.Set<EmployeeMi>().Where(s => s.EmployeeId == source.Id).ToList();
                _dbContext.Set<EmployeeMi>().RemoveRange(existsEmis);
                if (source.VisitMis != null && source.VisitMis.Count > 0)
                {
                    foreach (var temp in source.VisitMis)
                    {
                        var emi = new EmployeeMi();
                        emi.Id = Guid.NewGuid().ToString();
                        emi.EmployeeId = source.Id;
                        emi.MiId = temp.MiId;
                        _dbContext.Set<EmployeeMi>().Add(emi);
                    }
                }
            }
            _dbContext.SaveChanges();

            // 清除相关用户的可访问机构数据缓存
            DefaultCache.ClearUserMiCache(source.Id);
            return GetEmployeeDetail(source.Id);
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public bool DeleteEmployee(EmployeeDto source)
        {
            var target = _dbContext.Set<Employee>().Where(s => s.Id == source.Id).FirstOrDefault();
            if (target == null)
            {
                CommonFunc.ThrowExceptionIfRecordNotExists(EntityNames.User, source.Id, OperateType.Delete, _logger);
            }

            else if (!Enumerable.SequenceEqual(source.Version, target.Version))
            {
                var modifiedUser = GetEmployeeDetail(target.LastUpdateUserId);
                CommonFunc.ThrowExceptionIfRecordHasBeenModified(EntityNames.User, source.Id, modifiedUser.EmName, target.LastUpdateTime, OperateType.Delete, _logger);
            }
            _dbContext.Set<Employee>().Remove(target);
            var affected = _dbContext.SaveChanges();
            return affected == 1;
        }

        /// <summary>
        /// 搜索用户
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public List<EmployeeDto> SearchEmployees(string search)
        {
            var query = (from user in _dbContext.Set<Employee>()
                         join mi in _dbContext.Set<MedicalInstitution>() on user.SiteId equals mi.Id into mis
                         from mi in mis.DefaultIfEmpty()
                         join dept in _dbContext.Set<Department>() on user.DeptId equals dept.Id into depts
                         from dept in depts.DefaultIfEmpty()
                         select new { user, mi.MiName, dept.DeptName });
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(s => s.user.EmName.Contains(search));
            }

            var result = new List<EmployeeDto>();
            foreach (var temp in query)
            {
                var dto = Mapper.Map<Employee, EmployeeDto>(temp.user);
                dto.MiName = temp.MiName;
                dto.DeptName = temp.DeptName;
                result.Add(dto);
            }
            return result;

        }

        /// <summary>
        /// 获取用户详细信息
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <returns></returns>
        public EmployeeDto GetEmployeeDetail(string id)
        {
            var target = _dbContext.Set<Employee>().Where(s => s.Id == id).FirstOrDefault();
            if (target == null)
            {
                // TODO DEV-Purpose
                if (id == Consts.SuperUserId)
                {
                    return new EmployeeDto()
                    {
                        Id = Consts.SuperUserId,
                        EmName = "系统管理员"
                    };
                }
                return null;
            }
            var result = Mapper.Map<Employee, EmployeeDto>(target);
            result.DeptName = _dbContext.Set<Department>().Where(s => s.Id == result.DeptId).Select(s => s.DeptName).FirstOrDefault();
            result.MiName = _dbContext.Set<MedicalInstitution>().Where(s => s.Id == result.SiteId).Select(s => s.MiName).FirstOrDefault();

            // 可访问的机构列表
            result.VisitMis = new List<EmployeeMiDto>();
            var mis = (from mi in _dbContext.Set<MedicalInstitution>()
                       join emi in _dbContext.Set<EmployeeMi>() on mi.Id equals emi.MiId
                       where emi.EmployeeId == id
                       select new { mi.MiName, emi });
            foreach (var temp in mis)
            {
                result.VisitMis.Add(new EmployeeMiDto()
                {
                    Id = temp.emi.Id,
                    MiId = temp.emi.MiId,
                    MiName = temp.MiName
                });
            }
            return result;
        }
        #endregion

        #region 样本类型
        /// <summary>
        /// 样本类型维护
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public SampleTypeDto AddOrUpdateSampleType(SampleTypeDto source)
        {
            var isAddNew = string.IsNullOrEmpty(source.Id);
            if (isAddNew)
            {
                source.Id = Guid.NewGuid().ToString();
                var entity = Mapper.Map<SampleTypeDto, SampleType>(source);
                _dbContext.Set<SampleType>().Add(entity);
            }
            else
            {
                var target = _dbContext.Set<SampleType>().Where(s => s.Id == source.Id).FirstOrDefault();
                if (target == null)
                {
                    CommonFunc.ThrowExceptionIfRecordNotExists(EntityNames.SampleType, source.Id, OperateType.Update, _logger);
                }

                else if (!Enumerable.SequenceEqual(source.Version, target.Version))
                {
                    var modifiedUser = GetEmployeeDetail(target.LastUpdateUserId);
                    CommonFunc.ThrowExceptionIfRecordHasBeenModified(EntityNames.SampleType, source.Id, modifiedUser.EmName, target.LastUpdateTime, OperateType.Update, _logger);
                }
                Mapper.Map(source, target);
            }
            _dbContext.SaveChanges();
            return GetSampleTypeDetail(source.Id);
        }

        /// <summary>
        /// 删除样本类型
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public bool DeleteSampleType(SampleTypeDto source)
        {
            var target = _dbContext.Set<SampleType>().Where(s => s.Id == source.Id).FirstOrDefault();
            if (target == null)
            {
                CommonFunc.ThrowExceptionIfRecordNotExists(EntityNames.SampleType, source.Id, OperateType.Delete, _logger);
            }

            else if (!Enumerable.SequenceEqual(source.Version, target.Version))
            {
                var modifiedUser = GetEmployeeDetail(target.LastUpdateUserId);
                CommonFunc.ThrowExceptionIfRecordHasBeenModified(EntityNames.SampleType, source.Id, modifiedUser.EmName, target.LastUpdateTime, OperateType.Delete, _logger);
            }
            _dbContext.Set<SampleType>().Remove(target);
            var affected = _dbContext.SaveChanges();
            return affected == 1;
        }

        /// <summary>
        /// 搜索样本类型
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public List<SampleTypeDto> SearchSampleTypes(string search)
        {
            var query = (from mi in _dbContext.Set<SampleType>() select mi).AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(s => s.ChtName.Contains(search) || s.EngName.Contains(search) || s.Code.Contains(search));
            }
            return query.ToList().Select(Mapper.Map<SampleType, SampleTypeDto>).ToList();
        }

        /// <summary>
        /// 获取样本类型详细信息
        /// </summary>
        /// <param name="id">样本类型ID</param>
        /// <returns></returns>
        public SampleTypeDto GetSampleTypeDetail(string id)
        {
            var target = _dbContext.Set<SampleType>().Where(s => s.Id == id).FirstOrDefault();
            if (target == null)
            {
                return null;
            }
            var result = Mapper.Map<SampleType, SampleTypeDto>(target);
            return result;
        }
        #endregion

        #region 质控管理
        /// <summary>
        /// 质控维护
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public QcValueDto AddOrUpdateQcValue(QcValueDto source)
        {
            var isAddNew = string.IsNullOrEmpty(source.Id);
            if (isAddNew)
            {
                source.Id = Guid.NewGuid().ToString();
                source.QcTime = DateTime.Now;
                var entity = Mapper.Map<QcValueDto, QcValue>(source);
                _dbContext.Set<QcValue>().Add(entity);
            }
            else
            {
                var target = _dbContext.Set<QcValue>().Where(s => s.Id == source.Id).FirstOrDefault();
                if (target == null)
                {
                    CommonFunc.ThrowExceptionIfRecordNotExists(EntityNames.QcValue, source.Id, OperateType.Update, _logger);
                }

                else if (!Enumerable.SequenceEqual(source.Version, target.Version))
                {
                    var modifiedUser = GetEmployeeDetail(target.LastUpdateUserId);
                    CommonFunc.ThrowExceptionIfRecordHasBeenModified(EntityNames.QcValue, source.Id, modifiedUser.EmName, target.LastUpdateTime, OperateType.Update, _logger);
                }
                Mapper.Map(source, target);
            }
            _dbContext.SaveChanges();
            return GetQcValueDetail(source.Id);
        }

        /// <summary>
        /// 删除质控
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public bool DeleteQcValue(QcValueDto source)
        {
            var target = _dbContext.Set<QcValue>().Where(s => s.Id == source.Id).FirstOrDefault();
            if (target == null)
            {
                CommonFunc.ThrowExceptionIfRecordNotExists(EntityNames.QcValue, source.Id, OperateType.Delete, _logger);
            }

            else if (!Enumerable.SequenceEqual(source.Version, target.Version))
            {
                var modifiedUser = GetEmployeeDetail(target.LastUpdateUserId);
                CommonFunc.ThrowExceptionIfRecordHasBeenModified(EntityNames.QcValue, source.Id, modifiedUser.EmName, target.LastUpdateTime, OperateType.Delete, _logger);
            }
            _dbContext.Set<QcValue>().Remove(target);
            var affected = _dbContext.SaveChanges();
            return affected == 1;
        }

        /// <summary>
        /// 搜索质控
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public List<QcValueDto> SearchQcValues(string search)
        {
            var query = (from qc in _dbContext.Set<QcValue>()
                         join mi in _dbContext.Set<MedicalInstitution>() on qc.MiId equals mi.Id into mis
                         from mi in mis.DefaultIfEmpty()
                         join labItem in _dbContext.Set<LabItem>() on qc.LmId equals labItem.Id into labItems
                         from labItem in labItems.DefaultIfEmpty()
                         select new { qc, mi.MiName, labItem.ItemName }).AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(s => s.qc.InstrumentName.Contains(search) || s.ItemName.Contains(search));
            }
            var result = new List<QcValueDto>();
            foreach(var temp in query)
            {
                var dto = Mapper.Map<QcValue, QcValueDto>(temp.qc);
                dto.MiName = temp.MiName;
                dto.LabItemName = temp.ItemName;
                result.Add(dto);
            }
            return result;
        }

        /// <summary>
        /// 获取质控详情
        /// </summary>
        /// <param name="id">质控ID</param>
        /// <returns></returns>
        public QcValueDto GetQcValueDetail(string id)
        {
            var target = _dbContext.Set<QcValue>().Where(s => s.Id == id).FirstOrDefault();
            if (target == null)
            {
                return null;
            }
            var result = Mapper.Map<QcValue, QcValueDto>(target);
            result.MiName = _dbContext.Set<MedicalInstitution>().Where(s => s.Id == result.MiId).Select(s => s.MiName).FirstOrDefault();
            result.LabItemName = _dbContext.Set<LabItem>().Where(s => s.Id == result.LmId).Select(s => s.ItemName).FirstOrDefault();
            return result;
        }
        #endregion

        #region 危急值管理
        /// <summary>
        /// 危急值维护
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public CrisisDto AddOrUpdateCrisis(CrisisDto source)
        {
            var isAddNew = string.IsNullOrEmpty(source.Id);
            if (isAddNew)
            {
                source.Id = Guid.NewGuid().ToString();
                var entity = Mapper.Map<CrisisDto, Crisis>(source);
                _dbContext.Set<Crisis>().Add(entity);
            }
            else
            {
                var target = _dbContext.Set<Crisis>().Where(s => s.Id == source.Id).FirstOrDefault();
                if (target == null)
                {
                    CommonFunc.ThrowExceptionIfRecordNotExists(EntityNames.Crisis, source.Id, OperateType.Update, _logger);
                }

                else if (!Enumerable.SequenceEqual(source.Version, target.Version))
                {
                    var modifiedUser = GetEmployeeDetail(target.LastUpdateUserId);
                    CommonFunc.ThrowExceptionIfRecordHasBeenModified(EntityNames.Crisis, source.Id, modifiedUser.EmName, target.LastUpdateTime, OperateType.Update, _logger);
                }
                Mapper.Map(source, target);
            }
            _dbContext.SaveChanges();
            return GetCrisisDetail(source.Id);
        }

        /// <summary>
        /// 删除危急值
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public bool DeleteCrisis(CrisisDto source)
        {
            var target = _dbContext.Set<Crisis>().Where(s => s.Id == source.Id).FirstOrDefault();
            if (target == null)
            {
                CommonFunc.ThrowExceptionIfRecordNotExists(EntityNames.Crisis, source.Id, OperateType.Delete, _logger);
            }

            else if (!Enumerable.SequenceEqual(source.Version, target.Version))
            {
                var modifiedUser = GetEmployeeDetail(target.LastUpdateUserId);
                CommonFunc.ThrowExceptionIfRecordHasBeenModified(EntityNames.Crisis, source.Id, modifiedUser.EmName, target.LastUpdateTime, OperateType.Delete, _logger);
            }
            _dbContext.Set<Crisis>().Remove(target);
            var affected = _dbContext.SaveChanges();
            return affected == 1;
        }

        /// <summary>
        /// 搜索危急值
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public List<CrisisDto> SearchCrisiss(string search)
        {
            var query = (from crisi in _dbContext.Set<Crisis>()
                         join labItem in _dbContext.Set<LabItem>() on crisi.LmId equals labItem.Id
                         select new { crisi, labItem }).AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(s => s.labItem.ItemName.Contains(search) || s.labItem.ItemCode.Contains(search));
            }
            var result = new List<CrisisDto>();
            foreach(var temp in query)
            {
                var dto = Mapper.Map<Crisis, CrisisDto>(temp.crisi);
                dto.LabItem = Mapper.Map<LabItem, LabItemDto>(temp.labItem);
                result.Add(dto);
            }
            return result;
        }

        /// <summary>
        /// 获取危急值详情
        /// </summary>
        /// <param name="id">危急值ID</param>
        /// <returns></returns>
        public CrisisDto GetCrisisDetail(string id)
        {
            var target = _dbContext.Set<Crisis>().Where(s => s.Id == id).FirstOrDefault();
            if (target == null)
            {
                return null;
            }
            var result = Mapper.Map<Crisis, CrisisDto>(target);
            var labItem = _dbContext.Set<LabItem>().Where(s => s.Id == result.LmId).FirstOrDefault();
            result.LabItem = Mapper.Map<LabItem, LabItemDto>(labItem);

            return result;
        }
        #endregion

        #region 检验项目
        /// <summary>
        /// 检验项目维护
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public LabItemDto AddOrUpdateLabItem(LabItemDto source)
        {
            var isAddNew = string.IsNullOrEmpty(source.Id);
            if (isAddNew)
            {
                source.Id = Guid.NewGuid().ToString();
                var entity = Mapper.Map<LabItemDto, LabItem>(source);
                _dbContext.Set<LabItem>().Add(entity);
            }
            else
            {
                var target = _dbContext.Set<LabItem>().Where(s => s.Id == source.Id).FirstOrDefault();
                if (target == null)
                {
                    CommonFunc.ThrowExceptionIfRecordNotExists(EntityNames.LabItem, source.Id, OperateType.Update, _logger);
                }

                else if (!Enumerable.SequenceEqual(source.Version, target.Version))
                {
                    var modifiedUser = GetEmployeeDetail(target.LastUpdateUserId);
                    CommonFunc.ThrowExceptionIfRecordHasBeenModified(EntityNames.LabItem, source.Id, modifiedUser.EmName, target.LastUpdateTime, OperateType.Update, _logger);
                }
                Mapper.Map(source, target);
            }
            _dbContext.SaveChanges();
            return GetLabItemDetail(source.Id);
        }

        /// <summary>
        /// 删除检验项目
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public bool DeleteLabItem(LabItemDto source)
        {
            var target = _dbContext.Set<LabItem>().Where(s => s.Id == source.Id).FirstOrDefault();
            if (target == null)
            {
                CommonFunc.ThrowExceptionIfRecordNotExists(EntityNames.LabItem, source.Id, OperateType.Delete, _logger);
            }

            else if (!Enumerable.SequenceEqual(source.Version, target.Version))
            {
                var modifiedUser = GetEmployeeDetail(target.LastUpdateUserId);
                CommonFunc.ThrowExceptionIfRecordHasBeenModified(EntityNames.LabItem, source.Id, modifiedUser.EmName, target.LastUpdateTime, OperateType.Delete, _logger);
            }
            _dbContext.Set<LabItem>().Remove(target);
            var affected = _dbContext.SaveChanges();
            return affected == 1;
        }

        /// <summary>
        /// 搜索检验项目
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public List<LabItemDto> SearchLabItems(string search)
        {
            var query = (from lm in _dbContext.Set<LabItem>()
                         join labCategory in _dbContext.Set<LabCategory>() on lm.LcId equals labCategory.Id
                         select new { lm, labCategory.LcName });
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(s => s.lm.ItemCode.Contains(search) || s.lm.ItemName.Contains(search));
            }

            var list = query.OrderByDescending(s => s.lm.LastUpdateTime).ToList();
            var result = new List<LabItemDto>();
            foreach (var temp in list)
            {
                var dto = Mapper.Map<LabItem, LabItemDto>(temp.lm);
                dto.CategoryName = temp.LcName;
                result.Add(dto);
            }
            return result;
        }

        /// <summary>
        /// 获取检验项目详情
        /// </summary>
        /// <param name="id">检验项目ID</param>
        /// <returns></returns>
        public LabItemDto GetLabItemDetail(string id)
        {
            var target = _dbContext.Set<LabItem>().Where(s => s.Id == id).FirstOrDefault();
            if (target == null)
            {
                return null;
            }
            var result = Mapper.Map<LabItem, LabItemDto>(target);
            if (!string.IsNullOrEmpty(result.Category))
            {
                result.CategoryName = _dbContext.Set<LabCategory>().Where(s => s.Id == result.Category).Select(s => s.LcName).FirstOrDefault();
            }            
            return result;
        }
        #endregion

        #region 检验项目组合
        /// <summary>
        /// 检验项目组合维护
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public LabItemSetDto AddOrUpdateLabItemSet(LabItemSetDto source)
        {
            var isAddNew = string.IsNullOrEmpty(source.Id);
            if (isAddNew)
            {
                source.Id = Guid.NewGuid().ToString();
                var entity = Mapper.Map<LabItemSetDto, LabItemSet>(source);
                _dbContext.Set<LabItemSet>().Add(entity);

                if(source.LabItems != null && source.LabItems.Count > 0)
                {
                    foreach(var detail in source.LabItems)
                    {
                        var detailEntity = new LabItemSetDetail();
                        detailEntity.Id = Guid.NewGuid().ToString();
                        detailEntity.SetId = source.Id;
                        detailEntity.LmId = detail.Id;
                        _dbContext.Set<LabItemSetDetail>().Add(detailEntity);
                    }
                }
            }
            else
            {
                var target = _dbContext.Set<LabItemSet>().Where(s => s.Id == source.Id).FirstOrDefault();
                if (target == null)
                {
                    CommonFunc.ThrowExceptionIfRecordNotExists(EntityNames.LabItemSet, source.Id, OperateType.Update, _logger);
                }

                else if (!Enumerable.SequenceEqual(source.Version, target.Version))
                {
                    var modifiedUser = GetEmployeeDetail(target.LastUpdateUserId);
                    CommonFunc.ThrowExceptionIfRecordHasBeenModified(EntityNames.LabItemSet, source.Id, modifiedUser.EmName, target.LastUpdateTime, OperateType.Update, _logger);
                }
                Mapper.Map(source, target);

                var exists = _dbContext.Set<LabItemSetDetail>().Where(s => s.SetId == source.Id).ToList();
                _dbContext.Set<LabItemSetDetail>().RemoveRange(exists);

                foreach (var detail in source.LabItems)
                {
                    var detailEntity = new LabItemSetDetail();
                    detailEntity.Id = Guid.NewGuid().ToString();
                    detailEntity.SetId = source.Id;
                    detailEntity.LmId = detail.Id;
                    _dbContext.Set<LabItemSetDetail>().Add(detailEntity);
                }
            }
            _dbContext.SaveChanges();
            return GetLabItemSetDetail(source.Id);
        }

        /// <summary>
        /// 删除检验项目组合
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public bool DeleteLabItemSet(LabItemSetDto source)
        {
            var target = _dbContext.Set<LabItemSet>().Where(s => s.Id == source.Id).FirstOrDefault();
            if (target == null)
            {
                CommonFunc.ThrowExceptionIfRecordNotExists(EntityNames.LabItemSet, source.Id, OperateType.Delete, _logger);
            }

            else if (!Enumerable.SequenceEqual(source.Version, target.Version))
            {
                var modifiedUser = GetEmployeeDetail(target.LastUpdateUserId);
                CommonFunc.ThrowExceptionIfRecordHasBeenModified(EntityNames.LabItemSet, source.Id, modifiedUser.EmName, target.LastUpdateTime, OperateType.Delete, _logger);
            }
            _dbContext.Set<LabItemSet>().Remove(target);

            var details = _dbContext.Set<LabItemSetDetail>().Where(s => s.SetId == source.Id).ToList();
            _dbContext.Set<LabItemSetDetail>().RemoveRange(details);

            var affected = _dbContext.SaveChanges();
            return affected > 1;
        }

        /// <summary>
        /// 搜索检验项目组合
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public List<LabItemSetDto> SearchLabItemSets(string search)
        {
            var query = (from mi in _dbContext.Set<LabItemSet>() select mi).AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(s => s.LisName.Contains(search) || s.LisCode.Contains(search));
            }
            var result = query.ToList().Select(Mapper.Map<LabItemSet, LabItemSetDto>).ToList();
            foreach(var temp in result)
            {
                var items = (from detail in _dbContext.Set<LabItemSetDetail>()
                                 join lm in _dbContext.Set<LabItem>() on detail.LmId equals lm.Id
                                 where detail.SetId == temp.Id
                                 select lm).ToList();
                temp.LabItems = items.Select(Mapper.Map<LabItem, LabItemDto>).ToList();
                temp.LabItemString = string.Join(",", temp.LabItems.Select(s => s.ItemName));
            }
            return result;
        }

        /// <summary>
        /// 获取检验项目组合详情
        /// </summary>
        /// <param name="id">检验项目组合ID</param>
        /// <returns></returns>
        public LabItemSetDto GetLabItemSetDetail(string id)
        {
            var target = _dbContext.Set<LabItemSet>().Where(s => s.Id == id).FirstOrDefault();
            if (target == null)
            {
                return null;
            }
            var result = Mapper.Map<LabItemSet, LabItemSetDto>(target);
            var items = (from detail in _dbContext.Set<LabItemSetDetail>()
                         join lm in _dbContext.Set<LabItem>() on detail.LmId equals lm.Id
                         where detail.SetId == id
                         select lm).ToList();
            result.LabItems = items.Select(Mapper.Map<LabItem, LabItemDto>).ToList();
            result.LabItemString = string.Join(",", result.LabItems.Select(s => s.ItemName));
            return result;
        }
        #endregion

        #region 检验项目分类
        /// <summary>
        /// 检验项目分类维护
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public LabCategoryDto AddOrUpdateLabCategory(LabCategoryDto source)
        {
            var isAddNew = string.IsNullOrEmpty(source.Id);
            if (isAddNew)
            {
                source.Id = Guid.NewGuid().ToString();
                var entity = Mapper.Map<LabCategoryDto, LabCategory>(source);
                _dbContext.Set<LabCategory>().Add(entity);
            }
            else
            {
                var target = _dbContext.Set<LabCategory>().Where(s => s.Id == source.Id).FirstOrDefault();
                if (target == null)
                {
                    CommonFunc.ThrowExceptionIfRecordNotExists(EntityNames.LabCategory, source.Id, OperateType.Update, _logger);
                }

                else if (!Enumerable.SequenceEqual(source.Version, target.Version))
                {
                    var modifiedUser = GetEmployeeDetail(target.LastUpdateUserId);
                    CommonFunc.ThrowExceptionIfRecordHasBeenModified(EntityNames.LabCategory, source.Id, modifiedUser.EmName, target.LastUpdateTime, OperateType.Update, _logger);
                }
                Mapper.Map(source, target);
            }
            _dbContext.SaveChanges();
            return GetLabCategoryDetail(source.Id);
        }

        /// <summary>
        /// 删除检验项目分类
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public bool DeleteLabCategory(LabCategoryDto source)
        {
            var target = _dbContext.Set<LabCategory>().Where(s => s.Id == source.Id).FirstOrDefault();
            if (target == null)
            {
                CommonFunc.ThrowExceptionIfRecordNotExists(EntityNames.LabCategory, source.Id, OperateType.Delete, _logger);
            }

            else if (!Enumerable.SequenceEqual(source.Version, target.Version))
            {
                var modifiedUser = GetEmployeeDetail(target.LastUpdateUserId);
                CommonFunc.ThrowExceptionIfRecordHasBeenModified(EntityNames.LabCategory, source.Id, modifiedUser.EmName, target.LastUpdateTime, OperateType.Delete, _logger);
            }
            _dbContext.Set<LabCategory>().Remove(target);
            var affected = _dbContext.SaveChanges();
            return affected == 1;
        }

        /// <summary>
        /// 搜索检验项目分类
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public List<LabCategoryDto> SearchLabCategorys(string search)
        {
            var query = (from mi in _dbContext.Set<LabCategory>() select mi).AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(s => s.LcName.Contains(search));
            }
            return query.ToList().Select(Mapper.Map<LabCategory, LabCategoryDto>).ToList();
        }

        /// <summary>
        /// 获取检验项目分类目详情
        /// </summary>
        /// <param name="id">检验项目分类ID</param>
        /// <returns></returns>
        public LabCategoryDto GetLabCategoryDetail(string id)
        {
            var target = _dbContext.Set<LabCategory>().Where(s => s.Id == id).FirstOrDefault();
            if (target == null)
            {
                return null;
            }
            var result = Mapper.Map<LabCategory, LabCategoryDto>(target);
            return result;
        }
        #endregion

        #region 检验结果
        /// <summary>
        /// 检验结果数据维护
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public bool AddOrUpdateLabResult(List<LabInfoDto> source)
        {
            var labItemIds = source.Select(s => s.LabItemId).Distinct().ToList();
            var srcResults = _dbContext.Set<LabResult>().Where(s => labItemIds.Contains(s.LmId)).ToList();
            _dbContext.Set<LabResult>().RemoveRange(srcResults);
            foreach (var info in source)
            {
                var labResult = Mapper.Map<LabResultDto, LabResult>(info.LabResult);
                labResult.Id = Guid.NewGuid().ToString();
                _dbContext.Set<LabResult>().Add(labResult);
            }
            var affected = _dbContext.SaveChanges();
            return affected > 0;
        }
        #endregion
    }
}
