using Microsoft.Extensions.Configuration;
using MsSystem.Sys.IRepository;
using System;

namespace MsSystem.Sys.Repository
{
    public class SysDatabaseFixture : ISysDatabaseFixture, IDisposable
    {
        private IConfiguration _config;
        public SysDatabaseFixture(IConfiguration config)
        {
            _config = config;
            var section = _config.GetSection("MySQL");
            Db = new SysDbContext(section["Connection"].ToString());
            LogDb= new SysLogDbContext(section["LogConnection"].ToString());
        }

        public ISysDbContext Db { get; }
        public ISysLogDbContext LogDb { get; }
        public void Dispose()
        {
            Db.Dispose();
            LogDb.Dispose();
        }
    }
}
