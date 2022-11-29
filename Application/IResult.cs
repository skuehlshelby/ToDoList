using System;

namespace Application
{
    internal interface IResult
    {
        bool IsSuccess { get; }

        bool IsError { get; }

        string Error { get; } // Replace this with an Error object!
    }

    internal interface IResult<T> : IResult
    {
        IResult<K> Transform<K>(Func<T, K> transformation);

        void Match(Action<T> success, Action<string> failure);

        void OnSuccess(Action<T> success);

        void OnFailure(Action<string> failure);

        void OnBoth(Action action);
    }

    internal static class Result
    {
        private sealed class Success<T> : IResult<T>
        {
            private readonly T value;

            public Success(T value)
            {
                this.value = value;
            }

            public bool IsSuccess => true;

            public bool IsError => false;

            public string Error => string.Empty;

            public IResult<K> Transform<K>(Func<T, K> transformation)
            {
                try
                {
                    return new Success<K>(transformation.Invoke(value));
                }
                catch (Exception ex)
                {
                    return new FailureWrapper<K>(Fail(ex.Message));
                }
            }

            public void Match(Action<T> success, Action<string> failure) => success?.Invoke(value);

            public void OnBoth(Action action) => action?.Invoke();

            public void OnFailure(Action<string> failure)
            {
                return;
            }

            public void OnSuccess(Action<T> success) => success?.Invoke(value);
        }

        private sealed class Failure : IResult
        {
            public Failure(string error)
            {
                Error = error;
            }

            public bool IsSuccess => false;

            public bool IsError => true;

            public string Error { get; }
        }

        private sealed class FailureWrapper<T> : IResult<T>
        {
            private readonly IResult result;

            public FailureWrapper(IResult result)
            {
                this.result = result;
            }

            public bool IsSuccess => result.IsSuccess;

            public bool IsError => result.IsError;

            public string Error => result.Error;

            public IResult<K> Transform<K>(Func<T, K> transformation) => new FailureWrapper<K>(result);

            public void Match(Action<T> success, Action<string> failure) => failure?.Invoke(Error);

            public void OnBoth(Action action) => action?.Invoke();

            public void OnFailure(Action<string> failure) => failure?.Invoke(Error);

            public void OnSuccess(Action<T> success)
            {
                return;
            }
        }

        public static IResult<T> Ok<T>(T value)
        {
            return new Success<T>(value);
        }

        public static IResult Fail(string error)
        {
            return new Failure(error);
        }
    }
}