using IF.Core.Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IF.Persistence
{
    public static class IRepositoryExtensions
    {
        public static List<T> ToListNoLock<T>(this IQueryable<T> query, IRepository repository)
        {
            return repository.ToListNoLock(query);
        }

        public static T SingleOrDefaultNoLock<T>(this IQueryable<T> query, IRepository repository)
        {
            return repository.SingleOrDefaultNoLock(query);
        }

        public static T FirstOrDefaultNoLock<T>(this IQueryable<T> query, IRepository repository)
        {
            
               return repository.FirstOrDefaultNoLock(query);
            
        }

        public static int CountNoLock<T>(this IQueryable<T> query, IRepository repository)
        {
            return repository.CountNoLock(query);
        }

        ////async



        public static async Task<List<T>> ToListNoLockAsync<T>(this IQueryable<T> query, IRepository repository)
        {
            return await repository.ToListNoLockAsync(query);
        }

        public static async Task<T> SingleOrDefaultNoLockAsync<T>(this IQueryable<T> query, IRepository repository)
        {
            return await repository.SingleOrDefaultNoLockAsync(query);
        }

        public static async Task<T> FirstOrDefaultNoLockAsync<T>(this IQueryable<T> query, IRepository repository)
        {
            return await repository.FirstOrDefaultNoLockAsync(query);
        }

        public static async Task<int> CountNoLockAsync<T>(this IQueryable<T> query, IRepository repository)
        {
            return await repository.CountNoLockAsync(query);
        }
    }
}
