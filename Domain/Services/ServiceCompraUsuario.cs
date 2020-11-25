using Domain.Interfaces.InterfaceCompraUsuario;
using Domain.Interfaces.InterfaceServices;
using Entities.Entities;
using Entities.Entities.Enums;
using System;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class ServiceCompraUsuario : IServiceCompraUsuario
    {
        #region Construtores

        public ServiceCompraUsuario(ICompraUsuario ICompraUsuario) { _ICompraUsuario = ICompraUsuario; }

        #endregion

        #region Propriedades

        private readonly ICompraUsuario _ICompraUsuario;

        #endregion

        #region Métodos

        #region IServicoCompraUsuario

        public async Task<CompraUsuario> CarrinhoCompras(string userId) { return await _ICompraUsuario.ProdutosCompradosPorEstado(userId,EstadoCompra.Produto_Carrinho); }

        public async Task<CompraUsuario> ProdutosComprados(string userId) { return await _ICompraUsuario.ProdutosCompradosPorEstado(userId, EstadoCompra.Produto_Comprado); }

        #endregion

        #endregion
    }
}