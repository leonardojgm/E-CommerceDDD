﻿using Domain.Interfaces.InterfaceProduct;
using Domain.Interfaces.InterfaceServices;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class ServiceProduct : IServiceProduct
    {
        #region Construtores

        public ServiceProduct(IProduct IProduct) { _IProduct = IProduct; }

        #endregion

        #region Propriedades

        private readonly IProduct _IProduct;

        #endregion

        #region Métodos

        #region IServiceProduct

        public async Task AddProduct(Produto product)
        {
            var validaNome = product.ValidaPropriedadeString(product.Nome, "Nome");
            var validaValor = product.ValidaPropriedadDecimal(product.Valor, "Valor");
            var validaQtdEstoque = product.ValidaPropriedadInt(product.QtdEstoque, "QtdEstoque");

            if (validaNome && validaValor && validaQtdEstoque)
            {
                product.DataCadastro = DateTime.Now;
                product.DataAlteracao = DateTime.Now;
                product.Estado = true;

                await _IProduct.Add(product);
            }
        }

        public async Task UpdateProduct(Produto product)
        {
            var validaNome = product.ValidaPropriedadeString(product.Nome, "Nome");
            var validaValor = product.ValidaPropriedadDecimal(product.Valor, "Valor");
            var validaQtdEstoque = product.ValidaPropriedadInt(product.QtdEstoque, "QtdEstoque");

            if (validaNome && validaValor && validaQtdEstoque)
            {
                product.DataAlteracao = DateTime.Now;

                await _IProduct.Update(product);
            }
        }

        public async Task<List<Produto>> ListarProdutosComEstoque(string descricao)
        {
            if (string.IsNullOrWhiteSpace(descricao)) return await _IProduct.ListarProdutos(p => p.QtdEstoque > 0);
            
            else return await _IProduct.ListarProdutos(p => p.QtdEstoque > 0 && p.Nome.ToUpper().Contains(descricao.ToUpper()));
        }

        #endregion

        #endregion
    }
}