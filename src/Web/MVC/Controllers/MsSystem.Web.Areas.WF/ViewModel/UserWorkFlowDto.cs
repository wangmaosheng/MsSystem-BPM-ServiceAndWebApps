namespace MsSystem.Web.Areas.WF.ViewModel
{
    using System;
    public class UserWorkFlowDto
    {
        public Guid FlowId { get; set; }
        public string FlowName { get; set; }
        /// <summary>
        /// 表单名称
        /// </summary>
        public string FormName { get; set; }
        public Guid InstanceId { get; set; }
        public string InstanceCode { get; set; }
        public int? IsFinish { get; set; }
        public int Status { get; set; }
        public long CreateTime { get; set; }
        /// <summary>
        /// 发起人
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 表单类型
        /// </summary>
        public int FormType { get; set; }
        /// <summary>
        /// 表单跳转地址
        /// </summary>
        public string FormUrl { get; set; }
        /// <summary>
        /// 表单key id
        /// </summary>
        public string FormData { get; set; }
    }
}
