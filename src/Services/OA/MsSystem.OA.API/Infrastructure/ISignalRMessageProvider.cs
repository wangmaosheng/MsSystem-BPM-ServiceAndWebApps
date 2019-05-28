using System.Collections.Generic;

namespace MsSystem.OA.API.Infrastructure
{
    public interface ISignalRMessageProvider
    {
        string ConnectionId { get; set; }
        long UserId { get; set; }
        string GroupName { get; set; }
        List<ISignalRMessageProvider> UserGroups { get; set; }
    }

    public class SignalRMessageMemoryProvider: ISignalRMessageProvider
    {

        public string ConnectionId { get; set; }
        public long UserId { get; set; }
        public string GroupName { get; set; }

        private List<ISignalRMessageProvider> _userGroups;

        public SignalRMessageMemoryProvider()
        {
            this._userGroups = new List<ISignalRMessageProvider>();
        }

        public List<ISignalRMessageProvider> UserGroups
        {
            get { return _userGroups; }
            set { _userGroups = value; }
        }
    }
}
