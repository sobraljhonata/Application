using Application.Domain.Core.Models;

namespace Application.Application.Helper
{
    public abstract class HelperBase<TEntity, TDto>
        where TEntity : Entity<TEntity>
        where TDto : class
    {
        public abstract TEntity DtoToEntity(TDto obj);
        public abstract TDto EntityToDto(TEntity obj);
    }
}