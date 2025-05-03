using Infrastracture.Interceptors;
using Microsoft.EntityFrameworkCore;
using Model.Models;

namespace Infrastracture;

public class ApplicationDbContext : DbContext
{
	public DbSet<Company> Companies { get; set; } = default!;
	public DbSet<User> Users { get; set; } = default!;
	public DbSet<Folder> Folders { get; set; } = default!;
	
	public ApplicationDbContext() : base()
	{
		Database.EnsureCreated();
	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=space_rover;Username=postgres;Password=postgres");

		optionsBuilder.AddInterceptors(new SavingChangesInterceptor());
	}
}