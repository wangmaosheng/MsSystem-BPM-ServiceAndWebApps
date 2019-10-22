using JadeFramework.Core.Domain.Entities;
using JadeFramework.Core.Extensions;
using MsSystem.Sys.IRepository;
using MsSystem.Sys.IService;
using MsSystem.Sys.Model;
using MsSystem.Sys.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MsSystem.Sys.Service
{
    public class SysDeptService: ISysDeptService
    {
        private readonly ISysDatabaseFixture _databaseFixture;
        private readonly ILogJobs _logJobs;
        public SysDeptService(ISysDatabaseFixture databaseFixture, ILogJobs logJobs)
        {
            _databaseFixture = databaseFixture;
            _logJobs = logJobs;
        }


        public async Task<DeptIndexViewModel> GetTreeAsync()
        {
            DeptIndexViewModel model = new DeptIndexViewModel();
            var list = await _databaseFixture.Db.SysDept.FindAllAsync();
            var dblist = list.ToList();
            List<DeptTreeViewModel> trees = new List<DeptTreeViewModel>();
            foreach (var item in dblist.Where(m => m.ParentId == 0))
            {
                DeptTreeViewModel tree = new DeptTreeViewModel
                {
                    DeptId = item.DeptId,
                    DeptName = item.DeptName,
                    DeptCode = item.DeptCode,
                    ParentId = item.ParentId,
                    IsDel = item.IsDel,
                    Memo = item.Memo
                };

                tree.Children = dblist.Where(m => m.ParentId == tree.DeptId)
                    .Select(m => new DeptTreeViewModel()
                    {
                        DeptId = m.DeptId,
                        DeptName = m.DeptName,
                        DeptCode = m.DeptCode,
                        ParentId = m.ParentId,
                        IsDel = m.IsDel,
                        Memo = m.Memo
                    }).ToList();

                var tempchild = tree.Children;
                GetChildren(ref tempchild, ref dblist);
                tree.Children = tempchild;
                trees.Add(tree);
            }
            model.DeptTree = trees;
            return model;
        }

        /// <summary>
        /// 获取子节点集合
        /// </summary>
        /// <param name="child"></param>
        /// <param name="list"></param>
        private void GetChildren(ref List<DeptTreeViewModel> child, ref List<SysDept> list)
        {
            foreach (var item in child)
            {
                item.Children = list.Where(m => m.ParentId == item.DeptId )
                    .Select(m => new DeptTreeViewModel
                    {
                        DeptId = m.DeptId,
                        DeptName = m.DeptName,
                        DeptCode = m.DeptCode,
                        ParentId = m.ParentId,
                        IsDel = m.IsDel,
                        Memo = m.Memo
                    }).ToList();
                var tempchild = item.Children;
                GetChildren(ref tempchild, ref list);
                item.Children = tempchild;
            }
        }

        /// <summary>
        /// 获取部门信息
        /// </summary>
        /// <param name="deptid"></param>
        /// <returns></returns>
        public async Task<DeptShowViewModel> GetDeptAsync(long deptid)
        {
            DeptShowViewModel model;
            if (deptid > 0)
            {
                model = new DeptShowViewModel
                {
                    SysDept = await _databaseFixture.Db.SysDept.FindAsync(m => m.DeptId == deptid)
                };
            }
            else
            {
                model = new DeptShowViewModel()
                {
                    SysDept = new SysDept()
                };
            }
            var dblist = await _databaseFixture.Db.SysDept.FindAllAsync(m => m.IsDel == 0 && m.DeptId != deptid);
            model.ParentMenus = dblist.ToList()
                .Select(m => new ZTree()
                {
                    id = m.DeptId.ToString(),
                    name = m.DeptName,
                    pId = m.ParentId
                }).ToList();
            return model;
        }

        public async Task<bool> AddAsync(DeptShowDto dto)
        {
            using (var tran = _databaseFixture.Db.BeginTransaction())
            {
                try
                {
                    dto.SysDept.CreateTime = DateTime.Now.ToTimeStamp();
                    await _databaseFixture.Db.SysDept.InsertAsync(dto.SysDept, tran);

                    //path
                    string path = "";
                    if (dto.SysDept.ParentId > 0)
                    {
                        var parentres = await _databaseFixture.Db.SysDept.FindAsync(m => m.DeptId == dto.SysDept.ParentId);
                        if (parentres != null)
                        {
                            path = parentres.Path;
                        }
                    }
                    dto.SysDept.Path = path.IsNullOrEmpty() ? dto.SysDept.DeptId.ToString() : path + ":" + dto.SysDept.DeptId;
                    await _databaseFixture.Db.SysDept.UpdateAsync(dto.SysDept, tran);

                    tran.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    _logJobs.ExceptionLog(dto.SysDept.CreateUserId, ex);
                    return false;
                }
            }
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(DeptShowDto dto)
        {
            using (var tran = _databaseFixture.Db.BeginTransaction())
            {
                try
                {
                    var dbdept = await _databaseFixture.Db.SysDept.FindAsync(m => m.DeptId == dto.SysDept.DeptId);
                    if (dbdept == null)
                    {
                        return false;
                    }

                    #region 基本信息

                    dbdept.DeptName = dto.SysDept.DeptName;
                    dbdept.DeptCode = dto.SysDept.DeptCode;
                    dbdept.ParentId = dto.SysDept.ParentId;
                    dbdept.IsDel = dto.SysDept.IsDel;
                    dbdept.Memo = dto.SysDept.Memo;

                    //path
                    string path = "";
                    if (dto.SysDept.ParentId > 0)
                    {
                        var parentres = await _databaseFixture.Db.SysDept.FindAsync(m => m.DeptId == dto.SysDept.ParentId);
                        if (parentres != null)
                        {
                            path = parentres.Path;
                        }
                    }
                    dbdept.Path = path.IsNullOrEmpty() ? dto.SysDept.DeptId.ToString() : path + ":" + dto.SysDept.DeptId;

                    await _databaseFixture.Db.SysDept.UpdateAsync(dbdept, tran);

                    #endregion

                    tran.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    _logJobs.ExceptionLog(dto.SysDept.CreateUserId, ex);
                    return false;
                }
            }
        }

        public async Task<bool> DeleteAsync(long[] ids, long userid)
        {
            using (var tran = _databaseFixture.Db.BeginTransaction())
            {
                try
                {
                    List<long> list = ids.ToList();
                    var dblist = await _databaseFixture.Db.SysDept.FindAllAsync(m => m.IsDel == 0);
                    var dbres = dblist.ToList();
                    if (dbres.Any())
                    {
                        foreach (var item in dbres)
                        {
                            await _databaseFixture.Db.SysDept.DeleteAsync(item, tran);
                        }
                    }
                    tran.Commit();
                    return true;
                }
                catch (Exception e)
                {
                    tran.Rollback();
                    _logJobs.ExceptionLog(userid, e);
                    return false;
                }
            }
        }
    }
}
