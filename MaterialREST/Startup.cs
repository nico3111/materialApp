/**
 *
 * $$$$$$$\                                          $$\             $$\                                    $$\
 * $$  __$$\                                         $$ |            $$ |                                   $$ |
 * $$ |  $$ | $$$$$$\  $$\   $$\ $$$$$$$\   $$$$$$$\ $$$$$$$\        $$ |     $$\   $$\ $$$$$$$\   $$$$$$$\ $$$$$$$\
 * $$$$$$$\ |$$  __$$\ $$ |  $$ |$$  __$$\ $$  _____|$$  __$$\       $$ |     $$ |  $$ |$$  __$$\ $$  _____|$$  __$$\
 * $$  __$$\ $$ |  \__|$$ |  $$ |$$ |  $$ |$$ /      $$ |  $$ |      $$ |     $$ |  $$ |$$ |  $$ |$$ /      $$ |  $$ |
 * $$ |  $$ |$$ |      $$ |  $$ |$$ |  $$ |$$ |      $$ |  $$ |      $$ |     $$ |  $$ |$$ |  $$ |$$ |      $$ |  $$ |
 * $$$$$$$  |$$ |      \$$$$$$  |$$ |  $$ |\$$$$$$$\ $$ |  $$ |      $$$$$$$$\\$$$$$$  |$$ |  $$ |\$$$$$$$\ $$ |  $$ |
 * \_______/ \__|       \______/ \__|  \__| \_______|\__|  \__|      \________|\______/ \__|  \__| \_______|\__|  \__|
 *
 *
 *
 *                                     $$\      $$\            $$$$$$\  $$\
 *                                     $$$\    $$$ |          $$  __$$\ \__|
 *                                     $$$$\  $$$$ | $$$$$$\  $$ /  \__|$$\  $$$$$$\
 *                                     $$\$$\$$ $$ | \____$$\ $$$$\     $$ | \____$$\
 *                                     $$ \$$$  $$ | $$$$$$$ |$$  _|    $$ | $$$$$$$ |
 *                                     $$ |\$  /$$ |$$  __$$ |$$ |      $$ |$$  __$$ |
 *                                     $$ | \_/ $$ |\$$$$$$$ |$$ |      $$ |\$$$$$$$ |
 *                                     \__|     \__| \_______|\__|      \__| \_______|
 *
 *
 * Version 2.0
 * Author: BrunchFamily
 * copyright BrunchLunchMafia
 * */

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MaterialREST
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
            services.AddControllersWithViews().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddCors(options =>
            {
                options.AddPolicy("Policy1",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                                .AllowAnyMethod()
                                .AllowAnyHeader();
                    });
            });
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors("Policy1");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}