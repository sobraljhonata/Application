using System;

namespace Application.Domain.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        bool Commit();
    }
}