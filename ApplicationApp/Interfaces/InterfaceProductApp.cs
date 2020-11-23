using Entities.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationApp.Interfaces
{
    public interface InterfaceProductApp : InterfaceGenericaApp<Produto>
    {
        #region Métodos

        Task AddProduct(Produto product);

        Task UpdateProduct(Produto product);

        Task<List<Produto>> ListaProdutosUsuario(string userId);

        #endregion
    }
}