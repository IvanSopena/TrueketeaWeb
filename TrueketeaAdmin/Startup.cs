using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

using TrueketeaAdmin.Services.DB;
using TrueketeaAdmin.Services.FirebaseServices;
//using TrueketeaAdmin.Services.FirebaseServices;


namespace TrueketeaAdmin
{
    public class Startup
    {
        public readonly static DBContext dbContext = new DBContext();
        public readonly static MongoServiceDB mongo = new MongoServiceDB();

        private string server;
        private string db_name;
        private string user;
        private string pwd;
        private string section;

        public Startup(IConfiguration configuration)
        {
            
            Configuration = configuration;
            MachineName = System.Environment.MachineName;



             switch(MachineName)
             {
                case "ES5CD0179Z6R":
                    section = "ConnectionWork";
                    break;
                case "iMac-Pro-de-Ivan":
                    section = "ConnectioniMac";
                    break;
                case "MacBook-Pro-de-ivan":
                    section = "ConnectionMacBook";
                    break;
                default:
                    section = "ConnectionHome";
                    break;
             }

           

        }

        public void conection_db()
        {
            var connec_data = Configuration.GetSection(section).GetChildren().ToArray();

            this.server = connec_data[2].Value;
            this.db_name = connec_data[0].Value;
            this.user = connec_data[3].Value;
            this.pwd = connec_data[1].Value;

            dbContext.Conexion(this.user, this.pwd, this.server, this.db_name);
        }

        public IConfiguration Configuration { get; }
        public string MachineName { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddScoped<IJavaScriptService, JavaScriptService>();
            services.AddNodeServices();
            

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
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            this.conection_db();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
