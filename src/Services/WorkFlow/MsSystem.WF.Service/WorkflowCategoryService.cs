using JadeFramework.Core.Domain.Entities;
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
            var dblist = await databaseFixture.Db.WorkflowCategory.FindAllAsync();
            foreach (var item in dblist.Where(m => m.ParentId == default(Guid)))
            {
                CategoryTreeListDto tree = new CategoryTreeListDto
                {
                    Id = item.Id,
                    ParentId = item.ParentId,
                    Memo = item.Memo,
                    Name = item.Name
                };
                tree.Children = dblist.Where(m => m.ParentId == tree.Id).Select(m => new CategoryTreeListDto
                {
                    Id = m.Id,
                    Name = m.Name,
                    ParentId = m.ParentId,
                    Memo = m.Memo
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
                    Memo = m.Memo
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
    }
}
