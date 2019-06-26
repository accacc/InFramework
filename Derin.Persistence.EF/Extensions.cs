using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Derin.Persistence.EF
{
    public static class IEnumerableExtension
    {
        public static TransactionScope CreateNoLockTransaction()
        {
            var options = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted
            };
            return new TransactionScope(TransactionScopeOption.Required, options);
        }

        public static List<T> ToListNoLock<T>(this IEnumerable<T> query)
        {
            using (TransactionScope ts = CreateNoLockTransaction())
            {
                return query.ToList();
            }
        }

        public static T SingleOrDefaultNoLock<T>(this IEnumerable<T> query)
        {
            using (TransactionScope ts = CreateNoLockTransaction())
            {
                return query.SingleOrDefault();
            }
        }

        public static T FirstOrDefaultNoLock<T>(this IEnumerable<T> query)
        {
            using (TransactionScope ts = CreateNoLockTransaction())
            {
                return query.FirstOrDefault();
            }
        }
    }
}
