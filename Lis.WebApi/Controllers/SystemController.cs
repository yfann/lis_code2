using Lis.Common.Logger;
using Lis.Service;
using Lis.Service.Dtos;
using Lis.Service.Models;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Http;

namespace Lis.WebApi.Controllers
{
    public class SystemController : ApiControllerBase
    {
        private readonly ISystemService _systemService;
        private readonly IUserService _userService;
        private readonly ISystemLogger _logger;
        public SystemController(ISystemLogger logger, ISystemService systemService, IUserService userService)
        {
            _logger = logger;
            _systemService = systemService;
            _userService = userService;
        }

        /// <summary>
        /// 获取服务器时间
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ActionName("time")]
        public DateTime GetSystemTime()
        {
            return DateTime.Now;
        }

        #region 登录
        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="data">登录参数</param>
        /// <returns>登录结果</returns>
        [HttpPost]
        [ActionName("login")]
        public LoginValidateModel Login(LoginModel data)
        {
            var ret = _userService.Login(data.Code, data.Password);
            return ret;
        }

        /// <summary>
        /// Token验证
        /// </summary>
        /// <param name="token"></param>
        /// <returns>true:验证通过，false:验证失败</returns>
        [HttpGet]
        [ActionName("authenticate")]
        public bool ValidateToken(string token)
        {
            var tokenCookie = HttpContext.Current.Request.Cookies["token"];
            if (tokenCookie != null)
            {
                if (string.IsNullOrEmpty(tokenCookie.Value))
                {
                    return false;
                }
                var ret = _userService.ValidateToken(tokenCookie.Value);
                return ret;
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
        [HttpGet]
        [ActionName("changepwd")]
        public bool ChangePassword(string userId, string srcPwd, string newPwd)
        {
            var ret = _userService.ChangePassword(userId, srcPwd, newPwd);
            return ret;
        }
        #endregion

        #region 医疗机构
        /// <summary>
        /// 医疗机构信息维护
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("medicalinstitutions")]
        public MedicalInstitutionDto AddOrUpdateMedicalInstitution([FromBody]MedicalInstitutionDto source)
        {
            var result = _systemService.AddOrUpdateMedicalInstitution(source);
            return result;
        }

        /// <summary>
        /// 删除医疗机构
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        [HttpDelete]
        [ActionName("medicalinstitutions")]
        public bool DeleteMedicalInstitution([FromBody]MedicalInstitutionDto source)
        {
            var result = _systemService.DeleteMedicalInstitution(source);
            return result;
        }

        /// <summary>
        /// 医疗机构信息查询
        /// </summary>
        /// <param name="search">搜索关键字</param>
        /// <param name="parent">上级机构ID或者上级机构名称</param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("medicalinstitutions")]
        public List<MedicalInstitutionDto> GetAllMedicalInstitutions(string search, string parent = null)
        {
            var result = _systemService.GetAllMedicalInstitutions(search, parent);
            return result;
        }

        /// <summary>
        /// 获取医疗机构明细信息
        /// </summary>
        /// <param name="id">医疗机构ID</param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("medicalinstitutiondetail")]
        public MedicalInstitutionDto GetMedicalInstitutionDetail(string id)
        {
            var result = _systemService.GetMedicalInstitutionDetail(id);
            return result;
        }
        #endregion

        #region 部门
        /// <summary>
        /// 部门信息维护
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("depts")]
        public DepartmentDto AddOrUpdateDepartment(DepartmentDto source)
        {
            var result = _systemService.AddOrUpdateDepartment(source);
            return result;
        }

        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        [HttpDelete]
        [ActionName("depts")]
        public bool DeleteDepartment(DepartmentDto source)
        {
            var result = _systemService.DeleteDepartment(source);
            return result;
        }

        /// <summary>
        /// 部门信息查询
        /// </summary>
        /// <param name="search">搜索关键字</param>
        /// <param name="org">所属医疗机构ID或者名称</param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("depts")]
        public List<DepartmentDto> GetAllDepartments(string search, string org = null)
        {
            var result = _systemService.GetAllDepartments(search, org);
            return result;
        }

        /// <summary>
        /// 获取部门明细信息
        /// </summary>
        /// <param name="id">部门ID</param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("deptdetail")]
        public DepartmentDto GetDepartmentDetail(string id)
        {
            var result = _systemService.GetDepartmentDetail(id);
            return result;
        }
        #endregion

        #region 用户管理
        /// <summary>
        /// 用户信息维护
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("users")]
        public EmployeeDto AddOrUpdateEmployee(EmployeeDto source)
        {
            var result = _systemService.AddOrUpdateEmployee(source);
            return result;
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        [HttpDelete]
        [ActionName("users")]
        public bool DeleteEmployee(EmployeeDto source)
        {
            var result = _systemService.DeleteEmployee(source);
            return result;
        }

        /// <summary>
        /// 搜索用户
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("users")]
        public List<EmployeeDto> SearchEmployees(string search)
        {
            var result = _systemService.SearchEmployees(search);
            return result;
        }

        /// <summary>
        /// 获取用户详细信息
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("userdetail")]
        public EmployeeDto GetEmployeeDetail(string id)
        {
            var result = _systemService.GetEmployeeDetail(id);
            return result;
        }
        #endregion

        #region 样本类型
        /// <summary>
        /// 样本类型维护
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("sampletypes")]
        public SampleTypeDto AddOrUpdateSampleType(SampleTypeDto source)
        {
            var result = _systemService.AddOrUpdateSampleType(source);
            return result;
        }

        /// <summary>
        /// 删除样本类型
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        [HttpDelete]
        [ActionName("sampletypes")]
        public bool DeleteSampleType(SampleTypeDto source)
        {
            var result = _systemService.DeleteSampleType(source);
            return result;
        }

        /// <summary>
        /// 搜索样本类型
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("sampletypes")]
        public List<SampleTypeDto> SearchSampleTypes(string search)
        {
            var result = _systemService.SearchSampleTypes(search);
            return result;
        }

        /// <summary>
        /// 获取样本类型详细信息
        /// </summary>
        /// <param name="id">样本类型ID</param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("sampletypedetail")]
        public SampleTypeDto GetSampleTypeDetail(string id)
        {
            var result = _systemService.GetSampleTypeDetail(id);
            return result;
        }
        #endregion

        #region 质控管理
        /// <summary>
        /// 质控维护
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("qcvalues")]
        public QcValueDto AddOrUpdateQcValue(QcValueDto source)
        {
            var result = _systemService.AddOrUpdateQcValue(source);
            return result;
        }

        /// <summary>
        /// 删除质控
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        [HttpDelete]
        [ActionName("qcvalues")]
        public bool DeleteQcValue(QcValueDto source)
        {
            var result = _systemService.DeleteQcValue(source);
            return result;
        }

        /// <summary>
        /// 搜索质控
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("qcvalues")]
        public List<QcValueDto> SearchQcValues(string search)
        {
            var result = _systemService.SearchQcValues(search);
            return result;
        }

        /// <summary>
        /// 获取质控详情
        /// </summary>
        /// <param name="id">质控ID</param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("qcvaluedetail")]
        public QcValueDto GetQcValueDetail(string id)
        {
            var result = _systemService.GetQcValueDetail(id);
            return result;
        }
        #endregion

        #region 危急值管理
        /// <summary>
        /// 危急值维护
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("crisis")]
        public CrisisDto AddOrUpdateCrisis(CrisisDto source)
        {
            var result = _systemService.AddOrUpdateCrisis(source);
            return result;
        }

        /// <summary>
        /// 删除危急值
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        [HttpDelete]
        [ActionName("crisis")]
        public bool DeleteCrisis(CrisisDto source)
        {
            var result = _systemService.DeleteCrisis(source);
            return result;
        }

        /// <summary>
        /// 搜索危急值
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("crisis")]
        public List<CrisisDto> SearchCrisiss(string search)
        {
            var result = _systemService.SearchCrisiss(search);
            return result;
        }

        /// <summary>
        /// 获取危急值详情
        /// </summary>
        /// <param name="id">危急值ID</param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("crisisdetail")]
        public CrisisDto GetCrisisDetail(string id)
        {
            var result = _systemService.GetCrisisDetail(id);
            return result;
        }
        #endregion

        #region 检验项目
        /// <summary>
        /// 检验项目维护
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("labitems")]
        public LabItemDto AddOrUpdateLabItem(LabItemDto source)
        {
            var result = _systemService.AddOrUpdateLabItem(source);
            return result;
        }

        /// <summary>
        /// 删除检验项目
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        [HttpDelete]
        [ActionName("labitems")]
        public bool DeleteLabItem(LabItemDto source)
        {
            var result = _systemService.DeleteLabItem(source);
            return result;
        }

        /// <summary>
        /// 搜索检验项目
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("labitems")]
        public List<LabItemDto> SearchLabItems(string search)
        {
            var result = _systemService.SearchLabItems(search);
            return result;
        }

        /// <summary>
        /// 获取检验项目详情
        /// </summary>
        /// <param name="id">检验项目ID</param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("labitemdetail")]
        public LabItemDto GetLabItemDetail(string id)
        {
            var result = _systemService.GetLabItemDetail(id);
            return result;
        }
        #endregion

        #region 检验项目组合
        /// <summary>
        /// 检验项目组合维护
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("labitemsets")]
        public LabItemSetDto AddOrUpdateLabItemSet(LabItemSetDto source)
        {
            var result = _systemService.AddOrUpdateLabItemSet(source);
            return result;
        }

        /// <summary>
        /// 删除检验项目组合
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        [HttpDelete]
        [ActionName("labitemsets")]
        public bool DeleteLabItemSet(LabItemSetDto source)
        {
            var result = _systemService.DeleteLabItemSet(source);
            return result;
        }

        /// <summary>
        /// 搜索检验项目组合
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("labitemsets")]
        public List<LabItemSetDto> SearchLabItemSets(string search)
        {
            var result = _systemService.SearchLabItemSets(search);
            return result;
        }

        /// <summary>
        /// 获取检验项目组合详情
        /// </summary>
        /// <param name="id">检验项目组合ID</param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("labitemsetdetail")]
        public LabItemSetDto GetLabItemSetDetail(string id)
        {
            var result = _systemService.GetLabItemSetDetail(id);
            return result;
        }
        #endregion

        #region 检验项目分类
        /// <summary>
        /// 检验项目分类维护
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("labcategories")]
        public LabCategoryDto AddOrUpdateLabCategory(LabCategoryDto source)
        {
            var result = _systemService.AddOrUpdateLabCategory(source);
            return result;
        }

        /// <summary>
        /// 删除检验项目分类
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        [HttpDelete]
        [ActionName("labcategories")]
        public bool DeleteLabCategory(LabCategoryDto source)
        {
            var result = _systemService.DeleteLabCategory(source);
            return result;
        }

        /// <summary>
        /// 搜索检验项目分类
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("labcategories")]
        public List<LabCategoryDto> SearchLabCategorys(string search)
        {
            var result = _systemService.SearchLabCategorys(search);
            return result;
        }

        /// <summary>
        /// 获取检验项目分类目详情
        /// </summary>
        /// <param name="id">检验项目分类ID</param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("labcategorydetail")]
        public LabCategoryDto GetLabCategoryDetail(string id)
        {
            var result = _systemService.GetLabCategoryDetail(id);
            return result;
        }
        #endregion

        #region 检验结果
        /// <summary>
        /// 检验结果数据维护
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("labresults")]
        public bool AddOrUpdateLabResult(List<LabInfoDto> source)
        {
            var result = _systemService.AddOrUpdateLabResult(source);
            return result;
        }
        #endregion

    }
}