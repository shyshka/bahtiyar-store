using System;

namespace Bahtiar.Helper
{
    public static class Extensions
    {
        public static TOutput With<TInput, TOutput>(this TInput input, Func<TInput, TOutput> evaluator)
            where TInput : class 
            where TOutput : class
        {
            return input == null ? null : evaluator(input);
        }

        public static TOutput Return<TInput, TOutput>(this TInput input, Func<TInput, TOutput> evaluator, TOutput failureValue)
            where TInput : class
            where TOutput : class
        {
            return input == null ? failureValue : evaluator(input);
        }

        public static TInput If<TInput>(this TInput input, Func<TInput, bool> evaluator)
            where TInput : class
        {
            if (input == null) return null;
            return evaluator(input) ? input : null;
        }

        public static void Do<TInput>(this TInput input, Action action)
            where TInput : class
        {
            if (input == null)
                return;
            action();
        }
    }
}
