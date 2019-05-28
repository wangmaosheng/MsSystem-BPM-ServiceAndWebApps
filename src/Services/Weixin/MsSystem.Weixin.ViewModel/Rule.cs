using System.Collections.Generic;

namespace MsSystem.Weixin.ViewModel
{
    /// <summary>
    /// 规则列表DTO
    /// </summary>
    public class RuleListDto
    {
        public int Id { get; set; }

        /// <summary>
        /// 规则名称
        /// </summary>
        public string RuleName { get; set; }

        /// <summary>
        /// 规则类型0:普通，1:未匹配到回复规则
        /// </summary>
        public int RuleType { get; set; }

        /// <summary>
        /// 规则类型
        /// TODO：<see cref="JadeFramework.Weixin.Enums.RequestMsgType"/>请求类型
        /// </summary>
        public int RequestMsgType { get; set; }

        /// <summary>
        /// 响应消息类型
        /// TODO：<see cref="JadeFramework.Weixin.Enums.ResponseMsgType"/>响应类型
        /// </summary>
        public int ResponseMsgType { get; set; }
        /// <summary>
        /// 关键词
        /// </summary>
        public string Keywords { get; set; }
    }

    /// <summary>
    /// 规则明细
    /// </summary>
    public class RuleReplyDto
    {
        public RuleReplyDto()
        {
            this.Keywords = new List<string>();
            this.ResponseText = new List<string>();
        }

        public int Id { get; set; }

        /// <summary>
        /// 规则名称
        /// </summary>
        public string RuleName { get; set; }
        /// <summary>
        /// 规则类型0:普通，1:未匹配到回复规则
        /// </summary>
        public int RuleType { get; set; }

        /// <summary>
        /// 规则类型
        /// TODO：<see cref="JadeFramework.Weixin.Enums.RequestMsgType"/>请求类型
        /// </summary>
        public int RequestMsgType { get; set; }

        /// <summary>
        /// 响应消息类型
        /// TODO：<see cref="JadeFramework.Weixin.Enums.ResponseMsgType"/>响应类型
        /// </summary>
        public int ResponseMsgType { get; set; }

        /// <summary>
        /// 匹配的关键词
        /// </summary>
        public List<string> Keywords { get; set; }

        /// <summary>
        /// 文本响应
        /// </summary>
        public List<string> ResponseText { get; set; }

    }
}
