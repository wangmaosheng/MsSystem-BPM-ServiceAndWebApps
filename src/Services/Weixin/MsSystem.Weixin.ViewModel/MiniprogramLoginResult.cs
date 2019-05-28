using JadeFramework.Weixin.MiniProgram;

namespace MsSystem.Weixin.ViewModel
{
    public class MiniprogramLoginResult : MiniProgramResult
    {
        public MiniprogramLoginResultData Data { get; set; }
        public string Message { get; set; }
    }
    public class MiniprogramLoginResultData
    {
        public long Id { get; set; }
        public string SessionId { get; set; }
    }
}
