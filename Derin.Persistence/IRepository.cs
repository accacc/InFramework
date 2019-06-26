using Derin.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Derin.Core.Persistence
{
    public partial interface IRepository
    {
        TEntity GetByKey<TEntity>(object keyValue) where TEntity : class;
        IQueryable<TEntity> GetQuery<TEntity>() where TEntity : class;
        IQueryable<TEntity> GetQuery<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class;
        //IQueryable<TEntity> GetQuery<TEntity>(ISpecification<TEntity> criteria) where TEntity : class;
        TEntity Single<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class;
        //TEntity Single<TEntity>(ISpecification<TEntity> criteria) where TEntity : class;
        TEntity First<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class;
        //TEntity First<TEntity>(ISpecification<TEntity> criteria) where TEntity : class;
        void Add<TEntity>(TEntity entity) where TEntity : class;
        void Attach<TEntity>(TEntity entity) where TEntity : class;
        void Delete<TEntity>(TEntity entity) where TEntity : class;
        void Delete<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class;
        //void Delete<TEntity>(ISpecification<TEntity> criteria) where TEntity : class;
        void Update<TEntity>(TEntity entity) where TEntity : class;
        //IEnumerable<TEntity> Find<TEntity>(ISpecification<TEntity> criteria) where TEntity : class;
        IEnumerable<TEntity> Find<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class;
        //TEntity FindOne<TEntity>(ISpecification<TEntity> criteria) where TEntity : class;
        TEntity FindOne<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class;
        IEnumerable<TEntity> GetAll<TEntity>() where TEntity : class;
        IEnumerable<TEntity> Get<TEntity, TOrderBy>(Expression<Func<TEntity, TOrderBy>> orderBy, int pageIndex, int pageSize, SortOrder sortOrder = SortOrder.Ascending) where TEntity : class;
        IEnumerable<TEntity> Get<TEntity, TOrderBy>(Expression<Func<TEntity, bool>> criteria, Expression<Func<TEntity, TOrderBy>> orderBy, int pageIndex, int pageSize, SortOrder sortOrder = SortOrder.Ascending) where TEntity : class;
        //IEnumerable<TEntity> Get<TEntity, TOrderBy>(ISpecification<TEntity> specification, Expression<Func<TEntity, TOrderBy>> orderBy, int pageIndex, int pageSize, SortOrder sortOrder = SortOrder.Ascending) where TEntity : class;
        int Count<TEntity>() where TEntity : class;
        int Count<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class;
        //int Count<TEntity>(ISpecification<TEntity> criteria) where TEntity : class;
        IUnitOfWork UnitOfWork { get; }



    }

    public partial interface IRepository
    {
        Task<IQueryable<TEntity>> GetQueryAsync<TEntity>() where TEntity : class;
        Task AddAsync<TEntity>(TEntity entity) where TEntity : class;
        Task UpdateAsync<TEntity>(TEntity entity) where TEntity : class;
        Task DeleteAsync<TEntity>(TEntity entity) where TEntity : class;
        //Task<int> SaveChangesAsync();

        Task<IQueryable<TEntity>> GetQueryAsync<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class;

        void ChangeState<TEntity>(object Id) where TEntity : class, IActiveableEntity, new();
    }

}