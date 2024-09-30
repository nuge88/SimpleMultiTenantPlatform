﻿using FluentValidation;
using MediatR;

namespace OrganizationService.Application.Common.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (!validators.Any()) return await next();
            var context = new ValidationContext<TRequest>(request);

            var validationResults = await Task.WhenAll(
                validators.Select(v => v.ValidateAsync(request, cancellationToken))
            );

            var failures = validationResults
                .SelectMany(r => r.Errors)
                .Where(f => f != null)
                .ToList();

            if (failures.Count != 0)
            {
                throw new ValidationException(failures);
            }

            return await next();
        }
    }
}