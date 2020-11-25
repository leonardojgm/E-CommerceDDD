using Domain.Interfaces.Generics;
using Entities.Entities;
using System.Threading.Tasks;

namespace Domain.Interfaces.InterfaceCompraUsuario
{
    public interface ICompraUsuario : IGeneric<CompraUsuario>
    {
        #region Métodos

        public Task<int> QuantidadeProdutoCarrinhoUsuario(string userId);

        #endregion
    }
}