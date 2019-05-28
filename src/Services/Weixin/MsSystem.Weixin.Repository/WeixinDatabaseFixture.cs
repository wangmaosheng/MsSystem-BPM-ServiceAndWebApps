using Microsoft.Extensions.Configuration;
using MsSystem.Weixin.IRepository;

namespace MsSystem.Weixin.Repository
{
    public class WeixinDatabaseFixture : IWeixinDatabaseFixture
    {
        private IConfiguration _config;
        public WeixinDatabaseFixture(IConfiguration config)
        {
            _config = config;
            var connectionString = _config.GetSection("MySQL")["Connection"].ToString();
            Db = new WeixinDbContext(connectionString);
        }
        public IWeixinDbContext Db { get; }

        public void Dispose()
        {
            Db.Dispose();
        }
    }
}
