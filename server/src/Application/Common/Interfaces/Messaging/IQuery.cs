﻿using Domain.Primitives.Result;
using MediatR;

namespace Application.Common.Interfaces.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
