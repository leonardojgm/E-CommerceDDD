using Domain.Interfaces.InterfaceProduct;
using Domain.Interfaces.InterfaceServices;
using Entities.Entities;
using System.Threading.Tasks;

namespace Domain.Services
{
    class ServiceProduct : IServiceProduct
    {
        #region Propriedades

        private readonly IProduct _IProduct;

        #endregion

        #region Construtores

        public ServiceProduct(IProduct IProduct) { _IProduct = IProduct; }

        #endregion

        #region Métodos

        #region IServiceProduct

        public async Task AddProduct(Product product)
        {
            var validaNome = product.ValidaPropriedadeString(product.Nome, "Nome");
            var validaValor = product.ValidaPropriedadDecimal(product.Valor, "Valor");

            if (validaNome && validaValor)
            {
                product.Estado = true;

                await _IProduct.Add(product);
            }
        }

        public async Task UpdateProduct(Product product)
        {
            var validaNome = product.ValidaPropriedadeString(product.Nome, "Nome");
            var validaValor = product.ValidaPropriedadDecimal(product.Valor, "Valor");

            if (validaNome && validaValor) await _IProduct.Update(product);
        }

        #endregion

        #endregion
    }
}