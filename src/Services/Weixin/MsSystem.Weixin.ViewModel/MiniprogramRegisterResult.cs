using JadeFramework.Weixin.MiniProgram;

namespace MsSystem.Weixin.ViewModel
{
    public class MiniprogramRegisterResult: MiniProgramResult
    {
        public MiniprogramRegisterData Data { get; set; }
        public string Message { get; set; }
    }
    public class MiniprogramRegisterData
    {
        public long Id { get; set; }
        public string SessionId { get; set; }
    }

}
