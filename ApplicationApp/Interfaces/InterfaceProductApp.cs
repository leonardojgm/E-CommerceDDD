using Entities.Entities;
using System.Threading.Tasks;

namespace ApplicationApp.Interfaces
{
    interface InterfaceProductApp : InterfaceGenericaApp<Product>
    {
        #region Métodos

        Task AddProduct(Product product);

        Task UpdateProduct(Product product);

        #endregion
    }
}