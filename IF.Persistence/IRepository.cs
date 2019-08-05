using IF.Core.Data;
using IF.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IF.Core.Persistence
{
    public partial interface IRepository
    {
        TEntity GetByKey<TEntity>(object keyValue) where TEntity : class, IEntity;
        IQueryable<TEntity> GetQuery<TEntity>() where TEntity : class, IEntity;
        IQueryable<TEntity> GetQuery<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class, IEntity;
        //IQueryable<TEntity> GetQuery<TEntity>(ISpecification<TEntity> criteria) where TEntity : class;
        TEntity Single<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class, IEntity;
        //TEntity Single<TEntity>(ISpecification<TEntity> criteria) where TEntity : class;
        TEntity First<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class, IEntity;
        //TEntity First<TEntity>(ISpecification<TEntity> criteria) where TEntity : class;
        void Add<TEntity>(TEntity entity) where TEntity : class, IEntity;
        void Attach<TEntity>(TEntity entity) where TEntity : class, IEntity;
        void Delete<TEntity>(TEntity entity) where TEntity : class, IEntity;
        void Delete<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class, IEntity;
        //void Delete<TEntity>(ISpecification<TEntity> criteria) where TEntity : class;
        void Update<TEntity>(TEntity entity) where TEntity : class, IEntity;
        //IEnumerable<TEntity> Find<TEntity>(ISpecification<TEntity> criteria) where TEntity : class;
        IEnumerable<TEntity> Find<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class, IEntity;
        //TEntity FindOne<TEntity>(ISpecification<TEntity> criteria) where TEntity : class;
        TEntity FindOne<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class, IEntity;
        IEnumerable<TEntity> GetAll<TEntity>() where TEntity : class, IEntity;
        IEnumerable<TEntity> Get<TEntity, TOrderBy>(Expression<Func<TEntity, TOrderBy>> orderBy, int pageIndex, int pageSize, SortOrder sortOrder = SortOrder.Ascending) where TEntity : class, IEntity;
        IEnumerable<TEntity> Get<TEntity, TOrderBy>(Expression<Func<TEntity, bool>> criteria, Expression<Func<TEntity, TOrderBy>> orderBy, int pageIndex, int pageSize, SortOrder sortOrder = SortOrder.Ascending) where TEntity : class, IEntity;
        //IEnumerable<TEntity> Get<TEntity, TOrderBy>(ISpecification<TEntity> specification, Expression<Func<TEntity, TOrderBy>> orderBy, int pageIndex, int pageSize, SortOrder sortOrder = SortOrder.Ascending) where TEntity : class;
        int Count<TEntity>() where TEntity : class, IEntity;
        int Count<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class, IEntity;
        //int Count<TEntity>(ISpecification<TEntity> criteria) where TEntity : class;
        IUnitOfWork UnitOfWork { get; }



    }

    public partial interface IRepository
    {
        //Task<IQueryable<TEntity>> GetQueryAsync<TEntity>() where TEntity : class;
        Task AddAsync<TEntity>(TEntity entity) where TEntity : class, IEntity;
        Task UpdateAsync<TEntity>(TEntity entity) where TEntity : class, IEntity;
        Task DeleteAsync<TEntity>(TEntity entity) where TEntity : class, IEntity;
        //Task<int> SaveChangesAsync();

        //Task<IQueryable<TEntity>> GetQueryAsync<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class;

        void ChangeState<TEntity>(object Id) where TEntity : class, IEntity, IActiveableEntity, new();

        Task<PagedListResponse<TEntity>> ToPagedListResponseAsync<TEntity>(IQueryable<TEntity> source, BasePagingRequest request);

        Task<PagedListResponse<TEntity>> ToPagedListResponseAsyncNoLock<TEntity>(IQueryable<TEntity> source, BasePagingRequest request);




        PagedListResponse<TEntity> ToPagedListResponse<TEntity>(IQueryable<TEntity> source, BasePagingRequest request);

        PagedListResponse<TEntity> ToPagedListResponseNoLock<TEntity>(IQueryable<TEntity> source, BasePagingRequest request);
    }

    public partial interface IRepository
    {

        List<T> ToListNoLock<T>(IQueryable<T> query);

        T SingleOrDefaultNoLock<T>(IQueryable<T> query);

        T FirstOrDefaultNoLock<T>(IQueryable<T> query);


        int CountNoLock<T>(IQueryable<T> query);
        
        //async



        Task<List<T>> ToListNoLockAsync<T>( IQueryable<T> query);
        
        Task<T> SingleOrDefaultNoLockAsync<T>( IQueryable<T> query);
        
        Task<T> FirstOrDefaultNoLockAsync<T>( IQueryable<T> query) ;


        Task<int> CountNoLockAsync<T>(IQueryable<T> query);      
    }
}