using LanguageExt;
using System;

namespace Exercises.Chapter6
{
    /// <summary>
    /// Write a function with signature
    /// 
    /// TryRun : (() -> T) -> Exceptional<T>
    /// 
    /// that runs the given function in a try/catch, returning an appropriately populated Exceptional
    /// </summary>
    internal class Exercise3
    {
        public static Exceptional<T> TryRun<T>(Func<T> throwingFunc)
        {
            try
            {
                return new Exceptional<T>(throwingFunc());
            }
            catch (Exception ex)
            {
                return new Exceptional<T>(ex);
            }
        }
    }

    public struct Exceptional<T>
    {
        internal Either<Exception, T> _inner { get; private set; }

        public Exceptional(T val)
        {
            _inner = Either<Exception, T>.Right(val);
        }

        public Exceptional(Exception ex)
        {
            _inner = Either<Exception, T>.Left(ex);
        }

        public TT Match<TT>(Func<T, TT> Success, Func<Exception, TT> Exception) =>
            _inner.Match(Right: Success, Left: Exception);

        public Unit Match(Action<T> Success, Action<Exception> Exception) =>
            _inner.Match(Right: Success, Left: Exception);
    }
}
