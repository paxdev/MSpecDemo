using System;
using System.Threading.Tasks;
using Machine.Fakes;

namespace MSpecDemo._2._Async
{
    public static class ReturnAsyncExtensions
    {
        public static void ReturnAsync<TReturn>(this IQueryOptions<Task<TReturn>> queryOptions, TReturn returnValue)
            => queryOptions.Return(Task.FromResult(returnValue));

        public static void ReturnAsync<TReturn>(this IQueryOptions<Task<TReturn>> queryOptions, Func<TReturn> valueFunction)
            => queryOptions.Return(() => Task.FromResult(valueFunction()));

        public static void ReturnAsync<TReturn, T>(this IQueryOptions<Task<TReturn>> queryOptions,
                                                   Func<T, TReturn> valueFunction)
            => queryOptions.Return((T arg) => Task.FromResult(valueFunction(arg)));

        public static void ReturnAsync<TReturn, T1, T2>(this IQueryOptions<Task<TReturn>> queryOptions,
                                                        Func<T1, T2, TReturn> valueFunction)
            => queryOptions.Return((T1 arg1, T2 arg2) => Task.FromResult(valueFunction(arg1, arg2)));

        public static void ReturnAsync<TReturn, T1, T2, T3>(this IQueryOptions<Task<TReturn>> queryOptions,
                                                            Func<T1, T2, T3, TReturn> valueFunction)
            => queryOptions.Return((T1 arg1, T2 arg2, T3 arg3) => Task.FromResult(valueFunction(arg1, arg2, arg3)));

        public static void ReturnAsync<TReturn, T1, T2, T3, T4>(this IQueryOptions<Task<TReturn>> queryOptions,
                                                                Func<T1, T2, T3, T4, TReturn> valueFunction)
            => queryOptions.Return((T1 arg1, T2 arg2, T3 arg3, T4 arg4) => Task.FromResult(valueFunction(arg1, arg2, arg3, arg4)));
    }
}