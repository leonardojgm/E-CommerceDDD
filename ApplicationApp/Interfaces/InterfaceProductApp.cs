using Entities.Entities;
using System.Threading.Tasks;

namespace ApplicationApp.Interfaces
{
    interface InterfaceProductApp : InterfaceGenericaApp<Product>
    {
        Task AddProduct(Product product);
        Task UpdateProduct(Product product);
    }
}