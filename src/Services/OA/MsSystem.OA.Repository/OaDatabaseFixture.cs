using Microsoft.Extensions.Configuration;
using MsSystem.OA.IRepository;
using System;

namespace MsSystem.OA.Repository
{
    public class OaDatabaseFixture : IOaDatabaseFixture, IDisposable
    {
        private IConfiguration _config;
        public OaDatabaseFixture(IConfiguration config)
        {
            _config = config;
            var connectionString = _config.GetSection("MySQL")["Connection"].ToString();
            Db = new OaDbContext(connectionString);
        }

        public IOaDbContext Db { get; }
        public void Dispose()
        {
        }
    }
}
