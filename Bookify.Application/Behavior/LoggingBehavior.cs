﻿using Bookify.Application.Abstractions.Messaging;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Application.Behavior
{
    public class LoggingBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IBaseCommand
    {
        private readonly ILogger<TRequest> _logger;

        public LoggingBehavior(ILogger<TRequest> logger)
        {
            _logger = logger;
            //adding logging
        }

        public async Task<TResponse> Handle
            (TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var name = request.GetType().Name;

            try
            {
                _logger.LogInformation("Executing command {Command}", name);

                var result = await next();

                _logger.LogInformation("Command {Command} processed successfully", name);

                return result;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Command {Command} processing failed", name);

                throw;
            }
        }
    }
}
