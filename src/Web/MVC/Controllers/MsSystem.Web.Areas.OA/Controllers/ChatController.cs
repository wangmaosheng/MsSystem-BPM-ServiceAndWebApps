using JadeFramework.Core.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MsSystem.Utility.Filters;
using MsSystem.Web.Areas.OA.Service;
using MsSystem.Web.Areas.OA.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MsSystem.Web.Areas.OA.Controllers
{
    [Area("OA")]
    [Authorize]
    public class ChatController : BaseController
    {
        private readonly IOaChatService _chatService;

        public ChatController(IOaChatService chatService)
        {
            this._chatService = chatService;
        }

        [HttpGet]
        [Permission]
        public IActionResult Index()
        {
            ViewBag.User = UserIdentity;
            return View();
        }

        [HttpPost]
        [ActionName("GetChatUserAsync")]
        public async Task<List<ChatUserViewModel>> GetChatUserAsync([FromBody]List<long> chattinguserids)
        {
            var res = await _chatService.GetChatUserAsync(chattinguserids);
            res.Remove(res.First(m => m.UserId == UserIdentity.UserId));
            return res;
        }

        [HttpGet]
        [ActionName("GetChatListAsync")]
        public async Task<List<ChatUserListDto>> GetChatListAsync(long receiver,int pageindex)
        {
            ChatUserListSearchDto model = new ChatUserListSearchDto();
            model.Receiver = receiver;
            model.PageIndex = pageindex == 0 ? 1 : pageindex;
            model.PageSize = 20;
            model.Sender = UserIdentity.UserId;
            var res = await _chatService.GetChatListAsync(model);
            return res;
        }
    }
}
