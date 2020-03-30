using DotNetCore.CAP;
using JadeFramework.Core.Domain.Entities;
using JadeFramework.Core.Extensions;
using JadeFramework.WorkFlow;
using MsSystem.WF.IRepository;
using MsSystem.WF.IService;
using MsSystem.WF.Model;
using MsSystem.WF.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MsSystem.WF.Service
{
    public class WorkFlowInstanceService : IWorkFlowInstanceService
    {
        private readonly IWFDatabaseFixture databaseFixture;
        private readonly IConfigService configService;
        private readonly ICapPublisher capPublisher;

        public WorkFlowInstanceService(IWFDatabaseFixture databaseFixture, IConfigService configService, ICapPublisher capPublisher)
        {
            this.databaseFixture = databaseFixture;
            this.configService = configService;
            this.capPublisher = capPublisher;
        }

        /// <summary>
        /// 获取流程发起人
        /// </summary>
        /// <param name="node"></param>
        /// <param name="userid">当前人</param>
        /// <param name="optionParams"></param>
        /// <returns></returns>
        private async Task<string> GetMakerListAsync(FlowNode node, string userid, Dictionary<string, object> optionParams)
        {
            if (node.SetInfo == null)
            {
                return "";
            }
            switch (node.SetInfo.NodeDesignate)
            {
                case FlowNodeSetInfo.SPECIAL_USER:
                    {
                        string res = string.Join(",", node.SetInfo.Nodedesignatedata.Users);
                        return res.IsNullOrEmpty() ? res : res + ",";
                    }
                case FlowNodeSetInfo.SPECIAL_ROLE:
                    {
                        var userids = await configService.GetUserIdsByRoleIdsAsync(node.SetInfo.Nodedesignatedata.Roles.Select(x => Convert.ToInt64(x)).ToList());
                        string res = string.Join(",", userids);
                        return res.IsNullOrEmpty() ? res : res + ",";
                    }
                case FlowNodeSetInfo.SQL:
                    {
                        string idsql = node.SetInfo.Nodedesignatedata.SQL;
                        var array = idsql.Split('_');
                        if (array.Length >= 2)
                        {
                            string sysname = array[0].ToLower();//sys oa  wf weixin
                            var resids = await configService.GetFlowNodeInfo(sysname, new FlowViewModel
                            {
                                sql = idsql,
                                param = optionParams,
                                UserId = userid
                            });
                            if (!resids.HasItems())
                            {
                                return null;
                            }
                            string res = string.Join(",", resids);
                            return res.IsNullOrEmpty() ? res : res + ",";
                        }
                        else
                        {
                            throw new Exception("无法判断要访问哪个系统！");
                        }
                    }
                case FlowNodeSetInfo.ALL_USER:
                    return "0";
                default:
                    return null;
            }
        }

        /// <summary>
        /// 我的待办
        /// </summary>
        /// <param name="searchDto"></param>
        /// <returns></returns>
        public async Task<Page<UserWorkFlowDto>> GetUserTodoListAsync(WorkFlowTodoSearchDto searchDto)
        {
            return await databaseFixture.Db.WorkflowInstance.GetUserTodoListAsync(searchDto);
        }

        /// <summary>
        /// 获取用户流程操作历史记录
        /// </summary>
        /// <param name="searchDto"></param>
        /// <returns></returns>
        public async Task<Page<WorkFlowOperationHistoryDto>> GetUserOperationHistoryAsync(WorkFlowOperationHistorySearchDto searchDto)
        {
            return await databaseFixture.Db.WorkflowInstance.GetUserOperationHistoryAsync(searchDto);
        }

        /// <summary>
        /// 获取用户发起的流程
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<Page<UserWorkFlowDto>> GetUserWorkFlowPageAsync(int pageIndex, int pageSize, string userId)
        {
            return await databaseFixture.Db.WorkflowInstance.GetUserWorkFlowPageAsync(pageIndex, pageSize, userId);
        }

        /// <summary>
        /// 获取我的审批历史记录
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<Page<UserWorkFlowDto>> GetMyApprovalHistoryAsync(int pageIndex, int pageSize, string userId)
        {
            return await databaseFixture.Db.WorkflowInstance.GetMyApprovalHistoryAsync(pageIndex, pageSize, userId);
        }


        /// <summary>
        /// CAP发布订阅
        /// </summary>
        /// <param name="statusChange"></param>
        /// <param name="flowStatus">要改变成的状态</param>
        /// <returns></returns>
        private async Task FlowStatusChangePublisher(WorkFlowStatusChange statusChange, WorkFlowStatus flowStatus)
        {
            if (statusChange != null)
            {
                statusChange.Status = flowStatus;
                statusChange.FlowTime = DateTime.Now.ToTimeStamp();
                await capPublisher.PublishAsync(statusChange.TargetName, statusChange);
            }
        }

        #region 流程过程流转处理

        /// <summary>
        /// 添加或修改自定义表单数据
        /// </summary>
        /// <param name="addProcess"></param>
        /// <returns></returns>
        public async Task<WorkFlowResult> AddOrUpdateCustomFlowFormAsync(WorkFlowProcess addProcess)
        {
            using (var tran = databaseFixture.Db.BeginTransaction())
            {
                try
                {
                    var dbflow = await databaseFixture.Db.Workflow.FindByIdAsync(addProcess.FlowId);
                    if (addProcess.InstanceId == default(Guid))
                    {
                        WfWorkflowInstance workflowInstance = new WfWorkflowInstance
                        {
                            InstanceId = Guid.NewGuid(),
                            FlowId = dbflow.FlowId,
                            Code = DateTime.Now.ToTimeStamp() + string.Empty.CreateNumberNonce(),
                            CreateUserId = addProcess.UserId,
                            CreateUserName = addProcess.UserName,
                            FlowContent = dbflow.FlowContent,
                            IsFinish = null,
                            Status = (int)WorkFlowStatus.UnSubmit,
                            UpdateTime = DateTime.Now.ToTimeStamp()
                        };
                        await databaseFixture.Db.WorkflowInstance.InsertAsync(workflowInstance, tran);
                        addProcess.InstanceId = workflowInstance.InstanceId;

                        //表单关联记录创建
                        var dbform = await databaseFixture.Db.WorkflowForm.FindByIdAsync(addProcess.FormId);
                        WfWorkflowInstanceForm instanceForm = new WfWorkflowInstanceForm
                        {
                            Id = Guid.NewGuid(),
                            CreateUserId = addProcess.UserId,
                            FormContent = dbform.Content,
                            FormData = addProcess.FormData,
                            InstanceId = addProcess.InstanceId,
                            FormId = dbform.FormId,
                            FormType = dbform.FormType,
                            FormUrl = null,
                            CreateTime = DateTime.Now.ToTimeStamp(),
                        };
                        await databaseFixture.Db.WorkflowInstanceForm.InsertAsync(instanceForm, tran);
                    }
                    else
                    {
                        //实例不再创建
                        //表单关联记录修改
                        var dbinstanceForm = await databaseFixture.Db.WorkflowInstanceForm.FindAsync(m => m.InstanceId == addProcess.InstanceId);
                        dbinstanceForm.FormData = addProcess.FormData;
                        await databaseFixture.Db.WorkflowInstanceForm.UpdateAsync(dbinstanceForm, tran);
                    }

                    tran.Commit();
                    return WorkFlowResult.Success("提交成功", data: addProcess.InstanceId);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    return WorkFlowResult.Error("提交失败");
                }
            }
        }

        /// <summary>
        /// 创建实例
        /// 注意事项：
        /// 1、流程开始节点不可添加任何条件分支（不符合逻辑，故人为规定）,即开始节点之后必须只能有一个任务节点，否则整个逻辑就错误了
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<WorkFlowResult> CreateInstanceAsync(WorkFlowProcessTransition model)
        {
            using (var tran = databaseFixture.Db.BeginTransaction())
            {
                try
                {
                    WfWorkflow dbflow = await databaseFixture.Db.Workflow.FindByIdAsync(model.FlowId);
                    MsWorkFlowContext context = new MsWorkFlowContext(new JadeFramework.WorkFlow.WorkFlow
                    {
                        FlowId = dbflow.FlowId,
                        FlowJSON = dbflow.FlowContent,
                        ActivityNodeId = default(Guid)
                    });

                    #region 创建/修改实例
                    WfWorkflowInstance workflowInstance;
                    if (model.InstanceId == default(Guid))
                    {
                        workflowInstance = new WfWorkflowInstance
                        {
                            InstanceId = Guid.NewGuid(),
                            FlowId = context.WorkFlow.FlowId,
                            Code = DateTime.Now.ToTimeStamp() + string.Empty.CreateNumberNonce(),
                            ActivityId = context.WorkFlow.NextNodeId,
                            ActivityName = context.WorkFlow.NextNode.Name,
                            ActivityType = (int)context.WorkFlow.NextNodeType,
                            PreviousId = context.WorkFlow.ActivityNodeId,
                            MakerList = await this.GetMakerListAsync(context.WorkFlow.Nodes[context.WorkFlow.NextNodeId], model.UserId, model.OptionParams),
                            CreateUserId = model.UserId,
                            CreateUserName = model.UserName,
                            FlowContent = dbflow.FlowContent,
                            IsFinish = context.WorkFlow.NextNodeType.ToIsFinish(),
                            Status = (int)WorkFlowStatus.Running,
                            UpdateTime = DateTime.Now.ToTimeStamp(),
                            FlowVersion= dbflow.FlowVersion
                        };
                        await databaseFixture.Db.WorkflowInstance.InsertAsync(workflowInstance, tran);
                    }
                    else
                    {
                        workflowInstance = await databaseFixture.Db.WorkflowInstance.FindByIdAsync(model.InstanceId);
                        workflowInstance.ActivityId = context.WorkFlow.NextNodeId;
                        workflowInstance.ActivityName = context.WorkFlow.NextNode.Name;
                        workflowInstance.ActivityType = (int)context.WorkFlow.NextNodeType;
                        workflowInstance.PreviousId = context.WorkFlow.ActivityNodeId;
                        workflowInstance.MakerList = await this.GetMakerListAsync(context.WorkFlow.Nodes[context.WorkFlow.NextNodeId], model.UserId, model.OptionParams);
                        workflowInstance.FlowContent = dbflow.FlowContent;
                        workflowInstance.IsFinish = context.WorkFlow.NextNodeType.ToIsFinish();
                        workflowInstance.Status = (int)WorkFlowStatus.Running;
                        workflowInstance.UpdateTime = DateTime.Now.ToTimeStamp();
                        await databaseFixture.Db.WorkflowInstance.UpdateAsync(workflowInstance, tran);
                    }


                    #endregion

                    #region 创建流程实例表单关联记录

                    var dbform = await databaseFixture.Db.WorkflowForm.FindByIdAsync(dbflow.FormId);
                    if ((WorkFlowFormType)dbform.FormType == WorkFlowFormType.System)
                    {
                        WfWorkflowInstanceForm instanceForm = new WfWorkflowInstanceForm
                        {
                            Id = Guid.NewGuid(),
                            CreateUserId = model.UserId,
                            FormContent = model.StatusChange.KeyValue,//保存对应表单主键
                            FormData = model.StatusChange.KeyValue,
                            InstanceId = workflowInstance.InstanceId,
                            FormId = dbform.FormId,
                            FormType = dbform.FormType,
                            FormUrl = dbform.FormUrl
                        };
                        await databaseFixture.Db.WorkflowInstanceForm.InsertAsync(instanceForm, tran);
                    }
                    else
                    {
                        //强制修改为null
                        model.StatusChange = null;
                    }

                    #endregion

                    #region 创建流程操作记录

                    WfWorkflowOperationHistory operationHistory = new WfWorkflowOperationHistory
                    {
                        OperationId = Guid.NewGuid(),
                        InstanceId = workflowInstance.InstanceId,
                        CreateUserId = model.UserId,
                        CreateUserName = model.UserName,
                        Content = "提交流程",
                        NodeName = context.WorkFlow.ActivityNode.Name,
                        NodeId = context.WorkFlow.ActivityNodeId,
                        TransitionType = (int)WorkFlowMenu.Submit
                    };
                    await databaseFixture.Db.WorkflowOperationHistory.InsertAsync(operationHistory, tran);
                    #endregion

                    #region 创建流程流转记录

                    WfWorkflowTransitionHistory transitionHistory = new WfWorkflowTransitionHistory
                    {
                        TransitionId = Guid.NewGuid(),
                        InstanceId = workflowInstance.InstanceId,
                        FromNodeId = context.WorkFlow.ActivityNodeId,
                        FromNodeType = (int)context.WorkFlow.ActivityNodeType,
                        FromNodName = context.WorkFlow.ActivityNode.Name,
                        ToNodeId = context.WorkFlow.NextNodeId,
                        ToNodeType = (int)context.WorkFlow.NextNodeType,
                        ToNodeName = context.WorkFlow.NextNode.Name,
                        CreateUserId = model.UserId,
                        CreateUserName = model.UserName,
                        TransitionState = (int)WorkFlowTransitionStateType.Normal,
                        IsFinish = context.WorkFlow.NextNodeType.ToIsFinish(),
                    };
                    await databaseFixture.Db.WorkflowTransitionHistory.InsertAsync(transitionHistory, tran);
                    #endregion

                    //改变表单状态
                    await FlowStatusChangePublisher(model.StatusChange, WorkFlowStatus.Running);

                    tran.Commit();
                    return WorkFlowResult.Success();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    return WorkFlowResult.Error(ex.Message);
                }
            }
        }

        /// <summary>
        /// 获取执行过的节点
        /// </summary>
        /// <param name="instanceid"></param>
        /// <param name="currentNodeId"></param>
        /// <returns></returns>
        private async Task<List<FlowNode>> GetExcuteNodes(Guid instanceid, Guid currentNodeId)
        {
            var operationHis = await databaseFixture.Db.WorkflowOperationHistory.FindAllAsync(m => m.InstanceId == instanceid);
            var list = operationHis.Where(m => m.NodeId != currentNodeId && (m.TransitionType == (int)WorkFlowMenu.Agree || m.TransitionType == (int)WorkFlowMenu.Submit))
                .OrderBy(m => m.CreateTime);
            List<FlowNode> nodes = new List<FlowNode>();
            foreach (var item in list)
            {
                if (item.TransitionType == (int)WorkFlowMenu.Back)//当循环到Back节点时候，后面节点不在循环
                {
                    break;
                }
                else
                {
                    if (!nodes.Any(m => m.Id == item.NodeId))
                    {
                        nodes.Add(new FlowNode
                        {
                            Id = item.NodeId,
                            Name = item.NodeName
                        });
                    }
                }
            }
            return nodes;
        }

        /// <summary>
        /// 获取工作流进程信息
        /// </summary>
        /// <param name="process"></param>
        /// <returns></returns>
        public async Task<WorkFlowProcess> GetProcessAsync(WorkFlowProcess process)
        {
            WorkFlowProcess model = new WorkFlowProcess
            {
                InstanceId = process.InstanceId
            };
            var dbflow = await databaseFixture.Db.Workflow.FindByIdAsync(process.FlowId);
            model.FlowId = dbflow.FlowId;
            model.FlowName = dbflow.FlowName;
            model.FormId = dbflow.FormId;
            if (process.InstanceId == default(Guid))//流程刚开始
            {
                var dbform = await databaseFixture.Db.WorkflowForm.FindByIdAsync(dbflow.FormId);
                model.FormType = (WorkFlowFormType)dbform.FormType;
                model.FormContent = dbform.Content;
                model.FormUrl = dbform.FormUrl;
                model.FormData = null;
                model.Menus = new List<int>
                {
                    (int)WorkFlowMenu.Submit,
                    (int)WorkFlowMenu.FlowImage,
                    (int)WorkFlowMenu.Save,
                    (int)WorkFlowMenu.Return,
                };
                model.FlowData = new WorkFlowProcessFlowData
                {
                    IsFinish = null,
                    Status = (int)WorkFlowStatus.UnSubmit
                };
            }
            else
            {
                var instanceform = await databaseFixture.Db.WorkflowInstanceForm.FindAsync(m => m.InstanceId == process.InstanceId);
                model.FormType = (WorkFlowFormType)instanceform.FormType;
                model.FormContent = instanceform.FormContent;
                model.FormUrl = instanceform.FormUrl;
                model.FormData = instanceform.FormData;

                var flowinstance = await databaseFixture.Db.WorkflowInstance.FindByIdAsync(process.InstanceId);
                model.FlowData = new WorkFlowProcessFlowData
                {
                    IsFinish = flowinstance.IsFinish,
                    Status = flowinstance.Status
                };
                if (flowinstance.IsFinish == null && model.FormType == WorkFlowFormType.Custom)//表示自定义表单刚保存情况
                {
                    model.Menus = new List<int>
                    {
                        (int)WorkFlowMenu.Submit,
                        (int)WorkFlowMenu.FlowImage,
                        (int)WorkFlowMenu.Save,
                        (int)WorkFlowMenu.Return
                    };
                    return model;
                }
                if (flowinstance.IsFinish == (int)WorkFlowInstanceStatus.Finish) //流程结束情况
                {
                    model.Menus = new List<int>();
                    //流程打印按钮显示判断
                    if (process.UserId == flowinstance.CreateUserId)//流程通过并且是当前人查看才显示打印按钮
                    {
                        model.Menus.Add((int)WorkFlowMenu.Print);
                    }
                    //已阅按钮显示判断
                    var dbnotices = await databaseFixture.Db.WfWorkflowNotice.FindAllAsync(m => m.Maker == process.UserId && m.InstanceId == process.InstanceId && m.IsTransition == 1 && m.IsRead == 0 && m.Status == 1);
                    if (dbnotices.Any())
                    {
                        model.Menus.Add((int)WorkFlowMenu.View);
                    }
                    model.Menus.Add((int)WorkFlowMenu.Approval);
                    model.Menus.Add((int)WorkFlowMenu.FlowImage);
                    model.Menus.Add((int)WorkFlowMenu.Return);
                    return model;
                }

                //============= 流程运行中情况判断 =============//

                //根据当前人获取可操作的按钮
                //获取下一步的执行人
                MsWorkFlowContext context = new MsWorkFlowContext(new JadeFramework.WorkFlow.WorkFlow
                {
                    FlowId = dbflow.FlowId,
                    FlowJSON = flowinstance.FlowContent,
                    ActivityNodeId = flowinstance.ActivityId
                });
                model.FlowData.CurrentNode = context.WorkFlow.ActivityNode;
                if (context.WorkFlow.ActivityNode.Type == FlowNode.START)//节点退回到开始节点情况
                {
                    var dbinstance = await databaseFixture.Db.WorkflowInstance.FindByIdAsync(process.InstanceId);
                    if (dbinstance.CreateUserId == process.UserId)
                    {
                        model.Menus = new List<int>
                        {
                            (int)WorkFlowMenu.ReSubmit,
                            (int)WorkFlowMenu.Approval,
                            (int)WorkFlowMenu.FlowImage,
                            (int)WorkFlowMenu.Save,
                            (int)WorkFlowMenu.Return,
                        };
                    }
                    else
                    {
                        model.Menus = new List<int>
                        {
                            (int)WorkFlowMenu.Approval,
                            (int)WorkFlowMenu.FlowImage,
                            (int)WorkFlowMenu.Return,
                        };
                    }
                    return model;
                }
                else
                {
                    if (!string.IsNullOrEmpty(flowinstance.MakerList))
                    {
                        if (flowinstance.MakerList.Trim() == "0")//全部人员（实际上不允许存在，因为没有实际意义，但是还是实现了）
                        {
                            model.Menus = new List<int>
                            {
                                (int)WorkFlowMenu.Agree,
                                (int)WorkFlowMenu.Deprecate,
                                (int)WorkFlowMenu.Back,
                            };
                        }
                        else
                        {
                            List<long> userIds = flowinstance.MakerList.Split(',').Where(x => !string.IsNullOrEmpty(x)).Select(x => Convert.ToInt64(x)).ToList();
                            if (userIds.Contains(process.UserId.ToInt64()))
                            {
                                model.Menus = new List<int>
                                {
                                    (int)WorkFlowMenu.Agree,
                                    (int)WorkFlowMenu.Deprecate,
                                    (int)WorkFlowMenu.Back,
                                };
                                //获取执行过的节点
                                model.ExecutedNode = await GetExcuteNodes(process.InstanceId, flowinstance.ActivityId);
                            }
                        }
                        if (model.Menus == null)
                        {
                            model.Menus = new List<int>();
                        }
                        //已阅按钮显示判断
                        var dbnotices = await databaseFixture.Db.WfWorkflowNotice.FindAllAsync(m => m.Maker == model.UserId && m.InstanceId == model.InstanceId && m.IsTransition == 1 && m.IsRead == 0 && m.Status == 1);
                        if (dbnotices.Any())
                        {
                            model.Menus.Add((int)WorkFlowMenu.View);
                        }

                        //委托按钮显示判断
                        if (await CanAssign(process, flowinstance.MakerList))
                        {
                            model.Menus.Add((int)WorkFlowMenu.Assign);
                        }

                        //终止按钮显示判断
                        var prenode = context.GetLinesForFrom(flowinstance.ActivityId);
                        if (prenode.Count == 1)
                        {
                            var nodeType = context.GetNodeType(prenode[0].From);
                            if (nodeType == WorkFlowInstanceNodeType.BeginRound && process.UserId == flowinstance.CreateUserId)
                            {
                                model.Menus.Add((int)WorkFlowMenu.Withdraw);
                            }
                        }
                        model.Menus.Add((int)WorkFlowMenu.Approval);
                        model.Menus.Add((int)WorkFlowMenu.FlowImage);
                        model.Menus.Add((int)WorkFlowMenu.Return);
                    }
                    else
                    {
                        model.Menus = new List<int>
                        {
                            (int)WorkFlowMenu.Approval,
                            (int)WorkFlowMenu.FlowImage,
                            (int)WorkFlowMenu.Return
                        };
                    }
                }
            }
            return model;
        }

        /// <summary>
        /// 判断当前用户是否能显示委托操作按钮
        /// </summary>
        /// <param name="process"></param>
        /// <returns></returns>
        private async Task<bool> CanAssign(WorkFlowProcess process,string makerlist)
        {
            string str = process.UserId + ",";
            if (!makerlist.Contains(str))//当前人非执行人员
            {
                return false;
            }
            /// 将自己审批某个流程的权限赋予其他人，让其他用户代审批流程;
            /// 规则：A委托给B，A不能再审批且不能多次委托，B可再次委托给C，同理A
            var dbassigns = await databaseFixture.Db.WfWorkflowAssign.FindAllAsync(m => m.InstanceId == process.InstanceId && m.FlowId == process.FlowId);
            if (dbassigns.Any(m => m.UserId == process.UserId))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 系统定制流程获取
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<WorkFlowProcess> GetProcessForSystemAsync(SystemFlowDto model)
        {
            WorkFlowProcess process = new WorkFlowProcess
            {
                UserId = model.UserId
            };
            var dbflowform = await databaseFixture.Db.WorkflowForm.FindAsync(m => m.FormUrl == model.FormUrl);
            var dbflow = await databaseFixture.Db.Workflow.FindAsync(m => m.FormId == dbflowform.FormId);
            process.FlowId = dbflow.FlowId;
            process.FlowName = dbflow.FlowName;
            process.FormId = dbflow.FormId;
            if (model.PageId.IsNullOrEmpty())
            {
                process.InstanceId = default(Guid);
            }
            else
            {
                var instanceform = await databaseFixture.Db.WorkflowInstanceForm.FindAsync(m => m.FormId == dbflowform.FormId && m.FormContent == model.PageId);
                process.InstanceId = instanceform != null ? instanceform.InstanceId : default(Guid);
            }
            return await GetProcessAsync(process);
        }

        /// <summary>
        /// 流程过程流转处理
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<WorkFlowResult> ProcessTransitionFlowAsync(WorkFlowProcessTransition model)
        {
            WorkFlowResult result = new WorkFlowResult();
            switch (model.MenuType)
            {
                case WorkFlowMenu.ReSubmit:
                    result = await ProcessTransitionReSubmitAsync(model);
                    break;
                case WorkFlowMenu.Agree:
                    result = await ProcessTransitionAgreeAsync(model);
                    break;
                case WorkFlowMenu.Deprecate:
                    result = await ProcessTransitionDeprecateAsync(model);
                    break;
                case WorkFlowMenu.Back:
                    result = await ProcessTransitionBackAsync(model);
                    break;
                case WorkFlowMenu.Withdraw:
                    result = await ProcessTransitionWithdrawAsync(model);
                    break;
                case WorkFlowMenu.View:
                    result = await ProcessTransitionViewAsync(model);
                    break;
                case WorkFlowMenu.Stop:
                    break;
                case WorkFlowMenu.Cancel:
                    break;
                case WorkFlowMenu.Throgh:
                    break;
                case WorkFlowMenu.Assign:
                    result = await ProcessTransitionAssignAsync(model);
                    break;
                case WorkFlowMenu.CC:
                    break;
                case WorkFlowMenu.Suspend:
                    break;
                case WorkFlowMenu.Resume:
                    break;
                case WorkFlowMenu.Save:
                case WorkFlowMenu.Submit:
                case WorkFlowMenu.Return:
                case WorkFlowMenu.Approval:
                case WorkFlowMenu.FlowImage:
                default:
                    result = WorkFlowResult.Error("未找到匹配按钮！");
                    break;
            }
            return result;
        }

        /// <summary>
        /// 计算票数
        /// </summary>
        /// <param name="InstanceId"></param>
        /// <param name="nodeId"></param>
        /// <param name="node"></param>
        /// <param name="chatParallelCalcType"></param>
        /// <returns></returns>
        private async Task<WorkFlowInstanceStatus> CalcVotes(Guid InstanceId, Guid nodeId, FlowNode node, ChatParallelCalcType chatParallelCalcType)
        {
            var dboperhis = await databaseFixture.Db.WorkflowOperationHistory.FindAllAsync(m => m.InstanceId == InstanceId && m.NodeId == nodeId);
            bool result;
            switch (chatParallelCalcType)
            {
                case ChatParallelCalcType.MoreThenHalf:
                    result = dboperhis.Count(m => m.TransitionType == (int)WorkFlowMenu.Agree) > (dboperhis.Count() / 2);
                    break;
                case ChatParallelCalcType.OneHundredPercent:
                default:
                    result = dboperhis.Count(m => m.TransitionType == (int)WorkFlowMenu.Agree) == dboperhis.Count();
                    break;
            }
            if (node.NodeType() == WorkFlowInstanceNodeType.EndRound)
            {
                return WorkFlowInstanceStatus.Finish;
            }
            else
            {
                return WorkFlowInstanceStatus.Running;
            }
        }

        /// <summary>
        /// 会签节点逻辑
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="context"></param>
        /// <param name="dbflowinstance"></param>
        /// <param name="model"></param>
        /// <param name="flowInstanceStatus"></param>
        /// <returns></returns>
        private async Task ChatLogic(IDbTransaction tran, MsWorkFlowContext context, WfWorkflowInstance dbflowinstance, WorkFlowProcessTransition model, WorkFlowInstanceStatus flowInstanceStatus)
        {
            //并行逻辑
            if (context.WorkFlow.ActivityNode.SetInfo.ChatData.ChatType == ChatType.Parallel)
            {
                var makerUsers = dbflowinstance.MakerList.Split(',').Where(m => !string.IsNullOrEmpty(m)).ToList();
                WfWorkflowTransitionHistory transitionHistory = new WfWorkflowTransitionHistory
                {
                    TransitionId = Guid.NewGuid(),
                    InstanceId = dbflowinstance.InstanceId,
                    CreateUserId = model.UserId,
                    CreateUserName = model.UserName,
                    TransitionState = (int)WorkFlowTransitionStateType.Normal,
                    IsFinish = (int)flowInstanceStatus,
                    FromNodeId = context.WorkFlow.ActivityNodeId,
                    FromNodName = context.WorkFlow.ActivityNode.Name,
                    FromNodeType = (int)context.WorkFlow.ActivityNodeType,
                    ToNodeId = context.WorkFlow.ActivityNodeId,
                    ToNodeName = context.WorkFlow.ActivityNode.Name,
                    ToNodeType = (int)context.WorkFlow.ActivityNodeType,
                };
                await databaseFixture.Db.WorkflowTransitionHistory.InsertAsync(transitionHistory, tran);
                if (makerUsers.Count == 1)//当前人是最后一人
                {
                    var line = context.WorkFlow.Lines[dbflowinstance.ActivityId][0];
                    var nextNode = context.WorkFlow.Nodes[line.To];

                    WfWorkflowTransitionHistory transitionHistoryEnd = new WfWorkflowTransitionHistory
                    {
                        TransitionId = Guid.NewGuid(),
                        InstanceId = dbflowinstance.InstanceId,
                        FromNodeId = context.WorkFlow.ActivityNodeId,
                        FromNodName = context.WorkFlow.ActivityNode.Name,
                        FromNodeType = (int)context.WorkFlow.ActivityNodeType,
                        ToNodeId = nextNode.Id,
                        ToNodeType = (int)nextNode.NodeType(),
                        ToNodeName = nextNode.Name,
                        TransitionState = (int)WorkFlowTransitionStateType.Normal,
                        IsFinish = nextNode.NodeType().ToIsFinish(),
                        CreateUserId = model.UserId,
                        CreateUserName = model.UserName
                    };
                    await databaseFixture.Db.WorkflowTransitionHistory.InsertAsync(transitionHistoryEnd, tran);

                    //修改流程实例
                    dbflowinstance.PreviousId = dbflowinstance.ActivityId;
                    dbflowinstance.ActivityId = nextNode.Id;
                    dbflowinstance.ActivityName = nextNode.Name;
                    dbflowinstance.ActivityType = (int)nextNode.NodeType();
                    dbflowinstance.MakerList = nextNode.NodeType() == WorkFlowInstanceNodeType.EndRound
                        ? ""
                        : await this.GetMakerListAsync(nextNode, model.UserId, model.OptionParams);
                    //计算票数
                    var result = await CalcVotes(dbflowinstance.InstanceId, dbflowinstance.PreviousId, nextNode, context.WorkFlow.ActivityNode.SetInfo.ChatData.ParallelCalcType);
                    dbflowinstance.IsFinish = (int)result;
                    await databaseFixture.Db.WorkflowInstance.UpdateAsync(dbflowinstance, tran);
                }
                else
                {
                    makerUsers.Remove(model.UserId);
                    dbflowinstance.MakerList = string.Join(",", makerUsers) + ",";
                    await databaseFixture.Db.WorkflowInstance.UpdateAsync(dbflowinstance, tran);
                }
            }
            //串行逻辑
            else
            {
                var users = context.WorkFlow.ActivityNode.SetInfo.Nodedesignatedata.Users;
                int index = 0;
                for (int i = 0; i < users.Length; i++)
                {
                    if (users[i] == model.UserId)
                    {
                        index = i + 1;
                        break;
                    }
                }
                string nextUserId = users.Length == index ? "" : users[index];
                WfWorkflowTransitionHistory transitionHistory = new WfWorkflowTransitionHistory
                {
                    TransitionId = Guid.NewGuid(),
                    InstanceId = dbflowinstance.InstanceId,
                    CreateUserId = model.UserId,
                    CreateUserName = model.UserName,
                    TransitionState = (int)WorkFlowTransitionStateType.Normal,
                    IsFinish = (int)flowInstanceStatus,
                    FromNodeId = context.WorkFlow.ActivityNodeId,
                    FromNodName = context.WorkFlow.ActivityNode.Name,
                    FromNodeType = (int)context.WorkFlow.ActivityNodeType,
                    ToNodeId = context.WorkFlow.ActivityNodeId,
                    ToNodeName = context.WorkFlow.ActivityNode.Name,
                    ToNodeType = (int)context.WorkFlow.ActivityNodeType,
                };
                await databaseFixture.Db.WorkflowTransitionHistory.InsertAsync(transitionHistory, tran);
                if (users.Length == index)//最后一个人时候
                {
                    var line = context.WorkFlow.Lines[dbflowinstance.ActivityId][0];
                    var nextNode = context.WorkFlow.Nodes[line.To];
                    WfWorkflowTransitionHistory transitionHistoryEnd = new WfWorkflowTransitionHistory
                    {
                        TransitionId = Guid.NewGuid(),
                        InstanceId = dbflowinstance.InstanceId,
                        FromNodeId = context.WorkFlow.ActivityNodeId,
                        FromNodName = context.WorkFlow.ActivityNode.Name,
                        FromNodeType = (int)context.WorkFlow.ActivityNodeType,
                        ToNodeId = nextNode.Id,
                        ToNodeType = (int)nextNode.NodeType(),
                        ToNodeName = nextNode.Name,
                        TransitionState = (int)WorkFlowTransitionStateType.Normal,
                        IsFinish = nextNode.NodeType().ToIsFinish(),
                        CreateUserId = model.UserId,
                        CreateUserName = model.UserName
                    };
                    await databaseFixture.Db.WorkflowTransitionHistory.InsertAsync(transitionHistoryEnd, tran);
                    //修改流程实例
                    dbflowinstance.PreviousId = dbflowinstance.ActivityId;
                    dbflowinstance.ActivityId = nextNode.Id;
                    dbflowinstance.ActivityName = nextNode.Name;
                    dbflowinstance.ActivityType = (int)nextNode.NodeType();
                    dbflowinstance.MakerList = nextNode.NodeType() == WorkFlowInstanceNodeType.EndRound
                        ? ""
                        : await this.GetMakerListAsync(nextNode, model.UserId, model.OptionParams);
                    //计算票数
                    var result = await CalcVotes(dbflowinstance.InstanceId, dbflowinstance.PreviousId, nextNode, context.WorkFlow.ActivityNode.SetInfo.ChatData.ParallelCalcType);
                    dbflowinstance.IsFinish = (int)result;
                    await databaseFixture.Db.WorkflowInstance.UpdateAsync(dbflowinstance, tran);
                }
            }
        }

        /// <summary>
        /// 下个节点是会签逻辑
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="context"></param>
        /// <param name="dbflowinstance"></param>
        /// <param name="flowInstanceStatus"></param>
        /// <returns></returns>
        private async Task NextChatLogic(IDbTransaction tran, MsWorkFlowContext context, WfWorkflowInstance dbflowinstance, WorkFlowInstanceStatus flowInstanceStatus)
        {
            dbflowinstance.IsFinish = (int)flowInstanceStatus;
            if (context.WorkFlow.NextNode.SetInfo.ChatData.ChatType == ChatType.Parallel)
            {
                //并行会签
                dbflowinstance.MakerList = string.Join(",", context.WorkFlow.NextNode.SetInfo.Nodedesignatedata.Users);
            }
            else
            {
                //串行会签
                dbflowinstance.MakerList = context.WorkFlow.NextNode.SetInfo.Nodedesignatedata.Users[0];
            }
            dbflowinstance.PreviousId = dbflowinstance.ActivityId;
            dbflowinstance.ActivityId = context.WorkFlow.NextNodeId;
            dbflowinstance.ActivityName = context.WorkFlow.NextNode.Name;
            dbflowinstance.ActivityType = (int)context.WorkFlow.NextNodeType;
            await databaseFixture.Db.WorkflowInstance.UpdateAsync(dbflowinstance, tran);
        }

        /// <summary>
        /// 重新提交流程
        /// 实例只有一次
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        protected async Task<WorkFlowResult> ProcessTransitionReSubmitAsync(WorkFlowProcessTransition model)
        {
            using (var tran = databaseFixture.Db.BeginTransaction())
            {
                try
                {
                    var dbflow = await databaseFixture.Db.Workflow.FindByIdAsync(model.FlowId);
                    MsWorkFlowContext context = new MsWorkFlowContext(new JadeFramework.WorkFlow.WorkFlow
                    {
                        FlowId = dbflow.FlowId,
                        FlowJSON = dbflow.FlowContent,
                        ActivityNodeId = default(Guid)
                    });

                    #region 改变之前实例

                    var dbinstance = await databaseFixture.Db.WorkflowInstance.FindByIdAsync(model.InstanceId);
                    dbinstance.ActivityId = context.WorkFlow.NextNodeId;
                    dbinstance.ActivityName = context.WorkFlow.NextNode.Name;
                    dbinstance.ActivityType = (int)context.WorkFlow.NextNodeType;
                    dbinstance.PreviousId = context.WorkFlow.ActivityNodeId;
                    dbinstance.MakerList = await this.GetMakerListAsync(context.WorkFlow.Nodes[context.WorkFlow.NextNodeId], model.UserId, model.OptionParams);
                    dbinstance.IsFinish = context.WorkFlow.NextNodeType.ToIsFinish();
                    dbinstance.Status = (int)WorkFlowStatus.Running;
                    dbinstance.UpdateTime = DateTime.Now.ToTimeStamp();
                    await databaseFixture.Db.WorkflowInstance.UpdateAsync(dbinstance, tran);

                    #endregion

                    #region 创建流程操作记录

                    WfWorkflowOperationHistory operationHistory = new WfWorkflowOperationHistory
                    {
                        OperationId = Guid.NewGuid(),
                        InstanceId = dbinstance.InstanceId,
                        CreateUserId = model.UserId,
                        CreateUserName = model.UserName,
                        Content = "流程重新提交",
                        NodeName = context.WorkFlow.ActivityNode.Name,
                        NodeId = context.WorkFlow.ActivityNodeId,
                        TransitionType = (int)WorkFlowMenu.Submit
                    };
                    await databaseFixture.Db.WorkflowOperationHistory.InsertAsync(operationHistory, tran);
                    #endregion

                    #region 创建流程流转记录

                    WfWorkflowTransitionHistory transitionHistory = new WfWorkflowTransitionHistory
                    {
                        TransitionId = Guid.NewGuid(),
                        InstanceId = dbinstance.InstanceId,
                        FromNodeId = context.WorkFlow.ActivityNodeId,
                        FromNodeType = (int)context.WorkFlow.ActivityNodeType,
                        FromNodName = context.WorkFlow.ActivityNode.Name,
                        ToNodeId = context.WorkFlow.NextNodeId,
                        ToNodeType = (int)context.WorkFlow.NextNodeType,
                        ToNodeName = context.WorkFlow.NextNode.Name,
                        CreateUserId = model.UserId,
                        CreateUserName = model.UserName,
                        TransitionState = (int)WorkFlowTransitionStateType.Normal,
                        IsFinish = context.WorkFlow.NextNodeType.ToIsFinish(),
                    };
                    await databaseFixture.Db.WorkflowTransitionHistory.InsertAsync(transitionHistory, tran);

                    #endregion

                    //改变表单状态
                    await FlowStatusChangePublisher(model.StatusChange, WorkFlowStatus.Running);

                    tran.Commit();
                    return WorkFlowResult.Success();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    return WorkFlowResult.Error(ex.Message);
                }
            }
        }


        /// <summary>
        /// 获取确定最终要执行的唯一节点
        /// </summary>
        /// <param name="nextLines"></param>
        /// <param name="model"></param>
        /// <param name="createUserId">流程发起人</param>
        /// <returns></returns>
        private async Task<Guid?> GetFinalNodeId(List<FlowLine> nextLines, WorkFlowProcessTransition model,string createUserId)
        {
            var array = nextLines.First().SetInfo.CustomSQL.Split('_');
            if (array.Length < 2)
            {
                throw new Exception("流程设计错误！！！");
            }
            string sysname = array[0];
            //判断是否是工作流内置条件
            Dictionary<Guid, string> condition = nextLines.ToDictionary(m => m.To, n => n.SetInfo.CustomSQL);
            Guid? finalid = null;
            if (sysname.Equals("wf", StringComparison.OrdinalIgnoreCase))
            {
                finalid = await databaseFixture.Db.WfWorkflowsql.GetFinalNodeId(new FlowLineFinalNodeDto
                {
                    Data = condition,
                    Param = model.OptionParams,
                    UserId = createUserId
                });
            }
            else
            {
                finalid = await configService.GetFinalNodeId(sysname, new FlowLineFinalNodeDto
                {
                    Data = condition,
                    Param = model.OptionParams,
                    UserId = createUserId
                });
            }
            if (finalid == null)
            {
                throw new Exception("流程节点最终未找到！！！");
            }
            return finalid;
        }

        /// <summary>
        /// 同意
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        protected async Task<WorkFlowResult> ProcessTransitionAgreeAsync(WorkFlowProcessTransition model)
        {
            using (var tran = databaseFixture.Db.BeginTransaction())
            {
                try
                {
                    WorkFlowStatus publishFlowStatus = WorkFlowStatus.Running;
                    var dbflowinstance = await databaseFixture.Db.WorkflowInstance.FindByIdAsync(model.InstanceId);
                    if (dbflowinstance.IsFinish == (int)WorkFlowInstanceStatus.Finish)
                    {
                        return WorkFlowResult.Error("该流程已经结束！");
                    }
                    MsWorkFlowContext context = new MsWorkFlowContext(new JadeFramework.WorkFlow.WorkFlow
                    {
                        FlowId = model.FlowId,
                        FlowJSON = dbflowinstance.FlowContent,
                        ActivityNodeId = dbflowinstance.ActivityId,
                        PreviousId = dbflowinstance.PreviousId
                    });

                    if (context.WorkFlow.ActivityNode.NodeType() == WorkFlowInstanceNodeType.Normal)
                    {
                        if (context.IsMultipleNextNode())
                        {
                            var nextLines = context.GetLinesForTo(context.WorkFlow.ActivityNodeId);
                            /*
                             * 多条连线条件必须都存在情况判断
                             * 注：一个节点下多个连线，则连线必须有SQL判断
                             * **/
                            bool isOk = nextLines.Any(m => m.SetInfo == null || string.IsNullOrEmpty(m.SetInfo.CustomSQL));
                            if (isOk)
                            {
                                throw new Exception("流程设计错误！！！");
                            }
                            else
                            {
                                //获取确定最终要执行的唯一节点
                                Guid? finalid = await GetFinalNodeId(nextLines, model, dbflowinstance.CreateUserId);
                                FlowNode reallynode = context.WorkFlow.Nodes[finalid.Value];
                                dbflowinstance.IsFinish = reallynode.NodeType().ToIsFinish();
                                if (reallynode.NodeType() == WorkFlowInstanceNodeType.EndRound)
                                {
                                    dbflowinstance.Status = (int)WorkFlowStatus.IsFinish;
                                }
                                else
                                {
                                    dbflowinstance.Status = (int)WorkFlowStatus.Running;
                                }
                                dbflowinstance.ActivityId = reallynode.Id;
                                dbflowinstance.ActivityName = reallynode.Name;
                                dbflowinstance.ActivityType = (int)reallynode.NodeType();
                                dbflowinstance.UpdateTime = DateTime.Now.ToTimeStamp();
                                dbflowinstance.MakerList = reallynode.NodeType() == WorkFlowInstanceNodeType.EndRound ? "" : await this.GetMakerListAsync(reallynode, model.UserId, model.OptionParams);
                                await databaseFixture.Db.WorkflowInstance.UpdateAsync(dbflowinstance, tran);

                                //流程结束情况
                                if ((int)WorkFlowInstanceStatus.Finish == dbflowinstance.IsFinish)
                                {
                                    publishFlowStatus = WorkFlowStatus.IsFinish;
                                }

                                #region 添加流转记录

                                WfWorkflowTransitionHistory transitionHistory = new WfWorkflowTransitionHistory
                                {
                                    TransitionId = Guid.NewGuid(),
                                    InstanceId = dbflowinstance.InstanceId,
                                    FromNodeId = context.WorkFlow.ActivityNodeId,
                                    FromNodName = context.WorkFlow.ActivityNode.Name,
                                    FromNodeType = (int)context.WorkFlow.ActivityNodeType,
                                    ToNodeId = reallynode.Id,
                                    ToNodeType = (int)reallynode.NodeType(),
                                    ToNodeName = reallynode.Name,
                                    TransitionState = (int)WorkFlowTransitionStateType.Normal,
                                    IsFinish = reallynode.NodeType().ToIsFinish(),
                                    CreateUserId = model.UserId,
                                    CreateUserName = model.UserName
                                };
                                await databaseFixture.Db.WorkflowTransitionHistory.InsertAsync(transitionHistory, tran);
                                #endregion

                                #region 通知节点信息添加

                                var viewNodes = context.GetNextNodes(null, WorkFlowInstanceNodeType.ViewNode);
                                await AddFlowNotice(viewNodes, dbflowinstance.CreateUserId, model, tran);

                                #endregion
                            }
                        }
                        else
                        {
                            //下个节点是会签节点
                            if (context.WorkFlow.NextNode.NodeType() != WorkFlowInstanceNodeType.ChatNode)
                            {
                                //修改流程实例
                                dbflowinstance.PreviousId = dbflowinstance.ActivityId;
                                dbflowinstance.ActivityId = context.WorkFlow.NextNodeId;
                                dbflowinstance.ActivityName = context.WorkFlow.NextNode.Name;
                                dbflowinstance.ActivityType = (int)context.WorkFlow.NextNodeType;
                                dbflowinstance.UpdateTime = DateTime.Now.ToTimeStamp();
                                dbflowinstance.MakerList = context.WorkFlow.NextNodeType == WorkFlowInstanceNodeType.EndRound ? "" : await this.GetMakerListAsync(context.WorkFlow.NextNode, model.UserId, model.OptionParams);

                                dbflowinstance.IsFinish = context.WorkFlow.NextNodeType.ToIsFinish();

                                if (context.WorkFlow.NextNodeType == WorkFlowInstanceNodeType.EndRound)
                                {
                                    dbflowinstance.Status = (int)WorkFlowStatus.IsFinish;
                                }
                                else
                                {
                                    dbflowinstance.Status = (int)WorkFlowStatus.Running;
                                }
                                await databaseFixture.Db.WorkflowInstance.UpdateAsync(dbflowinstance, tran);

                                //流程结束情况
                                if ((int)WorkFlowInstanceStatus.Finish == dbflowinstance.IsFinish)
                                {
                                    publishFlowStatus = WorkFlowStatus.IsFinish;
                                }

                                #region 通知节点信息添加

                                var viewNodes = context.GetNextNodes(null, WorkFlowInstanceNodeType.ViewNode);
                                await AddFlowNotice(viewNodes, dbflowinstance.CreateUserId, model, tran);

                                #endregion
                            }
                            else
                            {
                                throw new Exception("当前不支持会签功能");
                                //await NextChatLogic(tran, context, dbflowinstance, WorkFlowInstanceStatus.Running);
                            }

                            #region 添加流转记录

                            WfWorkflowTransitionHistory transitionHistory = new WfWorkflowTransitionHistory
                            {
                                TransitionId = Guid.NewGuid(),
                                InstanceId = dbflowinstance.InstanceId,
                                FromNodeId = context.WorkFlow.ActivityNodeId,
                                FromNodName = context.WorkFlow.ActivityNode.Name,
                                FromNodeType = (int)context.WorkFlow.ActivityNodeType,
                                ToNodeId = context.WorkFlow.NextNodeId,
                                ToNodeType = (int)context.WorkFlow.NextNodeType,
                                ToNodeName = context.WorkFlow.NextNode.Name,
                                TransitionState = (int)WorkFlowTransitionStateType.Normal,
                                IsFinish = context.WorkFlow.NextNodeType.ToIsFinish(),
                                CreateUserId = model.UserId,
                                CreateUserName = model.UserName
                            };
                            await databaseFixture.Db.WorkflowTransitionHistory.InsertAsync(transitionHistory, tran);

                            #endregion
                        }
                    }
                    else
                    {
                        throw new Exception("当前只支持正常节点功能");
                        //await ChatLogic(tran, context, dbflowinstance, model, WorkFlowInstanceStatus.Running);
                    }

                    #region 添加操作记录

                    WfWorkflowOperationHistory operationHistory = new WfWorkflowOperationHistory
                    {
                        OperationId = Guid.NewGuid(),
                        InstanceId = dbflowinstance.InstanceId,
                        CreateUserId = model.UserId,
                        CreateUserName = model.UserName,
                        Content = model.ProcessContent,
                        NodeId = context.WorkFlow.ActivityNodeId,
                        NodeName = context.WorkFlow.ActivityNode.Name,
                        TransitionType = (int)WorkFlowMenu.Agree
                    };
                    await databaseFixture.Db.WorkflowOperationHistory.InsertAsync(operationHistory, tran);

                    #endregion

                    await FlowStatusChangePublisher(model.StatusChange, publishFlowStatus);

                    tran.Commit();

                    return WorkFlowResult.Success();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    return WorkFlowResult.Error(ex.Message);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewNodes"></param>
        /// <param name="createuserid"></param>
        /// <param name="model"></param>
        /// <param name="tran"></param>
        /// <returns></returns>
        private async Task AddFlowNotice(List<FlowNode> viewNodes, string createuserid, WorkFlowProcessTransition model, IDbTransaction tran)
        {
            if (viewNodes.Any())
            {
                Dictionary<string, FlowNode> dic = new Dictionary<string, FlowNode>();
                foreach (var item in viewNodes)
                {
                    string makerStr = "";
                    if (item.SetInfo.NodeDesignate == FlowNodeSetInfo.CREATEUSER)
                    {
                        makerStr = createuserid + ",";
                    }
                    else
                    {
                        makerStr = await this.GetMakerListAsync(item, model.UserId, model.OptionParams);
                    }
                    if (makerStr.IsNotNullOrEmpty() && makerStr != "0")
                    {
                        string[] makerlist = makerStr.Split(',');
                        foreach (var viewuserid in makerlist.Where(m => !string.IsNullOrEmpty(m)))
                        {
                            if (!dic.ContainsKey(viewuserid))
                            {
                                dic.Add(viewuserid, item);
                            }
                        }
                    }
                }
                List<WfWorkflowNotice> notices = dic.Select(m => new WfWorkflowNotice
                {
                    Id = Guid.NewGuid(),
                    IsRead = 0,
                    Maker = m.Key,
                    NodeId = m.Value.Id,
                    NodeName = m.Value.Name,
                    Status = 1,
                    IsTransition = 1,
                    InstanceId = model.InstanceId
                }).ToList();
                if (notices.Any())
                {
                    await databaseFixture.Db.WfWorkflowNotice.BulkInsertAsync(notices, tran);
                }
            }
        }

        /// <summary>
        /// 不同意
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        protected async Task<WorkFlowResult> ProcessTransitionDeprecateAsync(WorkFlowProcessTransition model)
        {
            using (var tran = databaseFixture.Db.BeginTransaction())
            {
                try
                {
                    var dbflowinstance = await databaseFixture.Db.WorkflowInstance.FindByIdAsync(model.InstanceId);
                    if (dbflowinstance.IsFinish == (int)WorkFlowInstanceStatus.Finish)
                    {
                        return WorkFlowResult.Error("该流程已经结束！");
                    }
                    MsWorkFlowContext context = new MsWorkFlowContext(new JadeFramework.WorkFlow.WorkFlow
                    {
                        FlowId = model.FlowId,
                        FlowJSON = dbflowinstance.FlowContent,
                        ActivityNodeId = dbflowinstance.ActivityId,
                        PreviousId = dbflowinstance.PreviousId
                    });
                    if (context.WorkFlow.ActivityNode.NodeType() == WorkFlowInstanceNodeType.ChatNode)
                    {
                        await ChatLogic(tran, context, dbflowinstance, model, WorkFlowInstanceStatus.Running);
                    }
                    else
                    {
                        //流程不同意节点判断
                        dbflowinstance.MakerList = "";
                        dbflowinstance.IsFinish = 0;
                        dbflowinstance.Status = (int)WorkFlowStatus.Deprecate;
                        dbflowinstance.PreviousId = dbflowinstance.ActivityId;
                        dbflowinstance.ActivityId = context.WorkFlow.NextNodeId;
                        dbflowinstance.UpdateTime = DateTime.Now.ToTimeStamp();
                        await databaseFixture.Db.WorkflowInstance.UpdateAsync(dbflowinstance, tran);

                        #region 流转记录

                        WfWorkflowTransitionHistory transitionHistory = new WfWorkflowTransitionHistory
                        {
                            TransitionId = Guid.NewGuid(),
                            InstanceId = dbflowinstance.InstanceId,
                            TransitionState = (int)WorkFlowTransitionStateType.Reject,
                            IsFinish = (int)WorkFlowInstanceStatus.Running,
                            CreateUserId = model.UserId,
                            CreateUserName = model.UserName,
                            FromNodeId = context.WorkFlow.ActivityNodeId,
                            FromNodName = context.WorkFlow.ActivityNode.Name,
                            FromNodeType = (int)context.WorkFlow.ActivityNodeType,
                            ToNodeId = context.WorkFlow.NextNodeId,
                            ToNodeType = (int)context.WorkFlow.NextNodeType,
                            ToNodeName = context.WorkFlow.NextNode.Name
                        };
                        await databaseFixture.Db.WorkflowTransitionHistory.InsertAsync(transitionHistory, tran);
                        #endregion
                    }

                    #region 操作历史

                    WfWorkflowOperationHistory operationHistory = new WfWorkflowOperationHistory
                    {
                        OperationId = Guid.NewGuid(),
                        InstanceId = dbflowinstance.InstanceId,
                        CreateUserId = model.UserId,
                        CreateUserName = model.UserName,
                        Content = model.ProcessContent,
                        NodeName = context.WorkFlow.ActivityNode.Name,
                        NodeId = context.WorkFlow.ActivityNodeId,
                        TransitionType = (int)WorkFlowMenu.Deprecate
                    };

                    await databaseFixture.Db.WorkflowOperationHistory.InsertAsync(operationHistory, tran);
                    #endregion

                    await FlowStatusChangePublisher(model.StatusChange, WorkFlowStatus.Deprecate);

                    tran.Commit();
                    return WorkFlowResult.Success();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    return WorkFlowResult.Error(ex.Message);
                }
            }
        }

        /// <summary>
        /// 流程退回
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        protected async Task<WorkFlowResult> ProcessTransitionBackAsync(WorkFlowProcessTransition model)
        {
            using (var tran = databaseFixture.Db.BeginTransaction())
            {
                try
                {
                    var dbflowinstance = await databaseFixture.Db.WorkflowInstance.FindByIdAsync(model.InstanceId);
                    if (dbflowinstance.IsFinish == (int)WorkFlowInstanceStatus.Finish)
                    {
                        return WorkFlowResult.Error("该流程已经结束！");
                    }
                    if (model.NodeRejectType == null)
                    {
                        throw new Exception("参数报错！");
                    }
                    MsWorkFlowContext context = new MsWorkFlowContext(new JadeFramework.WorkFlow.WorkFlow
                    {
                        FlowId = model.FlowId,
                        FlowJSON = dbflowinstance.FlowContent,
                        ActivityNodeId = dbflowinstance.ActivityId,
                        PreviousId = dbflowinstance.PreviousId
                    });
                    if (context.WorkFlow.ActivityNodeType == WorkFlowInstanceNodeType.Normal || context.WorkFlow.ActivityNodeType == WorkFlowInstanceNodeType.BeginRound)
                    {
                        Guid rejectNodeId = context.RejectNode(model.NodeRejectType.Value, model.RejectNodeId);
                        FlowNode rejectNode = context.WorkFlow.Nodes[rejectNodeId];

                        dbflowinstance.PreviousId = dbflowinstance.ActivityId;
                        dbflowinstance.ActivityId = rejectNodeId;
                        dbflowinstance.ActivityName = rejectNode.Name;
                        dbflowinstance.ActivityType = (int)rejectNode.NodeType();
                        dbflowinstance.UpdateTime = DateTime.Now.ToTimeStamp();
                        if (rejectNode.NodeType() == WorkFlowInstanceNodeType.BeginRound)//开始节点时候
                        {
                            dbflowinstance.MakerList = dbflowinstance.CreateUserId + ",";
                        }
                        else
                        {
                            dbflowinstance.MakerList = rejectNode.NodeType() == WorkFlowInstanceNodeType.EndRound
                                ? ""
                                : await this.GetMakerListAsync(rejectNode, dbflowinstance.CreateUserId, model.OptionParams);
                        }
                        dbflowinstance.IsFinish = rejectNode.NodeType().ToIsFinish();
                        dbflowinstance.Status = (int)WorkFlowStatus.Back;
                        await databaseFixture.Db.WorkflowInstance.UpdateAsync(dbflowinstance, tran);

                        #region 流转记录

                        WfWorkflowTransitionHistory transitionHistory = new WfWorkflowTransitionHistory
                        {
                            TransitionId = Guid.NewGuid(),
                            InstanceId = dbflowinstance.InstanceId,
                            CreateUserId = model.UserId,
                            CreateUserName = model.UserName,
                            IsFinish = (int)WorkFlowInstanceStatus.Running,
                            TransitionState = (int)WorkFlowTransitionStateType.Reject,
                            FromNodeId = context.WorkFlow.ActivityNodeId,
                            FromNodeType = (int)context.WorkFlow.ActivityNodeType,
                            FromNodName = context.WorkFlow.ActivityNode.Name,
                            ToNodeId = rejectNodeId,
                            ToNodeType = (int)rejectNode.NodeType(),
                            ToNodeName = rejectNode.Name
                        };
                        await databaseFixture.Db.WorkflowTransitionHistory.InsertAsync(transitionHistory, tran);

                        #endregion

                        #region 操作记录

                        WfWorkflowOperationHistory operationHistory = new WfWorkflowOperationHistory
                        {
                            OperationId = Guid.NewGuid(),
                            InstanceId = dbflowinstance.InstanceId,
                            CreateUserId = model.UserId,
                            CreateUserName = model.UserName,
                            Content = model.ProcessContent,
                            NodeName = context.WorkFlow.ActivityNode.Name,
                            TransitionType = (int)WorkFlowMenu.Back,
                            NodeId = context.WorkFlow.ActivityNodeId
                        };
                        await databaseFixture.Db.WorkflowOperationHistory.InsertAsync(operationHistory, tran);
                        #endregion
                    }
                    else
                    {
                        return WorkFlowResult.Error("当前节点为会签节点，不可退回！");
                    }

                    await FlowStatusChangePublisher(model.StatusChange, WorkFlowStatus.Back);

                    tran.Commit();

                    return WorkFlowResult.Success();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    return WorkFlowResult.Error(ex.Message);
                }
            }
        }

        /// <summary>
        /// 流程取消
        /// 刚开始提交，下一个节点未审批情况，流程发起人可以终止
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        protected async Task<WorkFlowResult> ProcessTransitionWithdrawAsync(WorkFlowProcessTransition model)
        {
            using (var tran = databaseFixture.Db.BeginTransaction())
            {
                try
                {
                    var dbflowinstance = await databaseFixture.Db.WorkflowInstance.FindByIdAsync(model.InstanceId);
                    //删除流程操作记录
                    var dboperationHistory = await databaseFixture.Db.WorkflowOperationHistory.FindAllAsync(m => m.InstanceId == model.InstanceId);
                    foreach (var item in dboperationHistory)
                    {
                        await databaseFixture.Db.WorkflowOperationHistory.DeleteAsync(item, tran);
                    }
                    //删除流程流转记录
                    var dbtransitionHistory = await databaseFixture.Db.WorkflowTransitionHistory.FindAllAsync(m => m.InstanceId == model.InstanceId);
                    foreach (var item in dbtransitionHistory)
                    {
                        await databaseFixture.Db.WorkflowTransitionHistory.DeleteAsync(item, tran);
                    }
                    //删除委托表信息
                    var dbassigns = await databaseFixture.Db.WfWorkflowAssign.FindAllAsync(m => m.InstanceId == model.InstanceId);
                    foreach (var item in dbassigns)
                    {
                        await databaseFixture.Db.WfWorkflowAssign.DeleteAsync(item, tran);
                    }
                    var dbinstanceForm = await databaseFixture.Db.WorkflowInstanceForm.FindAsync(m => m.InstanceId == dbflowinstance.InstanceId);
                    if ((WorkFlowFormType)dbinstanceForm.FormType == WorkFlowFormType.System)//定制表单
                    {
                        //删除流程实例表单关联记录
                        await databaseFixture.Db.WorkflowInstanceForm.DeleteAsync(dbinstanceForm, tran);
                        //删除流程实例
                        await databaseFixture.Db.WorkflowInstance.DeleteAsync(dbflowinstance, tran);
                        //改变表单状态
                        await FlowStatusChangePublisher(model.StatusChange, WorkFlowStatus.Withdraw);
                    }
                    else
                    {
                        //自定义表单流程实例修改
                        dbflowinstance.IsFinish = null;
                        dbflowinstance.Status = (int)WorkFlowStatus.UnSubmit;
                        dbflowinstance.MakerList = null;
                        dbflowinstance.UpdateTime = DateTime.Now.ToTimeStamp();
                        await databaseFixture.Db.WorkflowInstance.UpdateAsync(dbflowinstance, tran);
                    }

                    tran.Commit();
                    return WorkFlowResult.Success();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    return WorkFlowResult.Error("流程取消失败！");
                }
            }
        }

        /// <summary>
        /// 已阅操作
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        protected async Task<WorkFlowResult> ProcessTransitionViewAsync(WorkFlowProcessTransition model)
        {
            using (var tran = databaseFixture.Db.BeginTransaction())
            {
                try
                {
                    var dbnotices = await databaseFixture.Db.WfWorkflowNotice.FindAllAsync(m => m.Maker == model.UserId && m.InstanceId == model.InstanceId && m.IsTransition == 1 && m.IsRead == 0 && m.Status == 1);
                    foreach (var item in dbnotices)
                    {
                        item.IsRead = 1;

                        #region 添加操作记录
                        WfWorkflowOperationHistory operationHistory = new WfWorkflowOperationHistory
                        {
                            OperationId = Guid.NewGuid(),
                            InstanceId = model.InstanceId,
                            CreateUserId = model.UserId,
                            CreateUserName = model.UserName,
                            Content = "流程已阅",
                            NodeId = item.NodeId,
                            NodeName = item.NodeName,
                            TransitionType = (int)WorkFlowMenu.View
                        };
                        await databaseFixture.Db.WorkflowOperationHistory.InsertAsync(operationHistory, tran);
                        #endregion
                    }
                    await databaseFixture.Db.WfWorkflowNotice.BulkUpdateAsync(dbnotices, tran);


                    tran.Commit();

                    return WorkFlowResult.Success();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    return WorkFlowResult.Error("已阅操作失败");
                }
            }
        }

        /// <summary>
        /// 流程委托操作
        /// 将自己审批某个流程的权限赋予其他人，让其他用户代审批流程;
        /// 规则：A委托给B，A不能再审批且不能多次委托，B可再次委托给C，同理A
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        protected async Task<WorkFlowResult> ProcessTransitionAssignAsync(WorkFlowProcessTransition model)
        {
            using (var tran = databaseFixture.Db.BeginTransaction())
            {
                try
                {
                    //1、修改流程实例makerlist，替换人
                    var dbflowinstance = await databaseFixture.Db.WorkflowInstance.FindByIdAsync(model.InstanceId);
                    string oldreplaceStr = model.UserId + ",";
                    string newreplaceStr = model.Assign.AssignUserId + ",";
                    var newmakerList = dbflowinstance.MakerList.Replace(oldreplaceStr, newreplaceStr);
                    dbflowinstance.MakerList = newmakerList;
                    await databaseFixture.Db.WorkflowInstance.UpdateAsync(dbflowinstance, tran);

                    MsWorkFlowContext context = new MsWorkFlowContext(new JadeFramework.WorkFlow.WorkFlow
                    {
                        FlowId = model.FlowId,
                        FlowJSON = dbflowinstance.FlowContent,
                        ActivityNodeId = dbflowinstance.ActivityId,
                        PreviousId = dbflowinstance.PreviousId
                    });

                    //2、添加委托记录
                    WfWorkflowAssign workflowAssign = new WfWorkflowAssign
                    {
                        Id = Guid.NewGuid(),
                        UserId = model.UserId,
                        UserName = model.UserName,
                        FlowId = model.FlowId,
                        NodeId = context.WorkFlow.ActivityNodeId,
                        NodeName = context.WorkFlow.ActivityNode.Name,
                        InstanceId = model.InstanceId,
                        CreateUserId = model.UserId,
                        AssignUserId = model.Assign.AssignUserId,
                        AssignUserName = model.Assign.AssignUserName,
                        Content = model.Assign.AssignContent
                    };
                    await databaseFixture.Db.WfWorkflowAssign.InsertAsync(workflowAssign, tran);

                    //3、添加操作记录（不添加流转，因为实际情况流程并没有运行到下一个节点）
                    string operConent = $"用户【{workflowAssign.UserName}】将流程委托给【{workflowAssign.AssignUserName}】";
                    if (operConent.IsNotNullOrEmpty())
                    {
                        operConent += "<br/>请求委托意见：" + model.Assign.AssignContent;
                    }
                    WfWorkflowOperationHistory operationHistory = new WfWorkflowOperationHistory
                    {
                        OperationId = Guid.NewGuid(),
                        InstanceId = dbflowinstance.InstanceId,
                        CreateUserId = model.UserId,
                        CreateUserName = model.UserName,
                        Content = operConent,
                        NodeId = context.WorkFlow.ActivityNodeId,
                        NodeName = context.WorkFlow.ActivityNode.Name,
                        TransitionType = (int)WorkFlowMenu.Assign
                    };
                    await databaseFixture.Db.WorkflowOperationHistory.InsertAsync(operationHistory, tran);

                    tran.Commit();

                    return WorkFlowResult.Success();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    return WorkFlowResult.Error("委托操作失败");
                }
            }
        }

        /// <summary>
        /// 获取审批意见
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<WorkFlowResult> GetFlowApprovalAsync(WorkFlowProcessTransition model)
        {
            var dbhistory = await databaseFixture.Db.WorkflowOperationHistory.FindAllAsync(m => m.InstanceId == model.InstanceId);
            var dbinstance = await databaseFixture.Db.WorkflowInstance.FindByIdAsync(model.InstanceId);
            if (dbinstance.IsFinish == 1)
            {
                List<WfWorkflowOperationHistory> list = new List<WfWorkflowOperationHistory>
                {
                    new WfWorkflowOperationHistory
                    {
                        NodeName = "结束",
                        TransitionType = null,
                        CreateUserName = "",
                        Content = "系统自动结束",
                        CreateTime = dbhistory.OrderByDescending(m => m.CreateTime).Select(m => m.CreateTime).First() + 1
                    }
                };
                IEnumerable<WfWorkflowOperationHistory> result = dbhistory.Union(list);
                return WorkFlowResult.Success(string.Empty, result.OrderBy(m => m.CreateTime));
            }
            else
            {
                return WorkFlowResult.Success(string.Empty, dbhistory.OrderBy(m => m.CreateTime));
            }
        }

        /// <summary>
        /// 获取流程图信息
        /// </summary>
        /// <param name="instanceId">实例ID</param>
        /// <returns></returns>
        public async Task<WorkFlowImageDto> GetFlowImageAsync(Guid flowid, Guid? instanceId)
        {
            if (instanceId == null || instanceId.Value == default(Guid))
            {
                var dbflow = await databaseFixture.Db.Workflow.FindByIdAsync(flowid);
                return new WorkFlowImageDto
                {
                    FlowId = dbflow.FlowId,
                    FlowContent = dbflow.FlowContent,
                    InstanceId = default(Guid),
                    CurrentNodeId = default(Guid)
                };
            }
            else
            {
                var instance = await databaseFixture.Db.WorkflowInstance.FindAsync(m => m.InstanceId == instanceId);
                return new WorkFlowImageDto
                {
                    FlowId = instance.FlowId,
                    InstanceId = instance.InstanceId,
                    CurrentNodeId = instance.ActivityId,
                    FlowContent = instance.FlowContent,
                };
            }

        }

        #endregion

        /// <summary>
        /// 流程催办
        /// </summary>
        /// <param name="urge"></param>
        /// <returns></returns>
        public async Task<WorkFlowResult> UrgeAsync(UrgeDto urge)
        {
            WfWorkflowUrge workflowUrge = new WfWorkflowUrge
            {
                CreateUserId = urge.Sender,
                Sender = urge.Sender,
                InstanceId = urge.InstanceId,
                UrgeContent = urge.UrgeContent,
                UrgeType = urge.UrgeType,
                Id = Guid.NewGuid()
            };

            var instance = await databaseFixture.Db.WorkflowInstance.FindByIdAsync(urge.InstanceId);
            workflowUrge.NodeId = instance.ActivityId;
            workflowUrge.NodeName = instance.ActivityName;
            workflowUrge.UrgeUser = instance.MakerList;

            bool res = await databaseFixture.Db.WfWorkflowUrge.InsertAsync(workflowUrge);
            if (res)
            {
                return WorkFlowResult.Success("", workflowUrge.UrgeUser);
            }
            else
            {
                return WorkFlowResult.Error("insert error");
            }
        }

    }
}
