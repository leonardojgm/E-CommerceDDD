using Domain.Interfaces.InterfaceCompra;
using Domain.Interfaces.InterfaceCompraUsuario;
using Domain.Interfaces.InterfaceServices;
using Entities.Entities;
using Entities.Entities.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class ServiceCompraUsuario : IServiceCompraUsuario
    {
        #region Construtores

        public ServiceCompraUsuario(ICompraUsuario ICompraUsuario, ICompra ICompra)
        { 
            _ICompraUsuario = ICompraUsuario;
            _ICompra = ICompra;
        }

        #endregion

        #region Propriedades

        private readonly ICompraUsuario _ICompraUsuario;

        private readonly ICompra _ICompra;

        #endregion

        #region Métodos

        #region IServicoCompraUsuario

        public async Task<CompraUsuario> CarrinhoCompras(string userId) { return await _ICompraUsuario.ProdutosCompradosPorEstado(userId,EstadoCompra.Produto_Carrinho); }

        public async Task<CompraUsuario> ProdutosComprados(string userId, int? idCompra = null) { return await _ICompraUsuario.ProdutosCompradosPorEstado(userId, EstadoCompra.Produto_Comprado, idCompra); }

        public async Task<List<CompraUsuario>> MinhasCompras(string userId) { return await _ICompraUsuario.MinhasComprasPorEstado(userId, EstadoCompra.Produto_Comprado); }

        public async Task AdicionarProdutoCarrinho(string userId, CompraUsuario compraUsuario) 
        {
            var compra = await _ICompra.CompraPorEstado(userId, EstadoCompra.Produto_Carrinho);
            
            if (compra == null)
            {
                compra = new Compra
                {
                    UserId = userId,
                    Estado = EstadoCompra.Produto_Carrinho,
                };

                await _ICompra.Add(compra);
            }

            if (compra.Id > 0)
            {
                compraUsuario.IdCompra = compra.Id;

                await _ICompraUsuario.Add(compraUsuario);
            }
        }

        #endregion

        #endregion
    }
}