using Entities.Entities;
using System.Threading.Tasks;

namespace ApplicationApp.Interfaces
{
    public interface InterfaceCompraUsuarioApp : InterfaceGenericaApp<CompraUsuario>
    {
        #region Métodos

        public Task<int> QuantidadeProdutoCarrinhoUsuario(string userId);

        #endregion
    }
}