using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.EntityFrameworkCore.Storage;

namespace IF.Persistence.EF.Core
{
    public static class IQueryableExtensions
    {
        private static readonly TypeInfo QueryCompilerTypeInfo = typeof(QueryCompiler).GetTypeInfo();

        private static readonly FieldInfo QueryCompilerField = typeof(EntityQueryProvider).GetTypeInfo().DeclaredFields.First(x => x.Name == "_queryCompiler");

        private static readonly FieldInfo QueryModelGeneratorField = QueryCompilerTypeInfo.DeclaredFields.First(x => x.Name == "_queryModelGenerator");

        private static readonly FieldInfo DataBaseField = QueryCompilerTypeInfo.DeclaredFields.Single(x => x.Name == "_database");

        private static readonly PropertyInfo DatabaseDependenciesField = typeof(Database).GetTypeInfo().DeclaredProperties.Single(x => x.Name == "Dependencies");

        public static string ToSql<TEntity>(this IQueryable<TEntity> query) where TEntity : class
        {
            var queryCompiler = (QueryCompiler)QueryCompilerField.GetValue(query.Provider);
            var modelGenerator = (QueryModelGenerator)QueryModelGeneratorField.GetValue(queryCompiler);
            var queryModel = modelGenerator.ParseQuery(query.Expression);
            var database = (IDatabase)DataBaseField.GetValue(queryCompiler);
            var databaseDependencies = (DatabaseDependencies)DatabaseDependenciesField.GetValue(database);
            var queryCompilationContext = databaseDependencies.QueryCompilationContextFactory.Create(false);
            var modelVisitor = (RelationalQueryModelVisitor)queryCompilationContext.CreateQueryModelVisitor();
            modelVisitor.CreateQueryExecutor<TEntity>(queryModel);
            var sql = modelVisitor.Queries.First().ToString();

            return sql;
        }


        public static TransactionScope CreateNoLockTransaction()
        {
            var options = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted
            };
            return new TransactionScope(TransactionScopeOption.Required, options);
        }


        public static List<T> ToListNoLock<T>(this IQueryable<T> query)
        {
            using (TransactionScope ts = CreateNoLockTransaction())
            {
                return query.ToList();
            }
        }

        public static T SingleOrDefaultNoLock<T>(this IQueryable<T> query)
        {
            using (TransactionScope ts = CreateNoLockTransaction())
            {
                return query.SingleOrDefault();
            }
        }

        public static T FirstOrDefaultNoLock<T>(this IQueryable<T> query)
        {
            using (TransactionScope ts = CreateNoLockTransaction())
            {
                return query.FirstOrDefault();
            }
        }

        public static int CountNoLock<T>(this IQueryable<T> query)
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



        public static async Task<List<T>> ToListNoLockAsync<T>(this IQueryable<T> query)
        {
            using (TransactionScope ts = CreateNoLockTransaction())
            {
                return await query.ToListAsync();
            }
        }

        public static async Task<T> SingleOrDefaultNoLockAsync<T>(this IQueryable<T> query)
        {
            using (TransactionScope ts = CreateNoLockTransaction())
            {
                return await query.SingleOrDefaultAsync();
            }
        }

        public static async Task<T> FirstOrDefaultNoLockAsync<T>(this IQueryable<T> query)
        {
            using (TransactionScope ts = CreateNoLockTransaction())
            {
                return await query.FirstOrDefaultAsync();
            }
        }

        public static async Task<int> CountNoLockAsync<T>(this IQueryable<T> query)
        {
            using (var scope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions()
                {
                    IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted
                }))
            {
                int toReturn = await query.CountAsync();
                scope.Complete();
                return toReturn;
            }
        }
    }
}
