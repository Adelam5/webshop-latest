﻿namespace Domain.Abstractions.Interfaces;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}