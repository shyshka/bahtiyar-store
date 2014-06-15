using System;
using System.Text;

namespace Bahtiar
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
    }
}
