using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using Autofac;
using Common.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using Remboard.Auth;
using Remboard.Infrastructure;
using Remboard.Infrastructure.BaseControllers;
using Users;

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

            services.AddControllersWithViews(config =>
            {
                //config.Conventions.Add();
            }).AddNewtonsoftJson(options =>
                options.SerializerSettings.ContractResolver =
                    new CamelCasePropertyNamesContractResolver()).ConfigureApplicationPartManager(ap =>
            {
                //��������� Configuration["Modules:ComposerAssembly"]; -������ ������ ContainerBuilder � ������� ConfigureContainer
                // � ���� ��� ������

                var temporaryBuilder = new ContainerBuilder();
                temporaryBuilder.RegisterType<GenericControllerFeatureProvider>();
                ConfigureContainer(temporaryBuilder);

                ap.FeatureProviders.Add(temporaryBuilder.Build().Resolve<GenericControllerFeatureProvider>());
            });

            services.AddHttpContextAccessor();

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
        /// ������ ����� ���������� 2 ����.
        /// ������ ���, ����� �������� ��������� application parts.
        /// � ������ ��� ������, ����� ��������������� ContainerBuilder ������ ����������.
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            var assemblyPath = GetComposerAssemblyPath();
            var composerAssembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(assemblyPath);
            //var composerAssembly = Assembly.LoadFile(assemblyPath);
            builder.RegisterAssemblyModules(composerAssembly);
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
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            DiagnosticListener.AllListeners.Subscribe(new EfGlobalListener());

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

           
        }
    }
}
