using System;
using System.ComponentModel;

namespace MsSystem.Web.Areas.WF.ViewModel
{
    public class FormPageDto
    {
        public Guid FormId { get; set; }
        public string FormName { get; set; }
        /// <summary>
        /// 流程名称
        /// </summary>
        public string FlowName { get; set; }
        public int FormType { get; set; }
        public long CreateTime { get; set; }
    }

    public class FormDetailDto
    {
        public Guid FormId { get; set; }
        public string FormName { get; set; }
        public int FormType { get; set; }
        public string Content { get; set; }
        public string FormUrl { get; set; }
        public string CreateUserId { get; set; }
    }
}
