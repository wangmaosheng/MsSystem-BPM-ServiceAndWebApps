using System;

namespace MsSystem.Weixin.ViewModel
{
    public class WxAccountListDto
    {
        /// <summary>
        /// 微信原始Id
        /// </summary>
        public string WeixinId { get; set; }

        /// <summary>
        /// 开发者ID
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// 开发者密码
        /// </summary>
        public string AppSecret { get; set; }

        /// <summary>
        /// 公众号名称
        /// </summary>
        public string WeixinName { get; set; }

        /// <summary>
        /// 公众号二维码地址
        /// </summary>
        public string WeixinQRCode { get; set; }

        /// <summary>
        /// AccessToken
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// AccessToken创建时间
        /// </summary>
        public DateTime? AccessTokenCreateTime { get; set; }

        /// <summary>
        /// JS API临时票据
        /// </summary>
        public string JsApiTicket { get; set; }

        /// <summary>
        /// JS API临时票据创建时间
        /// </summary>
        public DateTime? JsApiTicketCreateTime { get; set; }

        /// <summary>
        /// 微信号关注引导页地址URL
        /// </summary>
        public string SubscribePageUrl { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public long CreateTime { get; set; }

        /// <summary>
        /// 令牌
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 消息加解密密钥
        /// </summary>
        public string EncodingAESKey { get; set; }
    }
}
