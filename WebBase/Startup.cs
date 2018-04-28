using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Reflection;
using WebBase.Autofac;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using WebExtentions.DependencyInjection;
using WebBase.Filters;

namespace WebBase
{
    public class Startup
    {
        public Startup(IConfiguration configuration, ILoggerFactory logger, IServiceProvider serviceProvider)
        {
            Configuration = configuration;
            LoggerFactory = logger;
            ServiceProvider = serviceProvider;
        }

        public IServiceProvider ServiceProvider { get; }

        public IConfiguration Configuration { get; }

        public ILoggerFactory LoggerFactory { get; }

        private IEnumerable<object> serviceInjPart;

        //依赖注入配置,替换默认服务容器
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //注册Session支持
            //Session 存储方式可以使用如下方式
            //无优化存储内存
            //services.AddMemoryCache();
            //存储到Redis服务器上
            string redis = Configuration.GetConnectionString("Redis");
            services.AddDistributedRedisCache(ss => ss.Configuration = redis);
            //存储到SqlServer服务器上，参数为SqlServer中的表，字段，连接等配置
            //services.AddDistributedSqlServerCache(ss => ss.ConnectionString = conStr);
            //使用有优化的内存缓存
            //services.AddDistributedMemoryCache();
            // services.AddMysqlCache(ss => ss.ConnectionString = Configuration.GetConnectionString("Mysql"));
            //添加Session支持
            services.AddSession();

            //注册跨域资源共享
            // services.AddCors();

            services.AddSingleton<ExceptionFilter>();
            //替换Controller为服务实现,必须放在 services.AddMvc() 之前
            services.Replace(ServiceDescriptor.Scoped<IControllerActivator, ServiceBasedControllerActivator>());
            var mvc = services.AddMvc(ss => ss.Filters.AddService<ExceptionFilter>())
                         .AddJsonOptions(ss =>
                         {
                             ss.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
                             ss.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
                             ss.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                             ss.SerializerSettings.DateParseHandling = DateParseHandling.DateTime;
                             ss.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                         });
            //加入日志
            services.AddLogging();

            //注册配置文件字段
            services.AddOptions();
            //将配置文件节注册到实体中
            //services.Configure<Entity>(Configuration.GetSection("sss"));

            //注册EF Core
            string conStr = Configuration.GetConnectionString("Mysql");


            //如果使用SqlServer，则替换上两句为下面两句
            //services.AddEntityFrameworkSqlServer();
            //services.AddDbContext<EntityFrameworkDbContext>(ss => ss.UseSqlServer(conStr));

            //创建Autofac依赖注入容器,替换系统自带的默认依赖注入容器
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterAssemblyModules(typeof(Startup).Assembly);
            LoadAndRegister(containerBuilder, mvc, services);
            containerBuilder.Populate(services);
            var container = containerBuilder.Build();
            return new AutofacServiceProvider(container);
        }

        private object[] GetParametersObj(ParameterInfo[] paramTypes)
        {
            if (paramTypes == null)
                return null;
            object[] ins = new object[paramTypes.Length];
            for (int i = 0; i < paramTypes.Length; i++)
            {
                ins[i] = ServiceProvider.GetService(paramTypes[i].ParameterType);
            }
            return ins;
        }

        //读取并加载Autofac模块
        private void LoadAndRegister(ContainerBuilder containerBuilder, IMvcBuilder mvc, IServiceCollection service)
        {
            ILogger logger = LoggerFactory.CreateLogger<Startup>();
            string current = Directory.GetCurrentDirectory();
            var loaded = Configuration.GetSection("AutofacLoadPath");
            var v = loaded.GetChildren();
            List<object> sijObjs = new List<object>();
            serviceInjPart = sijObjs;
            foreach (var item in v)
            {
                string name = item.Value;
                string path = Path.Combine(current, name);
                logger.LogInformation($"Loading assembly in {path}");
                DirectoryInfo dir = new DirectoryInfo(path);
                if (dir.Exists)
                {
                    foreach (var file in dir.EnumerateFiles("*.dll", SearchOption.TopDirectoryOnly))
                    {
                        try
                        {
                            logger.LogInformation($"Loading assembly {file.FullName}");
                            Assembly assembly = Assembly.LoadFrom(file.FullName);
                            mvc.AddApplicationPart(assembly);
                            containerBuilder.RegisterAssemblyModules(assembly);
                            var all = from p in assembly.GetTypes()
                                      where !p.IsAbstract &&
                                      !p.IsInterface &&
                                       p.GetCustomAttribute<StartupAttribute>() != null &&
                                       p.IsClass
                                      select p;
                            foreach (var sij in all)
                            {

                                //var constuctor = sij.GetConstructors().FirstOrDefault();
                                //if (constuctor == null)
                                //    continue;
                                var method = sij.GetMethod("ConfigureService");
                                if (method == null)
                                    continue;
                                //var pars = constuctor.GetParameters();
                                //var ins = GetParametersObj(pars);
                                //var sijobj = Activator.CreateInstance(sij, ins);
                                var factory = ActivatorUtilities.CreateFactory(sij, Type.EmptyTypes);
                                var sijobj = factory(ServiceProvider, null);
                                var pars = method.GetParameters();
                                var ins = GetParametersObj(pars);
                                if (pars[0].ParameterType == typeof(IServiceCollection))
                                    ins[0] = service;
                                method.Invoke(sijobj, ins);
                                sijObjs.Add(sijobj);

                            }
                        }
                        catch (Exception ex)
                        {
                            logger.LogError(ex, ex.Message);
                            continue;
                        }
                    }
                }
                else
                {
                    logger.LogWarning($"path {path} not exist ");
                }
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseSession();

            IServiceProvider provider = app.ApplicationServices;
            if (serviceInjPart != null)
            {
                foreach (var item in serviceInjPart)
                {
                    Type type = item.GetType();
                    var method = type.GetMethod("Configure");
                    if (method == null)
                        continue;
                    var param = method.GetParameters();
                    var ins = GetParametersObj(param);
                    if (param[0].ParameterType == typeof(IApplicationBuilder))
                        ins[0] = app;
                    method.Invoke(item, ins);
                }
            }

            app.UseMvc();
        }
    }
}
