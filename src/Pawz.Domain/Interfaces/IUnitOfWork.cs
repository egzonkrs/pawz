using System.Threading.Tasks;

namespace Pawz.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        Task<int> SaveAsync();
    }
}