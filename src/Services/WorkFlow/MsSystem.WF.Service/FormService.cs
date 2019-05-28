using AutoMapper;
using JadeFramework.Core.Domain.Entities;
using JadeFramework.WorkFlow;
using MsSystem.WF.IRepository;
using MsSystem.WF.IService;
using MsSystem.WF.Model;
using MsSystem.WF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MsSystem.WF.Service
{
    public class FormService : IFormService
    {
        private readonly IWFDatabaseFixture databaseFixture;
        private readonly IMapper mapper;

        public FormService(IWFDatabaseFixture databaseFixture, IMapper mapper)
        {
            this.databaseFixture = databaseFixture;
            this.mapper = mapper;
        }

        public async Task<Page<FormPageDto>> GetPageAsync(int pageIndex, int pageSize)
        {
            return await databaseFixture.Db.WorkflowForm.GetPageAsync(pageIndex, pageSize);
        }

        public async Task<FormDetailDto> GetFormDetailAsync(Guid id)
        {
            var dbform = await databaseFixture.Db.WorkflowForm.FindByIdAsync(id);
            var res = mapper.Map<WfWorkflowForm, FormDetailDto>(dbform);
            res.Content = dbform.OriginalContent;
            return res;
        }

        public async Task<bool> InsertAsync(FormDetailDto model)
        {
            var form = mapper.Map<FormDetailDto, WfWorkflowForm>(model);
            form.FormId = Guid.NewGuid();
            if ((WorkFlowFormType)form.FormType == WorkFlowFormType.Custom)
            {
                form.FormUrl = null;

                form.OriginalContent = model.Content;
                //解析html内容
                form.Content = addHtmlNameAttr(model.Content);
            }
            else
            {
                form.Content = null;
                form.OriginalContent = null;
            }
            return await databaseFixture.Db.WorkflowForm.InsertAsync(form);
        }

        private string addHtmlNameAttr(string html)
        {
            int index = 1;
            string name = "FlowParam_";
            List<string> tags = new List<string>();
            if (html.Contains("input"))
                tags.Add("input");
            if (html.Contains("select"))
                tags.Add("select");
            if (html.Contains("textarea"))
                tags.Add("textarea");
            foreach (var item in tags)
            {
                string pattern = "<" + item;
                string[] sArray = Regex.Split(html, pattern, RegexOptions.IgnoreCase);
                string newhtml = "";
                for (int i = 0; i < sArray.Length; i++)
                {
                    if (i == sArray.Length - 1)
                    {
                        newhtml += sArray[i];
                    }
                    else
                    {
                        string newInput = $"<{item} name=\"{name + index}\"";
                        newhtml += sArray[i] + newInput;
                        index++;
                    }
                }
                html = newhtml;
            }
            return html;
        }


        public async Task<bool> UpdateAsync(FormDetailDto model)
        {
            var dbform = await databaseFixture.Db.WorkflowForm.FindByIdAsync(model.FormId);
            dbform.FormName = model.FormName;
            dbform.FormType = model.FormType;
            if ((WorkFlowFormType)dbform.FormType == WorkFlowFormType.Custom)
            {
                dbform.FormUrl = null;
                dbform.OriginalContent = model.Content;
                dbform.Content = addHtmlNameAttr(model.Content);
            }
            else
            {
                dbform.Content = null;
                dbform.OriginalContent = null;
                dbform.FormUrl = model.FormUrl;
            }
            return await databaseFixture.Db.WorkflowForm.UpdateAsync(dbform);
        }

        public async Task<List<ZTree>> GetFormTreeAsync()
        {
            var dblist = await databaseFixture.Db.WorkflowForm.FindAllAsync();
            return dblist.Select(m => new ZTree
            {
                id = m.FormId.ToString(),
                name = m.FormName,
            }).ToList();
        }

    }
}
