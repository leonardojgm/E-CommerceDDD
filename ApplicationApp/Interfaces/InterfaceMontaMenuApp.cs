using Entities.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationApp.Interfaces
{
    public interface InterfaceMontaMenuApp
    {
        #region Métodos

        Task<List<MenuSite>> MontaMenuPorPerfil(string userID);

        #endregion
    }
}