using Dapper;
using JadeFramework.Core.Domain.Entities;
using JadeFramework.Core.Extensions;
using JadeFramework.Weixin.Enums;
using JadeFramework.Weixin.Models.RequestMsg;
using JadeFramework.Weixin.Models.RequestMsg.Events;
using JadeFramework.Weixin.Models.ResponseMsg;
using MsSystem.Weixin.IRepository;
using MsSystem.Weixin.IService;
using MsSystem.Weixin.Model;
using MsSystem.Weixin.ViewModel;
using NLog;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MsSystem.Weixin.Service
{
    public class WxRuleService : IWxRuleService
    {
        private readonly Logger nlog = LogManager.GetCurrentClassLogger();

        private readonly IWeixinDatabaseFixture databaseFixture;
        public WxRuleService(IWeixinDatabaseFixture databaseFixture)
        {
            this.databaseFixture = databaseFixture;
        }

        #region Weixin

        /// <summary>
        /// 默认回复
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ResponseRootMsg OnDefault(IRequestRootMsg request)
        {
            var dbrule = databaseFixture.Db.WxRule.Find(m => m.RuleType == (int)WxRuleType.Unmatched);
            if (dbrule != null)
            {
                switch ((ResponseMsgType)dbrule.ResponseMsgType)
                {
                    case ResponseMsgType.Text:
                    default:
                        return GetResponseTextMsgByRuleId(dbrule.Id, request);
                }
            }
            else
            {
                return new ResponseRootMsg() { };
            }
        }

        /// <summary>
        /// 文本请求响应
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ResponseRootMsg OnTextRequest(RequestTextMsg request)
        {
            var dbkeyword = databaseFixture.Db.WxKeyword.Find(m => m.Keyword == request.Content.Trim());
            if (dbkeyword == null)
            {
                return null;
            }
            var dbrule = databaseFixture.Db.WxRule.Find(m => m.Id == dbkeyword.RuleId);
            if (dbrule == null)
            {
                return null;
            }
            switch ((ResponseMsgType)dbrule.ResponseMsgType)
            {
                case ResponseMsgType.Text:
                    return GetResponseTextMsgByRuleId(dbrule.Id, request);
                case ResponseMsgType.Image:
                    break;
                case ResponseMsgType.Voice:
                    break;
                case ResponseMsgType.Video:
                    break;
                case ResponseMsgType.Music:
                    break;
                case ResponseMsgType.News:
                    break;
                case ResponseMsgType.Transfer_Customer_Service:
                    break;
                default:
                    break;
            }
            return null;
        }

        /// <summary>
        /// 订阅请求
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ResponseRootMsg OnEvent_SubscribeRequest(RequestSubscribeEventMsg request)
        {
            var dbrule = databaseFixture.Db.WxRule.Find(m => m.RuleType == (int)WxRuleType.Subscribe);
            if (dbrule == null)
            {
                return null;
            }
            #region 添加用户 TODO 可优化
            WxUser user = new WxUser
            {
                OpenId = request.FromUserName,
                Subscribe = 1,
                IsSync = 0
            };
            databaseFixture.Db.WxUser.Insert(user);
            #endregion
            switch ((ResponseMsgType)dbrule.ResponseMsgType)
            {
                case ResponseMsgType.Text:
                    return GetResponseTextMsgByRuleId(dbrule.Id, request);
                case ResponseMsgType.Image:
                    break;
                case ResponseMsgType.Voice:
                    break;
                case ResponseMsgType.Video:
                    break;
                case ResponseMsgType.Music:
                    break;
                case ResponseMsgType.News:
                    break;
                case ResponseMsgType.Transfer_Customer_Service:
                    break;
            }
            return null;
        }

        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ResponseRootMsg OnEvent_UnSubscribeRequest(RequestUnSubscribeEventMsg request)
        {
            var dbwxuser = databaseFixture.Db.WxUser.FindById(request.FromUserName);
            if (dbwxuser != null)
            {
                dbwxuser.Subscribe = 0;
                databaseFixture.Db.WxUser.Update(dbwxuser);
            }
            return null;
        }

        /// <summary>
        /// 文本响应
        /// </summary>
        /// <param name="ruleid"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        private ResponseTextMsg GetResponseTextMsgByRuleId(int ruleid, IRequestRootMsg request)
        {
            WxTextResponse dbtext = databaseFixture.Db.WxTextResponse.Find(m => m.RuleId == ruleid);
            ResponseTextMsg responseTextMsg = new ResponseTextMsg
            {
                Content = dbtext.Content,
                CreateTime = DateTime.Now.ToTimeStamp(),
                FromUserName = request.ToUserName,
                ToUserName = request.FromUserName
            };
            return responseTextMsg;
        }

        #endregion

        #region Database

        /// <summary>
        /// 获取规则
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<Page<RuleListDto>> GetRulePageAsync(int pageIndex, int pageSize)
        {
            int offset = (pageIndex - 1) * pageSize;
            string sql = $@"SELECT ru.*,kws.Keywords FROM `wx_rule` ru 
            LEFT JOIN(SELECT kw.`RuleId`, GROUP_CONCAT(kw.`Keyword`) AS Keywords FROM `wx_keyword` kw GROUP BY kw.`RuleId`) kws ON kws.RuleId = ru.`Id` 
            ORDER BY RuleType DESC, Id ASC LIMIT { offset},{pageSize}";
            Page<RuleListDto> page = new Page<RuleListDto>
            {
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            page.Items = await databaseFixture.Db.Connection.QueryAsync<RuleListDto>(sql, null);
            page.TotalItems = databaseFixture.Db.Connection.ExecuteScalar<int>(@"SELECT COUNT(1) FROM `wx_rule` ru
            LEFT JOIN (SELECT kw.`RuleId`,GROUP_CONCAT(kw.`Keyword`) AS Keywords FROM `wx_keyword` kw GROUP BY kw.`RuleId`) kws ON kws.RuleId=ru.`Id`
            ORDER BY RuleType DESC,Id ASC");
            return page;
        }

        /// <summary>
        /// 获取规则回复明细
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<RuleReplyDto> GetRuleReplyAsync(int id)
        {
            var dbrule = await databaseFixture.Db.WxRule.FindByIdAsync(id);
            RuleReplyDto model = new RuleReplyDto()
            {
                Id = dbrule.Id,
                RuleName = dbrule.RuleName,
                RuleType = dbrule.RuleType,
                RequestMsgType = dbrule.RequestMsgType,
                ResponseMsgType = dbrule.ResponseMsgType
            };
            switch ((RequestMsgType)dbrule.RequestMsgType)
            {
                case RequestMsgType.Text:
                    var dbkeywords = await databaseFixture.Db.WxKeyword.FindAllAsync(m => m.RuleId == dbrule.Id);
                    model.Keywords = dbkeywords.Select(m => m.Keyword).ToList();
                    break;
                case RequestMsgType.Image:
                    break;
                case RequestMsgType.Voice:
                    break;
                case RequestMsgType.Video:
                    break;
                case RequestMsgType.Location:
                    break;
                case RequestMsgType.Link:
                    break;
                case RequestMsgType.ShortVideo:
                    break;
                case RequestMsgType.Event:
                    break;
                default:
                    break;
            }
            switch ((ResponseMsgType)dbrule.ResponseMsgType)
            {
                case ResponseMsgType.Text:
                    var dbtexts = await databaseFixture.Db.WxTextResponse.FindAllAsync(m => m.RuleId == dbrule.Id);
                    model.ResponseText = dbtexts.Select(m => m.Content).ToList();
                    break;
                case ResponseMsgType.Image:
                    break;
                case ResponseMsgType.Voice:
                    break;
                case ResponseMsgType.Video:
                    break;
                case ResponseMsgType.Music:
                    break;
                case ResponseMsgType.News:
                    break;
                case ResponseMsgType.Transfer_Customer_Service:
                    break;
                default:
                    break;
            }
            return model;
        }

        /// <summary>
        /// 规则新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> AddAsync(RuleReplyDto model)
        {
            using (var tran = databaseFixture.Db.BeginTransaction())
            {
                try
                {
                    WxRule rule = new WxRule
                    {
                        RuleName = model.RuleName,
                        RuleType = model.RuleType,
                        ResponseMsgType = model.ResponseMsgType
                    };
                    switch ((WxRuleType)model.RuleType)
                    {
                        case WxRuleType.Normal:
                            rule.RequestMsgType = (int)RequestMsgType.Text;
                            break;
                        case WxRuleType.Subscribe:
                            rule.RequestMsgType = (int)RequestMsgType.Event;
                            break;
                        case WxRuleType.Unmatched:
                            rule.RequestMsgType = model.RequestMsgType;
                            break;
                    }
                    int ruleid = await databaseFixture.Db.WxRule.InsertReturnIdAsync(rule, tran);

                    switch ((RequestMsgType)rule.RequestMsgType)
                    {
                        case RequestMsgType.Text://关键字请求
                            foreach (var item in model.Keywords)
                            {
                                WxKeyword keyword = new WxKeyword
                                {
                                    Keyword = item,
                                    RuleId = ruleid
                                };
                                await databaseFixture.Db.WxKeyword.InsertAsync(keyword, tran);
                            }
                            break;
                        case RequestMsgType.Image:
                            break;
                        case RequestMsgType.Voice:
                            break;
                        case RequestMsgType.Video:
                            break;
                        case RequestMsgType.Location:
                            break;
                        case RequestMsgType.Link:
                            break;
                        case RequestMsgType.ShortVideo:
                            break;
                        case RequestMsgType.Event:
                            break;
                        default:
                            break;
                    }

                    switch ((ResponseMsgType)model.ResponseMsgType)
                    {
                        case ResponseMsgType.Text:
                            foreach (var item in model.ResponseText)
                            {
                                WxTextResponse textResponse = new WxTextResponse
                                {
                                    Content = item,
                                    RuleId = ruleid
                                };
                                await databaseFixture.Db.WxTextResponse.InsertAsync(textResponse, tran);
                            }
                            break;
                        case ResponseMsgType.Image:
                            break;
                        case ResponseMsgType.Voice:
                            break;
                        case ResponseMsgType.Video:
                            break;
                        case ResponseMsgType.Music:
                            break;
                        case ResponseMsgType.News:
                            break;
                        case ResponseMsgType.Transfer_Customer_Service:
                            break;
                        default:
                            break;
                    }

                    tran.Commit();
                    return true;
                }
                catch (System.Exception ex)
                {
                    tran.Rollback();
                    return false;
                }
            }
        }

        /// <summary>
        /// 规则修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(RuleReplyDto model)
        {
            using (var tran = databaseFixture.Db.BeginTransaction())
            {
                try
                {
                    var dbrule = await databaseFixture.Db.WxRule.FindByIdAsync(model.Id);
                    dbrule.ResponseMsgType = model.ResponseMsgType;
                    dbrule.RuleName = model.RuleName;
                    dbrule.RuleType = model.RuleType;
                    switch ((WxRuleType)model.RuleType)
                    {
                        case WxRuleType.Normal:
                            dbrule.RequestMsgType = (int)RequestMsgType.Text;
                            break;
                        case WxRuleType.Subscribe:
                            dbrule.RequestMsgType = (int)RequestMsgType.Event;
                            break;
                        case WxRuleType.Unmatched:
                            dbrule.RequestMsgType = model.RequestMsgType;
                            break;
                    }
                    await databaseFixture.Db.WxRule.UpdateAsync(dbrule, tran);

                    switch ((RequestMsgType)dbrule.RequestMsgType)
                    {
                        case RequestMsgType.Text://关键词
                            var dbkeywords = await databaseFixture.Db.WxKeyword.FindAllAsync(m => m.RuleId == dbrule.Id);
                            foreach (var item in dbkeywords)
                            {
                                await databaseFixture.Db.WxKeyword.DeleteAsync(item, tran);
                            }
                            foreach (var item in model.Keywords)
                            {
                                WxKeyword keyword = new WxKeyword
                                {
                                    RuleId = dbrule.Id,
                                    Keyword = item
                                };
                                await databaseFixture.Db.WxKeyword.InsertAsync(keyword, tran);
                            }
                            break;
                        case RequestMsgType.Image:
                            break;
                        case RequestMsgType.Voice:
                            break;
                        case RequestMsgType.Video:
                            break;
                        case RequestMsgType.Location:
                            break;
                        case RequestMsgType.Link:
                            break;
                        case RequestMsgType.ShortVideo:
                            break;
                        case RequestMsgType.Event:
                            break;
                        default:
                            break;
                    }
                    switch ((ResponseMsgType)model.ResponseMsgType)
                    {
                        case ResponseMsgType.Text:
                            var dbtexts = await databaseFixture.Db.WxTextResponse.FindAllAsync(m => m.RuleId == dbrule.Id);
                            foreach (var item in dbtexts)
                            {
                                await databaseFixture.Db.WxTextResponse.DeleteAsync(item, tran);
                            }
                            foreach (var item in model.ResponseText)
                            {
                                WxTextResponse textResponse = new WxTextResponse
                                {
                                    Content = item,
                                    RuleId = dbrule.Id
                                };
                                await databaseFixture.Db.WxTextResponse.InsertAsync(textResponse, tran);
                            }
                            break;
                        case ResponseMsgType.Image:
                            break;
                        case ResponseMsgType.Voice:
                            break;
                        case ResponseMsgType.Video:
                            break;
                        case ResponseMsgType.Music:
                            break;
                        case ResponseMsgType.News:
                            break;
                        case ResponseMsgType.Transfer_Customer_Service:
                            break;
                        default:
                            break;
                    }

                    tran.Commit();
                    return true;
                }
                catch (System.Exception ex)
                {
                    tran.Rollback();
                    return false;
                }
            }
        }

        #endregion


    }
}
