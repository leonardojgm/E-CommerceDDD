using Domain.Interfaces.InterfaceProduct;
using Domain.Interfaces.InterfaceServices;
using Domain.Services;
using Entities.Entities;
using Infraestructure.Repository.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace UnitTestEcommerceDDD
{
    [TestClass]
    public class UnitTestEcommerce
    {
        #region Métodos

        [TestMethod]
        public async Task AddProductComSucesso()
        {
            try
            {
                IProduct _IProduct = new RepositoryProduct();
                IServiceProduct _IServiceProduct = new ServiceProduct(_IProduct);
                var produto = new Produto
                {
                    Descricao = string.Concat("Descrição test DDD", DateTime.Now.ToString()),
                    QtdEstoque = 10,
                    Nome = string.Concat("Nome test DDD", DateTime.Now.ToString()),
                    Valor = 20,
                    UserId = "e83452fa-aa0e-424a-a58c-ea9a116ffc53",
                };

                await _IServiceProduct.AddProduct(produto);

                Assert.IsFalse(produto.Notifycoes.Any());
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public async Task AddProductComValidacaoCampoObrigatorio()
        {
            try
            {
                IProduct _IProduct = new RepositoryProduct();
                IServiceProduct _IServiceProduct = new ServiceProduct(_IProduct);
                var produto = new Produto();

                await _IServiceProduct.AddProduct(produto);

                Assert.IsTrue(produto.Notifycoes.Any());
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public async Task ListaProdutosUsuario()
        {
            try
            {
                IProduct _IProduct = new RepositoryProduct();
                var listaProdutos = await _IProduct.ListarProdutosUsuario("e83452fa-aa0e-424a-a58c-ea9a116ffc53");

                Assert.IsTrue(listaProdutos.Any());
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public async Task GetEntityById()
        {
            try
            {
                IProduct _IProduct = new RepositoryProduct();
                var listaProdutos = await _IProduct.ListarProdutosUsuario("e83452fa-aa0e-424a-a58c-ea9a116ffc53");
                var produto = await _IProduct.GetEntityById(listaProdutos.LastOrDefault().Id);

                Assert.IsTrue(produto != null);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public async Task Delete()
        {
            try
            {
                IProduct _IProduct = new RepositoryProduct();
                var listaProdutos = await _IProduct.ListarProdutosUsuario("e83452fa-aa0e-424a-a58c-ea9a116ffc53");
                var ultimoProduto = listaProdutos.LastOrDefault();

                await _IProduct.Delete(ultimoProduto);

                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        #endregion
    }
}