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

            //���WebApi������֤�����������
            services.AddAuthentication(options =>  
            {
                options.DefaultScheme = ApiAuthorizeHandler.SCHEME_NAME;//ָ��Ĭ����Ȩ�������������󶼻������֤
                options.AddScheme<ApiAuthorizeHandler>(ApiAuthorizeHandler.SCHEME_NAME, ApiAuthorizeHandler.SCHEME_NAME);
                //options.DefaultAuthenticateScheme = ApiAuthorizeHandler.SCHEME_NAME;
                //options.DefaultChallengeScheme = ApiAuthorizeHandler.SCHEME_NAME;
            });

            //������ݿ�SqlSugar
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
                    //���WebApi������Կ����Я����Կ�޷����ʳɹ�������ͷ��ӣ������AUTH_KEY����Կ�������AUTH_KEY
                    c.UseRequestInterceptor($@"(req) => {{ req.headers['{ApiAuthorizeHandler.AUTH_KEY}']='{ApiAuthorizeHandler.AUTH_PWD}';return req;}}");
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "TestSqlSugarWebAPI v1");
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();    //���WebApi������֤
            app.UseAuthorization();     //���WebApi������Ȩ

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
