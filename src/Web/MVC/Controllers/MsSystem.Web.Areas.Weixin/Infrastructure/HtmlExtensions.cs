using MsSystem.Web.Areas.Weixin.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MsSystem.Web.Areas.Weixin.Infrastructure
{
    /// <summary>
    /// HTML扩展
    /// </summary>
    public static class HtmlExtensions
    {
        /// <summary>
        /// 微信菜单树
        /// </summary>
        /// <param name="list"></param>
        /// <param name="parentid"></param>
        /// <returns></returns>
        public static StringBuilder ToWxMenus(this List<WxMenuDto> list, long? parentid = null)
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
                sb.Append("<td>" + item.Type + "</td>");
                sb.Append("<td>" + item.Key + "</td>");
                sb.Append("<td>" + item.Url + "</td>");
                sb.Append("<td>" + item.AppId + "</td>");
                sb.Append("<td>" + item.PagePath + "</td>");
                sb.Append("<td>" + item.Sort + "</td>");
                sb.Append("<td class=\"text-center\">");
                sb.Append(item.IsDel == 1
                    ? "<i title=\"已删除\" class=\"fa fa-remove red\"></i>"
                    : "<i title=\"可用\" class=\"fa fa-check green\"></i>");
                sb.Append("</td></tr>");
                if (item.Children.Any())
                {
                    StringBuilder sbchild = ToWxMenus(item.Children, item.Id);
                    sb.Append(sbchild);
                }
            }
            return sb;
        }


    }
}
