using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model.Models;

namespace Infrastracture.Configurations;

public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
	public void Configure(EntityTypeBuilder<Company> builder)
	{
		ArgumentNullException.ThrowIfNull(builder);

		builder.HasKey(c => c.Id);
	}
}