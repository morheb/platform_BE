using ConnectMediaEF.Models;
using Microsoft.EntityFrameworkCore;

namespace Platform_BE.Model
{
    
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Tasks> Tasks { get; set; }
        public DbSet<userTasks> UserTasks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            //optionBuilder.UseMySQL("Server= 162.241.252.224,3306;Database= qeiapmmy_manage_app;uid = qeiapmmy_platform-user;pwd=akR{1@2]4vgp;Pooling=false;");
            optionBuilder.UseMySQL("Server = 162.241.252.224; Port = 3306; Database = qeiapmmy_manage_app; Uid = qeiapmmy_platform-user; Pwd = akR{1@2]4vgp;");

        }

    }
}
