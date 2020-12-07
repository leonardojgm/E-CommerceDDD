using ApplicationApp.Interfaces;
using Entities.Entities;
using Entities.Entities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web_ECommerce.Controllers
{
    [Authorize]
    [LogActionFilter]
    public class UsuariosController : BaseController
    {
        #region Construtores

        public UsuariosController(UserManager<ApplicationUser> userManager, ILogger<BaseController> logger, InterfaceLogSistemaApp InterfaceLogSistemaApp, InterfaceUsuarioApp InterfaceUsuarioApp) 
            : base(logger, userManager, InterfaceLogSistemaApp) { _InterfaceUsuarioApp = InterfaceUsuarioApp; }

        #endregion

        #region Propriedades

        private InterfaceUsuarioApp _InterfaceUsuarioApp;

        #endregion

        #region Métodos

        public async Task<IActionResult> ListarUsuarios() { return View( await _InterfaceUsuarioApp.ListarUsuarioSomenteParaAdministradores(await RetornarIdUsuarioLogado())); }

        public async Task<IActionResult> Edit (string id)
        {
            var tipoUsuarios = new List<SelectListItem>();

            tipoUsuarios.Add(new SelectListItem { Text = Enum.GetName(typeof(EnumTipoUsuario), EnumTipoUsuario.Comum), Value = Convert.ToInt32(EnumTipoUsuario.Comum).ToString() });
            tipoUsuarios.Add(new SelectListItem { Text = Enum.GetName(typeof(EnumTipoUsuario), EnumTipoUsuario.Administrador), Value = Convert.ToInt32(EnumTipoUsuario.Administrador).ToString() });

            ViewBag.TiposUsuario = tipoUsuarios;

            return View(await _InterfaceUsuarioApp.ObterUsuarioPeloID(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ApplicationUser usuario)
        {
            try
            {
                await _InterfaceUsuarioApp.AtualizarTipoUsuario(usuario.Id, (EnumTipoUsuario)usuario.Tipo);

                await LogEcommerce(EnumTipoLog.Informativo, usuario);

                return RedirectToAction(nameof(ListarUsuarios));
            }
            catch (Exception erro)
            {
                await LogEcommerce(EnumTipoLog.Erro, erro);

                return View("Edit", usuario);
            }
        }

        #endregion
    }
}