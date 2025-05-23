using Controle.Financeiro.Domain.Kernel;
using Controle.Financeiro.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controle.Financeiro.Infrastructure.Repositories
{
    public abstract class Repository<TEntity> where TEntity : Entity
    {
        private DbContext _dbContext;
        protected DbSet<TEntity> _dbSet;
        public Repository(FinanceiroDbContext financeiroDbContext)
        {
            _dbContext = financeiroDbContext;
            _dbSet = financeiroDbContext.Set<TEntity>();
        }
        public async Task<TEntity> Delete(TEntity entity)
        {
            
            _dbSet.Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public Task<TEntity> Get(string Id)
        {
            return _dbSet.Where(x => x.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<TEntity> Insert(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            _dbSet.Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
    }
}
