using MsSystem.Web.Areas.Sys.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MsSystem.Web.Areas.Sys.Infrastructure
{
    public static class HtmlExtensions
    {
        /// <summary>
        /// 菜单树
        /// </summary>
        /// <param name="list"></param>
        /// <param name="parentid"></param>
        /// <returns></returns>
        public static StringBuilder ToMenus(this List<ResourceTreeViewModel> list, long? parentid = null)
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
                    sb.Append("<tr class=\"treegrid-" + item.ResourceId + "\">");
                }
                else
                {
                    sb.Append("<tr class=\"treegrid-" + item.ResourceId + " treegrid-parent-" + parentid + "\">");
                }
                sb.Append("<td><input type=\"checkbox\" class=\"i-checks\" data-pid=\"" + item.ResourceId + "\" value=\"" + item.ResourceId + "\" /></td>");
                sb.Append("<td>" + item.ResourceName + "</td>");
                sb.Append("<td>" + item.ResourceUrl + "</td>");
                sb.Append("<td>" + item.Memo + "</td>");

                if (item.Buttons != null && item.Buttons.Any())
                {
                    StringBuilder btnsb = new StringBuilder();
                    btnsb.Append("<td class=\"w300\">");
                    foreach (var btn in item.Buttons)
                    {
                        btnsb.Append("<a class=\"btn btn-primary btn-xs mh3\">" + btn.Value + "</a>");
                    }
                    btnsb.Append("</td>");
                    sb.Append(btnsb);
                }
                else
                {
                    sb.Append("<td class=\"w300\"></td>");
                }

                sb.Append("<td class=\"text-center\"><i class=\"" + item.Icon + "\"></i></td>");
                sb.Append("<td>" + item.Sort + "</td>");
                sb.Append("<td class=\"text-center\">");
                sb.Append(item.IsDel == 1
                    ? "<i title=\"已删除\" class=\"fa fa-remove red\"></i>"
                    : "<i title=\"可用\" class=\"fa fa-check green\"></i>");
                sb.Append("</td></tr>");
                if (item.Children.Any())
                {
                    StringBuilder sbchild = ToMenus(item.Children, item.ResourceId);
                    sb.Append(sbchild);
                }
            }
            return sb;
        }
        /// <summary>
        /// 部门树
        /// </summary>
        /// <param name="list"></param>
        /// <param name="parentid"></param>
        /// <returns></returns>
        public static StringBuilder ToDepts(this List<DeptTreeViewModel> list, long? parentid = null)
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
                    sb.Append("<tr class=\"treegrid-" + item.DeptId + "\">");
                }
                else
                {
                    sb.Append("<tr class=\"treegrid-" + item.DeptId + " treegrid-parent-" + parentid + "\">");
                }
                sb.Append("<td><input type=\"checkbox\" class=\"i-checks\" data-pid=\"" + item.DeptId + "\" value=\"" + item.DeptId + "\" /></td>");
                sb.Append("<td>" + item.DeptName + "</td>");
                sb.Append("<td>" + item.DeptCode + "</td>");
                sb.Append("<td>" + item.Memo + "</td>");
                sb.Append("<td class=\"text-center\">");
                sb.Append(item.IsDel == 1
                    ? "<i title=\"已删除\" class=\"fa fa-remove red\"></i>"
                    : "<i title=\"可用\" class=\"fa fa-check green\"></i>");
                sb.Append("</td></tr>");
                if (item.Children.Any())
                {
                    StringBuilder sbchild = ToDepts(item.Children, item.DeptId);
                    sb.Append(sbchild);
                }
            }
            return sb;
        }
    }
}
