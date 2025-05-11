using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model.Models;

namespace Infrastracture.Configurations;

public class FileConfiguration : IEntityTypeConfiguration<Model.Models.File>
{
	public void Configure(EntityTypeBuilder<Model.Models.File> builder)
	{
		ArgumentNullException.ThrowIfNull(builder);

		builder.HasKey(c => c.Id);

		builder.
			HasOne(f => f.Folder)
			.WithMany()
			.HasForeignKey(f => f.FolderId);
		
		builder.
        			HasOne(f => f.User)
        			.WithMany()
        			.HasForeignKey(f => f.UserId);
	}
}