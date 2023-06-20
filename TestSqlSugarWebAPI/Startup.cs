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
            //���ȫ���쳣������
            services.AddControllers(options =>
            {
                options.Filters.Add<GlobalExceptionFilter>();
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TestSqlSugarWebAPI", Version = "v1", Description = "ʵ�ֹ����ӿ�" });
                c.IncludeXmlComments("TestSqlSugarWebAPI.xml", true);
            });

            //������ݿ�SqlSugar
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
                   //�����������ã�������������Ч
                   db.Aop.OnLogExecuting = (sql, pars) =>
                               {
                                   Console.WriteLine(UtilMethods.GetNativeSql(sql, pars));
                                   //��ȡIOC����Ҫ����һ��������
                                   //vra log=s.GetService<Log>()

                                   //��ȡIOC����Ҫ����һ��������
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
        /// �Զ�����ʵ���ഴ����
        /// </summary>
        /// <param name="db"></param>
        public void CreateCodeFirstTable(SqlSugarClient db)
        {
            //�﷨2��                
            //Type[] types = typeof(Factory).Assembly.GetTypes()
            //.Where(it => it.FullName.Contains("Entity."))//�����ռ���ˣ���Ȼ��Ҳ����д������������
            //.ToArray();
            //db.CodeFirst.SetStringDefaultLength(200).InitTables(types);//����types������
            db.CodeFirst.InitTables(typeof(Factory));
        }
    }
}
