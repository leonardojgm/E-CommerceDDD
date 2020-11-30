using ApplicationApp.Interfaces;
using ApplicationApp.OpenApp;
using Domain.Interfaces.Generics;
using Domain.Interfaces.InterfaceCompra;
using Domain.Interfaces.InterfaceCompraUsuario;
using Domain.Interfaces.InterfaceLogSistema;
using Domain.Interfaces.InterfaceProduct;
using Domain.Interfaces.InterfaceServices;
using Domain.Services;
using Entities.Entities;
using Infraestructure.Configuration;
using Infraestructure.Repository.Generics;
using Infraestructure.Repository.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Threading.Tasks;
using Web_ECommerce.Token;

namespace Web_ECommerce
{
    public class Startup
    {
        #region Construtores

        public Startup(IConfiguration configuration) { Configuration = configuration; }

        #endregion

        #region Propriedades

        public IConfiguration Configuration { get; }

        #endregion

        #region Métodos

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ContextBase>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ContextBase>();
            services.AddControllersWithViews();
            services.AddRazorPages();

            #region Injeção de dependências

            //Interface e Repositório
            services.AddSingleton(typeof(IGeneric<>), typeof(RepositoryGenerics<>));
            services.AddSingleton<IProduct,RepositoryProduct>();
            services.AddSingleton<ICompraUsuario, RepositoryCompraUsuario>();
            services.AddSingleton<ICompra, RepositoryCompra>();
            services.AddSingleton<ILogSistema, RepositoryLogSistema>();

            //Interface Aplicação
            services.AddSingleton<InterfaceProductApp, AppProduct>();
            services.AddSingleton<InterfaceCompraUsuarioApp, AppCompraUsuario>();
            services.AddSingleton<InterfaceCompraApp, AppCompra>();
            services.AddSingleton<InterfaceLogSistemaApp, AppLogSistema>();

            //Interface Serviço
            services.AddSingleton<IServiceProduct, ServiceProduct>();
            services.AddSingleton<IServiceCompraUsuario, ServiceCompraUsuario>();

            #endregion

            #region Json Web Token

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(option =>
            {
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = "Teste.Security.Bearer",
                    ValidAudience = "Teste.Security.Bearer",
                    IssuerSigningKey = JwtSecurityKey.Create("Secret_Key-12345678"),
                };

                option.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        Console.WriteLine("OnAuthenticationFailed: " + context.Exception.Message);

                        return Task.CompletedTask;
                    },

                    OnTokenValidated = context =>
                    {
                        Console.WriteLine("OnTokenValidated: " + context.SecurityToken);

                        return Task.CompletedTask;
                    }
                };
            });

            services.AddAuthorization(options => { options.AddPolicy("UsuarioAPI", policy => policy.RequireClaim("UsuarioAPINumero")); });

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }

        #endregion
    }
}