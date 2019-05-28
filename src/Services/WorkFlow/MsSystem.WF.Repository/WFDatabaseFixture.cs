using Microsoft.Extensions.Configuration;
using MsSystem.WF.IRepository;
using System;

namespace MsSystem.WF.Repository
{
    public class WFDatabaseFixture: IWFDatabaseFixture, IDisposable
    {
        private IConfiguration _config;
        public WFDatabaseFixture(IConfiguration config)
        {
            _config = config;
            var connectionString = _config.GetSection("MySQL")["Connection"].ToString();
            Db = new WFDbContext(connectionString);
        }

        public IWFDbContext Db { get; }
        public void Dispose()
        {
            Db.Dispose();
        }
    }
}
