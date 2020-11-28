using Domain.Interfaces.Generics;
using Entities.Entities;
using Entities.Entities.Enums;
using System.Threading.Tasks;

namespace Domain.Interfaces.InterfaceCompra
{
    public interface ICompra : IGeneric<Compra>
    {
        #region Métodos

        public Task<Compra> CompraPorEstado(string userId, EstadoCompra estado);

        #endregion
    }
}