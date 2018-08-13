using Lis.Common.Logger;
using System;

namespace Lis.Common.Utils
{
    public static class CommonFunc
    {
        private const string DATE_FORMAT = "yyyy-MM-dd H:mm:ss";
        /// <summary>
        /// Throw an exception if records not exists
        /// </summary>
        /// <param name="entityName"></param>
        /// <param name="entityId"></param>
        /// <param name="operateType"></param>
        /// <param name="_logger"></param>
        public static void ThrowExceptionIfRecordNotExists(string entityName, object entityId, OperateType operateType, ISystemLogger _logger)
        {
            string errorMsg = string.Format("{0}{1}失败。可能的原因：1. 记录不存在 2.已被其他用户删除 3.不在操作权限范围内", operateType == OperateType.Update ? "更新" : "删除", entityName);
            var e = new Exception(errorMsg);
            _logger.Log(LogLevel.Error, errorMsg + string.Format("(记录ID: {0}).", entityId.ToString()), e);
            throw e;
        }

        /// <summary>
        /// Throw an exception if record is dirty (e.g. has been modified by another user)
        /// </summary>
        /// <param name="entityName"></param>
        /// <param name="entityId"></param>
        /// <param name="modifiedUserName"></param>
        /// <param name="modifiedDate"></param>
        /// <param name="operateType"></param>
        /// <param name="_logger"></param>
        public static void ThrowExceptionIfRecordHasBeenModified(string entityName, object entityId, string modifiedUserName, DateTime modifiedDate, OperateType operateType, ISystemLogger _logger)
        {
            string errorMsg = string.Format("{0}{1}失败：该记录已被{2}于{3}修改，请刷新页面重试！", operateType == OperateType.Update ? "更新" : "删除", entityName, modifiedUserName, modifiedDate.ToString(DATE_FORMAT));
            var e = new Exception(errorMsg);
            _logger.Log(LogLevel.Error, errorMsg + string.Format("(记录ID: {0})。", entityId.ToString()), e);
            throw e;
        }
    }

    public enum OperateType
    {
        Update = 0,
        Delete
    }


    /// <summary>
    /// 实体对象名称
    /// </summary>
    public static class EntityNames
    {
        /// <summary>
        /// 申请单
        /// </summary>
        public const string Request = "申请单";

        /// <summary>
        /// 医疗机构
        /// </summary>
        public const string MedicalInstitution = "医疗机构";

        /// <summary>
        /// 检验项目
        /// </summary>
        public const string LabItem = "检验项目";

        /// <summary>
        /// 检验项目组合
        /// </summary>
        public const string LabItemSet = "检验项目组合";

        /// <summary>
        /// 部门
        /// </summary>
        public const string Dept = "部门";

        /// <summary>
        /// 用户
        /// </summary>
        public const string User = "用户";

        /// <summary>
        /// 危急值
        /// </summary>
        public const string Crisis = "危急值";

        /// <summary>
        /// 检查项目
        /// </summary>
        public const string ExamItem = "检查项目";

        /// <summary>
        /// 检验项目分类
        /// </summary>
        public const string LabCategory = "检验项目分类";

        /// <summary>
        /// 样本类型
        /// </summary>
        public const string SampleType = "样本类型";

        /// <summary>
        /// 质控值
        /// </summary>
        public const string QcValue = "质控值";
    }
}
