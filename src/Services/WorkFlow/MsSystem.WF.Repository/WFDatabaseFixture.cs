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
            //docker compose获取配置信息
            string MYSQL_DB = _config.GetSection("MYSQL_DB").Value;
            if (string.IsNullOrEmpty(MYSQL_DB))
            {
                var section = _config.GetSection("MySQL");
                Db = new WFDbContext(section["Connection"].ToString());
            }
            else
            {
                string MYSQL_USER = _config.GetSection("MYSQL_USER").Value;
                string MYSQL_PASS = _config.GetSection("MYSQL_PASS").Value;
                string MYSQL_HOST = _config.GetSection("MYSQL_HOST").Value;
                string connectionString = $"Database={MYSQL_DB};Data Source={MYSQL_HOST};User Id={MYSQL_USER};Password={MYSQL_PASS}";
                Db = new WFDbContext(connectionString);
            }
        }

        public IWFDbContext Db { get; }
        public void Dispose()
        {
            Db.Dispose();
        }
    }
}
