using System;
using System.Collections.Generic;

namespace MsSystem.Sys.ViewModel
{
    public class FlowViewModel
    {
        public string sql { get; set; }
        public Dictionary<string, object> param { get; set; }
        /// <summary>
        /// 当前用户ID
        /// </summary>
        public string UserId { get; set; }
    }
    /// <summary>
    /// 流程连线最终的节点DTO
    /// </summary>
    public class FlowLineFinalNodeDto
    {
        /// <summary>
        /// 节点信息集合
        /// </summary>
        public Dictionary<Guid, string> Data { get; set; }
        /// <summary>
        /// 参数信息
        /// </summary>
        public Dictionary<string, object> Param { get; set; }
        /// <summary>
        /// 当前用户ID
        /// </summary>
        public string UserId { get; set; }
    }
}
