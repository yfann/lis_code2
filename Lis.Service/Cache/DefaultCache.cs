using Lis.Domain.Entities;
using Lis.EntityFramework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lis.Service.Cache
{
    public class DefaultCache
    {
        private static Dictionary<string, List<string>> UserMi = new Dictionary<string, List<string>>();
        private static object lockObj1 = new object();

        /// <summary>
        /// 查询用户可以访问的机构ID列表
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public static List<string> GetUserMi(string userId, ILisContext context)
        {
            if (!UserMi.ContainsKey(userId))
            {
                lock (lockObj1)
                {
                    var list = context.Set<EmployeeMi>().Where(s => s.EmployeeId == userId).Select(s => s.MiId).ToList();
                    if (!UserMi.ContainsKey(userId))
                    {
                        UserMi.Add(userId, list);
                    }
                }
            }
            return UserMi[userId];
        }
        public static bool ClearUserMiCache(string userId)
        {
            var cached = UserMi.ContainsKey(userId);
            if (cached)
            {
                lock (lockObj1)
                {
                    return UserMi.Remove(userId);
                }
            }
            return false;
        }

        private static Dictionary<string, List<string>> UserPatient = new Dictionary<string, List<string>>();
        private static object lockObj2 = new object();

        /// <summary>
        /// 查询用户可以访问的患者ID
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public static List<string> GetUserPatient(string userId, ILisContext context)
        {
            if (!UserPatient.ContainsKey(userId))
            {
                lock (lockObj2)
                {
                    var mids = GetUserMi(userId, context);
                    var list = context.Set<Requests>().Where(s => mids.Contains(s.MiId)).Select(s => s.PatientId).Distinct().ToList();
                    if (!UserPatient.ContainsKey(userId))
                    {
                        UserPatient.Add(userId, list);
                    }
                }
            }
            return UserPatient[userId];
        }
        public static bool ClearUserPatientCache(string userId)
        {
            var cached = UserPatient.ContainsKey(userId);
            if (cached)
            {
                lock (lockObj2)
                {
                    return UserPatient.Remove(userId);
                }
            }
            return false;
        }
    }
}
