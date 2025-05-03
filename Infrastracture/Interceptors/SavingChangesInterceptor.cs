using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Model.Models;

namespace Infrastracture.Interceptors;

public class SavingChangesInterceptor : SaveChangesInterceptor
{
	public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result,
		CancellationToken cancellationToken = new CancellationToken())
	{
		var dbContext = eventData.Context;
		ArgumentNullException.ThrowIfNull(dbContext);

		var currentDateTimeUtc = DateTimeOffset.UtcNow;

		var entities = dbContext.ChangeTracker.Entries()
			.Where(entity => entity.Entity is ICreatedAt or IUpdatedAt).ToList();

		foreach (var entity in entities)
		{
			if (entity.State == EntityState.Added)
			{
				entity.Property(nameof(ICreatedAt.CreatedAt)).CurrentValue = currentDateTimeUtc;
				entity.Property(nameof(IUpdatedAt.UpdatedAt)).CurrentValue = currentDateTimeUtc;
			}
			else if (entity.State == EntityState.Modified)
			{
				entity.Property(nameof(IUpdatedAt.UpdatedAt)).CurrentValue = currentDateTimeUtc;
			}
		}
		
		return base.SavingChangesAsync(eventData, result, cancellationToken);
	}
}