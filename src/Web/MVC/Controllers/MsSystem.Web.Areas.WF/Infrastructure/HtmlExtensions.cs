using JadeFramework.Core.Extensions;
using JadeFramework.WorkFlow;
using MsSystem.Web.Areas.WF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MsSystem.Web.Areas.WF.Infrastructure
{
    public static class HtmlExtensions
    {
        public static StringBuilder ToWorkFlowCategory(this List<CategoryTreeListDto> list, Guid? parentid = null)
        {
            if (!list.Any())
            {
                return null;
            }
            StringBuilder sb = new StringBuilder();
            foreach (var item in list)
            {
                if (parentid == null)
                {
                    sb.Append("<tr class=\"treegrid-" + item.Id + "\">");
                }
                else
                {
                    sb.Append("<tr class=\"treegrid-" + item.Id + " treegrid-parent-" + parentid + "\">");
                }
                sb.Append("<td><input type=\"checkbox\" class=\"i-checks\" data-pid=\"" + item.Id + "\" value=\"" + item.Id + "\" /></td>");
                sb.Append("<td>" + item.Name + "</td>");
                sb.Append("<td>" + item.Memo + "</td>");
                sb.Append("<td class=\"text-center\">" + (item.Status == 1 ? "<i title=\"可用\" class=\"fa fa-check green\"></i>" : "<i title=\"已删除\" class=\"fa fa-trash red\"></i>") + "</td>");
                sb.Append("<td class=\"text-center\"><a name=\"editcategory\" data-id=\"" + item.Id + "\">编辑</a></td>");
                sb.Append("</tr>");
                if (item.Children.Any())
                {
                    StringBuilder sbchild = ToWorkFlowCategory(item.Children, item.Id);
                    sb.Append(sbchild);
                }
            }
            return sb;
        }

        public static StringBuilder ToWorkFlowStatusIcon(this WorkFlowStatus workFlowStatus)
        {
            StringBuilder html = new StringBuilder();
            html.Append("<span title=\"" + workFlowStatus.GetDescription() + "\">");
            switch (workFlowStatus)
            {
                case WorkFlowStatus.Running:
                    html.Append("<i class=\"fa fa-spinner fa-spin green\"></i>");
                    break;
                case WorkFlowStatus.IsFinish:
                    html.Append("<i class=\"fa fa-check green\"></i>");
                    break;
                case WorkFlowStatus.Deprecate:
                    html.Append("<i class=\"fa fa-close red\"></i>");
                    break;
                case WorkFlowStatus.Back:
                    html.Append("<i class=\"fa fa-mail-reply red\"></i>");
                    break;
                case WorkFlowStatus.Withdraw:
                    html.Append("<i class=\"fa fa-reply-all red\" ></i>");
                    break;
                case WorkFlowStatus.UnSubmit:
                default:
                    html.Append("<i class=\"fa fa-save gray\"></i>");
                    break;
            }
            html.Append(workFlowStatus.GetDescription() + "</span>");
            return html;
        }

        public static StringBuilder ToWorkFlowInstanceStatusIcon(int? isFinish, int status)
        {
            StringBuilder html = new StringBuilder();
            WorkFlowStatus flowStatus = (WorkFlowStatus)status;
            if (isFinish == null && flowStatus == WorkFlowStatus.UnSubmit)
            {
                html.Append("<span class=\"gray\"><i class=\"fa fa-undo\"></i>未提交</span>");
            }
            else if (isFinish == 1 && flowStatus == WorkFlowStatus.IsFinish)
            {
                html.Append("<span><i class=\"fa fa-check green\"></i>已审核</span>");
            }
            else
            {
                switch (flowStatus)
                {
                    case WorkFlowStatus.Running:
                        html.Append("<span><i class=\"fa fa-spin fa-spinner green\"></i>审核中</span>");
                        break;
                    case WorkFlowStatus.Deprecate:
                        html.Append("<span><i class=\"fa fa-close red\"></i>不同意</span>");
                        break;
                    case WorkFlowStatus.Back:
                        html.Append("<span><i class=\"fa fa-mail-reply red\"></i>已退回</span>");
                        break;
                    case WorkFlowStatus.Stop:
                        html.Append("<span><i class=\"fa fa-mail-reply red\"></i>已终止</span>");
                        break;
                    case WorkFlowStatus.Withdraw:
                        html.Append("<span><i class=\"fa fa-reply-all red\"></i>已撤回</span>");
                        break;
                    default:
                        html.Append("<span>状态未知</span>");
                        break;
                }
            }
            return html;
        }
    }
}
