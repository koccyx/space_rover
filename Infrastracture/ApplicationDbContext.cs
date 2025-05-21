using System.Data.Entity;
using System.Reflection;
using System.Transactions;
using Infrastracture.Configurations;
using Infrastracture.Interceptors;
using Microsoft.EntityFrameworkCore;
using Model.Models;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace Infrastracture;

public class ApplicationDbContext : DbContext
{
	public Microsoft.EntityFrameworkCore.DbSet<Company> Companies { get; set; } = default!;
	public Microsoft.EntityFrameworkCore.DbSet<User> Users { get; set; } = default!;
	public Microsoft.EntityFrameworkCore.DbSet<Folder> Folders { get; set; } = default!;
	public Microsoft.EntityFrameworkCore.DbSet<Model.Models.File> Files { get; set; } = default!;
	public Microsoft.EntityFrameworkCore.DbSet<Model.Models.FileDetailsView> FileDetailsViews { get; set; } = default!;
	public Microsoft.EntityFrameworkCore.DbSet<Model.Models.UserFileView> UserFileViews { get; set; } = default!;
	

	[Microsoft.EntityFrameworkCore.DbFunction("get_percents_of_used_storage", "public")]
	public IQueryable<UsedStorageStatistics> GetCompanyStorageStatistics(Guid companyId) => throw new Exception("Method not supported");
	public ApplicationDbContext() : base()
	{
		Database.EnsureCreated();
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		
		modelBuilder.Entity<UsedStorageStatistics>().HasNoKey();	
		modelBuilder.Entity<UploadsByDayStatistics>().HasNoKey();	
	}
	
	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=space_rover;Username=postgres;Password=postgres");

		optionsBuilder.AddInterceptors(new SavingChangesInterceptor());
	}
}