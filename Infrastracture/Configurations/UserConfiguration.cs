using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model.Models;

namespace Infrastracture.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
	public void Configure(EntityTypeBuilder<User> builder)
	{
		ArgumentNullException.ThrowIfNull(builder);

		builder.HasKey(c => c.Id);

		builder.
			HasOne(u => u.Company)
			.WithMany()
			.HasForeignKey(u => u.CompanyId);
	}
}