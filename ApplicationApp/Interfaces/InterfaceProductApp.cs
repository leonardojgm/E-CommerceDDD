using Entities.Entities;
using System.Threading.Tasks;

namespace ApplicationApp.Interfaces
{
    interface InterfaceProductApp : InterfaceGenericaApp<Produto>
    {
        #region Métodos

        Task AddProduct(Produto product);

        Task UpdateProduct(Produto product);

        #endregion
    }
}