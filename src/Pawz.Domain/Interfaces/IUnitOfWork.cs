using System;

namespace Pawz.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        // IPetRepo Pets { get; }

        int Save();
    }
}