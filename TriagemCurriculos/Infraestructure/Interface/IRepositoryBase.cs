using System.Linq.Expressions;

namespace TriagemCurriculos.Infraestructure.Interface
{
    public interface IRepositoryBase<TEntity> :IDisposable, IAsyncDisposable where TEntity : class
    {
        //Task<TEntity> GetById(string? id,CancellationToken cancellationToken = default);
        //Task Add(TEntity entity, CancellationToken cancellationToken = default);
        //Task Update(TEntity entity, CancellationToken cancellationToken = default);
        //Task Remove(TEntity entity, CancellationToken cancellationToken = default);
        //Task<IList<TEntity>> ListAll(CancellationToken cancellationToken = default);
        //Task<IList<TEntity>> List(Expression<Func<TEntity, bool>> expression,CancellationToken cancellationToken = default);
        //IQueryable<TEntity> GetQuery();
    }
}
