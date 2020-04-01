using DotNetCore.Results;
using FluentValidation;
using System.Threading;
using System.Threading.Tasks;

namespace DotNetCore.Validation
{
    public abstract class Validator<T> : AbstractValidator<T>
    {
        public new async Task<IResult> ValidateAsync(T instance, CancellationToken cancellation = default)
        {
            if (instance == null)
            {
                return Result.Fail();
            }

            var result = await base.ValidateAsync(instance, cancellation);

            if (result.IsValid)
            {
                return Result.Success();
            }

            return Result.Fail(result.ToString());
        }
    }
}
