using System.Collections.Generic;
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
    /// Ӧ�ó��������Ƕ��ʽ�Զ������
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


            //���Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1.0",
                    Title = "Ray WebApi",
                    Description = "��ܼ���",
                    Contact = new OpenApiContact
                    { Name = "Ray", Email = "xuhaixia200@163.com" }
                });
                //��Ӷ�ȡע�ͷ���
                var xmlFile = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(xmlFile, "APIHelp.xml");
                var xmlFil = $"{ Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //var XMLPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                //��ӶԿ������ı�ǩ���� (true��ʾ��ʾ������ע��)
                c.IncludeXmlComments(xmlPath, true);
                //��ӶԿ������ı�ǩ����
                //  c.DocumentFilter<SwaggerDocTag>();


                //���header��֤��Ϣ
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT��Ȩ(���ݽ�������ͷ�н��д���)ֱ�����¿�������Bearer {token} (ע������֮ǰ��һ���ո�) \"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement() 
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference=new OpenApiReference
                              {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                              }
                        },new string[]{ }
                    }
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
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiHelp V1");
                //��RoutePrefix��������Ϊ��
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
