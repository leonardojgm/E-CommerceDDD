using Entities.Entities;
using System.Threading.Tasks;

namespace Domain.Interfaces.InterfaceServices
{
    public interface IServiceProduct
    {
        #region Métodos

        Task AddProduct(Product product);

        Task UpdateProduct(Product product);

        #endregion
    }
}