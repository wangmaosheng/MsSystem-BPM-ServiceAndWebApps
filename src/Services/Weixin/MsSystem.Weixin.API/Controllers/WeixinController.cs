using JadeFramework.Core.Helper;
using JadeFramework.Weixin;
using JadeFramework.Weixin.Models;
using JadeFramework.Weixin.Models.RequestMsg;
using JadeFramework.Weixin.Models.RequestMsg.Events;
using JadeFramework.Weixin.Models.ResponseMsg;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MsSystem.Weixin.IService;
using NLog;
using System.Xml.Linq;

namespace MsSystem.Weixin.API.Controllers
{
    /// <summary>
    /// 微信服务器接入接口
    /// </summary>
    [AllowAnonymous]
    [Route("api/Weixin/[action]")]
    [Produces("application/json")]
    [ApiController]
    public class WeixinController : WxApiController
    {
        private readonly IWxRuleService _ruleService;
        private readonly IWxAccountService wxAccountService;
        private readonly Logger nlog = LogManager.GetCurrentClassLogger();

        public WeixinController(IWxRuleService ruleService,IWxAccountService wxAccountService)
        {
            _ruleService = ruleService;
            this.wxAccountService = wxAccountService;
        }

        /// <summary>
        ///  微信接入请求
        /// </summary>
        /// <param name="signature"></param>
        /// <param name="timeStamp"></param>
        /// <param name="nonce"></param>
        /// <param name="echoStr"></param>
        /// <returns></returns>
        [ActionName("Index")]
        [HttpGet]
        public IActionResult Get([FromQuery]string signature, [FromQuery]string timeStamp, [FromQuery]string nonce, [FromQuery]string echoStr)
        {
            string token = wxAccountService.GetAccount().Token;
            if (CheckSignature.Check(signature, timeStamp, nonce, token))
            {
                return Content(echoStr);
            }
            else
            {
                return Content("接入失败");
            }
        }

        /// <summary>
        /// 消息接收处理
        /// </summary>
        /// <param name="authSignature"></param>
        /// <returns></returns>
        public override IActionResult Post(AuthSignature authSignature)
        {
            this.AuthSignature = authSignature;
            var stream = HttpContext.Request.Body;
            XDocument document = XmlHelper.Convert(stream);
            this.ConvertEntity(document.ToString());
            string result = this.ExecuteResult();
            return Content(result);
        }
        /// <summary>
        /// 默认回复（消息自动回复）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected override ResponseRootMsg OnDefault(IRequestRootMsg request)
        {
            return _ruleService.OnDefault(request);
        }

        /// <summary>
        /// 文本请求
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected override ResponseRootMsg OnTextRequest(RequestTextMsg request)
        {
            var res = _ruleService.OnTextRequest(request);
            return res ?? this.OnDefault(request);
        }
        /// <summary>
        /// 订阅
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected override ResponseRootMsg OnEvent_SubscribeRequest(RequestSubscribeEventMsg request)
        {
            var res = _ruleService.OnEvent_SubscribeRequest(request);
            return res ?? this.OnDefault(request);
        }
        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected override ResponseRootMsg OnEvent_UnSubscribeRequest(RequestUnSubscribeEventMsg request)
        {
            var res = _ruleService.OnEvent_UnSubscribeRequest(request);
            return res ?? this.OnDefault(request);
        }

        protected override void OnActionExecuting(RequestRootMsg requestRootMsg)
        {

        }
        protected override void OnActionExecuted(RequestRootMsg requestRootMsg)
        {

        }
    }
}