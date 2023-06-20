using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SqlSugar;
using Entity;
using System;
using Utils;
using TestSqlSugarWebAPI.Filter;

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

            //添加数据库SqlSugar
            services.AddSingleton<ISqlSugarClient>(s =>
            {
                SqlSugarScope sqlSugar = new SqlSugarScope(new ConnectionConfig()
                {
                    DbType = SqlSugar.DbType.SqlServer,
                    ConnectionString = "Data Source=BIELITHZ356;Database=Test;Uid=sa;Pwd=6MonkeysRLooking^;Enlist=true;Pooling=true;Connect TimeOut=3000;",
                    IsAutoCloseConnection = true,
                },
               db =>
               {
                   CreateCodeFirstTable(db);
                   //单例参数配置，所有上下文生效
                   db.Aop.OnLogExecuting = (sql, pars) =>
                               {
                                   Console.WriteLine(UtilMethods.GetNativeSql(sql, pars));
                                   //获取IOC对象不要求在一个上下文
                                   //vra log=s.GetService<Log>()

                                   //获取IOC对象要求在一个上下文
                                   //var appServive = s.GetService<IHttpContextAccessor>();
                                   //var log= appServive?.HttpContext?.RequestServices.GetService<Log>();
                               };
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
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TestSqlSugarWebAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

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
