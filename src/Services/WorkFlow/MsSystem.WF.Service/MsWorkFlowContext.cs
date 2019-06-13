namespace MsSystem.WF.Service
{
    using JadeFramework.Core.Extensions;
    using JadeFramework.WorkFlow;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// workflow context
    /// </summary>
    public class MsWorkFlowContext : WorkFlowContext
    {
        /// <summary>
        /// 构造器传参
        /// </summary>
        /// <param name="dbworkflow"></param>
        public MsWorkFlowContext(WorkFlow dbworkflow)
        {
            if (dbworkflow.FlowId == default(Guid))
            {
                throw new ArgumentNullException("FlowId", "input workflow flowid is null");
            }
            if (dbworkflow.FlowJSON.IsNullOrEmpty())
            {
                throw new ArgumentException("FlowJSON", "input workflow json is null");
            }
            if (dbworkflow.ActivityNodeId == null)
            {
                throw new ArgumentException("ActivityNodeId", "input workflow ActivityNodeId is null");
            }

            this.WorkFlow = dbworkflow;

            dynamic jsonobj = JsonConvert.DeserializeObject(this.WorkFlow.FlowJSON);
            //获取节点
            this.WorkFlow.Nodes = this.GetNodes(jsonobj.nodes);
            //获取连线
            this.WorkFlow.Lines = this.GetFromLines(jsonobj.lines);

            this.WorkFlow.ActivityNodeId = dbworkflow.ActivityNodeId == default(Guid) ? this.WorkFlow.StartNodeId : dbworkflow.ActivityNodeId;

            this.WorkFlow.ActivityNodeType = this.GetNodeType(this.WorkFlow.ActivityNodeId);

            //会签会签节点和流程结束节点没有下一步
            if (this.WorkFlow.ActivityNodeType == WorkFlowInstanceNodeType.ChatNode || this.WorkFlow.ActivityNodeType == WorkFlowInstanceNodeType.EndRound)
            {
                this.WorkFlow.NextNodeId = default(Guid);//未找到节点
                this.WorkFlow.NextNodeType = WorkFlowInstanceNodeType.NotRun;
            }
            else
            {
                var nodeids = this.GetNextNodeIdsNotSpecialNode(this.WorkFlow.ActivityNodeId, WorkFlowInstanceNodeType.ViewNode);
                if (nodeids.Count == 1)
                {
                    this.WorkFlow.NextNodeId = nodeids[0];
                    this.WorkFlow.NextNodeType = this.GetNodeType(this.WorkFlow.NextNodeId);
                }
                else
                {
                    //多个下个节点情况
                    this.WorkFlow.NextNodeId = default(Guid);
                    this.WorkFlow.NextNodeType = WorkFlowInstanceNodeType.NotRun;
                }
            }
        }

        /// <summary>
        /// 下个正常节点是否是多个(包含结束节点)
        /// </summary>
        public bool IsMultipleNextNode()
        {
            List<FlowNode> nodes = new List<FlowNode>();
            List<FlowLine> lines = this.WorkFlow.Lines[this.WorkFlow.ActivityNodeId];
            var nodeids = lines.Select(m => m.To).ToList();
            foreach (var item in nodeids)
            {
                var _thisnode = this.WorkFlow.Nodes[item];
                if (_thisnode.NodeType() == WorkFlowInstanceNodeType.Normal|| _thisnode.NodeType() == WorkFlowInstanceNodeType.EndRound)
                {
                    nodes.Add(_thisnode);
                }
            }
            return nodes.Count >= 2;
        }

        /// <summary>
        /// 获取节点集合
        /// </summary>
        /// <param name="nodesobj"></param>
        /// <returns></returns>
        private Dictionary<Guid, FlowNode> GetNodes(dynamic nodesobj)
        {
            Dictionary<Guid, FlowNode> nodes = new Dictionary<Guid, FlowNode>();

            foreach (JObject item in nodesobj)
            {
                FlowNode node = item.ToObject<FlowNode>();
                if (!nodes.ContainsKey(node.Id))
                {
                    nodes.Add(node.Id, node);
                }
                if (node.Type == FlowNode.START)
                {
                    this.WorkFlow.StartNodeId = node.Id;
                }
            }
            return nodes;
        }

        /// <summary>
        /// 获取工作流节点及以节点为出发点的流程
        /// </summary>
        /// <param name="linesobj"></param>
        /// <returns></returns>
        private Dictionary<Guid, List<FlowLine>> GetFromLines(dynamic linesobj)
        {
            Dictionary<Guid, List<FlowLine>> lines = new Dictionary<Guid, List<FlowLine>>();

            foreach (JObject item in linesobj)
            {
                FlowLine line = item.ToObject<FlowLine>();

                if (!lines.ContainsKey(line.From))
                {
                    lines.Add(line.From, new List<FlowLine> { line });
                }
                else
                {
                    lines[line.From].Add(line);
                }
            }

            return lines;
        }

        /// <summary>
        /// 获取全部流程线
        /// </summary>
        /// <returns></returns>
        public List<FlowLine> GetAllLines()
        {
            dynamic jsonobj = JsonConvert.DeserializeObject(this.WorkFlow.FlowJSON);
            List<FlowLine> lines = new List<FlowLine>();
            foreach (JObject item in jsonobj.lines)
            {
                FlowLine line = item.ToObject<FlowLine>();
                lines.Add(line);
            }
            return lines;
        }

        /// <summary>
        /// 根据节点ID获取From（流入的线条）
        /// </summary>
        /// <param name="nodeid"></param>
        /// <returns></returns>
        public List<FlowLine> GetLinesForFrom(Guid nodeid)
        {
            var lines = GetAllLines().Where(m => m.To == nodeid).ToList();
            return lines;
        }

        /// <summary>
        /// 根据节点ID获取该节点与下个节点的连线
        /// </summary>
        /// <param name="nodeid">节点ID</param>
        /// <param name="nodeType">要获取的节点类型,默认正常节点（包含结束节点）</param>
        /// <returns></returns>
        public List<FlowLine> GetLinesForTo(Guid nodeid)
        {
            List<FlowLine> list = new List<FlowLine>();
            var lines = GetAllLines().Where(m => m.From == nodeid).ToList();
            foreach (var item in lines)
            {
                var _nodeType = this.GetNodeType(item.To);
                if (_nodeType == WorkFlowInstanceNodeType.Normal||_nodeType==WorkFlowInstanceNodeType.EndRound)
                {
                    list.Add(item);
                }
            }
            return list;
        }

        /// <summary>
        /// 获取全部节点
        /// </summary>
        /// <returns></returns>
        public List<FlowNode> GetAllNodes()
        {
            dynamic jsonobj = JsonConvert.DeserializeObject(this.WorkFlow.FlowJSON);
            List<FlowNode> nodes = new List<FlowNode>();
            foreach (JObject item in jsonobj.nodes)
            {
                FlowNode node = item.ToObject<FlowNode>();
                nodes.Add(node);
            }
            return nodes;
        }

        /// <summary>
        /// 根据节点ID获取节点类型
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        public WorkFlowInstanceNodeType GetNodeType(Guid nodeId)
        {
            var _thisnode = this.WorkFlow.Nodes[nodeId];
            return _thisnode.NodeType();
        }

        /// <summary>
        /// 根据节点id获取下个节点id
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        public List<Guid> GetNextNodeIds(Guid nodeId)
        {
            List<FlowLine> lines = this.WorkFlow.Lines[nodeId];
            return lines.Select(m => m.To).ToList();
        }

        /// <summary>
        /// 根据节点id获取下个节点id
        /// </summary>
        /// <param name="nodeId"></param>
        /// <param name="nodeType"></param>
        /// <returns></returns>
        public List<Guid> GetNextNodeIds(Guid nodeId, WorkFlowInstanceNodeType nodeType)
        {
            List<Guid> list = new List<Guid>();
            List<FlowLine> lines = this.WorkFlow.Lines[nodeId];
            var nodeids = lines.Select(m => m.To).ToList();
            foreach (var item in nodeids)
            {
                var _thisnode = this.WorkFlow.Nodes[item];
                if (_thisnode.NodeType() == nodeType)
                {
                    list.Add(item);
                }
            }
            return list;
        }
        public List<Guid> GetNextNodeIdsNotSpecialNode(Guid nodeId, WorkFlowInstanceNodeType nodeType)
        {
            List<Guid> list = new List<Guid>();
            List<FlowLine> lines = this.WorkFlow.Lines[nodeId];
            var nodeids = lines.Select(m => m.To).ToList();
            foreach (var item in nodeids)
            {
                var _thisnode = this.WorkFlow.Nodes[item];
                if (_thisnode.NodeType() != nodeType)
                {
                    list.Add(item);
                }
            }
            return list;
        }

        /// <summary>
        /// 获取该节点的下面节点
        /// </summary>
        /// <param name="nodeId"></param>
        /// <param name="nodeType"></param>
        /// <returns></returns>
        public List<FlowNode> GetNextNodes(Guid? nodeId = null, WorkFlowInstanceNodeType? nodeType = WorkFlowInstanceNodeType.Normal)
        {
            if (nodeId == null)
            {
                nodeId = this.WorkFlow.ActivityNodeId;
            }
            List<FlowLine> lines = this.WorkFlow.Lines[nodeId.Value];
            List<FlowNode> list = new List<FlowNode>();
            var nodeids = lines.Select(m => m.To).ToList();
            foreach (var item in nodeids)
            {
                var _thisnode = this.WorkFlow.Nodes[item];
                if (_thisnode.NodeType() == nodeType)
                {
                    list.Add(_thisnode);
                }
            }
            return list;
        }


        /// <summary>
        /// 节点驳回
        /// </summary>
        /// <param name="rejectType">驳回节点类型</param>
        /// <param name="rejectNodeid">要驳回到的节点</param>
        /// <returns></returns>
        public Guid RejectNode(NodeRejectType rejectType, Guid? rejectNodeid)
        {
            switch (rejectType)
            {
                case NodeRejectType.PreviousStep:
                    return this.WorkFlow.PreviousId;
                case NodeRejectType.FirstStep:
                    return this.GetNextNodeIds(this.WorkFlow.StartNodeId).First();
                case NodeRejectType.ForOneStep:
                    if (rejectNodeid == null || rejectNodeid == default(Guid))
                    {
                        throw new Exception("驳回节点没有值！");
                    }
                    var fornode = this.WorkFlow.Nodes[rejectNodeid.Value];
                    return fornode.Id;
                case NodeRejectType.UnHandled:
                default:
                    return this.WorkFlow.PreviousId;
            }
        }

    }
}
