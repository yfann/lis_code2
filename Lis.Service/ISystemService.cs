using Lis.Service.Dtos;
using Lis.Domain.Entities;
using Lis.EntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lis.Service
{
    public interface ISystemService : IDisposable
    {
        #region 医疗机构
        /// <summary>
        /// 医疗机构信息维护
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        MedicalInstitutionDto AddOrUpdateMedicalInstitution(MedicalInstitutionDto source);

        /// <summary>
        /// 删除医疗机构
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        bool DeleteMedicalInstitution(MedicalInstitutionDto source);

        /// <summary>
        /// 医疗机构信息查询
        /// </summary>
        /// <param name="search">搜索关键字</param>
        /// <param name="parent">上级机构ID或者上级机构名称</param>
        /// <returns></returns>
        List<MedicalInstitutionDto> GetAllMedicalInstitutions(string search, string parent);

        /// <summary>
        /// 获取医疗机构明细信息
        /// </summary>
        /// <param name="id">医疗机构ID</param>
        /// <returns></returns>
        MedicalInstitutionDto GetMedicalInstitutionDetail(string id);
        #endregion

        #region 部门
        /// <summary>
        /// 部门信息维护
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        DepartmentDto AddOrUpdateDepartment(DepartmentDto source);

        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        bool DeleteDepartment(DepartmentDto source);

        /// <summary>
        /// 部门信息查询
        /// </summary>
        /// <param name="search">搜索关键字</param>
        /// <param name="org">所属医疗机构ID或者名称</param>
        /// <returns></returns>
        List<DepartmentDto> GetAllDepartments(string search, string org);

        /// <summary>
        /// 获取部门明细信息
        /// </summary>
        /// <param name="id">部门ID</param>
        /// <returns></returns>
        DepartmentDto GetDepartmentDetail(string id);
        #endregion

        #region 用户管理
        /// <summary>
        /// 用户信息维护
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        EmployeeDto AddOrUpdateEmployee(EmployeeDto source);

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        bool DeleteEmployee(EmployeeDto source);

        /// <summary>
        /// 搜索用户
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        List<EmployeeDto> SearchEmployees(string search);

        /// <summary>
        /// 获取用户详细信息
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <returns></returns>
        EmployeeDto GetEmployeeDetail(string id);
        #endregion

        #region 样本类型
        /// <summary>
        /// 样本类型维护
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        SampleTypeDto AddOrUpdateSampleType(SampleTypeDto source);

        /// <summary>
        /// 删除样本类型
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        bool DeleteSampleType(SampleTypeDto source);

        /// <summary>
        /// 搜索样本类型
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        List<SampleTypeDto> SearchSampleTypes(string search);

        /// <summary>
        /// 获取样本类型详细信息
        /// </summary>
        /// <param name="id">样本类型ID</param>
        /// <returns></returns>
        SampleTypeDto GetSampleTypeDetail(string id);
        #endregion

        #region 质控管理
        /// <summary>
        /// 质控维护
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        QcValueDto AddOrUpdateQcValue(QcValueDto source);

        /// <summary>
        /// 删除质控
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        bool DeleteQcValue(QcValueDto source);

        /// <summary>
        /// 搜索质控
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        List<QcValueDto> SearchQcValues(string search);

        /// <summary>
        /// 获取质控详情
        /// </summary>
        /// <param name="id">质控ID</param>
        /// <returns></returns>
        QcValueDto GetQcValueDetail(string id);
        #endregion

        #region 危急值管理
        /// <summary>
        /// 危急值维护
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        CrisisDto AddOrUpdateCrisis(CrisisDto source);

        /// <summary>
        /// 删除危急值
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        bool DeleteCrisis(CrisisDto source);

        /// <summary>
        /// 搜索危急值
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        List<CrisisDto> SearchCrisiss(string search);

        /// <summary>
        /// 获取危急值详情
        /// </summary>
        /// <param name="id">危急值ID</param>
        /// <returns></returns>
        CrisisDto GetCrisisDetail(string id);
        #endregion

        #region 检验项目
        /// <summary>
        /// 检验项目维护
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        LabItemDto AddOrUpdateLabItem(LabItemDto source);

        /// <summary>
        /// 删除检验项目
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        bool DeleteLabItem(LabItemDto source);

        /// <summary>
        /// 搜索检验项目
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        List<LabItemDto> SearchLabItems(string search);

        /// <summary>
        /// 获取检验项目详情
        /// </summary>
        /// <param name="id">检验项目ID</param>
        /// <returns></returns>
        LabItemDto GetLabItemDetail(string id);
        #endregion

        #region 检验项目组合
        /// <summary>
        /// 检验项目组合维护
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        LabItemSetDto AddOrUpdateLabItemSet(LabItemSetDto source);

        /// <summary>
        /// 删除检验项目组合
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        bool DeleteLabItemSet(LabItemSetDto source);

        /// <summary>
        /// 搜索检验项目组合
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        List<LabItemSetDto> SearchLabItemSets(string search);

        /// <summary>
        /// 获取检验项目组合详情
        /// </summary>
        /// <param name="id">检验项目组合ID</param>
        /// <returns></returns>
        LabItemSetDto GetLabItemSetDetail(string id);
        #endregion

        #region 检验项目分类
        /// <summary>
        /// 检验项目分类维护
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        LabCategoryDto AddOrUpdateLabCategory(LabCategoryDto source);

        /// <summary>
        /// 删除检验项目分类
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        bool DeleteLabCategory(LabCategoryDto source);

        /// <summary>
        /// 搜索检验项目分类
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        List<LabCategoryDto> SearchLabCategorys(string search);

        /// <summary>
        /// 获取检验项目分类目详情
        /// </summary>
        /// <param name="id">检验项目分类ID</param>
        /// <returns></returns>
        LabCategoryDto GetLabCategoryDetail(string id);
        #endregion

        #region 检验结果
        /// <summary>
        /// 检验结果数据维护
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        bool AddOrUpdateLabResult(List<LabInfoDto> source);
        #endregion
    }
}
