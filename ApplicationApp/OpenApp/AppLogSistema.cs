using ApplicationApp.Interfaces;
using Domain.Interfaces.InterfaceLogSistema;
using Entities.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationApp.OpenApp
{
    public class AppLogSistema : InterfaceLogSistemaApp
    {
        #region Construtores

        public AppLogSistema(ILogSistema ILogSistema)
        {
            _ILogSistema = ILogSistema;
        }

        #endregion

        #region Propriedades

        private readonly ILogSistema _ILogSistema;

        #endregion

        #region Métodos

        #region InterfaceGenericaApp

        public async Task Add(LogSistema Objeto) { await _ILogSistema.Add(Objeto); }

        public async Task Delete(LogSistema Objeto) { await _ILogSistema.Delete(Objeto); }

        public async Task<LogSistema> GetEntityById(int Id) { return await _ILogSistema.GetEntityById(Id); }

        public async Task<List<LogSistema>> List() { return await _ILogSistema.List(); }

        public async Task Update(LogSistema Objeto) { await _ILogSistema.Update(Objeto); }

        #endregion

        #endregion
    }
}