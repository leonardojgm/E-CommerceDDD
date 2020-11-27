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
        #region Construtores

        public AppProduct(IProduct IProduct, IServiceProduct IServiceProduct)
        {
            _IProduct = IProduct;
            _IServiceProduct = IServiceProduct;
        }

        #endregion

        #region Propriedades

        private readonly IProduct _IProduct;

        private readonly IServiceProduct _IServiceProduct;

        #endregion

        #region Métodos

        #region InterfaceProductApp

        public async Task AddProduct(Produto product) { await _IServiceProduct.AddProduct(product); }

        public async Task UpdateProduct(Produto product) { await _IServiceProduct.UpdateProduct(product); }

        public async Task<List<Produto>> ListarProdutosUsuario(string userId) { return await _IProduct.ListarProdutosUsuario(userId); }

        public async Task<List<Produto>> ListarPodutosComEstoque(string descricao) { return await _IServiceProduct.ListarProdutosComEstoque(descricao); }

        public async Task<List<Produto>> ListarProdutosCarrinhoUsuario(string userId) { return await _IProduct.ListarProdutosCarrinhoUsuario(userId); }

        public async Task<Produto> ObterProdutosCarrinho(int idProdutoCarrinho) { return await _IProduct.ObterProdutosCarrinho(idProdutoCarrinho); }

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