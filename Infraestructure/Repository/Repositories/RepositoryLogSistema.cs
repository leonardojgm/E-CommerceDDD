using Domain.Interfaces.InterfaceLogSistema;
using Entities.Entities;
using Infraestructure.Repository.Generics;

namespace Infraestructure.Repository.Repositories
{
    public class RepositoryLogSistema : RepositoryGenerics<LogSistema>, ILogSistema { }
}