using ApplicationApp.Interfaces;
using Entities.Entities;
using Entities.Entities.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Web_ECommerce.Controllers
{
    [LogActionFilter]
    public class BaseController : Controller
    {
        #region Construtores

        public BaseController(ILogger<BaseController> logger, UserManager<ApplicationUser> userManager, InterfaceLogSistemaApp InterfaceLogSistemaApp)
        {
            this.logger = logger;
            _userManager = userManager;
            _InterfaceLogSistemaApp = InterfaceLogSistemaApp;
        }

        #endregion

        #region Propriedades

        public readonly ILogger<BaseController> logger;

        public readonly UserManager<ApplicationUser> _userManager;

        public readonly InterfaceLogSistemaApp _InterfaceLogSistemaApp;

        #endregion

        #region Métodos

        public async Task<string> RetornarIdUsuarioLogado()
        {
            var idUsuario = await _userManager.GetUserAsync(User);

            return idUsuario != null ? idUsuario.Id : null;
        }

        public async Task LogEcommerce(EnumTipoLog tipoLog, Object objeto)
        {
            string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();

            await _InterfaceLogSistemaApp.Add(new LogSistema
            {
                TipoLog = tipoLog,
                JsonInformacao = JsonConvert.SerializeObject(objeto),
                UserId = await RetornarIdUsuarioLogado(),
                NomeAction = actionName,
                NomeController = controllerName,
            });
        }

        #endregion
    }
}