using ApplicationApp.Interfaces;
using Domain.Interfaces.InterfaceProduct;
using Domain.Interfaces.InterfaceServices;
using Entities.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationApp.OpenApp
{
    public class AppProduct : InterfaceProductApp
    {
        #region Propriedades

        private readonly IProduct _IProduct;
        private readonly IServiceProduct _IServiceProduct;

        #endregion

        #region Construtores

        public AppProduct(IProduct IProduct, IServiceProduct IServiceProduct)
        {
            _IProduct = IProduct;
            _IServiceProduct = IServiceProduct;
        }

        #endregion

        #region Métodos

        #region InterfaceProductApp

        public async Task AddProduct(Produto product) { await _IServiceProduct.AddProduct(product); }

        public async Task UpdateProduct(Produto product) { await _IServiceProduct.UpdateProduct(product); }

        #endregion

        #region InterfaceGenericaApp

        public async Task Add(Produto Objeto) { await _IProduct.Add(Objeto); }

        public async Task Delete(Produto Objeto) { await _IProduct.Delete(Objeto); }

        public async Task<Produto> GetEntityById(int Id) { return await _IProduct.GetEntityById(Id); }

        public async Task<List<Produto>> List() { return await _IProduct.List(); }

        public async Task Update(Produto Objeto) { await _IProduct.Update(Objeto); }

        #endregion

        #endregion
    }
}