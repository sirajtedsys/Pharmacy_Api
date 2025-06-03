using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
//using Pharmacy.Class.DbModel;
using Pharmacy.Data.DbModel;
using Oracle.ManagedDataAccess.Client;
using System.Xml;


namespace Pharmacy.Class
{
    public class AppDbContext : DbContext
    {

        protected readonly IConfiguration _configuration;
        protected readonly IServiceProvider _serviceProvider;

        public AppDbContext(IServiceProvider serviceProvider, DbContextOptions<AppDbContext> options, IConfiguration configuration)
             : base(options)
        {
            _configuration = configuration;
            _serviceProvider = serviceProvider;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //if (!optionsBuilder.IsConfigured)
            //{
            //    optionsBuilder.UseOracle("User Id=SYSTEM;Password=/:t3d5y5/;Data Source=FORA11G3;"
            //        );
            //}

            //var dbName = httpContextAccessor.HttpContext.Request.Headers["Bookit"].First();

            var httpContextAccessor = _serviceProvider.GetRequiredService<IHttpContextAccessor>();
            string connection = _configuration.GetConnectionString("OracleDbContext");
            optionsBuilder.UseOracle(connection);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            #region DHMMASTER

            modelBuilder.Entity<DHMMASTER_LoginSettings>().ToTable("APP_LOGIN_SETTINGS", schema: "DHMMASTER");

            //modelBuilder.Entity<DHMMASTER_ProjectLoginSettings>().ToTable("APP_PROJECT_LOGIN_SETTINGS", schema: "DHMMASTER");
            modelBuilder.Entity<DHMMASTER_Hrm_Employee>().ToTable("HRM_EMPLOYEE", schema: "DHMMASTER");

            #endregion DHMMASTER

         
             
             
        }

        //SELECT ausr_pwd, ausr_username, ausr_id, ausr_username, ausr_status FROM UCHEMR.EMR_ADMIN_USERS WHERE ausr_status<>'D' AND ausr_username = 'tedsys' AND ausr_pwd = 'ted@123'

        #region DHMMASTER

        public DbSet<DHMMASTER_LoginSettings> APP_LOGIN_SETTINGS { get; set; }

        //public DbSet<DHMMASTER_ProjectLoginSettings> APP_PROJECT_LOGIN_SETTINGS { get; set; }
        public DbSet<DHMMASTER_Hrm_Employee> HRM_EMPLOYEE { get; set; }

        #endregion DHMMASTER

 


       


    }
}
