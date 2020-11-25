using Domain.Interfaces.InterfaceCompraUsuario;
using Entities.Entities;
using Infraestructure.Repository.Generics;

namespace Infraestructure.Repository.Repositories
{
    public class RepositoryCompraUsuario : RepositoryGenerics <CompraUsuario>, ICompraUsuario
    {
    }
}