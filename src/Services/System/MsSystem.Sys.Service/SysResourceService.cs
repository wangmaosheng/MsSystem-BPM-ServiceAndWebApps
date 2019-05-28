using JadeFramework.Core;
using JadeFramework.Core.Domain.Entities;
using JadeFramework.Core.Domain.Enum;
using JadeFramework.Core.Domain.Permission;
using JadeFramework.Core.Extensions;
using JadeFramework.Core.ViewModel;
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
    public class SysResourceService : ISysResourceService
    {
        private ISysDatabaseFixture _databaseFixture;
        private ILogJobs _logJobs;
        public SysResourceService(ISysDatabaseFixture databaseFixture, ILogJobs logJobs)
        {
            _databaseFixture = databaseFixture;
            _logJobs = logJobs;
        }

        #region 资源列表

        /// <summary>
        /// 获取树形菜单
        /// </summary>
        /// <param name="systemId">系统ID</param>
        /// <returns></returns>
        public async Task<ResourceIndexViewModel> GetTreeAsync(long systemId)
        {
            ResourceIndexViewModel model = new ResourceIndexViewModel();
            var dbsys = await _databaseFixture.Db.SysSystem.FindAllAsync(m => m.IsDel == 0);
            //拉取系统
            model.SystemItems = dbsys.Select(m => new SelectListItem()
            {
                Value = m.SystemId.ToString(),
                Text = m.SystemName,
                Selected = m.SystemId == systemId
            }).ToList();
            var dbres = await _databaseFixture.Db.SysResource.FindAllAsync(m => m.SystemId == systemId);
            var dblist = dbres.OrderBy(m => m.Sort).ToList();
            List<ResourceTreeViewModel> trees = new List<ResourceTreeViewModel>();
            foreach (var item in dblist.Where(m => m.ParentId == 0))
            {
                ResourceTreeViewModel tree = new ResourceTreeViewModel
                {
                    ResourceId = item.ResourceId,
                    ResourceName = item.ResourceName,
                    ParentId = item.ParentId,
                    ResourceUrl = item.ResourceUrl,
                    Sort = item.Sort,
                    Icon = item.Icon,
                    IsDel = item.IsDel,
                    Memo = item.Memo
                };

                tree.Children = dblist.Where(m => m.ParentId == tree.ResourceId && m.IsButton == 0)
                    .Select(m => new ResourceTreeViewModel()
                    {
                        ResourceId = m.ResourceId,
                        ResourceName = m.ResourceName,
                        ParentId = m.ParentId,
                        ResourceUrl = m.ResourceUrl,
                        Sort = m.Sort,
                        Icon = m.Icon,
                        IsDel = m.IsDel,
                        Memo = m.Memo
                    }).ToList();
                tree.Buttons = dblist.Where(m => m.ParentId == tree.ResourceId && m.IsButton == 1)
                    .Select(m => new IdAndValue()
                    {
                        Id = m.ResourceId,
                        Value = m.ResourceName
                    }).ToList();

                var tempchild = tree.Children;
                GetChildren(ref tempchild, ref dblist);
                tree.Children = tempchild;
                trees.Add(tree);
            }
            model.ResourceTree = trees;
            return model;
        }

        /// <summary>
        /// 获取子节点集合
        /// </summary>
        /// <param name="child"></param>
        /// <param name="list"></param>
        private void GetChildren(ref List<ResourceTreeViewModel> child, ref List<SysResource> list)
        {
            foreach (var item in child)
            {
                item.Children = list.Where(m => m.ParentId == item.ResourceId && m.IsButton == 0)
                    .Select(m => new ResourceTreeViewModel()
                    {
                        ResourceId = m.ResourceId,
                        ResourceName = m.ResourceName,
                        ParentId = m.ParentId,
                        ResourceUrl = m.ResourceUrl,
                        Sort = m.Sort,
                        Icon = m.Icon,
                        IsDel = m.IsDel,
                        Memo = m.Memo
                    }).OrderBy(m => m.Sort).ToList();
                item.Buttons = list.Where(m => m.ParentId == item.ResourceId && m.IsButton == 1)
                    .Select(m => new IdAndValue()
                    {
                        Id = m.ResourceId,
                        Value = m.ResourceName
                    }).ToList();

                var tempchild = item.Children;
                GetChildren(ref tempchild, ref list);
                item.Children = tempchild;
            }
        }

        #endregion

        #region 资源查看

        /// <summary>
        /// 获取资源信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="systemid"></param>
        /// <returns></returns>
        public async Task<ResourceShowViewModel> GetResourceAsync(long id, long systemid)
        {
            ResourceShowViewModel model = new ResourceShowViewModel();
            //获取基本数据
            model.SysResource = await _databaseFixture.Db.SysResource.FindByIdAsync(id);
            if (model.SysResource == null)
            {
                model.SysResource = new SysResource()
                {
                    IsDel = 0,
                    IsShow = 1,
                    Sort = 1
                };
            }
            model.ButtonViewModels = new List<ButtonViewModel>();
            if (model.SysResource != null)
            {
                var dbbuttons = await _databaseFixture.Db.SysResource
                    .FindAllAsync(m => m.ParentId == model.SysResource.ResourceId && m.IsDel == 0 && m.IsButton == 1);
                //获取所属按钮
                var buttons = dbbuttons
                    .Select(m => new
                    {
                        m.ResourceId,
                        m.ResourceName,
                        m.ButtonType
                    }).ToList();
                foreach (var item in buttons)
                {
                    model.ButtonViewModels.Add(new ButtonViewModel()
                    {
                        Id = item.ResourceId,
                        ButtonType = item.ButtonType.Value,
                        ButtonModel = item.ButtonType.Value,
                        Name = item.ResourceName
                    });
                }
                var btntypes = model.ButtonViewModels.Select(m => m.ButtonType).ToList();
                //添加未选择的按钮
                var btnEnums = Enum<ButtonType>.AsEnumerable().Select(m => (byte)m).ToList();
                var pushEnums = btnEnums.Where(m => !btntypes.Contains(m) && m > 0).ToList();
                foreach (var item in pushEnums)
                {
                    model.ButtonViewModels.Add(new ButtonViewModel()
                    {
                        Id = 0,
                        ButtonModel = 0,
                        ButtonType = item,
                        Name = GetButtonName(item)
                    });
                }

                //父节点选择菜单下拉
                var dbpmenus = await _databaseFixture.Db.SysResource.FindAllAsync(m => m.ResourceId != model.SysResource.ResourceId && m.IsDel == 0 && m.IsShow == 1 && m.SystemId == systemid);
                model.ParentMenus = dbpmenus
                .Select(m => new ZTree()
                {
                    id = m.ResourceId.ToString(),
                    name = m.ResourceName,
                    pId = m.ParentId,
                    @checked = model.SysResource.ParentId == m.ResourceId
                }).ToList();
            }
            else
            {
                model.SysResource = new SysResource();
                model.ButtonViewModels = ButtonInit();
                var dbpmenus = await _databaseFixture.Db.SysResource.FindAllAsync(m => m.IsDel == 0 && m.IsShow == 1 && m.SystemId == systemid);
                model.ParentMenus = dbpmenus
                .Select(m => new ZTree()
                {
                    id = m.ResourceId.ToString(),
                    name = m.ResourceName,
                    pId = m.ParentId
                }).ToList();
            }
            return model;
        }

        /// <summary>
        /// 互动区按钮类型
        /// </summary>
        /// <param name="btntype"></param>
        /// <returns></returns>
        private string GetButtonName(byte btntype)
        {
            string btnName = "未知";
            switch (btntype)
            {
                case 1:
                    btnName = "查看";
                    break;
                case 2:
                    btnName = "新增";
                    break;
                case 3:
                    btnName = "编辑";
                    break;
                case 4:
                    btnName = "删除";
                    break;
                case 5:
                    btnName = "打印";
                    break;
                case 6:
                    btnName = "审核";
                    break;
                case 7:
                    btnName = "作废";
                    break;
                case 8:
                    btnName = "结束";
                    break;
                case 9:
                    btnName = "扩展";
                    break;
            }
            return btnName;
        }

        /// <summary>
        /// 按钮初始化
        /// </summary>
        /// <returns></returns>
        private List<ButtonViewModel> ButtonInit()
        {
            List<ButtonViewModel> buttons = new List<ButtonViewModel>
            {
                new ButtonViewModel()
                {
                    Id = 1,
                    ButtonType = (byte) ButtonType.View,
                    ButtonModel = (byte) ButtonType.View,
                    Name = "查看"
                },
                new ButtonViewModel()
                {
                    Id = 1,
                    ButtonType = (byte) ButtonType.Add,
                    ButtonModel = (byte) ButtonType.Add,
                    Name = "新增"
                },
                new ButtonViewModel()
                {
                    Id = 1,
                    ButtonType = (byte) ButtonType.Edit,
                    ButtonModel = (byte) ButtonType.Edit,
                    Name = "编辑"
                },
                new ButtonViewModel()
                {
                    Id = 1,
                    ButtonType = (byte) ButtonType.Delete,
                    ButtonModel = (byte) ButtonType.Delete,
                    Name = "删除"
                },
                new ButtonViewModel()
                {
                    Id = 0,
                    ButtonType = (byte) ButtonType.Print,
                    ButtonModel = 0,
                    Name = "打印"
                },
                new ButtonViewModel()
                {
                    Id = 0,
                    ButtonType = (byte) ButtonType.Check,
                    ButtonModel = 0,
                    Name = "审核"
                },
                new ButtonViewModel()
                {
                    Id = 0,
                    ButtonType = (byte) ButtonType.Cancle,
                    ButtonModel = 0,
                    Name = "作废"
                },
                new ButtonViewModel()
                {
                    Id = 0,
                    ButtonType = (byte) ButtonType.Finish,
                    ButtonModel = 0,
                    Name = "结束"
                },
                new ButtonViewModel()
                {
                    Id = 0,
                    ButtonType = (byte) ButtonType.Extend,
                    ButtonModel = 0,
                    Name = "扩展"
                }
            };
            return buttons;
        }

        #endregion

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<bool> AddAsync(ResourceShowDto dto)
        {
            using (var tran = _databaseFixture.Db.BeginTransaction())
            {
                try
                {
                    #region 基本信息添加
                    dto.SysResource.CreateTime = DateTime.Now.ToTimeStamp();
                    dto.SysResource.IsButton = 0;
                    await _databaseFixture.Db.SysResource.InsertAsync(dto.SysResource, tran);

                    //path
                    string path = "";
                    if (dto.SysResource.ParentId > 0)
                    {
                        var parentres = await _databaseFixture.Db.SysResource.FindAsync(m => m.ResourceId == dto.SysResource.ParentId);
                        if (parentres != null)
                        {
                            path = parentres.Path;
                        }
                    }
                    dto.SysResource.Path = path.IsNullOrEmpty() ? dto.SysResource.ResourceId.ToString() : path + ":" + dto.SysResource.ResourceId;
                    await _databaseFixture.Db.SysResource.UpdateAsync(dto.SysResource, tran);
                    #endregion

                    #region 按钮添加

                    List<SysResource> list = new List<SysResource>();
                    var addbutton = dto.ButtonDto.Where(m => m.ButtonType == m.ButtonModel).ToList();
                    foreach (var button in addbutton)
                    {
                        SysResource res = new SysResource()
                        {
                            IsButton = 1,
                            CreateTime = DateTime.Now.ToTimeStamp(),
                            SystemId = dto.SysResource.SystemId,
                            ResourceName = button.Name,
                            ButtonType = button.ButtonType,
                            ParentId = dto.SysResource.ResourceId,
                            ButtonClass = ((ButtonType)button.ButtonType).ToClass()
                        };
                        list.Add(res);
                    }
                    if (list.HasItems())
                    {
                        await _databaseFixture.Db.SysResource.BulkInsertAsync(list, tran);
                    }

                    #endregion

                    tran.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    _logJobs.ExceptionLog(dto.SysResource.CreateUserId, ex);
                    return false;
                }
            }
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(ResourceShowDto dto)
        {
            using (var tran = _databaseFixture.Db.BeginTransaction())
            {
                try
                {
                    var dbresource = await _databaseFixture.Db.SysResource.FindAsync(m => m.ResourceId == dto.SysResource.ResourceId);
                    if (dbresource == null)
                    {
                        return false;
                    }
                    #region 基本信息

                    dbresource.ResourceName = dto.SysResource.ResourceName;
                    dbresource.ResourceUrl = dto.SysResource.ResourceUrl;
                    dbresource.Sort = dto.SysResource.Sort;
                    dbresource.Icon = dto.SysResource.Icon;
                    dbresource.IsShow = dto.SysResource.IsShow;
                    dbresource.ParentId = dto.SysResource.ParentId;
                    dbresource.IsDel = dto.SysResource.IsDel;
                    dbresource.Memo = dto.SysResource.Memo;


                    //path
                    string path = "";
                    if (dto.SysResource.ParentId > 0)
                    {
                        var parentres = await _databaseFixture.Db.SysResource.FindAsync(m => m.ResourceId == dto.SysResource.ParentId);
                        if (parentres != null)
                        {
                            path = parentres.Path;
                        }
                    }
                    dbresource.Path = path.IsNullOrEmpty() ? dto.SysResource.ResourceId.ToString() : path + ":" + dto.SysResource.ResourceId;

                    await _databaseFixture.Db.SysResource.UpdateAsync(dbresource, tran);

                    #endregion

                    #region 按钮修改

                    var dbres = await _databaseFixture.Db.SysResource.FindAllAsync(m => m.ParentId == dbresource.ResourceId && m.IsButton == 1);
                    var dbbuttons = dbres.ToList();

                    var pageids = dto.ButtonDto.Where(m => m.ButtonModel == 0 && m.Id > 0).Select(m => m.Id).ToList();//ButtonModel是0 表示未选中

                    #region 删除

                    List<SysResource> dellist = dbbuttons.Where(m => pageids.Contains(m.ResourceId)).ToList();
                    if (dellist.Any())
                    {
                        foreach (var item in dellist)
                        {
                            await _databaseFixture.Db.SysResource.DeleteAsync(item, tran);
                        }
                    }

                    #endregion

                    #region 新增

                    var addbtns = dto.ButtonDto.Where(m => m.Id <= 0 && m.ButtonModel > 0).ToList();//获取选中
                    List<SysResource> list = addbtns.Select(item => new SysResource()
                    {
                        IsButton = 1,
                        CreateTime = DateTime.Now.ToTimeStamp(),
                        SystemId = dto.SysResource.SystemId,
                        ResourceName = item.Name,
                        ButtonType = item.ButtonModel,
                        ParentId = dto.SysResource.ResourceId,
                        ButtonClass = ((ButtonType)item.ButtonType).ToClass()
                    }).ToList();
                    if (list.HasItems())
                    {
                        await _databaseFixture.Db.SysResource.BulkInsertAsync(list, tran);
                    }
                    #endregion

                    #endregion

                    tran.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    _logJobs.ExceptionLog(dto.SysResource.CreateUserId, ex);
                    return false;
                }
            }
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="ids">菜单ID数组</param>
        /// <param name="userid">用户ID</param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(long[] ids, long userid)
        {
            using (var tran = _databaseFixture.Db.BeginTransaction())
            {
                try
                {
                    List<long> list = ids.ToList();
                    var dbres = await _databaseFixture.Db.SysResource.GetAllListAsync(list, 0);
                    if (dbres.HasItems())
                    {
                        foreach (var item in dbres)
                        {
                            //获取child
                            var childs = await _databaseFixture.Db.SysResource.GetChildButtonsAsync(item.ResourceId);
                            foreach (var child in childs)
                            {
                                await _databaseFixture.Db.SysResource.DeleteAsync(child, tran);
                            }
                            await _databaseFixture.Db.SysResource.DeleteAsync(item, tran);
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

        /// <summary>
        /// 根据用户获取左侧菜单列表
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <returns></returns>
        public async Task<List<ResourceTreeViewModel>> GetLeftTreeAsync(long userid)
        {
            var list = await _databaseFixture.Db.SysResource.GetListByUserIdAsync(userid);
            var dblist = list.DistinctBy(m => m.ResourceId).OrderBy(m => m.Sort).ToList();
            List<ResourceTreeViewModel> trees = new List<ResourceTreeViewModel>();
            foreach (var item in dblist.Where(m => m.ParentId == 0))
            {
                ResourceTreeViewModel tree = new ResourceTreeViewModel
                {
                    ResourceId = item.ResourceId,
                    ResourceName = item.ResourceName,
                    ParentId = item.ParentId,
                    ResourceUrl = item.ResourceUrl,
                    Sort = item.Sort,
                    Icon = item.Icon,
                    IsDel = item.IsDel,
                    Memo = item.Memo
                };

                tree.Children = dblist.Where(m => m.ParentId == tree.ResourceId)
                    .Select(m => new ResourceTreeViewModel()
                    {
                        ResourceId = m.ResourceId,
                        ResourceName = m.ResourceName,
                        ParentId = m.ParentId,
                        ResourceUrl = m.ResourceUrl,
                        Sort = m.Sort,
                        Icon = m.Icon,
                        IsDel = m.IsDel,
                        Memo = m.Memo
                    }).OrderBy(m => m.Sort).ToList();

                var tempchild = tree.Children;
                GetChildren(ref tempchild, ref dblist);
                tree.Children = tempchild;
                trees.Add(tree);
            }
            return trees;
        }

        /// <summary>
        /// 异步获取用户操作权限
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <returns></returns>
        public async Task<UserPermission> GetUserPermissionAsync(long userid)
        {
            UserPermission permission = new UserPermission();
            //获取角色
            var rolelist = await _databaseFixture.Db.SysRole.GetByUserIdAsync(userid);
            permission.Roles = rolelist
                .Select(m => new Role()
                {
                    RoleId = m.RoleId,
                    RoleName = m.RoleName,
                    SystemId = m.SystemId
                }).ToList();
            //角色ID集合
            var trueroleids = permission.Roles.Select(m => m.RoleId);
            //资源ID集合
            var reslist = await _databaseFixture.Db.SysRoleResource.GetListByRoleIdAsync(trueroleids);
            var resourceids = reslist.Select(m => m.ResourceId);
            //获取菜单
            var rlist = await _databaseFixture.Db.SysResource.GetAllListAsync(resourceids);
            var resourcelist = rlist.Select(m => new
            {
                m.ResourceId,
                m.ResourceName,
                m.ResourceUrl,
                m.SystemId,
                m.ParentId,
                m.IsButton,
                m.ButtonType,
                m.ButtonClass,
                m.Icon
            }).ToList();
            //操作菜单
            permission.Menus = resourcelist.Where(m => m.IsButton == 0).Select(m => new Menu()
            {
                MenuId = m.ResourceId,
                MenuName = m.ResourceName,
                MenuUrl = m.ResourceUrl,
                SystemId = m.SystemId,
                ParentId = m.ParentId
            }).ToList();
            //操作按钮
            foreach (var item in permission.Menus)
            {
                item.MenuButton = resourcelist.Where(m => m.IsButton == 1 && m.ParentId == item.MenuId)
                    .Select(m => new MenuButton()
                    {
                        Id = m.ResourceId,
                        ButtonName = m.ResourceName,
                        ButtonType = m.ButtonType.Value,
                        ButtonClass = m.ButtonClass,
                        Icon = m.Icon
                    }).ToList();
            }
            return permission;
        }

        /// <summary>
        /// 获取树形数据
        /// </summary>
        /// <param name="roleid">角色ID</param>
        /// <returns></returns>
        public async Task<List<ZTree>> GetBoxTreeAsync(long roleid)
        {
            var dbrole = await _databaseFixture.Db.SysRole.FindAsync(m => m.RoleId == roleid && m.IsDel == 0);
            if (dbrole == null)
            {
                return null;
            }
            //系统下全部资源
            var reslist = await _databaseFixture.Db.SysResource.FindAllAsync(m => m.SystemId == dbrole.SystemId && m.IsDel == 0);
            var dbres = reslist.OrderBy(m => m.Sort).ToList();
            //资源角色关系(含操作按钮)
            var resrolelist = await _databaseFixture.Db.SysRoleResource.FindAllAsync(m => m.RoleId == roleid);
            var dbresrole = resrolelist.ToList();
            List<ZTree> trees = new List<ZTree>();
            foreach (var item in dbres)
            {
                ZTree tree = new ZTree()
                {
                    id = item.ResourceId.ToString(),
                    name = item.ResourceName,
                    open = true,
                    pId = item.ParentId
                };
                var firstres = dbresrole.FirstOrDefault(m => m.ResourceId == item.ResourceId && m.RoleId == roleid);
                tree.@checked = firstres != null;
                trees.Add(tree);
            }
            return trees;
        }

        /// <summary>
        /// 角色分配资源保存
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<bool> BoxSaveAsync(RoleTreeDto dto)
        {
            using (var tran = _databaseFixture.Db.BeginTransaction())
            {
                try
                {
                    //该角色下全部资源关系
                    var rolerelist = await _databaseFixture.Db.SysRoleResource.FindAllAsync(m => m.RoleId == dto.RoleId);
                    var dbroleres = rolerelist.ToList();
                    //全增全删操作
                    if (dbroleres.HasItems())
                    {
                        foreach (var item in dbroleres)
                        {
                            await _databaseFixture.Db.SysRoleResource.DeleteAsync(item, tran);
                        }
                    }
                    //表示非操作按钮
                    var restree = dto.ResourceIds;
                    if (restree.HasItems())
                    {
                        foreach (var item in restree)
                        {
                            SysRoleResource roleres = new SysRoleResource()
                            {
                                CreateTime = DateTime.Now.ToTimeStamp(),
                                ResourceId = item.ToInt32(),
                                RoleId = dto.RoleId
                            };
                            await _databaseFixture.Db.SysRoleResource.InsertAsync(roleres, tran);
                        }
                    }
                    tran.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    _logJobs.ExceptionLog(dto.CreateUserId, ex);
                    return false;
                }
            }
        }
    }
}
