using Entities.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces.InterfaceServices
{
    public interface IServiceCompraUsuario
    {
        #region Métodos

        public Task<CompraUsuario> CarrinhoCompras(string userId);

        public Task<CompraUsuario> ProdutosComprados(string userId, int? idCompra = null);

        public Task<List<CompraUsuario>> MinhasCompras(string userId);

        public Task AdicionarProdutoCarrinho(string userId, CompraUsuario compraUsuario);

        #endregion
    }
}