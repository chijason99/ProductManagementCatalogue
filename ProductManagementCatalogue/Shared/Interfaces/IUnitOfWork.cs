﻿namespace ProductManagementCatalogue.Domain.Shared.Interfaces;

public interface IUnitOfWork : IDisposable
{
	Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}