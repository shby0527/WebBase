using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Autofac.Extras.DynamicProxy;
using Autofac.Extensions.DependencyInjection;
using Autofac;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Newtonsoft.Json;
using System.Reflection;
using System.IO;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Umi.Web.Metadatas.Configurations;
using Umi.Web.Filters;
using Umi.Web.Metadatas.Attributes;
using Castle.DynamicProxy;
using Umi.Web.Abstraction.Aspect;

namespace Umi.Web
{
    public class Startup
    {
        private readonly IServiceProvider serviceProvider;

        private readonly IConfiguration configuration;

        private readonly ILoggerFactory loggerFactory;

        private IEnumerable<object> serviceInjPart;

        public Startup(IConfiguration configuration, ILoggerFactory logger, IServiceProvider serviceProvider)
        {
            this.configuration = configuration;
            loggerFactory = logger;
            this.serviceProvider = serviceProvider;
            Regex.CacheSize = 10;
        }

        //依赖注入配置,替换默认服务容器
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //注册Session支持
            //Session 存储方式可以使用如下方式
            //无优化存储内存
            services.AddMemoryCache();

            if (configuration.GetValue<bool>("UseRedis"))
            {
                //存储到Redis服务器上
                string redis = configuration.GetConnectionString("Redis");
                services.AddDistributedRedisCache(ss => ss.Configuration = redis);
            }
            else
            {
                //使用分布式的内存缓存
                services.AddDistributedMemoryCache();
            }
            //存储到SqlServer服务器上，参数为SqlServer中的表，字段，连接等配置
            //services.AddDistributedSqlServerCache(ss => ss.ConnectionString = conStr);
            // services.AddMysqlCache(ss => ss.ConnectionString = Configuration.GetConnectionString("Mysql"));
            //添加Session支持
            services.AddSession(p =>
            {
                p.Cookie = new CookieBuilder()
                {
                    HttpOnly = true,
                    IsEssential = true,
                    Name = ".Umi.Session",
                    SameSite = SameSiteMode.None
                };
                p.IdleTimeout = TimeSpan.FromMinutes(40);
            });

            CorsConfiguration corsConfig = new CorsConfiguration();
            configuration.Bind("CorsConfiguration", corsConfig);

            //注册跨域资源共享
            //解释：https://developer.mozilla.org/en-US/docs/Web/HTTP/CORS
            //参考：http://www.ruanyifeng.com/blog/2016/04/cors.html
            services.AddCors(p =>
            {
                p.AddDefaultPolicy(cpb =>
                {
                    cpb.AllowCredentials();
                    cpb.WithExposedHeaders(corsConfig.ExposedHeaders ?? new string[0]);
                    cpb.WithHeaders(corsConfig.Headers ?? new string[0]);
                    cpb.WithMethods(corsConfig.Methods ?? new string[0]);
                    cpb.WithOrigins(corsConfig.Origins ?? new string[0]);
                });
            });

            //GDPR
            //参考：https://www.gdpr.net/
            //注解：https://docs.microsoft.com/zh-cn/aspnet/core/security/gdpr?view=aspnetcore-2.1
            //说明：http://www.cnblogs.com/GuZhenYin/p/9154447.html
            services.Configure<CookiePolicyOptions>(p =>
            {
                p.CheckConsentNeeded = ctx => false;
                p.MinimumSameSitePolicy = SameSiteMode.None;
                p.Secure = CookieSecurePolicy.None;
            });
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
            //string conStr = Configuration.GetConnectionString("Mysql");


            //如果使用SqlServer，则替换上两句为下面两句
            //services.AddEntityFrameworkSqlServer();
            //services.AddDbContext<EntityFrameworkDbContext>(ss => ss.UseSqlServer(conStr));

            //创建Autofac依赖注入容器,替换系统自带的默认依赖注入容器
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterAssemblyModules(typeof(Startup).Assembly);
            containerBuilder.RegisterType<TimeLoggerInterceptor>()
            .As<IInterceptor>()
            .Named("TimeLoggerInterceptor", typeof(IInterceptor))
            .PropertiesAutowired();
            containerBuilder.RegisterType<ExceptionInterceptor>()
            .As<IInterceptor>()
            .Named("ExceptionInterceptor", typeof(IInterceptor))
            .PropertiesAutowired();
            LoadAndRegister(containerBuilder, mvc, services);
            containerBuilder.Populate(services);
            var container = containerBuilder.Build();
            return new AutofacServiceProvider(container);
        }

        private object[] GetParametersObj(ParameterInfo[] paramTypes, IServiceProvider service)
        {
            if (paramTypes == null)
                return null;
            if (paramTypes.Length == 0)
                return new object[0];
            object[] ins = new object[paramTypes.Length];
            for (int i = 0; i < paramTypes.Length; i++)
            {
                ins[i] = service.GetService(paramTypes[i].ParameterType);
            }
            return ins;
        }

        //读取并加载Autofac模块
        private void LoadAndRegister(ContainerBuilder containerBuilder, IMvcBuilder mvc, IServiceCollection service)
        {
            ILogger logger = loggerFactory.CreateLogger<Startup>();
            string current = Directory.GetCurrentDirectory();
            var loaded = configuration.GetSection("AutofacLoadPath");
            var v = loaded.GetChildren();
            List<object> sijObjs = new List<object>();
            serviceInjPart = sijObjs;
            foreach (var item in v)
            {
                string name = item.Value;
                string path = Path.Combine(current, name);
                DirectoryInfo dir = new DirectoryInfo(path);
                logger.LogInformation($"Loading assembly in {dir.FullName}");
                if (dir.Exists)
                {
                    foreach (var file in dir.EnumerateFiles("*.dll", SearchOption.TopDirectoryOnly))
                    {
                        try
                        {
                            logger.LogInformation($"Loading assembly {file.FullName}");
                            Assembly assembly = Assembly.LoadFrom(file.FullName);
                            mvc.AddApplicationPart(assembly);
                            RegisterTypes(containerBuilder, assembly);
                            // 搜索ServiceAttribute注解的类进行注入
                            containerBuilder.RegisterAssemblyModules(assembly);
                            var all = from p in assembly.GetTypes()
                                      where !p.IsAbstract &&
                                      !p.IsInterface &&
                                       p.GetCustomAttribute<StartupAttribute>() != null &&
                                       p.IsClass
                                      select p;
                            //自动加载视图程序集
                            mvc.PartManager.ApplicationParts.Add(new CompiledRazorAssemblyPart(assembly));
                            foreach (var sij in all)
                            {
                                var factory = ActivatorUtilities.CreateFactory(sij, Type.EmptyTypes);
                                var sijobj = factory(serviceProvider, null);
                                sijObjs.Add(sijobj);
                                var method = sij.GetMethod("ConfigureService");
                                if (method == null)
                                    continue;
                                var pars = method.GetParameters();
                                var ins = GetParametersObj(pars, serviceProvider);
                                for (int i = 0; i < pars.Length; i++)
                                {
                                    if (pars[i].ParameterType == typeof(IServiceCollection))
                                        ins[i] = service;
                                    if (pars[i].ParameterType == typeof(IMvcBuilder))
                                        ins[i] = mvc;
                                }
                                method.Invoke(sijobj, ins);

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

        private void RegisterTypes(ContainerBuilder builder, Assembly assembly)
        {
            assembly.GetTypes()
            .Select(e =>
            {
                IEnumerable<ServiceAttribute> services = e.GetCustomAttributes<ServiceAttribute>();
                if (!services.Any())
                {
                    return e;
                }
                ServiceAttribute service = services.First();
                List<string> list = new List<string>();
                list.Add("TimeLoggerInterceptor");
                list.Add("ExceptionInterceptor");
                string[] interceptors = service.Interceptors ?? new string[0];
                list.AddRange(interceptors);
                var register = builder.RegisterType(e)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
                if (!string.IsNullOrEmpty(service.Name))
                {
                    foreach (var item in e.GetInterfaces())
                    {
                        register = register.Named(service.Name, item);
                    }
                }
                register.PropertiesAutowired()
                .EnableInterfaceInterceptors()
                .InterceptedBy(list.ToArray());
                return e;
            }).ToArray();
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
                    var ins = GetParametersObj(param, provider);
                    for (int i = 0; i < param.Length; i++)
                    {
                        if (param[i].ParameterType == typeof(IApplicationBuilder))
                            ins[i] = app;
                    }
                    method.Invoke(item, ins);
                }
            }

            app.UseMvc();

            if (serviceInjPart != null)
            {
                foreach (var item in serviceInjPart)
                {
                    Type type = item.GetType();
                    var method = type.GetMethod("ConfigureAfterMvc");
                    if (method == null)
                        continue;
                    var param = method.GetParameters();
                    var ins = GetParametersObj(param, provider);
                    for (int i = 0; i < param.Length; i++)
                    {
                        if (param[i].ParameterType == typeof(IApplicationBuilder))
                            ins[i] = app;
                    }
                    method.Invoke(item, ins);
                }
            }
        }

    }
}
