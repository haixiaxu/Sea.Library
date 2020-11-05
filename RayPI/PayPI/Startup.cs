using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;
using PayPI.SwaggerHelp;

namespace PayPI
{
    /// <summary>
    /// 应用程序所需的嵌入式自定义服务
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
         

            //添加Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1.0",
                    Title = "Ray WebApi",
                    Description = "框架集合",
                    Contact = new OpenApiContact
                    { Name = "Ray", Email = "xuhaixia200@163.com" }
                });
                //添加读取注释服务
                var xmlFile = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(xmlFile, "APIHelp.xml");
                var xmlFil =$"{ Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //var XMLPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                //添加对控制器的标签描述 (true表示显示控制器注释)
                c.IncludeXmlComments(xmlPath,true);
                //添加对控制器的标签描述
              //  c.DocumentFilter<SwaggerDocTag>();
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
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiHelp V1");
                //将RoutePrefix属性设置为空
                c.RoutePrefix = string.Empty;
            });



            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
