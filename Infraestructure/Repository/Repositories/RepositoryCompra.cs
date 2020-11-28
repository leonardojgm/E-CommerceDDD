using Domain.Interfaces.InterfaceCompra;
using Entities.Entities;
using Entities.Entities.Enums;
using Infraestructure.Configuration;
using Infraestructure.Repository.Generics;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Infraestructure.Repository.Repositories
{
    public class RepositoryCompra : RepositoryGenerics<Compra>, ICompra
    {
        #region Construtores

        public RepositoryCompra() { _optionsbuilder = new DbContextOptions<ContextBase>(); }

        #endregion

        #region Propriedades

        private readonly DbContextOptions<ContextBase> _optionsbuilder;

        #endregion

        #region Métodos

        #region ICompra
        public async Task<Compra> CompraPorEstado(string userId, EstadoCompra estado)
        {
            using (var banco = new ContextBase(_optionsbuilder))
            {
                return await banco.Compra.FirstOrDefaultAsync(c => c.Estado == estado && c.UserId == userId);
            }
        }

        #endregion

        #endregion
    }
}