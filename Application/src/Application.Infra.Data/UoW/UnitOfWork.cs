using Application.Domain.Interface;
using Application.Infra.Data.Context;

namespace Application.Infra.Data.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;
        public UnitOfWork(ApplicationContext context)
        {
            _context = context;
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public bool Commit()
        {
            return _context.SaveChanges() > 0;
        }
    }
}