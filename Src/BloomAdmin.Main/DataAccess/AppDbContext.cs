using BloomAdmin.Main.Config;
using BloomAdmin.Main.DataAccess.Entity;
using Microsoft.EntityFrameworkCore;

namespace BloomAdmin.Main.DataAccess
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public AppDbContext()
        {
        }

        public DbSet<UserEntity> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var sqlServerOptions = optionsBuilder.UseSqlServer(
                        AppConfig.DbCoonString ?? throw new InvalidOperationException("未找到数据库连接字符串"),
                        sqlServerOptionsAction: sqlServerOptions =>
                        {
                            sqlServerOptions.CommandTimeout(300);
                        });
            //开启敏感数据日志
            sqlServerOptions.EnableSensitiveDataLogging();
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // 如果有基类配置

            // 配置 User 实体
            modelBuilder.Entity<UserEntity>(entity =>
            {
                entity.ToTable("sys_user");
                entity.HasKey(u => u.UserId);

                entity.Property(u => u.UserId).ValueGeneratedNever();
                entity.Property(u => u.Account).HasColumnType("varchar(50)").IsRequired();
                entity.Property(u => u.Password).HasColumnType("varchar(64)").IsRequired();
                entity.Property(u => u.UserName).HasColumnType("nvarchar(50)").IsRequired();
                entity.Property(u => u.Gender).HasColumnType("int").IsRequired();
                entity.Property(u => u.Email).HasColumnType("varchar(128)");
                entity.Property(u => u.Avatar).HasColumnType("varchar(256)");
                entity.Property(u => u.CreateAt).HasColumnType("datetime").IsRequired();
                entity.Property(u => u.UpdateAt).HasColumnType("datetime");
            });
        }
    }
}