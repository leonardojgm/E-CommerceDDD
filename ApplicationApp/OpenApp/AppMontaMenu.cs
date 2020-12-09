using ApplicationApp.Interfaces;
using Domain.Interfaces.InterfaceServices;
using Entities.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationApp.OpenApp
{
    public class AppMontaMenu : InterfaceMontaMenuApp
    {
        #region Construtores

        public AppMontaMenu(IServiceMontaMenu IServiceMontaMenu) { _IServiceMontaMenu = IServiceMontaMenu; }

        #endregion

        #region Propriedades

        private readonly IServiceMontaMenu _IServiceMontaMenu;

        #endregion

        #region Métodos

        public async Task<List<MenuSite>> MontaMenuPorPerfil(string userID) { return await _IServiceMontaMenu.MontaMenuPorPerfil(userID); }

        #endregion
    }
}