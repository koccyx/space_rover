using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model.Models;

namespace Infrastracture.Configurations;

public class FolderConfiguration : IEntityTypeConfiguration<Folder>
{
	public void Configure(EntityTypeBuilder<Folder> builder)
	{
		ArgumentNullException.ThrowIfNull(builder);

		builder.HasKey(c => c.Id);

		builder.
			HasOne(f => f.Company)
			.WithMany()
			.HasForeignKey(f => f.CompanyId);
	}
}