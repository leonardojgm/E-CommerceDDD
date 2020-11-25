using ApplicationApp.Interfaces;
using Domain.Interfaces.InterfaceCompraUsuario;
using Entities.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationApp.OpenApp
{
    public class AppCompraUsuario : InterfaceCompraUsuarioApp
    {
        #region Construtores

        public AppCompraUsuario(ICompraUsuario ICompraUsuario) { _ICompraUsuario = ICompraUsuario; }

        #endregion

        #region Propriedades

        private readonly ICompraUsuario _ICompraUsuario;

        #endregion

        #region Métodos

        #region InterfaceCompraUsuarioApp

        public async Task<int> QuantidadeProdutoCarrinhoUsuario(string userId) { return await _ICompraUsuario.QuantidadeProdutoCarrinhoUsuario(userId); }

        #endregion


        #region InterfaceGenericaApp

        public async Task Add(CompraUsuario Objeto) { await _ICompraUsuario.Add(Objeto); }

        public async Task Delete(CompraUsuario Objeto) { await _ICompraUsuario.Delete(Objeto); }

        public async Task<CompraUsuario> GetEntityById(int Id) { return await _ICompraUsuario.GetEntityById(Id); }

        public async Task<List<CompraUsuario>> List()   { return await _ICompraUsuario.List(); }

        public async Task Update(CompraUsuario Objeto) { await _ICompraUsuario.Update(Objeto); }

        #endregion

        #endregion
    }
}