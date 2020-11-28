using Domain.Interfaces.Generics;
using Entities.Entities;
using Entities.Entities.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces.InterfaceCompraUsuario
{
    public interface ICompraUsuario : IGeneric<CompraUsuario>
    {
        #region Métodos

        public Task<int> QuantidadeProdutoCarrinhoUsuario(string userId);

        public Task<CompraUsuario> ProdutosCompradosPorEstado(string userId, EstadoCompra estado, int? idCompra = null);

        public Task<List<CompraUsuario>> MinhasComprasPorEstado(string userId, EstadoCompra estado);

        public Task<bool> ConfirmaCompraCarrinhoUsuario(string userId);

        #endregion
    }
}