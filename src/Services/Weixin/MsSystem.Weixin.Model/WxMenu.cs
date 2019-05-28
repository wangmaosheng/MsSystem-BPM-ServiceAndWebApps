using JadeFramework.Core.Dapper;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MsSystem.Weixin.Model
{
    /// <summary>
    /// 微信菜单
    /// </summary>
    [Table("wx_menu")]
    public class WxMenu
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key, Identity]
        public int Id { get; set; }
        /// <summary>
        /// 父级ID
        /// </summary>
        public int ParentId { get; set; }
        /// <summary>
        /// 菜单标题，不超过16个字节，子菜单不超过60个字节
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 菜单的响应动作类型，view表示网页类型，click表示点击类型，miniprogram表示小程序类型
        /// <see cref="JadeFramework.Weixin.Enums.WxMenuType"/>
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 菜单KEY值，用于消息接口推送，不超过128字节
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 网页 链接，用户点击菜单可打开链接，不超过1024字节。 type为miniprogram时，不支持小程序的老版本客户端将打开本url。
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 小程序的appid（仅认证公众号可配置）
        /// </summary>
        public string AppId { get; set; }
        /// <summary>
        /// 小程序的页面路径
        /// </summary>
        public string PagePath { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 是否已删除
        /// </summary>
        public int IsDel { get; set; }
        public long CreateTime { get; set; }
    }
    internal class WxMenuMapper : ClassMapper<WxMenu>
    {
        public WxMenuMapper()
        {
            Table("wx_menu");
            AutoMap();
        }
    }
}
