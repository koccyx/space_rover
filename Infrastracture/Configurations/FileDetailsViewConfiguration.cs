using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model.Models;

namespace Infrastracture.Configurations;

public class FileDetailsView : IEntityTypeConfiguration<Model.Models.FileDetailsView>
{
	public void Configure(EntityTypeBuilder<Model.Models.FileDetailsView> builder)
	{
		ArgumentNullException.ThrowIfNull(builder);

		builder.ToView("FileDetailsView")
			.HasKey(x => x.Id);
	}
}