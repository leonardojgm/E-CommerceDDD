﻿using Entities.Entities;
using System.Threading.Tasks;

namespace ApplicationApp.Interfaces
{
    public interface InterfaceCompraUsuarioApp : InterfaceGenericaApp<CompraUsuario>
    {
        #region Métodos

        public Task<int> QuantidadeProdutoCarrinhoUsuario(string userId);

        public Task<CompraUsuario> CarrinhoCompras(string userId);

        public Task<CompraUsuario> ProdutosComprados(string userId);

        public Task<bool> ConfirmaCompraCarrinhoUsuario(string userId);

        #endregion
    }
}