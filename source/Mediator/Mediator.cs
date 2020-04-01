using DotNetCore.Results;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace DotNetCore.Mediator
{
    public sealed class Mediator : IMediator
    {
        private readonly IServiceProvider _serviceProvider;

        public Mediator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<IResult> HandleAsync<TRequest>(TRequest request) where TRequest : IRequest
        {
            var validation = await ValidateAsync(request).ConfigureAwait(false);

            if (validation.Failed)
            {
                return Result.Fail(validation.Message);
            }

            var handler = _serviceProvider.GetService<IRequestHandler<TRequest>>();

            if (handler == null)
            {
                throw new RequestHandlerNotFoundException(typeof(TRequest));
            }

            return await handler.HandleAsync(request).ConfigureAwait(false);
        }

        public async Task<IResult<TResponse>> HandleAsync<TRequest, TResponse>(TRequest request) where TRequest : IRequest
        {
            var validation = await ValidateAsync(request).ConfigureAwait(false);

            if (validation.Failed)
            {
                return Result<TResponse>.Fail(validation.Message);
            }

            var handler = _serviceProvider.GetService<IRequestHandler<TRequest, TResponse>>();

            if (handler == null)
            {
                throw new RequestHandlerNotFoundException(typeof(TRequest), typeof(TResponse));
            }

            return await handler.HandleAsync(request).ConfigureAwait(false);
        }

        private async Task<IResult> ValidateAsync<TRequest>(TRequest request) where TRequest : IRequest
        {
            var validator = _serviceProvider.GetService<AbstractValidator<TRequest>>();

            if (validator == null)
            {
                return Result.Success();
            }

            var validation = await validator.ValidateAsync(request).ConfigureAwait(false);

            return !validation.IsValid ? Result.Fail(validation.ToString()) : Result.Success();
        }
    }
}
