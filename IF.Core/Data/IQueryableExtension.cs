//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Transactions;

//namespace IF.Core.Data
//{
//    public static class IQueryableExtension
//    {
//        public static TransactionScope CreateNoLockTransaction()
//        {
//            var options = new TransactionOptions
//            {
//                IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted
//            };
//            return new TransactionScope(TransactionScopeOption.Required, options);
//        }

//        public static List<T> ToListNoLock<T>(this IQueryable<T> query)
//        {
//            using (TransactionScope ts = CreateNoLockTransaction())
//            {
//                return query.ToList();
//            }
//        }

//        public static T SingleOrDefaultNoLock<T>(this IQueryable<T> query)
//        {
//            using (TransactionScope ts = CreateNoLockTransaction())
//            {
//                return query.SingleOrDefault();
//            }
//        }

//        public static T FirstOrDefaultNoLock<T>(this IQueryable<T> query)
//        {
//            using (TransactionScope ts = CreateNoLockTransaction())
//            {
//                return query.FirstOrDefault();
//            }
//        }

//        public static int CountNoLock<T>(this IQueryable<T> query)
//        {
//            using (var scope = new TransactionScope(
//                TransactionScopeOption.Required,
//                new TransactionOptions()
//                {
//                    IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted
//                }))
//            {
//                int toReturn = query.Count();
//                scope.Complete();
//                return toReturn;
//            }
//        }
//    }
//}
