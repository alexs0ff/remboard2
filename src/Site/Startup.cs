using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using Autofac;
using AutoMapper;
using Common.Data;
using Common.Extensions;
using Common.Features;
using Common.Features.TypeConverters;
using Common.Features.Users;
using Common.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Remboard.Auth;
using Remboard.Auth.Roles;
using Remboard.Infrastructure;
using Remboard.Infrastructure.BaseControllers;
using Users;
using Users.Api;
using IContainer = Autofac.IContainer;

namespace Remboard
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }

        public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            AssemblyLoadContext.Default.Resolving+=DefaultOnResolving;
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
            });
            string connection = Configuration.GetConnectionString("RemboardDb");

            services.AddDbContext<IdentityDbContext>(options =>
                options.UseSqlServer(
                    connection));

            services.AddDbContext<RemboardContext>
                (options => options.UseSqlServer(connection));

            services.AddDefaultIdentity<IdentityUser>().AddEntityFrameworkStores<IdentityDbContext>();

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                    };
                });

            var temporaryContainer = CreateTemporaryContainer();

            services.AddControllersWithViews(config =>
            {

                //config.Conventions.Add();
            }).AddNewtonsoftJson(options =>
                    {
                        options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
						options.SerializerSettings.Converters.Add(new StringEnumConverter(typeof(CamelCaseNamingStrategy)));
                        
                    }
                )
                .ConfigureApplicationPartManager(ap =>
                {
                    ap.FeatureProviders.Add(temporaryContainer.Resolve<GenericControllerFeatureProvider>());
                });

            services.AddHttpContextAccessor();

            services.AddAuthorization(options =>
            {
                //options.AddPolicy("TenantedOnly", policy => policy.RequireClaim(RemboardClaims.Tenant));
            });

            services.AddSingleton<IAuthorizationHandler, CrudAuthorizationHandler>();

            services.Configure<MvcOptions>(c =>
            {
                c.Conventions.Add(temporaryContainer.Resolve<PluralActionNameConvention>());
                //ReplaceFromBodyProvider(c);
                //c.ModelBinderProviders.Insert(0, new CustomModelBinderProvider());
            });
        }

        private IContainer CreateTemporaryContainer()
        {
            //Загружаем Configuration["Modules:ComposerAssembly"]; -просто создав ContainerBuilder и вызывав ConfigureContainer
            // и ищем все сборки

            var temporaryBuilder = new ContainerBuilder();
            temporaryBuilder.RegisterType<GenericControllerFeatureProvider>();
            temporaryBuilder.RegisterType<PluralActionNameConvention>();
            temporaryBuilder.RegisterType<PluralActionNameConvention>().PropertiesAutowired();
            ConfigureContainer(temporaryBuilder);
            var temporaryContainer = temporaryBuilder.Build();
            return temporaryContainer;
        }

        private Assembly DefaultOnResolving(AssemblyLoadContext arg1, AssemblyName arg2)
        {
            var assemblyPath = GetComposerAssemblyPath();
            assemblyPath = Path.GetDirectoryName(assemblyPath);
            assemblyPath = Path.Combine(assemblyPath, arg2.Name + ".dll");

            if (!File.Exists(assemblyPath))
            {
                return null;
            }
            return arg1.LoadFromAssemblyPath(assemblyPath);
        }

        /// <summary>
        /// Данный метод вызывается 2 раза.
        /// Первый раз, когда временно загружаем application parts.
        /// И второй раз неявно, когда конфигурируется ContainerBuilder уровня приложения.
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            var assemblyPath = GetComposerAssemblyPath();
            var composerAssembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(assemblyPath);
            //var composerAssembly = Assembly.LoadFile(assemblyPath);
            builder.RegisterAssemblyModules(composerAssembly);

            builder.RegisterType<CurrentIdentityInfoProvider>().As<ICurrentIdentityInfoProvider>();

            builder.RegisterInstance(new RemboardContextParameters { IsDesignTime = false });


			builder.Register<AutoMapper.IConfigurationProvider>(ctx =>
            {
                var profiles = ctx.Resolve <IEnumerable<FeatureMapperProfile>>();
                return new MapperConfiguration(cfg => cfg.AddProfiles(profiles));
            });


            builder.Register<IMapper>(ctx =>
            {
                var provider = ctx.Resolve<AutoMapper.IConfigurationProvider>();
                return new Mapper(provider);
            }).InstancePerDependency();

        }

        private string GetComposerAssemblyPath()
        {
            var assemblyPath = Configuration["Modules:ComposerAssembly"];
            var fullPath = Environment.ContentRootPath;
            assemblyPath = Path.Combine(fullPath, assemblyPath);
            return assemblyPath;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                DiagnosticListener.AllListeners.Subscribe(new EfGlobalListener());
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });

            //wait for resolving https://github.com/aspnet/AspNetCore/issues/8857
        }


        private static void ReplaceFromBodyProvider(MvcOptions c)
        {
            var provider = c.ModelBinderProviders.FirstOrDefault(p =>
                    p.GetType() == typeof(Microsoft.AspNetCore.Mvc.ModelBinding.Binders.BodyModelBinderProvider)) as
                Microsoft.AspNetCore.Mvc.ModelBinding.Binders.BodyModelBinderProvider;

            var formatters =
                SystemHelpers
                    .GetInstanceField<Microsoft.AspNetCore.Mvc.ModelBinding.Binders.BodyModelBinderProvider,
                        IList<IInputFormatter>>(provider, "_formatters");
            var readerFactory =
                SystemHelpers
                    .GetInstanceField<Microsoft.AspNetCore.Mvc.ModelBinding.Binders.BodyModelBinderProvider,
                        IHttpRequestStreamReaderFactory>(provider, "_readerFactory");
            var loggerFactory =
                SystemHelpers
                    .GetInstanceField<Microsoft.AspNetCore.Mvc.ModelBinding.Binders.BodyModelBinderProvider, ILoggerFactory>(
                        provider, "_loggerFactory");
            var localProvider =
                new Common.Features.Binders.BodyModelBinderProvider(formatters, readerFactory, loggerFactory, c);
            c.ModelBinderProviders.Remove(provider);
            c.ModelBinderProviders.Insert(1, localProvider);
        }
    }
}
