//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;

//namespace IF.Core.Resilience
//{
//    public static class Retry
//    {
//        public static void Do(
//            Action action,
//            TimeSpan retryInterval,
//            int maxAttemptCount = 3)
//        {
//            Do<object>(() =>
//            {
//                action();
//                return null;
//            }, retryInterval, maxAttemptCount);
//        }

//        public static T Do<T>(
//            Func<T> action,
//            TimeSpan retryInterval,
//            int maxAttemptCount = 3)
//        {
//            var exceptions = new List<System.Exception>();

//            for (int attempted = 0; attempted < maxAttemptCount; attempted++)
//            {
//                try
//                {
//                    if (attempted > 0)
//                    {
//                        Thread.Sleep(retryInterval);
//                    }
//                    return action();
//                }
//                catch (System.Exception ex)
//                {
//                    exceptions.Add(ex);
//                }
//            }

//            throw new AggregateException(exceptions);
//        }

//        public static async Task<T> DoAsync<T>(Func<dynamic> action, TimeSpan retryInterval, int retryCount = 3)
//        {
//            var exceptions = new List<System.Exception>();

//            for (int retry = 0; retry < retryCount; retry++)
//            {
//                try
//                {
//                    return await action().ConfigureAwait(false);
//                }
//                catch (System.Exception ex)
//                {
//                    exceptions.Add(ex);
//                }

//                await Task.Delay(retryInterval).ConfigureAwait(false);
//            }
//            throw new AggregateException(exceptions);
//        }
//    }
//}
