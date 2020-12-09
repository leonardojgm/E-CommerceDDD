using Domain.Interfaces.InterfaceServices;
using Domain.Interfaces.InterfaceUsuario;
using Entities.Entities;
using Entities.Entities.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class ServiceMontaMenu : IServiceMontaMenu
    {
        #region Construtores

        public ServiceMontaMenu(IUsuario IUsuario) { _IUsuario = IUsuario; }

        #endregion

        #region Propriedades

        private readonly IUsuario _IUsuario;

        #endregion

        #region Métodos

        public async Task<List<MenuSite>> MontaMenuPorPerfil(string userID)
        {
            var retorno = new List<MenuSite>();

            retorno.Add(new MenuSite { Controller = "Home", Action = "Index", Descricao = "Loja Virtual" });

            //Quando usuário logado
            if (!string.IsNullOrWhiteSpace(userID))
            {
                retorno.Add(new MenuSite { Controller = "Produtos", Action = "Index", Descricao = "Meus Produtos" });
                retorno.Add(new MenuSite { Controller = "CompraUsuario", Action = "MinhasCompras", Descricao = "Minhas Compras" });

                var usuario = await _IUsuario.ObterUsuarioPeloID(userID);

                if (usuario != null && usuario.Tipo != null)
                {
                    switch ((EnumTipoUsuario)usuario.Tipo)
                    {
                        case EnumTipoUsuario.Administrador:
                            retorno.Add(new MenuSite { Controller = "LogSistema", Action = "Index", Descricao = "Logs" });
                            retorno.Add(new MenuSite { Controller = "Usuarios", Action = "ListarUsuarios", Descricao = "Usuários" });
                            break;
                        case EnumTipoUsuario.Comum:
                            break;
                        default:
                            break;
                    }
                }

                retorno.Add(new MenuSite { Controller = "Produtos", Action = "ListarProdutosCarrinhoUsuario", Descricao = "", IdCampo = "qtdCarrinho", UrlImagem = "../img/carrinho.jpg" });
            }

            return retorno;
        }

        #endregion
    }
}