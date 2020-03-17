using JadeFramework.Core.Domain.Entities;
using JadeFramework.Core.Domain.Enum;
using JadeFramework.Core.Domain.Result;
using JadeFramework.Core.Extensions;
using JadeFramework.Core.Mvc;
using JadeFramework.Core.Mvc.Extensions;
using JadeFramework.Core.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using MsSystem.Utility;
using MsSystem.Utility.Filters;
using MsSystem.Web.Areas.Sys.Hubs;
using MsSystem.Web.Areas.Sys.Service;
using MsSystem.Web.Areas.Sys.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using SignalRMessageGroups = MsSystem.Utility.SignalRMessageGroups;

namespace MsSystem.Web.Areas.Sys.Controllers
{
    /// <summary>
    /// 用户管理
    /// </summary>
    [Area("Sys")]
    public class UserController : BaseController
    {
        private readonly string AESKey = "d4ae4a06a63a4c8687a0d884cc6cdff2";

        private ISysUserService _userService;
        private ISysRoleService _roleService;
        private ISysSystemService _systemService;
        private IVerificationCode _verificationCode;
        private readonly IScanningLoginService _scanningLoginService;
        private readonly IHostingEnvironment hostingEnvironment;
        private IHubContext<ScanningLoginHub> _hubContext;

        public UserController(
            ISysUserService userService,
            ISysRoleService roleService,
            ISysSystemService systemService,
            IVerificationCode verificationCode,
            IScanningLoginService scanningLoginService,
            IServiceProvider serviceProvider,
            IHostingEnvironment hostingEnvironment)
        {
            _hubContext = serviceProvider.GetService<IHubContext<ScanningLoginHub>>();
            _userService = userService;
            _roleService = roleService;
            _systemService = systemService;
            _verificationCode = verificationCode;
            this._scanningLoginService = scanningLoginService;
            this.hostingEnvironment = hostingEnvironment;
        }

        #region 用户页面

        /// <summary>
        /// 用户列表
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet]
        [Permission]
        public async Task<IActionResult> Index([FromQuery]UserIndexSearch search)
        {
            if (search.PageIndex.IsDefault())
            {
                search.PageIndex = 1;
            }
            if (search.PageSize.IsDefault())
            {
                search.PageSize = 10;
            }
            var res = await _userService.GetUserPageAsync(search);
            return View(res);
        }

        [HttpGet]
        [Permission]
        public IActionResult Show()
        {
            return View();
        }

        /// <summary>
        /// 数据权限
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Permission]
        public async Task<IActionResult> DataPrivileges()
        {
            var systems = await _systemService.ListAsync();
            return View(systems);
        }

        /// <summary>
        /// 分配部门
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Dept()
        {
            return View();
        }

        #region 登录

        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// 扫码登录
        /// </summary>
        /// <param name="account">账户</param>
        /// <param name="code">code</param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ScanningLogin(string account, string code)
        {
            //判断code是否是本地生成，并校验是否超时
            //string str = AESSecurity.AESDecrypt(code, AESKey);
            //var array = str.Split('&');
            //long ts = array[1].ToInt64();
            //if (DateTime.Now.AddMinutes(3).ToTimeStamp() > ts)
            //{
            //    return Ok("二维码失效");
            //}
            //string qrcode = array[0];
            string qrcode = code;
            StringValues accessToken = "";
            HttpContext.Request.Headers.TryGetValue("Authorization", out accessToken);
            var res = await _scanningLoginService.ScanningLoginAsync(account, accessToken);
            if (res.LoginStatus == LoginStatus.Success)
            {
                //通知页面跳转
                var msg = SignalRMessageGroups.UserGroups.FirstOrDefault(m => m.QrCode == qrcode && m.UserId == 0);
                msg.UserId = res.User.UserId;
                msg.JSON = JsonConvert.SerializeObject(res);
                SignalRMessageGroups.Clear(qrcode, msg.UserId);
                await _hubContext.Clients.Client(msg.ConnectionId).SendAsync("HomePage", msg.QrCode);
            }
            return Ok(res);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Qr(string code)
        {
            if (code == null)
            {
                return RedirectToAction("Login");
            }
            SignalRMessageGroups.Clear();
            var msg = SignalRMessageGroups.UserGroups.FirstOrDefault(m => m.QrCode == code && m.UserId > 0);
            if (msg == null)
            {
                return RedirectToAction("Login");
            }
            await SaveLogin(JsonConvert.DeserializeObject<LoginResult<UserIdentity>>(msg.JSON));
            return Redirect("/");
        }

        /// <summary>
        /// 图形验证码
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ValidateCode()
        {
            string code = "";
            System.IO.MemoryStream ms = _verificationCode.Create(out code);
            HttpContext.Session.SetString(Constants.LoginValidateCode, code);
            Response.Body.Dispose();
            return File(ms.ToArray(), @"image/png");
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<LoginResult<UserIdentity>> Login([FromBody]UserLoginDto model)
        {
            if (string.IsNullOrEmpty(model.username) || string.IsNullOrEmpty(model.password))
            {
                return new LoginResult<UserIdentity>
                {
                    Message = "用户名或密码无效！",
                    LoginStatus = LoginStatus.Error
                };
            }
            //if (model.validatecode.IsNullOrEmpty())
            //{
            //    return new LoginResult<UserIdentity>
            //    {
            //        Message = "请输入验证码！",
            //        LoginStatus = LoginStatus.Error
            //    };
            //}
            //if (HttpContext.Session.GetString(Constants.LoginValidateCode).ToLower() != model.validatecode.ToLower())
            //{
            //    return new LoginResult<UserIdentity>
            //    {
            //        Message = "验证码错误！",
            //        LoginStatus = LoginStatus.Error
            //    };
            //}
            var loginresult = await _userService.LoginAsync(model.username, model.password);
            if (loginresult != null && loginresult.LoginStatus == LoginStatus.Success)
            {
                await SaveLogin(loginresult);
                return new LoginResult<UserIdentity>
                {
                    LoginStatus = LoginStatus.Success,
                    Message = "登录成功"
                };
            }
            else
            {
                return new LoginResult<UserIdentity>
                {
                    Message = loginresult?.Message,
                    LoginStatus = LoginStatus.Success
                };
            }
        }

        private async Task SaveLogin(LoginResult<UserIdentity> loginResult)
        {
            ClaimsIdentity identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaims(loginResult.User.ToClaims());
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
        }


        #endregion

        /// <summary>
        /// 退出
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        #endregion

        #region CURD
        [HttpGet]
        [Permission("/Sys/User/Index", ButtonType.View, false)]
        [ActionName("Get")]
        public async Task<IActionResult> Get([FromQuery]long id)
        {
            var res = await _userService.GetAsync(id);
            return Ok(res);
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Permission("/Sys/User/Index", ButtonType.Add, false)]
        [ActionName("Add")]
        public async Task<IActionResult> Add([FromBody]UserShowDto dto)
        {
            dto.User.CreateUserId = UserIdentity.UserId;
            var res = await _userService.AddAsync(dto);
            return Ok(res);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Permission("/Sys/User/Index", ButtonType.Edit, false)]
        [ActionName("Update")]
        public async Task<IActionResult> Update([FromBody]UserShowDto dto)
        {
            dto.User.UpdateUserId = UserIdentity.UserId;
            var res = await _userService.UpdateAsync(dto);
            return Ok(res);
        }

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost]
        [Permission("/Sys/User/Index", ButtonType.Delete, false)]
        [ActionName("Delete")]
        public async Task<IActionResult> Delete([FromBody]List<long> ids)
        {
            long userid = UserIdentity.UserId;
            var res = await _userService.DeleteAsync(ids, userid);
            return Ok(res);
        }
        #endregion

        #region 角色分配

        [HttpGet]
        [Authorize]
        [ActionName("RoleBox")]
        public async Task<IActionResult> RoleBox([Bind("userid"), FromQuery]int userid)
        {
            var res = await _roleService.GetTreeAsync(userid);
            return Ok(res);
        }

        [HttpPost]
        [Authorize]
        [ActionName("RoleBoxSave")]
        public async Task<IActionResult> RoleBoxSave([FromBody]RoleBoxDto dto)
        {
            dto.CreateUserId = UserIdentity.UserId;
            var res = await _userService.SaveUserRoleAsync(dto);
            return Ok(res);
        }

        #endregion

        #region 数据权限

        [HttpGet]
        [Authorize]
        [ActionName("GetDataPrivileges")]
        public async Task<IActionResult> GetDataPrivileges([FromQuery]DataPrivilegesViewModel model)
        {
            var res = await _userService.GetPrivilegesAsync(model);
            return Ok(res);
        }

        /// <summary>
        /// 数据权限保存
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [ActionName("SaveDataPrivileges")]
        public async Task<IActionResult> SaveDataPrivileges([FromBody]DataPrivilegesDto model)
        {
            var res = await _userService.SaveDataPrivilegesAsync(model);
            return Ok(res);
        }

        #endregion

        #region 部门分配

        /// <summary>
        /// 获取用户部门
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [ActionName("GetUserDept")]
        public async Task<IActionResult> GetUserDept([FromQuery]long userid)
        {
            var res = await _userService.GetUserDeptAsync(userid);
            return Ok(res);
        }

        /// <summary>
        /// 保存用户部门
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [ActionName("SaveUserDept")]
        public async Task<IActionResult> SaveUserDept([FromBody]UserDeptDto dto)
        {
            var res = await _userService.SaveUserDeptAsync(dto);
            return Ok(res);
        }

        #endregion

        #region 个人中心

        [HttpGet]
        [Authorize]
        public IActionResult Center()
        {
            return View();
        }
        /// <summary>
        /// 用户头像
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public IActionResult Image()
        {
            ViewBag.HeadImg = UserIdentity.HeadImg;
            return View();
        }
        /// <summary>
        /// 保存用户上传扥头像
        /// </summary>
        /// <param name="imgurl"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [ActionName("ModifyUserHeadImgAsync")]
        public async Task<bool> ModifyUserHeadImgAsync(string imgurl)
        {
            if (imgurl.IsNullOrEmpty())
            {
                return false;
            }
            return await _userService.ModifyUserHeadImgAsync(UserIdentity.UserId, imgurl);
        }

        /// <summary>
        /// 单个文件上传
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public AjaxResult Upload()
        {
            if (Request.Form.Files.Count != 1)
            {
                return AjaxResult.Error("上传失败");
            }
            var file = Request.Form.Files[0];
            var filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            string newfilename = System.Guid.NewGuid().ToString() + "." + GetFileExt(filename);
            string impath = hostingEnvironment.WebRootPath + "//uploadfile";
            if (!Directory.Exists(impath))
            {
                Directory.CreateDirectory(impath);
            }
            string newfile = impath + $@"//{newfilename}";
            using (FileStream fs = System.IO.File.Create(newfile))
            {
                file.CopyTo(fs);
                fs.Flush();
            }
            string url = "/uploadfile/" + newfilename;
            return AjaxResult.Success(data: url);
        }
        private string GetFileExt(string filename)
        {
            var array = filename.Split('.');
            int leg = array.Length;
            string ext = array[leg - 1];
            return ext;
        }



        #endregion

    }
}