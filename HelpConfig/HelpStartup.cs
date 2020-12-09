using ApplicationApp.Interfaces;
using ApplicationApp.OpenApp;
using Domain.Interfaces.Generics;
using Domain.Interfaces.InterfaceCompra;
using Domain.Interfaces.InterfaceCompraUsuario;
using Domain.Interfaces.InterfaceLogSistema;
using Domain.Interfaces.InterfaceProduct;
using Domain.Interfaces.InterfaceServices;
using Domain.Interfaces.InterfaceUsuario;
using Domain.Services;
using Infraestructure.Repository.Generics;
using Infraestructure.Repository.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace HelpConfig
{
    public static class HelpStartup
    {
        #region Métodos

        public static void ConfigureSingleton(IServiceCollection services)
        {
            #region Injeção de dependências

            //Interface e Repositório
            services.AddSingleton(typeof(IGeneric<>), typeof(RepositoryGenerics<>));
            services.AddSingleton<IProduct, RepositoryProduct>();
            services.AddSingleton<ICompraUsuario, RepositoryCompraUsuario>();
            services.AddSingleton<ICompra, RepositoryCompra>();
            services.AddSingleton<ILogSistema, RepositoryLogSistema>();
            services.AddSingleton<IUsuario, RepositoryUsuario>();

            //Interface Aplicação
            services.AddSingleton<InterfaceProductApp, AppProduct>();
            services.AddSingleton<InterfaceCompraUsuarioApp, AppCompraUsuario>();
            services.AddSingleton<InterfaceCompraApp, AppCompra>();
            services.AddSingleton<InterfaceLogSistemaApp, AppLogSistema>();
            services.AddSingleton<InterfaceUsuarioApp, AppUsuario>();
            services.AddSingleton<InterfaceMontaMenuApp, AppMontaMenu>();

            //Interface Serviço
            services.AddSingleton<IServiceProduct, ServiceProduct>();
            services.AddSingleton<IServiceCompraUsuario, ServiceCompraUsuario>();
            services.AddSingleton<IServiceUsuario, ServiceUsuario>();
            services.AddSingleton<IServiceMontaMenu, ServiceMontaMenu>();

            #endregion
        }

        #endregion
    }
}
