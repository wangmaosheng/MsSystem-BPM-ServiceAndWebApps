using JadeFramework.Core.Domain.Entities;
using JadeFramework.Core.Extensions;
using MsSystem.OA.IRepository;
using MsSystem.OA.IService;
using MsSystem.OA.Model;
using MsSystem.OA.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MsSystem.OA.Service
{
    public class OaMessageService : IOaMessageService
    {
        private IOaDatabaseFixture _databaseFixture;
        public OaMessageService(IOaDatabaseFixture databaseFixture)
        {
            _databaseFixture = databaseFixture;
        }
        public async Task<bool> DeleteAsync(MessageDeleteDTO dto)
        {
            using (var tran = _databaseFixture.Db.BeginTransaction())
            {
                try
                {
                    var list = await _databaseFixture.Db.OaMessage.FindAllAsync(m => m.IsDel == 0 && dto.Ids.Contains(m.Id));
                    foreach (var item in list)
                    {
                        item.IsDel = 1;
                        await _databaseFixture.Db.OaMessage.UpdateAsync(item);
                    }
                    tran.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    return false;
                }
            }
        }

        public async Task<MessageShowDTO> GetByIdAsync(long id)
        {
            var dbmsg = await _databaseFixture.Db.OaMessage.FindByIdAsync(id);
            MessageShowDTO dto = new MessageShowDTO
            {
                Id = dbmsg.Id,
                MsgType = dbmsg.MsgType,
                FaceUserType = dbmsg.FaceUserType,
                Title = dbmsg.Title,
                IsLocal = dbmsg.IsLocal,
                TargetType = dbmsg.TargetType,
                Link = dbmsg.Link,
                StartTime = dbmsg.StartTime.ToDateTime(),
                EndTime = dbmsg.EndTime.ToDateTime(),
                IsEnable = dbmsg.IsEnable,
                IsDel = dbmsg.IsDel,
                MakerUserId = dbmsg.MakerUserId,
                CreateUserId = dbmsg.CreateUserId,
                CreateTime = dbmsg.CreateTime,
                Content = dbmsg.Content
            };

            var users = await _databaseFixture.Db.OaMessageUser.FindAllAsync(m => m.MessageId == dto.Id);
            dto.UserIds = users.Select(m => m.UserId).ToList();

            return dto;
        }

        public async Task<Page<OaMessage>> GetPageAsync(int pageIndex, int pageSize)
        {
            return await _databaseFixture.Db.OaMessage.GetPageAsync(pageIndex, pageSize);
        }

        public async Task<bool> InsertAsync(MessageShowDTO model)
        {
            using (var tran = _databaseFixture.Db.BeginTransaction())
            {
                try
                {
                    OaMessage addMsg = new OaMessage
                    {
                        Id = model.Id,
                        MsgType = model.MsgType,
                        FaceUserType = model.FaceUserType,
                        Title = model.Title,
                        IsLocal = model.IsLocal,
                        TargetType = model.TargetType,
                        Link = model.Link,
                        Content = model.Content,
                        StartTime = model.StartTime == null ? 0 : model.StartTime.Value.ToTimeStamp(),
                        EndTime = model.EndTime == null ? 0 : model.EndTime.Value.ToTimeStamp(),
                        IsExecuted = 0,
                        IsEnable = 0,
                        IsDel = 0,
                        MakerUserId = model.MakerUserId,
                        CreateUserId = model.CreateUserId,
                        CreateTime = DateTime.Now.ToTimeStamp()
                    };
                    int id = await _databaseFixture.Db.OaMessage.InsertReturnIdAsync(addMsg, tran);
                    if (model.UserIds.HasItems())
                    {
                        List<OaMessageUser> msgUsers = new List<OaMessageUser>();
                        foreach (var item in model.UserIds)
                        {
                            msgUsers.Add(new OaMessageUser
                            {
                                MessageId = id,
                                UserId = item
                            });
                        }
                        await _databaseFixture.Db.OaMessageUser.BulkInsertAsync(msgUsers, tran);
                    }

                    tran.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    return false;
                }
            }
        }

        public async Task<bool> UpdateAsync(MessageShowDTO model)
        {
            using (var tran = _databaseFixture.Db.BeginTransaction())
            {
                try
                {
                    var dbmodel = await _databaseFixture.Db.OaMessage.FindByIdAsync(model.Id);
                    if (dbmodel != null)
                    {
                        dbmodel.MsgType = model.MsgType;
                        dbmodel.FaceUserType = model.FaceUserType;
                        dbmodel.Title = model.Title;
                        dbmodel.TargetType = model.TargetType;
                        dbmodel.Link = model.Link;
                        dbmodel.Content = model.Content;
                        dbmodel.StartTime = model.StartTime == null ? 0 : model.StartTime.Value.ToTimeStamp();
                        dbmodel.EndTime = model.EndTime == null ? 0 : model.EndTime.Value.ToTimeStamp();
                        dbmodel.IsEnable = model.IsEnable;
                        await _databaseFixture.Db.OaMessage.UpdateAsync(dbmodel, tran);

                        //删除
                        var dbmsgUsers = await _databaseFixture.Db.OaMessageUser.FindAllAsync(m => m.MessageId == dbmodel.Id);
                        foreach (var item in dbmsgUsers)
                        {
                            await _databaseFixture.Db.OaMessageUser.DeleteAsync(item, tran);
                        }

                        //新增
                        if (model.UserIds.HasItems())
                        {
                            List<OaMessageUser> msgUsers = new List<OaMessageUser>();
                            foreach (var item in model.UserIds)
                            {
                                msgUsers.Add(new OaMessageUser
                                {
                                    MessageId = dbmodel.Id,
                                    UserId = item
                                });
                            }
                            await _databaseFixture.Db.OaMessageUser.BulkInsertAsync(msgUsers, tran);
                        }

                        tran.Commit();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    return false;
                }
            }
        }

        public async Task<List<OaMessage>> EnableMessageAsync(List<long> ids)
        {
            using (var tran = _databaseFixture.Db.BeginTransaction())
            {
                try
                {
                    var dbmsg = await _databaseFixture.Db.OaMessage.FindAllAsync(m => ids.Contains(m.Id));

                    foreach (var item in dbmsg)
                    {
                        item.IsEnable = 1;
                        if (item.StartTime == 0 || item.EndTime == 0)
                        {
                            item.IsExecuted = 1;
                        }
                    }
                    await _databaseFixture.Db.OaMessage.BulkUpdateAsync(dbmsg, tran);
                    tran.Commit();
                    return dbmsg.ToList();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    return null;
                }
            }
        }

        public async Task<Page<OaMessageMyList>> MyListAsync(OaMessageMyListSearch search)
        {
            return await _databaseFixture.Db.OaMessage.MyListAsync(search);
        }

        public async Task<OaMessageMyListDetail> MyListDetailAsync(long id, long userid)
        {
            using (var tran = _databaseFixture.Db.BeginTransaction())
            {
                try
                {
                    var dbmessage = await _databaseFixture.Db.OaMessage.FindByIdAsync(id);
                    if (dbmessage == null)
                    {
                        return null;
                    }
                    //判断是否存在
                    var dbread = await _databaseFixture.Db.OaMessageUserRead.FindAsync(m => m.MessageId == id && m.UserId == userid);
                    if (dbread == null)
                    {
                        OaMessageUserRead userRead = new OaMessageUserRead
                        {
                            MessageId = dbmessage.Id,
                            UserId = userid
                        };
                        await _databaseFixture.Db.OaMessageUserRead.InsertAsync(userRead, tran);
                    }
                    tran.Commit();
                    return new OaMessageMyListDetail
                    {
                        Id = dbmessage.Id,
                        Title = dbmessage.Title,
                        Content = dbmessage.Content,
                        CreateTime = dbmessage.CreateTime
                    };
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    return null;
                }
            }
        }
    }
}
