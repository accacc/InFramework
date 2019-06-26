using IF.Core.Data;
using IF.Core.Handler;
using IF.Core.Persistence;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Derin.Persistence.EF
{
    public class GenericRepository : IRepository
    {
        private readonly string _connectionStringName;
        private DbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository&lt;TEntity&gt;"/> class,IEntity.
        /// </summary>
        public GenericRepository()
            : this(string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericRepository&lt;TEntity&gt;"/> class,IEntity.
        /// </summary>
        /// <param name="connectionStringName">Name of the connection string.</param>
        public GenericRepository(string connectionStringName)
        {
            this._connectionStringName = connectionStringName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericRepository&lt;TEntity&gt;"/> class,IEntity.
        /// </summary>
        /// <param name="context">The context.</param>
        public GenericRepository(DbContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            
            _context = context;
            this.ConfigureDbContext();
        }

        private void ConfigureDbContext()
        {
            _context.Configuration.AutoDetectChangesEnabled = true;
            _context.Configuration.ValidateOnSaveEnabled = false;
            _context.Configuration.LazyLoadingEnabled = false;
        }

        public GenericRepository(ObjectContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            _context = new DbContext(context, true);
            this.ConfigureDbContext();
        }

        public TEntity GetByKey<TEntity>(object keyValue) where TEntity : class, IEntity
        {
            EntityKey key = GetEntityKey<TEntity>(keyValue);

            object originalItem;
            if (((IObjectContextAdapter)DbContext).ObjectContext.TryGetObjectByKey(key, out originalItem))
            {
                return (TEntity)originalItem;
            }
            return default(TEntity);
        }

        public IQueryable<TEntity> GetQuery<TEntity>() where TEntity : class, IEntity
        {
            var entityName = GetEntityName<TEntity>();
            return ((IObjectContextAdapter)DbContext).ObjectContext.CreateQuery<TEntity>(entityName);
        }

        public IQueryable<TEntity> GetQuery<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class,IEntity
        {
            return GetQuery<TEntity>().Where(predicate);
        }

        //public IQueryable<TEntity> GetQuery<TEntity>(ISpecification<TEntity> criteria) where TEntity : class,IEntity
        //{
        //    return criteria.SatisfyingEntitiesFrom(GetQuery<TEntity>());
        //}

        public IEnumerable<TEntity> Get<TEntity, TOrderBy>(Expression<Func<TEntity, TOrderBy>> orderBy, int pageIndex, int pageSize, SortOrder sortOrder = SortOrder.Ascending) where TEntity : class,IEntity
        {
            if (sortOrder == SortOrder.Ascending)
            {
                return GetQuery<TEntity>().OrderBy(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize).AsEnumerable();
            }
            return GetQuery<TEntity>().OrderByDescending(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize).AsEnumerable();
        }

        public IEnumerable<TEntity> Get<TEntity, TOrderBy>(Expression<Func<TEntity, bool>> criteria, Expression<Func<TEntity, TOrderBy>> orderBy, int pageIndex, int pageSize, SortOrder sortOrder = SortOrder.Ascending) where TEntity : class,IEntity
        {
            if (sortOrder == SortOrder.Ascending)
            {
                return GetQuery<TEntity>(criteria).OrderBy(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize).AsEnumerable();
            }
            return GetQuery<TEntity>(criteria).OrderByDescending(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize).AsEnumerable();
        }

        //public IEnumerable<TEntity> Get<TEntity, TOrderBy>(ISpecification<TEntity> specification, Expression<Func<TEntity, TOrderBy>> orderBy, int pageIndex, int pageSize, SortOrder sortOrder = SortOrder.Ascending) where TEntity : class,IEntity
        //{
        //    if (sortOrder == SortOrder.Ascending)
        //    {
        //        return specification.SatisfyingEntitiesFrom(GetQuery<TEntity>()).OrderBy(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize).AsEnumerable();
        //    }
        //    return specification.SatisfyingEntitiesFrom(GetQuery<TEntity>()).OrderByDescending(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize).AsEnumerable();
        //}

        public TEntity Single<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class,IEntity
        {
            return GetQuery<TEntity>().Single<TEntity>(criteria);
        }

        //public TEntity Single<TEntity>(ISpecification<TEntity> criteria) where TEntity : class,IEntity
        //{
        //    return criteria.SatisfyingEntityFrom(GetQuery<TEntity>());
        //}

        public TEntity First<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class,IEntity
        {
            return GetQuery<TEntity>().First(predicate);
        }

        //public TEntity First<TEntity>(ISpecification<TEntity> criteria) where TEntity : class,IEntity
        //{
        //    return criteria.SatisfyingEntitiesFrom(GetQuery<TEntity>()).First();
        //}

        public void Add<TEntity>(TEntity entity) where TEntity : class,IEntity
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            DbContext.Set<TEntity>().Add(entity);
        }

        public void Attach<TEntity>(TEntity entity) where TEntity : class,IEntity
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            DbContext.Set<TEntity>().Attach(entity);
        }

        public void Delete<TEntity>(TEntity entity) where TEntity : class,IEntity
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            DbContext.Set<TEntity>().Remove(entity);
        }

        public void Delete<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class,IEntity
        {
            IEnumerable<TEntity> records = Find<TEntity>(criteria);

            foreach (TEntity record in records)
            {
                Delete<TEntity>(record);
            }
        }

        public void ChangeState<TEntity>(object Id) where TEntity : class,IEntity, IActiveableEntity, new()
        {
            var entity = this.GetByKey<TEntity>(Id);

            if (entity != null)
            {
                entity.Active = !entity.Active;
            }

            this.DbContext.SaveChanges();
        }

        //public void Delete<TEntity>(ISpecification<TEntity> criteria) where TEntity : class,IEntity
        //{
        //    IEnumerable<TEntity> records = Find<TEntity>(criteria);
        //    foreach (TEntity record in records)
        //    {
        //        Delete<TEntity>(record);
        //    }
        //}

        public IEnumerable<TEntity> GetAll<TEntity>() where TEntity : class,IEntity
        {
            return GetQuery<TEntity>().AsEnumerable();
        }

        public TEntity Save<TEntity>(TEntity entity) where TEntity : class,IEntity
        {
            Add<TEntity>(entity);
            DbContext.SaveChanges();
            return entity;
        }

        public void Update<TEntity>(TEntity entity) where TEntity : class,IEntity
        {
            var fqen = GetEntityName<TEntity>();

            object originalItem;
            EntityKey key = ((IObjectContextAdapter)DbContext).ObjectContext.CreateEntityKey(fqen, entity);
            if (((IObjectContextAdapter)DbContext).ObjectContext.TryGetObjectByKey(key, out originalItem))
            {
                ((IObjectContextAdapter)DbContext).ObjectContext.ApplyCurrentValues(key.EntitySetName, entity);
            }
        }

        public IEnumerable<TEntity> Find<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class,IEntity
        {
            return GetQuery<TEntity>().Where(criteria);
        }

        public TEntity FindOne<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class,IEntity
        {
            return GetQuery<TEntity>().Where(criteria).FirstOrDefault();
        }

        //public TEntity FindOne<TEntity>(ISpecification<TEntity> criteria) where TEntity : class,IEntity
        //{
        //    return criteria.SatisfyingEntityFrom(GetQuery<TEntity>());
        //}

        //public IEnumerable<TEntity> Find<TEntity>(ISpecification<TEntity> criteria) where TEntity : class,IEntity
        //{
        //    return criteria.SatisfyingEntitiesFrom(GetQuery<TEntity>()).AsEnumerable();
        //}

        public int Count<TEntity>() where TEntity : class,IEntity
        {
            return GetQuery<TEntity>().Count();
        }

        public int Count<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class,IEntity
        {
            return GetQuery<TEntity>().Count(criteria);
        }


        //public IEnumerable<TEntity> SqlQuerySmart<TEntity>(string storedProcedure, object inputParameters = null, params SqlParameter[] outputParameters)
        //{
        //    if (string.IsNullOrEmpty(storedProcedure))
        //        throw new ArgumentException("storedProcedure");

        //    var arguments = PrepareArguments(storedProcedure, inputParameters, outputParameters);
        //    return this.DbContext.Database.SqlQuery<TEntity>(arguments.Item1, arguments.Item2);
        //}

        //public IEnumerable SqlQuerySmart(Type elementType, string storedProcedure, object inputParameters = null, params SqlParameter[] outputParameters)
        //{
        //    if (elementType == null)
        //        throw new ArgumentNullException("elementType");
        //    if (string.IsNullOrEmpty(storedProcedure))
        //        throw new ArgumentException("storedProcedure");

        //    var arguments = PrepareArguments(storedProcedure, inputParameters, outputParameters);
        //    return this.DbContext.Database.SqlQuery(elementType, arguments.Item1, arguments.Item2);
        //}

        //public int ExecuteSqlCommandSmart(string storedProcedure, object inputParameters = null, params SqlParameter[] outputParameters)
        //{
        //    if (string.IsNullOrEmpty(storedProcedure))
        //        throw new ArgumentException("storedProcedure");

        //    var arguments = PrepareArguments(storedProcedure, inputParameters, outputParameters);

        //    var result = this.DbContext.Database.ExecuteSqlCommand(arguments.Item1, arguments.Item2);

        //    return result;
        //}

        //public StoredProcedureResult ExecuteSqlCommandSmartError(string storedProcedure, object inputParameters = null, params SqlParameter[] outputParameters)
        //{
        //    SqlParameter errorValue = new SqlParameter("Err", DbType.Int32);

        //    errorValue.Direction = ParameterDirection.Output;

        //    if (outputParameters.Length == 0)
        //    {
        //        outputParameters = new SqlParameter[1];
        //    }

        //    outputParameters.SetValue(errorValue, outputParameters.Length - 1);

        //    int AffectedRows = this.ExecuteSqlCommandSmart(storedProcedure, inputParameters, outputParameters);

        //    StoredProcedureResult spError = new StoredProcedureResult(storedProcedure, Math.Abs((int)errorValue.Value));
        //    spError.AffectedRows = AffectedRows;

        //    return spError;
        //}

        //public StoredProcedureListResult<TEntity> SqlQuerySmartError<TEntity>(string storedProcedure, object inputParameters = null, params SqlParameter[] outputParameters) where TEntity : class,IEntity
        //{
        //    SqlParameter errorValue = new SqlParameter("Err", DbType.Int32);

        //    errorValue.Direction = ParameterDirection.Output;

        //    if (outputParameters.Length == 0)
        //    {
        //        outputParameters = new SqlParameter[1];
        //    }

        //    outputParameters.SetValue(errorValue, outputParameters.Length - 1);

        //    var result = this.SqlQuerySmart<TEntity>(storedProcedure, inputParameters, outputParameters).ToList();

        //    StoredProcedureListResult<TEntity> list = new StoredProcedureListResult<TEntity>(result);

        //    list.Result = new StoredProcedureResult(storedProcedure, Math.Abs((int)errorValue.Value));

        //    return list;
        //}

        //public int Count<TEntity>(ISpecification<TEntity> criteria) where TEntity : class,IEntity
        //{
        //    return criteria.SatisfyingEntitiesFrom(GetQuery<TEntity>()).Count();
        //}

        //private Tuple<string, object[]> PrepareArguments(string storedProcedure, object inputParameters, params SqlParameter[] outputParameters)
        //{
        //    var parameterNames = new List<string>();
        //    var parameters = new List<object>();

        //    if (inputParameters != null)
        //    {
        //        foreach (PropertyInfo propertyInfo in inputParameters.GetType().GetProperties())
        //        {
        //            string name = "@" + propertyInfo.Name;
        //            object value = propertyInfo.GetValue(inputParameters, null);

        //            parameterNames.Add(name);
        //            parameters.Add(new SqlParameter(name, value ?? DBNull.Value));
        //        }
        //    }

        //    if (outputParameters != null)
        //    {
        //        foreach (SqlParameter outputParameter in outputParameters)
        //        {
        //            outputParameter.Direction = ParameterDirection.Output;
        //            parameterNames.Add("@" + outputParameter.ParameterName + " OUT");
        //            parameters.Add(outputParameter);
        //        }
        //    }

        //    if (parameterNames.Count > 0)
        //    {
        //        storedProcedure += " " + string.Join(", ", parameterNames);
        //    }

        //    return new Tuple<string, object[]>(storedProcedure, parameters.ToArray());
        //}

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

        private EntityKey GetEntityKey<TEntity>(object keyValue) where TEntity : class,IEntity
        {
            var entitySetName = GetEntityName<TEntity>();
            var objectSet = ((IObjectContextAdapter)DbContext).ObjectContext.CreateObjectSet<TEntity>();
            var keyPropertyName = objectSet.EntitySet.ElementType.KeyMembers[0].ToString();
            var entityKey = new EntityKey(entitySetName, new[] { new EntityKeyMember(keyPropertyName, keyValue) });
            return entityKey;
        }

        public string GetEntityName<TEntity>() where TEntity : class,IEntity
        {
            string entitySetName = ((IObjectContextAdapter)DbContext).ObjectContext
                .MetadataWorkspace
                .GetEntityContainer(((IObjectContextAdapter)DbContext).ObjectContext.DefaultContainerName, DataSpace.CSpace)
                                    .BaseEntitySets.Where(bes => bes.ElementType.Name == typeof(TEntity).Name).First().Name;
            return string.Format("{0}.{1}", ((IObjectContextAdapter)DbContext).ObjectContext.DefaultContainerName, entitySetName);
        }

        public Task<IQueryable<TEntity>> GetQueryAsync<TEntity>() where TEntity : class,IEntity
        {
            throw new NotImplementedException();
        }

        public Task AddAsync<TEntity>(TEntity entity) where TEntity : class,IEntity
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync<TEntity>(TEntity entity) where TEntity : class,IEntity
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync<TEntity>(TEntity entity) where TEntity : class,IEntity
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<TEntity>> GetQueryAsync<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class,IEntity
        {
            throw new NotImplementedException();
        }

        public Task<PagedListResponse<TEntity>> ToPagedListResponseAsync<TEntity>(IQueryable<TEntity> source, BasePagingRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<PagedListResponse<TEntity>> ToPagedListResponseAsyncNoLock<TEntity>(IQueryable<TEntity> source, BasePagingRequest request)
        {
            throw new NotImplementedException();
        }

        public PagedListResponse<TEntity> ToPagedListResponse<TEntity>(IQueryable<TEntity> source, BasePagingRequest request)
        {
            throw new NotImplementedException();
        }

        public PagedListResponse<TEntity> ToPagedListResponseNoLock<TEntity>(IQueryable<TEntity> source, BasePagingRequest request)
        {
            throw new NotImplementedException();
        }

        List<T> IRepository.ToListNoLock<T>(IQueryable<T> query)
        {
            throw new NotImplementedException();
        }

        T IRepository.SingleOrDefaultNoLock<T>( IQueryable<T> query)
        {
            throw new NotImplementedException();
        }

        T IRepository.FirstOrDefaultNoLock<T>( IQueryable<T> query)
        {
            throw new NotImplementedException();
        }

        int IRepository.CountNoLock<T>( IQueryable<T> query)
        {
            throw new NotImplementedException();
        }

        Task<List<T>> IRepository.ToListNoLockAsync<T>( IQueryable<T> query)
        {
            throw new NotImplementedException();
        }

        Task<T> IRepository.SingleOrDefaultNoLockAsync<T>( IQueryable<T> query)
        {
            throw new NotImplementedException();
        }

        Task<T> IRepository.FirstOrDefaultNoLockAsync<T>( IQueryable<T> query)
        {
            throw new NotImplementedException();
        }

        Task<int> IRepository.CountNoLockAsync<T>( IQueryable<T> query)
        {
            throw new NotImplementedException();
        }

        public DbContext DbContext
        {
            get
            {
                if (this._context == null)
                {
                    if (this._connectionStringName == string.Empty)
                        this._context = DbContextManager.Current;
                    else
                        this._context = DbContextManager.CurrentFor(this._connectionStringName);
                }
                return this._context;
            }
        }

        private IUnitOfWork unitOfWork;
    }
}
