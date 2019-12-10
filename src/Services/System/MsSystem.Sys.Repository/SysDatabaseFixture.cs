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
            //docker compose获取配置信息
            string MYSQL_DB = _config.GetSection("MYSQL_DB").Value;
            if (string.IsNullOrEmpty(MYSQL_DB))
            {
                var section = _config.GetSection("MySQL");
                Db = new SysDbContext(section["Connection"].ToString());
            }
            else
            {
                string MYSQL_USER = _config.GetSection("MYSQL_USER").Value;
                string MYSQL_PASS = _config.GetSection("MYSQL_PASS").Value;
                string MYSQL_HOST = _config.GetSection("MYSQL_HOST").Value;
                string connectionString = $"Database={MYSQL_DB};Data Source={MYSQL_HOST};User Id={MYSQL_USER};Password={MYSQL_PASS}";
                Db = new SysDbContext(connectionString);
            }
            string MYSQL_LOG_DB = _config.GetSection("MYSQL_LOG_DB").Value;
            if (string.IsNullOrEmpty(MYSQL_DB))
            {
                var section = _config.GetSection("MySQL");
                LogDb = new SysLogDbContext(section["LogConnection"].ToString());
            }
            else
            {
                string MYSQL_LOG_USER = _config.GetSection("MYSQL_LOG_USER").Value;
                string MYSQL_LOG_PASS = _config.GetSection("MYSQL_LOG_PASS").Value;
                string MYSQL_LOG_HOST = _config.GetSection("MYSQL_LOG_HOST").Value;
                string connectionString = $"Database={MYSQL_LOG_DB};Data Source={MYSQL_LOG_HOST};User Id={MYSQL_LOG_USER};Password={MYSQL_LOG_PASS}";
                LogDb = new SysLogDbContext(connectionString);
            }
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
