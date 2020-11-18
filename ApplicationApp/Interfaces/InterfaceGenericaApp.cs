using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationApp.Interfaces
{
    public interface InterfaceGenericaApp<T> where T : class
    {
        #region Métodos

        Task Add(T Objeto);

        Task Update(T Objeto);

        Task Delete(T Objeto);

        Task<T> GetEntityById(int Id);

        Task<List<T>> List();

        #endregion
    }
}