using Domain.Interfaces.InterfaceUsuario;
using Entities.Entities;
using Entities.Entities.Enums;
using Infraestructure.Configuration;
using Infraestructure.Repository.Generics;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Infraestructure.Repository.Repositories
{
    public class RepositoryUsuario : RepositoryGenerics<ApplicationUser>, IUsuario
    {
        #region Construtores

        public RepositoryUsuario()
        {
            _optionsbuilder = new DbContextOptions<ContextBase>();
        }

        #endregion

        #region Propriedades

        private readonly DbContextOptions<ContextBase> _optionsbuilder;

        #endregion

        #region Métodos

        #region IUsuario

        public async Task AtualizarTipoUsuario(string userID, EnumTipoUsuario tipoUsuario)
        {
            using (var banco = new ContextBase(_optionsbuilder))
            {
                var usuario = await banco.ApplicationUser.FirstOrDefaultAsync(u => u.Id.Equals(userID));

                if (usuario != null)
                {
                    usuario.Tipo = tipoUsuario;

                    banco.ApplicationUser.Update(usuario);

                    await banco.SaveChangesAsync();
                }
            }
        }

        public async Task<ApplicationUser> ObterUsuarioPeloID(string userID)
        {
            using (var banco = new ContextBase(_optionsbuilder))
            {
                return await banco.ApplicationUser.FirstOrDefaultAsync(u => u.Id.Equals(userID));
            }
        }

        #endregion

        #endregion
    }
}