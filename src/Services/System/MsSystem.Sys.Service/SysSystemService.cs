using JadeFramework.Core.Domain.Entities;
using JadeFramework.Core.Extensions;
using MsSystem.Sys.IRepository;
using MsSystem.Sys.IService;
using MsSystem.Sys.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MsSystem.Sys.Service
{
    public class SysSystemService : ISysSystemService
    {
        private ISysDatabaseFixture _databaseFixture;
        private ILogJobs _logJobs;
        public SysSystemService(ISysDatabaseFixture databaseFixture, ILogJobs logJobs)
        {
            _databaseFixture = databaseFixture;
            _logJobs = logJobs;
        }

        /// <summary>
        /// 根据ID获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<SysSystem> GetByIdAsync(long id)
        {
            return await _databaseFixture.Db.SysSystem.FindByIdAsync(id);
        }
        /// <summary>
        /// 新增系统
        /// </summary>
        /// <param name="system"></param>
        /// <returns></returns>
        public async Task<bool> InsertAsync(SysSystem system)
        {
            try
            {
                system.SystemCode = Guid.NewGuid().ToString();
                system.CreateTime = DateTime.Now.ToTimeStamp();
                var res = await _databaseFixture.Db.SysSystem.InsertAsync(system);
                return res;
            }
            catch (Exception e)
            {
                _logJobs.ExceptionLog(system.CreateUserId, e);
                return false;
            }
        }

        /// <summary>
        /// 更新系统
        /// </summary>
        /// <param name="system"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(SysSystem system)
        {
            try
            {
                var dbsystem = await _databaseFixture.Db.SysSystem.FindByIdAsync(system.SystemId);
                if (dbsystem != null)
                {
                    dbsystem.SystemName = system.SystemName;
                    dbsystem.IsDel = system.IsDel;
                    dbsystem.Memo = system.Memo;
                    dbsystem.Sort = system.Sort;
                    dbsystem.UpdateTime = DateTime.Now.ToTimeStamp();
                    return await _databaseFixture.Db.SysSystem.UpdateAsync(dbsystem);
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                _logJobs.ExceptionLog(system.CreateUserId, e);
                return false;
            }
        }

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(List<long> ids, long userid)
        {
            using (var tran = _databaseFixture.Db.BeginTransaction())
            {
                try
                {
                    var dbuserrole = await _databaseFixture.Db.SysSystem.FindAllAsync(m => m.IsDel == 0 && ids.Contains(m.SystemId));
                    foreach (var item in dbuserrole)
                    {
                        item.IsDel = 1;
                        await _databaseFixture.Db.SysSystem.UpdateAsync(item);
                    }
                    tran.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    _logJobs.ExceptionLog(userid, ex);
                    return false;
                }
            }
        }

        /// <summary>
        /// 系统列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<SysSystem>> ListAsync()
        {
            var list = await _databaseFixture.Db.SysSystem.FindAllAsync(m => m.SystemId > 0);
            return list.ToList();
        }

        /// <summary>
        /// 系统分页列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<Page<SysSystem>> GetPageAsync(int pageIndex, int pageSize)
        {
            return await _databaseFixture.Db.SysSystem.GetPageAsync(pageIndex, pageSize);
        }

        /// <summary>
        /// 禁用系统
        /// </summary>
        /// <returns></returns>
        public async Task<bool> DisableSystemAsync(long id)
        {
            try
            {
                var system = await _databaseFixture.Db.SysSystem.FindAsync(m => m.IsDel == 0 && m.SystemId == id);
                if (system != null)
                {
                    system.IsDel = 1;
                    return await _databaseFixture.Db.SysSystem.UpdateAsync(system);
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                _logJobs.ExceptionLog(0, e);
                return false;
            }
        }

        /// <summary>
        /// 启用系统
        /// </summary>
        /// <param name="id">系统ID</param>
        /// <returns></returns>
        public async Task<bool> EnableSystemAsync(long id)
        {
            try
            {
                var system = await _databaseFixture.Db.SysSystem.FindAsync(m => m.IsDel == 1 && m.SystemId == id);
                if (system != null)
                {
                    system.IsDel = 0;
                    return await _databaseFixture.Db.SysSystem.UpdateAsync(system);
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                _logJobs.ExceptionLog(0, e);
                return false;
            }
        }
    }
}
