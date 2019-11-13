using JadeFramework.Core.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using MsSystem.OA.API.Hubs;
using MsSystem.OA.API.Infrastructure;
using MsSystem.OA.IService;
using MsSystem.OA.Model;
using MsSystem.OA.ViewModel;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MsSystem.OA.API.Controllers
{
    /// <summary>
    /// 消息接口
    /// </summary>
    [Authorize]
    [Route("api/Message/[action]")]
    [Produces("application/json")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private IHubContext<MessageHub> _hubContext;
        private readonly IOaMessageService _messageService;

        public MessageController(IServiceProvider serviceProvider, IOaMessageService messageService)
        {
            _hubContext = serviceProvider.GetService<IHubContext<MessageHub>>();
            this._messageService = messageService;
        }

        [HttpGet]
        public async Task<Page<OaMessage>> GetPageAsync(int pageIndex, int pageSize)
        {
            return await _messageService.GetPageAsync(pageIndex, pageSize);
        }

        [HttpGet]
        public async Task<MessageShowDTO> GetByIdAsync(long id)
        {
            return await _messageService.GetByIdAsync(id);
        }

        [HttpPost]
        public async Task<bool> InsertAsync([FromBody]MessageShowDTO model)
        {
            return await _messageService.InsertAsync(model);
        }

        [HttpPost]
        public async Task<bool> UpdateAsync([FromBody]MessageShowDTO model)
        {
            return await _messageService.UpdateAsync(model);
        }

        [HttpPost]
        public async Task<bool> DeleteAsync([FromBody]MessageDeleteDTO dto)
        {
            return await _messageService.DeleteAsync(dto);
        }

        /// <summary>
        /// 使消息可用
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> EnableMessageAsync([FromBody]MessageEnableDTO dto)
        {
            var list = await _messageService.EnableMessageAsync(dto.Ids);
            //获取立即执行的消息
            var imlist = list.Where(m => m.StartTime == 0 || m.EndTime == 0);
            foreach (var item in imlist)
            {
                await this.PushMessageAsync(item);
            }
            return true;
        }

        [HttpGet]
        public async Task<Page<OaMessageMyList>> MyListAsync([FromQuery]OaMessageMyListSearch search)
        {
            return await _messageService.MyListAsync(search);
        }

        [HttpGet]
        public async Task<OaMessageMyListDetail> MyListDetailAsync(long id, long userid)
        {
            return await _messageService.MyListDetailAsync(id, userid);
        }




        #region 消息推送

        /// <summary>
        /// 全员推送
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> PushMessageAsync([FromBody]object data)
        {
            await _hubContext.Clients.All.SendAsync(MessageDefault.ReceiveMessage, data);
            return Ok();
        }

        /// <summary>
        /// 对某人推送
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> PushAnyOneAsync([FromBody]MessagePushDTO model)
        {
            if (model == null)
            {
                return Forbid();
            }
            var user = SignalRMessageGroups.UserGroups.FirstOrDefault(m => m.UserId == model.UserId && m.GroupName == model.GroupName);
            if (user != null)
            {
                await _hubContext.Clients.Client(user.ConnectionId).SendAsync(MessageDefault.ReceiveAnyOne, model.MsgJson);
            }
            return Ok();
        }

        /// <summary>
        /// 对某组进行推送
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> PushGroupAsync([FromBody]MessageGroupPushDTO model)
        {
            if (model == null)
            {
                return Forbid();
            }
            var list = SignalRMessageGroups.UserGroups.Where(m => m.GroupName == model.GroupName);
            foreach (var item in list)
            {
                await _hubContext.Clients.Client(item.ConnectionId).SendAsync(model.GroupName, model.MsgJson);
            }
            return Ok();
        }

        #endregion
    }
}
