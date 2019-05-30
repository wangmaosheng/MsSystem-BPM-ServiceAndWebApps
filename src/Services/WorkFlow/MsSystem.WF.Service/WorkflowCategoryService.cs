using JadeFramework.Core.Domain.Entities;
using JadeFramework.Core.Extensions;
using MsSystem.WF.IRepository;
using MsSystem.WF.IService;
using MsSystem.WF.Model;
using MsSystem.WF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MsSystem.WF.Service
{
    public class WorkflowCategoryService : IWorkflowCategoryService
    {
        private readonly IWFDatabaseFixture databaseFixture;

        public WorkflowCategoryService(IWFDatabaseFixture databaseFixture)
        {
            this.databaseFixture = databaseFixture;
        }

        /// <summary>
        /// 获取树状列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<CategoryTreeListDto>> GetTreeListAsync()
        {
            List<CategoryTreeListDto> trees = new List<CategoryTreeListDto>();
            var mydblist = await databaseFixture.Db.WorkflowCategory.FindAllAsync();
            var dblist = mydblist.OrderByDescending(m => m.Status).ThenBy(m => m.CreateTime).AsEnumerable();
            foreach (var item in dblist.Where(m => m.ParentId == default(Guid)))
            {
                CategoryTreeListDto tree = new CategoryTreeListDto
                {
                    Id = item.Id,
                    ParentId = item.ParentId,
                    Memo = item.Memo,
                    Name = item.Name,
                    Status = item.Status
                };
                tree.Children = dblist.Where(m => m.ParentId == tree.Id).Select(m => new CategoryTreeListDto
                {
                    Id = m.Id,
                    Name = m.Name,
                    ParentId = m.ParentId,
                    Memo = m.Memo,
                    Status = m.Status
                }).ToList();
                var tempchilds = tree.Children;
                GetTreeListChildren(ref tempchilds, ref dblist);
                tree.Children = tempchilds;
                trees.Add(tree);
            }
            return trees;
        }

        /// <summary>
        /// 无限制获取子节点
        /// </summary>
        /// <param name="child"></param>
        /// <param name="list"></param>
        private void GetTreeListChildren(ref List<CategoryTreeListDto> child, ref IEnumerable<WfWorkflowCategory> list)
        {
            foreach (var item in child)
            {
                item.Children = list.Where(m => m.ParentId == item.Id).Select(m => new CategoryTreeListDto
                {
                    Id = m.Id,
                    ParentId = m.ParentId,
                    Name = m.Name,
                    Memo = m.Memo,
                    Status = m.Status
                }).ToList();
                var tempchilds = item.Children;
                GetTreeListChildren(ref tempchilds, ref list);
                item.Children = tempchilds;
            }
        }

        /// <summary>
        /// 获取流程分类树
        /// </summary>
        /// <returns></returns>
        public async Task<List<ZTree>> GetCategoryTreeAsync()
        {
            var dblist = await databaseFixture.Db.WorkflowCategory.FindAllAsync(m => m.Status == 1);
            var trees = dblist.Select(m => new ZTree
            {
                id = m.Id.ToString(),
                name = m.Name,
                pId = m.ParentId.ToString(),
                open = true,
                @checked = false
            }).ToList();
            return trees;
        }

        public async Task<CategoryDetailDto> GetCategoryDetailAsync(Guid id)
        {
            var category = await databaseFixture.Db.WorkflowCategory.FindByIdAsync(id);
            CategoryDetailDto categoryDto = new CategoryDetailDto()
            {
                Id = category.Id,
                Name = category.Name,
                Memo = category.Memo,
                ParentId = category.ParentId,
                Status = category.Status
            };
            if (category.ParentId != default(Guid))
            {
                var parent = await databaseFixture.Db.WorkflowCategory.FindByIdAsync(category.ParentId);
                categoryDto.ParentName = parent.Name;
            }
            return categoryDto;
        }

        public async Task<bool> InsertAsync(CategoryDetailDto model)
        {
            WfWorkflowCategory category = new WfWorkflowCategory
            {
                CreateTime = DateTime.Now.ToTimeStamp(),
                CreateUserId = model.UserId,
                Id = Guid.NewGuid(),
                Name = model.Name,
                Memo = model.Memo,
                ParentId = model.ParentId,
                Status = model.Status
            };
            bool res = await databaseFixture.Db.WorkflowCategory.InsertAsync(category);
            return res;
        }
        public async Task<bool> UpdateAsync(CategoryDetailDto model)
        {
            var dbmodel = await databaseFixture.Db.WorkflowCategory.FindByIdAsync(model.Id);
            dbmodel.Name = model.Name;
            dbmodel.ParentId = model.ParentId;
            dbmodel.Status = model.Status;
            dbmodel.Memo = model.Memo;
            bool res = await databaseFixture.Db.WorkflowCategory.UpdateAsync(dbmodel);
            return res;
        }

        public async Task<bool> DeleteAsync(CategoryDeleteDto model)
        {
            using (var tran = databaseFixture.Db.BeginTransaction())
            {
                try
                {
                    var dbmodel = await databaseFixture.Db.WorkflowCategory.GetCategoriesAsync(model.Ids);
                    foreach (var item in dbmodel)
                    {
                        item.Status = 0;
                    }
                    await databaseFixture.Db.WorkflowCategory.BulkUpdateAsync(dbmodel, tran);
                    tran.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    return false;
                }
            }
        }
    }
}
