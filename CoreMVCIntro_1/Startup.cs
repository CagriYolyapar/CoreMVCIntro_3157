using CoreMVCIntro_1.Models.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVCIntro_1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();


            //Hangi servisin eklenecegini belirliyorsunuz... Bazı servisler eklendiklerinde otomatik olarak kullanımı alınırken bazı servisleri de ekledikten sonra alttaki Configure metodunda özellikle kullanacagınız belirtmeniz gerekiyor...

            //Bureda standart bir Sql baglantısı belirtmek istiyorsanız(sınıf icerisinde optionsBuilder'in belirtilmesindense bu tercih edilir) su ifadeyi yazmalısınız...

            //Pool kullanmak bir Singleton Pattern görevi görür
            services.AddDbContextPool<MyContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MyConnection")).UseLazyLoadingProxies()); //böylece baglantı ayarlarını burada belirtmiş olduk...

            //Yukarıdaki ifadede dikkat ederseniz UseLazyLoadingProxies kullanılmıstır...Bu durum .Net Core'daki Lazy Loading'in sürekli tetiklenebilmesi adına environment'inizi garanti altına almanızı saglar...

            services.AddSession(x =>
            {
                x.IdleTimeout = TimeSpan.FromMinutes(20);
                x.Cookie.HttpOnly = true;
                x.Cookie.IsEssential = true;
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {



            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSession();

            app.UseHttpsRedirection();

            app.UseStaticFiles(); //wwwroot isimli klasorun projeye acılması icin gerekli olan ifadedir...

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {

                endpoints.MapControllerRoute(
                    name: "ozel",
                    pattern:"Kategori/Urunler",
                    new {Controller="Category",Action = "CategoryProductList"}
                    );

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Category}/{action=Index}/{id?}");
            });
        }
    }
}
