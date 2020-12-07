using Domain.Interfaces.InterfaceServices;
using Domain.Interfaces.InterfaceUsuario;
using Entities.Entities;
using Entities.Entities.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class ServiceUsuario : IServiceUsuario
    {
        #region Construtores

        public ServiceUsuario(IUsuario IUsuario) { _IUsuario = IUsuario; }

        #endregion

        #region Propriedades

        private readonly IUsuario _IUsuario;

        #endregion

        #region Métodos

        public async Task<List<ApplicationUser>> ListarUsuarioSomenteParaAdministradores(string userID)
        {
            var usuario = await _IUsuario.ObterUsuarioPeloID(userID);

            if (usuario != null && usuario.Tipo == EnumTipoUsuario.Administrador) return await _IUsuario.List();

            return new List<ApplicationUser>();
        }

        #endregion
    }
}