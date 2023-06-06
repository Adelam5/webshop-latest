﻿using Domain.Primitives.Result;
using MediatR;

namespace Application.Common.Interfaces.Messaging;

public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, Result>
    where TCommand : ICommand
{
}

public interface ICommandHandler<TCommand, TResponse>
    : IRequestHandler<TCommand, Result<TResponse>>
   where TCommand : ICommand<TResponse>
{
}
