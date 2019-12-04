using JadeFramework.Core.Domain.Enum;
using JadeFramework.Core.Mvc;
using Microsoft.AspNetCore.Mvc;
using MsSystem.Utility.Filters;
using MsSystem.Web.Areas.OA.Service;
using MsSystem.Web.Areas.OA.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MsSystem.Web.Areas.OA.Controllers
{
    [Area("OA")]
    public class MessageController : BaseController
    {
        private readonly IOaMessageService _messageService;

        public MessageController(IOaMessageService messageService)
        {
            this._messageService = messageService;
        }
        [HttpGet]
        [Permission]
        public async Task<IActionResult> Index(int pageIndex = 1, int pageSize = 10)
        {
            var res = await _messageService.GetPageAsync(pageIndex, pageSize);
            return View(res);
        }
        [HttpGet]
        [Permission("/OA/Message/Index", ButtonType.View)]
        public IActionResult Show()
        {
            return View();
        }

        #region 权限控制

        [HttpGet]
        [Permission("/OA/Message/Index", ButtonType.View, false)]
        [ActionName("Get")]
        public async Task<IActionResult> Get([Bind("Id"), FromQuery]long id)
        {
            if (id > 0)
            {
                var res = await _messageService.GetByIdAsync(id);
                return Ok(res);
            }
            else
            {
                return Ok(new MessageShowDTO()
                {
                    StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddDays(1)
                });
            }
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Permission("/OA/Message/Index", ButtonType.Add, false)]
        [ActionName("Add")]
        public async Task<IActionResult> Add([FromBody]MessageShowDTO model)
        {
            model.CreateUserId = UserIdentity.UserId;
            bool res = await _messageService.InsertAsync(model);
            return Ok(res);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Permission("/OA/Message/Index", ButtonType.Edit, false)]
        [ActionName("Update")]
        public async Task<IActionResult> Update([FromBody]MessageShowDTO model)
        {
            bool res = await _messageService.UpdateAsync(model);
            return Ok(res);
        }

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost]
        [Permission("/OA/Message/Index", ButtonType.Delete, false)]
        [ActionName("Delete")]
        public async Task<IActionResult> Delete([FromBody]List<long> ids)
        {
            var res = await _messageService.DeleteAsync(new MessageDeleteDTO
            {
                Ids = ids,
                UserId = UserIdentity.UserId
            });
            return Ok(res);
        }


        [HttpPost]
        [ActionName("EnableMessage")]
        public async Task<IActionResult> EnableMessage([FromBody]List<long> ids)
        {
            var res = await _messageService.EnableMessageAsync(new MessageEnableDTO
            {
                Ids = ids,
                UserId = UserIdentity.UserId
            });
            return Ok(res);
        }
        #endregion


        /// <summary>
        /// 我的消息
        /// </summary>
        /// <returns></returns>
        [Permission("/OA/Message/MyList", ButtonType.View, true)]
        public async Task<IActionResult> MyList(OaMessageMyListSearch search)
        {
            if (search.PageIndex == 0)
            {
                search.PageIndex = 1;
            }
            if (search.PageSize == 0)
            {
                search.PageSize = 10;
            }
            search.UserId = UserIdentity.UserId;
            var page = await _messageService.MyListAsync(search);
            return View(page);
        }

        /// <summary>
        /// 获取首页消息
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> HomeMessage()
        {
            OaMessageMyListSearch search = new OaMessageMyListSearch();
            if (search.PageIndex == 0)
            {
                search.PageIndex = 1;
            }
            if (search.PageSize == 0)
            {
                search.PageSize = 10;
            }
            search.IsRead = 0;
            search.UserId = UserIdentity.UserId;
            var page = await _messageService.MyListAsync(search);
            return Ok(page);
        }



        /// <summary>
        /// 消息明细
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Detail(long id)
        {
            var res = await _messageService.MyListDetailAsync(id, UserIdentity.UserId);
            return View(res);
        }

        /// <summary>
        /// 消息已读
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("ReadMessageAsync")]
        public async Task<bool> ReadMessageAsync([FromBody]OaMessageReadDto message)
        {
            message.UserId = UserIdentity.UserId;
            var res = await _messageService.ReadMessageAsync(message);
            return res;
        }
    }
}
