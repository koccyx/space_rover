using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model.Models;

namespace Infrastracture.Configurations;

public class UserFileView : IEntityTypeConfiguration<Model.Models.UserFileView>
{
	public void Configure(EntityTypeBuilder<Model.Models.UserFileView> builder)
	{
		ArgumentNullException.ThrowIfNull(builder);

		builder.HasNoKey().ToView("UserFileStatsView");
	}
}