using IF.Core.Data;
using IF.Persistence.EF.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Transactions;

namespace IF.Persistence.EF.Core
{
    public class GenericRepository : IRepository
    {
        private readonly DbContext context;

        public GenericRepository(DbContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(GenericRepository) + ": Any DbContext object not set");
            
            this.context = context;
        }

        public TEntity GetByKey<TEntity>(object keyValue) where TEntity : class,IEntity
        {
            return this.context.Find<TEntity>(keyValue);
        }

        public IQueryable<TEntity> GetQuery<TEntity>() where TEntity : class, IEntity
        {
            return this.context.Set<TEntity>().AsQueryable();
        }

        public IQueryable<TEntity> GetQuery<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class, IEntity
        {
            return GetQuery<TEntity>().Where(predicate);
        }


        public IEnumerable<TEntity> Get<TEntity, TOrderBy>(Expression<Func<TEntity, TOrderBy>> orderBy, int pageIndex, int pageSize, SortOrder sortOrder = SortOrder.Ascending) where TEntity : class, IEntity
        {
            if (sortOrder == SortOrder.Ascending)
            {
                return GetQuery<TEntity>().OrderBy(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize).AsEnumerable();
            }
            return GetQuery<TEntity>().OrderByDescending(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize).AsEnumerable();
        }

        public IEnumerable<TEntity> Get<TEntity, TOrderBy>(Expression<Func<TEntity, bool>> criteria, Expression<Func<TEntity, TOrderBy>> orderBy, int pageIndex, int pageSize, SortOrder sortOrder = SortOrder.Ascending) where TEntity : class, IEntity
        {
            if (sortOrder == SortOrder.Ascending)
            {
                return GetQuery<TEntity>(criteria).OrderBy(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize).AsEnumerable();
            }
            return GetQuery<TEntity>(criteria).OrderByDescending(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize).AsEnumerable();
        }

        public TEntity Single<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class, IEntity
        {
            return GetQuery<TEntity>().Single<TEntity>(criteria);
        }


        public TEntity First<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class, IEntity
        {
            return GetQuery<TEntity>().First(predicate);
        }


        public void Add<TEntity>(TEntity entity) where TEntity : class, IEntity
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            DbContext.Set<TEntity>().Add(entity);
        }

        public void Attach<TEntity>(TEntity entity) where TEntity : class, IEntity
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            DbContext.Set<TEntity>().Attach(entity);
        }

        public void Delete<TEntity>(TEntity entity) where TEntity : class, IEntity
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            DbContext.Set<TEntity>().Remove(entity);
        }

        public void Delete<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class, IEntity
        {
            IEnumerable<TEntity> records = Find<TEntity>(criteria);

            foreach (TEntity record in records)
            {
                Delete<TEntity>(record);
            }
        }

        public void ChangeState<TEntity>(object Id) where TEntity : class, IEntity, IActiveableEntity, new()
        {
            var entity = this.GetByKey<TEntity>(Id);

            if (entity != null)
            {
                entity.Active = !entity.Active;
            }

            this.DbContext.SaveChanges();
        }

       
        public IEnumerable<TEntity> GetAll<TEntity>() where TEntity : class, IEntity
        {
            
            return GetQuery<TEntity>().AsEnumerable();
        }

        public TEntity Save<TEntity>(TEntity entity) where TEntity : class, IEntity
        {
            Add<TEntity>(entity);
            DbContext.SaveChanges();
            return entity;
        }

        public void Update<TEntity>(TEntity entity) where TEntity : class, IEntity
        {
            context.Update(entity);
        }

        public IEnumerable<TEntity> Find<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity :  class, IEntity
        {
            return GetQuery<TEntity>().Where(criteria);
        }

        public TEntity FindOne<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity :  class, IEntity
        {
            return GetQuery<TEntity>().Where(criteria).FirstOrDefault();
        }

       

        public int Count<TEntity>() where TEntity :class,  IEntity
        {
            return GetQuery<TEntity>().Count();
        }

        public int Count<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class, IEntity
        {
            return GetQuery<TEntity>().Count(criteria);
        }
   
        public async Task AddAsync<TEntity>(TEntity entity) where TEntity : class, IEntity
       => await context.Set<TEntity>().AddAsync(entity);

        public async Task UpdateAsync<TEntity>(TEntity entity) where TEntity : class, IEntity
            => await Task.FromResult(context.Set<TEntity>().Update(entity));

        public async Task DeleteAsync<TEntity>(TEntity entity) where TEntity : class, IEntity
            => await Task.FromResult(context.Set<TEntity>().Remove(entity));

        public async Task<PagedListResponse<TEntity>> ToPagedListResponseAsync<TEntity>(IQueryable<TEntity> source,BasePagingRequest request)
        {


            if (request.PageNumber < 1)
                throw new ArgumentOutOfRangeException("pageNumber", request.PageNumber, "PageNumber cannot be below 1.");

            if (request.PageSize < 1)
                throw new ArgumentOutOfRangeException("pageSize", request.PageSize, "PageSize cannot be less than 1.");



            var count = await source.CountAsync();



            var data = await source.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToListAsync();

            PagedListResponse<TEntity> pagedList = new PagedListResponse<TEntity>(data, request.PageNumber, request.PageSize, count);

            return pagedList;


        }

        public async Task<PagedListResponse<TEntity>> ToPagedListResponseAsyncNoLock<TEntity>(IQueryable<TEntity> source, BasePagingRequest request) 
        {
            if (request.PageNumber < 1)
                throw new ArgumentOutOfRangeException("pageNumber", request.PageNumber, "PageNumber cannot be below 1.");

            if (request.PageSize < 1)
                throw new ArgumentOutOfRangeException("pageSize", request.PageSize, "PageSize cannot be less than 1.");



            var count = await source.CountNoLockAsync();



            var data = await source.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToListNoLockAsync();

            PagedListResponse<TEntity> pagedList = new PagedListResponse<TEntity>(data, request.PageNumber, request.PageSize, count);

            return pagedList;
        }

        public PagedListResponse<TEntity> ToPagedListResponse<TEntity>(IQueryable<TEntity> source, BasePagingRequest request) 
        {
            return new PagedListResponse<TEntity>(source, request);
        }

        public PagedListResponse<TEntity> ToPagedListResponseNoLock<TEntity>(IQueryable<TEntity> source, BasePagingRequest request)
        {
            if (request.PageNumber < 1)
                throw new ArgumentOutOfRangeException("pageNumber", request.PageNumber, "PageNumber cannot be below 1.");

            if (request.PageSize < 1)
                throw new ArgumentOutOfRangeException("pageSize", request.PageSize, "PageSize cannot be less than 1.");



            var count = source.CountNoLock();



            var data = source.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToListNoLock();

            PagedListResponse<TEntity> pagedList = new PagedListResponse<TEntity>(data, request.PageNumber, request.PageSize, count);

            return pagedList;
        }

        public TransactionScope CreateNoLockTransaction()
        {
            var options = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted
            };
            return new TransactionScope(TransactionScopeOption.Required, options);
        }


        public TransactionScope CreateNoLockTransactionAsync()
        {
            var options = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted,               
                
            };

            

            return new TransactionScope(TransactionScopeOption.Required, options, TransactionScopeAsyncFlowOption.Enabled);
        }


        public List<T> ToListNoLock<T>(IQueryable<T> query) 
        {
            using (TransactionScope ts = this.CreateNoLockTransaction())
            {
                return query.ToList();
            }
        }

        public T SingleOrDefaultNoLock<T>( IQueryable<T> query) 
        {
            using (TransactionScope ts = CreateNoLockTransaction())
            {
                return query.SingleOrDefault();
            }
        }

        public T FirstOrDefaultNoLock<T>(IQueryable<T> query)
        {
            using (TransactionScope ts = CreateNoLockTransaction())
            {
                return query.FirstOrDefault();
            }
        }

        public int CountNoLock<T>( IQueryable<T> query) 
        {
            using (var scope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions()
                {
                    IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted
                }))
            {
                int toReturn = query.Count();
                scope.Complete();
                return toReturn;
            }
        }

        //async



        public  async Task<List<T>> ToListNoLockAsync<T>( IQueryable<T> query) 
        {
            using (TransactionScope ts = CreateNoLockTransactionAsync())
            {
                return await query.ToListAsync();
            }
        }

        public async Task<T> SingleOrDefaultNoLockAsync<T>( IQueryable<T> query)
        {
            using (TransactionScope ts = CreateNoLockTransactionAsync())
            {
                return await query.SingleOrDefaultAsync();
            }
        }

        public  async Task<T> FirstOrDefaultNoLockAsync<T>( IQueryable<T> query)
        {
            using (TransactionScope ts = CreateNoLockTransactionAsync())
            {
                return await query.FirstOrDefaultAsync();
            }
        }

        public async Task<int> CountNoLockAsync<T>( IQueryable<T> query)
        {
            using (var scope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions()
                {
                    IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted
                }, TransactionScopeAsyncFlowOption.Enabled))
            {
                int toReturn = await query.CountAsync();
                scope.Complete();
                return toReturn;
            }
        }

        public IUnitOfWork UnitOfWork
        {
            get
            {
                if (unitOfWork == null)
                {
                    unitOfWork = new UnitOfWork(this.DbContext);
                }
                return unitOfWork;
            }
        }


        public DbContext DbContext
        {
            get
            {
                return this.context;
            }
        }

        private IUnitOfWork unitOfWork;
    }
}
