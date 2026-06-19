using System.Linq.Expressions;
using TriagemCurriculos.Infraestructure.Interface;

namespace TriagemCurriculos.Infraestructure
{
    public class RepositoryBase<TEntity> : IDisposable, IAsyncDisposable, IRepositoryBase<TEntity> where TEntity : class
    {

        private protected readonly MainDbContext _db;

        public RepositoryBase(MainDbContext db)
        {
            _db = db;
        }

        public Task Add(TEntity entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _db.Dispose();
            GC.SuppressFinalize(this);
        }

        public async ValueTask DisposeAsync()
        {
            await _db.DisposeAsync();
            GC.SuppressFinalize(this);
        }

        //public Task<TEntity> GetById(string? id, CancellationToken cancellationToken = default)
        //{
        //    throw new NotImplementedException();
        //}

        //public IQueryable<TEntity> GetQuery()
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IList<TEntity>> List(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IList<TEntity>> ListAll(CancellationToken cancellationToken = default)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task Remove(TEntity entity, CancellationToken cancellationToken = default)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task Update(TEntity entity, CancellationToken cancellationToken = default)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
