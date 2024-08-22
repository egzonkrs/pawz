using Pawz.Domain.Entities;
using System.Collections.Generic;

namespace Pawz.Domain.Common
{
    public class Result<T>
    {
        private readonly List<Error> _errors = new();
        public bool IsSuccess { get; }
        public T? Value { get; }
        public IReadOnlyList<Error> Errors => _errors.AsReadOnly();

        private Result(bool isSuccess, T value, IEnumerable<Error> errors)
        {
            IsSuccess = isSuccess;
            Value = value;

            if (errors is not null)
            {
                _errors.AddRange(errors);
            }
        }

        public static Result<T> Success(T value = default)
        {
            return new Result<T>(true, value, null);
        }

        public static Result<T> Failure(string error)
        {
            return new Result<T>(false, default, new List<Error> { new Error("General.Error", error) });
        }

        public static Result<T> Failure(params Error[] errors)
        {
            return new Result<T>(false, default, errors);
        }
    }
}
