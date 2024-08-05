using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pawz.Domain.Interfaces
{
    public interface IGenericRepo<T> where T : class
    {
        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetAll();
        Task Add(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}