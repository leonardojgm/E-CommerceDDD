using ApplicationApp.Interfaces;
using System.Threading.Tasks;
using Entities.Entities;
using Entities.Entities.Enums;
using Domain.Interfaces.InterfaceCompra;
using System.Collections.Generic;

namespace ApplicationApp.OpenApp
{
    public class AppCompra : InterfaceCompraApp
    {
        #region Construtores

        public AppCompra(ICompra ICompra)
        {
            _ICompra = ICompra;
        }

        #endregion

        #region Propriedades

        private readonly ICompra _ICompra;

        #endregion

        #region Métodos

        #region InterfaceCompraApp

        public async Task<Compra> CompraPorEstado(string userId, EstadoCompra estado) { return await _ICompra.CompraPorEstado(userId, estado); }

        #endregion

        #region InterfaceGenericaApp

        public async Task Add(Compra Objeto) { await _ICompra.Add(Objeto); }

        public async Task Delete(Compra Objeto) { await _ICompra.Delete(Objeto); }

        public async Task<Compra> GetEntityById(int Id) { return await _ICompra.GetEntityById(Id); }

        public async Task<List<Compra>> List() { return await _ICompra.List(); }

        public async Task Update(Compra Objeto) { await _ICompra.Update(Objeto); }

        #endregion

        #endregion
    }
}