using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Application.Domain.Core.Models;
using Application.Domain.Interface;
using Application.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Application.Infra.Data.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity<TEntity>
    {
        protected ApplicationContext Db;
        protected DbSet<TEntity> DbSet;

        public Repository(ApplicationContext context)
        {
            Db = context;
            DbSet = Db.Set<TEntity>();
        }

        public void Dispose()
        {
            Db.Dispose();
        }

        public void Adicionar(TEntity obj)
        {
            DbSet.Add(obj);
        }

        public TEntity ObterPorId(Guid id)
        {
            return DbSet.AsNoTracking().FirstOrDefault(t => t.Id == id);
        }

        public IEnumerable<TEntity> ObterTodos()
        {
            return DbSet.ToList();
        }

        public void Atualizar(TEntity obj)
        {
            DbSet.Update(obj);
        }

        public void Remover(Guid id)
        {
            DbSet.Remove(DbSet.Find(id));
        }

        public IEnumerable<TEntity> Buscar(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.AsNoTracking().Where(predicate);
        }

        public int SaveChanges()
        {
            return Db.SaveChanges();
        }
    }
}