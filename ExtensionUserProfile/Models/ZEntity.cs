using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace ExtensionUserProfile.Models
{
    // 实体类模型
    public class ZUserProfile
    {
        public int Id { get; set; }
        public string Item { get; set; }

        // 导航（和 ZUser 是多对一的关系）
        [ForeignKey("ZUser")]
        public string ZUserId { get; set; }// ApplicationUserId
        public virtual ZUser ZUser { get; set; }
    }

    // 继承 IdentityUser，相当于把ZUser当作ApplicationUser.
    public class ZUser : IdentityUser
    {
        // ApplicationUser和ZUserProfile 是一对多的关系。
        public virtual ICollection<ZUserProfile> ZUserProfile { get; set; }
    }

    /// <summary>
    /// 数据库迁移
    /// PM> Enable-Migrations -ContextTypeName ExtensionUserProfile.Models.ZUserContext -MigrationsDirectory Migrations\User
    /// PM> Add-Migration -ConfigurationTypeName ExtensionUserProfile.Migrations.User.Configuration "Init"
    /// PM> Update-Database -ConfigurationTypeName ExtensionUserProfile.Migrations.User.Configuration
    /// </summary>
    public class ZUserContext : IdentityDbContext<ZUser>
    {
        // 上下文字符串（若web.config里的的DefaultConnection是这个类名：ZUserContext，则可以不写下面的构造函数。
        public ZUserContext() : base("DefaultConnection") { }

        //// 更改默认的一些设置
        //// 加了这个OnModelCreating会出现一些问题：如EntityType“IdentityUserLogin”未定义键。请为该 EntityType 定义键等。
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    // 表名后加s
        //    modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

        //    modelBuilder.Entity<IdentityUserRole>().HasKey(t => new { t.RoleId, t.UserId });
        //    modelBuilder.Entity<IdentityUserLogin>().HasKey(t => new { t.LoginProvider,t.ProviderKey, t.UserId});
        //}

        // 将实体类加入上下文
        public DbSet<ZUserProfile> ZUserProfile { get; set; }
    }
}