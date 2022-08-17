using CoreMVCIntro_1.DBConfiguration;
using CoreMVCIntro_1.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVCIntro_1.Models.Context
{

    //EntityFrameworkCore.SqlServer kütüphanesini indirmeyi unutmayın...Options ayarları yoksa gelmeyecektir...
    public class MyContext:DbContext
    {
        //Dependency Injection yapısı Core platformunuzun arkasında otomatik olarak entegre gelir... Dolayısıyla siz bir veritabanı sınıfınızın constructor'ina parametre olarak bir DbContextOptions<> tipinde bir yapı verirseniz bu parametreye argüman DI sayesinde Startup'dan gönderilir...


        //public MyContext(DbContextOptionsBuilder options)
        //{
        //    options.UseSqlServer(connectionString: "server=.;database=CoreDB;uid=sa;pwd=123");
        //}

        public MyContext(DbContextOptions<MyContext> options):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OrderDetailConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new AppUserConfiguration());
            modelBuilder.ApplyConfiguration(new ProfileConfiguration());
        }

        //.Net Core üzerinden migrate yapmak istediginiz takdirde add-migration <isim> ve sonrasında update-database demeniz gerekir...

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<UserProfile> Profiles { get; set; }

    }
}
