using JadeFramework.Core.Domain.Entities;
using JadeFramework.Weixin.Models.RequestMsg;
using JadeFramework.Weixin.Models.RequestMsg.Events;
using JadeFramework.Weixin.Models.ResponseMsg;
using MsSystem.Weixin.ViewModel;
using System.Threading.Tasks;

namespace MsSystem.Weixin.IService
{
    public interface IWxRuleService
    {
        #region Weixin
        /// <summary>
        /// 默认回复
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        ResponseRootMsg OnDefault(IRequestRootMsg request);
        /// <summary>
        /// 文本请求
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        ResponseRootMsg OnTextRequest(RequestTextMsg request);
        /// <summary>
        /// 订阅请求
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        ResponseRootMsg OnEvent_SubscribeRequest(RequestSubscribeEventMsg request);
        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        ResponseRootMsg OnEvent_UnSubscribeRequest(RequestUnSubscribeEventMsg request);
        #endregion

        #region Database

        /// <summary>
        /// 获取规则
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<Page<RuleListDto>> GetRulePageAsync(int pageIndex, int pageSize);

        /// <summary>
        /// 获取规则回复明细
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<RuleReplyDto> GetRuleReplyAsync(int id);

        Task<bool> AddAsync(RuleReplyDto model);

        Task<bool> UpdateAsync(RuleReplyDto model);

        #endregion

    }
}
