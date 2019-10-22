using JadeFramework.Dapper.DbContext;
using JadeFramework.Dapper.SqlGenerator;
using MsSystem.Sys.IRepository;
using MySql.Data.MySqlClient;

namespace MsSystem.Sys.Repository
{
    public class SysDbContext : DapperDbContext, ISysDbContext
    {
        private readonly SqlGeneratorConfig _config = new SqlGeneratorConfig
        {
            SqlConnector = ESqlConnector.MySQL,
            UseQuotationMarks = true
        };
        public SysDbContext(string connectionString)
            : base(new MySqlConnection(connectionString))
        {

        }
        private ISysSystemRepository _sysSystem;
        private ISysButtonRepository _sysButton;
        private ISysDeptRepository _sysDept;
        private ISysReleaseLogRepository _sysReleaseLog;
        private ISysResourceRepository _sysResource;
        private ISysRoleRepository _sysRole;
        private ISysRoleResourceRepository _sysRoleResource;
        private ISysUserDeptRepository _sysUserDept;
        private ISysUserRepository _sysUser;
        private ISysUserRoleRepository _sysUserRole;
        private ISysDataPrivilegesRepository _sysDataPrivileges;

        private ISysLeaderRepository _sysLeader;
        private ISysDeptLeaderRepository _sysDeptLeader;
        private ISysWorkflowsqlRepository _sysWorkflowsql;

        private ISysScheduleRepository scheduleRepository;


        public ISysButtonRepository SysButton => _sysButton ?? (_sysButton = new SysButtonRepository(Connection, _config));
        public ISysDeptRepository SysDept => _sysDept ?? (_sysDept = new SysDeptRepository(Connection, _config));
        public ISysReleaseLogRepository SysReleaseLog => _sysReleaseLog ?? (_sysReleaseLog = new SysReleaseLogRepository(Connection, _config));
        public ISysResourceRepository SysResource => _sysResource ?? (_sysResource = new SysResourceRepository(Connection, _config));
        public ISysRoleRepository SysRole => _sysRole ?? (_sysRole = new SysRoleRepository(Connection, _config));
        public ISysRoleResourceRepository SysRoleResource => _sysRoleResource ?? (_sysRoleResource = new SysRoleResourceRepository(Connection, _config));
        public ISysSystemRepository SysSystem => _sysSystem ?? (_sysSystem = new SysSystemRepository(Connection, _config));
        public ISysUserDeptRepository SysUserDept => _sysUserDept ?? (_sysUserDept = new SysUserDeptRepository(Connection, _config));
        public ISysUserRepository SysUser => _sysUser ?? (_sysUser = new SysUserRepository(Connection, _config));
        public ISysUserRoleRepository SysUserRole => _sysUserRole ?? (_sysUserRole = new SysUserRoleRepository(Connection, _config));
        public ISysDataPrivilegesRepository SysDataPrivileges => _sysDataPrivileges ?? (_sysDataPrivileges = new SysDataPrivilegesRepository(Connection, _config));
        public ISysLeaderRepository SysLeader => _sysLeader ?? (_sysLeader = new SysLeaderRepository(Connection, _config));
        public ISysDeptLeaderRepository SysDeptLeader => _sysDeptLeader ?? (_sysDeptLeader = new SysDeptLeaderRepository(Connection, _config));
        public ISysWorkflowsqlRepository SysWorkflowsql => _sysWorkflowsql ?? (_sysWorkflowsql = new SysWorkflowsqlRepository(Connection, _config));

        public ISysScheduleRepository SysSchedule => scheduleRepository ?? (scheduleRepository = new SysScheduleRepository(Connection, _config));

    }
    public class SysLogDbContext : DapperDbContext, ISysLogDbContext
    {
        private readonly SqlGeneratorConfig _config = new SqlGeneratorConfig
        {
            SqlConnector = ESqlConnector.MySQL,
            UseQuotationMarks = true
        };
        public SysLogDbContext(string connectionString)
            : base(new MySqlConnection(connectionString))
        {

        }
        private ISysLogRepository _sysLog;
        public ISysLogRepository SysLog => _sysLog ?? (_sysLog = new SysLogRepository(Connection, _config));
    }
}
