using ApplicationApp.Interfaces;
using Domain.Interfaces.InterfaceServices;
using Domain.Interfaces.InterfaceUsuario;
using Entities.Entities;
using Entities.Entities.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationApp.OpenApp
{
    public class AppUsuario : InterfaceUsuarioApp
    {
        #region Construtores

        public AppUsuario(IUsuario IUsuario, IServiceUsuario IServiceUsuario) 
        { 
            _IUsuario = IUsuario;
            _IServiceUsuario = IServiceUsuario;
        }

        #endregion

        #region Propriedades

        private readonly IUsuario _IUsuario;

        private readonly IServiceUsuario _IServiceUsuario;

        #endregion

        #region Métodos

        #region InterfaceGenericaApp

        public async Task Add(ApplicationUser Objeto) { await _IUsuario.Add(Objeto); }

        public async Task Delete(ApplicationUser Objeto) { await _IUsuario.Delete(Objeto); }

        public async Task<ApplicationUser> GetEntityById(int Id) { return await _IUsuario.GetEntityById(Id); }

        public async Task<List<ApplicationUser>> List() { return await _IUsuario.List(); }

        public async Task Update(ApplicationUser Objeto) { await _IUsuario.Update(Objeto); }

        #endregion

        #region InterfaceUsuarioApp

        public async Task AtualizarTipoUsuario(string userID, EnumTipoUsuario tipoUsuario) { await _IUsuario.AtualizarTipoUsuario(userID, tipoUsuario); }

        public async Task<ApplicationUser> ObterUsuarioPeloID(string userID) { return await _IUsuario.ObterUsuarioPeloID(userID); }

        public async Task<List<ApplicationUser>> ListarUsuarioSomenteParaAdministradores(string userID) { return await _IServiceUsuario.ListarUsuarioSomenteParaAdministradores(userID); }

        #endregion

        #endregion
    }
}