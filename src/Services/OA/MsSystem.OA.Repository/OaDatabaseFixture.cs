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
            //docker compose获取配置信息
            string MYSQL_DB = _config.GetSection("MYSQL_DB").Value;
            if (string.IsNullOrEmpty(MYSQL_DB))
            {
                var section = _config.GetSection("MySQL");
                Db = new OaDbContext(section["Connection"].ToString());
            }
            else
            {
                string MYSQL_USER = _config.GetSection("MYSQL_USER").Value;
                string MYSQL_PASS = _config.GetSection("MYSQL_PASS").Value;
                string MYSQL_HOST = _config.GetSection("MYSQL_HOST").Value;
                string connectionString = $"Database={MYSQL_DB};Data Source={MYSQL_HOST};User Id={MYSQL_USER};Password={MYSQL_PASS}";
                Db = new OaDbContext(connectionString);
            }
        }

        public IOaDbContext Db { get; }
        public void Dispose()
        {
            Db.Dispose();
        }
    }
}
