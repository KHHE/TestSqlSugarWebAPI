using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SqlSugar;
using Entity;
using System;
using TestSqlSugarWebAPI.Filter;
using TestSqlSugarWebAPI.AuthorizeHandler;

namespace TestSqlSugarWebAPI
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
            //添加全局异常处理类
            services.AddControllers(options =>
            {
                options.Filters.Add<GlobalExceptionFilter>();
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TestSqlSugarWebAPI", Version = "v1", Description = "实现工厂接口" });
                c.IncludeXmlComments("TestSqlSugarWebAPI.xml", true);
            });

            //添加WebApi访问认证处理控制中心
            services.AddAuthentication(options =>  
            {
                options.DefaultScheme = ApiAuthorizeHandler.SCHEME_NAME;//指定默认授权方案，所有请求都会进行验证
                options.AddScheme<ApiAuthorizeHandler>(ApiAuthorizeHandler.SCHEME_NAME, ApiAuthorizeHandler.SCHEME_NAME);
                //options.DefaultAuthenticateScheme = ApiAuthorizeHandler.SCHEME_NAME;
                //options.DefaultChallengeScheme = ApiAuthorizeHandler.SCHEME_NAME;
            });

            //添加数据库SqlSugar
            services.AddSingleton<ISqlSugarClient>(s =>
            {
                SqlSugarScope sqlSugar = new SqlSugarScope(new ConnectionConfig()
                {
                    DbType = DbType.SqlServer,
                    ConnectionString = Configuration.GetConnectionString("DebugSqlConnection"),
                    IsAutoCloseConnection = true,
                },
               db =>
               {
                   CreateCodeFirstTable(db);
               });

                return sqlSugar;
            });

        }
        

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => {
                    //添加WebApi访问密钥，不携带密钥无法访问成功，请求头添加：定义的AUTH_KEY，密钥：定义的AUTH_KEY
                    c.UseRequestInterceptor($@"(req) => {{ req.headers['{ApiAuthorizeHandler.AUTH_KEY}']='{ApiAuthorizeHandler.AUTH_PWD}';return req;}}");
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "TestSqlSugarWebAPI v1");
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();    //添加WebApi访问认证
            app.UseAuthorization();     //添加WebApi访问授权

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }


        /// <summary>
        /// 自动根据实体类创建表
        /// </summary>
        /// <param name="db"></param>
        public void CreateCodeFirstTable(SqlSugarClient db)
        {
            //语法2：                
            //Type[] types = typeof(Factory).Assembly.GetTypes()
            //.Where(it => it.FullName.Contains("Entity."))//命名空间过滤，当然你也可以写其他条件过滤
            //.ToArray();
            //db.CodeFirst.SetStringDefaultLength(200).InitTables(types);//根据types创建表
            db.CodeFirst.InitTables(typeof(Factory));
        }
    }
}
