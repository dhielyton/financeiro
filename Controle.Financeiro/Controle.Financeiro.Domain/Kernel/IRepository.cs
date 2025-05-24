using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controle.Financeiro.Domain.Kernel
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        Task<TEntity> Insert(TEntity entity);
        Task<TEntity> Update(TEntity entity);
        Task<TEntity> Delete(TEntity entity);
        Task<TEntity> Get(string Id);
    }

}
